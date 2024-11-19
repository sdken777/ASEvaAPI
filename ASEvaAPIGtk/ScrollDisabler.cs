using System;
using GLib;
using Gtk;

namespace ASEva.UIGtk
{
    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Used to disable widget's scroll
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 用于禁用控件的鼠标滚动
    /// </summary>
    public class ScrollDisabler
    {
        public static void Disable(Widget widget)
        {
            widget.AddEvents((int)Gdk.EventMask.ScrollMask);
            widget.ScrollEvent += widget_Scroll;
        }

        [ConnectBefore]
        private static void widget_Scroll(object? o, ScrollEventArgs args)
        {
            args.RetVal = true;
        }
    }
}