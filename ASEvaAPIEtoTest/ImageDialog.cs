using System;
using Eto.Drawing;
using Eto.Forms;
using ASEva.Samples;
using ASEva.UIEto;

namespace ASEvaAPIEtoTest
{
    class ImageDialog : DialogPanel
    {
        public ImageDialog(CommonImage image)
        {
            Icon = Bitmap.FromResource("icon.png").ToIcon();
            SetFixMode(image.Width + 8, image.Height + 8, true);

            var imageView = new ImageView{ Image = image.ToEtoBitmap() };
            imageView.SetLogicalSize(image.Width, image.Height);
            this.SetContentAsOverlayLayout().AddControl(imageView, null, null, null, null);
        }
    }
}