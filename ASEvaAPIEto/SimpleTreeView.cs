using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.10.0) 简易树状视图
    /// </summary>
    public class SimpleTreeView : Panel, SimpleTreeViewCallback
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SimpleTreeView()
        {
            if (Factory == null) Factory = new DefaultSimpleTreeViewFactory();

            Control etoControl = null;
            Factory.CreateSimpleTreeViewBackend(this, out etoControl, out backend);
            if (etoControl != null) Content = etoControl;
        }

        /// <summary>
        /// 设置树状视图内容
        /// </summary>
        /// <param name="rootNodes">内容的根节点集合</param>
        /// <param name="sort">是否对条目排序</param>
        public void SetModel(SimpleTreeNode[] rootNodes, bool sort)
        {
            if (backend != null) backend.SetModel(rootNodes, sort);
        }

        /// <summary>
        /// 批量更新内容节点
        /// </summary>
        /// <param name="tasks">更新内容节点的任务</param>
        public void UpdateNodes(SimpleTreeNodeUpdateTask[] tasks)
        {
            if (backend != null) backend.UpdateNodes(tasks);
        }

        /// <summary>
        /// 获取当前选中条目的键对象
        /// </summary>
        public object GetSelectedKey()
        {
            return backend == null ? null : backend.GetSelectedKey();
        }

        /// <summary>
        /// (api:eto=2.10.1) 选中键对象对应的条目
        /// </summary>
        /// <param name="key">键对象</param>
        public void SelectItem(object key)
        {
            if (backend != null) backend.SelectItem(key);
        }

        /// <summary>
        /// 选中条目切换事件
        /// </summary>
        public event EventHandler SelectedItemChanged;

        /// <summary>
        /// 选中条目双击或回车事件
        /// </summary>
        public event EventHandler SelectedItemActivated;

        public void OnSelectedItemChanged()
        {
            if (SelectedItemChanged != null) SelectedItemChanged(this, null);
        }

        public void OnSelectedItemActivated()
        {
            if (SelectedItemActivated != null) SelectedItemActivated(this, null);
        }

        public static SimpleTreeViewFactory Factory { private get; set; }

		private SimpleTreeViewBackend backend;
    }

    /// <summary>
    /// (api:eto=2.10.0) 简易树状视图的内容节点
    /// </summary>
    public class SimpleTreeNode
    {
        /// <summary>
        /// 条目的键对象
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// 显示文本内容
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// 文本颜色，Colors.Transparent表示使用默认颜色
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// 条目背景颜色，Colors.Transparent表示使用默认颜色
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<SimpleTreeNode> ChildNodes { get { return childNodes; } }

        /// <summary>
        /// 子节点是否展开显示
        /// </summary>
        public bool ChildNodesExpanded { get; set; }

        private List<SimpleTreeNode> childNodes = new List<SimpleTreeNode>();
    }

    /// <summary>
    /// (api:eto=2.10.0) 更新简易树状视图内容节点的任务
    /// </summary>
    public class SimpleTreeNodeUpdateTask
    {
        /// <summary>
        /// 条目的键对象
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// 文本颜色，null表示不更新，Colors.Transparent表示设置为默认颜色
        /// </summary>
        public Color? TextColor { get; set; }

        /// <summary>
        /// 条目背景颜色，null表示不更新，Colors.Transparent表示设置为默认颜色
        /// </summary>
        public Color? BackgroundColor { get; set; }
    }

	public interface SimpleTreeViewCallback
	{
        void OnSelectedItemChanged();
        void OnSelectedItemActivated();
	}

	public interface SimpleTreeViewBackend
	{
        void SetModel(SimpleTreeNode[] rootNodes, bool sort);
        void UpdateNodes(SimpleTreeNodeUpdateTask[] tasks);
        object GetSelectedKey();
        void SelectItem(object key);
	}

	public interface SimpleTreeViewFactory
	{
		void CreateSimpleTreeViewBackend(SimpleTreeViewCallback callback, out Control etoControl, out SimpleTreeViewBackend backend);
	}

    public class DefaultSimpleTreeViewFactory : SimpleTreeViewFactory
    {
        public void CreateSimpleTreeViewBackend(SimpleTreeViewCallback callback, out Control etoControl, out SimpleTreeViewBackend backend)
        {
            var control = new DefaultSimpleTreeViewBackend(callback);
            etoControl = control;
            backend = control;
        }
    }

    class DefaultSimpleTreeViewBackend : TreeGridView, SimpleTreeViewBackend
    {
        public DefaultSimpleTreeViewBackend(SimpleTreeViewCallback callback)
        {
            this.callback = callback;

            ShowHeader = false;

            column = new GridColumn{ DataCell = new TextBoxCell(0), Width = this.Sizer(100) };
            Columns.Add(column);

            SelectedItemChanged += delegate
            {
                callback.OnSelectedItemChanged();
            };

            Activated += delegate
            {
                callback.OnSelectedItemActivated();
            };

            CellFormatting += (o, e) =>
            {
                var node = (e.Item as TreeGridItem).Tag as SimpleTreeNode;
                if (node.TextColor != Colors.Transparent) e.ForegroundColor = node.TextColor;
                else e.ForegroundColor = SystemColors.ControlText;
                if (node.BackgroundColor != Colors.Transparent) e.BackgroundColor = node.BackgroundColor;
                else e.BackgroundColor = SystemColors.ControlBackground;
            };

            SizeChanged += delegate
            {
                var width = this.GetLogicalWidth();
                if (width > 30)
                {
                    var targetWidth = this.Sizer(width - 20);
                    if (column.Width != targetWidth)
                    {
                        if (setWidthTimer != null)
                        {
                            setWidthTimer.Stop();
                            setWidthTimer = null;
                        }
                        setWidthTimer = new UITimer();
                        setWidthTimer.Interval = 0.05;
                        setWidthTimer.Elapsed += delegate
                        {
                            setWidthTimer.Stop();
                            setWidthTimer = null;
                            column.Width = targetWidth;
                        };
                        setWidthTimer.Start();
                    }
                }
            };
        }

        public void SetModel(SimpleTreeNode[] rootNodes, bool sort)
        {
            DataStore = null;
            nodeMap.Clear();
            itemMap.Clear();

            var model = new TreeGridItemCollection();
            if(rootNodes != null)
            {
                foreach (var node in rootNodes)
                {
                    if (node.Key == null || nodeMap.ContainsKey(node.Key)) continue;
                    if (String.IsNullOrEmpty(node.Text)) continue;

                    var rootItem = new TreeGridItem{ Tag = node, Values = new String[] { node.Text } };
                    nodeMap[node.Key] = node;
                    itemMap[node.Key] = rootItem;
                    if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                    {
                        addChildNodes(rootItem, node.ChildNodes, sort);
                        if (rootItem.Children.Count > 0)
                        {
                            if (node.ChildNodesExpanded) rootItem.Expanded = true;
                            if (sort) rootItem.Children.Sort(sortFunc);
                        }
                    }
                    model.Add(rootItem);
                }
            }

            DataStore = model;
        }

        public void UpdateNodes(SimpleTreeNodeUpdateTask[] tasks)
        {
            if (tasks == null || tasks.Length == 0) return;

            bool updated = false;
            foreach (var task in tasks)
            {
                if (task.Key == null || !nodeMap.ContainsKey(task.Key)) continue;

                var node = nodeMap[task.Key];
                if (task.TextColor != null && task.TextColor != node.TextColor)
                {
                    node.TextColor = task.TextColor.Value;
                    updated = true;
                }
                if (task.BackgroundColor != null && task.BackgroundColor != node.BackgroundColor)
                {
                    node.BackgroundColor = task.BackgroundColor.Value;
                    updated = true;
                }
            }

            if (updated) ReloadData();
        }

        public object GetSelectedKey()
        {
            if (SelectedItem == null) return null;
            else return ((SelectedItem as TreeGridItem).Tag as SimpleTreeNode).Key;
        }

        public void SelectItem(object key)
        {
            if (key != null && itemMap.ContainsKey(key)) SelectedItem = itemMap[key];
        }

        private void addChildNodes(TreeGridItem parentItem, List<SimpleTreeNode> childNodes, bool sort)
        {
            foreach (var node in childNodes)
            {
                if (nodeMap.ContainsKey(node.Key)) continue;
                if (String.IsNullOrEmpty(node.Text)) continue;

                var childItem = new TreeGridItem{ Tag = node, Values = new String[] { node.Text } };
                nodeMap[node.Key] = node;
                itemMap[node.Key] = childItem;
                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    addChildNodes(childItem, node.ChildNodes, sort);
                    if (childItem.Children.Count > 0)
                    {
                        if (node.ChildNodesExpanded) childItem.Expanded = true;
                        if (sort) childItem.Children.Sort(sortFunc);
                    }
                }
                parentItem.Children.Add(childItem);
            }
        }

        private int sortFunc(ITreeGridItem x, ITreeGridItem y)
        {
            var xs = (x as TreeGridItem).GetValue(0) as String;
            var ys = (y as TreeGridItem).GetValue(0) as String;
            return xs.CompareTo(ys);
        }

        private SimpleTreeViewCallback callback;
        private GridColumn column;
        private Dictionary<object, SimpleTreeNode> nodeMap = new Dictionary<object, SimpleTreeNode>();
        private Dictionary<object, TreeGridItem> itemMap = new Dictionary<object, TreeGridItem>();
        private UITimer setWidthTimer;
    }
}