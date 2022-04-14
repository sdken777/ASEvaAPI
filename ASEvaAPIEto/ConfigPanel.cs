using System;
using Eto.Forms;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.0) 配置界面控件，用于实现可视化配置
    /// </summary>
    public class ConfigPanel : Panel
    {
        /// <summary>
        /// (api:eto=2.2.1) [可选实现] 在初始化控件尺寸时被调用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public virtual void OnInitSize(String config) { }

        /// <summary>
        /// [必须实现] 获取尺寸时被调用
        /// </summary>
        /// <returns>默认DPI下的尺寸，将被调整为至少200x50</returns>
        public virtual IntSize OnGetSize() { return new IntSize(0, 0); }

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
    }
}
