using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ASEva.Samples;

namespace ASEva.UIWpf
{
    /// <summary>
    /// (api:wpf=1.0.0) 图像对象转换
    /// </summary>
    public class ImageConverter
    {
        public static object ConvertToBitmap(CommonImage image)
        {
            if (image == null) return null;

            if (image.WithAlpha)
            {
                return BitmapSource.Create(image.Width, image.Height, 96, 96, PixelFormats.Bgra32, null, image.Data, image.RowBytes);
            }
            else
            {
                return BitmapSource.Create(image.Width, image.Height, 96, 96, PixelFormats.Bgr24, null, image.Data, image.RowBytes);
            }
        }

        public static CommonImage ConvertFromBitmap(object bitmapObject)
        {
            if (bitmapObject == null) return null;
            if (!(bitmapObject is BitmapSource)) return null;

            var bitmap = bitmapObject as BitmapSource;
            if (bitmap.Format == PixelFormats.Bgra32)
            {
                var image = CommonImage.Create(bitmap.PixelWidth, bitmap.PixelHeight, true);
                bitmap.CopyPixels(image.Data, image.RowBytes, 0);
                return image;
            }
            else if (bitmap.Format == PixelFormats.Bgr24)
            {
                var image = CommonImage.Create(bitmap.PixelWidth, bitmap.PixelHeight, false);
                bitmap.CopyPixels(image.Data, image.RowBytes, 0);
                return image;
            }
            else return null;
        }
    }
}
