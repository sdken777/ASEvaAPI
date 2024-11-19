using System;
using System.Collections.Generic;
using System.IO.Pipes;
using Gtk;

namespace ASEva.UIGtk
{
    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Extension methods for list box
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 列表框扩展方法
    /// </summary>
    public static class ListBoxExtension
    {
        public static void AddLabel(this ListBox listBox, String text)
        {
            if (text.Length == 0) return;
            listBox.Add(new Label()
            {
                Text = text,
                Visible = true,
                Xalign = 0,
                MarginStart = 2,
            });
        }

        public static void AddCheckButton(this ListBox listBox, String text, bool active)
        {
            if (text.Length == 0) return;
            listBox.Add(new CheckButton()
            {
                Label = text,
                Active = active,
                Visible = true,
                Xalign = 0,
                MarginStart = 2,
            });
        }

        public static void RemoveAt(this ListBox listBox, int index)
        {
            var target = listBox.GetRowAtIndex(index);
            if (target != null) listBox.Remove(target);
        }

        public static void RemoveItem(this ListBox listBox, Widget item)
        {
            foreach (ListBoxRow row in listBox.Children)
            {
                if (row.Child.Equals(item))
                {
                    listBox.Remove(row);
                    row.Remove(item);
                    break;
                }
            }
        }

        public static void RemoveAll(this ListBox listBox)
        {
            foreach (var r in listBox.Children)
            {
                listBox.Remove(r);
            }
        }

        public static Widget? GetItemAt(this ListBox listBox, int index)
        {
            var target = listBox.GetRowAtIndex(index);
            return target?.Child;
        }

        public static Widget[] GetItems(this ListBox listBox)
        {
            var output = new List<Widget>();
            for (int i = 0; i < listBox.Children.Length; i++)
            {
                var c = listBox.GetRowAtIndex(i)?.Child;
                if (c != null) output.Add(c);
            }
            return output.ToArray();
        }

        public static int GetItemCount(this ListBox listBox)
        {
            return listBox.Children.Length;
        }

        public static int GetActiveIndex(this ListBox listBox)
        {
            var row = listBox.SelectedRow;
            return row == null ? -1 : row.Index;
        }

        public static int[] GetActiveIndices(this ListBox listBox)
        {
            var rows = listBox.SelectedRows;
            if (rows == null) return [];
            var output = new List<int>();
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i] is ListBoxRow listBoxRow) output.Add(listBoxRow.Index);
            }
            return output.ToArray();
        }

        public static Widget? GetActiveItem(this ListBox listBox)
        {
            var row = listBox.SelectedRow;
            return row != null ? row.Child : null;
        }

        public static Widget[] GetActiveItems(this ListBox listBox)
        {
            var rows = listBox.SelectedRows;
            if (rows == null) return [];

            var output = new List<Widget>();
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i] is ListBoxRow listBoxRow) output.Add(listBoxRow.Child);
            }
            return output.ToArray();
        }
    }
}