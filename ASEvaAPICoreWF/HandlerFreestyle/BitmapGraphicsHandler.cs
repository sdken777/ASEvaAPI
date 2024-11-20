using System;
using ASEva.UIEto;
using Eto.Drawing;

namespace ASEva.UICoreWF
{
    class BitmapGraphicsHandler : TextBitmap.ModifyBitmapGraphics
    {
        public void Modify(Graphics graphics)
        {
            if (graphics.ControlObject is System.Drawing.Graphics)
            {
                var winformGraphics = graphics.ControlObject as System.Drawing.Graphics;
                if (winformGraphics != null) winformGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            }
        }
    }
}
