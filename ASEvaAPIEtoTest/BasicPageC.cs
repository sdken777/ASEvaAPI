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

            var checkListBox = layout.AddControl(new CheckableListBox(), true) as CheckableListBox;
            for (int i = 0; i < 1000; i++) checkListBox.AddItem(t.Format("basic-list-item-short", i.ToString()), i % 2 == 0, (i / 2) % 2 == 0);
            checkListBox.ItemClicked += delegate
            {
                var selectedIndex = checkListBox.GetSelectedRow();
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

            var tableView = layout.AddControl(new TextTableView(), true) as TextTableView;
            tableView.AddColumn(t["basic-grid-key-title"]);
            tableView.AddColumn(t["basic-grid-value-title"]);
            tableView.CellEdited += (o, row, col) =>
            {
                if (row == 0) return;
                tableView.SetValue(0, 0, t.Format("basic-grid-edited", row, col));
                tableView.SetValue(0, 1, tableView.GetValue(row, col));
            };

            layoutGridViewRow = layout.AddRowLayout();
            var linkButtonChangeColor = layoutGridViewRow.AddLinkButton(t["basic-grid-change-color"]);
            layoutGridViewRow.AddSpace();

            linkButtonAdd.Click += delegate { tableView.AddRow(); };
            linkButtonRemove.Click += delegate { tableView.RemoveRow(tableView.GetSelectedRow()); };
            linkButtonChangeColor.Click += delegate
            {
                tableView.SetTextColor(0, 0, Colors.Red);
                tableView.SetBackgroundColor(0, 1, Colors.Green);
            };
        }

        private int checkListTarget = 0;
    }
}