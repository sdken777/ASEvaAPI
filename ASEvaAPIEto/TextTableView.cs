using System;
using System.Collections.Generic;
using Eto.Forms;

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
            
            DataStore = list;
            SelectRow(list.Count - 1);
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
            if (rowIndex >= 0 && rowIndex < list.Count) list.RemoveAt(rowIndex);

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
    }
}