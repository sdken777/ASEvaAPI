using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ASEva;
using ASEva.Samples;
using ASEva.UIEto;

namespace ASEva.UIWpf
{
    class SnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage Snapshot(Eto.Forms.Control control)
        {
            var element = control.ControlObject as FrameworkElement;
            if (element == null) return null;

            var bounds = VisualTreeHelper.GetDescendantBounds(element);

            var visual = new DrawingVisual();
            using (var context = visual.RenderOpen())
            {
                var visualBrush = new VisualBrush(element);
                context.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
            }

            var renderTarget = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);
            renderTarget.Render(visual);

            var output = CommonImage.Create(renderTarget.PixelWidth, renderTarget.PixelHeight, true);
            renderTarget.CopyPixels(output.Data, output.RowBytes, 0);
            return output;
        }
    }
}
