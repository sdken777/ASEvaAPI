using System;
using System.Windows.Forms;

namespace ASEva.UICoreWF
{
    class FullScreenHandler : ASEva.UIEto.FullScreenExtensions.FullScreenHandler
    {
        public void FullScreen(Eto.Forms.Window window)
        {
            var form = window.ControlObject as Form;
            if (form != null)
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
            }
        }
    }
}
