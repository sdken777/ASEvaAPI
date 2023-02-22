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

            var treeView = layoutPanel1.AddControl(new TreeGridView(), true) as TreeGridView;
            initBasicTabPageBTreeView(treeView);

            var layoutFlowItems = panel2.SetContentAsColumnLayout();
            initBasicTabPageBFlowItems(layoutFlowItems);
        }

        private void initBasicTabPageBLists(StackLayout layout)
        {
            var listBox = layout.AddControl(new ListBox(), true) as ListBox;
            for (int i = 0; i < 26; i++) listBox.Items.Add(t.Format("basic-list-item", (char)('A' + i)));

            var checkListBox = layout.AddControl(new CheckableListBox(), true) as CheckableListBox;
            for (int i = 0; i < 26; i++) checkListBox.AddItem(t.Format("basic-list-item-short", (char)('A' + i)), i % 2 == 0, (i / 2) % 2 == 0);
        }

        private void initBasicTabPageBTreeView(TreeGridView view)
        {
            view.ShowHeader  = false;
            view.Columns.Add(new GridColumn{ DataCell = new TextBoxCell(0), Width = this.Sizer(150) });

            var model = new TreeGridItemCollection();

            var parent1 = new TreeGridItem{ Tag = "p1", Values = new String[] { t.Format("basic-tree-parent", 1) } };
            var child1 = new TreeGridItem{ Tag = "c1", Values = new String[] { t.Format("basic-tree-child", 1) } };
            parent1.Children.Add(child1);
            var child2 = new TreeGridItem{ Tag = "c2", Values = new String[] { t.Format("basic-tree-child", 2) } };
            parent1.Children.Add(child2);
            model.Add(parent1);

            var parent2 = new TreeGridItem{ Tag = "p2", Values = new String[] { t.Format("basic-tree-parent", 2) } };
            var child3 = new TreeGridItem{ Tag = "c3", Values = new String[] { t.Format("basic-tree-child", 3) } };
            parent2.Children.Add(child3);
            var child4 = new TreeGridItem{ Tag = "c4", Values = new String[] { t.Format("basic-tree-child", 4) } };
            parent2.Children.Add(child4);
            model.Add(parent2);

            view.DataStore = model;

            view.CellFormatting += (sender, args) =>
            {
                args.ForegroundColor = (args.Item as TreeGridItem).Tag as String switch
                {
                    "c2" => Colors.Red,
                    "p2" => Colors.Blue,
                    _ => Colors.Black
                };
            };
            view.Activated += delegate
            {
                if (view.SelectedItem != null) MessageBox.Show((view.SelectedItem as TreeGridItem).Values[0] as String, "");
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