
// #define TEST_INSTALL // test installation without actually installing it.

using System;
using System.Collections.Generic;
using System.Threading;
using Eto.Forms;
using Eto.CustomControls;
using Eto.Drawing;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using System.Reflection;
using Eto.Wpf.Forms.Controls;
using ASEva.Utility;

#if WINFORMS
using WebView2Control = Microsoft.Web.WebView2.WinForms.WebView2;
using BaseHandler = Eto.WinForms.Forms.WindowsControl<Microsoft.Web.WebView2.WinForms.WebView2, Eto.Forms.WebView, Eto.Forms.WebView.ICallback>;
#elif WPF
using WebView2Control = Microsoft.Web.WebView2.Wpf.WebView2;
using BaseHandler = Eto.Wpf.Forms.WpfFrameworkElement<Microsoft.Web.WebView2.Wpf.WebView2, Eto.Forms.WebView, Eto.Forms.WebView.ICallback>;
using BaseHost = Eto.Wpf.Forms.EtoBorder;
#endif

namespace ASEva.UIWpf
{
	#if WPF
	class WebView2Host : BaseHost
	{
	}
	#endif

	class WebView2Handler : BaseHandler, WebView.IHandler
	{
		bool failed;
		bool webView2Ready;
		protected bool WebView2Ready => webView2Ready;
		CoreWebView2Environment _environment;
		List<Action> delayedActions;
		CoreWebView2Controller controller;
		System.Windows.Threading.DispatcherTimer timer;
		System.Windows.Point lastPos;

		public WebView2Handler()
		{
			Control = new WebView2Control();
			Control.CoreWebView2InitializationCompleted += Control_CoreWebView2Ready;
#if WPF
			_host = new WebView2Host();
			_host.Child = Control;
			_host.Handler = this;
#endif
		}

#if WPF
		WebView2Host _host;
		public override System.Windows.FrameworkElement ContainerControl => _host;
#endif
		/// <summary>
		/// The default environment to use if none is specified with <see cref="Environment"/>.
		/// </summary>
		public static CoreWebView2Environment CoreWebView2Environment;
		
		/// <summary>
		/// Specifies a function to call when we need the default environment, if not already specified
		/// </summary>
		public static Func<Task<CoreWebView2Environment>> GetCoreWebView2Environment;
		
		/// <summary>
		/// Gets or sets the environment to use, defaulting to <see cref="CoreWebView2Environment"/>.
		/// This can only be set once during construction or with a style for this handler.
		/// </summary>
		/// <value>Environment to use to initialize WebView2</value>
		public CoreWebView2Environment Environment
		{
			get => _environment ?? CoreWebView2Environment;
			set => _environment = value;
		}

		/// <summary>
		/// Override to use your own WebView2 initialization, if necessary
		/// </summary>
		/// <returns>Task</returns>
		protected async virtual System.Threading.Tasks.Task OnInitializeWebView2Async()
		{
			var env = Environment;
			if (env == null && GetCoreWebView2Environment != null)
			{
				env = CoreWebView2Environment = await GetCoreWebView2Environment();
			}

			try
			{
				await Control.EnsureCoreWebView2Async(env);
			}
			catch (Exception ex)
            {
                Dump.Exception(ex);
                failed = true;
            }
		}
		
		async void InitializeAsync() => await OnInitializeWebView2Async();
		
		protected override void Initialize()
		{
			base.Initialize();
			Size = new Size(100, 100);
		}

		protected override void OnInitializeComplete()
		{
			base.OnInitializeComplete();

			// initialize webview2 after styles are applied, since styles might be used to configure the Environment or CoreWebView2Environment
			InitializeAsync();
		}

		// CHECK: 简化WebView2环境验证与初始化
		public static void InitCoreWebView2Environment()
        {
			GetCoreWebView2Environment = () =>
			{
				var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "\\SpadasFiles\\temp\\webview2";
				return CoreWebView2Environment.CreateAsync(null, path, new CoreWebView2EnvironmentOptions("--disable-features=RendererCodeIntegrity"));
			};
		}

		void Control_CoreWebView2Ready(object sender, CoreWebView2InitializationCompletedEventArgs e)
		{
			if (!e.IsSuccess)
			{
				throw new WebView2InitializationException("Failed to initialze WebView2", e.InitializationException);
			}
			
			// can't actually do anything here, so execute them in the main loop
			Application.Instance.AsyncInvoke(RunDelayedActions);
		}

		private void RunDelayedActions()
		{
			if (Widget.IsDisposed)
				return;

			// CHECK: 截获初始化异常
			try
			{
				// CHECK: 修正窗口移动后下拉菜单显示位置不正确问题
				if (controller == null)
				{
					controller = Control.GetType().GetProperty("CoreWebView2Controller", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Control) as CoreWebView2Controller;
				}
				if (controller != null && timer == null)
				{
					timer = new System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.Normal);
					timer.Interval = TimeSpan.FromMilliseconds(100);
					timer.Tick += delegate
					{
						try
						{
							var curPos = Control.PointToScreen(new System.Windows.Point(0, 0));
							if (curPos != lastPos)
							{
								controller.NotifyParentWindowPositionChanged();
								lastPos = curPos;
							}
						}
						catch (Exception ex)
						{
                            Dump.Exception(ex);
                            timer.Stop();
							timer = null;
						}
					};
					timer.Start();
				}

				Control.CoreWebView2.DocumentTitleChanged += CoreWebView2_DocumentTitleChanged;
				Control.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
				webView2Ready = true;

				if (delayedActions != null)
				{
					for (int i = 0; i < delayedActions.Count; i++)
					{
						delayedActions[i].Invoke();
					}
					delayedActions = null;
				}
			}
			catch (Exception ex)
			{
                Dump.Exception(ex);
                failed = true;
			}
		}

