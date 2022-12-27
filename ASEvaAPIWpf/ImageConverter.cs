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
                if (image.BgrInverted)
                {
                    var imageData = new byte[image.Height * image.RowBytes];
                    unsafe
                    {
                        fixed (byte* srcData = &image.Data[0], dstData = &imageData[0])
                        {
                            for (int i = 0; i < image.Height; i++)
                            {
                                byte* srcRow = &srcData[i * image.RowBytes];
                                byte* dstRow = &dstData[i * image.RowBytes];
                                for (int n = 0; n < image.Width; n++)
                                {
                                    dstRow[4 * n] = srcRow[4 * n + 2];
                                    dstRow[4 * n + 1] = srcRow[4 * n + 1];
                                    dstRow[4 * n + 2] = srcRow[4 * n];
                                    dstRow[4 * n + 3] = srcRow[4 * n + 3];
                                }
                            }
                        }
                    }
                    return BitmapSource.Create(image.Width, image.Height, 96, 96, PixelFormats.Bgra32, null, imageData, image.RowBytes);
                }
                else
                {
                    return BitmapSource.Create(image.Width, image.Height, 96, 96, PixelFormats.Bgra32, null, image.Data, image.RowBytes);
                }
            }
            else
            {
                if (image.BgrInverted)
                {
                    var imageData = new byte[image.Height * image.RowBytes];
                    unsafe
                    {
                        fixed (byte* srcData = &image.Data[0], dstData = &imageData[0])
                        {
                            for (int i = 0; i < image.Height; i++)
                            {
                                byte* srcRow = &srcData[i * image.RowBytes];
                                byte* dstRow = &dstData[i * image.RowBytes];
                                for (int n = 0; n < image.Width; n++)
                                {
                                    dstRow[3 * n] = srcRow[3 * n + 2];
                                    dstRow[3 * n + 1] = srcRow[3 * n + 1];
                                    dstRow[3 * n + 2] = srcRow[3 * n];
                                }
                            }
                        }
                    }
                    return BitmapSource.Create(image.Width, image.Height, 96, 96, PixelFormats.Bgr24, null, imageData, image.RowBytes);
                }
                else
                {
                    return BitmapSource.Create(image.Width, image.Height, 96, 96, PixelFormats.Bgr24, null, image.Data, image.RowBytes);
                }
            }
        }

        public static CommonImage ConvertFromBitmap(object bitmapObject)
        {
            if (bitmapObject == null) return null;
            if (!(bitmapObject is BitmapSource)) return null;

            var bitmap = bitmapObject as BitmapSource;
            if (bitmap.Format == PixelFormats.Bgra32)
            {
                var image = CommonImage.Create(bitmap.PixelWidth, bitmap.PixelHeight, true, false);
                bitmap.CopyPixels(image.Data, image.RowBytes, 0);
                return image;
            }
            else if (bitmap.Format == PixelFormats.Bgr24)
            {
                var image = CommonImage.Create(bitmap.PixelWidth, bitmap.PixelHeight, false, false);
                bitmap.CopyPixels(image.Data, image.RowBytes, 0);
                return image;
            }
            else return null;
        }
    }
}
