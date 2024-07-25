using System;
using System.Text;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;
using Eto.Wpf;
using Eto.Wpf.Forms;
using System.Collections.Generic;

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
        public Application CreateApp(bool attach, out String uiBackend, out String webViewBackend)
        {
            // CHECK: 初始化WebView2环境
            WebView2Handler.InitCoreWebView2Environment();

            var platform = new global::Eto.Wpf.Platform();
            platform.Add<WebView.IHandler>(() => new WebView2Handler());
            platform.Add<SearchBox.IHandler>(() => new SearchBoxHandler());
            //platform.Add<UITimer.IHandler>(() => new UITimerHandler());
            platform.Add<Button.IHandler>(() => new ButtonHandler());
            platform.Add<MessageBox.IHandler>(() => new MessageBoxHandler());
            platform.Add<PasswordBox.IHandler>(() => new PasswordBoxHandler());
            platform.Add<LinkButton.IHandler>(() => new SafeLinkButtonHandler());
            platform.Add<SelectFolderDialog.IHandler>(() => new SelectFolderDialogHandler());
            var app = new Application(platform);

            SetContentExtensions.WindowInitializer = new InitWindowHandlerWpf();
            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerWpf();
            GLView.Factory = new GLViewFactoryWpf();
            SkiaView.Factory = new GLViewFactoryWpf();
            SkiaCanvasExtensions.DefaultFontName = "Microsoft Yahei";
            SkiaCanvasExtensions.DefaultFontSize = 12;
            TextBitmap.ImageInterpolationMode = ImageInterpolation.Medium;
            TextBitmap.FastModeDrawOffset = new PointF(1, 1);
            TopMostExtensions.QueryInterface = new TopMostHandler();
            SnapshotExtensions.Handler = new SnapshotHandler();
            SnapshotExtensions.ScreenModeHandler = new ScreenSnapshotHandler();
            CheckableListBox.Factory = new CheckableListBoxFactoryWpf();
            TextTableView.Factory = new TextTableViewFactoryWpf();
            FullScreenExtensions.Handler = new FullScreenHandler();
            IconExtensions.FinalFrameOnly = true;
            OxyPlotView.Factory = new OxyPlotViewFactoryWpf();

            FuncManager.Register("GetUIBackendAPIVersion", delegate { return APIInfo.GetAPIVersion(); });

            uiBackend = null;
            webViewBackend = "webview2";
            return app;
        }

        public Font CreateDefaultFont()
        {
            return SystemFonts.Default();
        }

        public void RunApp(Application application, Form window, Form[] subWindows)
        {
            try
            {
                application.Run(window);
            }
            catch (Exception ex)
            {
                App.TriggerFatalException(new UnhandledExceptionEventArgs(ex, false));
                window.Close();
            }
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

        public UIEto.WindowPanel ConvertWindowPanelToEto(object platformWindowPanel)
        {
            if (platformWindowPanel is WindowPanel) return new EtoWindowPanel(platformWindowPanel as WindowPanel);
            else return null;
        }

        public UIEto.ConfigPanel ConvertConfigPanelToEto(object platformConfigPanel)
        {
            if (platformConfigPanel is ConfigPanel) return new EtoConfigPanel(platformConfigPanel as ConfigPanel);
            else return null;
        }

        public bool RunDialog(DialogPanel panel)
        {
            if (panel.Mode == DialogPanel.DialogMode.Invalid) return false;

            var element = panel.ToNative(true);
            if (element == null) return false;

            try
            {
                var dialog = new AppDialogWpf(element, panel);
                dialog.ShowDialog();
                return true;
            }
            catch (Exception ex)
            {
                App.TriggerFatalException(new UnhandledExceptionEventArgs(ex, false));
                panel.Close();
                return false;
            }
        }

        public Dictionary<string, string> GetThirdPartyNotices()
        {
            var table = new Dictionary<string, string>();
            table["WebView2"] = Encoding.UTF8.GetString(Resource.WebView2);
            table["SharpDX"] = Encoding.UTF8.GetString(Resource.SharpDX);
            table["Extended WPF Toolkit version 3.6.0"] = Encoding.UTF8.GetString(Resource.Extended_WPF_Toolkit__3_6_);
            return table;
        }

        public bool ShouldPassParent()
        {
            return true;
        }
    }
}