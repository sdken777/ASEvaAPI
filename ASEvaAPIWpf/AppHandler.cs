using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;
using Eto.Wpf;

namespace ASEva.UIWpf
{
    public class AppHandlerCreator
    {
        public static AppHandler Create()
        {
            return new AppHandlerWpf();
        }
    }

    class AppHandlerWpf : AppHandler
    {
        public Application CreateApp(out String webViewBackend)
        {
            WebView2Handler.InitCoreWebView2Environment();

            var platform = new global::Eto.Wpf.Platform();
            platform.Add<WebView.IHandler>(() => new WebView2Handler());
            var app = new Application(platform);

            SetContentExtensions.WindowInitializer = new InitWindowHandlerWpf();
            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerWpf();

            webViewBackend = "webview2";
            return app;
        }

        public Font CreateDefaultFont()
        {
            return SystemFonts.Default();
        }

        public void RunApp(Application application, Form window)
        {
            application.Run(window);
        }
    }
}