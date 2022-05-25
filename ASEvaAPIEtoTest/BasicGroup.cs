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

            var tabPage1 = tabControl.AddPage(t.Format("basic-tabpage-title", "A"));
            initBasicTagPageA(tabPage1);

            var tabPage2 = tabControl.AddPage(t.Format("basic-tabpage-title", "B"));
            initBasicTagPageB(tabPage2);

            var tabPage3 = tabControl.AddPage(t.Format("basic-tabpage-title", "C"));
            initBasicTagPageC(tabPage3);
        }
    }
}