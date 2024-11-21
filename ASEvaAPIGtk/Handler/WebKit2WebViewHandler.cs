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
using ASEva.Utility;

namespace ASEva.UIGtk
{
	#pragma warning disable CS0612

	// CHECK: 大量修正和优化，官方Handler仅作为参考，更新部分根据情况合并至此实现
	class WebViewHandler : GtkControl<Gtk.ScrolledWindow, WebView, WebView.ICallback>, WebView.IHandler
	{
		public bool BrowserContextMenuEnabled { get; set; }

		public string DocumentTitle
		{
			get { return webView?.Title ?? ""; }
		}

		public Uri? Url
		{
			get
			{
				String? uriString = webView?.Uri;
				return String.IsNullOrEmpty(uriString) ? null : new Uri(uriString);
			}
			set
			{
				if (webView == null)
				{
					if (value != null && !String.IsNullOrEmpty(value.AbsoluteUri)) targetUrl = value.AbsoluteUri;
				}
				else
				{
					if (value != null) webView.LoadUri(value.AbsoluteUri);
					else webView.LoadHtml("", "");
				}
			}
		}

		public override Gtk.Widget ContainerControl
		{
			get { return Control; }
		}

		EventHandler<WebViewTitleEventArgs>? titleChanged;
		EventHandler<WebViewLoadedEventArgs>? navigated;
		EventHandler<WebViewLoadedEventArgs>? documentLoaded;
		EventHandler<WebViewLoadingEventArgs>? documentLoading;
		EventHandler<WebViewNewWindowEventArgs>? openNewWindow;

		Queue<TaskCompletionSource<string?>>? jsCompletionSources;
		WebKit.WebView? webView;
		string? targetUrl;
		string? targetHtmlString;
		string? targetBaseUrl;

		private static IntPtr webViewGroup = IntPtr.Zero;

		public WebViewHandler()
		{
			Control = new Gtk.ScrolledWindow();
			Control.Realized += delegate
			{
				PreInitializeSettings();

				webView = new WebKit.WebView(settings) { Visible = true };

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
					webView.LoadUri(targetUrl);
					targetUrl = null;
				}
				if (targetHtmlString != null)
				{
					webView.LoadHtml(targetHtmlString, targetBaseUrl);
					targetHtmlString = null;
					targetBaseUrl = null;
				}
			};

			Control.KeyPressEvent += (o, args) =>
			{
				if (webView != null && args.Event.Key == Gdk.Key.F12)
				{
					var inspector = webView.Inspector;
					inspector?.Show();
				}
			};
		}

		public static void PreInitializeSettings()
		{
			if (settings == null)
			{
				settings = new WebKit.Settings();
				settings.EnableDeveloperExtras = true;

				var uiBackend = ASEva.UIEto.App.GetUIBackend();
				if (uiBackend != null && uiBackend == "wayland") // Wayland下使用OpenGL可能导致花屏，或令WaylandOffscreenView卡死
				{
					settings.EnableAccelerated2dCanvas = false;
					settings.EnableWebgl = false;
					settings.HardwareAccelerationPolicy = WebKit.HardwareAccelerationPolicy.Never;
				}
			}
		}
		private static WebKit.Settings? settings = null;

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
			var type = (WebKit.PolicyDecisionType)args.Args[1];

