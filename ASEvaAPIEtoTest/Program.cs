using System;
using Eto.Forms;
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
            App.RunDialog(startup);

            App.KeyDown += (o, e) =>
            {
                if (e.Control && e.Key == Keys.P) MessageBox.Show("Ctrl+P");
            };

            var window = new TestWindow(startup.StringResult, startup.BoolResult);
            App.Run(window);
        }
    }
}