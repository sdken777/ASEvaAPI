using System;
using Eto.Forms;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) UI panel for configuration
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 配置界面控件，用于实现可视化配置
    /// </summary>
    public class ConfigPanel : Panel
    {
        /// \~English
        /// <summary>
        /// [Optional] Called while initializing panel's size
        /// </summary>
        /// <param name="config">Configuration string</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在初始化控件尺寸时被调用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public virtual void OnInitSize(String config) { }

        /// \~English
        /// <summary>
        /// [Required] Called while querying panel size
        /// </summary>
        /// <returns>Logical size, at least 200x50</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取尺寸时被调用
        /// </summary>
        /// <returns>逻辑尺寸，将被调整为至少200x50</returns>
        public virtual IntSize OnGetSize() { return new IntSize(0, 0); }

        /// \~English
        /// <summary>
        /// [Optional] Called while initializing the panel
        /// </summary>
        /// <param name="config">Configuration string</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在配置界面控件初始化时被调用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public virtual void OnInit(String config) { }

        /// \~English
        /// <summary>
        /// [Optional] Called before destroying the panel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在配置界面控件销毁前被调用
        /// </summary>
        public virtual void OnRelease() { }

        /// \~English
        /// <summary>
        /// [Optional] Called while updating the panel's UI
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在需要更新界面时被调用
        /// </summary>
        public virtual void OnUpdateUI() { }

        /// \~English
        /// <summary>
        /// [Optional] Called in the main loop, for running modal dialog
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在主循环中被调用，可进行模态对话
        /// </summary>
        public virtual void OnHandleModal() { }

        /// \~English
        /// <summary>
        /// Close the container holding the panel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 关闭配置界面
        /// </summary>
        public void Close()
        {
            if (CloseRequested != null) CloseRequested(this, null);
        }

        /// \~English
        /// <summary>
        /// Implement closing the panel container in this event
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 在此事件中实现配置界面的关闭
        /// </summary>
        public event EventHandler CloseRequested;
    }
}
