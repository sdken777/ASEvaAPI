using System;
using Gtk;

namespace ASEva.UIGtk
{
    /// <summary>
    /// (api:gtk=2.0.0) 表数据模型扩展方法
    /// </summary>
    public static class ListStoreExtension
    {
        public static int GetRowCount(this ListStore model)
        {
            return model.IterNChildren();
        }

        public static void SetAttribute(this ListStore model, int rowIndex, int attributeIndex, String value)
        {
            if (value == null) return;
            if (rowIndex < 0 || rowIndex >= model.IterNChildren()) return;

            TreeIter iter;
            if (!model.GetIterFirst(out iter)) return;

            for (int i = 0; i < rowIndex; i++)
            {
                if (!model.IterNext(ref iter)) return;
            }

            model.SetValue(iter, attributeIndex, value);
        }

        public static void SetAttribute(this ListStore model, String rowPath, int attributeIndex, String value)
        {
            if (value == null) return;
            if (String.IsNullOrEmpty(rowPath)) return;

            TreeIter iter;
            if (!model.GetIter(out iter, new TreePath(rowPath))) return;

            model.SetValue(iter, attributeIndex, value);
        }

        public static String GetAttribute(this ListStore model, int rowIndex, int attributeIndex)
        {
            if (rowIndex < 0 || rowIndex >= model.IterNChildren()) return null;

            TreeIter iter;
            if (!model.GetIterFirst(out iter)) return null;

            for (int i = 0; i < rowIndex; i++)
            {
                if (!model.IterNext(ref iter)) return null;
            }

            var obj = model.GetValue(iter, attributeIndex);
            if (obj == null || !(obj is String)) return null;

            return obj as String;
        }

        public static String GetAttribute(this ListStore model, String rowPath, int attributeIndex)
        {
            if (String.IsNullOrEmpty(rowPath)) return null;

            TreeIter iter;
            if (!model.GetIter(out iter, new TreePath(rowPath))) return null;

            var obj = model.GetValue(iter, attributeIndex);
            if (obj == null || !(obj is String)) return null;

            return obj as String;
        }
    }
}