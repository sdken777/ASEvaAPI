using System;
using System.Collections.Generic;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initBasicTabPageB(TabPage tabPage)
        {
            var splitter = tabPage.SetContentAsColumnLayout().AddControl(new Splitter { Position = this.Sizer(300) }, true) as Splitter;
            splitter.Panel1MinimumSize = 300;
            splitter.Panel2MinimumSize = 200;
            var panel1 = splitter.SetPanel1();
            var panel2 = splitter.SetPanel2();

            var layoutPanel1 = panel1.SetContentAsRowLayout(0, 2, VerticalAlignment.Stretch);
            
            var layoutLists = layoutPanel1.AddColumnLayout(false, 120, 0, 8);
            initBasicTabPageBLists(layoutLists);

            var treeViewLayout = layoutPanel1.AddColumnLayout(true, 2);
            initBasicTabPageBTreeView(treeViewLayout);

            var layoutFlowItems = panel2.SetContentAsColumnLayout();
            initBasicTabPageBFlowItems(layoutFlowItems);
        }

        private void initBasicTabPageBLists(StackLayout layout)
        {
            var listBox = layout.AddControl(new ListBox(), true) as ListBox;
            for (int i = 1; i <= 1000; i++) listBox.Items.Add(t.Format("basic-list-item", i.ToString()));

            var checkListBox = layout.AddControl(new CheckableListBox(), true) as CheckableListBox;
            for (int i = 1; i <= 1000; i++) checkListBox.AddItem(t.Format("basic-list-item-short", i.ToString()), i % 2 == 0, (i / 2) % 2 == 0);
        }

        private void initBasicTabPageBTreeView(StackLayout layout)
        {
            var view = layout.AddControl(new SimpleTreeView(), true) as SimpleTreeView;
            var button = layout.AddLinkButton(t["basic-grid-change-color"]);

            var parentNodes = new List<SimpleTreeNode>();
            for (int i = 1; i <= 99; i++)
            {
                var parentNode = new SimpleTreeNode();
                parentNode.Key = i.ToString();
                parentNode.Text = t.Format("basic-tree-parent", i.ToString("D2"));
                if (i % 2 == 0) parentNode.BackgroundColor = Colors.LightGrey;
                if (i > 90) parentNode.ChildNodesExpanded = true;
                for (int j = 1; j <= 99; j++)
                {
                    var childNode = new SimpleTreeNode();
                    childNode.Key = i + "." + j;
                    childNode.Text = t.Format("basic-tree-child", j.ToString("D2"));
                    if (j % 2 == 0) childNode.TextColor = Colors.Blue;
                    parentNode.ChildNodes.Add(childNode);
                }
                parentNodes.Add(parentNode);
            }
            view.SetModel(parentNodes.ToArray(), true);

            view.SelectedItemActivated += delegate
            {
                var selectedKey = view.GetSelectedKey();
                if (selectedKey != null) MessageBox.Show(selectedKey as String, "");
            };

            button.Click += delegate
            {
                var tasks = new List<SimpleTreeNodeUpdateTask>();
                for (int i = 1; i <= 99; i++)
                {
                    if (i % 2 == 0)
                    {
                        var task = new SimpleTreeNodeUpdateTask();
                        task.Key = i.ToString();
                        task.TextColor = Colors.White;
                        task.BackgroundColor = Colors.DimGray;
                        tasks.Add(task);
                    }
                }
                view.UpdateNodes(tasks.ToArray());
            };
        }

        private void initBasicTabPageBFlowItems(StackLayout layout)
        {
            var layoutButtons = layout.AddRowLayout();
            var flowLayout = layout.AddControl(new FlowLayout(), true) as FlowLayout;
            flowLayout.ControlSelected += delegate
            {
                MessageBox.Show(t.Format("basic-flow-selected", flowLayout.GetSelectedControlIndex(), ""));
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
        }

        private Panel generateFlowItem()
        {
            var panel = new Panel();
            panel.BackgroundColor = Colors.LightYellow;
            var table = panel.SetContentAsTableLayout();
            var row = table.AddRow(true);
            row.AddLabel(t.Format("basic-label-row", ++flowItemCount));
            row.AddControl(new SearchBox(), true);
            row = table.AddRow(true);
            row.AddLabel(t.Format("basic-label-row", ++flowItemCount));
            row.AddControl(new NumericStepper(), true);
            return panel;
        }

        private int flowItemCount = 0;
    }
}