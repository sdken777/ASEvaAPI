using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using ASEva;
using ASEva.UIEto;
using UI = Gtk.Builder.ObjectAttribute;
using Eto.Drawing;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649
    class TextTableViewBackendGtk : Box, TextTableViewBackend
    {
        [UI] TreeView treeView;

        public TextTableViewBackendGtk(TextTableViewCallback callback) : this(new Builder("TextTableViewBackendGtk.glade"))
        {
            this.callback = callback;

            treeView.Selection.Changed += delegate
            {
                callback.OnSelectedRowChanged();
            };
        }

        private TextTableViewBackendGtk(Builder builder) : base(builder.GetRawOwnedObject("TextTableViewBackendGtk"))
        {
            builder.Autoconnect(this);
        }

        public void AddColumn(string title, int logicalWidth, bool editable)
        {
            treeView.AppendColumn(new TreeViewColumn
            {
                Title = title,
                FixedWidth = logicalWidth <= 0 ? -1 : logicalWidth,
                Expand = logicalWidth <= 0,
                Resizable = true,
                MinWidth = 12,
            });
            
            var colIndex = treeView.Columns.Length - 1;
            var renderer = new CellRendererText{ Editable = editable };
            treeView.SetColumnRenderer(colIndex, renderer, "text", 3 * colIndex, "foreground", 3 * colIndex + 1, "background", 3 * colIndex + 2);

            var gTypes = new GLib.GType[treeView.Columns.Length * 3];
            for (int i = 0; i < gTypes.Length; i++) gTypes[i] = GLib.GType.String;
            treeView.Model = new ListStore(gTypes);

            renderer.Edited += (o, e) =>
            {
                var listStore = treeView.Model as ListStore;
                var iters = getIters();
                TreeIter iter;
                if (listStore.GetIter(out iter, new TreePath(e.Path)))
                {
                    listStore.SetValue(iter, colIndex * 3, e.NewText);
                    callback.OnCellEdited(iters.ToList().IndexOf(iter), colIndex);
                }
            };
        }

        public void AddRows(List<string[]> rowsValues)
        {
            var listStore = treeView.Model as ListStore;
            foreach (var row in rowsValues)
            {
                var values = new String[row.Length * 3];
                for (int i = 0; i < row.Length; i++)
                {
                    values[3 * i] = row[i];
                    values[3 * i + 1] = rgb(Eto.Drawing.Colors.Black);
                    values[3 * i + 2] = rgb(Eto.Drawing.Colors.White);
                }
                listStore.AppendValues(values);
            }
        }

        public int GetSelectedRowIndex()
        {
            TreeIter selected;
            if (treeView.Selection.GetSelected(out selected)) return getIters().ToList().IndexOf(selected);
            else return -1;
        }

        public string GetValue(int rowIndex, int columnIndex)
        {
            var listStore = treeView.Model as ListStore;
            TreeIter iter = getIters(rowIndex + 1)[rowIndex];
            var val = new GLib.Value();
            listStore.GetValue(iter, columnIndex * 3, ref val);
            return (string)val;
        }

        public void RemoveAllRows()
        {
            var listStore = treeView.Model as ListStore;
            listStore.Clear();
        }

        public void RemoveRows(int[] rowIndices)
        {
            var listStore = treeView.Model as ListStore;
            var iters = getIters(rowIndices[0] + 1);
            foreach (var index in rowIndices)
            {
                listStore.Remove(ref iters[index]);
            }
        }

        public void SetBackgroundColor(int rowIndex, int columnIndex, Color color)
        {
            var listStore = treeView.Model as ListStore;
            TreeIter iter = getIters(rowIndex + 1)[rowIndex];
            listStore.SetValue(iter, 3 * columnIndex + 2, rgb(color));
        }

        public void SetTextColor(int rowIndex, int columnIndex, Color color)
        {
            var listStore = treeView.Model as ListStore;
            TreeIter iter = getIters(rowIndex + 1)[rowIndex];
            listStore.SetValue(iter, 3 * columnIndex + 1, rgb(color));
        }

        public void SetValue(int rowIndex, int columnIndex, string val)
        {
            var listStore = treeView.Model as ListStore;
            TreeIter iter = getIters(rowIndex + 1)[rowIndex];
            listStore.SetValue(iter, 3 * columnIndex, val);
        }

        private TreeIter[] getIters(int maxCount = Int32.MaxValue)
        {
            var list = new List<TreeIter>();
            var listStore = (treeView.Model as ListStore);

            TreeIter iter;
            if (!listStore.GetIterFirst(out iter)) return list.ToArray();
            list.Add(iter);
            if (list.Count >= maxCount) return list.ToArray();

            while (listStore.IterNext(ref iter))
            {
                list.Add(iter);
                if (list.Count >= maxCount) return list.ToArray();
            }
            return list.ToArray();
        }

        private String rgb(Eto.Drawing.Color color)
        {
            return "rgb(" + color.Rb + "," + color.Gb + "," + color.Bb + ")";
        }

        private TextTableViewCallback callback;
    }
}
