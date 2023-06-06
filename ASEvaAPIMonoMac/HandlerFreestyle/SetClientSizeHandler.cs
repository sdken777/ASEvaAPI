using System;
using Eto.Forms;
using Eto.Drawing;
using ASEva.UIEto;
using MonoMac.AppKit;

namespace ASEva.UIMonoMac
{
    class SetClientSizeHandlerMonoMac : SetClientSizeHandler
    {
        public void SetClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            window.ClientSize = new Size(logicalWidth, logicalHeight);
        }

        public void SetMinimumClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            var nsWindow = window.ControlObject as NSWindow;
            var contentRect = NSWindow.ContentRectFor(nsWindow.Frame, nsWindow.StyleMask);
            var barHeight = (int)Math.Max(0, nsWindow.Frame.Height - contentRect.Height);
            window.MinimumSize = new Size(logicalWidth, logicalHeight + barHeight);
        }
    }
}