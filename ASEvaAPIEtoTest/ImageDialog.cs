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
            SetFixMode(image.Width, image.Height, true);

            this.SetContentAsColumnLayout(0).AddControl(new ImageView{ Image = image.ToEtoBitmap() }, true);
        }
    }
}