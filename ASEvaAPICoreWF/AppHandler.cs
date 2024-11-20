using System;
using System.Text;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;
using Eto.WinForms;
using System.Collections.Generic;
using Eto.WinForms.Forms;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    public class AppHandlerCreator
    {
        public static AppHandler Create()
        {
            return new AppHandlerCoreWF();
        }
    }

    class AppHandlerCoreWF : AppHandler
    {
        public Application? CreateApp(bool attach, out String? uiBackend, out String webViewBackend)
        {
            // CHECK: 支持高DPI显示
            System.Windows.Forms.Application.SetHighDpiMode(System.Windows.Forms.HighDpiMode.SystemAware);

            // CHECK: 初始化WebView2环境
            WebView2Handler.InitCoreWebView2Environment();

            ApplicationHandler.BubbleKeyEvents = false;
            ApplicationHandler.BubbleMouseEvents = false;

            var platform = new global::Eto.WinForms.Platform();
            platform.Add<GroupBox.IHandler>(() => new GroupBoxHandler());
            platform.Add<ProgressBar.IHandler>(() => new ProgressBarHandler());
            platform.Add<ColorPicker.IHandler>(() => new ColorPickerHandler());
            platform.Add<SearchBox.IHandler>(() => new SearchBoxHandler());
            platform.Add<WebView.IHandler>(() => new WebView2Handler());
            platform.Add<ComboBox.IHandler>(() => new ComboBoxHandler());
            platform.Add<Drawable.IHandler>(() => new DrawableHandler());
            platform.Add<PasswordBox.IHandler>(() => new PasswordBoxHandler());
            platform.Add<Bitmap.IHandler>(() => new SafeBitmapHandler());

            // 改为使用Wpf版的Handler，Eto-2.6.0已修正
            //platform.Add<DataObject.IHandler>(() => new DataObjectHandler());
            //platform.Add<DataFormats.IHandler>(() => new DataFormatsHandler());

            // CHECK: 使用默认文件夹选择对话框，避免COM异常
            platform.Add<SelectFolderDialog.IHandler>(() =>
            {
                var handler = new SelectFolderDialogHandler();
                handler.Control.UseDescriptionForTitle = true;
                return handler;
            });

            platform.Add<Slider.IHandler>(() => new SliderHandler());
            platform.Add<MessageBox.IHandler>(() => new MessageBoxHandler());
            var app = new Application(platform);

            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerCoreWF();
            ButtonPanel.TextAlphaUnsupported = true;
            Pixel.CalculateByScreenRealScale = true;
            ASEva.UIEto.ImageConverter.Mode = ASEva.UIEto.ImageConverter.ConvertMode.AlphaScale;
            CheckableListBox.Factory = new CheckableListBoxFactoryCoreWF();
            TextTableView.Factory = new TextTableViewFactoryCoreWF();
            GLView.Factory = new GLViewFactoryCoreWF();
            SkiaView.Factory = new GLViewFactoryCoreWF();
            SkiaCanvasExtensions.DefaultFontName = "Microsoft Yahei";
            SkiaCanvasExtensions.DefaultFontSize = 12;
            FlowLayout.Factory = new FlowLayoutFactoryCoreWF();
            FlowLayout2D.Factory = new FlowLayout2DFactoryCoreWF();
            TopMostExtensions.QueryInterface = new TopMostHandler();
            TextBitmap.ModifyInterface = new BitmapGraphicsHandler();
            TextBitmap.FastModeDrawOffset = new PointF(1, 1);
            SimpleTreeView.Factory = new SimpleTreeViewFactoryCoreWF();
            SnapshotExtensions.Handler = new SnapshotHandler();
            SnapshotExtensions.ScreenModeHandler = new ScreenSnapshotHandler();
            IconExtensions.FinalFrameOnly = true;
            OverlayLayout.ExpandControlSize = true;
            FullScreenExtensions.Handler = new FullScreenHandler();
            OxyPlotView.Factory = new OxyPlotViewFactoryCoreWF();
            SetToolTipExtensions.Handler = new ToolTipHandler();

            FuncManager.Register("GetUIBackendAPIVersion", delegate { return APIInfo.GetAPIVersion(); });
            FuncManager.Register("RegisterLegacyValueGraph", delegate { AgencyLocal.RegisterGraphPanelForType(GraphType.SingleValue, getLegacyStyleName(), typeof(ValueGraph)); return null; });
            FuncManager.Register("RegisterLegacyHistLineGraph", delegate { AgencyLocal.RegisterGraphPanelForType(GraphType.HistAndLine, getLegacyStyleName(), typeof(HistLineGraph)); return null; });
            FuncManager.Register("RegisterLegacyScatterPointsGraph", delegate { AgencyLocal.RegisterGraphPanelForType(GraphType.ScatterPoints, getLegacyStyleName(), typeof(ScatterPointsGraph)); return null; });
            FuncManager.Register("RegisterLegacyMatrixTableGraph", delegate { AgencyLocal.RegisterGraphPanelForType(GraphType.MatrixTable, getLegacyStyleName(), typeof(MatrixTableGraph)); return null; });
            FuncManager.Register("RegisterLegacyLabelTableGraph", delegate { AgencyLocal.RegisterGraphPanelForType(GraphType.LabelTable, getLegacyStyleName(), typeof(LabelTableGraph)); return null; });

            uiBackend = null;
            webViewBackend = "webview2";
            return app;
        }

        public Font CreateDefaultFont()
        {
            var testPanel = new System.Windows.Forms.Panel();
            var font = testPanel.Font;
            return new Font(font.FontFamily.ToEto(), font.Size);
        }

        private void findAndHideWebViews(Container panel)
        {
            foreach (var c in panel.Controls)
            {
                if (c is WebView webView)
                {
                    if (c.ControlObject is Microsoft.Web.WebView2.WinForms.WebView2 webView2) webView2.Visible = false;
                }
                else if (c is Container container) findAndHideWebViews(container);
            }
        }

        // CHECK: 修正vertical StackLayout中无控件时初始化异常
        private void fixVerticalStackLayoutIssue(Control rootControl)
        {
            if (rootControl is StackLayout stackLayout)
            {
                if (stackLayout.Orientation == Orientation.Vertical && stackLayout.Items.Count == 0)
                {
                    stackLayout.AddSpace(1);
                    return;
                }
            }
            if (rootControl is Container container)
            {
                foreach (var control in container.Children)
                {
                    fixVerticalStackLayoutIssue(control);
                }
            }
        }

        public void RunApp(Application application, Form window, Form[] subWindows)
        {
            fixVerticalStackLayoutIssue(window.Content);
            window.Closed += delegate { findAndHideWebViews(window); };

            try
            {
                application.Run(window);
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                App.TriggerFatalException(new UnhandledExceptionEventArgs(ex, false));
            }
        }

        public Control? ConvertControlToEto(object platformControl)
        {
            if (platformControl is System.Windows.Forms.Control) return (platformControl as System.Windows.Forms.Control).ToEto();
            else return null;
        }

        public object? ConvertControlToPlatform(Control etoControl)
        {
            return etoControl.ToNative(true);
        }

        public UIEto.WindowPanel? ConvertWindowPanelToEto(object platformWindowPanel)
        {
            if (platformWindowPanel is WindowPanel winformWindowPanel) return new EtoWindowPanel(winformWindowPanel);
            else return null;
        }

        public UIEto.ConfigPanel? ConvertConfigPanelToEto(object platformConfigPanel)
        {
            if (platformConfigPanel is ConfigPanel winformConfigPanel) return new EtoConfigPanel(winformConfigPanel);
            else return null;
        }

        public bool RunDialog(DialogPanel panel)
        {
            if (panel.Mode == DialogPanel.DialogMode.Invalid) return false;

            fixVerticalStackLayoutIssue(panel);

            var winformControl = (System.Windows.Forms.Control)panel.ToNative(true);
            if (winformControl == null) return false;

            try
            {
                var dialog = new AppDialogCoreWF(winformControl, panel);
                dialog.ShowDialog();
                dialog.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                App.TriggerFatalException(new UnhandledExceptionEventArgs(ex, false));
                return false;
            }
        }

        public Dictionary<string, string> GetThirdPartyNotices()
        {
            return APIInfo.GetThirdPartyNotices();
        }

        public bool ShouldPassParent()
        {
            return false;
        }

        public bool CanParentReceiveChildEvents()
        {
            return false;
        }

        private String getLegacyStyleName()
        {
            return AgencyLocal.GetAppLanguage() == Language.Chinese ? "旧图表" : "Legacy Graph";
        }
    }
}