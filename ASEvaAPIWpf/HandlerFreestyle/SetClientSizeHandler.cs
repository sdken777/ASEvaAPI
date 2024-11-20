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
            if (wpfWindow == null) return;
            if (wpfWindow.WindowStyle == System.Windows.WindowStyle.None) window.MinimumSize = new Size(logicalWidth, logicalHeight);
            else if (wpfWindow.WindowStyle == System.Windows.WindowStyle.SingleBorderWindow || wpfWindow.WindowStyle == System.Windows.WindowStyle.ThreeDBorderWindow)
            {
                wpfWindow.Loaded += delegate
                {
                    var content = wpfWindow.Content as System.Windows.FrameworkElement;
                    if (content == null) return;
                    var dw = (int)Math.Round(wpfWindow.ActualWidth - content.ActualWidth);
                    var dh = (int)Math.Round(wpfWindow.ActualHeight - content.ActualHeight);
                    window.MinimumSize = new Size(logicalWidth + dw, logicalHeight + dh);
                };
            }
            else window.MinimumSize = new Size(logicalWidth, logicalHeight);
        }
    }
}
