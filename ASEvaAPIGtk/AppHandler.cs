using System;
using ASEva.UIEto;
using Eto.Drawing;
using Eto.Forms;

namespace ASEva.UIGtk
{
    public class AppHandlerCreator
    {
        public static AppHandler Create()
        {
            return new AppHandlerGtk();
        }
    }

    class AppHandlerGtk : AppHandler
    {
        public Application CreateApp()
        {
            NativeMethods.DetectNM3();

            var platform = new global::Eto.GtkSharp.Platform();
            platform.Add<LinkButton.IHandler>(() => new LinkButtonHandler());
            platform.Add<NumericStepper.IHandler>(() => new NumericStepperHandler());
            platform.Add<ColorPicker.IHandler>(() => new ColorPickerHandler());
            platform.Add<WebView.IHandler>(() => new WebViewHandler());
            platform.Add<PixelLayout.IHandler>(() => new PixelLayoutHandler());

            var app = new Application(platform);
            return app;
        }

        public Font CreateDefaultFont()
        {
            return SystemFonts.Default(11);
        }

        public void RunApp(Application application, Form window)
        {
            application.Run(window);
        }
    }
}