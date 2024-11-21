using System;
using System.Linq;
using System.Collections.Generic;
using MonoMac.AppKit;
using MonoMac.Foundation;
using MonoMac.CoreGraphics;
using Eto.Mac;

namespace ASEva.UIMonoMac
{
    class TextTableViewBackendMonoMac : NSScrollView, ASEva.UIEto.TextTableViewBackend
    {
        public TextTableViewBackendMonoMac(ASEva.UIEto.TextTableViewCallback callback)
        {
            this.callback = callback;

            var dataSource = new TextTableDataSource(callback);

            tableView = new NSTableView();
			tableView.DataSource = dataSource;
            tableView.Delegate = new TextTableDelegate(callback);
            tableView.GridStyleMask = NSTableViewGridStyle.SolidHorizontalLine | NSTableViewGridStyle.SolidVerticalLine;
            tableView.ColumnAutoresizingStyle = NSTableViewColumnAutoresizingStyle.Uniform;
            tableView.AllowsColumnReordering = false;
            tableView.IntercellSpacing = new CGSize(0, 0);
            tableView.RowHeight = 18;
            tableView.BackgroundColor = Eto.Drawing.Color.FromArgb(0xf0, 0xf0, 0xf0).ToNSUI();

			DrawsBackground = false;
			AutoresizesSubviews = true;
			DocumentView = tableView;
			HasVerticalScroller = true;
            HasHorizontalScroller = true;
			AutohidesScrollers = true;
			BorderType = NSBorderType.BezelBorder;
        }

        public void AddColumn(string title, int logicalWidth, bool editable)
        {
            var dataSource = tableView.DataSource as TextTableDataSource;
            if (dataSource == null) return;

            var textCell = new NSTextFieldCell();
            textCell.Editable = true;
            textCell.Wraps = false;
            textCell.DrawsBackground = true;

            var textColumn = new NSTableColumn();
            textColumn.HeaderCell.Title = title;
			textColumn.Editable = editable;
			textColumn.DataCell = textCell;
            if (logicalWidth > 0)
            {
                textColumn.Width = logicalWidth;
                textColumn.ResizingMask = NSTableColumnResizing.UserResizingMask;
            }
            else
            {
                textColumn.Width = 1;
                textColumn.ResizingMask = NSTableColumnResizing.Autoresizing | NSTableColumnResizing.UserResizingMask;
            }
            tableView.AddColumn(textColumn);
            dataSource.Columns.Add(textColumn);

            if (!defaultColorsInit)
            {
                defaultColorsInit = true;
                dataSource.DefaultTextColor = textCell.TextColor;
                dataSource.DefaultBackColor = textCell.BackgroundColor;
            }
        }

        public void AddRows(List<string[]> rowsValues)
        {
            var items = (tableView.DataSource as TextTableDataSource)?.Items;
            if (items == null) return;

            var startRowIndex = items.Count;

            var indices = new List<int>();
            int i = 0;
            foreach (var values in rowsValues)
            {
                items.Add(new TableRowItem(values));
                indices.Add(startRowIndex + i++);
            }

            var colIndices = new int[tableView.ColumnCount];
            for (int n = 0; n < colIndices.Length; n++) colIndices[n] = n;

            tableView.ReloadData();
        }

        public void RemoveRows(int[] rowIndices)
        {
            var items = (tableView.DataSource as TextTableDataSource)?.Items;
            if (items == null) return;

            foreach (var index in rowIndices)
            {
                items.RemoveAt(index);
            }

            tableView.ReloadData();
        }

        public void RemoveAllRows()
        {
            var items = (tableView.DataSource as TextTableDataSource)?.Items;
            if (items == null) return;

            items.Clear();
            tableView.ReloadData();
        }

        public string? GetValue(int rowIndex, int columnIndex)
        {
            var items = (tableView.DataSource as TextTableDataSource)?.Items;
            return items?[rowIndex].Cells[columnIndex].Text;
        }

