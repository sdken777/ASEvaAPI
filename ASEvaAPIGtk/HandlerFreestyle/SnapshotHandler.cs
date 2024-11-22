using System;
using System.Runtime.InteropServices;
using System.IO;
using ASEva;
using ASEva.Samples;
using Gtk;
using GLib;
using Cairo;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0649

    class SnapshotHandler : ASEva.UIEto.SnapshotExtensions.SnapshotHandler
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

    class ScreenSnapshotHandler : ASEva.UIEto.SnapshotExtensions.SnapshotHandler
    {
        public CommonImage Snapshot(Eto.Forms.Control control)
        {
            var uiBackend = ASEva.UIEto.App.GetUIBackend();
            if (uiBackend == null) return null;

            var widget = control.ControlObject as Widget;
            if (widget == null) return null;

            if (widget is Window)
            {
                widget = (widget as Window).Child;
                if (widget == null) return null;
            }

            CommonImage rawImage = null;
            if (uiBackend == "wayland") // for future use
            {
                var dbus = new DBus();
                dbus.Snapshot();

                String uri = null;
                while (true)
                {
                    System.Threading.Thread.Sleep(1);
                    Gtk.Application.RunIteration();
                    uri = dbus.PopUri();
                    if (uri != null) break;
                }
                if (uri.Length == 0) return null;

                if (uri.StartsWith("file://")) uri = uri.Substring(7);

                var etoBitmap = new Eto.Drawing.Bitmap(uri);
                File.Delete(uri);

                var windowSize = control.ParentWindow.Size;
                var etoBitmapSize = etoBitmap.Size;
                if (Math.Abs(windowSize.Width * etoBitmapSize.Height - windowSize.Height * etoBitmapSize.Width) > 0.001)
                {
                    AgencyLocal.Print("Wayland screenshot: Choose 'Grab the current window' and 'Take screenshot'.");
                    return null;
                }

                var fullImage = ASEva.UIEto.ImageConverter.ConvertFromBitmap(etoBitmap);

                var loc = widget.Allocation;
                var factor = (double)etoBitmapSize.Width / windowSize.Width;
                var right = (loc.X + widget.AllocatedWidth) * factor;
                var bottom = (loc.Y + widget.AllocatedHeight) * factor;
                var left = Math.Max(0, loc.X) * factor;
                var top = Math.Max(0, loc.Y) * factor;

                int x = (int)left, y = (int)top;
                int width = (int)(right - left), height = (int)(bottom - top);
                if (x + width > fullImage.Width || y + height > fullImage.Height) return null;

                rawImage = CommonImage.Create(width, height, fullImage.WithAlpha);
                var cellBytes = fullImage.WithAlpha ? 4 : 3;
                unsafe
                {
                    fixed (byte *srcData = &fullImage.Data[0], dstData = &rawImage.Data[0])
                    {
                        for (int v = 0; v < height; v++)
                        {
                            byte *srcPtr = srcData + (y + v) * fullImage.RowBytes + x * cellBytes;
                            byte *dstPtr = dstData + v * rawImage.RowBytes;
                            int copyBytes = width * cellBytes;
                            for (int n = 0; n < copyBytes; n++)
                            {
                                *dstPtr++ = *srcPtr++;
                            }
                        }
                    }
                }
            }
            else if (uiBackend == "x11")
            {
                var loc = control.PointToScreen(new Eto.Drawing.PointF(0, 0));
                var factor = widget.ScaleFactor;
                var right = (loc.X + widget.AllocatedWidth) * factor;
                var bottom = (loc.Y + widget.AllocatedHeight) * factor;
                var left = Math.Max(0, loc.X) * factor;
                var top = Math.Max(0, loc.Y) * factor;

                var xcb = new XCB();
                rawImage = xcb.Snapshot((ushort)left, (ushort)top, (ushort)(right - left), (ushort)(bottom - top));
            }
            if (rawImage == null) return null;

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
                if (image == IntPtr.Zero)
                {
                    xcb_disconnect(connection);
                    return null;
                }

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

        class DBus
        {
            public void Snapshot()
            {
                if (curInstance != null) return;
                curInstance = this;

                connection = new DBusConnection(g_bus_get_sync(BusType.Session, IntPtr.Zero, IntPtr.Zero));
                proxy = new DBusProxy(g_dbus_proxy_new_sync(connection.Handle, DBusProxyFlags.None, IntPtr.Zero,
                    "org.freedesktop.portal.Desktop", "/org/freedesktop/portal/desktop",
                    "org.freedesktop.portal.Screenshot", IntPtr.Zero, IntPtr.Zero));

                var asvType = new VariantType("a{sv}");
                var builder = g_variant_builder_new(asvType.Handle);
                g_variant_builder_add(builder, "{sv}", "handle_token", g_variant_new_string(Guid.NewGuid().ToString().Replace("-", "")));
                g_variant_builder_add(builder, "{sv}", "interactive", g_variant_new_boolean(true));
                var parameters = new Variant(g_variant_new("(sa{sv})", "", builder));
                g_variant_builder_unref(builder);

                var result = new Variant(g_dbus_proxy_call_sync(proxy.Handle, "Screenshot", parameters.Handle, DBusCallFlags.None, -1, IntPtr.Zero, IntPtr.Zero));
                
                String objPath = null;
                g_variant_get(result.Handle, "(o)", ref objPath);

                id = g_dbus_connection_signal_subscribe(connection.Handle, "org.freedesktop.portal.Desktop",
                            "org.freedesktop.portal.Request", "Response", objPath,
                            null, DBusSignalFlags.None, signalCallback, IntPtr.Zero, IntPtr.Zero);
            }

            public String PopUri()
            {
                var output = uri;
                uri = null;
                return output;
            }

            private delegate void GDBusSignalCallback(IntPtr connection, String sender_name, String object_path, String interface_name, String signal_name, IntPtr parameters, IntPtr user_data);
            private static GDBusSignalCallback signalCallback = signalCallbackFunc;
            private static void signalCallbackFunc(IntPtr connection, String sender_name, String object_path, String interface_name, String signal_name, IntPtr parameters, IntPtr user_data)
            {
                var variant = new Variant(parameters);

                uint response = 0;
                IntPtr dict = IntPtr.Zero;
                g_variant_get(parameters, "(u@a{sv})", ref response, ref dict);

                if (response == 0)
                {
                    String uri = null;
                    g_variant_lookup(dict, "uri", "s", ref uri);
                    curInstance.uri = uri == null ? "" : uri;
                }
                else curInstance.uri = "";

                g_dbus_connection_signal_unsubscribe(curInstance.connection.Handle, curInstance.id.Value);
                curInstance.connection.Dispose();
                curInstance.proxy.Dispose();
                curInstance.id = null;
                curInstance = null;
            }

            private static DBus curInstance = null;
            private DBusConnection connection = null;
            private DBusProxy proxy = null;
            private uint? id = null;
            private String uri = null;

            [DllImport("libgio-2.0.so.0")]
            private static extern IntPtr g_bus_get_sync(BusType type, IntPtr cancel, IntPtr error);

            [DllImport("libgio-2.0.so.0")]
            private static extern IntPtr g_dbus_proxy_new_sync(IntPtr connection, DBusProxyFlags flags, IntPtr info, String name, String objectPath, String interfaceName, IntPtr cancel, IntPtr error);

            [DllImport("libgio-2.0.so.0")]
            private static extern IntPtr g_dbus_proxy_call_sync(IntPtr proxy, String methodName, IntPtr parameters, DBusCallFlags flags, int timeoutMs, IntPtr cancel, IntPtr error);

            [DllImport("libgio-2.0.so.0")]
            private static extern IntPtr g_variant_builder_new(IntPtr type);

            [DllImport("libgio-2.0.so.0")]
            private static extern void g_variant_builder_add(IntPtr builder, String format/* "{sv}" */, String str, IntPtr variant);

            [DllImport("libgio-2.0.so.0")]
            private static extern void g_variant_builder_unref(IntPtr builder);

            [DllImport("libgio-2.0.so.0")]
            private static extern IntPtr g_variant_new_string(String str);

            [DllImport("libgio-2.0.so.0")]
            private static extern IntPtr g_variant_new_boolean(bool boolean);

            [DllImport("libgio-2.0.so.0")]
            private static extern IntPtr g_variant_new(String format/* "(sa{sv})" */, String str, IntPtr builder);

            [DllImport("libgio-2.0.so.0")]
            private static extern void g_variant_get(IntPtr variant, String format/* "(o)" */, ref String str);

            [DllImport("libgio-2.0.so.0")]
            private static extern void g_variant_get(IntPtr variant, String format/* "(u@a{sv})" */, ref uint number, ref IntPtr keyValuePairs);

            [DllImport("libgio-2.0.so.0")]
            private static extern uint g_dbus_connection_signal_subscribe(IntPtr connection, String sender, String interface_name, String member, String object_path, String arg0, DBusSignalFlags flags, GDBusSignalCallback callback, IntPtr user_data, IntPtr user_data_free_func);

            [DllImport("libgio-2.0.so.0")]
            private static extern void g_dbus_connection_signal_unsubscribe(IntPtr connection, uint id);

            [DllImport("libgio-2.0.so.0")]
            private static extern bool g_variant_lookup(IntPtr dictionary, String key, String format/* "s" */, ref String str);
        }
    }
}