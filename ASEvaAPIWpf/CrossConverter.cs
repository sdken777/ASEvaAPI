using System;
using System.IO;
using System.Reflection;

namespace ASEva.UIWpf
{
#pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:wpf=2.1.0) Panel conversion tool
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:wpf=2.1.0) 面板转换工具
    /// </summary>
    public class CrossConverter
    {
        /// \~English
        /// <summary>
        /// (api:wpf=2.1.2) Enable converting Winform panel to WPF panel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:wpf=2.1.2) 启用Winform面板转WPF面板功能
        /// </summary>
        public static bool EnableWpfEmbedder()
        {
            if (wpfHostType != null) return true;

            var dllPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "WinformWpfConverter.dll";
            if (!File.Exists(dllPath)) return false;

            var assembly = Assembly.LoadFrom(dllPath);
            if (assembly == null) return false;

            var type = assembly.GetType("WinformWpfConverter.WpfHost");
            if (type != null)
            {
                var registerMethod = type.GetMethod("RegisterWinformFunctions");
                if (registerMethod == null) return false;

                registerMethod.Invoke(null, null);
            }

            wpfHostType = type;
            return true;
        }

        /// \~English
        /// <summary>
        /// Convert any window panel to WPF window panel
        /// </summary>
        /// <param name="anyWindowPanel">Any window panel</param>
        /// <returns>WPF window panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将任意窗口面板转化为WPF窗口面板
        /// </summary>
        /// <param name="anyWindowPanel">任意窗口面板</param>
        /// <returns>WPF窗口面板，若转化失败则返回null</returns>
        public static WindowPanel ConvertWindowPanel(object anyWindowPanel)
        {
            if (anyWindowPanel == null) return null;
            if (anyWindowPanel is WindowPanel) return anyWindowPanel as WindowPanel;
            if (anyWindowPanel is UIEto.WindowPanel)
            {
                var etoWindowPanelContainer = new WindowPanelEto();
                etoWindowPanelContainer.SetEtoWindowPanel(anyWindowPanel as UIEto.WindowPanel);
                return etoWindowPanelContainer;
            }
            if (wpfHostType != null)
            {
                var convertMethod = wpfHostType.GetMethod("ConvertWindowPanel");
                if (convertMethod != null)
                {
                    var convertedPanel = convertMethod.Invoke(null, [anyWindowPanel]) as WindowPanel;
                    if (convertedPanel != null) return convertedPanel;
                }
            }
            return null;
        }

        /// \~English
        /// <summary>
        /// Convert any config panel to WPF config panel
        /// </summary>
        /// <param name="anyConfigPanel">Any config panel</param>
        /// <returns>WPF config panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将任意配置面板转化为WPF配置面板
        /// </summary>
        /// <param name="anyConfigPanel">任意配置面板</param>
        /// <returns>WPF配置面板，若转化失败则返回null</returns>
        public static ConfigPanel ConvertConfigPanel(object anyConfigPanel)
        {
            if (anyConfigPanel == null) return null;
            if (anyConfigPanel is ConfigPanel) return anyConfigPanel as ConfigPanel;
            if (anyConfigPanel is UIEto.ConfigPanel)
            {
                var etoConfigPanelContainer = new ConfigPanelEto();
                etoConfigPanelContainer.SetEtoConfigPanel(anyConfigPanel as UIEto.ConfigPanel);
                return etoConfigPanelContainer;
            }
            if (wpfHostType != null)
            {
                var convertMethod = wpfHostType.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var convertedPanel = convertMethod.Invoke(null, [anyConfigPanel]) as ConfigPanel;
                    if (convertedPanel != null) return convertedPanel;
                }
            }
            return null;
        }

        private static Type wpfHostType = null;
    }
}