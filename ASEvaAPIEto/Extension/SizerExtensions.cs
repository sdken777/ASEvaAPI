using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.0) 提供非默认DPI尺寸转换的扩展
    /// </summary>
    public static class SizerExtensions
    {
        /// <summary>
        /// 转换像素值（单值）
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="size">默认DPI下的像素值</param>
        /// <returns>任意DPI下的像素值</returns>
        public static int Sizer(this Control control, int size)
        {
            initPixelScale();
            int outSize = size;
            var scale = PixelScale;
            if (scale > 0) outSize = (int)(size * scale);
            return Math.Max(1, outSize);
        }

        /// <summary>
        /// 转换像素值（二维）
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="width">默认DPI下的宽度像素值</param>
        /// <param name="height">默认DPI下的高度像素值</param>
        /// <returns>任意DPI下的像素尺寸</returns>
        public static Size Sizer(this Control control, int width, int height)
        {
            initPixelScale();
            int outWidth = width;
            int outHeight = height;
            var scale = PixelScale;
            if (scale > 0)
            {
                outWidth = (int)(width * scale);
                outHeight = (int)(height * scale);
            }
            return new Size(Math.Max(1, outWidth), Math.Max(1, outHeight));
        }

        /// <summary>
        /// 设置控件宽度
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="width">设置宽度</param>
        public static void SetLogicalWidth(this Control control, int width)
        {
            initPixelScale();
            int setWidth = width;
            var scale = PixelScale;
            if (scale > 0) setWidth = (int)(width * scale);
            control.Width = Math.Max(1, setWidth);
        }

        /// <summary>
        /// 设置控件高度
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="height">设置高度</param>
        public static void SetLogicalHeight(this Control control, int height)
        {
            initPixelScale();
            int setHeight = height;
            var scale = PixelScale;
            if (scale > 0) setHeight = (int)(height * scale);
            control.Height = Math.Max(1, setHeight);
        }

        /// <summary>
        /// 设置控件尺寸
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="width">设置宽度</param>
        /// <param name="height">设置高度</param>
        public static void SetLogicalSize(this Control control, int width, int height)
        {
            initPixelScale();
            int setWidth = width;
            int setHeight = height;
            var scale = PixelScale;
            if (scale > 0)
            {
                setWidth = (int)(width * scale);
                setHeight = (int)(height * scale);
            }
            control.Size = new Size(Math.Max(1, setWidth), Math.Max(1, setHeight));
        }

        /// <summary>
        /// 像素单位比例，用于优化在非默认DPI下的显示（一般情况下无需设置）
        /// </summary>
        public static float PixelScale { get; set; }

        private static void initPixelScale()
        {
            if (PixelScale != 0) return;

            if (ASEva.APIInfo.GetRunningOS() == "windows" && App.GetRunningUI() == "corewf")
            {
                var screen = Screen.PrimaryScreen;
                PixelScale = screen.RealScale;
            }
            else PixelScale = 1;
        }
    }
}
