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
            var panel1 = splitter.SetPanel1();
            var panel2 = splitter.SetPanel2();

            var layoutPanel1 = panel1.SetContentAsRowLayout(0, 2, VerticalAlignment.Stretch);
            
            var layoutLists = layoutPanel1.AddColumnLayout(false, 120, 0, 8);
            initBasicTagPageBLists(layoutLists);

            var treeView = layoutPanel1.AddControl(new TreeGridView(), true) as TreeGridView;
            initBasicTagPageBTreeView(treeView);

            var scrollBox = panel2.SetContentAsColumnLayout(0).AddControl(new Scrollable(), true) as Scrollable;
            scrollBox.SetContentAsColumnLayout().AddControl(new ImageView { Image = Bitmap.FromResource("picture.png") }, true);
        }

        private void initBasicTagPageBLists(StackLayout layout)
        {
            var listBox = layout.AddControl(new ListBox(), true) as ListBox;
            listBox.Items.Add(t.Format("basic-list-item", "A"));
            listBox.Items.Add(t.Format("basic-list-item", "B"));
            listBox.Items.Add(t.Format("basic-list-item", "C"));

            var checkListBox = layout.AddControl(new CheckBoxList { Orientation = Orientation.Vertical} , true) as CheckBoxList;
            checkListBox.Items.Add(t.Format("basic-list-item", "A"));
            checkListBox.Items.Add(t.Format("basic-list-item", "B"));
            checkListBox.Items.Add(t.Format("basic-list-item", "C"));
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
    }
}