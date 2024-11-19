using System;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class EtoTestPanel
    {
        private void initBasicGroupBox(GroupBox groupBox)
        {
            var tabControl = (groupBox.SetContentAsControl(new TabControl()) as TabControl)!;

            var tabPageA = tabControl.AddPage(t.Format("basic-tabpage-title", "A"));
            initBasicTabPageA(tabPageA);

            var tabPageB = tabControl.AddPage(t.Format("basic-tabpage-title", "B"));
            initBasicTabPageB(tabPageB);

            var tabPageC = tabControl.AddPage(t.Format("basic-tabpage-title", "C"));
            initBasicTabPageC(tabPageC);

            var tabPageD = tabControl.AddPage(t.Format("basic-tabpage-title", "D"));
            initBasicTabPageD(tabPageD);

            var tabPageE = tabControl.AddPage(t.Format("basic-tabpage-title", "E"));
            initBasicTabPageE(tabPageE);
        }
    }
}