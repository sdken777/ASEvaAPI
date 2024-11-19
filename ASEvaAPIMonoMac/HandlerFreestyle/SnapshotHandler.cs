using System;
using System.Runtime.InteropServices;
using ASEva;
using ASEva.Samples;
using ASEva.Utility;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;
using MonoMac.AppKit;
using MonoMac.Foundation;
using MonoMac.CoreGraphics;

namespace ASEva.UIMonoMac
{
    class SnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage? Snapshot(Control control)
        {
            if (control.ControlObject == null) return null;

            NSView? view = null;
            var logicalWidth = control.GetLogicalWidth();
            if (control.ControlObject is NSWindow nsWindow) 
            {
                view = nsWindow.ContentView;
                logicalWidth = (int)view.Frame.Width;
            }
            else if (control.ControlObject is NSView nsView) view = nsView;
            if (view == null) return null;

            var bitmap = view.BitmapImageRepForCachingDisplayInRect(view.Bounds);
            if (bitmap == null) return null;

            view.CacheDisplay(view.Bounds, bitmap);

            var pngData = bitmap.RepresentationUsingTypeProperties(NSBitmapImageFileType.Png, null);
            var pngBytes = new byte[pngData.Length];
            Marshal.Copy(pngData.Bytes, pngBytes, 0, (int)pngData.Length);

            try
            {
                var etoBitmap = new Bitmap(pngBytes);
                var rawImage = ASEva.UIEto.ImageConverter.ConvertFromBitmap(etoBitmap);

                if (rawImage.Width == logicalWidth) return rawImage;
                else return rawImage.Resize(logicalWidth);
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                return null;
            }
        }
    }

    class ScreenSnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage? Snapshot(Control control)
        {
            if (control.ControlObject == null) return null;

            NSView view = null;
            var logicalWidth = control.GetLogicalWidth();
            if (control.ControlObject is NSWindow)
            {
                view = (control.ControlObject as NSWindow).ContentView;
                logicalWidth = (int)view.Frame.Width;
            }
            else if (control.ControlObject is NSView) view = control.ControlObject as NSView;
            if (view == null) return null;

            var screenNumber = (NSNumber)NSScreen.MainScreen.DeviceDescription["NSScreenNumber"];
            var cgImage = cgDisplayCreateImage(screenNumber.UInt32Value);
            if (cgImage == null) return null;

            var bitmap = new NSBitmapImageRep(cgImage);

            var pngData = bitmap.RepresentationUsingTypeProperties(NSBitmapImageFileType.Png, null);
            var pngBytes = new byte[pngData.Length];
            Marshal.Copy(pngData.Bytes, pngBytes, 0, (int)pngData.Length);

            try
            {
                var etoBitmap = new Bitmap(pngBytes);

                var viewOffset = view.ConvertPointToView(new CGPoint(0,0), view.Window.ContentView);
                var bound = view.Window.ConvertRectToScreen(view.Frame);
                var scale = Screen.PrimaryScreen.LogicalPixelSize;
                bound = new CGRect((viewOffset.X + bound.X) * scale, (viewOffset.Y + bound.Y) * scale, bound.Width * scale, bound.Height * scale);

                etoBitmap = etoBitmap.Clone(new Rectangle((int)bound.X, etoBitmap.Height - (int)bound.Y - (int)bound.Height, (int)bound.Width, (int)bound.Height));
                var rawImage = ASEva.UIEto.ImageConverter.ConvertFromBitmap(etoBitmap);

                if (rawImage.Width == logicalWidth) return rawImage;
                else return rawImage.Resize(logicalWidth);
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                return null;
            }
        }

        private const string DllName = "/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics";

        [DllImport(DllName)]
        private extern static IntPtr CGDisplayCreateImage(UInt32 displayId);

        [DllImport(DllName)]
        private extern static void CFRelease(IntPtr handle);

        private static CGImage cgDisplayCreateImage(UInt32 displayId)
        {
            IntPtr handle = IntPtr.Zero;
            try
            {
                handle = CGDisplayCreateImage(displayId);
                return new CGImage(handle);
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    CFRelease(handle);
                }
            }
        }
    }
}
