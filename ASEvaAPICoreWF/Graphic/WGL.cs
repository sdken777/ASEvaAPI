using System;
using SharpGL;

namespace ASEva.UICoreWF
{
    class WGL
    {
        public static String GetWglExtensionsString(OpenGL gl, IntPtr hdc)
        {
            if (wglExtensionsString == null)
            {
                wglExtensionsString = "";
                try { wglExtensionsString = gl.GetExtensionsStringARB(hdc); }
                catch (Exception) { }
            }
            return wglExtensionsString;
        }

        private static String wglExtensionsString = null;
    }
}
