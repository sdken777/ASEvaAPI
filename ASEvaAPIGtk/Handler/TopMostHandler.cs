using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIGtk
{
    class TopMostHandler : TopMostExtensions.QueryTopMost
    {
        public bool IsTopMost(Control control)
        {
            if (control.ControlObject is Gtk.Widget)
            {
                var widget = control.ControlObject as Gtk.Widget;
                if (widget.Toplevel is Gtk.Window)
                {
                    var toplevelWindow = widget.Toplevel as Gtk.Window;
                    return toplevelWindow.IsActive;
                }
            }
            return false;
        }
    }
}