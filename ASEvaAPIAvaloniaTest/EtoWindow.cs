using System;
using ASEva;
using ASEva.UIEto;

namespace ASEvaAPIAvaloniaTest
{
    class EtoForm : Eto.Forms.Form
    {
        public EtoForm(Language language, bool onscreenRendering)
        {
            Title = "Eto Form";
            this.SetClientSize(1300, 800);
            this.SetMinimumClientSize(1300, 800);

            var panel = new ASEvaAPIEtoTest.EtoTestPanel(language, onscreenRendering);
            Content = panel;

            panel.RequestClose += delegate { this.Close(); };
            panel.RequestFullScreen += delegate { this.MaximizeToFullScreen(); };
            Closing += delegate{ panel.StopTimer(); };
        }
    }
}