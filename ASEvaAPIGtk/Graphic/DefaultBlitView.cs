using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;
using System.Runtime.InteropServices;

namespace ASEva.UIGtk
{
    class DefaultBlitView : DrawingArea, GLBackend
    {
        public DefaultBlitView()
        {
            gl = OpenGL.Create(new LinuxFuncLoader());

            setLegacyGL();

            Realized += onRealized;
            Drawn += onDraw;
        }

        public void SetCallback(GLCallback callback)
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
            if (!drawQueued && DrawBeat.CallerBegin(this))
            {
                QueueDraw();
                drawQueued = true;
                DrawBeat.CallerEnd(this);
            }
        }

        private void onRealized(object sender, EventArgs e)
        {
            if (Window == null) return;

            glContext = Window.CreateGlContext();
            if (glContext == null) return;

            glContext.SetUseEs(0);
            glContext.SetRequiredVersion(0, 0);
            if (!glContext.Realize()) return;

            glContext.MakeCurrent();

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;

                size = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);

                if (!gl.ExtensionList.Contains("GL_EXT_framebuffer_object") || 
                    !gl.ExtensionList.Contains("GL_EXT_framebuffer_blit")/* gdk_cairo_draw_from_gl需要 */)
                {
                    onDestroy();
                    return;
                }

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

            var passiveDraw = !drawQueued;

            var moduleID = callback == null ? null : callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            var curSize = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            if (resized) passiveDraw = false;

            if (passiveDraw)
            {
                try
                {
                    Linux.gdk_cairo_draw_from_gl(args.Cr.Handle, Window.Handle, (int)colorBuffer[0], (int)OpenGL.GL_RENDERBUFFER, (int)curSize.RealPixelScale, 0, 0, curSize.RealWidth, curSize.RealHeight);
                }
                catch (Exception)
                {
                    rendererStatusOK = false;
                }
            }
            else
            {
                glContext.MakeCurrent();

                GLTextTask[] texts = new GLTextTask[0];
                try
                {
                    if (resized)
                    {
                        gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                        gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                        gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                        gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    }

                    gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                    gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                    gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);

                    if (resized) callback.OnGLResize(gl, size);

                    var dummy = new GLTextTasks();
                    callback.OnGLRender(gl, dummy);

                    gl.Finish();

                    Linux.gdk_cairo_draw_from_gl(args.Cr.Handle, Window.Handle, (int)colorBuffer[0], (int)OpenGL.GL_RENDERBUFFER, (int)curSize.RealPixelScale, 0, 0, curSize.RealWidth, curSize.RealHeight);
                }
                catch (Exception)
                {
                    rendererStatusOK = false;
                }
            }

            drawQueued = false;
            DrawBeat.CallbackEnd(this);
        }

        private void onDestroy()
        {
            if (glContext != null)
            {
                glContext.MakeCurrent();
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

            if (glContext != null)
            {
                glContext.Dispose();
                glContext = null;
            }

            rendererStatusOK = false;
        }

        private struct VTable
        {
            public IntPtr dummy1;
            public IntPtr dummy2;
            public IntPtr dummy3;
            public IntPtr dummy4;
            public IntPtr dummy5;
            public IntPtr gdk_gl_set_flags;
        }

        private delegate void GdkGlSetFlagsDelegate(int gdkGlFlags);

        private void setLegacyGL()
        {
            IntPtr vTablePtr = Linux.gdk__private__();
            if (vTablePtr == IntPtr.Zero) return;

            unsafe
            {
                var vTable = *(VTable*)vTablePtr;
                Type delegateType = typeof(GdkGlSetFlagsDelegate);
                var gdkGlSetFlags = Marshal.GetDelegateForFunctionPointer(vTable.gdk_gl_set_flags, delegateType) as GdkGlSetFlagsDelegate;
                gdkGlSetFlags(1 << 5/* LEGACY */);
            }
        }

        private OpenGL gl = null;
        private GLCallback callback;
        private bool rendererStatusOK = false;
        private GLSizeInfo size = null;
        private bool drawQueued = false;

        private Gdk.GLContext glContext = null;
        private uint[] frameBuffer = null;
        private uint[] colorBuffer = null;
        private uint[] depthBuffer = null;
    }
}