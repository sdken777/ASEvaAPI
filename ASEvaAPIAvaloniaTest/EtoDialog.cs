using System;
using ASEva;
using ASEva.UIEto;
using Eto.Drawing;

namespace ASEvaAPIAvaloniaTest
{
    class EtoDialog : DialogPanel
    {
        public EtoDialog()
        {
            Title = Program.Language == Language.Chinese ? "Eto对话框" : "Eto Dialog";
            Icon = Icon.FromResource("icon.png");
            this.SetResizableMode(1350, 800, 1350, 800);

            panel = new ASEvaAPIEtoTest.EtoTestPanel(Program.Language, false);
            Content = panel;

            panel.RequestClose += delegate { this.Close(); };
        }

        public override void OnClosing()
        {
            panel.StopTimer();
        }

        private ASEvaAPIEtoTest.EtoTestPanel panel;
    }
}