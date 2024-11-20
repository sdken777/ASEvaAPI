using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.3.1) Extensions for bitmap operations
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.3.1) 方便图像操作
    /// </summary>
    public static class BitmapExtensions
    {
        public static Bitmap? ToAvaloniaBitmap(this byte[] imageBytes)
        {
            try
            {
                using (var stream = new MemoryStream(imageBytes))
                {
                    return new Bitmap(stream);
                }
            }
            catch (Exception) { return null; }
        }
    }
}