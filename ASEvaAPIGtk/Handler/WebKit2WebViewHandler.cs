
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Eto.Forms;
using Eto.GtkSharp;
using Eto.GtkSharp.Forms;

namespace ASEva.UIGtk
{
	class WebViewHandler : GtkControl<Gtk.ScrolledWindow, WebView, WebView.ICallback>, WebView.IHandler
	{
		public bool BrowserContextMenuEnabled { get; set; }

		public string DocumentTitle
		{
			get { return WebViewHandle == IntPtr.Zero ? "" : NativeMethods.webkit_web_view_get_title(WebViewHandle); }
		}

		public Uri Url
		{
			get { return WebViewHandle == IntPtr.Zero ? null : new Uri(NativeMethods.webkit_web_view_get_uri(WebViewHandle)); }
			set
			{
				if (WebViewHandle == IntPtr.Zero)
				{
					if (value != null && !String.IsNullOrEmpty(value.AbsoluteUri)) targetUrl = value.AbsoluteUri;
				}
				else
				{
					if (value != null) NativeMethods.webkit_web_view_load_uri(WebViewHandle, value.AbsoluteUri);
					else NativeMethods.webkit_web_view_load_html(WebViewHandle, "", "");
				}
			}
		}

		public override Gtk.Widget ContainerControl
		{
			get { return Control; }
		}

		IntPtr WebViewHandle
		{
			get
			{
				return webView == null ? IntPtr.Zero : webView.Handle;
			}
		}

		EventHandler<WebViewTitleEventArgs> titleChanged;
		EventHandler<WebViewLoadedEventArgs> navigated;
		EventHandler<WebViewLoadedEventArgs> documentLoaded;
		EventHandler<WebViewLoadingEventArgs> documentLoading;
		EventHandler<WebViewNewWindowEventArgs> openNewWindow;

		Queue<TaskCompletionSource<string>> jscs;
		Gtk.Widget webView;
		string targetUrl;
		string targetHtmlString;
		string targetBaseUrl;

		public WebViewHandler()
		{
			Control = new Gtk.ScrolledWindow();
			Control.Realized += delegate
			{
				var runningOS = ASEva.UIEto.App.GetRunningOS();
				var uiBackend = ASEva.UIEto.App.GetUIBackend();
				if (runningOS == "linuxarm" && uiBackend != null && uiBackend == "wayland")
				{
					var settings = NativeMethods.webkit_settings_new();
					NativeMethods.webkit_settings_set_enable_accelerated_2d_canvas(settings, false);
					NativeMethods.webkit_settings_set_enable_webgl(settings, false);
					NativeMethods.webkit_settings_set_hardware_acceleration_policy(settings, 2/* WEBKIT_HARDWARE_ACCELERATION_POLICY_NEVER */);
					webView = new Gtk.Widget(NativeMethods.webkit_web_view_new_with_settings(settings)) { Visible = true };
				}
				else
				{
					webView = new Gtk.Widget(NativeMethods.webkit_web_view_new()) { Visible = true };
				}
				Control.Add(webView);

				webView.AddSignalHandler(
					"context-menu",
					(Action<object, GLib.SignalArgs>)WebViewHandler_ContextMenu,
					typeof(GLib.SignalArgs)
				);

				webView.AddSignalHandler(
					"notify::title",
					(Action<object, GLib.SignalArgs>)WebViewHandler_TitleChanged,
					typeof(GLib.SignalArgs)
				);

				webView.AddSignalHandler(
					"load-changed",
					(Action<object, GLib.SignalArgs>)WebViewHandler_LoadChanged,
					typeof(GLib.SignalArgs)
				);

				webView.AddSignalHandler(
					"decide-policy",
					(Action<object, GLib.SignalArgs>)WebViewHandler_DecidePolicy,
					typeof(GLib.SignalArgs)
				);

				if (targetUrl != null)
				{
					NativeMethods.webkit_web_view_load_uri(WebViewHandle, targetUrl);
					targetUrl = null;
				}
				if (targetHtmlString != null)
				{
					NativeMethods.webkit_web_view_load_html(WebViewHandle, targetHtmlString, targetBaseUrl);
					targetHtmlString = null;
					targetBaseUrl = null;
				}
			};
		}