		public override void OnUnLoad(EventArgs e)
		{
			base.OnUnLoad(e);
			
			// Fixes crash when shown as a child window and the parent is closed
			// https://github.com/MicrosoftEdge/WebView2Feedback/issues/1971
			// See WebViewTests.WebViewClosedAsChildShouldNotCrash for repro
#if WPF
			_host.Child = null;
#endif
		}
		
		public override void OnLoad(EventArgs e)
		{
#if WPF
			if (_host.Child == null)
				_host.Child = Control;
#endif
		}

		private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
		{
			var args = new WebViewNewWindowEventArgs(new Uri(e.Uri), null);
			Callback.OnOpenNewWindow(Widget, args);
			e.Handled = args.Cancel;
		}

		private void CoreWebView2_DocumentTitleChanged(object sender, object e)
		{
			Callback.OnDocumentTitleChanged(Widget, new WebViewTitleEventArgs(CoreWebView2.DocumentTitle));
		}

		protected void RunWhenReady(Action action)
		{
			if (webView2Ready)
			{
				// already ready, run now!
				action();
				return;
			}
			
			if (delayedActions == null)
			{
				delayedActions = new List<Action>();
			}

			delayedActions.Add(action);
		}

		public override void AttachEvent(string handler)
		{
			switch (handler)
			{
				case WebView.NavigatedEvent:
					Control.ContentLoading += Control_ContentLoading;
					break;
				case WebView.DocumentLoadedEvent:
					Control.NavigationCompleted += Control_NavigationCompleted;
					break;
				case WebView.DocumentLoadingEvent:
					Control.NavigationStarting += Control_NavigationStarting;
					break;
				case WebView.OpenNewWindowEvent:
					break;
				case WebView.DocumentTitleChangedEvent:
					break;
				default:
					base.AttachEvent(handler);
					break;
			}

		}

		private void Control_ContentLoading(object sender, CoreWebView2ContentLoadingEventArgs e)
		{
			var args = new WebViewLoadedEventArgs(Control.Source);
			Callback.OnNavigated(Widget, args);
		}

		private void Control_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
		{
			var args = new WebViewLoadingEventArgs(new Uri(e.Uri), true);
			Callback.OnDocumentLoading(Widget, args);
			e.Cancel = args.Cancel;
		}

		private void Control_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			var args = new WebViewLoadedEventArgs(Control.Source);
			Application.Instance.AsyncInvoke(() => {
				if (Widget.IsDisposed)
					return;
				Callback.OnDocumentLoaded(Widget, args);
			});
		}

		public Uri Url
		{
			get { return Control.Source; }
			set { Control.Source = value; }
		}

		public string DocumentTitle => CoreWebView2?.DocumentTitle;

		public string ExecuteScript(string script)
		{
			// CHECK: 初始化异常则不执行
			if (failed) return null;

			var fullScript = string.Format("var _fn = function() {{ {0} }}; _fn();", script);
			var task = Control.ExecuteScriptAsync(fullScript);
			while (!task.IsCompleted)
			{
				if (!Widget.Loaded)
					return null;
				Application.Instance.RunIteration();
				Thread.Sleep(10);
			}
			return Decode(task.Result);
		}

		public async Task<string> ExecuteScriptAsync(string script)
		{
			// CHECK: 初始化异常则不执行
			if (failed) return null;

			var fullScript = string.Format("var _fn = function() {{ {0} }}; _fn();", script);
			var result = await Control.ExecuteScriptAsync(fullScript);
			return Decode(result);
		}

		string Decode(string result)
		{
			// result is json-encoded. cool, but why?
			if (result == null)
				return null;
			if (result.StartsWith("\"") && result.EndsWith("\""))
			{
				return result.Substring(1, result.Length - 2);
			}
			return result;
		}

		public void Stop() => Control.Stop();

		public void Reload() => Control.Reload();

		public void GoBack() => Control.GoBack();

		public bool CanGoBack => Control.CanGoBack;

		public void GoForward() => Control.GoForward();

		public bool CanGoForward => Control.CanGoForward;

		HttpServer server;

		public void LoadHtml(string html, Uri baseUri)
		{
			// CHECK: 初始化异常则不执行
			if (failed) return;

			if (!webView2Ready)
			{
				RunWhenReady(() => LoadHtml(html, baseUri));
				return;
			}
			if (baseUri != null)
			{
				if (server == null)
					server = new HttpServer();
				server.SetHtml(html, baseUri != null ? baseUri.LocalPath : null);
				Control.Source = server.Url;
			}
			else
			{
				Control.NavigateToString(html);
			}

		}

		protected Microsoft.Web.WebView2.Core.CoreWebView2 CoreWebView2
		{
			get
			{
				if (!webView2Ready)
					return null;
				return Control.CoreWebView2;
			}
		}

		public void ShowPrintDialog() => ExecuteScript("print()");


		static readonly object BrowserContextMenuEnabled_Key = new object();

		public bool BrowserContextMenuEnabled
		{
			get => true;
			set {}
		}

#if WPF
		public override Color BackgroundColor
		{
			get => Colors.Transparent;
			set { }
		}
#endif

	}
}
