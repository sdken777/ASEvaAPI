using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612
    class Redirection
    {
        public static void RedirectMarshaller()
        {
            var d_g_filename_from_utf8 = typeof(GLib.Marshaller).GetNestedType("d_g_filename_from_utf8", BindingFlags.Static | BindingFlags.NonPublic);
            var g_filename_from_utf8 = typeof(GLib.Marshaller).GetField("g_filename_from_utf8", BindingFlags.Static | BindingFlags.NonPublic);
            if (d_g_filename_from_utf8 != null) g_filename_from_utf8?.SetValue(null, Delegate.CreateDelegate(d_g_filename_from_utf8, typeof(Redirection), "g_filename_from_utf8_wrapper"));

            var d_g_filename_to_utf8 = typeof(GLib.Marshaller).GetNestedType("d_g_filename_to_utf8", BindingFlags.Static | BindingFlags.NonPublic);
            var g_filename_to_utf8 = typeof(GLib.Marshaller).GetField("g_filename_to_utf8", BindingFlags.Static | BindingFlags.NonPublic);
            if (d_g_filename_to_utf8 != null) g_filename_to_utf8?.SetValue(null, Delegate.CreateDelegate(d_g_filename_to_utf8, typeof(Redirection), "g_filename_to_utf8_wrapper"));
        }

        [DllImport("glib-2.0", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr g_filename_from_utf8(IntPtr mem, long len, IntPtr read, out IntPtr written, out IntPtr error);

        private static IntPtr g_filename_from_utf8_wrapper(IntPtr mem, int len, IntPtr read, out IntPtr written, out IntPtr error)
        {
            return g_filename_from_utf8(mem, len, read, out written, out error);
        }

        [DllImport("glib-2.0", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr g_filename_to_utf8(IntPtr mem, long len, IntPtr read, out IntPtr written, out IntPtr error);

        private static IntPtr g_filename_to_utf8_wrapper(IntPtr mem, int len, IntPtr read, out IntPtr written, out IntPtr error)
        {
            return g_filename_to_utf8(mem, len, read, out written, out error);
        }

        public static void RedirectMenu()
        {
            var d_gtk_menu_popup_at_pointer = typeof(Gtk.Menu).GetNestedType("d_gtk_menu_popup_at_pointer", BindingFlags.Static | BindingFlags.NonPublic);
            var gtk_menu_popup_at_pointer = typeof(Gtk.Menu).GetField("gtk_menu_popup_at_pointer", BindingFlags.Static | BindingFlags.NonPublic);
            if (d_gtk_menu_popup_at_pointer != null) gtk_menu_popup_at_pointer?.SetValue(null, Delegate.CreateDelegate(d_gtk_menu_popup_at_pointer, typeof(Redirection), "gtk_menu_popup_at_pointer"));
        }

        private static void gtk_menu_popup_at_pointer(IntPtr menuPtr, IntPtr evPtr)
        {
            var menu = new Gtk.Menu(menuPtr);

            var evRaw = evPtr == IntPtr.Zero ? Gtk.Global.CurrentEvent : new Gdk.Event(evPtr);
            if (evRaw.Type != Gdk.EventType.ButtonPress && evRaw.Type != Gdk.EventType.ButtonRelease) return;

            var ev = new Gdk.EventButton(evRaw.Handle);
            if (ev.Window == null || ev.Device == null) return;

            menu.PopupForDevice(ev.Device, null, null, null, null, ev.Button, ev.Time);
        }
    }
}