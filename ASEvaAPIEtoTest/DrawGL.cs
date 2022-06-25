using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;
using ASEva.UIEto;
using SharpGL;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initDrawGL(StackLayout layout)
        {
            var glView = layout.AddControl(new GLView(), true) as GLView;
            var layoutBottom = layout.AddRowLayout();
            layoutBottom.AddLinkButton(t["draw-gl-pause-render"]).Click += delegate { glRenderSwitch = false; };
            layoutBottom.AddLinkButton(t["draw-gl-resume-render"]).Click += delegate { glRenderSwitch = true; };

            glView.GLInitialize += (o, args) =>
            {
                var gl = args.GL;

                gl.ShadeModel(OpenGL.GL_SMOOTH);
                gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
                gl.ClearDepth(1.0f);
                gl.Enable(OpenGL.GL_DEPTH_TEST);
                gl.DepthFunc(OpenGL.GL_LEQUAL);
                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
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

                gl.VertexPointer(3, 0, new float[]
                {
                    -0.5f, 0.8f, -1.0f,
                    0.5f, 0.8f, -1.0f,
                    0.5f, 0.75f, -1.0f,
                    -0.5f, 0.75f, -1.0f,
                    -0.5f, 0.7f, -0.999f,
                    0.5f, 0.7f, -0.999f,
                    0.5f, 0.75f, -0.999f,
                    -0.5f, 0.775f, -0.999f,
                });
                gl.ColorPointer(3, 0, new float[]
                {
                    1.0f, 0.0f, 0.0f,
                    1.0f, 0.0f, 0.0f,
                    1.0f, 0.0f, 0.0f,
                    1.0f, 0.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                });
                gl.DrawArrays(OpenGL.GL_QUADS, 0, 8);

                texts.Add(new GLTextTask
                {
                    text = "FPS: " + glView.FPS.ToString("F1"),
                    posX = 10,
                    posY = glViewSizeInfo.LogicalHeight - 10,
                    anchor = TextAnchor.BottomLeft,
                    red = 255,
                });

                var buttonRect = getDrawGLButtonRect();

                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.PushMatrix();
                {
                    gl.LoadIdentity();
                    gl.Ortho(0, glViewSizeInfo.LogicalWidth, glViewSizeInfo.LogicalHeight, 0, -1, 1);

                    gl.Begin(OpenGL.GL_QUADS);
                    {
                        gl.Color(1.0f, 1.0f, 0.5f, buttonRect.Contains(glView.GetMouseLogicalPoint()) ? 1.0f : 0.5f);
                        gl.Vertex(buttonRect.Left, buttonRect.Top, 0);
                        gl.Vertex(buttonRect.Right, buttonRect.Top, 0);
                        gl.Vertex(buttonRect.Right, buttonRect.Bottom, 0);
                        gl.Vertex(buttonRect.Left, buttonRect.Bottom, 0);
                    }
                    gl.End();
                }
                gl.PopMatrix();

                texts.Add(t["draw-gl-detail"], (int)buttonRect.Center.X, (int)buttonRect.Center.Y, 0, 0, 0, 1.5f);
            };

            glView.MouseDown += (o, args) =>
            {
                if (args.Buttons == MouseButtons.Primary && getDrawGLButtonRect().Contains(args.GetLogicalPoint()) && glView.ContextInfo != null)
                {
                    var info = glView.ContextInfo.Value;
                    var rowTexts = new List<String>();
                    rowTexts.Add(t.Format("draw-gl-info-version", info.version));
                    rowTexts.Add(t.Format("draw-gl-info-vendor", info.vendor));
                    rowTexts.Add(t.Format("draw-gl-info-renderer", info.renderer));
                    rowTexts.Add(t.Format("draw-gl-info-extensions", info.extensions.Length > 500 ? (info.extensions.Substring(0, 500) + " ...") : info.extensions));
                    MessageBox.Show(String.Join('\n', rowTexts), "");
                };
            };

            glViewTimer.Interval = 0.001;
            glViewTimer.Elapsed += delegate
            {
                if (glRenderSwitch) glView.QueueRender();
            };
            glViewTimer.Start();

            Closing += delegate
            {
                glViewTimer.Stop();
                glView.Close();
            };
        }

        private RectangleF getDrawGLButtonRect()
        {
            if (glViewSizeInfo == null) return new RectangleF();
            else return new RectangleF(glViewSizeInfo.LogicalWidth - 150, glViewSizeInfo.LogicalHeight - 50, 140, 40);
        }

        private DateTime glViewStartTime = DateTime.Now;
        private GLSizeInfo glViewSizeInfo;
        private UITimer glViewTimer = new UITimer();
        private bool glRenderSwitch = true;
    }
}