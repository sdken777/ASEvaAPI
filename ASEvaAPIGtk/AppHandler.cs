using System;
using ASEva.UIEto;
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

            var app = new Application(platform);
            return app;
        }

        public void RunApp(Application application, Form window)
        {
            SizerExtensions.PixelScale = 1;
            application.Run(window);
        }
    }
}