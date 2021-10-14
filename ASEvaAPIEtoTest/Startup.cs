using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    class Startup : Dialog
    {
        public Startup()
        {
            WindowStyle = WindowStyle.None;
            Icon = Icon.FromResource("icon.png");
            Size = MinimumSize = this.Sizer(350, 50);

            var layout = this.SetContentAsRowLayout();
            layout.AddLabel("Language: ");
            radioButtonList = layout.AddRadioButtonList(new string[] { "English", "中文" });
            layout.AddSpace();
            layout.AddButton("OK").Click += delegate { this.Close(); };
        }

        public String LanguageCode
        {
            get
            {
                switch (radioButtonList.SelectedIndex)
                {
                    case 0:
                        return "en";
                    case 1:
                        return "ch";
                    default:
                        return null;
                }
            }
        }

        private RadioButtonList radioButtonList;
    }
}