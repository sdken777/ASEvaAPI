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
                        useRoundTheme = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000); // windows 11
                        break;
                    case "wpf":
                        useRoundTheme = false;
                        break;
                    case "gtk":
                    case "monomac":
                        useRoundTheme = true;
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
            var uiCode = App.GetRunningUI();
            var isCoreWf = uiCode == "corewf";
            var isWpf = uiCode == "wpf";
            if (isCoreWf && winformHostType != null)
            {
                var convertMethod = winformHostType.GetMethod("ConvertWindowPanel");
                if (convertMethod != null)
                {
                    var winformPanel = convertMethod.Invoke(null, [anyWindowPanel]);
                    if (winformPanel != null) return App.ConvertWindowPanelToEto(winformPanel);
                }
            }
            if (isWpf && wpfHostType != null)
            {
                var convertMethod = wpfHostType.GetMethod("ConvertWindowPanel");
                if (convertMethod != null)
                {
                    var wpfPanel = convertMethod.Invoke(null, [anyWindowPanel]);
                    if (wpfPanel != null) return App.ConvertWindowPanelToEto(wpfPanel);
                }
            }
            if (!isCoreWf && !isWpf && generalHostType != null)
            {
                var convertMethod = generalHostType.GetMethod("ConvertWindowPanel");
                if (convertMethod != null)
                {
                    var etoPanel = convertMethod.Invoke(null, [anyWindowPanel]);
                    if (etoPanel != null) return etoPanel as WindowPanel;
                }
            }
            return null;
        }

        public static ConfigPanel ConvertConfigPanel(object anyConfigPanel)
        {
            var uiCode = App.GetRunningUI();
            var isCoreWf = uiCode == "corewf";
            var isWpf = uiCode == "wpf";
            if (isCoreWf && winformHostType != null)
            {
                var convertMethod = winformHostType.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var winformPanel = convertMethod.Invoke(null, [anyConfigPanel]);
                    if (winformPanel != null) return App.ConvertConfigPanelToEto(winformPanel);
                }
            }
            if (isWpf && wpfHostType != null)
            {
                var convertMethod = wpfHostType.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var wpfPanel = convertMethod.Invoke(null, [anyConfigPanel]);
                    if (wpfPanel != null) return App.ConvertConfigPanelToEto(wpfPanel);
                }
            }
            if (!isCoreWf && !isWpf && generalHostType != null)
            {
                var convertMethod = generalHostType.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var etoPanel = convertMethod.Invoke(null, [anyConfigPanel]);
                    if (etoPanel != null) return etoPanel as ConfigPanel;
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
                case "linux":
                case "linuxarm":
                case "macos":
                case "macosarm":
                    dllFileName = "GeneralHostAvalonia.dll";
                    break;
                default:
                    return false;
            }
            
            var dllPath = entryFolder + Path.DirectorySeparatorChar + dllFileName;
            if (!File.Exists(dllPath)) return false;

            var assembly = Assembly.LoadFrom(dllPath);
            if (assembly == null) return false;

            Type winformHostType = null, wpfHostType = null, generalHostType = null;
            switch (ASEva.APIInfo.GetRunningOS())
            {
                case "windows":
                    winformHostType = assembly.GetType("HwndHostAvalonia.WinformHost");
                    wpfHostType = assembly.GetType("HwndHostAvalonia.WpfHost");
                    if (winformHostType == null || wpfHostType == null) return false;
                    commonType = winformHostType.BaseType;
                    break;
                case "linux":
                case "linuxarm":
                case "macos":
                case "macosarm":
                    generalHostType = assembly.GetType("GeneralHostAvalonia.GeneralHost");
                    if (generalHostType == null) return false;
                    commonType = generalHostType;
                    break;
                default:
                    return false;
            }

            AvaloniaEmbedder.winformHostType = winformHostType;
            AvaloniaEmbedder.wpfHostType = wpfHostType;
            AvaloniaEmbedder.generalHostType = generalHostType;

            return true;
        }

        private static bool enabled = false;
        private static Func<object> appBuilderCreationFunc = null;
        private static Type commonType = null;
        private static Type winformHostType = null;
        private static Type wpfHostType = null;
        private static Type generalHostType = null;
    }
}