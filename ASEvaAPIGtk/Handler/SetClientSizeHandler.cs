using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIGtk
{
    class SetClientSizeHandlerGtk : SetClientSizeHandler
    {
        public void SetClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            if (window is Form) window.Size = new Size(logicalWidth, logicalHeight);
            else if (window is Dialog) window.ClientSize = new Size(logicalWidth + 4, logicalHeight + 4);
            else window.ClientSize = new Size(logicalWidth, logicalHeight);
        }

        public void SetMinimumClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            if (window is Form) window.MinimumSize = new Size(logicalWidth, logicalHeight);
            else if (window is Dialog)
            {
                var dialog = window as Dialog;
                if (dialog.WindowStyle == WindowStyle.Default) window.MinimumSize = new Size(logicalWidth + 78, logicalHeight + 120);
                else if (dialog.WindowStyle == WindowStyle.None) window.MinimumSize = new Size(logicalWidth + 4, logicalHeight + 1);
                else window.MinimumSize = new Size(logicalWidth, logicalHeight);
            }
            else window.MinimumSize = new Size(logicalWidth, logicalHeight);
        }
    }
}