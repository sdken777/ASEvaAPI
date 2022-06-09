using System;
using SharpGL;

namespace ASEva.UIMonoMac
{
    class MacOSFuncLoader : FuncLoader
    {
        public IntPtr GetFunctionAddress(string name)
        {
            IntPtr address = IntPtr.Zero;
            if (MacOS.HandleGL != IntPtr.Zero)
            {
                if ((address = MacOS.dlsym(MacOS.HandleGL, name)) != IntPtr.Zero) return address;
            }
            if (MacOS.HandleGLU != IntPtr.Zero)
            {
                if ((address = MacOS.dlsym(MacOS.HandleGLU, name)) != IntPtr.Zero) return address;
            }
            if (MacOS.HandleGLEW != IntPtr.Zero)
            {
                var glewName = "__glew" + name.Substring(2);
                if ((address = MacOS.dlsym(MacOS.HandleGLEW, glewName)) != IntPtr.Zero) return address;
            }
            return IntPtr.Zero;
        }
    }
}