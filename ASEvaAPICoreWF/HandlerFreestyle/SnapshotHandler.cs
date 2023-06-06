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
}
