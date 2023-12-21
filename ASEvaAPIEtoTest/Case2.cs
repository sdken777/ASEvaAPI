using System;
using Eto.Forms;
using ASEva.UIEto;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    class Case2 : Form // 窗口高度缩小后，TargetPanel的高度超出范围 (使用eto 2.6.1后解决)
    {
        public Case2()
        {
            this.Title = "";
            this.SetClientSize(1000, 900);
            this.SetMinimumClientSize(500, 500);

            var overlay = new OverlayLayout{ BackgroundColor = Colors.DodgerBlue };
            Content = overlay;

            overlay.AddControl(new TargetPanel{ BackgroundColor = Colors.Tomato }, 8, 8, 8, 8);
        }
    }

    class TargetPanel : Panel // Eto控件嵌入至Gtk应用程序中的Overlay->ScrolledWindow->Layout时，容器高度缩小后Scrollable的高度突然变小至100像素左右
    {
        public TargetPanel()
        {
            var scrollable = this.SetContentAsColumnLayout().AddControl(new Scrollable(), true) as Scrollable;
            var mainLayout = scrollable.SetContentAsColumnLayout();

            for (int i = 0; i < 5; i++)
            {
                var row = mainLayout.AddRowLayout(false, 0);
                for (int j = 0; j < 8; j++)
                {
                    SubControl sceneControl = new SubControl{ BackgroundColor = Colors.LightGrey };
                    row.AddControl(sceneControl, false, 140, 150);
                }
            }
        }

        class SubControl : Panel
        {
            public SubControl()
            {
                var mainLayout = this.SetContentAsColumnLayout(8, 0);
                mainLayout.AddRowLayout(true, 8, VerticalAlignment.Stretch).AddControl(new Drawable{ BackgroundColor = Colors.Green }, true );
                mainLayout.AddRowLayout().AddLabel("TEST", TextAlignment.Center, true);
            }
        }
    }
}