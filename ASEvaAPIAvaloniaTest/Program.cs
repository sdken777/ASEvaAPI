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
            App.Run(new Startup());
            App.Run(new MainWindow());
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<AvaloniaApplication>().UsePlatformDetect().WithInterFont().LogToTrace();
        }
    }
}