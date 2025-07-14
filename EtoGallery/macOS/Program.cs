using System;

namespace macOS
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var platform = new Eto.Mac.Platform();
            var app = new Eto.Test.TestApplication(platform);
            app.Run();
        }
    }
}
