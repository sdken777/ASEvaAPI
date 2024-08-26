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
#if ASEVA_API_BUNDLE_MODE
            return null;
#else
            return AppBuilder.Configure<AvaloniaApplication>().UsePlatformDetect().WithInterFont().LogToTrace();
#endif
        }
    }
}