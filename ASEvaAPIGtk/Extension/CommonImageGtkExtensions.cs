using System;
using ASEva.Samples;
using Cairo;
using Gdk;

namespace ASEva.UIGtk
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:gtk=3.2.1) Extensions for common image object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.2.1) 方便使用通用图像数据的扩展
    /// </summary>
    public static class CommonImageGtkExtensions
    {
        /// \~English
        /// <summary>
        /// Convert to Gdk.Pixbuf object
        /// </summary>
        /// <param name="image">Common image object</param>
        /// <returns>Gdk.Pixbuf object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为Gdk.Pixbuf对象
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <returns>Gdk.Pixbuf对象</returns>
        public static Pixbuf ToPixbuf(this CommonImage image)
        {
            var mid = ImageConverter.ConvertToVideoFrameSampleImage(image) as VideoFrameSampleImage;
            if (mid == null) throw new Exception("Invalid VideoFrameSampleImage.");
            else return mid.ToPixbuf();
        }

        /// \~English
        /// <summary>
        /// Convert to Cairo.ImageSurface object
        /// </summary>
        /// <param name="image">Common image object</param>
        /// <returns>Cairo.ImageSurface object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为Cairo.ImageSurface对象
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <returns>Cairo.ImageSurface对象</returns>
        public static ImageSurface ToImageSurface(this CommonImage image)
        {
            var mid = ImageConverter.ConvertToVideoFrameSampleImage(image) as VideoFrameSampleImage;
            if (mid == null) throw new Exception("Invalid VideoFrameSampleImage.");
            else return mid.ToImageSurface();
        }

        /// \~English
        /// <summary>
        /// Convert to common image object
        /// </summary>
        /// <param name="pixbuf">Gdk.Pixbuf object</param>
        /// <returns>Common image object</returns>
        /// \~Chinese
        /// <summary>
        /// 转为通用图像数据
        /// </summary>
        /// <param name="pixbuf">Gdk.Pixbuf对象</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage? ToCommonImage(this Pixbuf pixbuf)
        {
            return ImageConverter.ConvertFromPlatformImage(pixbuf);
        }
    }
}