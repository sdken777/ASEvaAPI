using System;
using System.Runtime.InteropServices;
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

    class ScreenSnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage Snapshot(Eto.Forms.Control control)
        {
            var element = control.ControlObject as FrameworkElement;
            if (element == null) return null;

            if (element is Window)
            {
                element = (element as Window).Content as FrameworkElement;
                if (element == null) return null;
            }

            var topLeft = element.PointToScreen(new Point(0, 0));
            var bottomRight = element.PointToScreen(new Point(element.ActualWidth, element.ActualHeight));
            var width = bottomRight.X - topLeft.X;
            var height = bottomRight.Y - topLeft.Y;

            var bitmap = new System.Drawing.Bitmap((int)width, (int)height);
            using (var g = System.Drawing.Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen((int)topLeft.X, (int)topLeft.Y, 0, 0, bitmap.Size);
            }

            var rawImage = ConvertFromBitmap(bitmap);
            if (rawImage.Width == element.ActualWidth) return rawImage;
            else return rawImage.Resize((int)element.ActualWidth);
        }

        private static CommonImage ConvertFromBitmap(object bitmapObject)
        {
            if (bitmapObject == null) return null;
            if (!(bitmapObject is System.Drawing.Bitmap)) return null;

            var bitmap = bitmapObject as System.Drawing.Bitmap;
            if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                var image = CommonImage.Create(bitmap.Width, bitmap.Height, true, false);
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                for (int i = 0; i < image.Height; i++)
                {
                    Marshal.Copy(bitmapData.Scan0 + i * bitmapData.Stride, image.Data, i * image.RowBytes, 4 * image.Width);
                }
                bitmap.UnlockBits(bitmapData);
                return image;
            }
            else if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                var image = CommonImage.Create(bitmap.Width, bitmap.Height, false, false);
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                for (int i = 0; i < image.Height; i++)
                {
                    Marshal.Copy(bitmapData.Scan0 + i * bitmapData.Stride, image.Data, i * image.RowBytes, 3 * image.Width);
                }
                bitmap.UnlockBits(bitmapData);
                return image;
            }
            else return null;
        }
    }
}
