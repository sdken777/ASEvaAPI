using System;
using GLib;
using Gdk;

namespace ASEva.UIGtk
{
    class BackendChecker
    {
        public static BackendType Check(Window window)
        {
            try
            {
                var x11WindowType = new GType(Linux.gdk_x11_window_get_type());
                if (x11WindowType.IsInstance(window.Handle)) return BackendType.X11;
            }
            catch (Exception) {}

            try
            {
                var waylandWindowType = new GType(Linux.gdk_wayland_window_get_type());
                if (waylandWindowType.IsInstance(window.Handle)) return BackendType.Wayland;
            }
            catch (Exception) {}

            try
            {
                var mirWindowType = new GType(Linux.gdk_mir_window_get_type());
                if (mirWindowType.IsInstance(window.Handle)) return BackendType.Mir;
            }
            catch (Exception) {}

            return BackendType.Unknown;
        }
    }

    enum BackendType
    {
        Unknown = 0,
        X11 = 1,
        Wayland = 2,
        Mir = 3,
    }
}