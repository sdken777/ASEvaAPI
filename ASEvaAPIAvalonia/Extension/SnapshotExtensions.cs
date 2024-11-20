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
    /// (api:avalonia=1.3.1) Extensions for control snapshot
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.3.1) 方便控件截图
    /// </summary>
    public static class SnapshotExtensions
    {
        public static Bitmap? Snapshot(this Control control)
        {
            using (var bitmap = new RenderTargetBitmap(new PixelSize((int)control.Width, (int)control.Height), new Vector(96, 96)))
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