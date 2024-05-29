using System;
using Avalonia;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (!App.Init()) return;

            var startup = new Startup();
            App.Run(startup);

            App.Run(new MainWindow(startup.Language));
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<AvaloniaApplication>().UsePlatformDetect().WithInterFont().LogToTrace();
        }
    }
}