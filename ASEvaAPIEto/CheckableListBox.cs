using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.5.0) 多选框组
    /// </summary>
    public class CheckableListBox : GridView
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckableListBox()
        {
            ShowHeader = false;
            if (DefaultBackgroundColor != null) BackgroundColor = DefaultBackgroundColor.Value;
            Columns.Add(new GridColumn { DataCell = new CheckBoxCell(1), Resizable = false, Width = this.Sizer(22) });
            Columns.Add(new GridColumn { DataCell = new TextBoxCell(0), Resizable = false, });
            CellClick += CheckableListBox_CellClick;
            CellDoubleClick += CheckableListBox_CellClick;
            CellFormatting += CheckableListBox_CellFormatting;
            SizeChanged += CheckableListBox_SizeChanged;
            Shown += CheckableListBox_Shown;
        }

        /// <summary>
        /// 获取多选框个数
        /// </summary>
        /// <returns>多选框个数</returns>
        public int GetItemCount()
        {
            if (DataStore == null) return 0;
            if (!(DataStore is List<GridItem>)) return 0;
            return (DataStore as List<GridItem>).Count;
        }

        /// <summary>
        /// 添加多选框
        /// </summary>
        /// <param name="text">显示文字</param>
        /// <param name="isChecked">初始是否为勾选状态</param>
        /// <param name="isEnabled">初始是否为启用状态</param>
        public void AddItem(String text, bool isChecked = false, bool isEnabled = true)
        {
            if (DataStore == null) DataStore = new List<GridItem>();
            else if (!(DataStore is List<GridItem>)) return;

            if (text == null) text = "";

            var list = DataStore as List<GridItem>;
            list.Add(new GridItem(new object[] { text, isChecked }));
            enableFlags.Add(isEnabled);
            
            DataStore = list;
        }

        /// <summary>
        /// 一次性添加多个多选框
        /// </summary>
        /// <param name="itemsText">各多选框的显示文字</param>
        /// <param name="itemsChecked">各多选框的初始勾选状态，若为null则全部为未勾选，若非null则数组大小应与itemsText一致</param>
        /// <param name="itemsEnabled">各多选框的初始启用状态，若为null则全部启用，若非null则数组大小应与itemsText一致</param>
        public void AddItems(String[] itemsText, bool[] itemsChecked = null, bool[] itemsEnabled = null)
        {
            if (itemsText == null || itemsText.Length == 0) return;

            if (itemsChecked != null && itemsChecked.Length != itemsText.Length) return;
            if (itemsEnabled != null && itemsEnabled.Length != itemsText.Length) return;

            if (DataStore == null) DataStore = new List<GridItem>();
            else if (!(DataStore is List<GridItem>)) return;

            var list = DataStore as List<GridItem>;
            for (int i = 0; i < itemsText.Length; i++)
            {
                list.Add(new GridItem(new object[] { itemsText[i], itemsChecked == null ? false : itemsChecked[i] }));
                enableFlags.Add(itemsEnabled == null ? true : itemsEnabled[i]);
            }
            
            DataStore = list;
        }

        /// <summary>
        /// 移除多选框
        /// </summary>
        /// <param name="index">要移除的多选框的当前序号</param>
        public void RemoveItem(int index)
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;
            

            var list = DataStore as List<GridItem>;
            if (index < 0 || index >= list.Count) return;

            list.RemoveAt(index);
            enableFlags.RemoveAt(index);

            DataStore = list;
        }

        /// <summary>
        /// 一次性移除多个多选框
        /// </summary>
        /// <param name="indices">要移除的所有多选框的当前序号</param>
        public void RemoveItems(int[] indices)
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;
            if (indices == null || indices.Length == 0) return;
    
            var flags = new Dictionary<int, bool>();
            foreach (var index in indices) flags[index] = true;
            var sortedIndices = flags.Keys.ToList();
            sortedIndices.Sort();
            sortedIndices.Reverse();

            var list = DataStore as List<GridItem>;
            foreach (var rowIndex in sortedIndices)
            {
                if (rowIndex >= 0 && rowIndex < list.Count)
                {
                    list.RemoveAt(rowIndex);
                    enableFlags.RemoveAt(rowIndex);
                }
            }

            DataStore = list;
        }

        /// <summary>
        /// 移除所有多选框
        /// </summary>
        public void RemoveAllItems()
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;

            var list = DataStore as List<GridItem>;
            list.Clear();
            enableFlags.Clear();

            DataStore = list;
        }

        /// <summary>
        /// 获取某个多选框是否为勾选状态
        /// </summary>
        /// <param name="index">多选框的当前序号</param>
        /// <returns>是否勾选</returns>
        public bool GetChecked(int index)
        {
            if (DataStore == null) return false;
            if (!(DataStore is List<GridItem>)) return false;

            var list = DataStore as List<GridItem>;
            if (index < 0 || index >= list.Count) return false;

            var values = list[index].Values;
            if (values == null || values.Length < 2) return false;

            return (bool)values[1];
        }

        /// <summary>
        /// 设置某个多选框的勾选状态
        /// </summary>
        /// <param name="index">多选框的当前序号</param>
        /// <param name="isChecked">是否否选</param>
        public void SetChecked(int index, bool isChecked)
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;

            var list = DataStore as List<GridItem>;
            if (index < 0 || index >= list.Count) return;

            var values = list[index].Values;
            if (values == null || values.Length < 2) return;

            UnselectAll();

            values[1] = isChecked;
            ReloadData(index);
        }

        /// <summary>
        /// 获取某个多选框是否为启用状态
        /// </summary>
        /// <param name="index">多选框的当前序号</param>
        /// <returns>是否启用</returns>
        public bool GetEnabled(int index)
        {
            if (index < 0 || index >= enableFlags.Count) return false;
            else return enableFlags[index];
        }

        /// <summary>
        /// 设置某个多选框的启用状态
        /// </summary>
        /// <param name="index">多选框的当前序号</param>
        /// <param name="isEnabled">是否启用</param>
        public void SetEnabled(int index, bool isEnabled)
        {
            if (index < 0 || index >= enableFlags.Count) return;
            if (enableFlags[index] == isEnabled) return;

            enableFlags[index] = isEnabled;
            updateColor(index, 1);
        }

        /// <summary>
        /// (api:eto=2.8.9) 设置某个多选框的文字
        /// </summary>
        /// <param name="index">多选框的当前序号</param>
        /// <param name="text">多选框的文字</param>
        public void SetText(int index, String text)
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;

            var list = DataStore as List<GridItem>;
            if (index < 0 || index >= list.Count) return;

            var values = list[index].Values;
            if (values == null || values.Length < 2) return;

            UnselectAll();

            values[0] = text;
            ReloadData(index);
        }

        /// <summary>
        /// 获取所有已勾选的多选框的序号
        /// </summary>
        /// <returns>所有已勾选的多选框的序号</returns>
        public int[] GetCheckedIndices()
        {
            var buffer = new List<int>();

            if (DataStore == null) return buffer.ToArray();
            if (!(DataStore is List<GridItem>)) return buffer.ToArray();

            var list = (DataStore as List<GridItem>).ToArray();
            for (int i = 0; i < list.Length; i++)
            {
                var values = list[i].Values;
                if (values == null || values.Length < 2) continue;
                if ((bool)values[1]) buffer.Add(i);
            }
            return buffer.ToArray();
        }

        /// <summary>
        /// 勾选所有多选框（不改变禁用的部分）
        /// </summary>
        public void CheckAll()
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;

            int index = 0;
            foreach (var item in DataStore as List<GridItem>)
            {
                var curIndex = index++;
                var values = item.Values;
                if (values == null || values.Length < 2) continue;
                if (curIndex >= enableFlags.Count || !enableFlags[curIndex]) continue;
                values[1] = true;
            }
            ReloadData(new Range<int>(0, GetItemCount()));
        }

        /// <summary>
        /// 取消勾选所有多选框（不改变禁用的部分）
        /// </summary>
        public void UncheckAll()
        {
            if (DataStore == null) return;
            if (!(DataStore is List<GridItem>)) return;

            int index = 0;
            foreach (var item in DataStore as List<GridItem>)
            {
                var curIndex = index++;
                var values = item.Values;
                if (values == null || values.Length < 2) continue;
                if (curIndex >= enableFlags.Count || !enableFlags[curIndex]) continue;
                values[1] = false;
            }
            ReloadData(new Range<int>(0, GetItemCount()));
        }

        /// <summary>
        /// (api:eto=2.9.13) 多选框点击事件
        /// </summary>
        public event EventHandler ItemClicked;

        private void CheckableListBox_CellClick(object sender, GridCellMouseEventArgs e)
        {
            if (e.Row >= 0 && e.Row < enableFlags.Count && enableFlags[e.Row])
            {
                if (DataStore == null) return;
                if (!(DataStore is List<GridItem>)) return;
                
                var list = DataStore as List<GridItem>;
                if (e.Row >= list.Count) return;

                var values = list[e.Row].Values;
                if (values == null || values.Length < 2) return;

                values[1] = !(bool)values[1];
                ReloadData(e.Row);

                SelectRow(e.Row);

                if (ItemClicked != null)
                {
                    if (clickTimer != null)
                    {
                        clickTimer.Stop();
                        clickTimer = null;
                    }
                    clickTimer = new UITimer();
                    clickTimer.Interval = 0.05;
                    clickTimer.Elapsed += delegate
                    {
                        clickTimer.Stop();
                        clickTimer = null;
                        ItemClicked(this, null);
                    };
                    clickTimer.Start();
                }
            }
        }

        private void CheckableListBox_CellFormatting(object sender, GridCellFormatEventArgs e)
        {
            if (e.Row >= 0 && e.Row < enableFlags.Count)
            {
                e.ForegroundColor = enableFlags[e.Row] ? Colors.Black : Colors.LightGrey;
            }
        }

        private void CheckableListBox_Shown(object sender, EventArgs e)
        {
            updateColumnWidth();
        }

        private void CheckableListBox_SizeChanged(object sender, EventArgs e)
        {
            updateColumnWidth();
        }

        private void updateColumnWidth()
        {
            Columns[1].Width = this.Sizer(this.GetLogicalWidth() - 45);
        }

        private void updateColor(int rowIndex, int columnIndex)
        {
            if (UpdateColorMode == InvalidateMode.DelayedInvalidate)
            {
                if (colorTimer == null)
                {
                    colorTimer = new UITimer();
                    colorTimer.Interval = 0.01;
                    colorTimer.Elapsed += delegate
                    {
                        colorTimer.Stop();
                        colorTimer = null;
                        Invalidate();
                    };
                    colorTimer.Start();
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

        private List<bool> enableFlags = new List<bool>();
        private UITimer colorTimer = null, clickTimer = null;

        public enum InvalidateMode
        {
            DelayedInvalidate = 0,
            EditCell,
        }

        public static InvalidateMode UpdateColorMode { private get; set; }
        public static Color? DefaultBackgroundColor { private get; set; }
    }
}