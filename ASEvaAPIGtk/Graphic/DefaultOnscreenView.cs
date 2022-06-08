using System;
using Gtk;
using SharpGL;
using ASEva.UIEto;

namespace ASEva.UIGtk
{
    class DefaultOnscreenView : Overlay, GLView.GLViewBackend
    {
        public DefaultOnscreenView()
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

            glArea.Realized += onRealized;
            glArea.Render += onRenderer;
            drawArea.Drawn += onDraw;
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
            rendererStatusOK = false;
        }

        public void QueueRender()
        {
            if (Toplevel != null && Toplevel is Window && (Toplevel as Window).IsActive && !drawQueued)
            {
                glArea.QueueRender();
                drawQueued = true;
            }
        }

        private void onInitialize()
        {
            if (!initConditions[0] || !initConditions[1]) return;

            if (glArea.Context == null || glArea.Context.Handle == IntPtr.Zero) return;

            glArea.MakeCurrent();

            if (Linux.glewInit(Window) != 0) return;

            try
            {
                callback.OnGLInitialize(gl);
                gl.Flush();
            }
            catch (Exception)
            {
                return;
            }

            rendererStatusOK = true;
        }

        private void onRealized(object sender, EventArgs e)
        {
            initConditions[1] = true;
            onInitialize();
        }

        private void onRenderer(object o, RenderArgs args)
        {
            if (!rendererStatusOK) return;

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
        }

        private void onDraw(object o, DrawnArgs args)
        {
            CairoDrawText.Draw(args.Cr, texts, size);
        }

        private OpenGL gl = null;
        private GLView.GLViewCallback callback;
        private bool rendererStatusOK = false;
        private bool[] initConditions = new bool[2];
        private GLSizeInfo size = null;
        private bool drawQueued = false;

        private GLArea glArea = new GLArea();
        private DrawingArea drawArea = new DrawingArea();
        private GLTextTask[] texts = new GLTextTask[0];
    }
}