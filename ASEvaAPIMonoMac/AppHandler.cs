using System;
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
        public Application CreateApp()
        {
            var platform = new global::Eto.Mac.Platform();

            var app = new Application(platform);
            var appHandler = app.Handler as Eto.Mac.Forms.ApplicationHandler;
            appHandler.AppDelegate = new AppDelegate();

            SetContentExtensions.DefaultMenuSetter = new SetDefaultMenuHandlerMonoMac();

            return app;
        }

        public Font CreateDefaultFont()
        {
            return SystemFonts.Default();
        }

        public void RunApp(Application application, Form window)
        {
            application.Run(window);
        }
    }
}