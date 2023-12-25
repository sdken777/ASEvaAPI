using System;
using System.Runtime.InteropServices;
using ASEva;
using ASEva.Samples;
using ASEva.UIEto;
using Gtk;
using Cairo;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0649

    class SnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage Snapshot(Eto.Forms.Control control)
        {
            var widget = control.ControlObject as Widget;
            if (widget == null) return null;

            if (widget is Window)
            {
                widget = (widget as Window).Child;
                if (widget == null) return null;
            }

            var w = widget.AllocatedWidth;
            var h = widget.AllocatedHeight;

            var surface = new ImageSurface(Format.Argb32, w, h);
            var cc = new Context(surface);
            widget.Draw(cc);
            cc.Dispose();

            var output = CommonImage.Create(w, h, true);
            unsafe
            {
                byte* srcData = (byte*)surface.DataPtr;
                fixed (byte* dstData = &(output.Data[0]))
                {
                    for (int v = 0; v < h; v++)
                    {
                        byte* srcRow = &srcData[v * surface.Stride];
                        byte* dstRow = &dstData[v * output.RowBytes];
                        for (int u = 0; u < w; u++)
                        {
                            if (srcRow[4 * u + 3] == 0) continue;
                            var ratio = (float)255.0f / srcRow[4 * u + 3];
                            dstRow[4 * u] = (byte)(ratio * srcRow[4 * u]);
                            dstRow[4 * u + 1] = (byte)(ratio * srcRow[4 * u + 1]);
                            dstRow[4 * u + 2] = (byte)(ratio * srcRow[4 * u + 2]);
                            dstRow[4 * u + 3] = srcRow[4 * u + 3];
                        }
                    }
                }
            }

            surface.Dispose();
            return output;
        }
    }

    class ScreenSnapshotHandler : SnapshotExtensions.SnapshotHandler
    {
        public CommonImage Snapshot(Eto.Forms.Control control)
        {
            var uiBackend = ASEva.UIEto.App.GetUIBackend();
			if (uiBackend != null && uiBackend != "x11") return null; // Wayland not supported yet

            var widget = control.ControlObject as Widget;
            if (widget == null) return null;

            if (widget is Window)
            {
                widget = (widget as Window).Child;
                if (widget == null) return null;
            }

            var loc = control.PointToScreen(new Eto.Drawing.PointF(0, 0));
            var factor = widget.ScaleFactor;
            var right = (loc.X + widget.AllocatedWidth) * factor;
            var bottom = (loc.Y + widget.AllocatedHeight) * factor;
            var left = Math.Max(0, loc.X) * factor;
            var top = Math.Max(0, loc.Y) * factor;

            var xcb = new XCB();
            var rawImage = xcb.Snapshot((ushort)left, (ushort)top, (ushort)(right - left), (ushort)(bottom - top));

            if (rawImage.Width == widget.AllocatedWidth) return rawImage;
            else return rawImage.Resize(widget.AllocatedWidth);
        }

        class XCB
        {
            public CommonImage Snapshot(ushort x, ushort y, ushort width, ushort height)
            {
                IntPtr connection = xcb_connect(null, IntPtr.Zero);
                IntPtr setup = xcb_get_setup(connection);
                IntPtr screen = xcb_setup_roots_iterator(setup).data;

                uint draw = 0;
                unsafe
                {
                    xcb_screen_t *screenPtr = (xcb_screen_t*)screen;
                    draw = screenPtr->root;
                }

                IntPtr image = xcb_image_get(connection, draw, x, y, width, height, 0xffffffff, xcb_image_format_t.XCB_IMAGE_FORMAT_Z_PIXMAP);
                CommonImage commonImage = null;
                unsafe
                {
                    xcb_image_t *imagePtr = (xcb_image_t*)image;
                    byte *srcData = (byte*)imagePtr->data;

                    commonImage = CommonImage.Create(imagePtr->width, imagePtr->height, false);
                    fixed (byte *dstData = &commonImage.Data[0])
                    {
                        for (int v = 0; v < imagePtr->height; v++)
                        {
                            byte *srcRow = srcData + v * 4 * imagePtr->width;
                            byte *dstRow = dstData + v * commonImage.RowBytes;
                            for (int u = 0; u < imagePtr->width; u++)
                            {
                                *(dstRow++) = *(srcRow++);
                                *(dstRow++) = *(srcRow++);
                                *(dstRow++) = *(srcRow++);
                                srcRow++;
                            }
                        }
                    }
                }

                xcb_image_destroy(image);
                xcb_disconnect(connection);

                return commonImage;
            }

            private struct xcb_screen_iterator_t
            {
                public IntPtr data;
                public int rem;
                public int index;
            }

            private enum xcb_image_format_t
            {
                XCB_IMAGE_FORMAT_XY_BITMAP = 0,
                XCB_IMAGE_FORMAT_XY_PIXMAP = 1,
                XCB_IMAGE_FORMAT_Z_PIXMAP = 2
            }

            private struct xcb_screen_t
            {
                public uint root;
                public uint default_colormap;
                public uint white_pixel;
                public uint black_pixel;
                public uint current_input_masks;
                public ushort width_in_pixels;
                public ushort height_in_pixels;
                public ushort width_in_millimeters;
                public ushort height_in_millimeters;
                public ushort min_installed_maps;
                public ushort max_installed_maps;
                public uint root_visual;
                public byte backing_stores;
                public byte save_unders;
                public byte root_depth;
                public byte allowed_depths_len;
            }

            struct xcb_image_t
            {
                public ushort width;
                public ushort height;
                public xcb_image_format_t format;
                public byte scanline_pad;
                public byte depth;
                public byte bpp;
                public byte	unit;
                public uint plane_mask;
                public int byte_order;
                public int bit_order;
                public uint stride;
                public uint size;
                public IntPtr bas;
                public IntPtr data;
            }

            [DllImport("libxcb.so.1")]
            private static extern IntPtr xcb_connect(String displayName, IntPtr screen);

            [DllImport("libxcb.so.1")]
            private static extern void xcb_disconnect(IntPtr connection);

            [DllImport("libxcb.so.1")]
            private static extern IntPtr xcb_get_setup(IntPtr connection);

            [DllImport("libxcb.so.1")]
            private static extern xcb_screen_iterator_t xcb_setup_roots_iterator(IntPtr setup);

            [DllImport("libxcb-image.so.0")]
            private static extern IntPtr xcb_image_get(IntPtr connection, uint draw, ushort x, ushort y, ushort width, ushort height, uint mask, xcb_image_format_t format);

            [DllImport("libxcb-image.so.0")]
            private static extern void xcb_image_destroy(IntPtr image);
        }
    }
}