using System;
using ASEva.Eto;
using Eto.Forms;

namespace ASEva.CoreWF
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
        public Application CreateApp()
        {
            System.Windows.Forms.Application.SetHighDpiMode(System.Windows.Forms.HighDpiMode.SystemAware);

            var platform = new global::Eto.WinForms.Platform();
            platform.Add<GroupBox.IHandler>(() => new ASEvaCoreWFHandler.GroupBoxHandler());

            var app = new Application(platform);
            return app;
        }

        public void RunApp(Application application, Form window)
        {
            application.Run(window);
        }
    }
}