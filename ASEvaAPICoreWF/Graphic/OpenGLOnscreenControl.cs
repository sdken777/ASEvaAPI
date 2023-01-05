using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;
using Eto.WinForms;

namespace ASEva.UICoreWF
{
    partial class OpenGLOnscreenControl : UserControl, GLBackend
    {
        public OpenGLOnscreenControl(GLCallback callback, GLAntialias antialias)
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.Opaque, true);

            this.callback = callback;
            this.antialias = antialias;
            this.gl = OpenGL.Create(new WindowsFuncLoader());
        }

        public void ReleaseGL()
        {
            onDestroy();
        }

        public void QueueRender()
        {
            if (ParentForm != null && ParentForm.WindowState != FormWindowState.Minimized && Visible && DrawBeat.CallerBegin(this))
            {
                Invalidate();
                DrawBeat.CallerEnd(this);
            }
        }

        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            QueueRender();
        }

        private void OpenGLOnscreenControl_Paint(object sender, PaintEventArgs e)
        {
            if (initOK == null) onInit();

            if (!initOK.Value)
            {
                e.Graphics.Clear(Color.Black);
                return;
            }

            var moduleID = callback == null ? null : callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            var pixelScale = (float)DeviceDpi / 96;
            var curSize = new GLSizeInfo((int)(Width / pixelScale), (int)(Height / pixelScale), Width, Height, pixelScale, (float)Width / Height);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            Win32.wglMakeCurrent(hdc, context);

            try
            {
                if (resized) callback.OnGLResize(gl, size);

                var dummy = new GLTextTasks();
                callback.OnGLRender(gl, dummy);
                gl.Finish();
            }
            catch (Exception)
            {
                onDestroy();
            }

            if (supportSwapInterval) gl.SwapIntervalEXT(0);
            Win32.SwapBuffers(hdc);

            DrawBeat.CallbackEnd(this);
        }

        private void onInit()
        {
            initOK = false;

            var formats = chooseFormats(antialias, gl, Handle);
            if (formats == null) return;

            hdc = Win32.GetDC(Handle);
            if (hdc == IntPtr.Zero) return;

            var pfd = genPFD();

            bool setPixelFormatOK = false;
            foreach (var format in formats)
            {
                if (Win32.SetPixelFormat(hdc, format, pfd) != 0)
                {
                    setPixelFormatOK = true;
                    break;
                }
            }
            if (!setPixelFormatOK) return;

            context = Win32.wglCreateContext(hdc);
            if (context == IntPtr.Zero) return;

            Win32.wglMakeCurrent(hdc, context);

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;

                supportSwapInterval = gl.ExtensionList.Contains("WGL_EXT_swap_control");

                var pixelScale = (float)DeviceDpi / 96;
                size = new GLSizeInfo((int)(Width / pixelScale), (int)(Height / pixelScale), Width, Height, pixelScale, (float)Width / Height);

                callback.OnGLInitialize(gl, ctxInfo);
                if (antialias != GLAntialias.Disabled) gl.Enable(OpenGL.GL_MULTISAMPLE);

                callback.OnGLResize(gl, size);

                gl.Flush();
            }
            catch (Exception)
            {
                onDestroy();
                return;
            }

            initOK = true;
        }

        private void onDestroy()
        {
            if (context != IntPtr.Zero)
            {
                Win32.wglMakeCurrent(hdc, context);
            }

            if (context != IntPtr.Zero)
            {
                Win32.wglDeleteContext(context);
                context = IntPtr.Zero;
            }
            if (hdc != IntPtr.Zero)
            {
                Win32.ReleaseDC(Handle, hdc);
                hdc = IntPtr.Zero;
            }

            initOK = false;
        }

        private static Win32.PIXELFORMATDESCRIPTOR genPFD()
        {
            Win32.PIXELFORMATDESCRIPTOR pfd = new Win32.PIXELFORMATDESCRIPTOR();
            pfd.Init();
            pfd.nVersion = 1;
            pfd.dwFlags = Win32.PFD_DRAW_TO_WINDOW | Win32.PFD_SUPPORT_OPENGL | Win32.PFD_DOUBLEBUFFER;
            pfd.iPixelType = Win32.PFD_TYPE_RGBA;
            pfd.cColorBits = 32;
            pfd.cDepthBits = 16;
            pfd.cStencilBits = 8;
            pfd.iLayerType = Win32.PFD_MAIN_PLANE;
            return pfd;
        }

        private static int[] chooseFormats(GLAntialias antialias, OpenGL gl, IntPtr hwnd)
        {
            var tempPanel = new Panel();

            var tempHDC = Win32.GetDC(tempPanel.Handle);
            if (tempHDC == IntPtr.Zero) return null;

            var pfd = genPFD();

            int basicPixelformat;
            if ((basicPixelformat = Win32.ChoosePixelFormat(tempHDC, pfd)) == 0)
            {
                Win32.ReleaseDC(hwnd, tempHDC);
                return null;
            }

            if (Win32.SetPixelFormat(tempHDC, basicPixelformat, pfd) == 0)
            {
                Win32.ReleaseDC(hwnd, tempHDC);
                return null;
            }

            var tempContext = Win32.wglCreateContext(tempHDC);
            if (tempContext == IntPtr.Zero)
            {
                Win32.ReleaseDC(hwnd, tempHDC);
                return null;
            }

            Win32.wglMakeCurrent(tempHDC, tempContext);

            var formats = new List<int>();
            if (antialias != GLAntialias.Disabled)
            {
                int[] sampleCounts = null;
                switch (antialias)
                {
                    case GLAntialias.Sample2x:
                        sampleCounts = new int[] { 2 };
                        break;
                    case GLAntialias.Sample4x:
                        sampleCounts = new int[] { 4, 2 };
                        break;
                    case GLAntialias.Sample8x:
                        sampleCounts = new int[] { 8, 4, 2 };
                        break;
                    case GLAntialias.Sample16x:
                        sampleCounts = new int[] { 16, 8, 4, 2 };
                        break;
                }

                foreach (var sampleCount in sampleCounts)
                {
                    var floatAttribList = new float[] { 0, 0 };
                    var intAttribList = new int[]
                    {
                            (int)OpenGL.WGL_DRAW_TO_WINDOW_ARB, 1,
                            (int)OpenGL.WGL_SUPPORT_OPENGL_ARB, 1,
                            (int)OpenGL.WGL_DOUBLE_BUFFER_ARB, 1,
                            (int)OpenGL.WGL_COLOR_BITS_ARB, 32,
                            (int)OpenGL.WGL_RED_BITS_ARB, 8,
                            (int)OpenGL.WGL_GREEN_BITS_ARB, 8,
                            (int)OpenGL.WGL_BLUE_BITS_ARB, 8,
                            (int)OpenGL.WGL_ALPHA_BITS_ARB, 8,
                            (int)OpenGL.WGL_DEPTH_BITS_ARB, 16,
                            (int)OpenGL.WGL_STENCIL_BITS_ARB, 8,
                            (int)OpenGL.WGL_PIXEL_TYPE_ARB, (int)OpenGL.WGL_TYPE_RGBA_ARB,
                            (int)OpenGL.WGL_SAMPLE_BUFFERS_ARB, 1,
                            (int)OpenGL.WGL_SAMPLES_ARB, sampleCount,
                    };

                    var format = new int[1];
                    var dummy = new uint[1];
                    if (gl.ChoosePixelFormatARB(tempHDC, intAttribList, floatAttribList, 1, format, dummy)) formats.Add(format[0]);
                }
            }
            formats.Add(basicPixelformat);

            Win32.wglDeleteContext(tempContext);
            Win32.ReleaseDC(hwnd, tempHDC);

            return formats.ToArray();
        }

        private OpenGL gl = null;
        private GLCallback callback = null;
        private GLAntialias antialias;
        private bool? initOK = null;
        private IntPtr context = IntPtr.Zero;
        private GLSizeInfo size = null;
        private IntPtr hdc = IntPtr.Zero;
        private bool supportSwapInterval = false;
    }
}
