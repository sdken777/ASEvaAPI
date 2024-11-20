using System;
using System.Drawing;
using ASEva.Samples;

namespace ASEva.UICoreWF
{
#pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:corewf=3.1.1) Extensions for common image object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.1.1) 方便使用通用图像数据的扩展
    /// </summary>
    public static class CommonImageCoreWFExtensions
    {
        /// \~English
        /// <summary>
        /// Convert to Winform bitmap object
        /// </summary>
        /// <param name="image">Common image object</param>
        /// <returns>Winform bitmap object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为Winform位图对象
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <returns>Winform位图对象</returns>
        public static Bitmap ToWinformBitmap(this CommonImage image)
        {
            return (ImageConverter.ConvertToBitmap(image) as Bitmap)!;
        }

        /// \~English
        /// <summary>
        /// Convert to common image object
        /// </summary>
        /// <param name="bitmap">Winform bitmap object</param>
        /// <returns>Common image object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为通用图像数据
        /// </summary>
        /// <param name="bitmap">Winform位图对象</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage? ToCommonImage(this Bitmap bitmap)
        {
            return ImageConverter.ConvertFromBitmap(bitmap);
        }
    }
}