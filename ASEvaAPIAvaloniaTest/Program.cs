using System;
using Avalonia;
using ASEva.UIAvalonia;
using ASEva.Utility;
using ASEva;

namespace ASEvaAPIAvaloniaTest
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args) // Test program entry point
        {
            if (!App.Init(() => AppBuilder.Configure<AvaloniaApplication>().UsePlatformDetect().WithInterFont())) return;

            var startup = new Startup();
            App.Run(startup);

            Language = startup.Language;
            App.Run(new MainWindow());
        }

        public static AppBuilder BuildAvaloniaApp() // Designer entry point
        {
            DesignerMode = true;
            return AppBuilder.Configure<AvaloniaApplication>().UsePlatformDetect().WithInterFont().LogToTrace();
        }

        public static bool DesignerMode { get; private set; } = false;
        public static Language Language { get; private set; } = Language.English;
    }
}