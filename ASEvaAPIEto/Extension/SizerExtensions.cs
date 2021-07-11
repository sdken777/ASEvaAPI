using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.Eto
{
    /// <summary>
    /// (api:eto=1.0.0) 提供非默认DPI尺寸转换的扩展
    /// </summary>
    public static class SizerExtensions
    {
        /// <summary>
        /// 转换像素值（单值）
        /// </summary>
        /// <param name="panel">自定义控件</param>
        /// <param name="size">默认DPI下的像素值</param>
        /// <returns>任意DPI下的像素值</returns>
        public static int Sizer(this Panel panel, int size)
        {
            var scale = PixelScale;
            if (scale <= 0) return size;
            else return (int)(size * scale);
        }

        /// <summary>
        /// 转换像素值（二维）
        /// </summary>
        /// <param name="panel">自定义控件</param>
        /// <param name="width">默认DPI下的宽度像素值</param>
        /// <param name="height">默认DPI下的高度像素值</param>
        /// <returns>任意DPI下的像素尺寸</returns>
        public static Size Sizer(this Panel panel, int width, int height)
        {
            var scale = PixelScale;
            if (scale <= 0) return new Size(width, height);
            else return new Size((int)(width * scale), (int)(height * scale));
        }

        /// <summary>
        /// 转换像素值（单值）
        /// </summary>
        /// <param name="dialog">对话框</param>
        /// <param name="size">默认DPI下的像素值</param>
        /// <returns>任意DPI下的像素值</returns>
        public static int Sizer(this Dialog dialog, int size)
        {
            var scale = PixelScale;
            if (scale <= 0) return size;
            else return (int)(size * scale);
        }

        /// <summary>
        /// 转换像素值（二维）
        /// </summary>
        /// <param name="dialog">对话框</param>
        /// <param name="width">默认DPI下的宽度像素值</param>
        /// <param name="height">默认DPI下的高度像素值</param>
        /// <returns>任意DPI下的像素尺寸</returns>
        public static Size Sizer(this Dialog dialog, int width, int height)
        {
            var scale = PixelScale;
            if (scale <= 0) return new Size(width, height);
            else return new Size((int)(width * scale), (int)(height * scale));
        }

        /// <summary>
        /// 像素单位比例，用于优化在非默认DPI下的显示（一般情况下无需设置）
        /// </summary>
        public static float PixelScale { get; set; }
    }
}
