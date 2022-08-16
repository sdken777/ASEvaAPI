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
        private void initBasicTagPageB(TabPage tabPage)
        {
            var splitter = tabPage.SetContentAsColumnLayout().AddControl(new Splitter { Position = this.Sizer(300) }, true) as Splitter;
            splitter.Panel1MinimumSize = 300;
            splitter.Panel2MinimumSize = 200;
            var panel1 = splitter.SetPanel1();
            var panel2 = splitter.SetPanel2();

            var layoutPanel1 = panel1.SetContentAsRowLayout(0, 2, VerticalAlignment.Stretch);
            
            var layoutLists = layoutPanel1.AddColumnLayout(false, 120, 0, 8);
            initBasicTagPageBLists(layoutLists);

            var treeView = layoutPanel1.AddControl(new TreeGridView(), true) as TreeGridView;
            initBasicTagPageBTreeView(treeView);

            var layoutDynamicItems = panel2.SetContentAsColumnLayout();
            initBasicTagPageBDynamicItems(layoutDynamicItems);
        }

        private void initBasicTagPageBLists(StackLayout layout)
        {
            var listBox = layout.AddControl(new ListBox(), true) as ListBox;
            for (int i = 0; i < 26; i++) listBox.Items.Add(t.Format("basic-list-item", (char)('A' + i)));

            var checkListBox = layout.AddControl(new CheckableListBox(), true) as CheckableListBox;
            for (int i = 0; i < 26; i++) checkListBox.AddItem(t.Format("basic-list-item-short", (char)('A' + i)), i % 2 == 0, (i / 2) % 2 == 0);
        }

        private void initBasicTagPageBTreeView(TreeGridView view)
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

        // 仅部分平台完美支持（如对跨平台兼容性有较高要求，应手绘实现）
        private void initBasicTagPageBDynamicItems(StackLayout layout)
        {
            var layoutButtons = layout.AddRowLayout();
            var scrollBox = layout.AddControl(new Scrollable(), true) as Scrollable;
            var layoutItems = scrollBox.SetContentAsColumnLayout(2, 2);

            layoutButtons.AddButton(t["basic-dynamic-add"]).Click += delegate
            {
                var panel = new Panel();
                panel.BackgroundColor = Colors.LightYellow;
                var table = panel.SetContentAsTableLayout();
                var row = table.AddRow(true);
                row.AddLabel(t.Format("basic-label-row", ++dynamicItemCount));
                row.AddControl(new SearchBox(), true);
                row = table.AddRow(true);
                row.AddLabel(t.Format("basic-label-row", ++dynamicItemCount));
                row.AddControl(new NumericStepper(), true);
                layoutItems.AddControl(panel, false, 0, 80);
            };
            layoutButtons.AddSpace();
            layoutButtons.AddButton(t["basic-dynamic-remove"]).Click += delegate
            {
                if (layoutItems.Items.Count > 0) layoutItems.Items.RemoveAt(0);
            };
        }

        private int dynamicItemCount = 0;
    }
}