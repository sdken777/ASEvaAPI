using System;
using System.Collections.Generic;
using System.Linq;
using ASEva;
using ASEva.UIEto;
using Eto.Mac;
using MonoMac.AppKit;
using MonoMac.CoreGraphics;
using MonoMac.Foundation;

namespace ASEva.UIMonoMac
{
    class SimpleTreeViewBackendMonoMac : NSScrollView, SimpleTreeViewBackend
    {
        public SimpleTreeViewBackendMonoMac(SimpleTreeViewCallback callback)
        {
            var textCell = new VerticalAlignTextCell();
            textCell.Wraps = false;
            textCell.DrawsBackground = true;

            var textColumn = new NSTableColumn();
            textColumn.ResizingMask = NSTableColumnResizing.Autoresizing;
    		textColumn.Editable = false;
    		textColumn.DataCell = textCell;

            var dataSource = new SimpleTreeViewDataSource();
            dataSource.DefaultTextColor = textCell.TextColor;
            dataSource.DefaultBackColor = textCell.BackgroundColor;

            outlineView = new NSOutlineView();
            outlineView.AddColumn(textColumn);
            outlineView.OutlineTableColumn = textColumn;
            outlineView.Delegate = new SimpleTreeViewDelegate(callback);
            outlineView.HeaderView = null;
            outlineView.DataSource = dataSource;
            outlineView.IntercellSpacing = new CGSize(0, 0);
            outlineView.RowHeight = 18;

            DrawsBackground = false;
            AutoresizesSubviews = true;
            DocumentView = outlineView;
            HasVerticalScroller = true;
            HasHorizontalScroller = true;
            AutohidesScrollers = true;
            BorderType = NSBorderType.BezelBorder;

            outlineView.DoubleClick += delegate { callback.OnSelectedItemActivated(); };
        }

        public object GetSelectedKey()
        {
            var rowIndex = outlineView.SelectedRow;
            if (rowIndex < 0) return null;

            var dataSource = outlineView.DataSource as SimpleTreeViewDataSource;
            var node = dataSource.GetNode(outlineView.ItemAtRow(rowIndex));
            return node == null ? null : node.Key;
        }

        public void SetModel(SimpleTreeNode[] rootNodes, bool sort)
        {
            var treeViewRootNodes = new List<SimpleTreeNode>();
            addNodes(rootNodes, treeViewRootNodes, sort);

            var expandIDs = (outlineView.DataSource as SimpleTreeViewDataSource).Reset(treeViewRootNodes);
            outlineView.ReloadData();

            foreach (var id in expandIDs)
            {
                outlineView.ExpandItem(id);
            }
        }

        public void UpdateNodes(SimpleTreeNodeUpdateTask[] tasks)
        {
            foreach (var task in tasks)
            {
                if (!nodeMap.ContainsKey(task.Key)) continue;
                var targetNode = nodeMap[task.Key];
                if (task.TextColor != null) targetNode.TextColor = task.TextColor.Value;
                if (task.BackgroundColor != null) targetNode.BackgroundColor = task.BackgroundColor.Value;
            }
            outlineView.ReloadData();
        }

        public void SelectItem(object key)
        {
            if (!nodeMap.ContainsKey(key)) return;
            outlineView.SelectRow(outlineView.RowForItem(new NSString((nodeMap[key] as SimpleTreeViewNode).IDValue.ToString())), false);
        }

        private void addNodes(SimpleTreeNode[] inNodes, List<SimpleTreeNode> outNodes, bool sort)
        {
            if (sort)
            {
                var list = inNodes.ToList();
                list.Sort((n1, n2) => n1.Text.CompareTo(n2.Text));
                inNodes = list.ToArray();
            }

            foreach (var inNode in inNodes)
            {
                var outNode = new SimpleTreeViewNode();
                outNode.IDValue = ++nodeID;
                outNode.BackgroundColor = inNode.BackgroundColor;
                outNode.ChildNodesExpanded = inNode.ChildNodesExpanded;
                outNode.Key = inNode.Key;
                outNode.Text = inNode.Text;
                outNode.TextColor = inNode.TextColor;
                addNodes(inNode.ChildNodes.ToArray(), outNode.ChildNodes, sort);
                outNodes.Add(outNode);
                nodeMap[outNode.Key] = outNode;
            }
        }

