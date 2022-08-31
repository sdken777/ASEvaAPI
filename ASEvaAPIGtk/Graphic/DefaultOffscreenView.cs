using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEva.UIGtk
{
    class DefaultOffscreenView : Overlay, GLBackend
    {
        public DefaultOffscreenView()
        {
            glArea.Visible = true;
            glArea.UseEs = false;
            glArea.HasStencilBuffer = false;
            glArea.HasDepthBuffer = true;
            glArea.HasAlpha = false;
            glArea.AutoRender = false;
            Add(glArea);

            drawArea.Visible = true;
            AddOverlay(drawArea);

            gl = OpenGL.Create(new LinuxFuncLoader());

            glArea.Realized += onRealized;
            glArea.Render += onRenderer;
            drawArea.Drawn += onDraw;
        }

        public void SetCallback(GLCallback callback)
        {
            this.callback = callback;
        }

        public void ReleaseGL()
        {
            rendererStatusOK = false;
        }

        public void QueueRender()
        {
            if (Toplevel != null && Toplevel is Window && !(Toplevel as Window).StateFlags.HasFlag(StateFlags.Backdrop) && !drawQueued && DrawBeat.CallerBegin(this))
            {
                glArea.QueueRender();
                drawQueued = true;
                DrawBeat.CallerEnd(this);
            }
        }

        private void onRealized(object sender, EventArgs e)
        {
            if (glArea.Context == null || glArea.Context.Handle == IntPtr.Zero) return;

            glArea.MakeCurrent();

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;

                callback.OnGLInitialize(gl, ctxInfo);
                gl.Flush();
            }
            catch (Exception)
            {
                return;
            }

            rendererStatusOK = true;
        }

        private void onRenderer(object o, RenderArgs args)
        {
            if (!rendererStatusOK) return;

            var moduleID = callback == null ? null : callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            glArea.MakeCurrent();

            try
            {
                var curSize = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);
                if (size == null || curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight)
                {
                    size = curSize;
                    callback.OnGLResize(gl, size);
                }

                var textTasks = new GLTextTasks();
                callback.OnGLRender(gl, textTasks);
                texts = textTasks.Clear();
                drawArea.QueueDraw();

                gl.Finish();
            }
            catch (Exception)
            {
                rendererStatusOK = false;
            }

            Gdk.GLContext.ClearCurrent();

            drawQueued = false;
            DrawBeat.CallbackEnd(this);
        }

        private void onDraw(object o, DrawnArgs args)
        {
            CairoDrawText.Draw(args.Cr, texts, size);
        }

        private OpenGL gl = null;
        private GLCallback callback;
        private bool rendererStatusOK = false;
        private GLSizeInfo size = null;
        private bool drawQueued = false;

        private GLArea glArea = new GLArea();
        private DrawingArea drawArea = new DrawingArea();
        private GLTextTask[] texts = new GLTextTask[0];
    }
}