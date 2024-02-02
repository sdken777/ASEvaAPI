using System;
using ASEva.UIEto;
using Eto.Forms;
using MonoMac.AppKit;

namespace ASEva.UIMonoMac
{
    class TopMostHandlerMonoMac : TopMostExtensions.QueryTopMost
    {
        public bool IsTopMost(Control control)
        {
            var window = control.ParentWindow;
            if (window == null) return false;

            var nsWindow = window.ControlObject as NSWindow;
            if (nsWindow == null) return false;

            return nsWindow.IsKeyWindow;
        }
    }
}