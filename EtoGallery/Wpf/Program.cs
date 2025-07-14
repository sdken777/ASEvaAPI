using System;

namespace Wpf
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var platform = new Eto.Wpf.Platform();
            var app = new Eto.Test.TestApplication(platform);
            app.Run();
        }
    }
}
