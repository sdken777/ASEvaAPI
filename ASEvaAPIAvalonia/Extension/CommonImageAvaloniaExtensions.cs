using System;
using ASEva.Samples;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace ASEva.UIAvalonia
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.8) Extensions for common image object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.8) 方便使用通用图像数据的扩展
    /// </summary>
    public static class CommonImageAvaloniaExtensions
    {
        /// \~English
        /// <summary>
        /// Convert to Avalonia bitmap object
        /// </summary>
        /// <param name="image">Common image object</param>
        /// <returns>Avalonia bitmap object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为Avalonia位图对象
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <returns>Avalonia位图对象</returns>
        public static Bitmap ToAvaloniaBitmap(this CommonImage image)
        {
            var pixelFormat = image.BgrInverted ? PixelFormat.Rgba8888 : PixelFormat.Bgra8888;

            AlphaFormat alphaFormat = AlphaFormat.Unpremul;
            var data = image.Data;
            var rowBytes = image.RowBytes;
            if (!image.WithAlpha)
            {
                data = new byte[image.Width * image.Height * 4];
                rowBytes = image.Width * 4;
                alphaFormat = AlphaFormat.Opaque;

                unsafe
                {
                    var srcRowBytes = image.RowBytes;
                    var width = image.Width;
                    var height = image.Height;
                    fixed (byte* srcData = &image.Data[0], dstData = &data[0])
                    {
                        for (int v = 0; v < height; v++)
                        {
                            byte* srcRow = srcData + v * srcRowBytes;
                            uint* dstRow = (uint*)(dstData + v * rowBytes);
                            for (int u = 0; u < width; u++)
                            {
                                uint b1 = *srcRow++;
                                uint b2 = *srcRow++;
                                uint b3 = *srcRow++;
                                *dstRow++ = b1 | (b2 << 8) | (b3 << 16) | 0xff000000;
                            }
                        }
                    }
                }
            }

            unsafe
            {
                fixed (byte* dataPtr = &data[0])
                {
                    return new Bitmap(pixelFormat, alphaFormat, (nint)dataPtr, new PixelSize(image.Width, image.Height), new Vector(96, 96), rowBytes);
                }
            }
        }

        /// \~English
        /// <summary>
        /// Convert to common image object
        /// </summary>
        /// <param name="bitmap">Avalonia bitmap object</param>
        /// <returns>Common image object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为通用图像数据
        /// </summary>
        /// <param name="bitmap">Avalonia位图对象</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage ToCommonImage(this Bitmap bitmap)
        {
            if (bitmap.Format != PixelFormat.Bgra8888 && bitmap.Format != PixelFormat.Rgba8888) return null;
            if (bitmap.AlphaFormat != AlphaFormat.Unpremul && bitmap.AlphaFormat != AlphaFormat.Opaque) return null;

            var image = CommonImage.Create(bitmap.PixelSize.Width, bitmap.PixelSize.Height, bitmap.AlphaFormat == AlphaFormat.Unpremul, bitmap.Format == PixelFormat.Rgba8888);
            if (image.WithAlpha)
            {
                unsafe
                {
                    fixed (byte* dataPtr = &image.Data[0])
                    {
                        bitmap.CopyPixels(new PixelRect(0, 0, image.Width, image.Height), (nint)dataPtr, image.RowBytes * image.Height, image.RowBytes);
                    }
                }
            }
            else
            {
                var width = image.Width;
                var height = image.Height;
                var srcRowBytes = width * 4;
                var dstRowBytes = image.RowBytes;
                var buffer = new byte[srcRowBytes * height];
                unsafe
                {
                    fixed (byte* srcPtr = &buffer[0], dstPtr = &image.Data[0])
                    {
                        bitmap.CopyPixels(new PixelRect(0, 0, width, height), (nint)srcPtr, buffer.Length, srcRowBytes);
                        for (int v = 0; v < height; v++)
                        {
                            uint* srcRow = (uint*)(srcPtr + v * srcRowBytes);
                            byte* dstRow = dstPtr + v * dstRowBytes;
                            for (int u = 0; u < width; u++)
                            {
                                uint cell = *srcRow++;
                                *dstRow++ = (byte)(cell & 0x000000ff);
                                *dstRow++ = (byte)((cell & 0x0000ff00) >> 8);
                                *dstRow++ = (byte)((cell & 0x00ff0000) >> 16);
                            }
                        }
                    }
                }
            }
            return image;
        }
    }
}