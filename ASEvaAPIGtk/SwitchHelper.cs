using System;
using GLib;
using Gtk;

namespace ASEva.UIGtk
{
    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Switch helper class
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 开关框辅助类
    /// </summary>
    public class SwitchHelper
    {
        public void Add(Switch s)
        {
            s.AddNotification("active", switch_ActiveNotify);
        }

        public delegate void ToggledHandler(Switch s, bool toggled);

        public event ToggledHandler? Toggled;

        private void switch_ActiveNotify(object o, NotifyArgs args)
        {
            var s = o as Switch;
            if (s == null) return;
            Toggled?.Invoke(s, s.Active);
        }
    }
}