		private void WebViewHandler_TitleChanged(object o, GLib.SignalArgs args)
		{
			titleChanged?.Invoke(this, new WebViewTitleEventArgs(DocumentTitle));
		}

		private void WebViewHandler_ContextMenu(object o, GLib.SignalArgs args)
		{
			args.RetVal = !BrowserContextMenuEnabled;
		}

		private void WebViewHandler_LoadChanged(object o, GLib.SignalArgs args)
		{
			var loadEvent = (int)args.Args[0];

			switch (loadEvent)
			{
				case 2: // WEBKIT_LOAD_COMMITTED
					navigated?.Invoke(this, new WebViewLoadedEventArgs(Url));
					break;
				case 3: // WEBKIT_LOAD_FINISHED
					documentLoaded?.Invoke(this, new WebViewLoadedEventArgs(Url));
					break;
			}
		}

		[GLib.ConnectBefore]
		private void WebViewHandler_DecidePolicy(object o, GLib.SignalArgs args)
		{
			var decision = (GLib.Object)args.Args[0];
			var type = (int)args.Args[1];

			if (type == 0) // WEBKIT_POLICY_DECISION_TYPE_NAVIGATION_ACTION
			{
				var request = NativeMethods.webkit_navigation_policy_decision_get_request(decision.Handle);
				var uriString = NativeMethods.webkit_uri_request_get_uri(request);
				var uri = String.IsNullOrEmpty(uriString) ? null : new Uri(uriString);

				var loadingArgs = new WebViewLoadingEventArgs(uri, true);
				documentLoading?.Invoke(this, loadingArgs);
				args.RetVal = loadingArgs.Cancel;
			}
			else if (type == 1) // WEBKIT_POLICY_DECISION_TYPE_NEW_WINDOW_ACTION
			{
				var request = NativeMethods.webkit_navigation_policy_decision_get_request(decision.Handle);
				var uriString = NativeMethods.webkit_uri_request_get_uri(request);
				var uri = String.IsNullOrEmpty(uriString) ? null : new Uri(uriString);

				var newWindowArgs = new WebViewNewWindowEventArgs(uri, "");
				openNewWindow?.Invoke(this, newWindowArgs);
				args.RetVal = newWindowArgs.Cancel;
			}
			else if (type == 2)
			{
				var request = NativeMethods.webkit_response_policy_decision_get_request(decision.Handle);
				var uriString = NativeMethods.webkit_uri_request_get_uri(request);
				var mimeSupport = NativeMethods.webkit_response_policy_decision_is_mime_type_supported(decision.Handle);

				if (!mimeSupport && !String.IsNullOrEmpty(uriString))
				{
					GLib.Timeout.Add(1, timer_Timeout);
					NativeMethods.webkit_policy_decision_download(decision.Handle);
					args.RetVal = true;
					return;
				}
			}
		}

        private bool timer_Timeout()
        {
			var userDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			var downloadDirName = new String[] {"Downloads", "Download", "下载"};
			String targetDir = null;
			foreach (var dirName in downloadDirName)
			{
				var downloadDir = userDir + "/" + dirName;
				if (Directory.Exists(downloadDir)) targetDir = downloadDir;
			}
			if (targetDir != null)
			{
				try
				{
					var startInfo = new ProcessStartInfo();
					startInfo.FileName = targetDir;
					startInfo.WorkingDirectory = targetDir;
					startInfo.UseShellExecute = true;
					Process.Start(startInfo);
				}
				catch (Exception) { }
			}
            return false;
        }

        public override void AttachEvent(string id)
		{
			switch (id)
			{
				case WebView.NavigatedEvent:
					navigated += (sender, e) => Callback.OnNavigated(Widget, e);
					break;
				case WebView.DocumentLoadedEvent:
					documentLoaded += (sender, e) => Callback.OnDocumentLoaded(Widget, e);
					break;
				case WebView.DocumentLoadingEvent:
					documentLoading += (sender, e) => Callback.OnDocumentLoading(Widget, e);
					break;
				case WebView.OpenNewWindowEvent:
					openNewWindow += (sender, e) => Callback.OnOpenNewWindow(Widget, e);
					break;
				case WebView.DocumentTitleChangedEvent:
					titleChanged += (sender, e) => Callback.OnDocumentTitleChanged(Widget, e);
					break;
				default:
					base.AttachEvent(id);
					break;
			}
		}

