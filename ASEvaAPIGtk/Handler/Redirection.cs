using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ASEva.UIEto
{
    class Redirection
    {
        public static void RedirectMarshaller()
        {
            var d_g_filename_from_utf8 = typeof(GLib.Marshaller).GetNestedType("d_g_filename_from_utf8", BindingFlags.Static | BindingFlags.NonPublic);
            var g_filename_from_utf8 = typeof(GLib.Marshaller).GetField("g_filename_from_utf8", BindingFlags.Static | BindingFlags.NonPublic);
            g_filename_from_utf8.SetValue(null, Delegate.CreateDelegate(d_g_filename_from_utf8, typeof(Redirection), "g_filename_from_utf8_wrapper"));

            var d_g_filename_to_utf8 = typeof(GLib.Marshaller).GetNestedType("d_g_filename_to_utf8", BindingFlags.Static | BindingFlags.NonPublic);
            var g_filename_to_utf8 = typeof(GLib.Marshaller).GetField("g_filename_to_utf8", BindingFlags.Static | BindingFlags.NonPublic);
            g_filename_to_utf8.SetValue(null, Delegate.CreateDelegate(d_g_filename_to_utf8, typeof(Redirection), "g_filename_to_utf8_wrapper"));
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
    }
}