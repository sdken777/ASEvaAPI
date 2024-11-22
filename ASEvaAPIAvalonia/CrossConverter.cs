using System;
using System.IO;
using System.Reflection;
using ASEva.Utility;

namespace ASEva.UIAvalonia
{
    #pragma warning disable CS1571, CS0414

    /// \~English
    /// <summary>
    /// (api:avalonia=1.1.0) Panel conversion tool
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.1.0) 面板转换工具
    /// </summary>
    public class CrossConverter
    {
        /// \~English
        /// <summary>
        /// (api:avalonia=1.1.3) Enable converting WPF panel to Avalonia panel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.1.3) 启用WPF面板转Avalonia面板功能
        /// </summary>
        public static bool EnableWpfEmbedder()
        {
#if ASEVA_API_BUNDLE_MODE
            return false;
#else
            if (winformHostType != null) return true;

            var entryFolder = EntryFolder.Path;
            if (entryFolder == null) return false;

            var dllPath = entryFolder + Path.DirectorySeparatorChar + "WinformWpfConverter.dll";
            if (!File.Exists(dllPath)) return false;

            var assembly = Assembly.LoadFrom(dllPath);
            if (assembly == null) return false;

            var type = assembly.GetType("WinformWpfConverter.WinformHost");
            if (type != null)
            {
                var registerMethod = type.GetMethod("RegisterWpfFunctions");
                if (registerMethod == null) return false;

                registerMethod.Invoke(null, null);
            }

            winformHostType = type;
            return true;
#endif
        }

        /// \~English
        /// <summary>
        /// Convert any window panel to Avalonia window panel
        /// </summary>
        /// <param name="anyWindowPanel">Any window panel</param>
        /// <returns>Avalonia window panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将任意窗口面板转化为Avalonia窗口面板
        /// </summary>
        /// <param name="anyWindowPanel">任意窗口面板</param>
        /// <returns>Avalonia窗口面板，若转化失败则返回null</returns>
        public static WindowPanel ConvertWindowPanel(object anyWindowPanel)
        {
            if (anyWindowPanel == null) return null;
            if (anyWindowPanel is WindowPanel) return anyWindowPanel as WindowPanel;
            
#if !ASEVA_API_BUNDLE_MODE
            var etoWindowPanel = UIEto.App.ConvertWindowPanelToEto(anyWindowPanel);
            if (etoWindowPanel != null)
            {
                var etoWindowPanelContainer = new EtoWindowPanel();
                etoWindowPanelContainer.SetEtoWindowPanel(etoWindowPanel);
                return etoWindowPanelContainer;
            }

            if (winformHostType != null)
            {
                var convertMethod = winformHostType.GetMethod("ConvertWindowPanel");
                if (convertMethod != null)
                {
                    var winformPanel = convertMethod.Invoke(null, [anyWindowPanel]);
                    if (winformPanel != null)
                    {
                        etoWindowPanel = UIEto.App.ConvertWindowPanelToEto(winformPanel);
                        if (etoWindowPanel != null)
                        {
                            var etoWindowPanelContainer = new EtoWindowPanel();
                            etoWindowPanelContainer.SetEtoWindowPanel(etoWindowPanel);
                            return etoWindowPanelContainer;
                        }
                    }
                }
            }
#endif
            return null;
        }

        /// \~English
        /// <summary>
        /// Convert any config panel to Avalonia config panel
        /// </summary>
        /// <param name="anyConfigPanel">Any config panel</param>
        /// <returns>Avalonia config panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将任意配置面板转化为Avalonia配置面板
        /// </summary>
        /// <param name="anyConfigPanel">任意配置面板</param>
        /// <returns>Avalonia配置面板，若转化失败则返回null</returns>
        public static ConfigPanel ConvertConfigPanel(object anyConfigPanel)
        {
            if (anyConfigPanel == null) return null;
            if (anyConfigPanel is ConfigPanel) return anyConfigPanel as ConfigPanel;
            
#if !ASEVA_API_BUNDLE_MODE
            var etoConfigPanel = UIEto.App.ConvertConfigPanelToEto(anyConfigPanel);
            if (etoConfigPanel != null)
            {
                var etoConfigPanelContainer = new EtoConfigPanel();
                etoConfigPanelContainer.SetEtoConfigPanel(etoConfigPanel);
                return etoConfigPanelContainer;
            }

            if (winformHostType != null)
            {
                var convertMethod = winformHostType.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var winformPanel = convertMethod.Invoke(null, [anyConfigPanel]);
                    if (winformPanel != null)
                    {
                        etoConfigPanel = UIEto.App.ConvertConfigPanelToEto(winformPanel);
                        if (etoConfigPanel != null)
                        {
                            var etoConfigPanelContainer = new EtoConfigPanel();
                            etoConfigPanelContainer.SetEtoConfigPanel(etoConfigPanel);
                            return etoConfigPanelContainer;
                        }
                    }
                }
            }
#endif
            return null;
        }

        private static Type winformHostType = null;
    }
}