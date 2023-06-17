using System;
using System.Linq;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto;
using Eto.Forms;
using UI = Gtk.Builder.ObjectAttribute;

namespace ASEva.UIGtk
{
    class FlowLayoutFactoryGtk : FlowLayoutFactory
    {
        public void CreateFlowLayoutBackend(FlowLayoutCallback callback, out Control etoControl, out FlowLayoutBackend backend)
        {
            var box = new FlowLayoutBackendGtk(callback);
            etoControl = box.ToEto();
            backend = box;
        }
    }

    #pragma warning disable CS0612, CS0649
    class FlowLayoutBackendGtk : Gtk.Box, FlowLayoutBackend
    {
        [UI] Gtk.Box mainBox;

        public FlowLayoutBackendGtk(FlowLayoutCallback callback) : this(new Gtk.Builder("FlowLayoutBackendGtk.glade"))
        {
            this.callback = callback;
        }

        private FlowLayoutBackendGtk(Gtk.Builder builder) : base(builder.GetRawOwnedObject("FlowLayoutBackendGtk"))
        {
            builder.Autoconnect(this);
        }

        public void AddControl(Control control, int logicalHeight)
        {
            var gtkControl = control.ToNative(true) as Gtk.Widget;
            
            var viewPort = new Gtk.Viewport{ Visible = true };
            viewPort.Add(gtkControl);

            var container = new Gtk.ScrolledWindow{ Visible = true };
            container.HscrollbarPolicy = container.VscrollbarPolicy = Gtk.PolicyType.Never;
            container.HeightRequest = logicalHeight + 2;
            container.Add(viewPort);

            mainBox.PackStart(container, false, false, 0);

            ctxs.Add(new ControlContext { EtoControl = control, Container = container, Visible = true });
            control.MouseDown += (obj, args) =>
            {
                callback.OnControlClicked(ctxs.FindIndex(c => c.EtoControl.Equals(obj)));
            };
        }

        public void InsertControl(int index, Control control, int logicalHeight)
        {
            var gtkControl = control.ToNative(true) as Gtk.Widget;
            
            var viewPort = new Gtk.Viewport{ Visible = true };
            viewPort.Add(gtkControl);

            var container = new Gtk.ScrolledWindow{ Visible = true };
            container.HscrollbarPolicy = container.VscrollbarPolicy = Gtk.PolicyType.Never;
            container.HeightRequest = logicalHeight + 2;
            container.Add(viewPort);

            mainBox.PackStart(container, false, false, 0);

            if (index < ctxs.Count)
            {
                int position = 0;
                for (int i = 0; i < index; i++)
                {
                    if (ctxs[i].Visible) position++;
                }
                mainBox.ReorderChild(container, position);
            }

            ctxs.Insert(index, new ControlContext { EtoControl = control, Container = container, Visible = true });
            control.MouseDown += (obj, args) =>
            {
                callback.OnControlClicked(ctxs.FindIndex(c => c.EtoControl.Equals(obj)));
            };
        }

        public void RemoveAllControls()
        {
            var children = mainBox.Children;
            foreach (var child in children) mainBox.Remove(child);
            ctxs.Clear();
        }

        public void RemoveControl(int index)
        {
            if (ctxs[index].Visible) mainBox.Remove(ctxs[index].Container);
            ctxs.RemoveAt(index);
        }

        public void SelectControl(int index)
        {
            if (selectedContainer != null)
            {
                selectedContainer.ShadowType = Gtk.ShadowType.None;
                selectedContainer = null;
            }
            selectedContainer = ctxs[index].Container;
            selectedContainer.ShadowType = Gtk.ShadowType.In;
        }

        public void SetControlVisible(int index, bool visible)
        {
            if (ctxs[index].Visible == visible) return;

            if (visible)
            {
                mainBox.PackStart(ctxs[index].Container, false, false, 0);
                if (index < ctxs.Count)
                {
                    int position = 0;
                    for (int i = 0; i < index; i++)
                    {
                        if (ctxs[i].Visible) position++;
                    }
                    mainBox.ReorderChild(ctxs[index].Container, position);
                }
            }
            else
            {
                mainBox.Remove(ctxs[index].Container);
            }

            ctxs[index].Visible = visible;
        }

        private class ControlContext
        {
            public Gtk.ScrolledWindow Container { get; set; }
            public Control EtoControl { get; set; }
            public bool Visible { get; set; }
        }

        private FlowLayoutCallback callback;
        private List<ControlContext> ctxs = new List<ControlContext>();
        private Gtk.ScrolledWindow selectedContainer = null;
    }
}
