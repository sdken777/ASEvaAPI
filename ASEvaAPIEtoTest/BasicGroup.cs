using System;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initBasicGroupBox(GroupBox groupBox)
        {
            var tabControl = groupBox.SetContentAsColumnLayout().AddControl(new TabControl(), true) as TabControl;

            var tabPageA = tabControl.AddPage(t.Format("basic-tabpage-title", "A"));
            initBasicTagPageA(tabPageA);

            var tabPageB = tabControl.AddPage(t.Format("basic-tabpage-title", "B"));
            initBasicTagPageB(tabPageB);

            var tabPageC = tabControl.AddPage(t.Format("basic-tabpage-title", "C"));
            initBasicTagPageC(tabPageC);

            var tabPageD = tabControl.AddPage(t.Format("basic-tabpage-title", "D"));
            initBasicTagPageD(tabPageD);
        }
    }
}