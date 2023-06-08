using System;
using System.Runtime.InteropServices;
using ASEva;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace ASEva.UIMonoMac
{
    class SnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage Snapshot(Control control)
        {
            if (control.ControlObject == null) return null;

            NSView view = null;
            if (control.ControlObject is NSWindow) view = (control.ControlObject as NSWindow).ContentView;
            else if (control.ControlObject is NSView) view = control.ControlObject as NSView;
            if (view == null) return null;

            var bitmap = view.BitmapImageRepForCachingDisplayInRect(view.Bounds);
            if (bitmap == null) return null;

            view.CacheDisplay(view.Bounds, bitmap);

            var pngData = bitmap.RepresentationUsingTypeProperties(NSBitmapImageFileType.Png, null);
            var pngBytes = new byte[pngData.Length];
            Marshal.Copy(pngData.Bytes, pngBytes, 0, (int)pngData.Length);

            return CommonImage.FromBinary(pngBytes);
        }
    }
}
