using System;
using Gtk;

namespace ASEva.Gtk
{
    #pragma warning disable CS1591

    /// <summary>
    /// (api:gtk=1.0.0) 配置界面控件，用于实现可视化配置
    /// </summary>
    public class ConfigPanel : Box
    {
        public ConfigPanel(IntPtr raw) : base(raw)
        {}

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
