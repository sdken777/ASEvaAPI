using System;
using System.IO;
using System.Reflection;
using ASEva.UIEto;
using Avalonia.Threading;

namespace ASEva.UIAvalonia
{
    class EtoInitializer
    {
        public static bool Validate()
        {
            if (AdaptorManager.Instance == null) return false;
            if (getUICode() == null) return false;
            return true;
        }

        public static void Initialize(RunDialogHandler runDialogHandler)
        {
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += delegate
            {
                if (initResult == null)
                {
                    initResult = UIEto.App.Init(getUICode(), true);
                    if (initResult.Value && AdaptorManager.Instance.ShouldOverrideRunDialog()) UIEto.App.RunDialogHandler = runDialogHandler;
                }
                else if (initResult.Value == true)
                {
                    AdaptorManager.Instance.RunIteration();
                    EtoEmbedder.RunIteration();
                }
            };
            timer.Start();
        }

        public static bool Initialized
        {
            get { return initResult != null && initResult.Value; }
        }

        public static AvaloniaAdaptor Adaptor
        {
            get { return AdaptorManager.Instance; }
        }

        private static String getUICode()
        {
            switch (ASEva.APIInfo.GetRunningOS())
            {
                case "windows":
                    return "corewf";
                case "linux":
                case "linuxarm":
                    return "gtk";
                case "macosarm":
                    return "monomac";
                default:
                    return null;
            }
        }

        private class AdaptorManager
        {
            public static AvaloniaAdaptor Instance
            {
                get
                {
                    init();
                    return instance;
                }
            }

            private static void init()
            {
                if (!initialized)
                {
                    initialized = true;
                    
                    String postfix = "";
                    switch (ASEva.APIInfo.GetRunningOS())
                    {
                        case "windows":
                            postfix = "CoreWF";
                            break;
                        case "linux":
                        case "linuxarm":
                            postfix = "Gtk";
                            break;
                        case "macosarm":
                            postfix = "MonoMac";
                            break;
                        default:
                            return;
                    }

                    var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var targetDll = dir + Path.DirectorySeparatorChar + "ASEvaAPI" + postfix + ".dll";
                    if (!File.Exists(targetDll)) return;

                    var assembly = Assembly.LoadFrom(targetDll);
                    if (assembly == null) return;

                    instance = assembly.CreateInstance("ASEva.UI" + postfix + ".AvaloniaAdaptor" + postfix) as AvaloniaAdaptor;
                }
            }

            private static AvaloniaAdaptor instance = null;
            private static bool initialized = false;
        }

        private static DispatcherTimer timer = new DispatcherTimer();
        private static bool? initResult = null;
    }
}