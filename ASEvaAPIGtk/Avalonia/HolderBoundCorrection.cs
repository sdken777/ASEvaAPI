using System;
using System.Runtime.InteropServices;

namespace ASEva.UIGtk
{
    class HolderBoundCorrection
    {
        public static void Correct(uint container, double scale, int logicalWidth, int logicalHeight)
        {
            var x11Display = XOpenDisplay(null);

            var root = new uint[1];
            var holder = new uint[1];
            var children = new nint[1];
            var nchildren = new uint[1];
            XQueryTree(x11Display, container, root, holder, children, nchildren);
            if (holder[0] > 0)
            {
                var attribs = new uint[36];
                XGetWindowAttributes(x11Display, holder[0], attribs);
                if (Math.Abs(attribs[2] - logicalWidth) <= 1 && Math.Abs(attribs[3] - logicalHeight) <= 1)
                {
                    var targetX = (int)(attribs[0] * scale);
                    var targetY = (int)(attribs[1] * scale);
                    var targetWidth = (uint)(attribs[2] * scale);
                    var targetHeight = (uint)(attribs[3] * scale);
                    XMoveResizeWindow(x11Display, holder[0], targetX, targetY, targetWidth, targetHeight);
                    XMoveResizeWindow(x11Display, container, 0, 0, targetWidth, targetHeight);
                }
            }

            XCloseDisplay(x11Display);
        }

        [DllImport("libX11.so.6")]
        private static extern nint XOpenDisplay(string display_name);

        [DllImport("libX11.so.6")]
        private static extern int XCloseDisplay(nint x11_display);

        [DllImport("libX11.so.6")]
        private static extern int XQueryTree(nint x11_display, uint window, uint[] root, uint[] parent, nint[] children, uint[] nchildren);

        [DllImport("libX11.so.6")]
        private static extern int XGetWindowAttributes(nint x11_display, uint window, uint[] attribs);

        [DllImport("libX11.so.6")]
        private static extern int XMoveResizeWindow(nint x11_display, uint window, int x, int y, uint width, uint height);
    }
}