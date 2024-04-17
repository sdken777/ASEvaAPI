using System;
using System.Drawing;
using Eto;
using Eto.Drawing;
using Eto.WinForms.Drawing;

namespace ASEva.UICoreWF
{
    // TOCHECK: 修正高DPI下绘制图像尺寸问题
    class SafeBitmapHandler : BitmapHandler, IWindowsImage
    {
        public new void DrawImage(GraphicsHandler graphics, Eto.Drawing.RectangleF source, Eto.Drawing.RectangleF destination)
        {
            beforeDrawImage(graphics);
            graphics.Control.DrawImage(base.Control, destination.ToSD(), source.ToSD(), GraphicsUnit.Pixel);
            afterDrawImage();
        }

        public new void DrawImage(GraphicsHandler graphics, float x, float y)
        {
            beforeDrawImage(graphics);
            graphics.Control.DrawImage(base.Control, x, y);
            afterDrawImage();
        }

        public new void DrawImage(GraphicsHandler graphics, float x, float y, float width, float height)
        {
            beforeDrawImage(graphics);
            graphics.Control.DrawImage(base.Control, x, y, width, height);
            afterDrawImage();
        }

        private void beforeDrawImage(GraphicsHandler graphics)
        {
            originDpiX = base.Control.HorizontalResolution;
            originDpiY = base.Control.VerticalResolution;
            differentDpi = graphics.Control.DpiX != originDpiX || graphics.Control.DpiY != originDpiY;
            if (differentDpi) base.Control.SetResolution(graphics.Control.DpiX, graphics.Control.DpiY);
        }

        private void afterDrawImage()
        {
            if (differentDpi) base.Control.SetResolution(originDpiX, originDpiY);
        }

        float originDpiX = 96, originDpiY = 96;
        bool differentDpi = false;
    }
}
