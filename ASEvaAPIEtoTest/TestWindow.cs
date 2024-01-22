using System;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow : Form
    {
        public TestWindow(String languageCode, bool onscreenRendering)
        {
            t = TextResource.Load("test.xml", languageCode);

            var title = t["title"] + " (OS:" + ASEva.APIInfo.GetRunningOS() + " / UI:" + App.GetRunningUI();
            if (!String.IsNullOrEmpty(App.GetUIBackend())) title += "." + App.GetUIBackend();
            title += ")";

            Icon = Icon.FromResource("icon.png");
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

            rowSecond.AddGroupBox(t["reserved"], true, true, 200, 100);

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
                if (e.Control) MessageBox.Show("Ctrl+" + e.Key.ToString());
            };

            Closing += (o, e) =>
            {
                if (MessageBox.Show(t["exit-confirm"], MessageBoxButtons.YesNo, MessageBoxType.Question) == DialogResult.Yes)
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