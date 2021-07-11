using System;
using GLib;
using Gtk;

namespace ASEva.Gtk
{
    /// <summary>
    /// (api:gtk=1.0.0) 用于禁用控件的鼠标滚动
    /// </summary>
    public class ScrollDisabler
    {
        public static void Disable(Widget widget)
        {
            if (widget == null) return;
            widget.AddEvents((int)Gdk.EventMask.ScrollMask);
            widget.ScrollEvent += widget_Scroll;
        }

        [ConnectBefore]
        private static void widget_Scroll(object o, ScrollEventArgs args)
        {
            args.RetVal = true;
        }
    }
}