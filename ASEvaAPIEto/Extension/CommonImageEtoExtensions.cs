using System;
using System.Runtime.InteropServices;
using Eto.Drawing;
using ASEva.Samples;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Extensions for common image object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 方便使用通用图像数据的扩展
    /// </summary>
    public static class CommonImageEtoExtensions
    {
        /// \~English
        /// <summary>
        /// Convert to Eto bitmap object
        /// </summary>
        /// <param name="image">Common image object</param>
        /// <returns>Eto bitmap object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为Eto位图对象
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <returns>Eto位图对象</returns>
        public static Bitmap ToEtoBitmap(this CommonImage image)
        {
            return ImageConverter.ConvertToBitmap(image) as Bitmap;
        }
    }
}