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
        private void initDrawGL(StackLayout layout, bool onscreenRendering)
        {
            bool requestOverlay, drawText;
            requestOverlay = drawText = !onscreenRendering; // For onscreen rendering, control overlay and text drawing is not required / 在屏渲染时不要求支持控件覆盖和绘制文本

            glView = new GLView(null, GLAntialias.Sample16x, onscreenRendering, requestOverlay, drawText, true);
            var button = new Button { Text = t["draw-gl-detail"]};

            if (glView.SupportOverlay)
            {
                var overlay = layout.AddControl(new OverlayLayout(), true) as OverlayLayout;
                overlay.AddControl(glView, 0, 0, 0, 0);
                overlay.AddControl(button, null, 10, null, 10);
            }
            else layout.AddControl(glView, true);

            var layoutBottom = layout.AddRowLayout();
            layoutBottom.AddLinkButton(t["draw-gl-pause-render"]).Click += delegate { glRenderSwitch = false; };
            layoutBottom.AddLinkButton(t["draw-gl-resume-render"]).Click += delegate { glRenderSwitch = true; };
            layoutBottom.AddSpace();
            var labelMouseCount = layoutBottom.AddLabel("0");

            if (!glView.SupportOverlay)
            {
                layoutBottom.AddSpace();
                layoutBottom.AddControl(button);
            }

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
                if (glViewSizeInfo.VerticalInverted) gl.Scale(1, -1, 1);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
            };

            glView.GLRender += (o, args) =>
            {
                var gl = args.GL;
                var texts = args.TextTasks;

                var offset = (float)Math.Cos((DateTime.Now - startTime).TotalSeconds * 3) * 0.5f;

                gl.ClearColor(0.0f, 0.0f, 0.25f + offset * 0.5f, 1.0f);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                var triangleVertexData = new float[]
                {
                    0.0f + offset, 0.5f, -1.0f,
                    -0.5f + offset, -0.5f, -1.0f,
                    0.5f + offset, -0.5f, -1.0f,
                    0.0f - offset, 0.5f, -2.0f,
                    -0.5f - offset, -0.5f, -2.0f,
                    0.5f - offset, -0.5f, -2.0f
                };
                var triangleColorData = new float[]
                {
                    1.0f, 0.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 0.0f, 1.0f,
                    0.0f, 0.0f, 1.0f,
                    0.0f, 0.0f, 1.0f,
                };
                unsafe
                {
                    fixed (float* triangleVertexDataPtr = &(triangleVertexData[0]), triangleColorDataPtr = &(triangleColorData[0]))
                    {
                        gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)triangleVertexDataPtr);
                        gl.ColorPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)triangleColorDataPtr);
                        gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 6);
                    }
                }

                var centerX = glViewSizeInfo.LogicalWidth / 2;
                var centerY = glViewSizeInfo.LogicalHeight / 2;

                var textBrightness = (byte)(128 - 200 * offset);
                texts.Add("Hello 天气不错", centerX - (int)(100 * offset), centerY, textBrightness, textBrightness, textBrightness, 3.0f);
                texts.Add("UPPER BOUND", centerX, centerY - 20, 255, 255, 255, 0.5f);
                texts.Add("LOWER BOUND", centerX, centerY + 20, 255, 255, 255, 0.5f);

                var quadVertexData = new float[]
                {
                    -0.5f, 0.8f, -1.0f,
                    0.5f, 0.8f, -1.0f,
                    0.5f, 0.75f, -1.0f,
                    -0.5f, 0.75f, -1.0f,
                    -0.5f, 0.7f, -0.999f,
                    0.5f, 0.7f, -0.999f,
                    0.5f, 0.75f, -0.999f,
                    -0.5f, 0.775f, -0.999f,
                };
                var quadColorData = new float[]
                {
                    1.0f, 0.0f, 0.0f,
                    1.0f, 0.0f, 0.0f,
                    1.0f, 0.0f, 0.0f,
                    1.0f, 0.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                    0.0f, 1.0f, 0.0f,
                };
                unsafe
                {
                    fixed (float* quadVertexDataPtr = &(quadVertexData[0]), quadColorDataPtr = &(quadColorData[0]))
                    {
                        gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)quadVertexDataPtr);
                        gl.ColorPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)quadColorDataPtr);
                        gl.DrawArrays(OpenGL.GL_QUADS, 0, 8);
                    }
                }

                texts.Add(new GLTextTask
                {
                    text = "FPS: " + glView.FPS.ToString("F1"),
                    posX = 10,
                    posY = glViewSizeInfo.LogicalHeight - 10,
                    anchor = TextAnchor.BottomLeft,
                    red = 255,
                });
            };

            glView.MouseDown += delegate
            {
                labelMouseCount.Text = (++glMouseCount).ToString();
            };
            glView.MouseWheel += delegate
            {
                labelMouseCount.Text = (++glMouseCount).ToString();
            };

            button.Click += delegate
            {
                if (glView.ContextInfo != null)
                {
                    var info = glView.ContextInfo.Value;
                    var rowTexts = new List<String>();
                    rowTexts.Add(t.Format("draw-gl-info-version", info.version));
                    rowTexts.Add(t.Format("draw-gl-info-vendor", info.vendor));
                    rowTexts.Add(t.Format("draw-gl-info-renderer", info.renderer));
                    rowTexts.Add(t.Format("draw-gl-info-extensions", String.Join('\n', info.ToExtensionList())));
                    App.RunDialog(new InfoDialog(t["draw-gl-info-title"], String.Join('\n', rowTexts)));
                }
            };
        }

        private void loopDrawGL()
        {
            if (glRenderSwitch) glView.QueueRender();
        }

        private GLView glView;
        private GLSizeInfo glViewSizeInfo;
        private bool glRenderSwitch = true;
        private int glMouseCount = 0;
    }
}