using System;
using System.Collections.Generic;
using System.Drawing;
using ASEva;
using ASEva.Samples;
using ASEva.UIEto;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=3.0.0) Extension for icon object generation
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.0.0) 方便生成图标的扩展
    /// </summary>
    public static class WinformIconExtensions
    {
        public static Icon? ToWinformIcon(this byte[] imageData)
        {
            var etoIcon = imageData.ToIcon();
            if (etoIcon == null) return null;
            else return etoIcon.ControlObject as Icon;
        }

        public static Icon? ToWinformIcon(this Bitmap bitmap)
        {
            var image = ImageConverter.ConvertFromBitmap(bitmap);
            if (image == null) return null;
            else return ToWinformIcon(image);
        }

        public static Icon? ToWinformIcon(this CommonImage image)
        {
            var etoIcon = image.ToIcon();
            return etoIcon.ControlObject as Icon;
        }
    }
}