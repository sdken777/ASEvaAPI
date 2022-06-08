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
            if (Linux.HandleGLEW != IntPtr.Zero)
            {
                var glewName = "__glew" + name.Substring(2);
                if ((address = Linux.dlsym(Linux.HandleGLEW, glewName)) != IntPtr.Zero) return address;
            }
            return IntPtr.Zero;
        }
    }
}