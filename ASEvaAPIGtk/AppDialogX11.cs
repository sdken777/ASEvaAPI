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

        private AppDialogX11(Builder builder) : base(builder.GetObject("AppDialogX11").Handle)
        {
            builder.Autoconnect(this);
        }

        private void OnDialogShown(object sender, EventArgs e)
        {
            overlay.WidthRequest = panel.MinSize.Width;
            overlay.HeightRequest = panel.MinSize.Height;

            if (DialogChain.Count == 0)
            {
                if (DialogHelper.MainWindow != null) DialogHelper.MainWindow.Sensitive = false;
                if (DialogHelper.OtherMainWindows != null)
                {
                    foreach (var window in DialogHelper.OtherMainWindows) window.Sensitive = false;
                }
            }
            else 
            {
                var parentWindow = DialogChain.Last();
                parentWindow.KeepAbove = false;
                parentWindow.Sensitive = false;
            }

            DialogChain.Add(this);
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
                if (DialogHelper.MainWindow != null) DialogHelper.MainWindow.Sensitive = true;
                if (DialogHelper.OtherMainWindows != null)
                {
                    foreach (var window in DialogHelper.OtherMainWindows) window.Sensitive = true;
                }
            }
            else
            {
                var parentWindow = DialogChain.Last();
                parentWindow.KeepAbove = true;
                parentWindow.Sensitive = true;
            }
        }

        private DialogPanel panel;
        private Widget panelWidget;

        private static List<Window> DialogChain = new List<Window>();
    }
}
