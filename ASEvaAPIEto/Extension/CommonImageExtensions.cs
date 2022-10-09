using System;
using Eto.Drawing;
using SkiaSharp;
using ASEva.Samples;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.7.0) 方便使用通用图像数据的扩展
    /// </summary>
    public static class CommonImageExtensions
    {
        /// <summary>
        /// 转为Eto位图对象
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <returns>Eto位图对象</returns>
        public static Bitmap ToEtoBitmap(this CommonImage image)
        {
            return ImageConverter.ConvertToBitmap(image) as Bitmap;
        }

        /// <summary>
        /// 转为Skia图像对象
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <returns>Skia图像对象</returns>
        public static SKImage ToSKImage(this CommonImage image)
        {
            int width = image.Width;
            int height = image.Height;
            var buffer = new byte[width * height * 4];
            unsafe
            {
                fixed (byte *srcData = &(image.Data[0]), dstData = &(buffer[0]))
                {
                    if (image.WithAlpha)
                    {
                        for (int v = 0; v < height; v++)
                        {
                            uint* srcRow = (uint*)&srcData[v * image.RowBytes];
                            uint* dstRow = (uint*)&dstData[v * width * 4];
                            for (int u = 0; u < width; u++)
                            {
                                uint cell = srcRow[u];
                                dstRow[u] = (cell & 0xff00ff00) | ((cell & 0x000000ff) << 16) | ((cell & 0x00ff0000) >> 16);
                            }
                        }
                    }
                    else
                    {
                        for (int v = 0; v < height; v++)
                        {
                            byte* srcRow = &srcData[v * image.RowBytes];
                            uint* dstRow = (uint*)&dstData[v * width * 4];
                            for (int u = 0; u < width; u++)
                            {
                                dstRow[u] = 0xff000000 | (uint)srcRow[3 * u + 2] | ((uint)srcRow[3 * u + 1] << 8) | ((uint)srcRow[3 * u] << 16);
                            }
                        }
                    }
                }
            }

            var info = new SKImageInfo(image.Width, image.Height, SKColorType.Rgba8888);
            return SKImage.FromPixelCopy(info, buffer, width * 4);
        }
    }
}