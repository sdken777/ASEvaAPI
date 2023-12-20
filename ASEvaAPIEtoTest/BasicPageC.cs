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
        private void initBasicTabPageC(TabPage tabPage)
        {
            var layoutRow = tabPage.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);

            layoutRow.AddControl(new TextArea { Text = t["empty"] }, false, 150);

            var layoutTableView = layoutRow.AddColumnLayout(true, 2);
            initBasicTabPageCTableView(layoutTableView);
        }

        private void initBasicTabPageCTableView(StackLayout layout)
        {
            var layoutGridViewRow = layout.AddRowLayout();
            var linkButtonAdd = layoutGridViewRow.AddLinkButton(t["basic-grid-add-row"]);
            layoutGridViewRow.AddSpace();
            var linkButtonRemove = layoutGridViewRow.AddLinkButton(t["basic-grid-remove-row"]);

            var tableView = layout.AddControl(new TextTableView(), true) as TextTableView;
            tableView.AddColumn(t["basic-grid-key-title"]);
            tableView.AddColumn(t["basic-grid-value-title"]);

            layoutGridViewRow = layout.AddRowLayout();
            var linkButtonChangeColor = layoutGridViewRow.AddLinkButton(t["basic-grid-change-color"]);
            layoutGridViewRow.AddSpace();
            var linkButtonReload = layoutGridViewRow.AddLinkButton(t["basic-grid-reload"]);

            linkButtonAdd.Click += delegate { tableView.AddRow(); };
            linkButtonRemove.Click += delegate { tableView.RemoveRow(tableView.SelectedRow); };
            linkButtonChangeColor.Click += delegate
            {
                tableView.SetTextColor(0, 0, Colors.Red);
                tableView.SetBackgroundColor(0, 1, Colors.Green);
            };
            linkButtonReload.Click += delegate { tableView.ReloadData(0); };
        }
    }
}