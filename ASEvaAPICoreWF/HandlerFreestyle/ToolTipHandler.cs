using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UICoreWF
{
    class ToolTipHandler : SetToolTipExtensions.SetToolTipHandler
    {
        public void SetToolTip(Control control, string tooltip)
        {
            if (control is ButtonPanel) return;
            if (control.ParentWindow != null) return;

            var winformControl = control.ControlObject as System.Windows.Forms.Control;
            globalToolTip.SetToolTip(winformControl, tooltip);
        }

        private System.Windows.Forms.ToolTip globalToolTip = new System.Windows.Forms.ToolTip();
    }
}
