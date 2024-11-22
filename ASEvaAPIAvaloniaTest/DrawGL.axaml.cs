using System;
using System.Collections.Generic;
using System.Linq;
using ASEva;
using ASEva.UIEto;
using ASEva.UIAvalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SharpGL;

namespace ASEvaAPIAvaloniaTest
{
    partial class DrawGL : Panel
    {
        public DrawGL()
        {
            InitializeComponent();

            this.AddToResources(Program.Texts);

            if (!Program.DesignerMode)
            {
                glView = new GLView();
                etoEmbedder.EtoControl = glView;

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
                    var glViewSizeInfo = args.SizeInfo;
                    
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

                    var centerX = (int)(etoEmbedder.Bounds.Width / 2);
                    var centerY = (int)(etoEmbedder.Bounds.Height / 2);

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

                    texts.Add(new GLTextTask("FPS: " + glView.FPS.ToString("F1"))
                    {
                        PosX = 10,
                        PosY = (int)etoEmbedder.Bounds.Height - 10,
                        Anchor = TextAnchor.BottomLeft,
                        Red = 255,
                    });
                };

                glView.MouseDown += delegate
                {
                    labelMouseCount.Content = (++glMouseCount).ToString();
                };
                glView.MouseWheel += delegate
                {
                    labelMouseCount.Content = (++glMouseCount).ToString();
                };

                glView.MouseDoubleClick += delegate
                {
                    showInfo();
                };
            }
        }

        public void OnLoop()
        {
            labelLoopInterval.Content = glLoopIntervalStat.Update() + "ms";
            if (glView != null && glRenderSwitch) glView.QueueRender();
        }

        private void linkPause_Click(object? sender, RoutedEventArgs e)
        {
            glRenderSwitch = false;
        }

        private void linkResume_Click(object? sender, RoutedEventArgs e)
        {
            glRenderSwitch = true;
        }

        private void linkInfo_Click(object? sender, RoutedEventArgs e)
        {
            showInfo();
        }

        private async void showInfo()
        {
            if (glView == null || glView.ContextInfo == null) return;

            var info = glView.ContextInfo;
            var rowTexts = new List<String>();
            rowTexts.Add(Program.Texts.Format("draw-gl-info-version", info.Version));
            rowTexts.Add(Program.Texts.Format("draw-gl-info-vendor", info.Vendor));
            rowTexts.Add(Program.Texts.Format("draw-gl-info-renderer", info.Renderer));
            rowTexts.Add(Program.Texts.Format("draw-gl-info-extensions", String.Join('\n', info.ToExtensionList())));
            
            var dialog = new InfoDialog(Program.Texts["draw-gl-info-title"], String.Join('\n', rowTexts));
            await ASEva.UIAvalonia.App.RunDialog(dialog.ShowDialog);
        }

        private class LoopIntervalStat
        {
            public int Update()
            {
                var now = DateTime.Now;
                var loopInterval = (int)(now - glLastLoopTime).TotalMilliseconds;
                glLastLoopTime = now;

                loopIntervals.Add(loopInterval);
                if (loopIntervals.Count > 1000) loopIntervals.RemoveAt(0);
                return loopIntervals.Max();
            }

            private DateTime glLastLoopTime = DateTime.Now;
            private List<int> loopIntervals = [];
        }

        private GLView? glView;
        private bool glRenderSwitch = true;
        private int glMouseCount = 0;
        private LoopIntervalStat glLoopIntervalStat = new();
        private DateTime startTime = DateTime.Now;
    }
}