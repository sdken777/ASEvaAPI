using System;

namespace ASEva.UIAvalonia
{
    #pragma warning disable CS1571

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
            
            var etoWindowPanel = UIEto.App.ConvertWindowPanelToEto(anyWindowPanel);
            if (etoWindowPanel != null)
            {
                var etoWindowPanelContainer = new EtoWindowPanel();
                etoWindowPanelContainer.SetEtoWindowPanel(etoWindowPanel);
                return etoWindowPanelContainer;
            }

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
            
            var etoConfigPanel = UIEto.App.ConvertConfigPanelToEto(anyConfigPanel);
            if (etoConfigPanel != null)
            {
                var etoConfigPanelContainer = new EtoConfigPanel();
                etoConfigPanelContainer.SetEtoConfigPanel(etoConfigPanel);
                return etoConfigPanelContainer;
            }

            return null;
        }
    }
}