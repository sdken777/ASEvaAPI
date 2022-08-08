using System;
using Gtk;
using ASEva.Samples;
using ASEva.UIEto;
using UI = Gtk.Builder.ObjectAttribute;
using SO = System.Reflection.ObfuscationAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649
    partial class AppDialogGtk : Dialog
    {
        [UI] [SO] Overlay overlay;

        public AppDialogGtk(Widget widget, DialogPanel dialogPanel) : this(new Builder(dialogPanel.WithBorder ? "AppDialogGtk.glade" : "AppDialogGtkPopup.glade"))
        {
            DefaultResponse = ResponseType.Cancel;
            ActionArea.Hide();

            this.panel = dialogPanel;
            this.panelWidget = widget;

            Resizable = dialogPanel.Mode == DialogPanel.DialogMode.ResizableMode;

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
            Response += OnDialogResponse;
            dialogPanel.OnDialogClose += OnDialogClose;
        }

        private AppDialogGtk(Builder builder) : base(builder.GetObject("AppDialogGtk").Handle)
        {
            builder.Autoconnect(this);
        }

        private void OnDialogShown(object sender, EventArgs e)
        {
            overlay.WidthRequest = panel.MinSize.Width;
            overlay.HeightRequest = panel.MinSize.Height;
        }

        private void OnDialogSizeAllocated(object o, SizeAllocatedArgs args)
        {
            panelWidget.WidthRequest = overlay.AllocatedWidth;
            panelWidget.HeightRequest = overlay.AllocatedHeight;
        }

        private void OnDialogClose(object sender, EventArgs e)
        {
            panel.OnClosing();
            Hide();
        }

        private void OnDialogResponse(object o, ResponseArgs args)
        {
            panel.OnClosing();
            Hide();
        }

        private DialogPanel panel;
        private Widget panelWidget;
    }
}
