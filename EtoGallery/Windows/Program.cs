using System;

namespace Windows
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var platform = new Eto.WinForms.Platform();
            var app = new Eto.Test.TestApplication(platform);
            app.Run();
        }
    }
}
