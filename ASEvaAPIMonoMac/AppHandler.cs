using System;
using ASEva.UIEto;
using Eto.Drawing;
using Eto.Forms;
using MonoMac.AppKit;

namespace ASEva.UIMonoMac
{
    public class AppHandlerCreator
    {
        public static AppHandler Create()
        {
            return new AppHandlerMonoMac();
        }
    }

    class AppDelegate : Eto.Mac.AppDelegate
    {
        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }
    }

    class AppHandlerMonoMac : AppHandler
    {
        public Application CreateApp(out String uiBackend, out String webViewBackend)
        {
            var platform = new global::Eto.Mac.Platform();

            var app = new Application(platform);
            var appHandler = app.Handler as Eto.Mac.Forms.ApplicationHandler;
            appHandler.AppDelegate = new AppDelegate();

            SetContentExtensions.WindowInitializer = new InitWindowHandlerMonoMac();
            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerMonoMac();
            ASEva.UIEto.ImageConverter.Mode = ASEva.UIEto.ImageConverter.ConvertMode.AlphaScaleColorInverted;
            TextTableView.UpdateColorMode = TextTableView.InvalidateMode.EditCell;
            TextTableView.DefaultTextColor = Colors.Black;
            TextTableView.DefaultBackgroundColor = Color.FromArgb(0, 0, 0, 16);
            CheckableListBox.UpdateColorMode = CheckableListBox.InvalidateMode.EditCell;
            GLView.Factory = new GLViewFactoryMonoMac();
            SkiaView.Factory = new GLViewFactoryMonoMac();
            SkiaCanvasExtensions.DefaultFontName = "STHeiti";
            SkiaCanvasExtensions.DefaultFontSize = 13.0f;

            uiBackend = null;
            webViewBackend = "wkwebview";
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