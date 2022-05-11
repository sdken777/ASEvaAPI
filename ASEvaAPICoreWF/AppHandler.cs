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
            var app = new Application(platform);

            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerCoreWF();
            ButtonPanel.UseInnerEnterLeave = true;
            Pixel.CalculateByScreenRealScale = true;

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
    }
}