using System;
using System.Threading;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;
using ASEva;

namespace ASEvaAPIEtoTest
{
    class Startup : DialogPanel
    {
        public Startup()
        {
            Icon = Bitmap.FromResource("icon.png").ToIcon();
            SetFixMode(400, 80, false);

            var layout = this.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);
            var optionsLayout = layout.AddTableLayout(true, 8, 0);
            
            var button = layout.AddButtonPanel("OK", false, 80);
            button.DefaultBackgroundColor = Colors.LightGrey;
            button.Click += delegate { this.Close(); };

            var row = optionsLayout.AddRow(true);
            row.AddLabel("Language: ");
            radiosLanguage = row.AddRadioButtonList(new string[] { "English", "中文" });

            row = optionsLayout.AddRow(true);
            row.AddLabel("Rendering: ");
            radiosRendering = row.AddRadioButtonList(new string[] { "Offscreen", "Onscreen" });

            var menu = this.SetContextMenuAsNew();
            menu.AddButtonItem("Main thread exception").Click += delegate
            {
                String[] arr = [];
                Console.WriteLine(arr[0]);
            };
            menu.AddButtonItem("Sub thread exception").Click += delegate
            {
                var thread = new Thread(() =>
                {
                    String[] arr = [];
                    Console.WriteLine(arr[0]);
                });
                thread.Start();
            };
        }

        public override void OnClosing()
        {
            IntResult = (int)(radiosLanguage.SelectedIndex == 1 ? Language.Chinese : Language.English);
            BoolResult = radiosRendering.SelectedIndex == 1;
        }

        private RadioButtonList radiosLanguage, radiosRendering;
    }
}