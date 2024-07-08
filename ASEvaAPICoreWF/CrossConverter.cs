using System;
using System.IO;
using System.Reflection;

namespace ASEva.UICoreWF
{
#pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:corewf=3.2.0) Panel conversion tool
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.2.0) 面板转换工具
    /// </summary>
    public class CrossConverter
    {
        /// \~English
        /// <summary>
        /// (api:corewf=3.2.2) Enable converting WPF panel to Winform panel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:corewf=3.2.2) 启用WPF面板转Winform面板功能
        /// </summary>
        public static bool EnableWpfEmbedder()
        {
            if (winformHostType != null) return true;

            var dllPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "WinformWpfConverter.dll";
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
        }

        /// \~English
        /// <summary>
        /// Convert any window panel to Winform window panel
        /// </summary>
        /// <param name="anyWindowPanel">Any window panel</param>
        /// <returns>Winform window panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将任意窗口面板转化为Winform窗口面板
        /// </summary>
        /// <param name="anyWindowPanel">任意窗口面板</param>
        /// <returns>Winform窗口面板，若转化失败则返回null</returns>
        public static WindowPanel ConvertWindowPanel(object anyWindowPanel)
        {
            if (anyWindowPanel == null) return null;
            if (anyWindowPanel is WindowPanel) return anyWindowPanel as WindowPanel;
            if (anyWindowPanel is UIEto.WindowPanel) return new WindowPanelEto(anyWindowPanel as UIEto.WindowPanel);
            if (winformHostType != null)
            {
                var convertMethod = winformHostType.GetMethod("ConvertWindowPanel");
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
        /// Convert any config panel to Winform config panel
        /// </summary>
        /// <param name="anyConfigPanel">Any config panel</param>
        /// <returns>Winform config panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将任意配置面板转化为Winform配置面板
        /// </summary>
        /// <param name="anyConfigPanel">任意配置面板</param>
        /// <returns>Winform配置面板，若转化失败则返回null</returns>
        public static ConfigPanel ConvertConfigPanel(object anyConfigPanel)
        {
            if (anyConfigPanel == null) return null;
            if (anyConfigPanel is ConfigPanel) return anyConfigPanel as ConfigPanel;
            if (anyConfigPanel is UIEto.ConfigPanel) return new ConfigPanelEto(anyConfigPanel as UIEto.ConfigPanel);
            if (winformHostType != null)
            {
                var convertMethod = winformHostType.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var convertedPanel = convertMethod.Invoke(null, [anyConfigPanel]) as ConfigPanel;
                    if (convertedPanel != null) return convertedPanel;
                }
            }
            return null;
        }

        private static Type winformHostType = null;
    }
}