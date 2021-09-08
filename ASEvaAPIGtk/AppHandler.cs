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
            var platform = new global::Eto.GtkSharp.Platform();
            platform.Add<LinkButton.IHandler>(() => new LinkButtonHandler());
            platform.Add<NumericStepper.IHandler>(() => new NumericStepperHandler());

            var app = new Application(platform);
            return app;
        }

        public Font CreateDefaultFont(string languageCode)
        {
            return SystemFonts.Default(11);
        }

        public void RunApp(Application application, Form window)
        {
            SizerExtensions.PixelScale = 1;
            application.Run(window);
        }
    }
}