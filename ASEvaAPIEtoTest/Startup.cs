using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    class Startup : DialogPanel
    {
        public Startup()
        {
            Icon = Icon.FromResource("icon.png");
            SetFixMode(350, 50, false);

            var layout = this.SetContentAsRowLayout();
            layout.AddLabel("Language: ");
            radioButtonList = layout.AddRadioButtonList(new string[] { "English", "中文" });
            layout.AddSpace();
            layout.AddButton("OK").Click += delegate { this.Close(); };
        }

        public override void OnClosing()
        {
            switch (radioButtonList.SelectedIndex)
            {
                case 0:
                    StringResult = "en";
                    break;
                case 1:
                    StringResult = "ch";
                    break;
            }
        }

        private RadioButtonList radioButtonList;
    }
}