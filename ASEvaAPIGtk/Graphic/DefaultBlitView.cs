using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEva.UIGtk
{
    class DefaultBlitView : Overlay, GLBackend
    {
        public DefaultBlitView(GLCallback callback, GLAntialias antialias)
        {
            drawAreaGL.Visible = true;
            Add(drawAreaGL);

            this.callback = callback;
            this.gl = OpenGL.Create(new LinuxFuncLoader());
            this.antialias = antialias;

            drawAreaGL.Realized += onRealized;
            drawAreaGL.Drawn += onDraw;
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

        private void onRealized(object? sender, EventArgs e)
        {
            if (Window == null) return;

            try { glContext = Window.CreateGlContext(); }
            catch (Exception ex) { Dump.Exception(ex); }
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
                ctxInfo.extensions = String.Join(' ', gl.ExtensionList);

                size = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);

                if (!gl.ExtensionList.Contains("GL_EXT_framebuffer_object") || 
                    !gl.ExtensionList.Contains("GL_EXT_framebuffer_blit")/* gdk_cairo_draw_from_gl需要 */)
                {
                    onDestroy();
                    return;
                }

                if (antialias != GLAntialias.Disabled)
                {
                    if (gl.ExtensionList.Contains("GL_EXT_framebuffer_multisample"))
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

                callback.OnGLInitialize(gl, ctxInfo);
                callback.OnGLResize(gl, size);
                
                gl.Flush();
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                onDestroy();
                return;
            }

            rendererStatusOK = true;
        }

        private void onDraw(object? o, DrawnArgs args)
        {
            if (!rendererStatusOK) return;
            if (colorBuffer == null || depthBuffer == null || frameBuffer == null || glContext == null) return;

            var passiveDraw = !drawQueued;

            var moduleID = callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            var curSize = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);
            bool resized = size == null || curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            if (resized) passiveDraw = false;

            if (passiveDraw)
            {
                try
                {
                    Linux.gdk_cairo_draw_from_gl(args.Cr.Handle, Window.Handle, (int)colorBuffer[antialias == GLAntialias.Disabled ? 0 : 1], (int)OpenGL.GL_RENDERBUFFER, (int)curSize.RealPixelScale, 0, 0, curSize.RealWidth, curSize.RealHeight);
                }
                catch (Exception ex)
                {
                    Dump.Exception(ex);
                    rendererStatusOK = false;
                }
            }
            else
            {
                glContext.MakeCurrent();

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
                    }

                    gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);

                    if (resized) callback.OnGLResize(gl, size);

                    var textTasks = new GLTextTasks();
                    callback.OnGLRender(gl, textTasks);
                    texts = textTasks.Clear();

                    if (texts.Length > 0 && drawAreaText == null)
                    {
                        drawAreaText = new DrawingArea();
                        drawAreaText.Visible = true;
                        AddOverlay(drawAreaText);
                        drawAreaText.Drawn += onDrawText;
                    }

                    gl.Finish();

                    if (antialias != GLAntialias.Disabled)
                    {
                        gl.BindFramebufferEXT(OpenGL.GL_READ_FRAMEBUFFER_EXT, frameBuffer[0]);
                        gl.BindFramebufferEXT(OpenGL.GL_DRAW_FRAMEBUFFER_EXT, frameBuffer[1]);
                        gl.BlitFramebufferEXT(0, 0, size.RealWidth, size.RealHeight, 0, 0, size.RealWidth, size.RealHeight, OpenGL.GL_COLOR_BUFFER_BIT, OpenGL.GL_NEAREST);
                    }

                    Linux.gdk_cairo_draw_from_gl(args.Cr.Handle, Window.Handle, (int)colorBuffer[antialias == GLAntialias.Disabled ? 0 : 1], (int)OpenGL.GL_RENDERBUFFER, (int)curSize.RealPixelScale, 0, 0, curSize.RealWidth, curSize.RealHeight);
                }
                catch (Exception ex)
                {
                    Dump.Exception(ex);
                    rendererStatusOK = false;
                }
            }

            drawQueued = false;
            DrawBeat.CallbackEnd(this);
        }

        private void onDrawText(object? o, DrawnArgs args)
        {
            if (texts.Length == 0 || size == null) return;
            CairoDrawText.Draw(args.Cr, texts, size);
        }

        private void onDestroy()
        {
            if (glContext != null)
            {
                glContext.MakeCurrent();
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

            if (glContext != null)
            {
                glContext.Dispose();
                glContext = null;
            }

            rendererStatusOK = false;
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

        private DrawingArea drawAreaGL = new DrawingArea();
        private DrawingArea? drawAreaText = null;

        private GLTextTask[] texts = [];

        private OpenGL gl;
        private GLCallback callback;
        private GLAntialias antialias;
        private bool rendererStatusOK = false;
        private GLSizeInfo? size = null;
        private bool drawQueued = false;

        private Gdk.GLContext? glContext = null;
        private uint[]? frameBuffer = null;
        private uint[]? colorBuffer = null;
        private uint[]? depthBuffer = null;
    }
}