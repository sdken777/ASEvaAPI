using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using ASEva.Samples;
using ASEva.UIEto;
using UI = Gtk.Builder.ObjectAttribute;
using SO = System.Reflection.ObfuscationAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649
    partial class AppDialogX11 : Window
    {
        [UI] [SO] Overlay overlay;

        public AppDialogX11(Widget widget, DialogPanel dialogPanel) : this(new Builder("AppDialogX11.glade"))
        {
            this.panel = dialogPanel;
            this.panelWidget = widget;

            Resizable = dialogPanel.Mode == DialogPanel.DialogMode.ResizableMode;
            Decorated = dialogPanel.WithBorder;
            KeepAbove = true;

            if (dialogPanel.Title != null) Title = dialogPanel.Title;

            var icon = dialogPanel.Icon;
            if (icon != null)
            {
                var iconSet = icon.ControlObject as IconSet;
                var pixbuf = iconSet.RenderIconPixbuf(StyleContext, IconSize.Dialog);
                if (pixbuf != null) Icon = pixbuf;
            }

            widget.WidthRequest = dialogPanel.DefaultSize.Width;
            widget.HeightRequest = dialogPanel.DefaultSize.Height;

            widget.Halign = Align.Start;
            widget.Valign = Align.Start;

            overlay.WidthRequest = dialogPanel.DefaultSize.Width;
            overlay.HeightRequest = dialogPanel.DefaultSize.Height;
            overlay.AddOverlay(widget);

            Shown += OnDialogShown;
            SizeAllocated += OnDialogSizeAllocated;
            DeleteEvent += OnDialogDelete;

            dialogPanel.OnDialogClose += delegate { Close(); };
        }

        private AppDialogX11(Builder builder) : base(builder.GetRawOwnedObject("AppDialogX11"))
        {
            builder.Autoconnect(this);
        }

        private void OnDialogShown(object sender, EventArgs e)
        {
            overlay.WidthRequest = panel.MinSize.Width;
            overlay.HeightRequest = panel.MinSize.Height;

            if (DialogChain.Count == 0)
            {
                if (DialogHelper.MainWindow != null) mainWindows.Add(DialogHelper.MainWindow);
                if (DialogHelper.OtherMainWindows != null) mainWindows.AddRange(DialogHelper.OtherMainWindows);
                foreach (var window in mainWindows)
                {
                    window.Sensitive = false;
                    window.DeleteEvent += parent_DeleteEvent;
                }
            }
            else 
            {
                var parentWindow = DialogChain.Last();
                parentWindow.KeepAbove = false;
                parentWindow.Sensitive = false;
                parentWindow.DeleteEvent += parent_DeleteEvent;
            }

            DialogChain.Add(this);
        }

        [GLib.ConnectBefore]
        private static void parent_DeleteEvent(object o, DeleteEventArgs args)
        {
            args.RetVal = true;
        }

        private void OnDialogSizeAllocated(object o, SizeAllocatedArgs args)
        {
            panelWidget.WidthRequest = overlay.AllocatedWidth;
            panelWidget.HeightRequest = overlay.AllocatedHeight;
        }

        private void OnDialogDelete(object o, DeleteEventArgs args)
        {
            panel.OnClosing();
            panel.CloseRecursively();

            DialogChain.Remove(this);

            if (DialogChain.Count == 0)
            {
                foreach (var window in mainWindows)
                {
                    window.Sensitive = true;
                    window.DeleteEvent -= parent_DeleteEvent;
                }
                mainWindows.Clear();
            }
            else
            {
                var parentWindow = DialogChain.Last();
                parentWindow.KeepAbove = true;
                parentWindow.Sensitive = true;
                parentWindow.Deletable = true;
                parentWindow.DeleteEvent -= parent_DeleteEvent;
            }
        }

        private DialogPanel panel;
        private Widget panelWidget;

        private List<Window> mainWindows = new List<Window>();

        private static List<Window> DialogChain = new List<Window>();
    }
}
