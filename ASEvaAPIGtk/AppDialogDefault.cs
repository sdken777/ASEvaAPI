using System;
using Gtk;
using ASEva.Samples;
using ASEva.UIEto;
using UI = Gtk.Builder.ObjectAttribute;
using SO = System.Reflection.ObfuscationAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649
    partial class AppDialogDefault : Dialog
    {
        [UI] [SO] Overlay? overlay;

        public AppDialogDefault(Widget widget, DialogPanel dialogPanel) : this(new Builder("AppDialogDefault.glade"), widget, dialogPanel)
        {}

        private AppDialogDefault(Builder builder, Widget widget, DialogPanel dialogPanel) : base(builder.GetRawOwnedObject("AppDialogDefault"))
        {
            builder.Autoconnect(this);

            DefaultResponse = ResponseType.Cancel;
            ActionArea.Hide();

            this.panel = dialogPanel;
            this.panelWidget = widget;

            Resizable = dialogPanel.Mode == DialogPanel.DialogMode.ResizableMode;
            Decorated = dialogPanel.WithBorder;

            if (dialogPanel.Title != null) Title = dialogPanel.Title;

            var icon = dialogPanel.Icon;
            if (icon != null)
            {
                var iconSet = icon.ControlObject as IconSet;
                var pixbuf = iconSet?.RenderIconPixbuf(StyleContext, IconSize.Dialog);
                if (pixbuf != null) Icon = pixbuf;
            }

            widget.WidthRequest = dialogPanel.DefaultSize.Width;
            widget.HeightRequest = dialogPanel.DefaultSize.Height;

            widget.Halign = Align.Start;
            widget.Valign = Align.Start;

            if (overlay != null)
            {
                overlay.WidthRequest = dialogPanel.DefaultSize.Width;
                overlay.HeightRequest = dialogPanel.DefaultSize.Height;
                overlay.AddOverlay(widget);
            }

            Shown += OnDialogShown;
            SizeAllocated += OnDialogSizeAllocated;
            Response += OnDialogResponse;
            dialogPanel.OnDialogClose += OnDialogClose;
        }

        private void OnDialogShown(object? sender, EventArgs e)
        {
            if (overlay == null) return;
            overlay.WidthRequest = panel.MinSize.Width;
            overlay.HeightRequest = panel.MinSize.Height;
        }

        private void OnDialogSizeAllocated(object? o, SizeAllocatedArgs args)
        {
            if (overlay == null) return;
            panelWidget.WidthRequest = overlay.AllocatedWidth;
            panelWidget.HeightRequest = overlay.AllocatedHeight;
        }

        private void OnDialogClose(object? sender, EventArgs e)
        {
            panel.OnClosing();
            panel.CloseRecursively();
            Hide();
        }

        private void OnDialogResponse(object? o, ResponseArgs args)
        {
            panel.OnClosing();
            panel.CloseRecursively();
            Hide();
        }

        private DialogPanel panel;
        private Widget panelWidget;
    }
}
