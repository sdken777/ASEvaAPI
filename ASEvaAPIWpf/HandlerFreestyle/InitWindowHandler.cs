using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIWpf
{
    class InitWindowHandlerWpf : InitWindowHandler
    {
        public void InitWindow(Window window)
        {
            if (window.BackgroundColor == Color.FromArgb(0xf0, 0xf0, 0xf0))
            {
                window.BackgroundColor = Colors.Transparent;
            }
        }
    }
}
