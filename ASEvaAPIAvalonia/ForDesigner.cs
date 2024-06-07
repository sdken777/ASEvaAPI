using System;
using Avalonia;
using ASEva.UIAvalonia;

namespace ForDesigner
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<AvaloniaApplication>().UsePlatformDetect().WithInterFont().LogToTrace();
        }
    }
}