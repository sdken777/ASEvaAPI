using System;
using Eto.Forms;
using ASEva.UIEto;
using SharpGL;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initDrawGL(StackLayout layout)
        {
            var glView = layout.AddControl(new GLView(), true) as GLView;

            glView.GLInitialize += (o, args) =>
            {
                var gl = args.GL;

                gl.ShadeModel(OpenGL.GL_SMOOTH);
                gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
                gl.ClearDepth(1.0f);
                gl.Enable(OpenGL.GL_DEPTH_TEST);
                gl.DepthFunc(OpenGL.GL_LEQUAL);
                gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
                gl.EnableClientState(OpenGL.GL_COLOR_ARRAY);
                gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);
            };

            glView.GLResize += (o, args) =>
            {
                var gl = args.GL;
                glViewSizeInfo = args.SizeInfo;
                
                gl.Viewport(0, 0, glViewSizeInfo.RealWidth, glViewSizeInfo.RealHeight);
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();
                gl.Perspective(90.0f, glViewSizeInfo.AspectRatio, 0.1f, 100.0f);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
            };

            glView.GLRender += (o, args) =>
            {
                var gl = args.GL;
                var texts = args.TextTasks;

                var offset = (float)Math.Cos((DateTime.Now - glViewStartTime).TotalSeconds * 3) * 0.5f;

                gl.ClearColor(0.0f, 0.0f, 0.25f + offset * 0.5f, 1.0f);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                gl.VertexPointer(3, 0, new float[]
                {
                    0.0f + offset, 0.5f, -1.0f,
                    -0.5f + offset, -0.5f, -1.0f,
                    0.5f + offset, -0.5f, -1.0f,
                    0.0f - offset, 0.5f, -2.0f,
                    -0.5f - offset, -0.5f, -2.0f,
                    0.5f - offset, -0.5f, -2.0f
                });
                gl.ColorPointer(3, 0, new float[]
                {
                    1.0f, 0.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 0.0f, 1.0f,
                    0.0f, 0.0f, 1.0f,
                    0.0f, 0.0f, 1.0f,
                });
                gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 6);

                var centerX = glViewSizeInfo.LogicalWidth / 2;
                var centerY = glViewSizeInfo.LogicalHeight / 2;

                var textBrightness = (byte)(128 - 200 * offset);
                texts.Add("Hello 天气不错", centerX - (int)(100 * offset), centerY, textBrightness, textBrightness, textBrightness, 3.0f);
                texts.Add("UPPER BOUND", centerX, centerY - 20, 255, 255, 255, 0.5f);
                texts.Add("LOWER BOUND", centerX, centerY + 20, 255, 255, 255, 0.5f);
            };

            glViewTimer.Interval = 0.001;
            glViewTimer.Elapsed += delegate
            {
                glView.QueueRender();
            };
            glViewTimer.Start();

            Closing += delegate
            {
                glViewTimer.Stop();
                glView.Close();
            };
        }

        private DateTime glViewStartTime = DateTime.Now;
        private GLSizeInfo glViewSizeInfo;
        private UITimer glViewTimer = new UITimer();
    }
}