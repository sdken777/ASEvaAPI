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

            var contextMenu = this.SetContextMenuAsNew();
            initContextMenu(contextMenu);

            var layout = this.SetContentAsTableLayout();
            var rowFirst = layout.AddRow(true);
            var rowSecond = layout.AddRow(true);

            var groupBasic = rowFirst.AddGroupBox(t["basic-group-title"], true, true);
            initBasicGroupBox(groupBasic);

            var groupWeb = rowFirst.AddGroupBox(t["util-group-title"], true, true);
            initUtilGroupBox(groupWeb);

            var groupDraw = rowSecond.AddGroupBox(t["draw-group-title"], true, true);
            initDrawGroupBox(groupDraw, onscreenRendering);

            var groupPlot = rowSecond.AddGroupBox(t["plot-group-title"], true, true, 200, 100);
            initPlotGroupBox(groupPlot);

            loopTimer.Interval = 0.015;
            loopTimer.Elapsed += delegate
            {
                loopBasicPageA();
                loopDrawDefault2D();
                loopDrawSkia2D();
                loopDrawGL();
            };
            loopTimer.Start();

            KeyDown += (o, e) =>
            {
                if (e.Control && e.Key != Keys.Control && e.Key != Keys.LeftControl && e.Key != Keys.RightControl && e.Key != Keys.Space && e.Key != Keys.None)
                {
                    MessageBox.Show("Ctrl+" + e.Key.ToString());
                }
            };

            Closing += (o, e) =>
            {
                if (App.FatalException || MessageBox.Show(t["exit-confirm"], MessageBoxButtons.YesNo, MessageBoxType.Question) == DialogResult.Yes)
                {
                    loopTimer.Stop();
                }
                else e.Cancel = true;
            };
        }

        private TextResource t;
        private DateTime startTime = DateTime.Now;
        private UITimer loopTimer = new UITimer();
    }
}