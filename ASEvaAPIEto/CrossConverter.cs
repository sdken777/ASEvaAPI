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
        /// (api:eto=3.5.0) Enable converting Avalonia panel to Eto panel (The method will be invoked in App.Init)
        /// </summary>
        /// <returns>Whether initialization is successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=3.5.0) 启用Avalonia面板转Eto面板功能（在App.Init中会自动调用）
        /// </summary>
        /// <returns>是否成功</returns>
        public static bool EnableAvaloniaEmbedder()
        {
            return AvaloniaEmbedder.Enable();
        }

        /// \~English
        /// <summary>
        /// (api:eto=3.4.0) Enable converting Avalonia panel to Eto panel (The method should be called before App.Init)
        /// </summary>
        /// <param name="appBuilderCreation">The function to create AppBuilder object</param>
        /// <returns>Whether initialization is successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=3.4.0) 启用Avalonia面板转Eto面板功能（需要在App.Init前调用）
        /// </summary>
        /// <param name="appBuilderCreation">创建AppBuilder对象的函数</param>
        /// <returns>是否成功</returns>
        public static bool EnableAvaloniaEmbedder(Func<object> appBuilderCreation)
        {
            return AvaloniaEmbedder.Enable(appBuilderCreation);
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
            var convertedFromAvalonia = AvaloniaEmbedder.ConvertWindowPanel(anyWindowPanel);
            if (convertedFromAvalonia != null) return convertedFromAvalonia;
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
            var convertedFromAvalonia = AvaloniaEmbedder.ConvertConfigPanel(anyConfigPanel);
            if (convertedFromAvalonia != null) return convertedFromAvalonia;
            return App.ConvertConfigPanelToEto(anyConfigPanel);
        }
    }
}