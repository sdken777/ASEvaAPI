using System;
using System.Runtime.InteropServices;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0649
    class LegacySetter
    {
        public static void SetLegacyGL()
        {
            IntPtr vTablePtr = Linux.gdk__private__();
            if (vTablePtr == IntPtr.Zero) return;

            unsafe
            {
                var vTable = *(VTable*)vTablePtr;
                Type delegateType = typeof(GdkGlSetFlagsDelegate);
                var gdkGlSetFlags = Marshal.GetDelegateForFunctionPointer(vTable.gdk_gl_set_flags, delegateType) as GdkGlSetFlagsDelegate;
                gdkGlSetFlags?.Invoke(1 << 5/* LEGACY */);
            }
        }

        private struct VTable
        {
            public IntPtr dummy1;
            public IntPtr dummy2;
            public IntPtr dummy3;
            public IntPtr dummy4;
            public IntPtr dummy5;
            public IntPtr gdk_gl_set_flags;
        }

        private delegate void GdkGlSetFlagsDelegate(int gdkGlFlags);
    }
}