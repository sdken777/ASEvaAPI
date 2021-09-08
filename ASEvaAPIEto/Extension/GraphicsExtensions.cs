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
            g.ScaleTransform(SizerExtensions.PixelScale);
            
        }

        /// <summary>
        /// 调用SetScaleForLogical后，绘制文字需要使用此函数返回的字体
        /// </summary>
        /// <param name="g">图形对象</param>
        /// <param name="f">原字体</param>
        /// <returns>可用于绘制的字体</returns>
        public static Font ScaledFont(this Graphics g, Font f)
        {
            if (ASEva.APIInfo.GetRunningOS() == "windows")
            {
                if (SizerExtensions.PixelScale == 1) return f;
                else return new Font(f.Family, f.Size / SizerExtensions.PixelScale, f.FontStyle, f.FontDecoration);
            }
            else return f;
        }

        /// <summary>
        /// 调用SetScaleForLogical后，以默认字体绘制需要使用此函数返回的字体
        /// </summary>
        /// <param name="g">图形对象</param>
        /// <param name="languageCode">语言代号，en表示英文，ch表示中文，null则通过 ASEva.Agency.GetAppLanguage 获取</param>
        /// <returns>可用于绘制的默认字体</returns>
        public static Font ScaledDefaultFont(this Graphics g, String languageCode = null)
        {
            var f = App.DefaultFont(languageCode);
            if (ASEva.APIInfo.GetRunningOS() == "windows")
            {
                if (SizerExtensions.PixelScale == 1) return f;
                else return new Font(f.Family, f.Size / SizerExtensions.PixelScale, f.FontStyle, f.FontDecoration);
            }
            else return f;
        }
    }
}