using System;
using System.IO;
using ASEva.Samples;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.2.6) Extensions for control snapshot
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.2.6) 方便控件截图
    /// </summary>
    public static class SnapshotExtensions
    {
        public static Bitmap Snapshot(this Control control)
        {
            var width = control.Width;
            var height = control.Height;
            if (width <= 0 || width >= 10000 || !Double.IsNormal(width) || height <= 0 || height >= 10000 || !Double.IsNormal(height))
            {
                width = control.Bounds.Width;
                height = control.Bounds.Height;
                if (width <= 0 || width >= 10000 || !Double.IsNormal(width) || height <= 0 || height >= 10000 || !Double.IsNormal(height)) return null;
            }
            using (var bitmap = new RenderTargetBitmap(new PixelSize((int)width, (int)height), new Vector(96, 96)))
            {
                bitmap.Render(control);
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream);
                    return stream.ToArray().ToAvaloniaBitmap();
                }
            }
        }
    }
}