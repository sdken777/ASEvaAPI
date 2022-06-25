using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.4) 方便操作图形对象的扩展
    /// </summary>
    public static class GraphicsExtensions
    {
        /// <summary>
        /// 设置绘图尺度，令所有相关绘制函数的输入尺寸都为logical尺寸
        /// </summary>
        /// <param name="g">图形对象</param>
        public static void SetScaleForLogical(this Graphics g)
        {
            g.ScaleTransform(Pixel.Scale);
        }

        /// <summary>
        /// 调用SetScaleForLogical后，绘制文字需要使用此函数返回的字体
        /// </summary>
        /// <param name="g">图形对象</param>
        /// <param name="f">原字体</param>
        /// <returns>可用于绘制的字体</returns>
        public static Font ScaledFont(this Graphics g, Font f)
        {
            if (Pixel.Scale == 1) return f;
            else return new Font(f.Family, f.Size / Pixel.Scale, f.FontStyle, f.FontDecoration);
        }

        /// <summary>
        /// (api:eto=2.0.6) 调用SetScaleForLogical后，以默认字体绘制需要使用此函数返回的字体
        /// </summary>
        /// <param name="g">图形对象</param>
        /// <param name="sizeRatio">相对字体默认大小的比例，默认为1</param>
        /// <returns>可用于绘制的默认字体</returns>
        public static Font ScaledDefaultFont(this Graphics g, float sizeRatio = 1)
        {
            var f = App.DefaultFont(sizeRatio);
            if (Pixel.Scale == 1) return f;
            else return new Font(f.Family, f.Size / Pixel.Scale, f.FontStyle, f.FontDecoration);
        }
    }
}