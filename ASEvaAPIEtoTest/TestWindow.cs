using System;
using ASEva;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow : Form
    {
        public TestWindow(Language language, bool onscreenRendering)
        {
            t = TextResource.Load("test.xml", language);

            var title = t["title"] + " (OS:" + ASEva.APIInfo.GetRunningOS() + " / UI:" + App.GetRunningUI();
            if (!String.IsNullOrEmpty(App.GetUIBackend())) title += "." + App.GetUIBackend();
            title += ")";

            Icon = Bitmap.FromResource("icon.png").ToIcon();
            Title = title;
            this.SetClientSize(1300, 750);
            this.SetMinimumClientSize(1200, 700);
            Resizable = true;

            testPanel = new EtoTestPanel(language, onscreenRendering);
            this.Content = testPanel;

            Closing += (o, e) =>
            {
                if (App.FatalException || MessageBox.Show(t["exit-confirm"], MessageBoxButtons.YesNo, MessageBoxType.Question) == DialogResult.Yes)
                {
                    testPanel.StopTimer();
                }
                else e.Cancel = true;
            };

            testPanel.RequestClose += delegate
            {
                this.Close();
            };

            testPanel.RequestFullScreen += delegate
            {
                this.MaximizeToFullScreen();
            };
        }

        private TextResource t;
        private EtoTestPanel testPanel;
    }
}