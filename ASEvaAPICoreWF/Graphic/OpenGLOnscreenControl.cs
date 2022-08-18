using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;
using Eto.WinForms;

namespace ASEva.UICoreWF
{
    partial class OpenGLOnscreenControl : UserControl, GLBackend
    {
        public OpenGLOnscreenControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.Opaque, true);

            gl = OpenGL.Create(new WindowsFuncLoader());

            MouseWheel += OpenGLOnscreenControl_MouseWheel;
        }

        public void SetCallback(GLCallback callback)
        {
            this.callback = callback;
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

            if (!wglSwapIntervalEXTInitialized)
            {
                IntPtr proc = Win32.wglGetProcAddress("wglSwapIntervalEXT");
                if (proc != IntPtr.Zero)
                {
                    var d = Marshal.GetDelegateForFunctionPointer(proc, typeof(wglSwapIntervalEXTDelegate));
                    if (d != null) wglSwapIntervalEXT = d as wglSwapIntervalEXTDelegate;
                }
                wglSwapIntervalEXTInitialized = true;
            }

            if (wglSwapIntervalEXT != null) wglSwapIntervalEXT(0);
            Win32.SwapBuffers(hdc);

            DrawBeat.CallbackEnd(this);
        }

        private void OpenGLOnscreenControl_MouseDown(object sender, MouseEventArgs e)
        {
            callback.OnRaiseMouseDown(e.ToEto(this));
        }

        private void OpenGLOnscreenControl_MouseMove(object sender, MouseEventArgs e)
        {
            callback.OnRaiseMouseMove(e.ToEto(this));
        }

        private void OpenGLOnscreenControl_MouseUp(object sender, MouseEventArgs e)
        {
            callback.OnRaiseMouseUp(e.ToEto(this));
        }

        private void OpenGLOnscreenControl_MouseWheel(object sender, MouseEventArgs e)
        {
            callback.OnRaiseMouseWheel(e.ToEto(this));
        }

        private void onInit()
        {
            initOK = false;

            hdc = Win32.GetDC(Handle);
            if (hdc == IntPtr.Zero) return;

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

            context = Win32.wglCreateContext(hdc);
            if (context == IntPtr.Zero) return;

            Win32.wglMakeCurrent(hdc, context);

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;

                var wglExtensionsString = WGL.GetWglExtensionsString(gl, hdc);
                ctxInfo.extensions = gl.Extensions + (wglExtensionsString.Length == 0 ? "" : (" " + wglExtensionsString));

                var pixelScale = (float)DeviceDpi / 96;
                size = new GLSizeInfo((int)(Width / pixelScale), (int)(Height / pixelScale), Width, Height, pixelScale, (float)Width / Height);

                callback.OnGLInitialize(gl, ctxInfo);
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

        private OpenGL gl = null;
        private GLCallback callback = null;
        private bool? initOK = null;
        private IntPtr context = IntPtr.Zero;
        private GLSizeInfo size = null;
        private IntPtr hdc = IntPtr.Zero;

        private delegate bool wglSwapIntervalEXTDelegate(int interval);
        private wglSwapIntervalEXTDelegate wglSwapIntervalEXT = null;
        private bool wglSwapIntervalEXTInitialized = false;
    }
}
