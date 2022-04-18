using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UICoreWF
{
    class SetClientSizeHandlerCoreWF : SetClientSizeHandler
    {
        public void SetClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            var realSize = window.Sizer(logicalWidth, logicalHeight);
            if (window.WindowStyle == WindowStyle.None) window.Size = realSize;
            else
            {
                var form = window.ControlObject as System.Windows.Forms.Form;
                window.Size = new Size(realSize.Width + form.Width - form.ClientSize.Width, realSize.Height + form.Height - form.ClientSize.Height);
            }
        }

        public void SetMinimumClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            var realSize = window.Sizer(logicalWidth, logicalHeight);
            if (window.WindowStyle == WindowStyle.None) window.MinimumSize = realSize;
            else
            {
                var form = window.ControlObject as System.Windows.Forms.Form;
                window.MinimumSize = new Size(realSize.Width + form.Width - form.ClientSize.Width, realSize.Height + form.Height - form.ClientSize.Height);
            }
        }
    }
}
