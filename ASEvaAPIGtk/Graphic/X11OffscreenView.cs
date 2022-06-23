using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;

namespace ASEva.UIGtk
{
    #pragma warning disable 612
    class X11OffscreenView : Box, GLView.GLViewBackend
    {
        public X11OffscreenView() : base(Orientation.Vertical, 0)
        {
            gl = OpenGL.Create(new LinuxFuncLoader());

            dummyArea.HeightRequest = 1;

            PackStart(realArea, true, true, 0);
            PackStart(dummyArea, false, false, 0);

            dummyArea.Realized += onRealized;
            realArea.Drawn += onDraw;
        }

        public void SetCallback(GLView.GLViewCallback callback)
        {
            this.callback = callback;
        }

        public void ReleaseGL()
        {
            if (!rendererStatusOK) return;
            onDestroy();
        }

        public void QueueRender()
        {
            if (Toplevel != null && Toplevel is Window && !(Toplevel as Window).Window.State.HasFlag(Gdk.WindowState.Iconified) && !drawQueued)
            {
                QueueDraw();
                drawQueued = true;
            }
        }

        private void onDestroy()
        {
            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            var xid = Linux.gdk_x11_window_get_xid(dummyArea.Window.Handle);

            if (context != IntPtr.Zero)
            {
                Linux.glXMakeCurrent(display, xid, context);
            }

            if (frameBuffer != null)
            {
                gl.DeleteFramebuffersEXT(1, frameBuffer);
                frameBuffer = null;
            }
            if (colorBuffer != null)
            {
                gl.DeleteRenderbuffersEXT(1, colorBuffer);
                colorBuffer = null;
            }
            if (depthBuffer != null)
            {
                gl.DeleteRenderbuffersEXT(1, depthBuffer);
                depthBuffer = null;
            }
            if (cairoSurface != null)
            {
                cairoSurface.Dispose();
                cairoSurface = null;
            }
            if (context != IntPtr.Zero)
            {
                Linux.glXDestroyContext(display, context);
                context = IntPtr.Zero;
            }

            rendererStatusOK = false;
        }

        private void onRealized(object sender, EventArgs e)
        {
            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            if (display == IntPtr.Zero) return;

            IntPtr visual = Linux.gdk_x11_visual_get_xvisual(dummyArea.Window.Visual.Handle);
            if (visual == IntPtr.Zero) return;

            int screen = Linux.gdk_x11_screen_get_screen_number(dummyArea.Window.Screen.Handle);
            uint visualID = Linux.XVisualIDFromVisual(visual);

            var vinfoTemplate = new XVisualInfo[1];
            vinfoTemplate[0].visualid = visualID;
            vinfoTemplate[0].screen = screen;

            var dummy = new int[1];
            IntPtr vinfo = IntPtr.Zero;
            unsafe
            {
                fixed (XVisualInfo *vtemplate = &(vinfoTemplate[0]))
                {
                    fixed (int *count = &(dummy[0]))
                    {
                        vinfo = Linux.XGetVisualInfo(display, 3, vtemplate, count);
                    }
                }
            }
            if (vinfo == IntPtr.Zero) return;

            context = Linux.glXCreateContext(display, vinfo, IntPtr.Zero, true);
            if (context == IntPtr.Zero) return;

            var xid = Linux.gdk_x11_window_get_xid(dummyArea.Window.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            if (Linux.glewInit(dummyArea.Window) != 0) return;

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;
                
                size = new GLSizeInfo(realArea.AllocatedWidth, realArea.AllocatedHeight, realArea.AllocatedWidth, realArea.AllocatedHeight, 1, (float)realArea.AllocatedWidth / realArea.AllocatedHeight);

                colorBuffer = new uint[1];
                gl.GenRenderbuffersEXT(1, colorBuffer);
                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);

                depthBuffer = new uint[1];
                gl.GenRenderbuffersEXT(1, depthBuffer);
                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);

                frameBuffer = new uint[1];
                gl.GenFramebuffersEXT(1, frameBuffer);
                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);

                if (gl.CheckFramebufferStatusEXT(OpenGL.GL_FRAMEBUFFER_EXT) != OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT)
                {
                    onDestroy();
                    return;
                }

                hostBuffer = new byte[size.RealWidth * size.RealHeight * 4];
                cairoSurface = new Cairo.ImageSurface(Cairo.Format.RGB24, size.RealWidth, size.RealHeight);

                callback.OnGLInitialize(gl, ctxInfo);
                callback.OnGLResize(gl, size);

                gl.Flush();
            }
            catch (Exception)
            {
                onDestroy();
                return;
            }

            dummyArea.Visible = false;
            rendererStatusOK = true;
        }

        private void onDraw(object o, DrawnArgs args)
        {
            if (!rendererStatusOK) return;

            var curSize = new GLSizeInfo(realArea.AllocatedWidth, realArea.AllocatedHeight, realArea.AllocatedWidth, realArea.AllocatedHeight, 1, (float)realArea.AllocatedWidth / realArea.AllocatedHeight);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            var xid = Linux.gdk_x11_window_get_xid(dummyArea.Window.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            try
            {
                if (resized)
                {
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    hostBuffer = new byte[size.RealWidth * size.RealHeight * 4];
                    if (cairoSurface != null) cairoSurface.Dispose();
                    cairoSurface = new Cairo.ImageSurface(Cairo.Format.RGB24, size.RealWidth, size.RealHeight);
                }

                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);

                if (resized) callback.OnGLResize(gl, size);

                var textTasks = new GLTextTasks();
                callback.OnGLRender(gl, textTasks);
                gl.Finish();

                var cairoWidth = size.RealWidth;
                var cairoHeight = size.RealHeight;
                var cairoSurfaceStep = cairoSurface.Stride;

                gl.ReadPixels(0, 0, cairoWidth, cairoHeight, OpenGL.GL_RGBA, hostBuffer);

                unsafe
                {
                    byte *surfaceData = (byte*)cairoSurface.DataPtr;
                    fixed (byte *srcData = &(hostBuffer[0]))
                    {
                        for (int v = 0; v < cairoHeight; v++)
                        {
                            uint *srcRow = (uint*)&srcData[v * cairoWidth * 4];
                            uint *dstRow = (uint*)&surfaceData[(cairoHeight - 1 - v) * cairoSurfaceStep];
                            for (int u = 0; u < cairoWidth; u++)
                            {
                                dstRow[u] = ((srcRow[u] & 0x000000ff) << 16) | (srcRow[u] & 0x0000ff00) | ((srcRow[u] & 0x00ff0000) >> 16) | 0xff000000;
                            }
                        }
                    }
                }

                var cairo = args.Cr;
                cairo.SetSourceSurface(cairoSurface, 0, 0);
                cairo.Paint();

                CairoDrawText.Draw(cairo, textTasks.Clear(), size);
            }
            catch (Exception)
            {
                onDestroy();
                return;
            }

            drawQueued = false;
        }

        private OpenGL gl = null;
        private GLView.GLViewCallback callback;
        private IntPtr context = IntPtr.Zero;
        private uint[] frameBuffer = null;
        private uint[] colorBuffer = null;
        private uint[] depthBuffer = null;
        private byte[] hostBuffer = null;
        private Cairo.ImageSurface cairoSurface = null;
        private bool rendererStatusOK = false;
        private GLSizeInfo size = null;
        private bool drawQueued = false;
        private DrawingArea realArea = new DrawingArea();
        private DrawingArea dummyArea = new DrawingArea();
    }
}