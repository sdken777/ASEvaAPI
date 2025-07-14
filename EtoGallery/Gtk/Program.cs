using System;

namespace Gtk
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var platform = new Eto.GtkSharp.Platform();
            var app = new Eto.Test.TestApplication(platform);
            app.Run();
        }
    }
}
