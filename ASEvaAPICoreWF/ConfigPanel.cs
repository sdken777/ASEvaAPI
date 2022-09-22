using System;
using System.Windows.Forms;

namespace ASEva.UICoreWF
{
    /// <summary>
    /// (api:corewf=2.0.0) 配置界面控件，用于实现可视化配置
    /// </summary>
    public class ConfigPanel : UserControl
    {
        /// <summary>
        /// (api:corewf=2.1.1) [可选实现] 在初始化控件尺寸时被调用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public virtual void OnInitSize(String config) { }

        /// <summary>
        /// [可选实现] 在配置界面控件初始化时被调用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public virtual void OnInit(String config) { }

        /// <summary>
        /// [可选实现] 在配置界面控件销毁前被调用
        /// </summary>
        public virtual void OnRelease() { }

        /// <summary>
        /// [可选实现] 在需要更新界面时被调用
        /// </summary>
        public virtual void OnUpdateUI() { }

        /// <summary>
        /// (api:corewf=2.3.6) 关闭配置界面
        /// </summary>
        public void Close()
        {
            if (CloseRequested != null) CloseRequested(this, null);
        }

        /// <summary>
        /// (api:corewf=2.3.6) 在此事件中实现配置界面的关闭
        /// </summary>
        public event EventHandler CloseRequested;
    }
}
