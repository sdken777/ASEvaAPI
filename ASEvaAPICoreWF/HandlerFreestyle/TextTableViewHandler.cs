using System;
using System.Linq;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto;
using Eto.Forms;
using Eto.Drawing;
using Eto.WinForms;

namespace ASEva.UICoreWF
{
    class TextTableViewFactoryCoreWF : TextTableViewFactory
    {
        public void CreateTextTableViewBackend(TextTableViewCallback callback, out Control etoControl, out TextTableViewBackend backend)
        {
            var panel = new TextTableViewBackendCoreWF(callback);
            etoControl = panel.ToEto();
            backend = panel;
        }
    }

    class TextTableViewBackendCoreWF : System.Windows.Forms.DataGridView, TextTableViewBackend
    {
        public TextTableViewBackendCoreWF(TextTableViewCallback callback)
        {
            SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;
            AllowUserToResizeColumns = true;
            AllowUserToResizeRows = false;
            RowHeadersVisible = false;
            ColumnHeadersHeight = (int)(Pixel.Scale * 24);
            ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            RowTemplate.Height = (int)(Pixel.Scale * 20);

            CellEndEdit += (o, e) =>
            {
                callback.OnCellEdited(e.RowIndex, e.ColumnIndex);
            };

            SelectionChanged += delegate
            {
                callback.OnSelectedRowChanged();
            };
        }

        public void AddColumn(string title, int logicalWidth, bool editable)
        {
            var column = new System.Windows.Forms.DataGridViewColumn();
            column.HeaderText = title;
            column.ReadOnly = !editable;
            column.CellTemplate = new System.Windows.Forms.DataGridViewTextBoxCell();
            if (logicalWidth <= 0)
            {
                column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                column.Width = Math.Max(1, (int)(logicalWidth * Pixel.Scale));
            }
            Columns.Add(column);
        }

        public void AddRows(List<string[]> rowsValues)
        {
            Rows.Add(rowsValues.Count);
            for (int i = 0; i < rowsValues.Count; i++)
            {
                var rowIndex = Rows.Count - rowsValues.Count + i;
                Rows[rowIndex].SetValues(rowsValues[i]);
            }
        }

        public void RemoveRows(int[] rowIndices)
        {
            foreach (var index in rowIndices)
            {
                Rows.RemoveAt(index);
            }
        }

        public void RemoveAllRows()
        {
            Rows.Clear();
        }

        public string GetValue(int rowIndex, int columnIndex)
        {
            return Rows[rowIndex].Cells[columnIndex].Value.ToString();
        }

        public void SetValue(int rowIndex, int columnIndex, string val)
        {
            Rows[rowIndex].Cells[columnIndex].Value = val;
        }

        public void SetTextColor(int rowIndex, int columnIndex, Color color)
        {
            Rows[rowIndex].Cells[columnIndex].Style.ForeColor = color.ToSD();
        }

        public void SetBackgroundColor(int rowIndex, int columnIndex, Color color)
        {
            Rows[rowIndex].Cells[columnIndex].Style.BackColor = color.ToSD();
        }

        public int GetSelectedRowIndex()
        {
            if (SelectedRows == null || SelectedRows.Count == 0) return -1;
            foreach (System.Windows.Forms.DataGridViewRow row in SelectedRows)
            {
                return Rows.IndexOf(row);
            }
            return -1;
        }
    }
}
