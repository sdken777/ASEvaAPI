using System;
using System.Runtime.InteropServices;
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
        /// <returns>Skia图像对象（使用完毕后建议及时调用Dispose，可减少内存占用）</returns>
        public static SKImage ToSKImage(this CommonImage image)
        {
            byte[] targetData = null;
            SKColorType colorType;
            if (image.WithAlpha)
            {
                targetData = image.Data;
                colorType = image.BgrInverted ? SKColorType.Rgba8888 : SKColorType.Bgra8888;
            }
            else
            {
                int width = image.Width;
                int height = image.Height;
                var buffer = new byte[width * height * 4];
                unsafe
                {
                    fixed (byte *srcData = &(image.Data[0]), dstData = &(buffer[0]))
                    {
                        for (int v = 0; v < height; v++)
                        {
                            byte* srcRow = &srcData[v * image.RowBytes];
                            uint* dstRow = (uint*)&dstData[v * width * 4];
                            if (image.BgrInverted)
                            {
                                for (int u = 0; u < width; u++)
                                {
                                    dstRow[u] = 0xff000000 | (uint)srcRow[3 * u] | ((uint)srcRow[3 * u + 1] << 8) | ((uint)srcRow[3 * u + 2] << 16);
                                }
                            }
                            else
                            {
                                for (int u = 0; u < width; u++)
                                {
                                    dstRow[u] = 0xff000000 | (uint)srcRow[3 * u + 2] | ((uint)srcRow[3 * u + 1] << 8) | ((uint)srcRow[3 * u] << 16);
                                }
                            }
                        }
                    }
                }
                targetData = buffer;
                colorType = SKColorType.Rgba8888;
            }

            var context = new SKPixmapContext{ Handle = GCHandle.Alloc(targetData, GCHandleType.Pinned) };
            var info = new SKImageInfo(image.Width, image.Height, colorType, SKAlphaType.Unpremul);
            var pixmap = new SKPixmap(info, context.Handle.AddrOfPinnedObject(), image.Width * 4);
            return SKImage.FromPixels(pixmap, (p, c) => (c as SKPixmapContext).Handle.Free(), context);
        }

        private class SKPixmapContext
        {
            public GCHandle Handle { get; set; }
        }
    }
}