using System;
using System.Windows.Media.Imaging;
using ASEva.Samples;

namespace ASEva.UIEto
{
#pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:wpf=2.0.4) Extensions for common image object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:wpf=2.0.4) 方便使用通用图像数据的扩展
    /// </summary>
    public static class CommonImageWpfExtensions
    {
        /// \~English
        /// <summary>
        /// Convert to BitmapSource object
        /// </summary>
        /// <param name="image">Common image object</param>
        /// <returns>BitmapSource object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为BitmapSource对象
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <returns>BitmapSource对象</returns>
        public static BitmapSource ToBitmapSource(this CommonImage image)
        {
            return ImageConverter.ConvertToBitmap(image) as BitmapSource;
        }

        /// \~English
        /// <summary>
        /// Convert to common image object
        /// </summary>
        /// <param name="bitmap">BitmapSource object</param>
        /// <returns>Common image object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为通用图像数据
        /// </summary>
        /// <param name="bitmap">BitmapSource对象</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage ToCommonImage(this BitmapSource bitmap)
        {
            return ImageConverter.ConvertFromBitmap(bitmap);
        }
    }
}