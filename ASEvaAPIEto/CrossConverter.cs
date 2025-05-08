using System;
using System.IO;
using System.Reflection;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=3.2.0) Panel conversion tool
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.2.0) 面板转换工具
    /// </summary>
    public class CrossConverter
    {
        /// \~English
        /// <summary>
        /// (api:eto=3.4.0) Enable converting Avalonia panel to Eto panel (only for winform and wpf)
        /// </summary>
        /// <param name="appBuilderCreation">The function to create AppBuilder object</param>
        /// <returns>Whether initialization is successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=3.4.0) 启用Avalonia面板转Eto面板功能（仅限winform和wpf）
        /// </summary>
        /// <param name="appBuilderCreation">创建AppBuilder对象的函数</param>
        /// <returns>是否成功</returns>
        public static bool EnableAvaloniaEmbedder(Func<object> appBuilderCreation = null)
        {
            if (winformHostTypeForAvalonia != null && wpfHostTypeForAvalonia != null) return true;

            var entryFolder = ASEva.Utility.EntryFolder.Path;
            if (entryFolder == null) return false;

            var dllPath = entryFolder + Path.DirectorySeparatorChar + "HwndHostAvalonia.dll";
            if (!File.Exists(dllPath)) return false;

            var assembly = Assembly.LoadFrom(dllPath);
            if (assembly == null) return false;

            var winformHostType = assembly.GetType("HwndHostAvalonia.WinformHost");
            var wpfHostType = assembly.GetType("HwndHostAvalonia.WpfHost");
            if (winformHostType == null || wpfHostType == null) return false;

            var initMethod = winformHostType.BaseType.GetMethod("InitAvaloniaEnvironment");
            if (initMethod == null) return false;

            initMethod.Invoke(null, [appBuilderCreation]);

            winformHostTypeForAvalonia = winformHostType;
            wpfHostTypeForAvalonia = wpfHostType;
            return true;
        }

        /// \~English
        /// <summary>
        /// Convert any window panel to Eto window panel
        /// </summary>
        /// <param name="anyWindowPanel">Any window panel</param>
        /// <returns>Eto window panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将任意窗口面板转化为Eto窗口面板
        /// </summary>
        /// <param name="anyWindowPanel">任意窗口面板</param>
        /// <returns>Eto窗口面板，若转化失败则返回null</returns>
        public static WindowPanel ConvertWindowPanel(object anyWindowPanel)
        {
            if (anyWindowPanel == null) return null;
            if (anyWindowPanel is WindowPanel) return anyWindowPanel as WindowPanel;
            if (App.GetRunningUI() == "corewf" && winformHostTypeForAvalonia != null)
            {
                var convertMethod = winformHostTypeForAvalonia.GetMethod("ConvertWindowPanel");
                if (convertMethod != null)
                {
                    var winformPanel = convertMethod.Invoke(null, [anyWindowPanel]);
                    if (winformPanel != null) return App.ConvertWindowPanelToEto(winformPanel);
                }
            }
            if (App.GetRunningUI() == "wpf" && wpfHostTypeForAvalonia != null)
            {
                var convertMethod = wpfHostTypeForAvalonia.GetMethod("ConvertWindowPanel");
                if (convertMethod != null)
                {
                    var wpfPanel = convertMethod.Invoke(null, [anyWindowPanel]);
                    if (wpfPanel != null) return App.ConvertWindowPanelToEto(wpfPanel);
                }
            }
            return App.ConvertWindowPanelToEto(anyWindowPanel);
        }

        /// \~English
        /// <summary>
        /// Convert any config panel to Eto config panel
        /// </summary>
        /// <param name="anyConfigPanel">Any config panel</param>
        /// <returns>Eto config panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将任意配置面板转化为Eto配置面板
        /// </summary>
        /// <param name="anyConfigPanel">任意配置面板</param>
        /// <returns>Eto配置面板，若转化失败则返回null</returns>
        public static ConfigPanel ConvertConfigPanel(object anyConfigPanel)
        {
            if (anyConfigPanel == null) return null;
            if (anyConfigPanel is ConfigPanel) return anyConfigPanel as ConfigPanel;
            if (App.GetRunningUI() == "corewf" && winformHostTypeForAvalonia != null)
            {
                var convertMethod = winformHostTypeForAvalonia.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var winformPanel = convertMethod.Invoke(null, [anyConfigPanel]);
                    if (winformPanel != null) return App.ConvertConfigPanelToEto(winformPanel);
                }
            }
            if (App.GetRunningUI() == "wpf" && wpfHostTypeForAvalonia != null)
            {
                var convertMethod = wpfHostTypeForAvalonia.GetMethod("ConvertConfigPanel");
                if (convertMethod != null)
                {
                    var wpfPanel = convertMethod.Invoke(null, [anyConfigPanel]);
                    if (wpfPanel != null) return App.ConvertConfigPanelToEto(wpfPanel);
                }
            }
            return App.ConvertConfigPanelToEto(anyConfigPanel);
        }

        private static Type winformHostTypeForAvalonia = null;
        private static Type wpfHostTypeForAvalonia = null;
    }
}