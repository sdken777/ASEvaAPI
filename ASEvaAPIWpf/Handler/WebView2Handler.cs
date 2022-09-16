
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using Eto.Forms;
using Eto.CustomControls;
using Eto.Drawing;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;

using WebView2Control = Microsoft.Web.WebView2.Wpf.WebView2;
using BaseHandler = Eto.Wpf.Forms.WpfFrameworkElement<Microsoft.Web.WebView2.Wpf.WebView2, Eto.Forms.WebView, Eto.Forms.WebView.ICallback>;

namespace ASEva.UIWpf
{
	public class WebView2Handler : BaseHandler, WebView.IHandler
	{
		bool failed;
		bool webView2Ready;
		List<Action> delayedActions;

		public WebView2Handler()
		{
			Control = new WebView2Control();
			Control.CoreWebView2InitializationCompleted += Control_CoreWebView2Ready;
			InitializeAsync();
		}

		protected override void Initialize()
		{
			base.Initialize();
			Size = new Size(100, 100);
		}

		public static CoreWebView2Environment CoreWebView2Environment;

		public static void InitCoreWebView2Environment()
        {
			var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SpadasFiles\\temp\\webview2";
			CoreWebView2Environment.CreateAsync(null, path, new CoreWebView2EnvironmentOptions("--disable-features=RendererCodeIntegrity")).ContinueWith((task) => CoreWebView2Environment = task.Result);
		}

		async void InitializeAsync()
		{
			if (CoreWebView2Environment != null)
			{
				try
				{
					await Control.EnsureCoreWebView2Async(CoreWebView2Environment);
				}
				catch (Exception) { }
			}
		}

		void Control_CoreWebView2Ready(object sender, EventArgs e)
		{
			// can't actually do anything here, so execute them in the main loop
			Application.Instance.AsyncInvoke(RunDelayedActions);
		}

		private void RunDelayedActions()
		{
			try
			{
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
			catch (Exception)
			{
				failed = true;
			}
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

		void RunWhenReady(Action action)
		{
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
			Application.Instance.AsyncInvoke(() => Callback.OnDocumentLoaded(Widget, args));
		}

		public Uri Url
		{
			get { return Control.Source; }
			set { Control.Source = value; }
		}

		public string DocumentTitle => CoreWebView2?.DocumentTitle;

		public string ExecuteScript(string script)
		{
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

		Microsoft.Web.WebView2.Core.CoreWebView2 CoreWebView2
		{
			get
			{
				if (!webView2Ready)
					return null;
				return Control.CoreWebView2;
			}
		}

		public void ShowPrintDialog() => ExecuteScript("print()");

		public bool BrowserContextMenuEnabled
		{
			get => true;
			set {}
		}

		public override Color BackgroundColor
		{
			get => Colors.Transparent;
			set { }
		}
	}
}