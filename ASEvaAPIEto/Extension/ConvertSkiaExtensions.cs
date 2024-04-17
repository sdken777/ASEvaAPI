using System;
using ASEva;
using SkiaSharp;

namespace ASEva.UIEto
{
    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Extensions for conversion between structures
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 方便进行结构体转换操作的扩展
    /// </summary>
    public static class ConvertSkiaExtensions
    {
        public static IntSize ToCommon(this SKSizeI size)
        {
            return new IntSize(size.Width, size.Height);
        }

        public static SKSizeI ToSkia(this IntSize size)
        {
            return new SKSizeI(size.Width, size.Height);
        }

        public static FloatSize ToCommon(this SKSize size)
        {
            return new FloatSize(size.Width, size.Height);
        }

        public static SKSize ToSkia(this FloatSize size)
        {
            return new SKSize(size.Width, size.Height);
        }

        public static IntPoint ToCommon(this SKPointI pt)
        {
            return new IntPoint(pt.X, pt.Y);
        }

        public static SKPointI ToSkia(this IntPoint pt)
        {
            return new SKPointI(pt.X, pt.Y);
        }

        public static FloatPoint ToCommon(this SKPoint pt)
        {
            return new FloatPoint(pt.X, pt.Y);
        }

        public static SKPoint ToSkia(this FloatPoint pt)
        {
            return new SKPoint(pt.X, pt.Y);
        }

        public static IntRect ToCommon(this SKRectI rect)
        {
            return new IntRect(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static SKRectI ToSkia(this IntRect rect)
        {
            return new SKRectI(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static FloatRect ToCommon(this SKRect rect)
        {
            return new FloatRect(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static SKRect ToSkia(this FloatRect rect)
        {
            return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static ColorRGBA ToCommon(this SKColor color)
        {
            return new ColorRGBA(color.Red, color.Green, color.Blue, color.Alpha);
        }

        public static SKColor ToSkia(this ColorRGBA color)
        {
            return new SKColor(color.R, color.G, color.B, color.A);
        }
    }
}