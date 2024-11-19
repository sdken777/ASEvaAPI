using System;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class EtoTestPanel
    {
        private void initBasicTabPageC(TabPage tabPage)
        {
            var layoutRow = tabPage.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);

            var layoutStuff = layoutRow.AddColumnLayout(false, 150, 0, 2);
            layoutStuff.AddControl(new TextArea { Text = t["empty"] }, true);
            var layoutCases = layoutStuff.AddRowLayout();

            // layoutCases.AddLabel(t["basic-cases"]);

            var layoutCheckList = layoutRow.AddColumnLayout(false, 180, 0, 2);
            initBasicTabPageCCheckList(layoutCheckList);

            layoutRow.AddSeparator();

            var layoutTableView = layoutRow.AddColumnLayout(true, 2);
            initBasicTabPageCTableView(layoutTableView);
        }

        private void initBasicTabPageCCheckList(StackLayout layout)
        {
            var buttonRow = layout.AddRowLayout();
            var buttonCheckAll = buttonRow.AddLinkButton(t["basic-checklist-check-all"]);
            buttonRow.AddSpace();
            var buttonUncheckAll = buttonRow.AddLinkButton(t["basic-checklist-uncheck-all"]);

            var checkListBox = (layout.AddControl(new CheckableListBox(), true) as CheckableListBox)!;
            for (int i = 0; i < 1000; i++) checkListBox.AddItem(t.Format("basic-list-item-short", i.ToString()), i % 2 == 0, (i / 2) % 2 == 0);
            checkListBox.ItemClicked += delegate
            {
                var selectedIndex = checkListBox.SelectedRow;
                MessageBox.Show(selectedIndex + ": " + checkListBox.GetChecked(selectedIndex));
            };

            buttonCheckAll.Click += delegate { checkListBox.CheckAll(); };
            buttonUncheckAll.Click += delegate { checkListBox.UncheckAll(); };

            buttonRow = layout.AddRowLayout();
            buttonRow.AddLinkButton(t["basic-checklist-remove"]).Click += delegate
            {
                if (checkListBox.GetItemCount() > 100) checkListBox.RemoveItem(checkListBox.GetItemCount() - 1);
            };
            buttonRow.AddSpace();
            buttonRow.AddLinkButton(t["basic-checklist-set-text"]).Click += delegate
            {
                checkListBox.SetText(checkListTarget, checkListTarget.ToString());
                if (checkListTarget < 99) checkListTarget++;
            };

            buttonRow = layout.AddRowLayout();
            buttonRow.AddLinkButton(t["basic-checklist-get-check"]).Click += delegate
            {
                MessageBox.Show(checkListTarget + ": " + checkListBox.GetChecked(checkListTarget).ToString());
            };
            buttonRow.AddSpace();
            buttonRow.AddLinkButton(t["basic-checklist-set-check"]).Click += delegate
            {
                checkListBox.SetChecked(checkListTarget, !checkListBox.GetChecked(checkListTarget));
                if (checkListTarget < 99) checkListTarget++;
            };

            buttonRow = layout.AddRowLayout();
            buttonRow.AddLinkButton(t["basic-checklist-get-enable"]).Click += delegate
            {
                MessageBox.Show(checkListTarget + ": " + checkListBox.GetEnabled(checkListTarget).ToString());
            };
            buttonRow.AddSpace();
            buttonRow.AddLinkButton(t["basic-checklist-set-enable"]).Click += delegate
            {
                checkListBox.SetEnabled(checkListTarget, !checkListBox.GetEnabled(checkListTarget));
                if (checkListTarget < 99) checkListTarget++;
            };
        }

        private void initBasicTabPageCTableView(StackLayout layout)
        {
            var layoutGridViewRow = layout.AddRowLayout();
            var linkButtonAdd = layoutGridViewRow.AddLinkButton(t["basic-grid-add-row"]);
            layoutGridViewRow.AddSpace();
            var linkButtonRemove = layoutGridViewRow.AddLinkButton(t["basic-grid-remove-row"]);

            var tableView = (layout.AddControl(new TextTableView(), true) as TextTableView)!;
            tableView.AddColumn(t["basic-grid-index-title"], 50, false);
            tableView.AddColumn(t["basic-grid-key-title"]);
            tableView.AddColumn(t["basic-grid-value-title"]);

            layoutGridViewRow = layout.AddRowLayout();
            var linkButtonChangeColor = layoutGridViewRow.AddLinkButton(t["basic-grid-change-color"]);
            layoutGridViewRow.AddSpace();
            var labelChangedRow = layoutGridViewRow.AddLabel("");

            tableView.SelectedRowsChanged += delegate { labelChangedRow.Text = tableView.SelectedRow.ToString(); };
            tableView.CellEdited += (o, e) =>
            {
                if (e.Row == 0) return;
                tableView.SetValue(0, 1, t.Format("basic-grid-edited", e.Row, e.Column));
                tableView.SetValue(0, 2, tableView.GetValue(e.Row, e.Column) ?? "");
            };

            linkButtonAdd.Click += delegate { tableView.AddRow(new String[]{ (tableItemIndex++).ToString(), "", "" }); };
            linkButtonRemove.Click += delegate { tableView.RemoveRow(tableView.SelectedRow); };
            linkButtonChangeColor.Click += delegate
            {
                tableView.SetTextColor(0, 1, Colors.Red);
                tableView.SetBackgroundColor(0, 2, Colors.Green);
            };

            layout.AddSpace(4);
            var tableView2 = (layout.AddControl(new TextTableView(), false, 0, 80) as TextTableView)!;
            for (int i = 0; i < 10; i++)
            {
                tableView2.AddColumn(t.Format("basic-grid-column", i + 1), 50, true);
            }
            tableView2.AddRow();
        }

        private int checkListTarget = 0;
        private int tableItemIndex = 0;
    }
}