        public void SetValue(int rowIndex, int columnIndex, string val)
        {
            var items = (tableView.DataSource as TextTableDataSource)?.Items;
            if (items == null) return;

            items[rowIndex].Cells[columnIndex].Text = val;
            tableView.ReloadData(NSIndexSet.FromArray(new int[]{ rowIndex }), NSIndexSet.FromArray(new int[]{ columnIndex }));
        }

        public void SetTextColor(int rowIndex, int columnIndex, Eto.Drawing.Color color)
        {
            var items = (tableView.DataSource as TextTableDataSource)?.Items;
            if (items == null) return;

            items[rowIndex].Cells[columnIndex].TextColor = color.ToNSUI();
            tableView.ReloadData(NSIndexSet.FromArray(new int[]{ rowIndex }), NSIndexSet.FromArray(new int[]{ columnIndex }));
        }

        public void SetBackgroundColor(int rowIndex, int columnIndex, Eto.Drawing.Color color)
        {
            var items = (tableView.DataSource as TextTableDataSource)?.Items;
            if (items == null) return;

            items[rowIndex].Cells[columnIndex].BackColor = color.ToNSUI();
            tableView.ReloadData(NSIndexSet.FromArray(new int[]{ rowIndex }), NSIndexSet.FromArray(new int[]{ columnIndex }));
        }

        public int GetSelectedRowIndex()
        {
            return (int)tableView.SelectedRow;
        }

        class TableCellItem(String text)
        {
            public String Text { get; set; } = text;
            public NSColor? TextColor { get; set; }
            public NSColor? BackColor { get; set; }
        }

        class TableRowItem(String[] values)
        {
            public TableCellItem[] Cells { get; set; } = new TableCellItem[values.Length].Populate((i) => new TableCellItem(values[i]));
        }

        class TextTableDataSource(ASEva.UIEto.TextTableViewCallback callback) : NSTableViewDataSource
		{
			public List<TableRowItem> Items { get; private set; } = [];
            public List<NSTableColumn> Columns { get; private set; } = [];
            public NSColor? DefaultTextColor { private get; set; }
            public NSColor? DefaultBackColor { private get; set; }
            public ASEva.UIEto.TextTableViewCallback Callback { private get; set; } = callback;

			public override long GetRowCount(NSTableView tableView)
			{
				return Items.Count;
			}

			public override NSObject? GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, long row)
			{
				if (row < 0 || row >= Items.Count) return null;

                var cells = Items[(int)row].Cells;
                int index = 0;
                foreach (var column in Columns)
                {
                    var cell = cells[index++];
                    if (tableColumn.Equals(column))
                    {
                        var textCell = new NSTextFieldCell(tableColumn.DataCell.Handle);
                        textCell.TextColor = cell.TextColor == null ? DefaultTextColor : cell.TextColor;
                        textCell.BackgroundColor = cell.BackColor == null ? DefaultBackColor : cell.BackColor;
                        return new NSString(cell.Text);
                    }
                }

                return null;
			}

            public override void SetObjectValue(NSTableView tableView, NSObject theObject, NSTableColumn tableColumn, long row)
            {
                if (row < 0 || row >= Items.Count) return;

                var cells = Items[(int)row].Cells;
                int index = 0;
                foreach (var column in Columns)
                {
                    var cell = cells[index++];
                    if (tableColumn.Equals(column))
                    {
                        cell.Text = new NSString(theObject.Handle);
                        Callback.OnCellEdited((int)row, index - 1);
                        return;
                    }
                }
            }
		}

        class TextTableDelegate : NSTableViewDelegate
        {
            public TextTableDelegate(ASEva.UIEto.TextTableViewCallback callback)
            {
                this.callback = callback;
            }

            public override void SelectionDidChange(NSNotification notification)
            {
                callback.OnSelectedRowChanged();
            }

            private ASEva.UIEto.TextTableViewCallback callback;
        }

        private ASEva.UIEto.TextTableViewCallback callback;
        private NSTableView tableView;
        private bool defaultColorsInit = false;
    }
}
