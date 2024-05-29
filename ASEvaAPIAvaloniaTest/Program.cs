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
            if (!App.Init()) return;

            var startup = new Startup();
            App.Run(startup);

            commonInitialization(startup.Language);
            App.Run(new MainWindow());
        }

        public static AppBuilder BuildAvaloniaApp() // Designer entry point
        {
            commonInitialization(Language.English);
            return AppBuilder.Configure<AvaloniaApplication>().UsePlatformDetect().WithInterFont().LogToTrace();
        }

        public static Language Language { get; private set; }
        public static TextResource Texts { get; private set; }

        private static void commonInitialization(Language language)
        {
            Language = language;
            Texts = TextResource.Load("test.xml", Language);
        }
    }
}