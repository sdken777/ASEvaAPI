using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Mac;
using MonoMac.AppKit;
using MonoMac.CoreGraphics;

namespace ASEva.UIMonoMac
{
    public class AvaloniaAdaptorMonoMac : AvaloniaAdaptor
    {
        public nint CreateContainer(nint parent, Control control, out object context)
        {
            var nsView = MonoMac64Helpers.ToNative(control, true);
            nsView.WantsLayer = true;
            nsView.Layer.BackgroundColor = new CGColor(0.975, 0.975, 0.975, 1.0);
            context = null;
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

        public void UseContainer(nint container, Control control, out object context)
        {
            context = null;
        }

        public bool ShouldOverrideRunDialog()
        {
            return false;
        }
    }
}