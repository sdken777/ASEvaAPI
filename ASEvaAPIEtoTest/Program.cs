using System;
using ASEva.UIEto;

namespace ASEvaAPIEtoTest
{
    partial class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App.Init();

            var startup = new Startup();
            startup.MoveToCenter();
            startup.ShowModal();

            var window = new TestWindow(startup.LanguageCode);
            window.MoveToCenter();
            App.Run(window);
        }
    }
}