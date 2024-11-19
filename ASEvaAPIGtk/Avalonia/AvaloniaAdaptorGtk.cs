using System;
using System.Collections.Generic;
using ASEva.UIEto;

namespace ASEva.UIGtk
{
    public class AvaloniaAdaptorGtk : AvaloniaAdaptor
    {
        public static bool AvaloniaApp { get; private set; }

        public AvaloniaAdaptorGtk()
        {
            AvaloniaApp = true;
            String? screenName = null;
            scale = ScaleFactorQuery.Query(out screenName);
            if (!String.IsNullOrEmpty(screenName) && scale != null)
            {
                Environment.SetEnvironmentVariable("AVALONIA_SCREEN_SCALE_FACTORS", screenName + "=" + scale.Value);
            }
        }

        public void RunIteration()
        {
            while (Gtk.Application.EventsPending())
            {
                Gtk.Main.IterationDo(false);
            }

            foreach (var ctx in ctxs)
            {
                if (ctx.Socket == 0)
                {
                    if (ctx.ParentXID != 0)
                    {
                        ctx.Socket = XembedSocket.xembed_socket_create((uint)ctx.ParentXID, 1);
                        var socketID = XembedSocket.xembed_socket_get_socket_id(ctx.Socket);

                        XembedSocket.xembed_socket_update_both_allocation(ctx.Socket);
                        for (int i = 0; i < 3; i++) XembedSocket.xembed_socket_iteration(ctx.Socket, 0, 0);

                        var uiBackend = App.GetUIBackend();
                        if (uiBackend != null && uiBackend == "x11")
                        {
                            ctx.Plug = new Gtk.Plug((ulong)socketID);
                            ctx.Plug.Add(ctx.Control is Gtk.Widget ? (ctx.Control as Gtk.Widget) : Eto.Forms.Gtk3Helpers.ToNative(ctx.Control as Eto.Forms.Control, true));
                            ctx.Plug.ShowAll();
                        }
                    }
                }
                else
                {
                    XembedSocket.xembed_socket_iteration(ctx.Socket, ctx.Active, ctx.Active);
                }
            }
        }

        public bool ShouldCreateContainer()
        {
            return false;
        }

        public bool IsControlValid(object control)
        {
            if (control == null) return false;
            if (control is Eto.Forms.Control) return true;
            if (control is Gtk.Widget) return true;
            return false;
        }

        public nint CreateContainer(nint parent, object control, out object? context)
        {
            context = null;
            return 0;
        }

        public void UseContainer(nint container, object control, out object? context)
        {
            var ctx = new Context(control);
            ctx.ParentXID = container;
            ctxs.Add(ctx);
            context = ctx;
        }

        public void ReleaseResource(object context)
        {
            var ctx = context as Context;
            if (ctx == null) return;
            
            if (ctx.Socket != 0)
            {
                if (ctx.Plug != null)
                {
                    ctx.Plug.Hide();
                    ctx.Plug.Destroy();
                    for (int i = 0; i < 3; i++)
                    {
                        while (Gtk.Application.EventsPending()) Gtk.Main.IterationDo(false);
                        XembedSocket.xembed_socket_iteration(ctx.Socket, ctx.Active, ctx.Active);
                    }
                    ctx.Plug = null;
                }
                ctx.Socket = 0;
            }
            ctxs.Remove(ctx);
        }

        public void HandleWindowActive(object context, bool active)
        {
            var ctx = context as Context;
            if (ctx == null) return;

            ctx.Active = active ? 1 : 0;
            XembedSocket.xembed_socket_update_focus_in(ctx.Socket, ctx.Active);
            XembedSocket.xembed_socket_update_active(ctx.Socket, ctx.Active);
        }

        public void HandleControlResize(object context, double width, double height)
        {
            var ctx = context as Context;
            if (ctx == null) return;

            if (!ctx.CorrectionInvoked && scale != null && scale.Value != 1)
            {
                HolderBoundCorrection.Correct((uint)ctx.ParentXID, scale.Value, (int)width, (int)height);
            }
            XembedSocket.xembed_socket_update_both_allocation(ctx.Socket);
        }

        public bool ShouldOverrideRunDialog()
        {
            return true;
        }

        private class Context(object control)
        {
            public nint ParentXID { get; set; }
            public nint Socket { get; set; }
            public Gtk.Plug? Plug { get; set; }
            public object Control { get; set; } = control;
            public int Active { get; set; }
            public bool CorrectionInvoked { get; set; }
        }

        private List<Context> ctxs = [];
        private double? scale = null;
    }
}
