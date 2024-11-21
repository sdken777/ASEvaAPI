using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Eto.Wpf;

namespace ASEva.UIWpf
{
    class TextTableViewBackendWpf : DataGrid, ASEva.UIEto.TextTableViewBackend
    {
        public TextTableViewBackendWpf(ASEva.UIEto.TextTableViewCallback callback)
        {
            this.callback = callback;

            HorizontalGridLinesBrush = new SolidColorBrush(Colors.Gray);
            VerticalGridLinesBrush = new SolidColorBrush(Colors.Gray);
            RowHeaderWidth = 0;
            ItemsSource = new ObservableCollection<TableRowItem>();
            AutoGenerateColumns = false;
            CanUserAddRows = false;
            CanUserDeleteRows = false;
            CanUserReorderColumns = false;
            CanUserResizeRows = false;
            CanUserSortColumns = false;

            SelectionChanged += delegate
            {
                callback.OnSelectedRowChanged();
            };
        }

        public void AddColumn(string title, int logicalWidth, bool editable)
        {
            var column = new DataGridTextColumn();
            column.Header = title;
            column.IsReadOnly = !editable;
            if (logicalWidth <= 0) column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
            else column.Width = new DataGridLength(logicalWidth);
            column.Binding = new Binding("Cells[" + Columns.Count + "].Text");

            var style = new Style();
            style.TargetType = typeof(TextBlock);
            style.Setters.Add(new Setter(TextBlock.ForegroundProperty, new Binding("Cells[" + Columns.Count + "].TextBrush")));
            style.Setters.Add(new Setter(TextBlock.BackgroundProperty, new Binding("Cells[" + Columns.Count + "].BackBrush")));
            column.ElementStyle = style;

            Columns.Add(column);
        }

        public void AddRows(List<string[]> rowsValues)
        {
            var items = ItemsSource as ObservableCollection<TableRowItem>;
            if (items == null) return;

            foreach (var values in rowsValues)
            {
                var rowItem = new TableRowItem(Columns.Count, items, values, callback);
                items.Add(rowItem);
            }
        }

        public void RemoveRows(int[] rowIndices)
        {
            var items = ItemsSource as ObservableCollection<TableRowItem>;
            if (items == null) return;

            foreach (var index in rowIndices)
            {
                items.RemoveAt(index);
            }
        }

        public void RemoveAllRows()
        {
            var items = ItemsSource as ObservableCollection<TableRowItem>;
            if (items == null) return;
            items.Clear();
        }

        public string GetValue(int rowIndex, int columnIndex)
        {
            var items = ItemsSource as ObservableCollection<TableRowItem>;
            return items?[rowIndex].Cells[columnIndex].Text ?? "";
        }

        public void SetValue(int rowIndex, int columnIndex, string val)
        {
            var items = ItemsSource as ObservableCollection<TableRowItem>;
            if (items == null) return;
            items[rowIndex].Cells[columnIndex].SetText(val);
        }

        public void SetTextColor(int rowIndex, int columnIndex, Eto.Drawing.Color color)
        {
            var items = ItemsSource as ObservableCollection<TableRowItem>;
            if (items == null) return;
            items[rowIndex].Cells[columnIndex].SetTextColor(color.ToWpf());
        }

        public void SetBackgroundColor(int rowIndex, int columnIndex, Eto.Drawing.Color color)
        {
            var items = ItemsSource as ObservableCollection<TableRowItem>;
            if (items == null) return;
            items[rowIndex].Cells[columnIndex].SetBackColor(color.ToWpf());
        }

        public int GetSelectedRowIndex()
        {
            return SelectedIndex;
        }

        class TableCellItem
        {
            public TableCellItem(TableRowItem row, String text, int columnIndex, ASEva.UIEto.TextTableViewCallback callback)
            {
                this.row = row;
                this.text = text;
                this.columnIndex = columnIndex;
                this.callback = callback;
                textColor = Colors.Black;
                backColor = Colors.Transparent;
            }

            public Brush TextBrush
            {
                get { return new SolidColorBrush(textColor); }
            }

            public Brush BackBrush
            {
                get { return new SolidColorBrush(backColor); }
            }

            public String Text
            {
                get { return text; }
                set // by user (binding)
                {
                    text = value;
                    callback.OnCellEdited(row.RowIndex, columnIndex);
                }
            }

            public void SetText(String text) // by program
            {
                this.text = text;
                row.Notify();
            }

            public void SetTextColor(Color color) // by program
            {
                textColor = color;
                row.Notify();
            }

            public void SetBackColor(Color color) // by program
            {
                backColor = color;
                row.Notify();
            }

            private TableRowItem row;
            private String text;
            private Color textColor;
            private Color backColor;
            private ASEva.UIEto.TextTableViewCallback callback;
            private int columnIndex;
        }

        class TableRowItem : INotifyPropertyChanged
        {
            public TableRowItem(int columns, ObservableCollection<TableRowItem> items, String[] values, ASEva.UIEto.TextTableViewCallback callback)
            {
                Cells = new TableCellItem[columns].Populate((i) => new TableCellItem(this, values[i], i, callback));
                this.items = items;
            }

            public TableCellItem[] Cells { get; private set; }

            public int RowIndex { get { return items.IndexOf(this); } }


            public event PropertyChangedEventHandler? PropertyChanged;
            public void Notify()
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cells"));
            }

            private ObservableCollection<TableRowItem> items;
        }

        private ASEva.UIEto.TextTableViewCallback callback;
    }
}
