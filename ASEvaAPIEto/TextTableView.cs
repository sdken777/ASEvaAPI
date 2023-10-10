using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.3) 纯文字的表格控件
    /// </summary>
    public class TextTableView : GridView
    {
        public TextTableView()
        {
            GridLines = GridLines.Both;
            CellFormatting += TextTableView_CellFormatting;
        }

        /// <summary>
        /// 添加一列（需要在添加行之前进行）
        /// </summary>
        /// <param name="title">列标题</param>
        /// <param name="logicalWidth">列宽度</param>
        /// <param name="editable">是否可编辑该列文字</param>
        public void AddColumn(String title, int logicalWidth, bool editable = true)
        {
            if (DataStore != null) return;

            var column = new GridColumn { DataCell = new TextBoxCell(Columns.Count), HeaderText = title, Width = this.Sizer(logicalWidth), Editable = editable };
            Columns.Add(column);
        }

        /// <summary>
        /// (api:eto=2.0.10) 获取行数
        /// </summary>
        /// <returns>行数</returns>
        public int GetRowCount()
        {
            if (DataStore == null) return 0;
            if (!(DataStore is List<GridItem>)) return 0;
            return (DataStore as List<GridItem>).Count;
        }

        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="values">该行的初始文字，设为null则默认为空</param>
        public void AddRow(String[] values = null)
        {
            if (Columns.Count == 0) return;

            if (DataStore == null) DataStore = new List<GridItem>();
            else if (!(DataStore is List<GridItem>)) return;

            var list = DataStore as List<GridItem>;
            var valuesToAdd = new String[Columns.Count];
            int copyCount = values == null ? 0 : Math.Min(values.Length, valuesToAdd.Length);
            for (int i = 0; i < copyCount; i++)
            {
                valuesToAdd[i] = values[i] == null ? "" : values[i];
            }
            for (int i = copyCount; i < valuesToAdd.Length; i++)
            {
                valuesToAdd[i] = "";
            }
            list.Add(new GridItem(valuesToAdd));
            foregroundColors.Add(new Dictionary<GridColumn, Color>());
            backgroundColors.Add(new Dictionary<GridColumn, Color>());
            
            DataStore = list;
        }

        /// <summary>
        /// (api:eto=2.4.2) 添加多行
        /// </summary>
        /// <param name="rowsValues">每行的初始文字，元素为null则该行默认为空</param>
        public void AddRows(List<String[]> rowsValues)
        {
            if (Columns.Count == 0) return;
            if (rowsValues == null || rowsValues.Count == 0) return;

            if (DataStore == null) DataStore = new List<GridItem>();
            else if (!(DataStore is List<GridItem>)) return;

            var list = DataStore as List<GridItem>;
            foreach (var values in rowsValues)
            {
                var valuesToAdd = new String[Columns.Count];
                int copyCount = values == null ? 0 : Math.Min(values.Length, valuesToAdd.Length);
                for (int i = 0; i < copyCount; i++)
                {
                    valuesToAdd[i] = values[i] == null ? "" : values[i];
                }
                for (int i = copyCount; i < valuesToAdd.Length; i++)
                {
                    valuesToAdd[i] = "";
                }
                list.Add(new GridItem(valuesToAdd));
                foregroundColors.Add(new Dictionary<GridColumn, Color>());
                backgroundColors.Add(new Dictionary<GridColumn, Color>());
            }
            
            DataStore = list;
        }

        /// <summary>
        /// 移除一行
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        public void RemoveRow(int rowIndex)
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;
            

            var list = DataStore as List<GridItem>;
            if (rowIndex < 0 || rowIndex >= list.Count) return;

            list.RemoveAt(rowIndex);
            foregroundColors.RemoveAt(rowIndex);
            backgroundColors.RemoveAt(rowIndex);

            DataStore = list;
        }

        /// <summary>
        /// (api:eto=2.4.2) 移除多行
        /// </summary>
        /// <param name="rowIndices">各行序号</param>
        public void RemoveRows(int[] rowIndices)
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;
            if (rowIndices == null || rowIndices.Length == 0) return;
    
            var flags = new Dictionary<int, bool>();
            foreach (var index in rowIndices) flags[index] = true;
            var sortedIndices = flags.Keys.ToList();
            sortedIndices.Sort();
            sortedIndices.Reverse();

            var list = DataStore as List<GridItem>;
            foreach (var rowIndex in sortedIndices)
            {
                if (rowIndex >= 0 && rowIndex < list.Count)
                {
                    list.RemoveAt(rowIndex);
                    foregroundColors.RemoveAt(rowIndex);
                    backgroundColors.RemoveAt(rowIndex);
                }
            }

            DataStore = list;
        }

        /// <summary>
        /// (api:eto=2.0.10) 移除所有行
        /// </summary>
        public void RemoveAllRows()
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;

            var list = DataStore as List<GridItem>;
            list.Clear();
            foregroundColors.Clear();
            backgroundColors.Clear();

            DataStore = list;
        }

        /// <summary>
        /// 获取某格文字
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="columnIndex">列序号</param>
        /// <returns>该格文字</returns>
        public String GetValue(int rowIndex, int columnIndex)
        {
            if (DataStore == null) return null;
            if (!(DataStore is List<GridItem>)) return null;

            var list = DataStore as List<GridItem>;
            if (rowIndex < 0 || rowIndex >= list.Count) return null;

            var values = list[rowIndex].Values;
            if (values == null || columnIndex < 0 || columnIndex >= values.Length) return null;

            return values[columnIndex].ToString();
        }

        /// <summary>
        /// 设置某格文字
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="columnIndex">列序号</param>
        /// <param name="val">该格文字</param>
        public void SetValue(int rowIndex, int columnIndex, String val)
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;

            var list = DataStore as List<GridItem>;
            if (rowIndex < 0 || rowIndex >= list.Count) return;

            var values = list[rowIndex].Values;
            if (values == null || columnIndex < 0 || columnIndex >= values.Length) return;

            if (val == null) val = "";
            values[columnIndex] = val;

            DataStore = list;
        }

        /// <summary>
        /// (api:eto=2.4.3) 设置某格文字颜色
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="columnIndex">列序号</param>
        /// <param name="color">文字颜色</param>
        public void SetTextColor(int rowIndex, int columnIndex, Color color)
        {
            if (rowIndex < 0 || rowIndex >= foregroundColors.Count || columnIndex < 0 || columnIndex >= Columns.Count) return;
            foregroundColors[rowIndex][Columns[columnIndex]] = color;
            updateColor(rowIndex, columnIndex);
        }

        /// <summary>
        /// (api:eto=2.4.3) 设置某格背景颜色
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="columnIndex">列序号</param>
        /// <param name="color">背景颜色</param>
        public void SetBackgroundColor(int rowIndex, int columnIndex, Color color)
        {
            if (rowIndex < 0 || rowIndex >= GetRowCount() || columnIndex < 0 || columnIndex >= Columns.Count) return;
            backgroundColors[rowIndex][Columns[columnIndex]] = color;
            updateColor(rowIndex, columnIndex);
        }

        private void TextTableView_CellFormatting(object sender, GridCellFormatEventArgs e)
        {
            if (e.Row < foregroundColors.Count)
            {
                var rowTable = foregroundColors[e.Row];
                if (rowTable.ContainsKey(e.Column)) e.ForegroundColor = rowTable[e.Column];
                else if (DefaultTextColor != null) e.ForegroundColor = DefaultTextColor.Value;
            }
            if (e.Row < backgroundColors.Count)
            {
                var rowTable = backgroundColors[e.Row];
                if (rowTable.ContainsKey(e.Column)) e.BackgroundColor = rowTable[e.Column];
                else if (DefaultBackgroundColor != null) e.BackgroundColor = DefaultBackgroundColor.Value;
            }
        }

        private void timer_Elapsed(object sender, EventArgs e)
        {
            timer.Stop();
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

        private List<Dictionary<GridColumn, Color>> foregroundColors = new List<Dictionary<GridColumn, Color>>();
        private List<Dictionary<GridColumn, Color>> backgroundColors = new List<Dictionary<GridColumn, Color>>();
        private UITimer timer = null;

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