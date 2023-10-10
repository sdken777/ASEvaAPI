using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UICoreWF
{
    class TopMostHandler : TopMostExtensions.QueryTopMost
    {
        public bool IsTopMost(Control control)
        {
            if (control.ControlObject is System.Windows.Forms.Control)
            {
                var winformControl = control.ControlObject as System.Windows.Forms.Control;
                var activeForm = System.Windows.Forms.Form.ActiveForm;
                while (true)
                {
                    if (winformControl is System.Windows.Forms.ContainerControl) break;
                    winformControl = winformControl.Parent;
                    if (winformControl == null) return false;
                }
                var containerControl = winformControl as System.Windows.Forms.ContainerControl;
                return containerControl.ParentForm != null && activeForm != null && activeForm.Equals(containerControl.ParentForm);
            }
            else return false;
        }
    }
}
