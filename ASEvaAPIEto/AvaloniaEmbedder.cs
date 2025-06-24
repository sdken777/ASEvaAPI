using System;
using System.IO;
using System.Reflection;

namespace ASEva.UIEto
{
    class AvaloniaEmbedder
    {
        public static void DoEnable(String uiCode)
        {
            if (!enabled || commonType == null) return;

            if (appBuilderCreationFunc == null)
            {
                bool useRoundTheme = false;
                switch (uiCode)
                {
                    case "corewf":
                        useRoundTheme = OperatingSystem.IsWindowsVersionAtLeast(11);
                        break;
                    case "wpf":
                        useRoundTheme = false;
                        break;
                }

                var initMethod = commonType.GetMethod("InitAvaloniaEnvironmentSimple");
                if (initMethod != null) initMethod.Invoke(null, [useRoundTheme]);
            }
            else
            {
                var initMethod = commonType.GetMethod("InitAvaloniaEnvironment");
                if (initMethod != null) initMethod.Invoke(null, [appBuilderCreationFunc]);
            }
        }

        public static bool Enable()
        {
            if (enabled) return true;
            if (!initLibrary()) return false;

            enabled = true;
            appBuilderCreationFunc = null;
            return true;
        }

        public static bool Enable(Func<object> appBuilderCreation)
        {
            if (enabled) return false;
            if (!initLibrary()) return false;

            enabled = true;
            appBuilderCreationFunc = appBuilderCreation;
            return true;
        }

        public static WindowPanel ConvertWindowPanel(object anyWindowPanel)
        {
            if (App.GetRunningUI() == "corewf" && winformHostType != null)
            {
                var convertMethod = winformHostType.GetMethod("ConvertWindowPanel");
                if (convertMethod != null)
                {
                    var winformPanel = convertMethod.Invoke(null, [anyWindowPanel]);
                    if (winformPanel != null) return App.ConvertWindowPanelToEto(winformPanel);
                }
            }
            if (App.GetRunningUI() == "wpf" && wpfHostType != null)
            {
                var convertMethod = wpfHostType.GetMethod("ConvertWindowPanel");
                if (convertMethod != null)
                {
                    var wpfPanel = convertMethod.Invoke(null, [anyWindowPanel]);
                    if (wpfPanel != null) return App.ConvertWindowPanelToEto(wpfPanel);
                }
            }
            return null;
        }

        public static ConfigPanel ConvertConfigPanel(object anyConfigPanel)
        {
            if (App.GetRunningUI() == "corewf" && winformHostType != null)
            {
                var convertMethod = winformHostType.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var winformPanel = convertMethod.Invoke(null, [anyConfigPanel]);
                    if (winformPanel != null) return App.ConvertConfigPanelToEto(winformPanel);
                }
            }
            if (App.GetRunningUI() == "wpf" && wpfHostType != null)
            {
                var convertMethod = wpfHostType.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var wpfPanel = convertMethod.Invoke(null, [anyConfigPanel]);
                    if (wpfPanel != null) return App.ConvertConfigPanelToEto(wpfPanel);
                }
            }
            return null;
        }

        private static bool initLibrary()
        {
            var entryFolder = ASEva.Utility.EntryFolder.Path;
            if (entryFolder == null) return false;

            String dllFileName = null;
            switch (ASEva.APIInfo.GetRunningOS())
            {
                case "windows":
                    dllFileName = "HwndHostAvalonia.dll";
                    break;
                default:
                    return false;
            }
            
            var dllPath = entryFolder + Path.DirectorySeparatorChar + dllFileName;
            if (!File.Exists(dllPath)) return false;

            var assembly = Assembly.LoadFrom(dllPath);
            if (assembly == null) return false;

            Type winformHostType = null, wpfHostType = null;
            switch (ASEva.APIInfo.GetRunningOS())
            {
                case "windows":
                    winformHostType = assembly.GetType("HwndHostAvalonia.WinformHost");
                    wpfHostType = assembly.GetType("HwndHostAvalonia.WpfHost");
                    if (winformHostType == null || wpfHostType == null) return false;
                    commonType = winformHostType.BaseType;
                    break;
                default:
                    return false;
            }

            AvaloniaEmbedder.winformHostType = winformHostType;
            AvaloniaEmbedder.wpfHostType = wpfHostType;

            return true;
        }

        private static bool enabled = false;
        private static Func<object> appBuilderCreationFunc = null;
        private static Type commonType = null;
        private static Type winformHostType = null;
        private static Type wpfHostType = null;
    }
}