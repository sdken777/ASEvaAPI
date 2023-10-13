using System;
using Gtk;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:gtk=2.0.0) 获取鼠标在控件内坐标的扩展方法
    /// </summary>
    public static class WidgetPointerExtension
    {
        public static IntPoint GetPointer(this Widget widget)
        {
            int x, y;
            widget.GetPointer(out x, out y);
            return new IntPoint(x, y);
        }
    }
}