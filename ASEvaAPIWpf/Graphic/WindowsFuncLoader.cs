using System;
using System.Collections.Generic;
using System.Text;
using SharpGL;

namespace ASEva.UIWpf
{
    class WindowsFuncLoader : FuncLoader
    {
        public IntPtr GetFunctionAddress(string name)
        {
            IntPtr address = IntPtr.Zero;
            if (Win32.HandleOpengl32 != IntPtr.Zero)
            {
                if ((address = Win32.GetProcAddress(Win32.HandleOpengl32, name)) != IntPtr.Zero) return address;
            }
            if (Win32.HandleGlu32 != IntPtr.Zero)
            {
                if ((address = Win32.GetProcAddress(Win32.HandleGlu32, name)) != IntPtr.Zero) return address;
            }
            return Win32.wglGetProcAddress(name);
        }
    }
}
