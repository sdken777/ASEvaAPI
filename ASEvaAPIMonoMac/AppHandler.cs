using System;
using System.Collections.Generic;
using ASEva.Utility;
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
            platform.Add<WebView.IHandler>(() => new WKWebViewHandler());
            platform.Add<UITimer.IHandler>(() => new UITimerHandler());

            var app = new Application(platform);

            // CHECK: 点击主窗口关闭按钮后令应用程序退出
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
            DefaultSimpleTreeViewBackend.DefaultBackgroundColor = Colors.Transparent;
            SnapshotExtensions.Handler = new SnapshotHandler();

            uiBackend = null;
            webViewBackend = "webkit2";
            return app;
        }

        public Font CreateDefaultFont()
        {
            return SystemFonts.Default();
        }

        public void RunApp(Application application, Form window)
        {
            window.Closing += (o, e) => { if (e.Cancel) window.Visible = true; };
            application.Run(window);
        }

        public Control ConvertControlToEto(object platformControl)
        {
            if (platformControl == null) return null;
            if (platformControl is NSView) return (platformControl as NSView).ToEto();
            else return null;
        }

        public object ConvertControlToPlatform(Control etoControl)
        {
            if (etoControl == null) return null;
            return etoControl.ToNative(true);
        }

        public bool RunDialog(DialogPanel panel)
        {
            return false;
        }

        public Dictionary<string, string> GetThirdPartyNotices()
        {
            var table = new Dictionary<string, string>();
            table["MonoMac"] = ResourceLoader.LoadText("MonoMac.LICENSE");
            table["The OpenGL Extension Wrangler Library"] = ResourceLoader.LoadText("GLEW.LICENSE");
            return table;
        }

        public bool ShouldPassParent()
        {
            return false;
        }
    }
}