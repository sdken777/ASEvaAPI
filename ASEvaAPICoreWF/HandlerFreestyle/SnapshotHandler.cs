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
        public CommonImage Snapshot(Eto.Forms.Control control)
        {
            var winformControl = control.ControlObject as Control;
            if (winformControl == null) return null;

            var bitmap = new Bitmap(winformControl.Width, winformControl.Height);
            winformControl.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            return ImageConverter.ConvertFromBitmap(bitmap);
        }
    }

    class ScreenSnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage Snapshot(Eto.Forms.Control control)
        {
            var winformControl = control.ControlObject as Control;
            if (winformControl == null) return null;

            var topLeft = winformControl.PointToScreen(new Point(0, 0));
            var bottomRight = winformControl.PointToScreen(new Point(winformControl.Width, winformControl.Height));
            var width = bottomRight.X - topLeft.X;
            var height = bottomRight.Y - topLeft.Y;

            var bitmap = new System.Drawing.Bitmap((int)width, (int)height);
            using (var g = System.Drawing.Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen((int)topLeft.X, (int)topLeft.Y, 0, 0, bitmap.Size);
            }

            return ImageConverter.ConvertFromBitmap(bitmap);
        }
    }
    }
