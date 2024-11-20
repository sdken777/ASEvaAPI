using System;
using System.Linq;
using System.Collections.Generic;
using MonoMac.AppKit;
using MonoMac.Foundation;
using MonoMac.CoreGraphics;

namespace ASEva.UIMonoMac
{
    class CheckableListBoxBackendMonoMac : NSScrollView, ASEva.UIEto.CheckableListBoxBackend
    {
        public CheckableListBoxBackendMonoMac(ASEva.UIEto.CheckableListBoxCallback callback)
        {
            this.callback = callback;

            var switchCell = new NSButtonCell();
            switchCell.SetButtonType(NSButtonType.Switch);
            switchCell.Title = "";

			var switchColumn = new NSTableColumn();
			switchColumn.Width = switchCell.CellSize.Width;
			switchColumn.Editable = true;
			switchColumn.DataCell = switchCell;

            var textCell = new VerticalAlignTextCell();
            textCell.Wraps = false;

            var textColumn = new NSTableColumn();
            textColumn.ResizingMask = NSTableColumnResizing.Autoresizing;
			textColumn.Editable = false;
			textColumn.DataCell = textCell;

            var dataSource = new CheckableListDataSource(switchColumn, textColumn, textCell.TextColor, callback);

            tableView = new NSTableView();
			tableView.AddColumn(switchColumn);
            tableView.AddColumn(textColumn);
			tableView.HeaderView = null;
			tableView.DataSource = dataSource;
            tableView.IntercellSpacing = new CGSize(0, 0);
            tableView.RowHeight = 18;

			DrawsBackground = false;
			AutoresizesSubviews = true;
			DocumentView = tableView;
			HasVerticalScroller = true;
			AutohidesScrollers = true;
			BorderType = NSBorderType.BezelBorder;
        }

        public void AddItems(string[] itemsText, bool[]? itemsChecked, bool[]? itemsEnabled)
        {
            var items = (tableView.DataSource as CheckableListDataSource)?.Items;
            if (items == null) return;
            
            var startRowIndex = items.Count;

            var indices = new List<int>();
            for (int i = 0; i < itemsText.Length; i++)
            {
                var text = itemsText[i];
                bool isChecked = itemsChecked == null ? false : itemsChecked[i];
                bool isEnabled = itemsEnabled == null ? true : itemsEnabled[i];
                items.Add(new CheckableItem(text)
                {
                    IsChecked = isChecked,
                    IsEnabled = isEnabled,
                });
                indices.Add(startRowIndex + i);
            }

            tableView.ReloadData();
        }

        public void RemoveItems(int[] indices)
        {
            var items = (tableView.DataSource as CheckableListDataSource)?.Items;
            if (items == null) return;

            foreach (var index in indices)
            {
                items.RemoveAt(index);
            }

            tableView.ReloadData();
        }

        public void RemoveAllItems()
        {
            var items = (tableView.DataSource as CheckableListDataSource)?.Items;
            if (items == null) return;

            items.Clear();
            tableView.ReloadData();
        }

        public bool GetChecked(int index)
        {
            var items = (tableView.DataSource as CheckableListDataSource)?.Items;
            return items?[index].IsChecked ?? false;
        }

        public void SetChecked(int[] indices, bool isChecked)
        {
            var items = (tableView.DataSource as CheckableListDataSource)?.Items;
            if (items == null) return;

            foreach (var index in indices)
            {
                items[index].IsChecked = isChecked;
            }
            tableView.ReloadData(NSIndexSet.FromArray(indices.ToArray()), NSIndexSet.FromArray(new int[]{ 0 }));
        }

        public void SetText(int index, string text)
        {
            var items = (tableView.DataSource as CheckableListDataSource)?.Items;
            if (items == null) return;

            items[index].Text = text;
            tableView.ReloadData(NSIndexSet.FromArray(new int[]{ index }), NSIndexSet.FromArray(new int[]{ 1 }));
        }

        public void SetEnabled(int index, bool isEnabled)
        {
            var items = (tableView.DataSource as CheckableListDataSource)?.Items;
            if (items == null) return;

            items[index].IsEnabled = isEnabled;
            tableView.ReloadData(NSIndexSet.FromArray(new int[]{ index }), NSIndexSet.FromArray(new int[]{ 0, 1 }));
        }

        public int[] GetCheckedIndices()
        {
            var list = new List<int>();
            var items = (tableView.DataSource as CheckableListDataSource)?.Items.ToArray();
            if (items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if(items[i].IsChecked) list.Add(i);
                }
            }
            return list.ToArray();
        }

        public int GetSelectedRowIndex()
        {
            return (int)tableView.SelectedRow;
        }

        class CheckableItem(String text)
        {
            public String Text { get; set; } = text;
            public bool IsChecked { get; set; }
            public bool IsEnabled { get; set; }
        }

		class CheckableListDataSource(NSTableColumn switchColumn, NSTableColumn textColumn, NSColor defaultTextColor, ASEva.UIEto.CheckableListBoxCallback callback) : NSTableViewDataSource
		{
			public List<CheckableItem> Items { get; private set; } = [];
            public NSTableColumn SwitchColumn { private get; set; } = switchColumn;
            public NSTableColumn TextColumn { private get; set; } = textColumn;
            public NSColor DefaultTextColor { private get; set; } = defaultTextColor;
            public ASEva.UIEto.CheckableListBoxCallback Callback { private get; set; } = callback;

			public override long GetRowCount(NSTableView tableView)
			{
				return Items.Count;
			}

			public override NSObject? GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, long row)
			{
				if (row < 0 || row >= Items.Count) return null;

                var item = Items[(int)row];
				if (tableColumn.Equals(SwitchColumn))
                {
                    var switchCell = new NSButtonCell(tableColumn.DataCell.Handle);
                    switchCell.Enabled = item.IsEnabled;
                    return new NSNumber(item.IsChecked ? 1 : 0);
                }
                else if (tableColumn.Equals(TextColumn))
                {
                    var textCell = new VerticalAlignTextCell(tableColumn.DataCell.Handle);
                    textCell.TextColor = item.IsEnabled ? DefaultTextColor : NSColor.LightGray;
                    return new NSString(item.Text);
                }
                else return null;
			}

            public override void SetObjectValue(NSTableView tableView, NSObject theObject, NSTableColumn tableColumn, long row)
            {
                if (row < 0 || row >= Items.Count) return;

                var item = Items[(int)row];
                if (tableColumn.Equals(SwitchColumn))
                {
                    var number = new NSNumber(theObject.Handle);
                    item.IsChecked = number.BoolValue;
                    Callback.OnItemClicked();
                }
            }
		}

        private ASEva.UIEto.CheckableListBoxCallback callback;
        private NSTableView tableView;
    }

    class VerticalAlignTextCell : NSTextFieldCell
    {
        public VerticalAlignTextCell()
        {}

        public VerticalAlignTextCell(IntPtr handle) : base(handle)
        {}

        public override CGRect DrawingRectForBounds(CGRect theRect)
        {
            var titleFrame = base.DrawingRectForBounds(theRect);
            var yOffset = (titleFrame.Height - CellSize.Height) / 2;
            return new CGRect(titleFrame.X, titleFrame.Y + yOffset, titleFrame.Width, titleFrame.Height - yOffset);
        }
    }
}
