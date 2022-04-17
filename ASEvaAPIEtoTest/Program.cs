using System;
using ASEva.UIEto;

namespace ASEvaAPIEtoTest
{
    partial class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var uiCode = args.Length == 0 ? null : args[0];
            if (!App.Init(uiCode))
            {
                Console.WriteLine("App initialization failed.");
                return;
            }

            var startup = new Startup();
            startup.MoveToCenter();
            startup.ShowModal();

            var window = new TestWindow(startup.LanguageCode);
            window.MoveToCenter();
            App.Run(window);
        }
    }
}