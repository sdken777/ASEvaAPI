using System;
using Gtk;

namespace ASEva.UIGtk
{
    class FullScreenHandler : ASEva.UIEto.FullScreenExtensions.FullScreenHandler
    {
        public void FullScreen(Eto.Forms.Window window)
        {
            var gtkWindow = window.ControlObject as Window;
            if (gtkWindow != null)
            {
                gtkWindow.Fullscreen();
            }
        }
    }
}