using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;

namespace ASEva.UIGtk
{
    #pragma warning disable 612
    class X11OffscreenView : DrawingArea, GLView.GLViewBackend
    {
        public X11OffscreenView()
        {
            DoubleBuffered = false;

            Realized += onRealized;
            Drawn += onDraw;
        }

        public void SetCallback(GLView.GLViewCallback callback)
        {
            this.callback = callback;
        }

        public void InitializeGL()
        {
            gl = OpenGL.Create(new LinuxFuncLoader());

            initConditions[0] = true;
            onInitialize();
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

        private void onInitialize()
        {
            if (!initConditions[0] || !initConditions[1]) return;

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Window.Display.Handle);
            if (display == IntPtr.Zero) return;

            IntPtr visual = Linux.gdk_x11_visual_get_xvisual(Window.Visual.Handle);
            if (visual == IntPtr.Zero) return;

            int screen = Linux.gdk_x11_screen_get_screen_number(Window.Screen.Handle);
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

            var xid = Linux.gdk_x11_window_get_xid(Window.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            if (Linux.glewInit(Window) != 0) return;

            try
            {
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

                callback.OnGLInitialize(gl);
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

        private void onDestroy()
        {
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

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            Linux.glXDestroyContext(display, context);

            rendererStatusOK = false;
        }

        private void onRealized(object sender, EventArgs e)
        {
            initConditions[1] = true;
            onInitialize();
        }

        private void onDraw(object o, DrawnArgs args)
        {
            if (!rendererStatusOK) return;

            var curSize = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth, AllocatedHeight, 1, (float)AllocatedWidth / AllocatedHeight);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            var xid = Linux.gdk_x11_window_get_xid(Window.Handle);
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
                }

                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);

                if (resized) callback.OnGLResize(gl, size);

                var textTasks = new GLTextTasks();
                callback.OnGLRender(gl, textTasks);
                gl.Finish();

                gl.ReadPixels(0, 0, size.RealWidth, size.RealHeight, OpenGL.GL_RGBA, hostBuffer);
                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, 0);
                gl.DrawPixels(size.RealWidth, size.RealHeight, OpenGL.GL_RGBA, hostBuffer);
                gl.Finish();

                Linux.glXSwapIntervalEXT(display, xid, 0);
                Linux.glXSwapBuffers(display, xid);
                Linux.glXMakeCurrent(display, 0, IntPtr.Zero);

                CairoDrawText.Draw(args.Cr, textTasks.Clear(), size);
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
        private bool rendererStatusOK = false;
        private bool[] initConditions = new bool[2];
        private GLSizeInfo size = null;
        private bool drawQueued = false;
    }
}