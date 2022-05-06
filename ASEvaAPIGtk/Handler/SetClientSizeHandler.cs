using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIGtk
{
    class SetClientSizeHandlerGtk : SetClientSizeHandler
    {
        public void SetClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            if (window is Form) window.Size = new Size(logicalWidth, logicalHeight);
            else if (window is Dialog) window.ClientSize = new Size(logicalWidth + 4, logicalHeight + 4);
            else window.ClientSize = new Size(logicalWidth, logicalHeight);
        }

        public void SetMinimumClientSize(Window window, int logicalWidth, int logicalHeight)
        {
            if (App.GetUIBackend() == "wayland")
            {
                var gwindow = window.ControlObject as Gtk.Window;
                gwindow.Realized += delegate
                {
                    if (gwindow.Child != null)
                    {
                        if (window is Dialog) gwindow.Child.SetSizeRequest(logicalWidth + 4, logicalHeight + 4);
                        else gwindow.Child.SetSizeRequest(logicalWidth, logicalHeight); 
                    }
                    else
                    {
                        window.MinimumSize = new Size(logicalWidth, logicalHeight);
                    }
                };
            }
            else
            {
                if (window is Form) window.MinimumSize = new Size(logicalWidth, logicalHeight);
                else if (window is Dialog)
                {
                    var dialog = window as Dialog;
                    if (dialog.WindowStyle == WindowStyle.Default)
                    {
                        var gdialog = dialog.ControlObject as Gtk.Dialog;
                        gdialog.VisibilityNotifyEvent += delegate
                        {
                            if (!gdialog.Visible) return;
                            if (gdialog.Window != null)
                            {
                                var gwindow = gdialog.Window;
                                int dw = gwindow.Width - dialog.ClientSize.Width;
                                int dh = gwindow.Height - dialog.ClientSize.Height;
                                window.MinimumSize = new Size(logicalWidth + dw, logicalHeight + dh);
                            }
                            else
                            {
                                window.MinimumSize = new Size(logicalWidth + 78, logicalHeight + 120);
                            }
                        };
                    }
                    else if (dialog.WindowStyle == WindowStyle.None) window.MinimumSize = new Size(logicalWidth + 4, logicalHeight + 1);
                    else window.MinimumSize = new Size(logicalWidth, logicalHeight);
                }
                else window.MinimumSize = new Size(logicalWidth, logicalHeight);
            }
        }
    }
}