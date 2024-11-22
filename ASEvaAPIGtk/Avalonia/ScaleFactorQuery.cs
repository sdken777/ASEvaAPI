using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0649

    class ScaleFactorQuery
    {
        public static double? Query()
        {
            String dummy = null;
            return Query(out dummy);
        }

        public static double? Query(out String screenName)
        {
            screenName = null;

            nint x11Display = XOpenDisplay(null);
            var rootWindow = XDefaultRootWindow(x11Display);
            var rmCharPtr = XResourceManagerString(x11Display);

            nint screenResources = XRRGetScreenResources(x11Display, rootWindow);
            var outputPrimary = XRRGetOutputPrimary(x11Display, rootWindow);
            nint outputInfoPtr = XRRGetOutputInfo(x11Display, screenResources, outputPrimary);

            var rmString = charPtrToString(rmCharPtr);
            double? scale = null;
            foreach (var row in rmString.Split('\n'))
            {
                var comps = row.Split(":\t");
                double number = 0;
                if (comps.Length == 2 && comps[0] == "Xft.dpi" && Double.TryParse(comps[1], out number)) scale = number / 96;
            }

            unsafe
            {
                XRROutputInfo *outputInfo = (XRROutputInfo*)outputInfoPtr;
                screenName = charPtrToString(outputInfo->nameCharPtr);
            }

            XCloseDisplay(x11Display);
            return scale;
        }

        private static String charPtrToString(nint charPtr)
        {
            unsafe
            {
                byte *chars = (byte*)charPtr;
                var charList = new List<char>();
                if (chars != null)
                {
                    while (*chars != 0)
                    {
                        charList.Add((char)*chars);
                        chars++;
                    }
                }
                return new String(charList.ToArray());
            }
        }

        private struct XRROutputInfo
        {
            public nint dummy1;
            public nint dummy2;
            public nint nameCharPtr;
        };

        [DllImport("libX11.so.6")]
        private static extern nint XOpenDisplay(string display_name);

        [DllImport("libX11.so.6")]
        private static extern int XCloseDisplay(nint x11_display);

        [DllImport("libX11.so.6")]
        private static extern uint XDefaultRootWindow(nint x11_display);

        [DllImport("libX11.so.6")]
        private static extern nint XResourceManagerString(nint x11_display);

        [DllImport("libXrandr.so.2")]
        private static extern nint XRRGetScreenResources(nint x11_display, uint window);

        [DllImport("libXrandr.so.2")]
        private static extern uint XRRGetOutputPrimary(nint x11_display, uint window);

        [DllImport("libXrandr.so.2")]
        private static extern nint XRRGetOutputInfo(nint x11_display, nint screen_resources, uint output_primary);
    }
}