using System;
using ASEva.UIEto;
using MonoMac.AppKit;
using MonoMac.CoreGraphics;

namespace ASEva.UIMonoMac
{
    public class AvaloniaAdaptorMonoMac : AvaloniaAdaptor
    {
        public bool IsControlValid(object control)
        {
            if (control is null) return false;
            if (control is Eto.Forms.Control) return true;
            if (control is NSView) return true;
            return false;
        }

        public nint CreateContainer(nint parent, object control, out object? context)
        {
            var nsView = control is NSView view ? view : Eto.Forms.MonoMac64Helpers.ToNative(control as Eto.Forms.Control, true);
            nsView.WantsLayer = true;
            nsView.Layer.BackgroundColor = new CGColor(0.975, 0.975, 0.975, 1.0);
            context = new object();
            return nsView.Handle;
        }

        public void HandleControlResize(object context, double width, double height)
        {
        }

        public void HandleWindowActive(object context, bool active)
        {
        }

        public void ReleaseResource(object context)
        {
        }

        public void RunIteration()
        {
        }

        public bool ShouldCreateContainer()
        {
            return true;
        }

        public void UseContainer(nint container, object control, out object? context)
        {
            context = null;
        }

        public bool ShouldOverrideRunDialog()
        {
            return false;
        }
    }
}