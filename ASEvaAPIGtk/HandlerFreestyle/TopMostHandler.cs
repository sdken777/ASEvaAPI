using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIGtk
{
    class TopMostHandler : TopMostExtensions.QueryTopMost
    {
        public bool IsTopMost(Control control)
        {
            if (control.ControlObject is Gtk.Widget widget)
            {
                if (widget.Toplevel is Gtk.Window toplevelWindow)
                {
                    return toplevelWindow.IsActive;
                }
            }
            return false;
        }
    }
}