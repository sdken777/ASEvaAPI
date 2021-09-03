using System;
using Gtk;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612

    /// <summary>
    /// (api:gtk=2.0.0) 控件颜色设置扩展方法
    /// </summary>
    public static class WidgetColorExtension
    {
        public static void SetForeColor(this Widget widget, ColorRGBA color)
        {
            var black = ColorRGBA.Black;
            if (color.R == black.R && color.G == black.G && color.B == black.B && color.A == black.A) widget.ModifyFg(StateType.Normal);
            else widget.OverrideColor(StateFlags.Normal, ColorConv.Conv(color));
        }

        public static void SetBackColor(this Widget widget, ColorRGBA color)
        {
            widget.OverrideBackgroundColor(StateFlags.Normal, ColorConv.Conv(color));
        }
    }
}