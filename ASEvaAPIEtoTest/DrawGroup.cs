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

            var layout2D = layoutRow.AddColumnLayout();
            initDraw2D(layout2D);

            var layoutGL = layoutRow.AddColumnLayout(true);
            initDrawGL(layoutGL);
        }
    }
}