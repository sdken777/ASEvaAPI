using System;
using ASEva;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    public partial class EtoTestPanel : Panel
    {
        public EtoTestPanel(Language language, bool onscreenRendering)
        {
            t = TextResource.Load("test.xml", language);

            var contextMenu = this.SetContextMenuAsNew();
            initContextMenu(contextMenu);

            var layout = this.SetContentAsTableLayout();
            var rowFirst = layout.AddRow(true);
            var rowSecond = layout.AddRow(true);

            var groupBasic = rowFirst.AddGroupBox(t["basic-group-title"], true, true);
            initBasicGroupBox(groupBasic);

            var groupWeb = rowFirst.AddGroupBox(t["web-group-title"], true, true);
            initWebGroupBox(groupWeb);

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
        }

        public void StopTimer()
        {
            loopTimer.Stop();
        }

        private TextResource t;
        private DateTime startTime = DateTime.Now;
        private UITimer loopTimer = new UITimer();
    }
}