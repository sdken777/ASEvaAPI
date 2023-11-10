using System;
using ASEva;
using ASEva.Samples;
using ASEva.UIEto;
using Gtk;
using Cairo;

namespace ASEva.UIGtk
{
    class SnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage Snapshot(Eto.Forms.Control control)
        {
            var widget = control.ControlObject as Widget;
            if (widget == null) return null;

            if (widget is Window)
            {
                widget = (widget as Window).Child;
                if (widget == null) return null;
            }

            var w = widget.AllocatedWidth;
            var h = widget.AllocatedHeight;

            var surface = new ImageSurface(Format.Argb32, w, h);
            var cc = new Context(surface);
            widget.Draw(cc);
            cc.Dispose();

            var output = CommonImage.Create(w, h, true);
            unsafe
            {
                byte* srcData = (byte*)surface.DataPtr;
                fixed (byte* dstData = &(output.Data[0]))
                {
                    for (int v = 0; v < h; v++)
                    {
                        byte* srcRow = &srcData[v * surface.Stride];
                        byte* dstRow = &dstData[v * output.RowBytes];
                        for (int u = 0; u < w; u++)
                        {
                            if (srcRow[4 * u + 3] == 0) continue;
                            var ratio = (float)255.0f / srcRow[4 * u + 3];
                            dstRow[4 * u] = (byte)(ratio * srcRow[4 * u]);
                            dstRow[4 * u + 1] = (byte)(ratio * srcRow[4 * u + 1]);
                            dstRow[4 * u + 2] = (byte)(ratio * srcRow[4 * u + 2]);
                            dstRow[4 * u + 3] = srcRow[4 * u + 3];
                        }
                    }
                }
            }

            surface.Dispose();
            return output;
        }
    }

    class ScreenSnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage Snapshot(Eto.Forms.Control control)
        {
            var widget = control.ControlObject as Widget;
            if (widget == null) return null;

            if (widget is Window)
            {
                widget = (widget as Window).Child;
                if (widget == null) return null;
            }

            var pixbuf = new Gdk.Pixbuf(widget.Window, widget.Allocation.X, widget.Allocation.Y, widget.AllocatedWidth, widget.AllocatedHeight);
            var rawImage = ImageConverter.ConvertFromPlatformImage(pixbuf);

            if (rawImage.Width == widget.AllocatedWidth) return rawImage;
            else return rawImage.Resize(widget.AllocatedWidth);
        }
    }
}