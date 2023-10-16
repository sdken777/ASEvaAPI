using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=2.0.0) Extensions for conversion between logical size and pixel size
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.0.0) 提供逻辑尺寸与实际像素值转换的扩展
    /// </summary>
    public static class SizerExtensions
    {
        /// \~English
        /// <summary>
        /// Convert to pixel size (value)
        /// </summary>
        /// <param name="control">Control object</param>
        /// <param name="size">Logical size</param>
        /// <returns>Pixel size</returns>
        /// \~Chinese
        /// <summary>
        /// 转换为实际像素值（单值）
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="size">逻辑尺寸</param>
        /// <returns>实际像素值</returns>
        public static int Sizer(this Control control, int size)
        {
            int outSize = size;
            var scale = Pixel.Scale;
            if (scale != 1) outSize = (int)(size * scale);
            return Math.Max(1, outSize);
        }

        /// \~English
        /// <summary>
        /// Convert to pixel size (2D)
        /// </summary>
        /// <param name="control">Control object</param>
        /// <param name="width">Logical width</param>
        /// <param name="height">Logical height</param>
        /// <returns>Pixel size</returns>
        /// \~Chinese
        /// <summary>
        /// 转换为实际像素值（二维）
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="width">逻辑宽度</param>
        /// <param name="height">逻辑高度</param>
        /// <returns>实际像素值</returns>
        public static Size Sizer(this Control control, int width, int height)
        {
            int outWidth = width;
            int outHeight = height;
            var scale = Pixel.Scale;
            if (scale != 1)
            {
                outWidth = (int)(width * scale);
                outHeight = (int)(height * scale);
            }
            return new Size(Math.Max(1, outWidth), Math.Max(1, outHeight));
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.3.5) Get control's logical width
        /// </summary>
        /// <param name="control">Control object</param>
        /// <returns>Logical width</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.3.5) 获取控件逻辑宽度
        /// </summary>
        /// <param name="control">控件</param>
        /// <returns>逻辑宽度</returns>
        public static int GetLogicalWidth(this Control control)
        {
            var scale = Pixel.Scale;
            if (scale != 1) return (int)(control.Width / scale);
            else return control.Width;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.3.5) Get control's logical height
        /// </summary>
        /// <param name="control">Control object</param>
        /// <returns>Logical height</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.3.5) 获取控件逻辑高度
        /// </summary>
        /// <param name="control">控件</param>
        /// <returns>逻辑高度</returns>
        public static int GetLogicalHeight(this Control control)
        {
            var scale = Pixel.Scale;
            if (scale != 1) return (int)(control.Height / scale);
            else return control.Height;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.5.1) Get control's logical size
        /// </summary>
        /// <param name="control">Control object</param>
        /// <returns>Logical size</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.5.1) 获取控件逻辑尺寸
        /// </summary>
        /// <param name="control">控件</param>
        /// <returns>逻辑尺寸</returns>
        public static Size GetLogicalSize(this Control control)
        {
            var scale = Pixel.Scale;
            if (scale != 1) return new Size((int)(control.Width / scale), (int)(control.Height / scale));
            else return control.Size;
        }

        /// \~English
        /// <summary>
        /// Set control's logical width
        /// </summary>
        /// <param name="control">Control object</param>
        /// <param name="width">Logical width</param>
        /// \~Chinese
        /// <summary>
        /// 设置控件逻辑宽度
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="width">逻辑宽度</param>
        public static void SetLogicalWidth(this Control control, int width)
        {
            int setWidth = width;
            var scale = Pixel.Scale;
            if (scale != 1) setWidth = (int)(width * scale);
            var targetWidth = Math.Max(1, setWidth);
            if (control.Width != targetWidth) control.Width = targetWidth;
        }

        /// \~English
        /// <summary>
        /// Set control's logical height
        /// </summary>
        /// <param name="control">Control object</param>
        /// <param name="height">Logical height</param>
        /// \~Chinese
        /// <summary>
        /// 设置控件逻辑高度
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="height">逻辑高度</param>
        public static void SetLogicalHeight(this Control control, int height)
        {
            int setHeight = height;
            var scale = Pixel.Scale;
            if (scale != 1) setHeight = (int)(height * scale);
            var targetHeight = Math.Max(1, setHeight);
            if (control.Height != targetHeight) control.Height = targetHeight;
        }

        /// \~English
        /// <summary>
        /// Set control's logical size
        /// </summary>
        /// <param name="control">Control object</param>
        /// <param name="width">Logical width</param>
        /// <param name="height">Logical height</param>
        /// \~Chinese
        /// <summary>
        /// 设置控件逻辑尺寸
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="width">逻辑宽度</param>
        /// <param name="height">逻辑高度</param>
        public static void SetLogicalSize(this Control control, int width, int height)
        {
            int setWidth = width;
            int setHeight = height;
            var scale = Pixel.Scale;
            if (scale != 1)
            {
                setWidth = (int)(width * scale);
                setHeight = (int)(height * scale);
            }
            var targetWidth = Math.Max(1, setWidth);
            var targetHeight = Math.Max(1, setHeight);
            if (control.Width != targetWidth || control.Height != targetHeight) control.Size = new Size(targetWidth, targetHeight);
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.5.1) Get mouse's logical position in the MouseEventArgs object
        /// </summary>
        /// <param name="args">MouseEventArgs object</param>
        /// <returns>Mouse's logical position</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.5.1) 获取鼠标事件中鼠标位置的逻辑坐标
        /// </summary>
        /// <param name="args">鼠标事件</param>
        /// <returns>鼠标位置的逻辑坐标</returns>
        public static PointF GetLogicalPoint(this MouseEventArgs args)
        {
            var scale = Pixel.Scale;
            if (scale != 1) return new PointF(args.Location.X / scale, args.Location.Y / scale);
            else return args.Location;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.6.2) Get mouse's logical position to the control's origin
        /// </summary>
        /// <param name="control">Control object</param>
        /// <returns>Mouse's logical position to the control's origin</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.6.2) 获取鼠标相对于当前控件的逻辑坐标
        /// </summary>
        /// <param name="control">控件</param>
        /// <returns>鼠标相对于当前控件的逻辑坐标</returns>
        public static PointF GetMouseLogicalPoint(this Control control)
        {
            var pt = control.PointFromScreen(Mouse.Position);
            var scale = Pixel.Scale;
            if (scale != 1) return new PointF(pt.X / scale, pt.Y / scale);
            else return pt;
        }

        /// \~English
        /// <summary>
        /// Ratio of logical unit to pixel unit
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 像素单位比例
        /// </summary>
        public static float PixelScale
        {
            get { return Pixel.Scale; } 
            set {}
        }
    }
}
