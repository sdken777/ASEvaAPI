#if GTKCORE || GTK3
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text;
using Eto.Forms;
using Eto.GtkSharp;
using Eto.GtkSharp.Forms;

namespace ASEva.UIGtk
{
	// CHECK: 大量修正和优化，官方Handler仅作为参考，更新部分根据情况合并至此实现
	class WebViewHandler : GtkControl<Gtk.ScrolledWindow, WebView, WebView.ICallback>, WebView.IHandler
	{
		public bool BrowserContextMenuEnabled { get; set; }

		public string DocumentTitle
		{
			get { return WebViewHandle == IntPtr.Zero ? "" : NativeMethods.webkit_web_view_get_title(WebViewHandle); }
		}

		public Uri Url
		{
			get
			{
				String uriString = WebViewHandle == IntPtr.Zero ? null : NativeMethods.webkit_web_view_get_uri(WebViewHandle);
				return String.IsNullOrEmpty(uriString) ? null : new Uri(uriString);
			}
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

		IntPtr WebViewHandle
		{
			get
			{
				return webView == null ? IntPtr.Zero : webView.Handle;
			}
		}

		public override Gtk.Widget ContainerControl
		{
			get { return Control; }
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

		private static IntPtr webViewGroup = IntPtr.Zero;

		public WebViewHandler()
		{
			Control = new Gtk.ScrolledWindow();
			Control.Realized += delegate
			{
				var settings = NativeMethods.webkit_settings_new();

				var uiBackend = ASEva.UIEto.App.GetUIBackend();
				if (uiBackend != null && uiBackend == "wayland") // Wayland下使用OpenGL可能导致花屏，或令WaylandOffscreenView卡死
				{
					NativeMethods.webkit_settings_set_enable_accelerated_2d_canvas(settings, false);
					NativeMethods.webkit_settings_set_enable_webgl(settings, false);
					NativeMethods.webkit_settings_set_hardware_acceleration_policy(settings, 2/* WEBKIT_HARDWARE_ACCELERATION_POLICY_NEVER */);
				}
				
				var settingsObject = GLib.Object.GetObject(settings);
				settingsObject.SetProperty("enable-developer-extras", new GLib.Value(true));

				webView = new Gtk.Widget(NativeMethods.webkit_web_view_new_with_settings(settings)) { Visible = true };

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

			Control.KeyPressEvent += (o, args) =>
			{
				if (webView != null && args.Event.Key == Gdk.Key.F12)
				{
					var inspector = NativeMethods.webkit_web_view_get_inspector(WebViewHandle);
					if (inspector != IntPtr.Zero) NativeMethods.webkit_web_inspector_show(inspector);
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
			var url = Url;
			if (url == null) return;

			var loadEvent = (int)args.Args[0];

			switch (loadEvent)
			{
				case 2: // WEBKIT_LOAD_COMMITTED
					navigated?.Invoke(this, new WebViewLoadedEventArgs(url));
					break;
				case 3: // WEBKIT_LOAD_FINISHED
					documentLoaded?.Invoke(this, new WebViewLoadedEventArgs(url));
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

		static class NativeMethods
		{
			static class NM4
			{
				public const string libwebkit = "libwebkit2gtk-4.0.so.37";

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_web_view_new();

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_web_view_new_with_settings(IntPtr settings);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_settings_new();

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_settings_set_enable_accelerated_2d_canvas(IntPtr settings, bool enable);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_settings_set_enable_webgl(IntPtr settings, bool enable);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_settings_set_hardware_acceleration_policy(IntPtr settings, int policy);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_web_view_load_uri(IntPtr web_view, string uri);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_web_view_get_uri(IntPtr web_view);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_web_view_load_html(IntPtr web_view, string content, string base_uri);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_web_view_get_title(IntPtr web_view);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_web_view_reload(IntPtr web_view);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_web_view_stop_loading(IntPtr web_view);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static bool webkit_web_view_can_go_back(IntPtr web_view);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_web_view_go_back(IntPtr web_view);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static bool webkit_web_view_can_go_forward(IntPtr web_view);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_web_view_go_forward(IntPtr web_view);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_web_view_run_javascript(IntPtr web_view, string script, IntPtr cancellable, Delegate callback, IntPtr user_data);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_web_view_run_javascript_finish(IntPtr web_view, IntPtr result, IntPtr error);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_javascript_result_get_global_context(IntPtr js_result);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_javascript_result_get_value(IntPtr js_result);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr JSValueToStringCopy(IntPtr context, IntPtr value, IntPtr idk);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static int JSStringGetMaximumUTF8CStringSize(IntPtr js_str_value);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void JSStringGetUTF8CString(IntPtr js_str_value, IntPtr str_value, int str_length);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void JSStringRelease(IntPtr js_str_value);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_navigation_policy_decision_get_request(IntPtr decision);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_uri_request_get_uri(IntPtr request);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static bool webkit_response_policy_decision_is_mime_type_supported(IntPtr decision);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_policy_decision_download(IntPtr decision);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_response_policy_decision_get_request(IntPtr decision);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_javascript_result_unref(IntPtr result);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static IntPtr webkit_web_view_get_inspector(IntPtr web_view);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_web_inspector_show(IntPtr inspector);

				[DllImport(libwebkit, CallingConvention = CallingConvention.Cdecl)]
				public extern static void webkit_web_inspector_detach(IntPtr inspector);
			}

			public static string GetString(IntPtr handle)
			{
				if (handle == IntPtr.Zero)
					return "";

				int len = 0;
				while (Marshal.ReadByte(handle, len) != 0)
					len++;

				var bytes = new byte[len];
				Marshal.Copy(handle, bytes, 0, bytes.Length);
				return Encoding.UTF8.GetString(bytes);
			}

			public static IntPtr webkit_web_view_new()
			{
				return NM4.webkit_web_view_new();
			}

			public static IntPtr webkit_web_view_new_with_settings(IntPtr settings)
			{
				return NM4.webkit_web_view_new_with_settings(settings);
			}

			public static IntPtr webkit_settings_new()
			{
				return NM4.webkit_settings_new();
			}

			public static void webkit_settings_set_enable_accelerated_2d_canvas(IntPtr settings, bool enable)
			{
				NM4.webkit_settings_set_enable_accelerated_2d_canvas(settings, enable);
			}

			public static void webkit_settings_set_enable_webgl(IntPtr settings, bool enable)
			{
				NM4.webkit_settings_set_enable_webgl(settings, enable);
			}

			public static void webkit_settings_set_hardware_acceleration_policy(IntPtr settings, int policy)
			{
				NM4.webkit_settings_set_hardware_acceleration_policy(settings, policy);
			}

			public static void webkit_web_view_load_uri(IntPtr web_view, string uri)
			{
				NM4.webkit_web_view_load_uri(web_view, uri);
			}

			public static string webkit_web_view_get_uri(IntPtr web_view)
			{
				return GetString(NM4.webkit_web_view_get_uri(web_view));
			}

			public static void webkit_web_view_load_html(IntPtr web_view, string content, string base_uri)
			{
				NM4.webkit_web_view_load_html(web_view, content, base_uri);
			}

			public static string webkit_web_view_get_title(IntPtr web_view)
			{
				return GetString(NM4.webkit_web_view_get_title(web_view));
			}

			public static void webkit_web_view_reload(IntPtr web_view)
			{
				NM4.webkit_web_view_reload(web_view);
			}

			public static void webkit_web_view_stop_loading(IntPtr web_view)
			{
				NM4.webkit_web_view_stop_loading(web_view);
			}

			public static bool webkit_web_view_can_go_back(IntPtr web_view)
			{
				return NM4.webkit_web_view_can_go_back(web_view);
			}

			public static void webkit_web_view_go_back(IntPtr web_view)
			{
				NM4.webkit_web_view_go_back(web_view);
			}

			public static bool webkit_web_view_can_go_forward(IntPtr web_view)
			{
				return NM4.webkit_web_view_can_go_forward(web_view);
			}

			public static void webkit_web_view_go_forward(IntPtr web_view)
			{
				NM4.webkit_web_view_go_forward(web_view);
			}

			public static void webkit_web_view_run_javascript(IntPtr web_view, string script, IntPtr cancellable, Delegate callback, IntPtr user_data)
			{
				NM4.webkit_web_view_run_javascript(web_view, script, cancellable, callback, user_data);
			}

			public static IntPtr webkit_web_view_run_javascript_finish(IntPtr web_view, IntPtr result, IntPtr error)
			{
				return NM4.webkit_web_view_run_javascript_finish(web_view, result, error);
			}

			public static IntPtr webkit_javascript_result_get_global_context(IntPtr js_result)
			{
				return NM4.webkit_javascript_result_get_global_context(js_result);
			}

			public static IntPtr webkit_javascript_result_get_value(IntPtr js_result)
			{
				return NM4.webkit_javascript_result_get_value(js_result);
			}

			public static IntPtr JSValueToStringCopy(IntPtr context, IntPtr value, IntPtr idk)
			{
				return NM4.JSValueToStringCopy(context, value, idk);
			}

			public static int JSStringGetMaximumUTF8CStringSize(IntPtr js_str_value)
			{
				return NM4.JSStringGetMaximumUTF8CStringSize(js_str_value);
			}

			public static void JSStringGetUTF8CString(IntPtr js_str_value, IntPtr str_value, int str_length)
			{
				NM4.JSStringGetUTF8CString(js_str_value, str_value, str_length);
			}

			public static void JSStringRelease(IntPtr js_str_value)
			{
				NM4.JSStringRelease(js_str_value);
			}

			public static IntPtr webkit_navigation_policy_decision_get_request(IntPtr decision)
			{
				return NM4.webkit_navigation_policy_decision_get_request(decision);
			}

			public static string webkit_uri_request_get_uri(IntPtr request)
			{
				return GetString(NM4.webkit_uri_request_get_uri(request));
			}

			public static bool webkit_response_policy_decision_is_mime_type_supported(IntPtr decision)
			{
				return NM4.webkit_response_policy_decision_is_mime_type_supported(decision);
			}

			public static void webkit_policy_decision_download(IntPtr decision)
			{
				NM4.webkit_policy_decision_download(decision);
			}

			public static IntPtr webkit_response_policy_decision_get_request(IntPtr decision)
			{
				return NM4.webkit_response_policy_decision_get_request(decision);
			}

			public static void webkit_javascript_result_unref(IntPtr result)
			{
				NM4.webkit_javascript_result_unref(result);
			}

			public static IntPtr webkit_web_view_get_inspector(IntPtr web_view)
			{
				return NM4.webkit_web_view_get_inspector(web_view);
			}

			public static void webkit_web_inspector_show(IntPtr inspector)
			{
				NM4.webkit_web_inspector_show(inspector);
			}

			public static void webkit_web_inspector_detach(IntPtr inspector)
			{
				NM4.webkit_web_inspector_detach(inspector);
			}
		}
	}
}
#endif