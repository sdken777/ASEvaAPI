using System;
using GLib;
using Gtk;

namespace ASEva.UIGtk
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:gtk=2.0.0) 开关框辅助类
    /// </summary>
    public class SwitchHelper
    {
        public void Add(Switch s)
        {
            if (s == null) return;
            s.AddNotification("active", switch_ActiveNotify);
        }

        public delegate void ToggledHandler(Switch s, bool toggled);

        public event ToggledHandler Toggled;

        private void switch_ActiveNotify(object o, NotifyArgs args)
        {
            var s = o as Switch;
            if (Toggled != null) Toggled(s, s.Active);
        }
    }
}