using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIWpf
{
    class TopMostHandler : TopMostExtensions.QueryTopMost
    {
        public bool IsTopMost(Control control)
        {
            if (control.ControlObject is System.Windows.DependencyObject)
            {
                var wpfControl = control.ControlObject as System.Windows.DependencyObject;
                var rootWindow = System.Windows.Window.GetWindow(wpfControl);
                if (rootWindow != null) return rootWindow.IsActive;
                else return false;
            }
            else return false;
        }
    }
}
