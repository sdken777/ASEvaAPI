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
        public Application CreateApp(out String uiBackend, out String webViewBackend)
        {
            WebView2Handler.InitCoreWebView2Environment();

            var platform = new global::Eto.Wpf.Platform();
            platform.Add<WebView.IHandler>(() => new WebView2Handler());
            platform.Add<SearchBox.IHandler>(() => new SearchBoxHandler());
            platform.Add<UITimer.IHandler>(() => new UITimerHandler());
            platform.Add<Button.IHandler>(() => new ButtonHandler());
            var app = new Application(platform);

            SetContentExtensions.WindowInitializer = new InitWindowHandlerWpf();
            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerWpf();
            ASEva.UIEto.ImageConverter.Mode = ASEva.UIEto.ImageConverter.ConvertMode.AlphaScale;
            GLView.Factory = new GLViewFactoryWpf();
            SkiaView.Factory = new GLViewFactoryWpf();
            SkiaCanvasExtensions.DefaultFontName = "Microsoft Yahei";
            SkiaCanvasExtensions.DefaultFontSize = 12;
            TextBitmap.ImageInterpolationMode = ImageInterpolation.Medium;
            TopMostExtensions.QueryInterface = new TopMostHandler();
            SnapshotExtensions.Handler = new SnapshotHandler();

            uiBackend = null;
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

        public Control ConvertControlToEto(object platformControl)
        {
            if (platformControl == null) return null;
            if (platformControl is System.Windows.FrameworkElement) return (platformControl as System.Windows.FrameworkElement).ToEto();
            else return null;
        }

        public object ConvertControlToPlatform(Control etoControl)
        {
            if (etoControl == null) return null;
            return etoControl.ToNative(true);
        }

        public bool RunDialog(DialogPanel panel)
        {
            if (panel.Mode == DialogPanel.DialogMode.Invalid) return false;

            var element = panel.ToNative(true);
            if (element == null) return false;

            var dialog = new AppDialogWpf(element, panel);
            dialog.ShowDialog();
            return true;
        }
    }
}