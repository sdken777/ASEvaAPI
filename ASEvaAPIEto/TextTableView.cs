using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Plain text table control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 纯文字的表格控件
    /// </summary>
    public class TextTableView : Panel, TextTableViewCallback
    {
        public TextTableView()
        {
            if (Factory == null) Factory = new DefaultTextTableViewFactory();

            Control etoControl;
            Factory.CreateTextTableViewBackend(this, out etoControl, out backend);
            Content = etoControl;
        }

        /// \~English
        /// <summary>
        /// Add a column (should be called before any ASEva.UIEto.TextTableView.AddRow )
        /// </summary>
        /// <param name="title">Column title</param>
        /// <param name="logicalWidth">Logical width of the column, set to zero to expand</param>
        /// <param name="editable">Whether text of the column is editable</param>
        /// \~Chinese
        /// <summary>
        /// 添加一列（需要在添加行之前进行）
        /// </summary>
        /// <param name="title">列标题</param>
        /// <param name="logicalWidth">列宽度，设为0表示自动扩展</param>
        /// <param name="editable">是否可编辑该列文字</param>
        public void AddColumn(String title, int logicalWidth = 0, bool editable = true)
        {
            if (rowAdded) return;
            colCount++;
            backend.AddColumn(title, logicalWidth, editable);
        }

        /// \~English
        /// <summary>
        /// Get number of rows
        /// </summary>
        /// <returns>行数</returns>
        /// \~Chinese
        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns>行数</returns>
        public int GetRowCount()
        {
            return rowCount;
        }

        /// \~English
        /// <summary>
        /// Get the selected row's index
        /// </summary>
        /// <returns>The selected row's index, -1 means not selected</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前选中行的序号
        /// </summary>
        /// <returns>获取当前选中行的序号，-1表示未选中</returns>
        public int GetSelectedRow()
        {
            return backend.GetSelectedRowIndex();
        }

        /// \~English
        /// <summary>
        /// Get the selected row's index, -1 means not selected
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取当前选中行的序号，-1表示未选中
        /// </summary>
        public int SelectedRow
        {
            get { return GetSelectedRow(); }
        }

        /// \~English
        /// <summary>
        /// Add a row
        /// </summary>
        /// <param name="values">Initial texts for columns, null as empty</param>
        /// \~Chinese
        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="values">该行的初始文字，设为null则默认为空</param>
        public void AddRow(String[]? values = null)
        {
            if (colCount == 0) return;

            var valuesToAdd = new String[colCount].Populate((i) => "");
            int copyCount = values == null ? 0 : Math.Min(values.Length, valuesToAdd.Length);
            for (int i = 0; i < copyCount; i++)
            {
                valuesToAdd[i] = values?[i] == null ? "" : values[i];
            }
            for (int i = copyCount; i < valuesToAdd.Length; i++)
            {
                valuesToAdd[i] = "";
            }

            var list = new List<String[]>();
            list.Add(valuesToAdd);

            rowCount++;
            rowAdded = true;
            backend.AddRows(list);
        }

        /// \~English
        /// <summary>
        /// Add multiple rows
        /// </summary>
        /// <param name="rowsValues">Initial texts of each row, null as empty</param>
        /// \~Chinese
        /// <summary>
        /// 添加多行
        /// </summary>
        /// <param name="rowsValues">每行的初始文字，元素为null则该行默认为空</param>
        public void AddRows(List<String[]> rowsValues)
        {
            if (colCount == 0) return;
            if (rowsValues.Count == 0) return;

            var list = new List<String[]>();
            foreach (var raw in rowsValues)
            {
                var valuesToAdd = new String[colCount].Populate((i) => "");
                int copyCount = raw == null ? 0 : Math.Min(raw.Length, valuesToAdd.Length);
                for (int i = 0; i < copyCount; i++)
                {
                    valuesToAdd[i] = raw?[i] == null ? "" : raw[i];
                }
                for (int i = copyCount; i < valuesToAdd.Length; i++)
                {
                    valuesToAdd[i] = "";
                }
                list.Add(valuesToAdd);
            }

            rowCount += list.Count;
            rowAdded = true;
            backend.AddRows(list);
        }

        /// \~English
        /// <summary>
        /// Remove a row
        /// </summary>
        /// <param name="rowIndex">The row index</param>
        /// \~Chinese
        /// <summary>
        /// 移除一行
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        public void RemoveRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= rowCount) return;

            rowCount--;
            backend.RemoveRows([rowIndex]);
        }

        /// \~English
        /// <summary>
        /// Remove multiple rows
        /// </summary>
        /// <param name="rowIndices">Row indices</param>
        /// \~Chinese
        /// <summary>
        /// 移除多行
        /// </summary>
        /// <param name="rowIndices">各行序号</param>
        public void RemoveRows(int[] rowIndices)
        {
            if (rowIndices.Length == 0) return;

            var flags = new Dictionary<int, bool>();
            foreach (var rowIndex in rowIndices)
            {
                if (rowIndex < 0 || rowIndex >= rowCount) return;
                flags[rowIndex] = true;
            }
            var sortedIndices = flags.Keys.ToList();
            if (sortedIndices.Count == 0) return;

            sortedIndices.Sort();
            sortedIndices.Reverse();

            rowCount -= sortedIndices.Count;
            backend.RemoveRows(sortedIndices.ToArray());
        }

        /// \~English
        /// <summary>
        /// Remove all rows
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 移除所有行
        /// </summary>
        public void RemoveAllRows()
        {
            rowCount = 0;
            backend.RemoveAllRows();
        }

        /// \~English
        /// <summary>
        /// Get text of a cell
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnIndex">Column index</param>
        /// <returns>Text of the cell</returns>
        /// \~Chinese
        /// <summary>
        /// 获取某格文字
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="columnIndex">列序号</param>
        /// <returns>该格文字</returns>
        public String? GetValue(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= rowCount) return null;
            if (columnIndex < 0 || columnIndex >= colCount) return null;
            return backend.GetValue(rowIndex, columnIndex);
        }

        /// \~English
        /// <summary>
        /// Set text of a cell
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnIndex">Column index</param>
        /// <param name="val">Text of the cell</param>
        /// \~Chinese
        /// <summary>
        /// 设置某格文字
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="columnIndex">列序号</param>
        /// <param name="val">该格文字</param>
        public void SetValue(int rowIndex, int columnIndex, String val)
        {
            if (rowIndex < 0 || rowIndex >= rowCount) return;
            if (columnIndex < 0 || columnIndex >= colCount) return;
            backend.SetValue(rowIndex, columnIndex, val);
        }

        /// \~English
        /// <summary>
        /// Set text color of a cell
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnIndex">Column index</param>
        /// <param name="color">Text color</param>
        /// \~Chinese
        /// <summary>
        /// 设置某格文字颜色
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="columnIndex">列序号</param>
        /// <param name="color">文字颜色</param>
        public void SetTextColor(int rowIndex, int columnIndex, Color color)
        {
            if (rowIndex < 0 || rowIndex >= rowCount) return;
            if (columnIndex < 0 || columnIndex >= colCount) return;
            backend.SetTextColor(rowIndex, columnIndex, color);
        }

        /// \~English
        /// <summary>
        /// Set background color of a cell
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnIndex">Column index</param>
        /// <param name="color">Background color</param>
        /// \~Chinese
        /// <summary>
        /// 设置某格背景颜色
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="columnIndex">列序号</param>
        /// <param name="color">背景颜色</param>
        public void SetBackgroundColor(int rowIndex, int columnIndex, Color color)
        {
            if (rowIndex < 0 || rowIndex >= rowCount) return;
            if (columnIndex < 0 || columnIndex >= colCount) return;
            backend.SetBackgroundColor(rowIndex, columnIndex, color);
        }

        /// \~English
        /// <summary>
        /// Occurs when the selected row is changed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 选中行变更事件
        /// </summary>
        public event EventHandler<EventArgs>? SelectedRowsChanged;

        public void OnSelectedRowChanged()
        {
            SelectedRowsChanged?.Invoke(this, EventArgs.Empty);
        }

        /// \~English
        /// <summary>
        /// Occurs after a cell has been edited
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 文本框编辑事件
        /// </summary>
        public event EventHandler<GridViewCellEventArgs>? CellEdited;

        public void OnCellEdited(int rowIndex, int colIndex)
        {
            CellEdited?.Invoke(this, new GridViewCellEventArgs(null, rowIndex, colIndex, null));
        }

        private int colCount = 0;
        private int rowCount = 0;
        private bool rowAdded = false;

        public static TextTableViewFactory? Factory { private get; set; }

		private TextTableViewBackend backend;
    }

	public interface TextTableViewCallback
	{
        void OnSelectedRowChanged();
        void OnCellEdited(int rowIndex, int colIndex);
	}

	public interface TextTableViewBackend
	{
        void AddColumn(String title, int logicalWidth, bool editable); // title not null
        void AddRows(List<String[]> rowsValues); // not empty, text not null
        void RemoveRows(int[] rowIndices); // validated, not empty, reversed
        void RemoveAllRows();
        String? GetValue(int rowIndex, int columnIndex); // validated
        void SetValue(int rowIndex, int columnIndex, String val); // validated, val not null
        void SetTextColor(int rowIndex, int columnIndex, Color color); // validated
        void SetBackgroundColor(int rowIndex, int columnIndex, Color color); // validated
        int GetSelectedRowIndex();
	}

	public interface TextTableViewFactory
	{
		void CreateTextTableViewBackend(TextTableViewCallback callback, out Control etoControl, out TextTableViewBackend backend);
	}

    class DefaultTextTableViewFactory : TextTableViewFactory
    {
        public void CreateTextTableViewBackend(TextTableViewCallback callback, out Control etoControl, out TextTableViewBackend backend)
        {
            var control = new DefaultTextTableViewBackend(callback);
            etoControl = control;
            backend = control;
        }
    }

    public class DefaultTextTableViewBackend : GridView, TextTableViewBackend
    {
        public DefaultTextTableViewBackend(TextTableViewCallback callback)
        {
            GridLines = GridLines.Both;
            CellFormatting += TextTableView_CellFormatting;
            CellEdited += (o, e) =>
            {
                callback.OnCellEdited(e.Row, e.Column);
            };
        }

        public void AddColumn(string title, int logicalWidth, bool editable)
        {
            var column = new GridColumn { DataCell = new TextBoxCell(Columns.Count), HeaderText = title, Editable = editable };
            if (logicalWidth <= 0) column.Expand = true;
            else column.Width = this.Sizer(logicalWidth);
            Columns.Add(column);
        }

        public void AddRows(List<string[]> rowsValues)
        {
            if (DataStore == null) DataStore = new List<GridItem>();

            var list = DataStore as List<GridItem>;
            if (list == null) return;

            foreach (var values in rowsValues)
            {
                list.Add(new GridItem(values));
                foregroundColors.Add(new Dictionary<GridColumn, Color>());
                backgroundColors.Add(new Dictionary<GridColumn, Color>());
            }
            
            DataStore = list;
        }

        public void RemoveRows(int[] rowIndices)
        {
            if (DataStore == null) return;

            var list = DataStore as List<GridItem>;
            if (list == null) return;

            foreach (var rowIndex in rowIndices)
            {
                list.RemoveAt(rowIndex);
                foregroundColors.RemoveAt(rowIndex);
                backgroundColors.RemoveAt(rowIndex);
            }

            DataStore = list;
        }

        public void RemoveAllRows()
        {
            if (DataStore == null) return;

            var list = DataStore as List<GridItem>;
            if (list == null) return;

            list.Clear();
            foregroundColors.Clear();
            backgroundColors.Clear();

            DataStore = list;
        }

        public string? GetValue(int rowIndex, int columnIndex)
        {
            if (DataStore == null) return null;

            var list = DataStore as List<GridItem>;
            if (list == null) return null;

            var values = list[rowIndex].Values;
            return values[columnIndex].ToString();
        }

        public void SetValue(int rowIndex, int columnIndex, string val)
        {
            if (DataStore == null) return;

            var list = DataStore as List<GridItem>;
            if (list == null) return;

            var values = list[rowIndex].Values;
            values[columnIndex] = val;

            DataStore = list;
        }

        public void SetTextColor(int rowIndex, int columnIndex, Color color)
        {
            foregroundColors[rowIndex][Columns[columnIndex]] = color;
            updateColor(rowIndex, columnIndex);
        }

        public void SetBackgroundColor(int rowIndex, int columnIndex, Color color)
        {
            backgroundColors[rowIndex][Columns[columnIndex]] = color;
            updateColor(rowIndex, columnIndex);
        }

        public int GetSelectedRowIndex()
        {
            var index = SelectedRow;
            return index < 0 ? -1 : index;
        }

        private void TextTableView_CellFormatting(object? sender, GridCellFormatEventArgs e)
        {
            if (e.Row >= 0 && e.Row < foregroundColors.Count)
            {
                var rowTable = foregroundColors[e.Row];
                if (rowTable.ContainsKey(e.Column)) e.ForegroundColor = rowTable[e.Column];
                else if (DefaultTextColor != null) e.ForegroundColor = DefaultTextColor.Value;
            }
            if (e.Row >= 0 && e.Row < backgroundColors.Count)
            {
                var rowTable = backgroundColors[e.Row];
                if (rowTable.ContainsKey(e.Column)) e.BackgroundColor = rowTable[e.Column];
                else if (DefaultBackgroundColor != null) e.BackgroundColor = DefaultBackgroundColor.Value;
            }
        }

        private void timer_Elapsed(object? sender, EventArgs e)
        {
            timer?.Stop();
            timer = null;
            Invalidate();
        }

        private void updateColor(int rowIndex, int columnIndex)
        {
            if (UpdateColorMode == InvalidateMode.DelayedInvalidate)
            {
                if (timer == null)
                {
                    timer = new UITimer();
                    timer.Interval = 0.01;
                    timer.Elapsed += timer_Elapsed;
                    timer.Start();
                }
            }
            else if (UpdateColorMode == InvalidateMode.EditCell)
            {
                if (AllowMultipleSelection)
                {
                    var selected = SelectedRows;
                    BeginEdit(rowIndex, columnIndex);
                    CancelEdit();
                    SelectedRows = selected;
                }
                else
                {
                    var selected = SelectedRow;
                    BeginEdit(rowIndex, columnIndex);
                    CancelEdit();
                    SelectedRow = selected;
                }
            }
        }

        private List<Dictionary<GridColumn, Color>> foregroundColors = [];
        private List<Dictionary<GridColumn, Color>> backgroundColors = [];
        private UITimer? timer = null;

        public enum InvalidateMode
        {
            DelayedInvalidate = 0,
            EditCell,
        }

        public static InvalidateMode UpdateColorMode { private get; set; }
        public static Color? DefaultTextColor { private get; set; }
        public static Color? DefaultBackgroundColor { private get; set; }
    }
}