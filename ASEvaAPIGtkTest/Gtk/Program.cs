using System;
using Gtk;

namespace ASEvaAPIGtkTest
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var mainWindow = new ASEvaAPIGtkTest();
            mainWindow.Show();
            Application.Run();
        }
    }
}
