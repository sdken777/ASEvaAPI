using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ASEva;
using ASEva.UIEto;
using Eto.WinForms;

namespace ASEva.UICoreWF
{
    class SimpleTreeViewBackendCoreWF : TreeView, SimpleTreeViewBackend
    {
        public SimpleTreeViewBackendCoreWF(SimpleTreeViewCallback callback)
        {
            this.callback = callback;

            HideSelection = false;

            AfterSelect += treeView_AfterSelect;
            MouseDoubleClick += treeView_MouseDoubleClick;
        }

        public object? GetSelectedKey()
        {
            var target = SelectedNode;
            if (target == null || !treeNodeMap.ContainsKey(target)) return null;

            return treeNodeMap[target].Common.Key;
        }

        public void SetModel(SimpleTreeNode[] rootNodes, bool sort)
        {
            SelectedNode = null;
            Nodes.Clear();

            if (rootNodes == null || rootNodes.Length == 0) return;

            var expandedItemKeys = new List<object>();
            foreach (var node in rootNodes)
            {
                if (nodeKeyMap.ContainsKey(node.Key)) continue;
                if (String.IsNullOrEmpty(node.Text)) continue;

                var textColor = node.TextColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlText : node.TextColor;
                var backColor = node.BackgroundColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlBackground : node.BackgroundColor;
                var treeNode = new TreeNode(node.Text);
                treeNode.ForeColor = textColor.ToSD();
                treeNode.BackColor = backColor.ToSD();

                var nodeCoreWF = new SimpleTreeNodeCoreWF(node, treeNode);

                nodeKeyMap[node.Key] = nodeCoreWF;
                treeNodeMap[treeNode] = nodeCoreWF;

                if (node.ChildNodes.Count > 0)
                {
                    if (node.ChildNodesExpanded) expandedItemKeys.Add(node.Key);
                    addChildNodes(treeNode, node.ChildNodes, expandedItemKeys);
                }

                Nodes.Add(treeNode);
            }

            if (sort) Sort();

            foreach (var key in expandedItemKeys)
            {
                nodeKeyMap[key].TreeNode.Expand();
            }
        }

        public void UpdateNodes(SimpleTreeNodeUpdateTask[] tasks)
        {
            if (tasks.Length == 0) return;

            foreach (var task in tasks)
            {
                if (!nodeKeyMap.ContainsKey(task.Key)) continue;

                var node = nodeKeyMap[task.Key];
                if (task.TextColor != null && task.TextColor.Value != node.Common.TextColor)
                {
                    node.TreeNode.ForeColor = task.TextColor.Value.ToSD();
                    node.Common.TextColor = task.TextColor.Value;
                }
                if (task.BackgroundColor != null && task.BackgroundColor.Value != node.Common.BackgroundColor)
                {
                    node.TreeNode.BackColor = task.BackgroundColor.Value.ToSD();
                    node.Common.BackgroundColor = task.BackgroundColor.Value;
                }
            }
        }

        public void SelectItem(object key)
        {
            if (nodeKeyMap.ContainsKey(key))
            {
                SelectedNode = nodeKeyMap[key].TreeNode;
            }
        }

        private void treeView_AfterSelect(object? sender, EventArgs e)
        {
            callback.OnSelectedItemChanged();
        }

        private void treeView_MouseDoubleClick(object? sender, EventArgs e)
        {
            callback.OnSelectedItemActivated();
        }

        private void addChildNodes(TreeNode parentNode, List<SimpleTreeNode> childNodes, List<object> expandedItemKeys)
        {
            foreach (var node in childNodes)
            {
                if (nodeKeyMap.ContainsKey(node.Key)) continue;
                if (String.IsNullOrEmpty(node.Text)) continue;

                var textColor = node.TextColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlText : node.TextColor;
                var backColor = node.BackgroundColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlBackground : node.BackgroundColor;
                var treeNode = new TreeNode(node.Text);
                treeNode.ForeColor = textColor.ToSD();
                treeNode.BackColor = backColor.ToSD();

                var nodeCoreWF = new SimpleTreeNodeCoreWF(node, treeNode);

                nodeKeyMap[node.Key] = nodeCoreWF;
                treeNodeMap[treeNode] = nodeCoreWF;

                if (node.ChildNodes.Count > 0)
                {
                    if (node.ChildNodesExpanded) expandedItemKeys.Add(node.Key);
                    addChildNodes(treeNode, node.ChildNodes, expandedItemKeys);
                }

                parentNode.Nodes.Add(treeNode);
            }
        }

        private class SimpleTreeNodeCoreWF(SimpleTreeNode common, TreeNode treeNode)
        {
            public SimpleTreeNode Common { get; set; } = common;
            public TreeNode TreeNode { get; set; } = treeNode;
        }

        private SimpleTreeViewCallback callback;
        private Dictionary<object, SimpleTreeNodeCoreWF> nodeKeyMap = new Dictionary<object, SimpleTreeNodeCoreWF>();
        private Dictionary<TreeNode, SimpleTreeNodeCoreWF> treeNodeMap = new Dictionary<TreeNode, SimpleTreeNodeCoreWF>();
    }
}
