using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using ASEva;
using ASEva.UIAvalonia;
using SharpGL;

namespace ASEvaAPIAvaloniaTest
{
    partial class DrawGL : Panel
    {
        public DrawGL()
        {
            InitializeComponent();

            this.AddToResources(Program.Texts);
        }

        public void OnLoop()
        {
            labelLoopInterval.Content = glLoopIntervalStat.Update() + "ms";
            if (glRenderSwitch) glView.QueueRender();
        }

        private void glView_Initialize(object sender, GLEventArgs e)
        {
            var gl = e.GL;

            glInfos.Add(Program.Texts.Format("draw-gl-info-version", gl.Version));
            glInfos.Add(Program.Texts.Format("draw-gl-info-vendor", gl.Vendor));
            glInfos.Add(Program.Texts.Format("draw-gl-info-renderer", gl.Renderer));
            glInfos.Add(Program.Texts.Format("draw-gl-info-extensions", String.Join('\n', gl.GetExtensions())));
        }

        private void glView_Resize(object sender, GLResizeEventArgs e)
        {
            var gl = e.GL;
            var glViewSizeInfo = e.SizeInfo;
            
            gl.Viewport(0, 0, glViewSizeInfo.RealWidth, glViewSizeInfo.RealHeight);
        }

        private void glView_Render(object sender, GLRenderEventArgs e)
        {
            var gl = e.GL;
            var texts = e.TextTasks;

            var offset = (float)Math.Cos((DateTime.Now - startTime).TotalSeconds * 3) * 0.5f;

            gl.ClearColor(0.0f, 0.0f, 0.25f + offset * 0.5f, 1.0f);
            gl.Clear((int)OpenGL.GL_COLOR_BUFFER_BIT | (int)OpenGL.GL_DEPTH_BUFFER_BIT);

            // var triangleVertexData = new float[]
            // {
            //     0.0f + offset, 0.5f, -1.0f,
            //     -0.5f + offset, -0.5f, -1.0f,
            //     0.5f + offset, -0.5f, -1.0f,
            //     0.0f - offset, 0.5f, -2.0f,
            //     -0.5f - offset, -0.5f, -2.0f,
            //     0.5f - offset, -0.5f, -2.0f
            // };
            // var triangleColorData = new float[]
            // {
            //     1.0f, 0.0f, 0.0f,
            //     0.0f, 1.0f, 0.0f,
            //     0.0f, 1.0f, 0.0f,
            //     0.0f, 0.0f, 1.0f,
            //     0.0f, 0.0f, 1.0f,
            //     0.0f, 0.0f, 1.0f,
            // };
            // unsafe
            // {
            //     fixed (float* triangleVertexDataPtr = &(triangleVertexData[0]), triangleColorDataPtr = &(triangleColorData[0]))
            //     {
            //         gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)triangleVertexDataPtr);
            //         gl.ColorPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)triangleColorDataPtr);
            //         gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 6);
            //     }
            // }

            var centerX = (int)(glView.Bounds.Width / 2);
            var centerY = (int)(glView.Bounds.Height / 2);

            var textBrightness = (byte)(128 - 200 * offset);
            texts.Add("Hello 天气不错", centerX - (int)(100 * offset), centerY, textBrightness, textBrightness, textBrightness, 3.0f);
            texts.Add("UPPER BOUND", centerX, centerY - 20, 255, 255, 255, 0.5f);
            texts.Add("LOWER BOUND", centerX, centerY + 20, 255, 255, 255, 0.5f);

            // var quadVertexData = new float[]
            // {
            //     -0.5f, 0.8f, -1.0f,
            //     0.5f, 0.8f, -1.0f,
            //     0.5f, 0.75f, -1.0f,
            //     -0.5f, 0.75f, -1.0f,
            //     -0.5f, 0.7f, -0.999f,
            //     0.5f, 0.7f, -0.999f,
            //     0.5f, 0.75f, -0.999f,
            //     -0.5f, 0.775f, -0.999f,
            // };
            // var quadColorData = new float[]
            // {
            //     1.0f, 0.0f, 0.0f,
            //     1.0f, 0.0f, 0.0f,
            //     1.0f, 0.0f, 0.0f,
            //     1.0f, 0.0f, 0.0f,
            //     0.0f, 1.0f, 0.0f,
            //     0.0f, 1.0f, 0.0f,
            //     0.0f, 1.0f, 0.0f,
            //     0.0f, 1.0f, 0.0f,
            // };
            // unsafe
            // {
            //     fixed (float* quadVertexDataPtr = &(quadVertexData[0]), quadColorDataPtr = &(quadColorData[0]))
            //     {
            //         gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)quadVertexDataPtr);
            //         gl.ColorPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)quadColorDataPtr);
            //         gl.DrawArrays(OpenGL.GL_QUADS, 0, 8);
            //     }
            // }

            texts.Add(new GLTextTask
            {
                text = "FPS: " + glView.FPS.ToString("F1"),
                posX = 10,
                posY = (int)glView.Bounds.Height - 10,
                anchorX = AlignmentX.Left,
                anchorY = AlignmentY.Bottom,
                red = 255,
            });
        }

        private void glView_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            labelMouseCount.Content = (++glMouseCount).ToString();
        }

        private void glView_PointerWheelChanged(object sender, PointerWheelEventArgs e)
        {
            labelMouseCount.Content = (++glMouseCount).ToString();
        }

        private void linkPause_Click(object sender, RoutedEventArgs e)
        {
            glRenderSwitch = false;
        }

        private void linkResume_Click(object sender, RoutedEventArgs e)
        {
            glRenderSwitch = true;
        }

        private void linkInfo_Click(object sender, RoutedEventArgs e)
        {
            // TODO
            Console.WriteLine(String.Join('\n', glInfos));
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
            private List<int> loopIntervals = new List<int>();
        }

        private bool glRenderSwitch = true;
        private int glMouseCount = 0;
        private LoopIntervalStat glLoopIntervalStat = new LoopIntervalStat();
        private List<String> glInfos = new List<string>();
        private DateTime startTime = DateTime.Now;
    }
}