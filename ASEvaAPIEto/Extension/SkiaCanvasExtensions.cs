using System;
using SkiaSharp;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.7.0) 方便操作Skia画布的扩展
    /// </summary>
    public static class SkiaCanvasExtensions
    {
        /// <summary>
        /// 获取默认字体
        /// </summary>
        /// <param name="canvas">Skia画布</param>
        /// <param name="sizeScale">相对于默认尺寸的比例</param>
        public static SKFont GetDefaultFont(this SKCanvas canvas, float sizeScale = 1.0f)
        {
            if (sizeScale <= 0) sizeScale = 1;
            if (String.IsNullOrEmpty(DefaultFontName)) return new SKFont();
            else
            {
                if (DefaultFontSize == 0) return new SKFont(SKTypeface.FromFamilyName(DefaultFontName));
                else return new SKFont(SKTypeface.FromFamilyName(DefaultFontName), DefaultFontSize * sizeScale);
            }
        }

		public static String DefaultFontName { private get; set; }
		public static float DefaultFontSize { private get; set; }
    }
}