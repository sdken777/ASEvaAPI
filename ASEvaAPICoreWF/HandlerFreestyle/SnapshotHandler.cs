using System;
using ASEva;
using ASEva.Samples;
using ASEva.UIEto;
using System.Windows.Forms;
using System.Drawing;

namespace ASEva.UICoreWF
{
    class SnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage? Snapshot(Eto.Forms.Control control)
        {
            var winformControl = control.ControlObject as Control;
            if (winformControl == null) return null;

            int x = 0, y = 0, w = winformControl.Width, h = winformControl.Height;
            var logicalWidth = control.GetLogicalWidth();
            if (winformControl is Form)
            {
                var clientXY = winformControl.PointToScreen(new Point(0, 0));
                var clientSize = winformControl.ClientSize;
                x = clientXY.X - winformControl.Location.X;
                y = clientXY.Y - winformControl.Location.Y;
                w = clientSize.Width;
                h = clientSize.Height;
                logicalWidth = (int)((float)clientSize.Width / Pixel.Scale);
            }

            var bitmap = new Bitmap(x + w, y + h);
            winformControl.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            if (x > 0 || y > 0) bitmap = bitmap.Clone(new Rectangle(x, y, w, h), bitmap.PixelFormat);
            var rawImage = ImageConverter.ConvertFromBitmap(bitmap);

            if (rawImage == null || rawImage.Width == logicalWidth) return rawImage;
            else return rawImage.Resize(logicalWidth);
        }
    }

    class ScreenSnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage? Snapshot(Eto.Forms.Control control)
        {
            var winformControl = control.ControlObject as Control;
            if (winformControl == null) return null;

            int w = winformControl.Width, h = winformControl.Height;
            var logicalWidth = control.GetLogicalWidth();
            if (winformControl is Form)
            {
                var clientSize = winformControl.ClientSize;
                w = clientSize.Width;
                h = clientSize.Height;
                logicalWidth = (int)((float)clientSize.Width / Pixel.Scale);
            }

            var topLeft = winformControl.PointToScreen(new Point(0, 0));
            var bottomRight = winformControl.PointToScreen(new Point(w, h));
            var width = bottomRight.X - topLeft.X;
            var height = bottomRight.Y - topLeft.Y;

            var bitmap = new Bitmap((int)width, (int)height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen((int)topLeft.X, (int)topLeft.Y, 0, 0, bitmap.Size);
            }

            var rawImage = ImageConverter.ConvertFromBitmap(bitmap);

            if (rawImage == null || rawImage.Width == logicalWidth) return rawImage;
            else return rawImage.Resize(logicalWidth);
        }
    }
}
