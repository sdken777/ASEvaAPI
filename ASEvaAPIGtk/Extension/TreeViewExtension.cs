using System;
using Gtk;

namespace ASEva.UIGtk
{
    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Extension methods for tree view
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 树和表视图框扩展方法
    /// </summary>
    public static class TreeViewExtension
    {
        public static void SetColumnRenderer(this TreeView treeView, int columnIndex, CellRenderer renderer, params object[] attributeIndexMap)
        {
            if (columnIndex < 0 || columnIndex >= treeView.Columns.Length) return;

            treeView.Columns[columnIndex].PackStart(renderer, true);
            treeView.Columns[columnIndex].SetAttributes(renderer, attributeIndexMap);
        }
    }
}