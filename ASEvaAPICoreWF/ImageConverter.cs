using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ASEva.Samples;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=3.0.0) Conversion of image object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.0.0) 图像对象转换
    /// </summary>
    public class ImageConverter
    {
        public static object ConvertToBitmap(CommonImage image)
        {
            Bitmap bitmap;
            if (image.WithAlpha)
            {
                bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                if (image.BgrInverted)
                {
                    unsafe
                    {
                        byte* dstData = (byte*)bitmapData.Scan0;
                        fixed (byte* srcData = &image.Data[0])
                        {
                            for (int i = 0; i < image.Height; i++)
                            {
                                byte* srcRow = &srcData[i * image.RowBytes];
                                byte* dstRow = &dstData[i * bitmapData.Stride];
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
                }
                else
                {
                    for (int i = 0; i < image.Height; i++)
                    {
                        Marshal.Copy(image.Data, i * image.RowBytes, bitmapData.Scan0 + i * bitmapData.Stride, 4 * image.Width);
                    }
                }
                bitmap.UnlockBits(bitmapData);
            }
            else
            {
                bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                if (image.BgrInverted)
                {
                    unsafe
                    {
                        byte* dstData = (byte*)bitmapData.Scan0;
                        fixed (byte* srcData = &image.Data[0])
                        {
                            for (int i = 0; i < image.Height; i++)
                            {
                                byte* srcRow = &srcData[i * image.RowBytes];
                                byte* dstRow = &dstData[i * bitmapData.Stride];
                                for (int n = 0; n < image.Width; n++)
                                {
                                    dstRow[3 * n] = srcRow[3 * n + 2];
                                    dstRow[3 * n + 1] = srcRow[3 * n + 1];
                                    dstRow[3 * n + 2] = srcRow[3 * n];
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < image.Height; i++)
                    {
                        Marshal.Copy(image.Data, i * image.RowBytes, bitmapData.Scan0 + i * bitmapData.Stride, 3 * image.Width);
                    }
                }
                bitmap.UnlockBits(bitmapData);
            }
            return bitmap;
        }

        public static CommonImage? ConvertFromBitmap(object bitmapObject)
        {
            if (!(bitmapObject is Bitmap bitmap)) return null;

            if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
            {
                var image = CommonImage.Create(bitmap.Width, bitmap.Height, true, false);
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                for (int i = 0; i < image.Height; i++)
                {
                    Marshal.Copy(bitmapData.Scan0 + i * bitmapData.Stride, image.Data, i * image.RowBytes, 4 * image.Width);
                }
                bitmap.UnlockBits(bitmapData);
                return image;
            }
            else if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
            {
                var image = CommonImage.Create(bitmap.Width, bitmap.Height, false, false);
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
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
