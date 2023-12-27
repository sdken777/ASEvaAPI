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
            Icon = Icon.FromResource("icon.png");
            SetFixMode(image.Width + 8, image.Height + 8, true);
            this.SetContentAsOverlayLayout().AddControl(new ImageView{ Image = image.ToEtoBitmap() }, null, null, null, null);
        }
    }
}