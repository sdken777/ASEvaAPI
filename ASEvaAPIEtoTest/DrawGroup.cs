using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initDrawGroupBox(GroupBox groupBox)
        {
            var layoutRow = groupBox.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);

            var layoutImages = layoutRow.AddColumnLayout();
            initDrawImages(layoutImages);

            var tabControl = layoutRow.AddControl(new TabControl(), false, 220) as TabControl;

            var tabPageDefault2D = tabControl.AddPage(t["draw-tabpage-default"]);
            initDrawDefault2D(tabPageDefault2D);

            var tabPageSkia2D = tabControl.AddPage(t["draw-tabpage-skia"]);
            initDrawSkia2D(tabPageSkia2D);

            var layoutGL = layoutRow.AddColumnLayout(true);
            initDrawGL(layoutGL);
        }
    }
}