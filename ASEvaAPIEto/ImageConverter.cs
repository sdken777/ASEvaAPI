using System;
using Eto.Drawing;
using ASEva.Samples;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.2.0) 图像对象转换
    /// </summary>
    public class ImageConverter
    {
        public static object ConvertToBitmap(CommonImage image)
        {
            if (image == null) return null;

            Bitmap bitmap = null;
            if (image.WithAlpha)
            {
                bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppRgba);
                using (var bitmapData = bitmap.Lock())
                {
                    unsafe
                    {
                        var dstData = (byte*)bitmapData.Data;
                        fixed (byte* srcData = &image.Data[0])
                        {
                            switch (Mode)
                            {
                                case ConvertMode.AlphaScale:
                                    if (image.BgrInverted) convert4AlphaScaleColorInvertedToBitmap(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    else convert4AlphaScaleToBitmap(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                case ConvertMode.ColorInverted:
                                    if (image.BgrInverted) convert4Default(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    else convert4ColorInverted(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                case ConvertMode.AlphaScaleColorInverted:
                                    if (image.BgrInverted) convert4AlphaScaleToBitmap(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    else convert4AlphaScaleColorInvertedToBitmap(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                default:
                                    if (image.BgrInverted) convert4ColorInverted(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    else convert4Default(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
                using (var bitmapData = bitmap.Lock())
                {
                    unsafe
                    {
                        var dstData = (byte*)bitmapData.Data;
                        fixed (byte* srcData = &image.Data[0])
                        {
                            switch (Mode)
                            {
                                case ConvertMode.AlphaScale:
                                    if (image.BgrInverted) convert3ColorInverted(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    else convert3Default(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                case ConvertMode.ColorInverted:
                                    if (image.BgrInverted) convert3Default(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    else convert3ColorInverted(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                case ConvertMode.AlphaScaleColorInverted:
                                    if (image.BgrInverted) convert3Default(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    else convert3ColorInverted(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                default:
                                    if (image.BgrInverted) convert3ColorInverted(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    else convert3Default(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                            }
                        }
                    }
                }
            }
            return bitmap;
        }

        public static CommonImage ConvertFromBitmap(object bitmapObject)
        {
            if (bitmapObject == null) return null;
            if (!(bitmapObject is Bitmap)) return null;
            
            var bitmap = bitmapObject as Bitmap;
            using (var bitmapData = bitmap.Lock())
            {
                if (bitmapData.BytesPerPixel == 3)
                {
                    var image = CommonImage.Create(bitmap.Width, bitmap.Height, false, false);
                    unsafe
                    {
                        var srcData = (byte*)bitmapData.Data;
                        fixed (byte* dstData = &image.Data[0])
                        {
                            switch (Mode)
                            {
                                case ConvertMode.AlphaScale:
                                    convert3Default(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                case ConvertMode.ColorInverted:
                                    convert3ColorInverted(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                case ConvertMode.AlphaScaleColorInverted:
                                    convert3ColorInverted(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                default:
                                    convert3Default(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                            }
                        }
                    }
                    return image;
                }
                else if (bitmapData.BytesPerPixel == 4)
                {
                    var image = CommonImage.Create(bitmap.Width, bitmap.Height, true, false);
                    unsafe
                    {
                        var srcData = (byte*)bitmapData.Data;
                        fixed (byte* dstData = &image.Data[0])
                        {
                            switch (Mode)
                            {
                                case ConvertMode.AlphaScale:
                                    convert4AlphaScaleFromBitmap(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                case ConvertMode.ColorInverted:
                                    convert4ColorInverted(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                case ConvertMode.AlphaScaleColorInverted:
                                    convert4AlphaScaleColorInvertedFromBitmap(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                                default:
                                    convert4Default(srcData, dstData, image.RowBytes, bitmapData.ScanWidth, image.Width, image.Height);
                                    break;
                            }
                        }
                    }
                    return image;
                }
                else return null;
            }
        }

        private static unsafe void convert3Default(byte *srcData, byte *dstData, int srcStep, int dstStep, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                byte *srcRow = srcData + i * srcStep;
                byte *dstRow = dstData + i * dstStep;
                for (int j = 0; j < width; j++)
                {
                    dstRow[3 * j] = srcRow[3 * j];
                    dstRow[3 * j + 1] = srcRow[3 * j + 1];
                    dstRow[3 * j + 2] = srcRow[3 * j + 2];
                }
            }
        }

        private static unsafe void convert4Default(byte *srcData, byte *dstData, int srcStep, int dstStep, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                byte *srcRow = srcData + i * srcStep;
                byte *dstRow = dstData + i * dstStep;
                for (int j = 0; j < width; j++)
                {
                    dstRow[4 * j] = srcRow[4 * j];
                    dstRow[4 * j + 1] = srcRow[4 * j + 1];
                    dstRow[4 * j + 2] = srcRow[4 * j + 2];
                    dstRow[4 * j + 3] = srcRow[4 * j + 3];
                }
            }
        }

        private static unsafe void convert3ColorInverted(byte *srcData, byte *dstData, int srcStep, int dstStep, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                byte *srcRow = srcData + i * srcStep;
                byte *dstRow = dstData + i * dstStep;
                for (int j = 0; j < width; j++)
                {
                    dstRow[3 * j] = srcRow[3 * j + 2];
                    dstRow[3 * j + 1] = srcRow[3 * j + 1];
                    dstRow[3 * j + 2] = srcRow[3 * j];
                }
            }
        }

        private static unsafe void convert4ColorInverted(byte *srcData, byte *dstData, int srcStep, int dstStep, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                byte *srcRow = srcData + i * srcStep;
                byte *dstRow = dstData + i * dstStep;
                for (int j = 0; j < width; j++)
                {
                    dstRow[4 * j] = srcRow[4 * j + 2];
                    dstRow[4 * j + 1] = srcRow[4 * j + 1];
                    dstRow[4 * j + 2] = srcRow[4 * j];
                    dstRow[4 * j + 3] = srcRow[4 * j + 3];
                }
            }
        }

        private static unsafe void convert4AlphaScaleToBitmap(byte *srcData, byte *dstData, int srcStep, int dstStep, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                byte *srcRow = srcData + i * srcStep;
                byte *dstRow = dstData + i * dstStep;
                for (int j = 0; j < width; j++)
                {
                    int scale = srcRow[4 * j + 3];
                    dstRow[4 * j] = (byte)((scale * srcRow[4 * j]) >> 8);
                    dstRow[4 * j + 1] = (byte)((scale * srcRow[4 * j + 1]) >> 8);
                    dstRow[4 * j + 2] = (byte)((scale * srcRow[4 * j + 2]) >> 8);
                    dstRow[4 * j + 3] = (byte)scale;
                }
            }
        }

        private static unsafe void convert4AlphaScaleFromBitmap(byte *srcData, byte *dstData, int srcStep, int dstStep, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                byte *srcRow = srcData + i * srcStep;
                byte *dstRow = dstData + i * dstStep;
                for (int j = 0; j < width; j++)
                {
                    int scale = srcRow[4 * j + 3];
                    if (scale == 0)
                    {
                        dstRow[4 * j] = 0;
                        dstRow[4 * j + 1] = 0;
                        dstRow[4 * j + 2] = 0;
                    }
                    else
                    {
                        dstRow[4 * j] = (byte)Math.Min(255, ((int)srcRow[4 * j] << 8) / scale);
                        dstRow[4 * j + 1] = (byte)Math.Min(255, ((int)srcRow[4 * j + 1] << 8) / scale);
                        dstRow[4 * j + 2] = (byte)Math.Min(255, ((int)srcRow[4 * j + 2] << 8) / scale);
                    }
                    dstRow[4 * j + 3] = (byte)scale;
                }
            }
        }

        private static unsafe void convert4AlphaScaleColorInvertedToBitmap(byte *srcData, byte *dstData, int srcStep, int dstStep, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                byte *srcRow = srcData + i * srcStep;
                byte *dstRow = dstData + i * dstStep;
                for (int j = 0; j < width; j++)
                {
                    int scale = srcRow[4 * j + 3];
                    dstRow[4 * j + 2] = (byte)((scale * srcRow[4 * j]) >> 8);
                    dstRow[4 * j + 1] = (byte)((scale * srcRow[4 * j + 1]) >> 8);
                    dstRow[4 * j] = (byte)((scale * srcRow[4 * j + 2]) >> 8);
                    dstRow[4 * j + 3] = (byte)scale;
                }
            }
        }

        private static unsafe void convert4AlphaScaleColorInvertedFromBitmap(byte *srcData, byte *dstData, int srcStep, int dstStep, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                byte *srcRow = srcData + i * srcStep;
                byte *dstRow = dstData + i * dstStep;
                for (int j = 0; j < width; j++)
                {
                    int scale = srcRow[4 * j + 3];
                    if (scale == 0)
                    {
                        dstRow[4 * j] = 0;
                        dstRow[4 * j + 1] = 0;
                        dstRow[4 * j + 2] = 0;
                    }
                    else
                    {
                        dstRow[4 * j + 2] = (byte)Math.Min(255, ((int)srcRow[4 * j] << 8) / scale);
                        dstRow[4 * j + 1] = (byte)Math.Min(255, ((int)srcRow[4 * j + 1] << 8) / scale);
                        dstRow[4 * j] = (byte)Math.Min(255, ((int)srcRow[4 * j + 2] << 8) / scale);
                    }
                    dstRow[4 * j + 3] = (byte)scale;
                }
            }
        }

        public enum ConvertMode
        {
            Default = 0,
            AlphaScale = 1,
            ColorInverted = 2,
            AlphaScaleColorInverted = 3,
        }

        public static ConvertMode Mode { private get; set; }
    }
}