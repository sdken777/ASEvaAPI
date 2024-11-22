using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using ASEva;
using ASEva.UIEto;
using UI = Gtk.Builder.ObjectAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649
    class CheckableListBoxBackendGtk : EventBox, CheckableListBoxBackend
    {
        [UI] TreeView treeView;
        [UI] ListStore listStore;

        public CheckableListBoxBackendGtk(CheckableListBoxCallback callback) : this(new Builder("CheckableListBoxBackendGtk.glade"))
        {
            var toggle = new CellRendererToggle();
            var label = new CellRendererText();
            treeView.SetColumnRenderer(0, toggle, "sensitive", 1, "active", 2);
            treeView.SetColumnRenderer(1, label, "text", 0, "foreground", 3);

            toggle.Toggled += (o, e) =>
            {
                TreeIter iter;
                if (listStore.GetIter(out iter, new TreePath(e.Path)))
                {
                    var enable = new GLib.Value();
                    listStore.GetValue(iter, 1, ref enable);
                    if ((bool)enable)
                    {
                        var active = new GLib.Value();
                        listStore.GetValue(iter, 2, ref active);
                        listStore.SetValue(iter, 2, new GLib.Value(!(bool)active));
                    }

                    treeView.Selection.SelectIter(iter);
                    callback.OnItemClicked();

                    toggled = true;
                }
            };

            ButtonReleaseEvent += (o, e) =>
            {
                if (toggled)
                {
                    toggled = false;
                    return;
                }

                if (e.Event.Button != 1 || e.Event.Type != Gdk.EventType.ButtonRelease) return;
                e.RetVal = true;

                TreeIter iter;
                if (treeView.Selection.GetSelected(out iter))
                {
                    GLib.Value enabled = new GLib.Value(), active = new GLib.Value();
                    listStore.GetValue(iter, 1, ref enabled);
                    listStore.GetValue(iter, 2, ref active);
                    if ((bool)enabled)
                    {
                        listStore.SetValue(iter, 2, new GLib.Value(!(bool)active));
                        callback.OnItemClicked();
                    }
                }
            };
        }

        private CheckableListBoxBackendGtk(Builder builder) : base(builder.GetRawOwnedObject("CheckableListBoxBackendGtk"))
        {
            builder.Autoconnect(this);
        }

        public void AddItems(string[] itemsText, bool[] itemsChecked, bool[] itemsEnabled)
        {
            for (int i = 0; i < itemsText.Length; i++)
            {
                var enable = itemsEnabled == null ? true : itemsEnabled[i];
                var active = itemsChecked == null ? false : itemsChecked[i];
                listStore.AppendValues(itemsText[i], enable, active, rgb(enable ? Eto.Drawing.Colors.Black : Eto.Drawing.Colors.LightGrey));
            }
        }

        public void RemoveItems(int[] indices)
        {
            var iters = getIters(indices[0] + 1);
            foreach (var index in indices)
            {
                listStore.Remove(ref iters[index]);
            }
        }

        public void RemoveAllItems()
        {
            listStore.Clear();
        }

        public bool GetChecked(int index)
        {
            var active = new GLib.Value();
            listStore.GetValue(getIters(index + 1)[index], 2, ref active);
            return (bool)active;
        }

        public void SetChecked(int[] indices, bool isChecked)
        {
            var iters = getIters(indices.Last() + 1);
            foreach (var index in indices)
            {
                listStore.SetValue(iters[index], 2, new GLib.Value(isChecked));
            }
        }

        public void SetText(int index, string text)
        {
            listStore.SetValue(getIters(index + 1)[index], 0, new GLib.Value(text));
        }

        public void SetEnabled(int index, bool isEnabled)
        {
            TreeIter iter = getIters(index + 1)[index];
            listStore.SetValue(iter, 1, new GLib.Value(isEnabled));
            listStore.SetValue(iter, 3, rgb(isEnabled ? Eto.Drawing.Colors.Black : Eto.Drawing.Colors.LightGrey));
        }

        public int[] GetCheckedIndices()
        {
            var iters = getIters();
            var list = new List<int>();
            for (int i = 0; i < iters.Length; i++)
            {
                var active = new GLib.Value();
                listStore.GetValue(iters[i], 2, ref active);
                if ((bool)active) list.Add(i);
            }
            return list.ToArray();
        }

        public int GetSelectedRowIndex()
        {
            TreeIter selected;
            if (treeView.Selection.GetSelected(out selected)) return getIters().ToList().IndexOf(selected);
            else return -1;
        }

        private TreeIter[] getIters(int maxCount = Int32.MaxValue)
        {
            var list = new List<TreeIter>();

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

        private bool toggled = false;
    }
}
