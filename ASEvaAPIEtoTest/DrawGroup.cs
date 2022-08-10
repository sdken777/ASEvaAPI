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

            var tabPageSkiaGL = tabControl.AddPage(t["draw-tabpage-skia-gl"]);
            initDrawSkia2D(tabPageSkiaGL, false);

            var tabPageSkiaCPU = tabControl.AddPage(t["draw-tabpage-skia-cpu"]);
            initDrawSkia2D(tabPageSkiaCPU, true);

            var layoutGL = layoutRow.AddColumnLayout(true);
            initDrawGL(layoutGL);
        }
    }
}