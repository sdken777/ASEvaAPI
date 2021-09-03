using System;
using Gtk;

namespace ASEva.UIGtk
{
    /// <summary>
    /// (api:gtk=2.0.0) 组合框扩展方法
    /// </summary>
    public static class ComboBoxTextExtension
    {
        public static int GetItemCount(this ComboBoxText combo)
        {
            return combo.Model.IterNChildren();
        }

        public static String GetItemAt(this ComboBoxText combo, int index)
        {
            if (index < 0 || index >= combo.Model.IterNChildren()) return null;
            else return combo.GetItems()[index];
        }

        public static String[] GetItems(this ComboBoxText combo)
        {
            if (combo.Model.IterNChildren() == 0) return new string[0];

            var output = new String[combo.Model.IterNChildren()];

            TreeIter iter;
            combo.Model.GetIterFirst(out iter);

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = combo.Model.GetValue(iter, 0) as String;
                combo.Model.IterNext(ref iter);
            }

            return output;
        }

        public static void SetItemAt(this ComboBoxText combo, int index, String item)
        {
            if (index < 0 || index >= combo.Model.IterNChildren()) return;

            TreeIter iter;
            combo.Model.GetIterFirst(out iter);
            for (int i = 0; i < index; i++) combo.Model.IterNext(ref iter);

            combo.Model.SetValue(iter, 0, item);
        }
    }
}