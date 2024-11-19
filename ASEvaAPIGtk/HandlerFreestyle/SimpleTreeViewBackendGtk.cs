using System;
using System.Collections.Generic;
using Gtk;
using ASEva;
using ASEva.UIEto;
using UI = Gtk.Builder.ObjectAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649
    class SimpleTreeViewBackendGtk : Box, SimpleTreeViewBackend
    {
        [UI] TreeView? treeView;
        [UI] TreeStore? treeStore;
        [UI] TreeModelSort? treeModel;

        public SimpleTreeViewBackendGtk(SimpleTreeViewCallback callback) : this(new Builder("SimpleTreeViewBackendGtk.glade"), callback)
        {}

        private SimpleTreeViewBackendGtk(Builder builder, SimpleTreeViewCallback callback) : base(builder.GetRawOwnedObject("SimpleTreeViewBackendGtk"))
        {
            builder.Autoconnect(this);

            this.callback = callback;

            var renderer = new CellRendererText();
            treeView?.Columns[0].PackStart(renderer, true);
            treeView?.Columns[0].SetAttributes(renderer, "text", 0, "foreground", 1, "background", 2);

            if (treeView != null)
            {
                treeView.Selection.Changed += treeView_SelectionChanged;
                treeView.RowActivated += treeView_RowActivated;
            }
        }

        public object? GetSelectedKey()
        {
            if (treeView?.Selection == null || treeModel == null) return null;

            TreeIter target;
            if (!treeView.Selection.GetSelected(out target)) return null;

            target = treeModel.ConvertIterToChildIter(target);
            if (!iterMap.ContainsKey(target)) return null;

            return iterMap[target].Common.Key;
        }

        public void SetModel(SimpleTreeNode[] rootNodes, bool sort)
        {
            treeStore?.Clear();
            nodeMap.Clear();
            iterMap.Clear();

            if (rootNodes == null || rootNodes.Length == 0 || treeStore == null) return;

            var expandedItemKeys = new List<object>();
            foreach (var node in rootNodes)
            {
                if (node.Key == null || nodeMap.ContainsKey(node.Key)) continue;
                if (String.IsNullOrEmpty(node.Text)) continue;

                var textColor = node.TextColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlText : node.TextColor;
                var backColor = node.BackgroundColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlBackground : node.BackgroundColor;
                var iter = treeStore.AppendValues(node.Text, rgb(textColor), rgb(backColor));

                var nodeGtk = new SimpleTreeNodeGtk(node);
                nodeGtk.Iter = iter;

                nodeMap[node.Key] = nodeGtk;
                iterMap[iter] = nodeGtk;

                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    if (node.ChildNodesExpanded) expandedItemKeys.Add(node.Key);
                    addChildNodes(iter, node.ChildNodes, expandedItemKeys);
                }
            }

            if (sort)
            {
                treeModel?.SetSortFunc(0, delegate(ITreeModel model, TreeIter a, TreeIter b)
                {
                    string s1 = (string)model.GetValue(a, 0);
                    string s2 = (string)model.GetValue(b, 0);
                    return String.Compare(s1, s2);
                });
                treeModel?.SetSortColumnId(0, SortType.Ascending);
            }
            else
            {
                treeModel?.ResetDefaultSortFunc();
            }

            foreach (var key in expandedItemKeys)
            {
                treeView?.ExpandRow(treeStore.GetPath(nodeMap[key].Iter), false);
            }
        }

        public void UpdateNodes(SimpleTreeNodeUpdateTask[] tasks)
        {
            if (tasks == null || tasks.Length == 0) return;

            foreach (var task in tasks)
            {
                if (task.Key == null || !nodeMap.ContainsKey(task.Key)) continue;

                var node = nodeMap[task.Key];
                if (task.TextColor != null && task.TextColor.Value != node.Common.TextColor)
                {
                    treeStore?.SetValue(node.Iter, 1, rgb(task.TextColor.Value));
                    node.Common.TextColor = task.TextColor.Value;
                }
                if (task.BackgroundColor != null && task.BackgroundColor.Value != node.Common.BackgroundColor)
                {
                    treeStore?.SetValue(node.Iter, 2, rgb(task.BackgroundColor.Value));
                    node.Common.BackgroundColor = task.BackgroundColor.Value;
                }
            }
        }

        public void SelectItem(object key)
        {
            if (key != null && nodeMap.ContainsKey(key) && treeModel != null)
            {
                var targetIter = treeModel.ConvertChildIterToIter(nodeMap[key].Iter);
                treeView?.ExpandToPath(treeModel.GetPath(targetIter));
                treeView?.Selection.SelectIter(targetIter);
            }
        }

        private void treeView_SelectionChanged(object? sender, EventArgs e)
        {
            callback.OnSelectedItemChanged();
        }

        private void treeView_RowActivated(object? sender, EventArgs e)
        {
            callback.OnSelectedItemActivated();
        }

        private void addChildNodes(TreeIter parentIter, List<SimpleTreeNode> childNodes, List<object> expandedItemKeys)
        {
            if (treeStore == null) return;

            foreach (var node in childNodes)
            {
                if (node.Key == null || nodeMap.ContainsKey(node.Key)) continue;
                if (String.IsNullOrEmpty(node.Text)) continue;

                var textColor = node.TextColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlText : node.TextColor;
                var backColor = node.BackgroundColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlBackground : node.BackgroundColor;
                var iter = treeStore.AppendValues(parentIter, node.Text, rgb(textColor), rgb(backColor));

                var nodeGtk = new SimpleTreeNodeGtk(node);
                nodeGtk.Iter = iter;

                nodeMap[node.Key] = nodeGtk;
                iterMap[iter] = nodeGtk;

                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    if (node.ChildNodesExpanded) expandedItemKeys.Add(node.Key);
                    addChildNodes(iter, node.ChildNodes, expandedItemKeys);
                }
            }
        }

        private String rgb(Eto.Drawing.Color color)
        {
            return "rgb(" + color.Rb + "," + color.Gb + "," + color.Bb + ")";
        }

        private class SimpleTreeNodeGtk(SimpleTreeNode common)
        {
            public SimpleTreeNode Common { get; set; } = common;
            public TreeIter Iter { get; set; }
        }

        private SimpleTreeViewCallback callback;
        private Dictionary<object, SimpleTreeNodeGtk> nodeMap = new Dictionary<object, SimpleTreeNodeGtk>();
        private Dictionary<TreeIter, SimpleTreeNodeGtk> iterMap = new Dictionary<TreeIter, SimpleTreeNodeGtk>();
    }
}
