using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.4.0) 像素单位转换
    /// </summary>
    public class Pixel
    {
        /// <summary>
        /// 逻辑像素值转原始像素值
        /// </summary>
        public static int FromLogicalValue(int logicalValue)
        {
            return (int)(logicalValue * Scale);
        }

        /// <summary>
        /// 原始像素值转逻辑像素值
        /// </summary>
        public static int ToLogicalValue(int rawValue)
        {
            return (int)(rawValue / Scale);
        }

        /// <summary>
        /// 逻辑点坐标转原始点坐标
        /// </summary>
        public static Point FromLogicalPoint(Point logicalPoint)
        {
            return new Point((int)(logicalPoint.X * Scale), (int)(logicalPoint.Y * Scale));
        }

        /// <summary>
        /// 原始点坐标转逻辑点坐标
        /// </summary>
        public static Point ToLogicalPoint(Point rawPoint)
        {
            return new Point((int)(rawPoint.X / Scale), (int)(rawPoint.Y / Scale));
        }

        /// <summary>
        /// 逻辑尺寸转原始尺寸
        /// </summary>
        public static Size FromLogicalSize(Size logicalSize)
        {
            return new Size((int)(logicalSize.Width * Scale), (int)(logicalSize.Height * Scale));
        }

        /// <summary>
        /// 原始尺寸转逻辑尺寸
        /// </summary>
        public static Size ToLogicalSize(Size rawSize)
        {
            return new Size((int)(rawSize.Width / Scale), (int)(rawSize.Height / Scale));
        }

        /// <summary>
        /// 像素单位比例，用于优化在非默认DPI下的显示
        /// </summary>
        public static float Scale
        {
            get
            {
                if (scale == 0)
                {
                    if (CalculateByScreenRealScale)
                    {
                        var screen = Screen.PrimaryScreen;
                        scale = screen.RealScale;
                        if (scale <= 0) scale = 1;
                    }
                    else scale = 1;
                }
                return scale;
            }
        }

        private static float scale = 0;

        public static bool CalculateByScreenRealScale { private get; set; }
    }
}