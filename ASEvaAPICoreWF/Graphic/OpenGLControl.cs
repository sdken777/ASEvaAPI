using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using SharpGL;
using ASEva.UIEto;

namespace ASEva.UICoreWF
{
    public partial class OpenGLControl : UserControl, GLView.GLViewBackend
    {
        public OpenGLControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            Paint += delegate { drawEvent.Set(); };
        }

        public void SetCallback(GLView.GLViewCallback callback)
        {
            this.callback = callback;
        }

        public void InitializeGL()
        {
            if (thread != null) return;

            gl = OpenGL.Create(new WindowsFuncLoader());

            // 直接开始线程可能导致字体初始化失败？
            timer.Interval = 100;
            timer.Tick += delegate
            {
                timer.Stop();
                hdc = Win32.GetDC(Handle);
                shouldEnd = false;
                drawEvent.Set();
                thread = new Thread(threadFunc);
                thread.Start();
            };
            timer.Start();
        }

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public void ReleaseGL()
        {
            if (thread != null)
            {
                shouldEnd = true;
                thread.Join();
                thread = null;
                Win32.ReleaseDC(Handle, hdc);
            }
        }

        public void QueueRender()
        {
            if (ParentForm != null && ParentForm.WindowState != FormWindowState.Minimized && Visible)
            {
                QueueDrawWithoutCheckingParentForm();
            }
        }

        public void QueueDrawWithoutCheckingParentForm()
        {
            drawEvent.Set();
            finishEvent.WaitOne(20);
        }

        private void OpenGLControl_SizeChanged(object sender, EventArgs e)
        {
            drawEvent.Set();
        }

        private void threadFunc()
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

            int iPixelformat;
            if ((iPixelformat = Win32.ChoosePixelFormat(hdc, pfd)) == 0) return;
            if (Win32.SetPixelFormat(hdc, iPixelformat, pfd) == 0) return;

            var renderContext = Win32.wglCreateContext(hdc);
            if (renderContext == IntPtr.Zero) return;

            Win32.wglMakeCurrent(hdc, renderContext);

            if (Win32.glewInit() != 0) return;

            try
            {
                callback.OnGLInitialize(gl);
                gl.Flush();
            }
            catch (Exception)
            {
                return;
            }

            GLSizeInfo size = null;
            while (!shouldEnd)
            {
                if (!drawEvent.WaitOne(20)) continue;

                try
                {
                    if (size == null || Width != size.RealWidth || Height != size.RealHeight)
                    {
                        var pixelScale = (float)DeviceDpi / 96;
                        size = new GLSizeInfo((int)(Width / pixelScale), (int)(Height / pixelScale), Width, Height, pixelScale, (float)Width / Height);
                        callback.OnGLResize(gl, size);
                    }

                    var textTasks = new GLTextTasks();
                    callback.OnGLRender(gl, textTasks);

                    gl.MatrixMode(OpenGL.GL_PROJECTION);
                    gl.PushMatrix();
                    gl.LoadIdentity();
                    gl.Ortho(0, Width, 0, Height, -1, 1);

                    gl.MatrixMode(OpenGL.GL_MODELVIEW);
                    gl.PushMatrix();

                    gl.PushAttrib(OpenGL.GL_LIST_BIT | OpenGL.GL_CURRENT_BIT | OpenGL.GL_ENABLE_BIT | OpenGL.GL_TRANSFORM_BIT);
                    gl.Disable(OpenGL.GL_LIGHTING);
                    gl.Disable(OpenGL.GL_TEXTURE_2D);
                    gl.Disable(OpenGL.GL_DEPTH_TEST);

                    IntPtr? oldFont = null;
                    foreach (var task in textTasks.Clear())
                    {
                        DrawText.Draw(gl, task, size, hdc, ref oldFont);
                    }
                    if (oldFont != null) Win32.SelectObject(hdc, oldFont.Value);

                    gl.PopAttrib();

                    gl.MatrixMode(OpenGL.GL_PROJECTION);
                    gl.PopMatrix();

                    gl.MatrixMode(OpenGL.GL_MODELVIEW);
                    gl.PopMatrix();

                    gl.Finish();
                }
                catch (Exception)
                {
                    break;
                }

                finishEvent.Set();

                Win32.SwapBuffers(hdc);
            }

            Win32.wglDeleteContext(renderContext);
        }

        private OpenGL gl = null;
        private GLView.GLViewCallback callback = null;
        private Thread thread = null;
        private bool shouldEnd = false;
        private AutoResetEvent drawEvent = new AutoResetEvent(false);
        private AutoResetEvent finishEvent = new AutoResetEvent(false);
        private IntPtr hdc = IntPtr.Zero;
    }
}
