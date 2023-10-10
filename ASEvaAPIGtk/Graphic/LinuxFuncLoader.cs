using System;
using SharpGL;

namespace ASEva.UIGtk
{
    class LinuxFuncLoader : FuncLoader
    {
        public IntPtr GetFunctionAddress(string name)
        {
            IntPtr address = IntPtr.Zero;
            if (Linux.HandleGL != IntPtr.Zero)
            {
                if ((address = Linux.dlsym(Linux.HandleGL, name)) != IntPtr.Zero) return address;
            }
            if (Linux.HandleGLU != IntPtr.Zero)
            {
                if ((address = Linux.dlsym(Linux.HandleGLU, name)) != IntPtr.Zero) return address;
            }
            if (UseEGL)
            {
                if (!name.StartsWith("glX") && (address = Linux.eglGetProcAddress(name)) != IntPtr.Zero) return address;
            }
            else
            {
                if (!name.StartsWith("egl") && (address = Linux.glXGetProcAddress(name)) != IntPtr.Zero) return address;
            }
            return IntPtr.Zero;
        }

        public static bool UseEGL { private get; set; }
    }
}