using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEva.UIGtk
{
    #pragma warning disable 612
    class X11OnscreenView : DrawingArea, GLView.GLViewBackend
    {
        public X11OnscreenView()
        {
            gl = OpenGL.Create(new LinuxFuncLoader());

            DoubleBuffered = false;

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
            if (context != IntPtr.Zero)
            {
                Linux.glXDestroyContext(display, context);
                context = IntPtr.Zero;
            }

            rendererStatusOK = false;
        }

        private void onRealized(object sender, EventArgs e)
        {
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

            xid = Linux.gdk_x11_window_get_xid(Window.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            if (Linux.glewInit(Window) != 0) return;

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;
                
                size = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);

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

            var curSize = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            try
            {
                if (resized) callback.OnGLResize(gl, size);

                var dummy = new GLTextTasks(); // X11在屏渲染不支持文字绘制
                callback.OnGLRender(gl, dummy);
                gl.Finish();
            }
            catch (Exception)
            {
                onDestroy();
            }

            Linux.glXSwapIntervalEXT(display, xid, 0);
            Linux.glXSwapBuffers(display, xid);
            Linux.glXMakeCurrent(display, 0, IntPtr.Zero);

            drawQueued = false;
            DrawBeat.CallbackEnd(this);
        }

        private OpenGL gl = null;
        private GLView.GLViewCallback callback;
        private IntPtr context = IntPtr.Zero;
        private uint xid = 0;
        private bool rendererStatusOK = false;
        private GLSizeInfo size = null;
        private bool drawQueued = false;
    }
}