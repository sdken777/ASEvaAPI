using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIWpf
{
    class SetClientSizeHandlerWpf : SetClientSizeHandler
    {
        public void SetClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            window.ClientSize = new Size(logicalWidth, logicalHeight);
        }

        public void SetMinimumClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            var wpfWindow = window.ControlObject as System.Windows.Window;
            if (wpfWindow.WindowStyle == System.Windows.WindowStyle.None) window.MinimumSize = new Size(logicalWidth, logicalHeight);
            else if (wpfWindow.WindowStyle == System.Windows.WindowStyle.SingleBorderWindow) window.MinimumSize = new Size(logicalWidth + 14, logicalHeight + 37);
            else if (wpfWindow.WindowStyle == System.Windows.WindowStyle.ThreeDBorderWindow) window.MinimumSize = new Size(logicalWidth + 16, logicalHeight + 39);
            else window.MinimumSize = new Size(logicalWidth, logicalHeight);
        }
    }
}
