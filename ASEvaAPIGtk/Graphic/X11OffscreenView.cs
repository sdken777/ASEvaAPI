using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEva.UIGtk
{
    #pragma warning disable 612
    class X11OffscreenView : Box, GLBackend
    {
        public X11OffscreenView(GLCallback callback, GLAntialias antialias) : base(Orientation.Vertical, 0)
        {
            this.callback = callback;
            this.antialias = antialias;
            this.gl = OpenGL.Create(new LinuxFuncLoader());

            dummyArea.WidthRequest = 1;
            dummyArea.HeightRequest = 1;

            PackStart(realArea, true, true, 0);

            var dummyBox = new Box(Orientation.Horizontal, 0);
            dummyBox.PackStart(dummyArea, false, false, 0);
            PackStart(dummyBox, false, false, 0);

            dummyArea.Realized += onRealized;
            realArea.Drawn += onDraw;
        }

        public void ReleaseGL()
        {
            if (!rendererStatusOK) return;
            onDestroy();
        }

        public void QueueRender()
        {
            if (Toplevel != null && Toplevel is Window && !(Toplevel as Window).Window.State.HasFlag(Gdk.WindowState.Iconified) && !drawQueued && DrawBeat.CallerBegin(this))
            {
                QueueDraw();
                drawQueued = true;
                DrawBeat.CallerEnd(this);
            }
        }

        private void onDestroy()
        {
            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);

            if (context != IntPtr.Zero)
            {
                Linux.glXMakeCurrent(display, xid, context);
            }

            if (frameBuffer != null)
            {
                gl.DeleteFramebuffersEXT((uint)frameBuffer.Length, frameBuffer);
                frameBuffer = null;
            }
            if (colorBuffer != null)
            {
                gl.DeleteRenderbuffersEXT((uint)colorBuffer.Length, colorBuffer);
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

            xid = Linux.gdk_x11_window_get_xid(dummyArea.Window.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;
                
                size = new GLSizeInfo(realArea.AllocatedWidth, realArea.AllocatedHeight, realArea.AllocatedWidth * realArea.ScaleFactor, realArea.AllocatedHeight * realArea.ScaleFactor, realArea.ScaleFactor, (float)realArea.AllocatedWidth / realArea.AllocatedHeight);

                if (!gl.ExtensionList.Contains("GL_EXT_framebuffer_object"))
                {
                    onDestroy();
                    return;
                }

                if (antialias != GLAntialias.Disabled)
                {
                    if (gl.ExtensionList.Contains("GL_EXT_framebuffer_multisample") && gl.ExtensionList.Contains("GL_EXT_framebuffer_blit"))
                    {
                        var maxSamples = new int[1];
                        gl.GetInteger(OpenGL.GL_MAX_SAMPLES_EXT, maxSamples);
                        
                        if (antialias == GLAntialias.Sample16x && maxSamples[0] < 16) antialias = GLAntialias.Sample8x;
                        if (antialias == GLAntialias.Sample8x && maxSamples[0] < 8) antialias = GLAntialias.Sample4x;
                        if (antialias == GLAntialias.Sample4x && maxSamples[0] < 4) antialias = GLAntialias.Sample2x;
                        if (antialias == GLAntialias.Sample2x && maxSamples[0] < 2) antialias = GLAntialias.Disabled;
                    }
                    else antialias = GLAntialias.Disabled;
                }

                colorBuffer = new uint[antialias == GLAntialias.Disabled ? 1 : 2];
                gl.GenRenderbuffersEXT((uint)colorBuffer.Length, colorBuffer);
                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                if (antialias != GLAntialias.Disabled)
                {
                    gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[1]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                }
                else gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);

                depthBuffer = new uint[1];
                gl.GenRenderbuffersEXT(1, depthBuffer);
                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                if (antialias == GLAntialias.Disabled) gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                else gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);

                frameBuffer = new uint[antialias == GLAntialias.Disabled ? 1 : 2];
                gl.GenFramebuffersEXT((uint)frameBuffer.Length, frameBuffer);
                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);
                if (antialias != GLAntialias.Disabled)
                {
                    gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[1]);
                    gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[1]);
                }

                if (gl.CheckFramebufferStatusEXT(OpenGL.GL_FRAMEBUFFER_EXT) != OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT)
                {
                    onDestroy();
                    return;
                }

                hostBuffer = new byte[size.RealWidth * size.RealHeight * 4];

                if (cairoSurface == null || cairoSurface.Width != size.RealWidth || cairoSurface.Height != size.RealHeight)
                {
                    if (cairoSurface != null)
                    {
                        cairoSurface.Dispose();
                        cairoSurface = null;
                    }
                    cairoSurface = new Cairo.ImageSurface(Cairo.Format.RGB24, size.RealWidth, size.RealHeight);
                }

                callback.OnGLInitialize(gl, ctxInfo);
                callback.OnGLResize(gl, size);

                gl.Flush();
            }
            catch (Exception)
            {
                onDestroy();
                return;
            }

            rendererStatusOK = true;
        }

        private void onDraw(object o, DrawnArgs args)
        {
            if (!rendererStatusOK) return;

            var moduleID = callback == null ? null : callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            var curSize = new GLSizeInfo(realArea.AllocatedWidth, realArea.AllocatedHeight, realArea.AllocatedWidth * realArea.ScaleFactor, realArea.AllocatedHeight * realArea.ScaleFactor, realArea.ScaleFactor, (float)realArea.AllocatedWidth / realArea.AllocatedHeight);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            try
            {
                if (resized)
                {
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                    if (antialias != GLAntialias.Disabled)
                    {
                        gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                        gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[1]);
                        gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                    }
                    else gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);

                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                    if (antialias != GLAntialias.Disabled)
                    {
                        gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    }
                    else gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    
                    hostBuffer = new byte[size.RealWidth * size.RealHeight * 4];
                    if (cairoSurface != null)
                    {
                        cairoSurface.Dispose();
                        cairoSurface = null;
                    }
                    cairoSurface = new Cairo.ImageSurface(Cairo.Format.RGB24, size.RealWidth, size.RealHeight);
                }

                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);

                if (resized) callback.OnGLResize(gl, size);

                var textTasks = new GLTextTasks();
                callback.OnGLRender(gl, textTasks);
                gl.Finish();

                if (antialias != GLAntialias.Disabled)
                {
                    gl.BindFramebufferEXT(OpenGL.GL_READ_FRAMEBUFFER_EXT, frameBuffer[0]);
                    gl.BindFramebufferEXT(OpenGL.GL_DRAW_FRAMEBUFFER_EXT, frameBuffer[1]);
                    gl.BlitFramebufferEXT(0, 0, size.RealWidth, size.RealHeight, 0, 0, size.RealWidth, size.RealHeight, OpenGL.GL_COLOR_BUFFER_BIT, OpenGL.GL_NEAREST);
                    gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[1]);
                }

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
                cairo.Save();
                var cairoScale = 1.0 / size.RealPixelScale;
                cairo.Scale(cairoScale, cairoScale);
                cairo.SetSourceSurface(cairoSurface, 0, 0);
                cairo.Paint();
                cairo.Restore();

                CairoDrawText.Draw(cairo, textTasks.Clear(), size);
            }
            catch (Exception)
            {
                onDestroy();
            }

            drawQueued = false;
            DrawBeat.CallbackEnd(this);
        }

        private int getSampleCount(GLAntialias antialias)
        {
            switch (antialias)
            {
                case GLAntialias.Sample2x:
                    return 2;
                case GLAntialias.Sample4x:
                    return 4;
                case GLAntialias.Sample8x:
                    return 8;
                case GLAntialias.Sample16x:
                    return 16;
                default:
                    return 0;
            }
        }

        private OpenGL gl = null;
        private GLCallback callback;
        private IntPtr context = IntPtr.Zero;
        private uint xid = 0;
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
        private GLAntialias antialias;
    }
}