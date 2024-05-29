using System;
using ASEva;
using ASEva.UIEto;
using Eto.Drawing;

namespace ASEvaAPIAvaloniaTest
{
    class EtoWindow : Eto.Forms.Form
    {
        public EtoWindow(Language language)
        {
            Title = "Eto Window";
            Icon = Icon.FromResource("icon.png");
            this.SetClientSize(1300, 800);
            this.SetMinimumClientSize(1300, 800);

            var panel = new ASEvaAPIEtoTest.EtoTestPanel(language, false);
            Content = panel;

            panel.RequestClose += delegate { this.Close(); };
            panel.RequestFullScreen += delegate { this.MaximizeToFullScreen(); };
            Closing += delegate{ panel.StopTimer(); };
        }
    }

    class EtoDialog : DialogPanel
    {
        public EtoDialog(Language language)
        {
            Title = "Eto Dialog";
            Icon = Icon.FromResource("icon.png");
            this.SetResizableMode(1300, 800, 1300, 800);

            panel = new ASEvaAPIEtoTest.EtoTestPanel(language, false);
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