using System;
using System.Text;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;
using Eto.WinForms;
using System.Collections.Generic;

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
        public Application CreateApp(out String uiBackend, out String webViewBackend)
        {
            System.Windows.Forms.Application.SetHighDpiMode(System.Windows.Forms.HighDpiMode.SystemAware);

            WebView2Handler.InitCoreWebView2Environment();

            var platform = new global::Eto.WinForms.Platform();
            platform.Add<GroupBox.IHandler>(() => new GroupBoxHandler());
            platform.Add<ProgressBar.IHandler>(() => new ProgressBarHandler());
            platform.Add<ColorPicker.IHandler>(() => new ColorPickerHandler());
            platform.Add<SearchBox.IHandler>(() => new SearchBoxHandler());
            platform.Add<WebView.IHandler>(() => new WebView2Handler());
            platform.Add<ComboBox.IHandler>(() => new ComboBoxHandler());
            platform.Add<Drawable.IHandler>(() => new DrawableHandler());
            platform.Add<DataObject.IHandler>(() => new DataObjectHandler());
            platform.Add<DataFormats.IHandler>(() => new DataFormatsHandler());
            platform.Add<Slider.IHandler>(() => new SliderHandler());
            platform.Add<MessageBox.IHandler>(() => new MessageBoxHandler());
            var app = new Application(platform);

            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerCoreWF();
            ButtonPanel.UseInnerEnterLeave = true;
            ButtonPanel.TextAlphaUnsupported = true;
            Pixel.CalculateByScreenRealScale = true;
            ASEva.UIEto.ImageConverter.Mode = ASEva.UIEto.ImageConverter.ConvertMode.AlphaScale;
            CheckableListBox.DefaultBackgroundColor = Colors.White;
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
            IconExtensions.FinalFrameOnly = true;

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

        public void RunApp(Application application, Form window)
        {
            window.Closed += delegate { findAndHideWebViews(window); };
            application.Run(window);
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

        public bool RunDialog(DialogPanel panel)
        {
            if (panel.Mode == DialogPanel.DialogMode.Invalid) return false;

            var winformControl = (System.Windows.Forms.Control)panel.ToNative(true);
            if (winformControl == null) return false;

            var dialog = new AppDialogCoreWF(winformControl, panel);
            dialog.ShowDialog();
            dialog.Dispose();
            return true;
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
    }
}