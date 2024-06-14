using System;
using System.Text;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;
using Eto.WinForms;
using System.Collections.Generic;
using Eto.WinForms.Forms;

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
        public Application CreateApp(bool attach, out String uiBackend, out String webViewBackend)
        {
            // CHECK: 支持高DPI显示
            System.Windows.Forms.Application.SetHighDpiMode(System.Windows.Forms.HighDpiMode.SystemAware);

            // CHECK: 初始化WebView2环境
            WebView2Handler.InitCoreWebView2Environment();

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

            platform.Add<Slider.IHandler>(() => new SliderHandler());
            platform.Add<MessageBox.IHandler>(() => new MessageBoxHandler());
            var app = new Application(platform);

            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerCoreWF();
            ButtonPanel.UseInnerEnterLeave = true;
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

            // CHECK: 修正application.Run之前不触发MouseDown等事件
            if (!attach) System.Windows.Forms.Application.AddMessageFilter(TempBubbleEventFilter);

            FuncManager.Register("GetUIBackendAPIVersion", delegate { return APIInfo.GetAPIVersion(); });
            FuncManager.Register("RegisterLegacyValueGraph", delegate { Agency.RegisterGraphPanel(GraphType.SingleValue, getLegacyStyleName(), typeof(ValueGraph)); return null; });
            FuncManager.Register("RegisterLegacyHistLineGraph", delegate { Agency.RegisterGraphPanel(GraphType.HistAndLine, getLegacyStyleName(), typeof(HistLineGraph)); return null; });
            FuncManager.Register("RegisterLegacyScatterPointsGraph", delegate { Agency.RegisterGraphPanel(GraphType.ScatterPoints, getLegacyStyleName(), typeof(ScatterPointsGraph)); return null; });
            FuncManager.Register("RegisterLegacyMatrixTableGraph", delegate { Agency.RegisterGraphPanel(GraphType.MatrixTable, getLegacyStyleName(), typeof(MatrixTableGraph)); return null; });
            FuncManager.Register("RegisterLegacyLabelTableGraph", delegate { Agency.RegisterGraphPanel(GraphType.LabelTable, getLegacyStyleName(), typeof(LabelTableGraph)); return null; });

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
                if (c is WebView) (c.ControlObject as Microsoft.Web.WebView2.WinForms.WebView2).Visible = false;
                else if (c is Container) findAndHideWebViews(c as Container);
            }
        }

        public void RunApp(Application application, Form window, Form[] subWindows)
        {
            // CHECK: 修正application.Run之前不触发MouseDown等事件
            System.Windows.Forms.Application.RemoveMessageFilter(TempBubbleEventFilter);

            if (window.Content == null) window.Content = new Panel();
            window.Closed += delegate { findAndHideWebViews(window); };

            try
            {
                application.Run(window);
            }
            catch (Exception ex)
            {
                App.TriggerFatalException(new UnhandledExceptionEventArgs(ex, false));
            }
        }

        public Control ConvertControlToEto(object platformControl)
        {
            if (platformControl == null) return null;
            if (platformControl is System.Windows.Forms.Control) return (platformControl as System.Windows.Forms.Control).ToEto();
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
                App.TriggerFatalException(new UnhandledExceptionEventArgs(ex, false));
                return false;
            }
        }

        public Dictionary<string, string> GetThirdPartyNotices()
        {
            var table = new Dictionary<String, String>();
            table["WebView2"] = Encoding.UTF8.GetString(Resource.WebView2);
            return table;
        }

        public bool ShouldPassParent()
        {
            return false;
        }

        private String getLegacyStyleName()
        {
            var lang = Agency.GetAppLanguage();
            return lang != null && lang == "ch" ? "旧图表" : "Legacy Graph";
        }

        private BubbleEventFilter TempBubbleEventFilter
        {
            get
            {
                if (tempBubbleEventFilter == null)
                {
                    var bubble = new BubbleEventFilter();
                    bubble.AddBubbleMouseEvent((c, cb, e) => cb.OnMouseWheel(c, e), null, Win32.WM.MOUSEWHEEL);
                    bubble.AddBubbleMouseEvent((c, cb, e) => cb.OnMouseMove(c, e), null, Win32.WM.MOUSEMOVE);
                    bubble.AddBubbleMouseEvents((c, cb, e) =>
                    {
                        cb.OnMouseDown(c, e);
                        if (e.Handled && c.Handler is IWindowsControl handler && handler.ShouldCaptureMouse)
                        {
                            handler.ContainerControl.Capture = true;
                            handler.MouseCaptured = true;
                        }
                    }, true, Win32.WM.LBUTTONDOWN, Win32.WM.RBUTTONDOWN, Win32.WM.MBUTTONDOWN);
                    bubble.AddBubbleMouseEvents((c, cb, e) =>
                    {
                        cb.OnMouseDoubleClick(c, e);
                        if (!e.Handled)
                            cb.OnMouseDown(c, e);
                    }, null, Win32.WM.LBUTTONDBLCLK, Win32.WM.RBUTTONDBLCLK, Win32.WM.MBUTTONDBLCLK);
                    void OnMouseUpHandler(Control c, Control.ICallback cb, MouseEventArgs e)
                    {
                        if (c.Handler is IWindowsControl handler && handler.MouseCaptured)
                        {
                            handler.MouseCaptured = false;
                            handler.ContainerControl.Capture = false;
                        }
                        cb.OnMouseUp(c, e);
                    }
                    bubble.AddBubbleMouseEvent(OnMouseUpHandler, false, Win32.WM.LBUTTONUP, b => MouseButtons.Primary);
                    bubble.AddBubbleMouseEvent(OnMouseUpHandler, false, Win32.WM.RBUTTONUP, b => MouseButtons.Alternate);
                    bubble.AddBubbleMouseEvent(OnMouseUpHandler, false, Win32.WM.MBUTTONUP, b => MouseButtons.Middle);
                    tempBubbleEventFilter = bubble;
                }
                return tempBubbleEventFilter;
            }
        }
        private BubbleEventFilter tempBubbleEventFilter;
    }


}