using System;
using ASEva;
using ASEva.UIEto;
using Eto.Drawing;

namespace ASEvaAPIAvaloniaTest
{
    class EtoWindow : Eto.Forms.Form
    {
        public EtoWindow()
        {
            Title = Program.Texts["eto-window"];
            Icon = Icon.FromResource("icon.png");
            this.SetClientSize(1300, 800);
            this.SetMinimumClientSize(1300, 800);

            var panel = new ASEvaAPIEtoTest.EtoTestPanel(Program.Language, false);
            Content = panel;

            panel.RequestClose += delegate { this.Close(); };
            panel.RequestFullScreen += delegate { this.MaximizeToFullScreen(); };
            Closing += delegate{ panel.StopTimer(); };
        }
    }

    class EtoDialog : DialogPanel
    {
        public EtoDialog()
        {
            Title = Program.Texts["eto-dialog"];
            Icon = Icon.FromResource("icon.png");
            this.SetResizableMode(1300, 800, 1300, 800);

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