		public string ExecuteScript(string script)
		{
			if (WebViewHandle == IntPtr.Zero) return null;

			var task = ExecuteScriptAsync(script);

			while (!task.IsCompleted)
			{
				Gtk.Application.RunIteration();
			}

			return task.Result;
		}

		private Delegate theDelegate = null;

		public Task<string> ExecuteScriptAsync(string script)
		{
			if (WebViewHandle == IntPtr.Zero) return null;

			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
			if (jscs == null)
				jscs = new Queue<TaskCompletionSource<string>>();
			jscs.Enqueue(tcs);

			if (theDelegate == null) theDelegate = (Delegate)(FinishScriptExecutionDelegate)FinishScriptExecution;
			NativeMethods.webkit_web_view_run_javascript(WebViewHandle, $"function _fn() {{{script}}} _fn();", IntPtr.Zero, (FinishScriptExecutionDelegate)theDelegate, IntPtr.Zero);

			return tcs.Task;
		}

		delegate void FinishScriptExecutionDelegate(IntPtr webview, IntPtr result, IntPtr error);

		private void FinishScriptExecution(IntPtr webview, IntPtr result, IntPtr error)
		{
			var jsresult = NativeMethods.webkit_web_view_run_javascript_finish(WebViewHandle, result, IntPtr.Zero);
			var tcs = jscs?.Count > 0 ? jscs?.Dequeue() : null;
			if (jsresult != IntPtr.Zero)
			{
				var context = NativeMethods.webkit_javascript_result_get_global_context(jsresult);
				var value = NativeMethods.webkit_javascript_result_get_value(jsresult);

				var strvalue = NativeMethods.JSValueToStringCopy(context, value, IntPtr.Zero);
				var strlen = NativeMethods.JSStringGetMaximumUTF8CStringSize(strvalue);
				var utfvalue = Marshal.AllocHGlobal(strlen);
				NativeMethods.JSStringGetUTF8CString(strvalue, utfvalue, strlen);
				var jsreturn = NativeMethods.GetString(utfvalue);

				Marshal.FreeHGlobal(utfvalue);
				NativeMethods.JSStringRelease(strvalue);
				NativeMethods.webkit_javascript_result_unref(jsresult);
				tcs?.SetResult(jsreturn);
			}
			else
			{
				tcs?.SetResult(null);
			}
		}

		public void LoadHtml(string html, Uri baseUri)
		{
			if (WebViewHandle == IntPtr.Zero)
			{
				targetHtmlString = html;
				targetBaseUrl = baseUri?.AbsoluteUri ?? "";
			}
			else
			{
				NativeMethods.webkit_web_view_load_html(WebViewHandle, html, baseUri?.AbsoluteUri ?? "");
			}
		}

		public void Stop()
		{
			if (WebViewHandle != IntPtr.Zero) NativeMethods.webkit_web_view_stop_loading(WebViewHandle);
		}

		public void Reload()
		{
			if (WebViewHandle != IntPtr.Zero) NativeMethods.webkit_web_view_reload(WebViewHandle);
		}

		public void GoBack()
		{
			if (WebViewHandle != IntPtr.Zero) NativeMethods.webkit_web_view_go_back(WebViewHandle);
		}

		public void GoForward()
		{
			if (WebViewHandle != IntPtr.Zero) NativeMethods.webkit_web_view_go_forward(WebViewHandle);
		}

        public bool CanGoBack
		{
			get { return WebViewHandle == IntPtr.Zero ? false : NativeMethods.webkit_web_view_can_go_back(WebViewHandle); }
		}

		public bool CanGoForward
		{
			get { return WebViewHandle == IntPtr.Zero ? false : NativeMethods.webkit_web_view_can_go_forward(WebViewHandle); }
		}

		public void ShowPrintDialog()
		{
			if (WebViewHandle != IntPtr.Zero) NativeMethods.webkit_web_view_run_javascript(WebViewHandle, "print();", IntPtr.Zero, null, IntPtr.Zero);
		}
	}
}