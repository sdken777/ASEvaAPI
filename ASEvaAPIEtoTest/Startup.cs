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
            SetFixMode(400, 80, false);

            var layout = this.SetContentAsRowLayout();
            var optionsLayout = layout.AddTableLayout(true);
            layout.AddButton("OK", false, 80).Click += delegate { this.Close(); };

            var row = optionsLayout.AddRow(true);
            row.AddLabel("Language: ");
            radiosLanguage = row.AddRadioButtonList(new string[] { "English", "中文" });

            row = optionsLayout.AddRow(true);
            row.AddLabel("Rendering: ");
            radiosRendering = row.AddRadioButtonList(new string[] { "Offscreen", "Onscreen" });
        }

        public override void OnClosing()
        {
            StringResult = radiosLanguage.SelectedIndex == 1 ? "ch" : "en";
            BoolResult = radiosRendering.SelectedIndex == 1;
        }

        private RadioButtonList radiosLanguage, radiosRendering;
    }
}