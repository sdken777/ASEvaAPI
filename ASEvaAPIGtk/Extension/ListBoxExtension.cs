using System;
using Gtk;

namespace ASEva.Gtk
{
    /// <summary>
    /// (api:gtk=1.0.0) 列表框扩展方法
    /// </summary>
    public static class ListBoxExtension
    {
        public static void AddLabel(this ListBox listBox, String text)
        {
            if (String.IsNullOrEmpty(text)) return;
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
            if (String.IsNullOrEmpty(text)) return;
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
            if (item == null) return;
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

        public static Widget GetItemAt(this ListBox listBox, int index)
        {
            var target = listBox.GetRowAtIndex(index);
            return target.Child is Widget ? target.Child as Widget : null;
        }

        public static Widget[] GetItems(this ListBox listBox)
        {
            var output = new Widget[listBox.Children.Length];
            for (int i = 0; i < output.Length; i++)
            {
                var c = listBox.GetRowAtIndex(i).Child;
                if (c is Widget) output[i] = c as Widget;
            }
            return output;
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
            var output = new int[rows == null ? 0 : rows.Count];
            for (int i = 0; i < output.Length; i++) output[i] = (rows[i] as ListBoxRow).Index;
            return output;
        }

        public static Widget GetActiveItem(this ListBox listBox)
        {
            var row = listBox.SelectedRow;
            return row != null && row.Child is Widget ? row.Child as Widget : null;
        }

        public static Widget[] GetActiveItems(this ListBox listBox)
        {
            var rows = listBox.SelectedRows;
            var output = new Widget[rows == null ? 0 : rows.Count];
            for (int i = 0; i < output.Length; i++)
            {
                var c = (rows[i] as ListBoxRow).Child;
                if (c is Widget) output[i] = c as Widget;
            }
            return output;
        }
    }
}