        private class SimpleTreeViewNode : SimpleTreeNode
        {
            public ulong IDValue { get; set; }
        }

        private class SimpleTreeViewDataSource : NSOutlineViewDataSource
        {
            public NSColor DefaultTextColor { private get; set; }
            public NSColor DefaultBackColor { private get; set; }

            public override NSObject GetChild(NSOutlineView outlineView, long childIndex, NSObject item)
            {
                if (item == null) return new NSString((rootNodes[(int)childIndex] as SimpleTreeViewNode).IDValue.ToString());
                else
                {
                    var idString = new NSString(item.Handle);
                    var id = Convert.ToUInt64(idString.ToString());
                    return new NSString((nodeMap[id].ChildNodes[(int)childIndex] as SimpleTreeViewNode).IDValue.ToString());
                }
            }

            public override long GetChildrenCount(NSOutlineView outlineView, NSObject item)
            {
                if (item == null) return rootNodes.Count;
                else
                {
                    var idString = new NSString(item.Handle);
                    var id = Convert.ToUInt64(idString.ToString());
                    return nodeMap[id].ChildNodes.Count;
                }
            }

            public override NSObject GetObjectValue(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
            {
                var idString = new NSString(item.Handle);
                var id = Convert.ToUInt64(idString.ToString());
                var targetNode = nodeMap[id];

                var textCell = new VerticalAlignTextCell(tableColumn.DataCell.Handle);
                textCell.TextColor = targetNode.TextColor == Eto.Drawing.Colors.Transparent ? DefaultTextColor : targetNode.TextColor.ToNSUI();
                textCell.BackgroundColor = targetNode.BackgroundColor == Eto.Drawing.Colors.Transparent ? DefaultBackColor : targetNode.BackgroundColor.ToNSUI();
                return new NSString(targetNode.Text);
            }

            public override bool ItemExpandable(NSOutlineView outlineView, NSObject item)
            {
                var idString = new NSString(item.Handle);
                var id = Convert.ToUInt64(idString.ToString());
                return nodeMap[id].ChildNodes.Count > 0;
            }

            public NSString[] Reset(List<SimpleTreeNode> rootNodes)
            {
                this.rootNodes = rootNodes;
                var expandIDs = new List<NSString>();
                nodeMap.Clear();
                addPairs(rootNodes, expandIDs);
                return expandIDs.ToArray();
            }

            public SimpleTreeNode GetNode(NSObject idObj)
            {
                var id = new NSString(idObj.Handle);
                return nodeMap[Convert.ToUInt64(id.ToString())];
            }

            private void addPairs(List<SimpleTreeNode> nodes, List<NSString> expandIDs)
            {
                foreach (SimpleTreeViewNode node in nodes)
                {
                    nodeMap[Convert.ToUInt64(node.IDValue.ToString())] = node;
                    if (node.ChildNodesExpanded) expandIDs.Add(new NSString(node.IDValue.ToString()));
                    addPairs(node.ChildNodes, expandIDs);
                }
            }

            private List<SimpleTreeNode> rootNodes = new List<SimpleTreeNode>();
            private Dictionary<ulong, SimpleTreeNode> nodeMap = new Dictionary<ulong, SimpleTreeNode>();
        }

        private class SimpleTreeViewDelegate : NSOutlineViewDelegate
        {
            public SimpleTreeViewDelegate(SimpleTreeViewCallback callback)
            {
                this.callback = callback;
            }

            public override void SelectionDidChange(NSNotification notification)
            {
                callback.OnSelectedItemChanged();
            }

            private SimpleTreeViewCallback callback;
        }

        private NSOutlineView outlineView;
        private ulong nodeID = 0;
        private Dictionary<object, SimpleTreeNode> nodeMap = new Dictionary<object, SimpleTreeNode>();
    }
}
