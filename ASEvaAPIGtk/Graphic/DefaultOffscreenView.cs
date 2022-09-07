using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEva.UIGtk
{
    class DefaultOffscreenView : DrawingArea, GLBackend
    {
        public DefaultOffscreenView()
        {
            gl = OpenGL.Create(new LinuxFuncLoader());

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

            try { glContext = Window.CreateGlContext(); }
            catch (Exception) {}
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

                if (!gl.ExtensionList.Contains("GL_EXT_framebuffer_object"))
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

            var curSize = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

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
                    hostBuffer = new byte[size.RealWidth * size.RealHeight * 4];
                    if (cairoSurface != null)
                    {
                        cairoSurface.Dispose();
                        cairoSurface = null;
                    }
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
                rendererStatusOK = false;
            }

            Gdk.GLContext.ClearCurrent();

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
            if (cairoSurface != null)
            {
                cairoSurface.Dispose();
                cairoSurface = null;
            }

            if (glContext != null)
            {
                glContext.Dispose();
                glContext = null;
            }

            rendererStatusOK = false;
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
        private byte[] hostBuffer = null;
        private Cairo.ImageSurface cairoSurface = null;
    }
}