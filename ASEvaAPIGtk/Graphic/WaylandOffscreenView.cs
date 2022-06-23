using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;

namespace ASEva.UIGtk
{
    #pragma warning disable 612
    class WaylandOffscreenView : DrawingArea, GLView.GLViewBackend
    {
        public WaylandOffscreenView()
        {
            gl = OpenGL.Create(new LinuxFuncLoader());

            Realized += onRealized;
            Drawn += onDraw;
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
            if (Toplevel != null && Toplevel is Window && (Toplevel as Window).IsActive && !drawQueued)
            {
                QueueDraw();
                drawQueued = true;
            }
        }

        private void onDestroy()
        {
            IntPtr wlDisplay = Linux.gdk_wayland_display_get_wl_display(Display.Handle);
            IntPtr eglDisplay = Linux.eglGetDisplay(wlDisplay);

            if (context != IntPtr.Zero)
            {
                Linux.eglMakeCurrent(eglDisplay, eglSurface, eglSurface, context);
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
                Linux.eglDestroyContext(eglDisplay, context);
                context = IntPtr.Zero;
            }
            if (eglSurface != IntPtr.Zero)
            {
                Linux.eglDestroySurface(eglDisplay, eglSurface);
                eglSurface = IntPtr.Zero;
            }
            if (wlEglWindow != IntPtr.Zero)
            {
                Linux.wl_egl_window_destroy(wlEglWindow);
                wlEglWindow = IntPtr.Zero;
            }

            rendererStatusOK = false;
        }

        private void onRealized(object sender, EventArgs e)
        {
            IntPtr wlDisplay = Linux.gdk_wayland_display_get_wl_display(Display.Handle);
            if (wlDisplay == IntPtr.Zero) return;

            IntPtr eglDisplay = Linux.eglGetDisplay(wlDisplay);
            if (eglDisplay == IntPtr.Zero) return;

            var eglVersion = new int[] { 1, 4 };
            unsafe
            {
                fixed (int* major = &eglVersion[0], minor = &eglVersion[1])
                {
                    if (!Linux.eglInitialize(eglDisplay, major, minor)) return;
                }
            }

            if (!Linux.eglBindAPI(0x30A2/* EGL_OPENGL_API */)) return;

            var attribs = new int[]
            {
                0x3033/*EGL_SURFACE_TYPE*/, 0x0004/*EGL_WINDOW_BIT*/,
                0x3040/*EGL_RENDERABLE_TYPE*/, 0x0008/*EGL_OPENGL_BIT*/,
                0x3024/*EGL_RED_SIZE*/, 8,
                0x3023/*EGL_GREEN_SIZE*/, 8,
                0x3022/*EGL_BLUE_SIZE*/, 8,
                0x3038/*EGL_NONE*/
            };
            var configs = new IntPtr[1];
            var numConfig = new int[1];
            unsafe
            {
                fixed (int *attribsPtr = &(attribs[0]), numConfigPtr = &(numConfig[0]))
                {
                    fixed (IntPtr *configsPtr = &configs[0])
                    {
                        if (!Linux.eglChooseConfig(eglDisplay, attribsPtr, configsPtr, 1, numConfigPtr)) return;
                    }
                }
            }

            var wlSurface = Linux.gdk_wayland_window_get_wl_surface(Window.Handle);
            if (wlSurface == IntPtr.Zero) return;

            wlEglWindow = Linux.wl_egl_window_create(wlSurface, 16, 16);
            if (wlEglWindow == IntPtr.Zero) return;

            eglSurface = Linux.eglCreateWindowSurface(eglDisplay, configs[0], wlEglWindow, IntPtr.Zero);
            if (eglSurface == IntPtr.Zero) return;

            context = Linux.eglCreateContext(eglDisplay, configs[0], IntPtr.Zero, IntPtr.Zero);
            if (context == IntPtr.Zero) return;

            Linux.eglMakeCurrent(eglDisplay, eglSurface, eglSurface, context);
            
            if (Linux.glewInit(Window) != 0) return;

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;

                size = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth, AllocatedHeight, 1, (float)AllocatedWidth / AllocatedHeight);

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

            rendererStatusOK = true;
        }

        private void onDraw(object o, DrawnArgs args)
        {
            if (!rendererStatusOK) return;

            var curSize = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth, AllocatedHeight, 1, (float)AllocatedWidth / AllocatedHeight);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            IntPtr wlDisplay = Linux.gdk_wayland_display_get_wl_display(Display.Handle);
            IntPtr eglDisplay = Linux.eglGetDisplay(wlDisplay);

            Linux.eglMakeCurrent(eglDisplay, eglSurface, eglSurface, context);

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
        private IntPtr wlEglWindow = IntPtr.Zero;
        private IntPtr eglSurface = IntPtr.Zero;
        private uint[] frameBuffer = null;
        private uint[] colorBuffer = null;
        private uint[] depthBuffer = null;
        private byte[] hostBuffer = null;
        private Cairo.ImageSurface cairoSurface = null;
        private bool rendererStatusOK = false;
        private GLSizeInfo size = null;
        private bool drawQueued = false;
    }
}