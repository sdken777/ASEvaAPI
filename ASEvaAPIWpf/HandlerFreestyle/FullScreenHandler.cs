using System;
using System.Windows;

namespace ASEva.UIWpf
{
    class FullScreenHandler : ASEva.UIEto.FullScreenExtensions.FullScreenHandler
    {
        public void FullScreen(Eto.Forms.Window window)
        {
            var wpfWindow = window.ControlObject as Window;
            if (wpfWindow != null)
            {
                wpfWindow.WindowStyle = WindowStyle.None;
                wpfWindow.WindowState = WindowState.Maximized;
            }
        }
    }
}