			if (type == WebKit.PolicyDecisionType.NavigationAction)
			{
				var navigationDecision = args.Args[0] as WebKit.NavigationPolicyDecision;
				var uriString = navigationDecision?.Request.Uri;
				var uri = String.IsNullOrEmpty(uriString) ? null : new Uri(uriString);

				var loadingArgs = new WebViewLoadingEventArgs(uri, true);
				documentLoading?.Invoke(this, loadingArgs);
				args.RetVal = loadingArgs.Cancel;
			}
			else if (type == WebKit.PolicyDecisionType.NewWindowAction)
			{
				var navigationDecision = args.Args[0] as WebKit.NavigationPolicyDecision;
				var uriString = navigationDecision?.Request.Uri;
				var uri = String.IsNullOrEmpty(uriString) ? null : new Uri(uriString);

				var newWindowArgs = new WebViewNewWindowEventArgs(uri, "");
				openNewWindow?.Invoke(this, newWindowArgs);
				args.RetVal = newWindowArgs.Cancel;
			}
			else if (type == WebKit.PolicyDecisionType.Response)
			{
				var responseDecision = args.Args[0] as WebKit.ResponsePolicyDecision;
				var uriString = responseDecision?.Request.Uri;
				var mimeSupport = responseDecision?.IsMimeTypeSupported ?? false;

				if (!mimeSupport && !String.IsNullOrEmpty(uriString))
				{
					GLib.Timeout.Add(1, timer_Timeout);
					responseDecision?.Download();
					args.RetVal = true;
					return;
				}
			}
		}

        private bool timer_Timeout()
        {
			var userDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			var downloadDirName = new[] {"Downloads", "Download", "下载"};
			String? targetDir = null;
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
				catch (Exception ex) { Dump.Exception(ex); }
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

		public string? ExecuteScript(string script)
		{
			if (webView == null) return null;

			var task = ExecuteScriptAsync(script);

			while (!task.IsCompleted)
			{
				Gtk.Application.RunIteration();
			}

			return task.Result;
		}
		
		private Delegate? theDelegate = null;

		public Task<string?> ExecuteScriptAsync(string script)
		{
			if (webView == null) return Task.FromResult<String?>(null);

			var taskCompletionSource = new TaskCompletionSource<string?>();
			if (jsCompletionSources == null) jsCompletionSources = new Queue<TaskCompletionSource<string?>>();
			jsCompletionSources.Enqueue(taskCompletionSource);

			if (theDelegate == null) theDelegate = (Delegate)(FinishScriptExecutionDelegate)FinishScriptExecution;
			NativeMethods.webkit_web_view_run_javascript(webView.Handle, $"function _fn() {{{script}}} _fn();", IntPtr.Zero, (FinishScriptExecutionDelegate)theDelegate, IntPtr.Zero);

			return taskCompletionSource.Task;
		}

		delegate void FinishScriptExecutionDelegate(IntPtr webview, IntPtr result, IntPtr error);

		private void FinishScriptExecution(IntPtr webview, IntPtr result, IntPtr error)
		{
			if (webView == null) return;

			var jsResult = NativeMethods.webkit_web_view_run_javascript_finish(webView.Handle, result, IntPtr.Zero);
			var taskCompletionSource = jsCompletionSources?.Count > 0 ? jsCompletionSources?.Dequeue() : null;
			if (jsResult != IntPtr.Zero)
			{
				var context = NativeMethods.webkit_javascript_result_get_global_context(jsResult);
				var value = NativeMethods.webkit_javascript_result_get_value(jsResult);

				var strValue = NativeMethods.JSValueToStringCopy(context, value, IntPtr.Zero);
				var strLength = NativeMethods.JSStringGetMaximumUTF8CStringSize(strValue);
				var utfValue = Marshal.AllocHGlobal(strLength);
				NativeMethods.JSStringGetUTF8CString(strValue, utfValue, strLength);
				var jsReturn = GetString(utfValue);

				Marshal.FreeHGlobal(utfValue);
				NativeMethods.JSStringRelease(strValue);
				NativeMethods.webkit_javascript_result_unref(jsResult);
				taskCompletionSource?.SetResult(jsReturn);
			}
			else
			{
				taskCompletionSource?.SetResult(null);
			}
		}

		private string GetString(IntPtr handle)
		{
			if (handle == IntPtr.Zero) return "";

			int len = 0;
			while (Marshal.ReadByte(handle, len) != 0) len++;

			var bytes = new byte[len];
			Marshal.Copy(handle, bytes, 0, bytes.Length);
			return Encoding.UTF8.GetString(bytes);
		}

		public void LoadHtml(string html, Uri baseUri)
		{
			if (webView == null)
			{
				targetHtmlString = html;
				targetBaseUrl = baseUri?.AbsoluteUri ?? "";
			}
			else
			{
				webView.LoadHtml(html, baseUri?.AbsoluteUri ?? "");
			}
		}

		public void Stop()
		{
			webView?.StopLoading();
		}

		public void Reload()
		{
			webView?.Reload();
		}

		public void GoBack()
		{
			webView?.GoBack();
		}

		public void GoForward()
		{
			webView?.GoForward();
		}

        public bool CanGoBack
		{
			get { return webView?.CanGoBack() ?? false; }
		}

		public bool CanGoForward
		{
			get { return webView?.CanGoForward() ?? false; }
		}

		public void ShowPrintDialog()
		{
			webView?.RunJavascript("print();", null, null);
		}

		static class NativeMethods
		{
			static class NM40
			{
				public const string libwebkit = "libwebkit2gtk-4.0.so.37";

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
				public extern static void webkit_javascript_result_unref(IntPtr result);
			}

			static class NM41
			{
				public const string libwebkit = "libwebkit2gtk-4.1.so.0";

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
				public extern static void webkit_javascript_result_unref(IntPtr result);
			}

			public static void webkit_web_view_run_javascript(IntPtr web_view, string script, IntPtr cancellable, Delegate callback, IntPtr user_data)
			{
				checkVer();
				if (ver == TargetVersion.NM40) NM40.webkit_web_view_run_javascript(web_view, script, cancellable, callback, user_data);
				else if (ver == TargetVersion.NM41) NM41.webkit_web_view_run_javascript(web_view, script, cancellable, callback, user_data);
			}

			public static IntPtr webkit_web_view_run_javascript_finish(IntPtr web_view, IntPtr result, IntPtr error)
			{
				checkVer();
				if (ver == TargetVersion.NM40) return NM40.webkit_web_view_run_javascript_finish(web_view, result, error);
				else if (ver == TargetVersion.NM41) return NM41.webkit_web_view_run_javascript_finish(web_view, result, error);
				else return IntPtr.Zero;
			}

			public static IntPtr webkit_javascript_result_get_global_context(IntPtr js_result)
			{
				checkVer();
				if (ver == TargetVersion.NM40) return NM40.webkit_javascript_result_get_global_context(js_result);
				else if (ver == TargetVersion.NM41) return NM41.webkit_javascript_result_get_global_context(js_result);
				else return IntPtr.Zero;
			}

			public static IntPtr webkit_javascript_result_get_value(IntPtr js_result)
			{
				checkVer();
				if (ver == TargetVersion.NM40) return NM40.webkit_javascript_result_get_value(js_result);
				else if (ver == TargetVersion.NM41) return NM41.webkit_javascript_result_get_value(js_result);
				else return IntPtr.Zero;
			}

			public static IntPtr JSValueToStringCopy(IntPtr context, IntPtr value, IntPtr idk)
			{
				checkVer();
				if (ver == TargetVersion.NM40) return NM40.JSValueToStringCopy(context, value, idk);
				else if (ver == TargetVersion.NM41) return NM41.JSValueToStringCopy(context, value, idk);
				else return IntPtr.Zero;
			}

			public static int JSStringGetMaximumUTF8CStringSize(IntPtr js_str_value)
			{
				checkVer();
				if (ver == TargetVersion.NM40) return NM40.JSStringGetMaximumUTF8CStringSize(js_str_value);
				else if (ver == TargetVersion.NM41) return NM41.JSStringGetMaximumUTF8CStringSize(js_str_value);
				else return 0;
			}

			public static void JSStringGetUTF8CString(IntPtr js_str_value, IntPtr str_value, int str_length)
			{
				checkVer();
				if (ver == TargetVersion.NM40) NM40.JSStringGetUTF8CString(js_str_value, str_value, str_length);
				else if (ver == TargetVersion.NM41) NM41.JSStringGetUTF8CString(js_str_value, str_value, str_length);
			}

			public static void JSStringRelease(IntPtr js_str_value)
			{
				checkVer();
				if (ver == TargetVersion.NM40) NM40.JSStringRelease(js_str_value);
				else if (ver == TargetVersion.NM41) NM41.JSStringRelease(js_str_value);
			}

			public static void webkit_javascript_result_unref(IntPtr result)
			{
				checkVer();
				if (ver == TargetVersion.NM40) NM40.webkit_javascript_result_unref(result);
				else if (ver == TargetVersion.NM41) NM41.webkit_javascript_result_unref(result);
			}

			private enum TargetVersion
			{
				Unknown,
				None,
				NM40,
				NM41,
			}

			private static void checkVer()
			{
				if (ver != TargetVersion.Unknown) return;

				String? archID = null;
				switch (ASEva.APIInfo.GetRunningOS())
				{
					case "linux":
						archID = "x86_64-linux-gnu";
						break;
					case "linuxarm":
						archID = "aarch64-linux-gnu";
						break;
					default:
						ver = TargetVersion.None;
						return;
				}

				if (File.Exists("/usr/lib/" + archID + "/libwebkit2gtk-4.0.so.37")) ver = TargetVersion.NM40;
				else if (File.Exists("/usr/lib/" + archID + "/libwebkit2gtk-4.1.so.0")) ver = TargetVersion.NM41;
				else ver = TargetVersion.None;
			}

			private static TargetVersion ver = TargetVersion.Unknown;
		}
	}
}
#endif