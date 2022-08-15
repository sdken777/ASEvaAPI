using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;
using Eto.WinForms;

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
            var app = new Application(platform);

            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerCoreWF();
            ButtonPanel.UseInnerEnterLeave = true;
            Pixel.CalculateByScreenRealScale = true;
            ASEva.UIEto.ImageConverter.Mode = ASEva.UIEto.ImageConverter.ConvertMode.AlphaScale;
            CheckableListBox.DefaultBackgroundColor = Colors.White;
            GLView.Factory = new GLViewFactoryCoreWF(true);
            SkiaView.Factory = new GLViewFactoryCoreWF(false);
            SkiaCanvasExtensions.DefaultFontName = "Microsoft Yahei";
            SkiaCanvasExtensions.DefaultFontSize = 12;

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

        public void RunApp(Application application, Form window)
        {
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
            return true;
        }
    }
}