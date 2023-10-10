using System;
using System.Collections.Generic;
using System.Linq;
using ASEva;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;
using UI = Gtk.Builder.ObjectAttribute;

namespace ASEva.UIGtk
{
    class FlowLayout2DFactoryGtk : FlowLayout2DFactory
    {
        public void CreateFlowLayout2DBackend(FlowLayoutCallback callback, out Control etoControl, out FlowLayout2DBackend backend)
        {
            var box = new FlowLayout2DBackendGtk(callback);
            etoControl = box.ToEto();
            backend = box;
        }
    }

    #pragma warning disable CS0612, CS0649
    class FlowLayout2DBackendGtk : Gtk.Box, FlowLayout2DBackend
    {
        [UI] Gtk.Box mainBox;

        public FlowLayout2DBackendGtk(FlowLayoutCallback callback) : this(new Gtk.Builder("FlowLayout2DBackendGtk.glade"))
        {
            this.callback = callback;
        }
 
        private FlowLayout2DBackendGtk(Gtk.Builder builder) : base(builder.GetObject("FlowLayout2DBackendGtk").Handle)
        {
            builder.Autoconnect(this);
        }

        public void SetControlWidth(int logicalWidth)
        {
            controlWidth = logicalWidth;
        }

        public void SetControlHeight(int index, int logicalHeight)
        {
            ctxs[index].Container.HeightRequest = logicalHeight + 2;
            ctxs[index].LogicalHeight = logicalHeight;
        }

        public void UpdateControlsLayout(Size containerLogicalSize)
        {
            int containerWidth = containerLogicalSize.Width - 8;
            int containerHeight = containerLogicalSize.Height - 8;
            if (containerWidth < 8 || containerHeight < 8) return;

            int itemWidth = controlWidth + 8;

            var identifierMap = new Dictionary<int, ControlContext>();
            var targetIdentifiers = new List<List<int>>();
            var colIdentifiers = new List<int>();
            int colHeight = 0;
            for (int i = 0; i < ctxs.Count; i++)
            {
                var ctx = ctxs[i];
                if (!ctx.Visible) continue;

                var itemHeight = ctx.LogicalHeight + 8;
                if (itemHeight >= containerHeight) continue;

                if (colHeight + itemHeight >= containerHeight)
                {
                    targetIdentifiers.Add(colIdentifiers);
                    colIdentifiers = new List<int>(); 
                    colHeight = 0;
                }

                identifierMap[ctx.Identifier] = ctx;
                colIdentifiers.Add(ctx.Identifier);
                colHeight += itemHeight;
            }
            if (colIdentifiers.Count > 0) targetIdentifiers.Add(colIdentifiers);

            if (curIdentifiers.Count > 0)
            {
                int i = curIdentifiers.Count - 1;
                while (i >= 0)
                {
                    var colBox = mainBox.Children[i] as Gtk.Box;
                    if (i >= targetIdentifiers.Count)
                    {
                        var children = colBox.Children;
                        foreach (var child in children) colBox.Remove(child);

                        curIdentifiers.RemoveAt(i);
                        mainBox.Remove(mainBox.Children[i]);
                    }
                    else
                    {
                        var curColIdentifiers = curIdentifiers[i];
                        int j = curColIdentifiers.Count - 1;
                        while (j >= 0)
                        {
                            if (!targetIdentifiers[i].Contains(curColIdentifiers[j]))
                            {
                                curColIdentifiers.RemoveAt(j);
                                colBox.Remove(colBox.Children[j]);
                            }
                            j--;
                        }
                    }
                    i--;
                }
            }

            for (int i = 0; i < targetIdentifiers.Count; i++)
            {
                if (i >= curIdentifiers.Count)
                {
                    var colBox = new Gtk.VBox{ Spacing = 6, Visible = true };
                    mainBox.PackStart(colBox, false, false, 0);

                    foreach (var identifier in targetIdentifiers[i])
                    {
                        var ctx = identifierMap[identifier];
                        ctx.Container.WidthRequest = controlWidth + 2;
                        colBox.PackStart(ctx.Container, false, false, 0);
                    }

                    curIdentifiers.Add(targetIdentifiers[i]);
                }
                else
                {
                    var curColIdentifiers = curIdentifiers[i];
                    var colBox = mainBox.Children[i] as Gtk.Box;
                    int insertIndex = 0;

                    foreach (var identifier in targetIdentifiers[i])
                    {
                        var ctx = identifierMap[identifier];
                        ctx.Container.WidthRequest = controlWidth + 2;
                        if (curColIdentifiers.Contains(identifier))
                        {
                            insertIndex = curColIdentifiers.IndexOf(identifier) + 1;
                            continue;
                        }
                        if (insertIndex >= colBox.Children.Length)
                        {
                            colBox.PackStart(ctx.Container, false, false, 0);
                            curColIdentifiers.Add(identifier);
                        }
                        else
                        {
                            colBox.PackStart(ctx.Container, false, false, 0);
                            colBox.ReorderChild(ctx.Container, insertIndex);
                            curColIdentifiers.Insert(insertIndex, identifier);
                        }
                        insertIndex++;
                    }
                }
            }
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

            ctxs.Add(new ControlContext { Identifier = ++identifierCount, EtoControl = control, Container = container, LogicalHeight = logicalHeight, Visible = true });
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

            ctxs.Insert(index, new ControlContext { Identifier = ++identifierCount, EtoControl = control, Container = container, LogicalHeight = logicalHeight, Visible = true });
            control.MouseDown += (obj, args) =>
            {
                callback.OnControlClicked(ctxs.FindIndex(c => c.EtoControl.Equals(obj)));
            };
        }

        public void RemoveAllControls()
        {
            selectedContainer = null;
            var children = mainBox.Children;
            foreach (var child in children) mainBox.Remove(child);
            ctxs.Clear();
            curIdentifiers.Clear();
        }

        public void RemoveControl(int index)
        {
            if (ctxs[index].Container.Equals(selectedContainer)) selectedContainer = null;
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
            ctxs[index].Visible = visible;
        }

        public int GetControlWithMouse()
        {
            foreach (var ctx in ctxs)
            {
                if (!ctx.Visible) continue;

                int mouseX, mouseY;
                ctx.Container.GetPointer(out mouseX, out mouseY);
                if (mouseX > 0 && mouseY > 0 && mouseX < ctx.Container.AllocatedWidth && mouseY < ctx.Container.AllocatedHeight) return ctxs.IndexOf(ctx);
            }
            return -1;
        }

        private class ControlContext
        {
            public int Identifier { get; set; }
            public Control EtoControl { get; set; }
            public Gtk.ScrolledWindow Container { get; set; }
            public int LogicalHeight { get; set; }
            public bool Visible { get; set; }
        }

        private FlowLayoutCallback callback;
        private List<ControlContext> ctxs = new List<ControlContext>();
        private List<List<int>> curIdentifiers = new List<List<int>>();
        private Gtk.ScrolledWindow selectedContainer;
        private int controlWidth;

        private int identifierCount = 0;
    }
}