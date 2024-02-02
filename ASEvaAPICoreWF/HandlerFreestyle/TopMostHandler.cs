using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UICoreWF
{
    class TopMostHandler : TopMostExtensions.QueryTopMost
    {
        public bool IsTopMost(Control control)
        {
            var window = control.ParentWindow;
            if (window == null) return false;

            var form = window.ControlObject as System.Windows.Forms.Form;
            if (form == null) return false;

            var activeForm = System.Windows.Forms.Form.ActiveForm;
            if (activeForm == null) return false;

            return form.Equals(activeForm);
        }
    }
}
