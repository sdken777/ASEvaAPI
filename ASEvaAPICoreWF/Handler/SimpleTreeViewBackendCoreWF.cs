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

        public object GetSelectedKey()
        {
            if (SelectedNode == null) return null;

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
                if (node.Key == null || nodeKeyMap.ContainsKey(node.Key)) continue;
                if (String.IsNullOrEmpty(node.Text)) continue;

                var textColor = node.TextColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlText : node.TextColor;
                var backColor = node.BackgroundColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlBackground : node.BackgroundColor;
                var treeNode = new TreeNode(node.Text);
                treeNode.ForeColor = textColor.ToSD();
                treeNode.BackColor = backColor.ToSD();

                var nodeCoreWF = new SimpleTreeNodeCoreWF();
                nodeCoreWF.Common = node;
                nodeCoreWF.TreeNode = treeNode;

                nodeKeyMap[node.Key] = nodeCoreWF;
                treeNodeMap[treeNode] = nodeCoreWF;

                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
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
            if (tasks == null || tasks.Length == 0) return;

            foreach (var task in tasks)
            {
                if (task.Key == null || !nodeKeyMap.ContainsKey(task.Key)) continue;

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
            if (key != null && nodeKeyMap.ContainsKey(key))
            {
                SelectedNode = nodeKeyMap[key].TreeNode;
            }
        }

        private void treeView_AfterSelect(object sender, EventArgs e)
        {
            if (callback != null) callback.OnSelectedItemChanged();
        }

        private void treeView_MouseDoubleClick(object sender, EventArgs e)
        {
            if (callback != null) callback.OnSelectedItemActivated();
        }

        private void addChildNodes(TreeNode parentNode, List<SimpleTreeNode> childNodes, List<object> expandedItemKeys)
        {
            foreach (var node in childNodes)
            {
                if (node.Key == null || nodeKeyMap.ContainsKey(node.Key)) continue;
                if (String.IsNullOrEmpty(node.Text)) continue;

                var textColor = node.TextColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlText : node.TextColor;
                var backColor = node.BackgroundColor == Eto.Drawing.Colors.Transparent ? Eto.Drawing.SystemColors.ControlBackground : node.BackgroundColor;
                var treeNode = new TreeNode(node.Text);
                treeNode.ForeColor = textColor.ToSD();
                treeNode.BackColor = backColor.ToSD();

                var nodeCoreWF = new SimpleTreeNodeCoreWF();
                nodeCoreWF.Common = node;
                nodeCoreWF.TreeNode = treeNode;

                nodeKeyMap[node.Key] = nodeCoreWF;
                treeNodeMap[treeNode] = nodeCoreWF;

                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    if (node.ChildNodesExpanded) expandedItemKeys.Add(node.Key);
                    addChildNodes(treeNode, node.ChildNodes, expandedItemKeys);
                }

                parentNode.Nodes.Add(treeNode);
            }
        }

        private class SimpleTreeNodeCoreWF
        {
            public SimpleTreeNode Common { get; set; }
            public TreeNode TreeNode { get; set; }
        }

        private SimpleTreeViewCallback callback;
        private Dictionary<object, SimpleTreeNodeCoreWF> nodeKeyMap = new Dictionary<object, SimpleTreeNodeCoreWF>();
        private Dictionary<TreeNode, SimpleTreeNodeCoreWF> treeNodeMap = new Dictionary<TreeNode, SimpleTreeNodeCoreWF>();
    }
}
