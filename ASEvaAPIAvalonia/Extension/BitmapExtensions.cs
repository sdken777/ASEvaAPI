using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.2.6) Extensions for bitmap operations
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.2.6) 方便图像操作
    /// </summary>
    public static class BitmapExtensions
    {
        public static Bitmap ToAvaloniaBitmap(this byte[] imageBytes)
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