using System;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initBasicTabPageE(TabPage tabPage)
        {
            var layoutFlowItems = tabPage.SetContentAsColumnLayout();
            initBasicTabPageEFlowItems(layoutFlowItems);
        }

        private void initBasicTabPageEFlowItems(StackLayout layout)
        {
            var layoutButtons = layout.AddRowLayout();
            var flowLayout = layout.AddControl(new FlowLayout2D(250), true) as FlowLayout2D;
            flowLayout.ControlSelected += delegate
            {
                var selectedIndex = flowLayout.GetSelectedControlIndex();
                var withMouseIndex = flowLayout.GetControlWithMouse();
                MessageBox.Show(t.Format("basic-flow-selected", selectedIndex, withMouseIndex));
            };

            layoutButtons.AddLinkButton(t["basic-flow-add"]).Click += delegate
            {
                flowLayout.AddControl(generateFlowItem(), 80);
            };
            layoutButtons.AddLinkButton(t["basic-flow-remove"]).Click += delegate
            {
                flowLayout.RemoveControl(flowLayout.GetControlCount() / 2);
            };
            layoutButtons.AddLinkButton(t["basic-flow-insert"]).Click += delegate
            {
                flowLayout.InsertControl(1, generateFlowItem(), 80);
            };
            layoutButtons.AddLinkButton(t["basic-flow-select"]).Click += delegate
            {
                flowLayout.SelectControl(0, false);
            };
            layoutButtons.AddLinkButton(t["basic-flow-show"]).Click += delegate
            {
                flowLayout.SetControlVisible(0, true);
            };
            layoutButtons.AddLinkButton(t["basic-flow-hide"]).Click += delegate
            {
                flowLayout.SetControlVisible(0, false);
            };
            layoutButtons.AddLinkButton(t["basic-flow-bigger"]).Click += delegate
            {
                int controlCount = flowLayout.GetControlCount();
                var controlsHeight = new Dictionary<int, int>();
                for (int i = 0; i < controlCount; i++) controlsHeight[i] = 120;
                flowLayout.UpdateControlsSize(350, controlsHeight);
            };
            layoutButtons.AddLinkButton(t["basic-flow-smaller"]).Click += delegate
            {
                int controlCount = flowLayout.GetControlCount();
                var controlsHeight = new Dictionary<int, int>();
                for (int i = 0; i < controlCount; i++) controlsHeight[i] = 80;
                flowLayout.UpdateControlsSize(250, controlsHeight);
            };
        }
    }
}