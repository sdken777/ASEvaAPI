using System;
using Eto.Drawing;
using Eto.Forms;
using ASEva.UIEto;

namespace ASEvaAPIEtoTest
{
    class InfoDialog : DialogPanel
    {
        public InfoDialog(String title, String text)
        {
            Icon = Icon.FromResource("icon.png");
            Title = title;
            SetResizableMode(500, 700, 500, 700);

            this.SetContentAsColumnLayout().AddControl(new TextArea{ Text = text, ReadOnly = true }, true);
        }
    }
}