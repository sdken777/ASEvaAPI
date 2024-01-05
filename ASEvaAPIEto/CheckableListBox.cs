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
    /// (api:eto=2.13.1) List box of checkboxes
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.13.1) 多选框组
    /// </summary>
    public class CheckableListBox : Panel, CheckableListBoxCallback
    {
        /// \~English
        /// <summary>
        /// Constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckableListBox()
        {
            if (Factory == null) Factory = new DefaultCheckableListBoxFactory();

            Control etoControl = null;
            Factory.CreateCheckableListBoxBackend(this, out etoControl, out backend);
            if (etoControl != null) Content = etoControl;
        }

        /// \~English
        /// <summary>
        /// Get number of check boxes
        /// </summary>
        /// <returns>多选框个数</returns>
        /// \~Chinese
        /// <summary>
        /// 获取多选框个数
        /// </summary>
        /// <returns>多选框个数</returns>
        public int GetItemCount()
        {
            return enableFlags.Count;
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
        /// Add check box
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="isChecked">Initial check state</param>
        /// <param name="isEnabled">Initial enable state</param>
        /// \~Chinese
        /// <summary>
        /// 添加多选框
        /// </summary>
        /// <param name="text">显示文字</param>
        /// <param name="isChecked">初始是否为勾选状态</param>
        /// <param name="isEnabled">初始是否为启用状态</param>
        public void AddItem(String text, bool isChecked = false, bool isEnabled = true)
        {
            enableFlags.Add(isEnabled);
            backend.AddItems(new String[]{ text }, new bool[]{ isChecked }, new bool[]{ isEnabled });
        }

        /// \~English
        /// <summary>
        /// Add check boxes at once
        /// </summary>
        /// <param name="itemsText">Text of each check box</param>
        /// <param name="itemsChecked">Initial check state of each check box. "null" indicated all are unchecked. If it's not null, you should guarantee the size is the same</param>
        /// <param name="itemsEnabled">Initial enable state of each check box. "null" indicated all are enabled. If it's not null, you should guarantee the size is the same</param>
        /// \~Chinese
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

            for (int i = 0; i < itemsText.Length; i++)
            {
                if (itemsText[i] == null) itemsText[i] = "";
                enableFlags.Add(itemsEnabled == null ? true : itemsEnabled[i]);
            }

            backend.AddItems(itemsText, itemsChecked, itemsEnabled);
        }

        /// \~English
        /// <summary>
        /// Remove check box
        /// </summary>
        /// <param name="index">The index of check box to remove</param>
        /// \~Chinese
        /// <summary>
        /// 移除多选框
        /// </summary>
        /// <param name="index">要移除的多选框的当前序号</param>
        public void RemoveItem(int index)
        {
            if (index < 0 || index >= enableFlags.Count) return;
            enableFlags.RemoveAt(index);
            backend.RemoveItems(new int[]{ index });
        }

        /// \~English
        /// <summary>
        /// Remove check boxes at once
        /// </summary>
        /// <param name="indices">The indices of check boxes to remove</param>
        /// \~Chinese
        /// <summary>
        /// 一次性移除多个多选框
        /// </summary>
        /// <param name="indices">要移除的所有多选框的当前序号</param>
        public void RemoveItems(int[] indices)
        {
            if (indices == null || indices.Length == 0) return;

            var flags = new Dictionary<int, bool>();
            foreach (var index in indices)
            {
                if (index < 0 || index >= enableFlags.Count) continue;
                flags[index] = true;
            }
            var sortedIndices = flags.Keys.ToList();
            if (sortedIndices.Count == 0) return;

            sortedIndices.Sort();
            sortedIndices.Reverse();

            foreach (var index in sortedIndices)
            {
                enableFlags.RemoveAt(index);
            }

            backend.RemoveItems(sortedIndices.ToArray());
        }

        /// \~English
        /// <summary>
        /// Remove all check boxes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 移除所有多选框
        /// </summary>
        public void RemoveAllItems()
        {
            enableFlags.Clear();
            backend.RemoveAllItems();
        }

        /// \~English
        /// <summary>
        /// Get check status of check box
        /// </summary>
        /// <param name="index">Index of check box</param>
        /// <returns>Whether it's checked</returns>
        /// \~Chinese
        /// <summary>
        /// 获取某个多选框是否为勾选状态
        /// </summary>
        /// <param name="index">多选框的当前序号</param>
        /// <returns>是否勾选</returns>
        public bool GetChecked(int index)
        {
            if (index < 0 || index >= enableFlags.Count) return false;
            return backend.GetChecked(index);
        }

        /// \~English
        /// <summary>
        /// Set check status of check box, no matter whether it's enabled
        /// </summary>
        /// <param name="index">Index of check box</param>
        /// <param name="isChecked">Whether it's checked</param>
        /// \~Chinese
        /// <summary>
        /// 设置某个多选框的勾选状态，无论该选项是否启用
        /// </summary>
        /// <param name="index">多选框的当前序号</param>
        /// <param name="isChecked">是否否选</param>
        public void SetChecked(int index, bool isChecked)
        {
            if (index < 0 || index >= enableFlags.Count) return;
            backend.SetChecked(new int[]{ index }, isChecked);
        }

        /// \~English
        /// <summary>
        /// Get enable status of check box
        /// </summary>
        /// <param name="index">Index of check box</param>
        /// <returns>Whether it's enabled</returns>
        /// \~Chinese
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

        /// \~English
        /// <summary>
        /// Set enable status of check box
        /// </summary>
        /// <param name="index">Index of check box</param>
        /// <param name="isEnabled">Whether it's enabled</param>
        /// \~Chinese
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
            backend.SetEnabled(index, isEnabled);
        }

        /// \~English
        /// <summary>
        /// Set text of check box
        /// </summary>
        /// <param name="index">Index of check box</param>
        /// <param name="text">Text of check box</param>
        /// \~Chinese
        /// <summary>
        /// 设置某个多选框的文字
        /// </summary>
        /// <param name="index">多选框的当前序号</param>
        /// <param name="text">多选框的文字</param>
        public void SetText(int index, String text)
        {
            if (index < 0 || index >= enableFlags.Count) return;
            if (text == null) text = "";
            backend.SetText(index, text);
        }

        /// \~English
        /// <summary>
        /// Get all indices of checked check box
        /// </summary>
        /// <returns>All indices of checked check box (Not included disabled ones)</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有已勾选的多选框的序号
        /// </summary>
        /// <returns>所有已勾选的多选框的序号（不包括禁用的部分）</returns>
        public int[] GetCheckedIndices()
        {
            var list = new List<int>();
            foreach (var index in backend.GetCheckedIndices())
            {
                if (enableFlags[index]) list.Add(index);
            }
            return list.ToArray();
        }

        /// \~English
        /// <summary>
        /// Check all items (Not for disabled ones)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 勾选所有多选框（不改变禁用的部分）
        /// </summary>
        public void CheckAll()
        {
            var indices = new List<int>();
            int index = 0;
            foreach (var flag in enableFlags)
            {
                var curIndex = index++;
                if (flag) indices.Add(curIndex);
            }
            if (indices.Count == 0) return;

            backend.SetChecked(indices.ToArray(), true);
        }

        /// \~English
        /// <summary>
        /// Uncheck all items (Not for disabled ones)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 取消勾选所有多选框（不改变禁用的部分）
        /// </summary>
        public void UncheckAll()
        {
            var indices = new List<int>();
            int index = 0;
            foreach (var flag in enableFlags)
            {
                var curIndex = index++;
                if (flag) indices.Add(curIndex);
            }
            if (indices.Count == 0) return;

            backend.SetChecked(indices.ToArray(), false);
        }

        /// \~English
        /// <summary>
        /// Click event of check box
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 多选框点击事件
        /// </summary>
        public event EventHandler ItemClicked;

        public void OnItemClicked()
        {
            ItemClicked?.Invoke(this, null);
        }

        private List<bool> enableFlags = new List<bool>();

        public static CheckableListBoxFactory Factory { private get; set; }

		private CheckableListBoxBackend backend;
    }

	public interface CheckableListBoxCallback
	{
        void OnItemClicked();
	}

	public interface CheckableListBoxBackend
	{
        void AddItems(String[] itemsText, bool[] itemsChecked, bool[] itemsEnabled); // not empty, length validated, text not null
        void RemoveItems(int[] indices); // validated, not empty, reversed
        void RemoveAllItems();
        bool GetChecked(int index); // validated
        void SetChecked(int[] indices, bool isChecked); // validated, not empty, ordered
        void SetText(int index, String text); // validated, text not null
        void SetEnabled(int index, bool isEnabled); // validated
        int[] GetCheckedIndices();
        int GetSelectedRowIndex();
	}

	public interface CheckableListBoxFactory
	{
		void CreateCheckableListBoxBackend(CheckableListBoxCallback callback, out Control etoControl, out CheckableListBoxBackend backend);
	}

    class DefaultCheckableListBoxFactory : CheckableListBoxFactory
    {
        public void CreateCheckableListBoxBackend(CheckableListBoxCallback callback, out Control etoControl, out CheckableListBoxBackend backend)
        {
            var control = new DefaultCheckableListBoxBackend(callback);
            etoControl = control;
            backend = control;
        }
    }

    public class DefaultCheckableListBoxBackend : GridView, CheckableListBoxBackend
    {
        public DefaultCheckableListBoxBackend(CheckableListBoxCallback callback)
        {
            this.callback = callback;
            ShowHeader = false;
            if (DefaultBackgroundColor != null) BackgroundColor = DefaultBackgroundColor.Value;
            Columns.Add(new GridColumn { DataCell = new CheckBoxCell(1), Resizable = false, Width = this.Sizer(22) });
            Columns.Add(new GridColumn { DataCell = new TextBoxCell(0), Resizable = false, Expand = true });
            CellClick += CheckableListBox_CellClick;
            CellDoubleClick += CheckableListBox_CellClick;
            CellFormatting += CheckableListBox_CellFormatting;
        }

        public void AddItems(String[] itemsText, bool[] itemsChecked, bool[] itemsEnabled)
        {
            if (DataStore == null) DataStore = new List<GridItem>();

            var list = DataStore as List<GridItem>;
            for (int i = 0; i < itemsText.Length; i++)
            {
                list.Add(new GridItem(new object[] { itemsText[i], itemsChecked == null ? false : itemsChecked[i] }));
                enableFlags.Add(itemsEnabled == null ? true : itemsEnabled[i]);
            }
            
            DataStore = list;
        }
        
        public void RemoveItems(int[] indices)
        {
            if (DataStore == null) return;

            var list = DataStore as List<GridItem>;
            foreach (var rowIndex in indices)
            {
                list.RemoveAt(rowIndex);
                enableFlags.RemoveAt(rowIndex);
            }

            DataStore = list;
        }
        
        public void RemoveAllItems()
        {
            if (DataStore == null) return;

            var list = DataStore as List<GridItem>;
            list.Clear();
            enableFlags.Clear();

            DataStore = list;
        }
        
        public bool GetChecked(int index)
        {
            if (DataStore == null) return false;

            var list = DataStore as List<GridItem>;

            var values = list[index].Values;
            if (values == null || values.Length < 2) return false;

            return (bool)values[1];
        }
        
        public void SetChecked(int[] indices, bool isChecked)
        {
            if (DataStore == null) return;

            var list = (DataStore as List<GridItem>).ToArray();

            UnselectAll();

            for (int i = 0; i < indices.Length; i++)
            {
                var values = list[i].Values;
                if (values == null || values.Length < 2) continue;
                values[1] = isChecked;
            }

            ReloadData(new Range<int>(indices[0], indices.Last() + 1));
        }
        
        public void SetText(int index, String text)
        {
            if (DataStore == null) return;

            var list = DataStore as List<GridItem>;

            var values = list[index].Values;
            if (values == null || values.Length < 2) return;

            UnselectAll();

            values[0] = text;
            ReloadData(index);
        }
        
        public void SetEnabled(int index, bool isEnabled)
        {
            enableFlags[index] = isEnabled;
            updateColor(index, 1);
        }
        
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

        public int GetSelectedRowIndex()
        {
            var index = SelectedRow;
            return index < 0 ? -1 : index;
        }

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
                    callback.OnItemClicked();
                };
                clickTimer.Start();
            }
        }

        private void CheckableListBox_CellFormatting(object sender, GridCellFormatEventArgs e)
        {
            if (e.Row >= 0 && e.Row < enableFlags.Count)
            {
                e.ForegroundColor = enableFlags[e.Row] ? Colors.Black : Colors.LightGrey;
            }
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

        private CheckableListBoxCallback callback;
        private UITimer colorTimer = null, clickTimer = null;
        private List<bool> enableFlags = new List<bool>();

        public enum InvalidateMode
        {
            DelayedInvalidate = 0,
            EditCell,
        }

        public static InvalidateMode UpdateColorMode { private get; set; }
        public static Color? DefaultBackgroundColor { private get; set; }
    }
}