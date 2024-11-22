using System;
using ASEva.Samples;
using Avalonia.Media.Imaging;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.16) Conversion of image object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.16) 图像对象转换
    /// </summary>
    public class ImageConverter
    {
        public static object ConvertToBitmap(CommonImage image)
        {
            return image?.ToAvaloniaBitmap();
        }

        public static CommonImage ConvertFromBitmap(object bitmapObject)
        {
            if (bitmapObject == null) return null;
            if (bitmapObject is not Bitmap) return null;

            var bitmap = bitmapObject as Bitmap;
            return bitmap.ToCommonImage();
        }
    }
}
