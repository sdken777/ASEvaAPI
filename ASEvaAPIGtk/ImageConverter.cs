using System;
using ASEva.Samples;

namespace ASEva.UIGtk
{
    /// \~English
    /// <summary>
    /// (api:gtk=2.1.0) Conversion of image object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=2.1.0) 图像对象转换 
    /// </summary>
    public class ImageConverter
    {
        public static object ConvertToVideoFrameSampleImage(CommonImage image)
        {
            return VideoFrameSampleImage.FromCommonImage(image);
        }

        public static CommonImage ConvertFromPlatformImage(object platformImage)
        {
            if (platformImage == null) return null;

            if (platformImage is VideoFrameSampleImage)
            {
                var sampleImage = platformImage as VideoFrameSampleImage;
                var image = CommonImage.Create(sampleImage.Width, sampleImage.Height, sampleImage.WithAlpha, false);
                unsafe
                {
                    fixed (byte* srcData = &(sampleImage.Data[0]), dstData = &(image.Data[0]))
                    {
                        if (sampleImage.BgrInverted)
                        {
                            for (int i = 0; i < sampleImage.Height; i++)
                            {
                                byte* srcRow = srcData + i * sampleImage.RowBytes;
                                byte* dstRow = dstData + i * image.RowBytes;
                                if (sampleImage.WithAlpha)
                                {
                                    for (int n = 0; n < sampleImage.Width; n++)
                                    {
                                        dstRow[4 * n] = srcRow[4 * n + 2];
                                        dstRow[4 * n + 1] = srcRow[4 * n + 1];
                                        dstRow[4 * n + 2] = srcRow[4 * n];
                                        dstRow[4 * n + 3] = srcRow[4 * n + 3];
                                    }
                                }
                                else
                                {
                                    for (int n = 0; n < sampleImage.Width; n++)
                                    {
                                        dstRow[3 * n] = srcRow[3 * n + 2];
                                        dstRow[3 * n + 1] = srcRow[3 * n + 1];
                                        dstRow[3 * n + 2] = srcRow[3 * n];
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < sampleImage.Height; i++)
                            {
                                byte* srcRow = srcData + i * sampleImage.RowBytes;
                                byte* dstRow = dstData + i * image.RowBytes;
                                int copyBytes = Math.Min(sampleImage.RowBytes, image.RowBytes);
                                for (int n = 0; n < copyBytes; n++) dstRow[n] = srcRow[n];
                            }
                        }
                    }
                }
                return image;
            }
            else if (platformImage is Gdk.Pixbuf)
            {
                var pixbuf = platformImage as Gdk.Pixbuf;
                if (pixbuf.HasAlpha)
                {
                    var image = CommonImage.Create(pixbuf.Width, pixbuf.Height, true, false);
                    unsafe
                    {
                        byte* srcData = (byte*)pixbuf.Pixels;
                        fixed (byte* dstData = &(image.Data[0]))
                        {
                            for (int v = 0; v < image.Height; v++)
                            {
                                byte* srcRow = &srcData[v * pixbuf.Rowstride];
                                byte* dstRow = &dstData[v * image.RowBytes];
                                for (int u = 0; u < image.Width; u++)
                                {
                                    dstRow[4 * u] = srcRow[4 * u + 2];
                                    dstRow[4 * u + 1] = srcRow[4 * u + 1];
                                    dstRow[4 * u + 2] = srcRow[4 * u];
                                    dstRow[4 * u + 3] = srcRow[4 * u + 3];
                                }
                            }
                        }
                    }
                    return image;
                }
                else
                {
                    var image = CommonImage.Create(pixbuf.Width, pixbuf.Height, false, false);
                    unsafe
                    {
                        byte* srcData = (byte*)pixbuf.Pixels;
                        fixed (byte* dstData = &(image.Data[0]))
                        {
                            for (int v = 0; v < image.Height; v++)
                            {
                                byte* srcRow = &srcData[v * pixbuf.Rowstride];
                                byte* dstRow = &dstData[v * image.RowBytes];
                                for (int u = 0; u < image.Width; u++)
                                {
                                    dstRow[3 * u] = srcRow[3 * u + 2];
                                    dstRow[3 * u + 1] = srcRow[3 * u + 1];
                                    dstRow[3 * u + 2] = srcRow[3 * u];
                                }
                            }
                        }
                    }
                    return image;
                }
            }
            else return null;
        }
    }
}