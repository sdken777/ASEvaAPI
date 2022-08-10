using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing.Imaging;
using SharpGL;
using ASEva.UIEto;
using Eto.WinForms;

namespace ASEva.UICoreWF
{
    partial class OpenGLControl : UserControl, GLView.GLViewBackend
    {
        public OpenGLControl()
        {
            InitializeComponent();

            gl = OpenGL.Create(new WindowsFuncLoader());

            pictureBox.MouseWheel += pictureBox_MouseWheel;
        }

        public void SetCallback(GLView.GLViewCallback callback)
        {
            this.callback = callback;
        }

        public void ReleaseGL()
        {
            onDestroy();
        }

        public void QueueRender()
        {
            if (ParentForm != null && ParentForm.WindowState != FormWindowState.Minimized && Visible)
            {
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            QueueRender();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (initOK == null)
            {
                onInit();
                if (initOK == null) initOK = false;
            }
            if (!initOK.Value)
            {
                e.Graphics.Clear(Color.Black);
                return;
            }

            var pixelScale = (float)DeviceDpi / 96;
            var curSize = new GLSizeInfo((int)(Width / pixelScale), (int)(Height / pixelScale), Width, Height, pixelScale, (float)Width / Height);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            Win32.wglMakeCurrent(hdc, context);

            try
            {
                if (resized)
                {
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    hostBuffer = new byte[size.RealWidth * size.RealHeight * 4]; 
                    bitmap = new Bitmap(size.RealWidth, size.RealHeight, PixelFormat.Format32bppArgb);
                }

                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);

                if (resized) callback.OnGLResize(gl, size);

                var textTasks = new GLTextTasks();
                callback.OnGLRender(gl, textTasks);
                gl.Finish();

                gl.ReadPixels(0, 0, bitmap.Width, bitmap.Height, OpenGL.GL_RGBA, hostBuffer);

                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                {
                    var bitmapWidth = bitmapData.Width;
                    var bitmapHeight = bitmapData.Height;
                    var bitmapStep = bitmapData.Stride;
                    unsafe
                    {
                        byte* surfaceData = (byte*)bitmapData.Scan0;
                        fixed (byte* srcData = &(hostBuffer[0]))
                        {
                            for (int v = 0; v < bitmapHeight; v++)
                            {
                                uint* srcRow = (uint*)&srcData[v * bitmapWidth * 4];
                                uint* dstRow = (uint*)&surfaceData[(bitmapHeight - 1 - v) * bitmapStep];
                                for (int u = 0; u < bitmapWidth; u++)
                                {
                                    dstRow[u] = ((srcRow[u] & 0x000000ff) << 16) | (srcRow[u] & 0x0000ff00) | ((srcRow[u] & 0x00ff0000) >> 16) | 0xff000000;
                                }
                            }
                        }
                    }
                }
                bitmap.UnlockBits(bitmapData);

                e.Graphics.DrawImageUnscaled(bitmap, 0, 0);

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                foreach (var task in textTasks.Clear())
                {
                    if (String.IsNullOrEmpty(task.text)) continue;

                    var fontName = String.IsNullOrEmpty(task.fontName) ? "Microsoft Yahei" : task.fontName;
                    var fontSize = (task.sizeScale <= 0 ? 1.0f : task.sizeScale) * 9.0f;
                    var font = new Font(fontName, fontSize);

                    var textSize = e.Graphics.MeasureString(task.text, font);

                    int posX = task.posX;
                    int posY = task.posY;
                    if (!task.isRealPos)
                    {
                        posX = (int)(size.RealPixelScale * posX);
                        posY = (int)(size.RealPixelScale * posY);
                    }

                    int fullWidth = (int)textSize.Width;
                    int fullHeight = (int)textSize.Height;
                    int halfWidth = (int)(textSize.Width / 2);
                    int halfHeight = (int)(textSize.Height / 2);
                    switch (task.anchor)
                    {
                        case TextAnchor.TopLeft:
                            break;
                        case TextAnchor.LeftCenter:
                            posY -= halfHeight;
                            break;
                        case TextAnchor.BottomLeft:
                            posY -= fullHeight;
                            break;
                        case TextAnchor.TopCenter:
                            posX -= halfWidth;
                            break;
                        case TextAnchor.Center:
                            posX -= halfWidth;
                            posY -= halfHeight;
                            break;
                        case TextAnchor.BottomCenter:
                            posX -= halfWidth;
                            posY -= fullHeight;
                            break;
                        case TextAnchor.TopRight:
                            posX -= fullWidth;
                            break;
                        case TextAnchor.RightCenter:
                            posX -= fullWidth;
                            posY -= halfHeight;
                            break;
                        case TextAnchor.BottomRight:
                            posX -= fullWidth;
                            posY -= fullHeight;
                            break;
                        default:
                            break;
                    }

                    var brush = new SolidBrush(Color.FromArgb(task.alpha == 0 ? (byte)255 : task.alpha, task.red, task.green, task.blue));

                    e.Graphics.DrawString(task.text, font, brush, posX, posY);
                }
            }
            catch (Exception)
            {
                initOK = false;
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            callback.OnRaiseMouseDown(e.ToEto(this));
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            callback.OnRaiseMouseMove(e.ToEto(this));
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            callback.OnRaiseMouseUp(e.ToEto(this));
        }

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            callback.OnRaiseMouseWheel(e.ToEto(this));
        }

        private void onInit()
        {
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
                ctxInfo.extensions = gl.Extensions;

                var pixelScale = (float)DeviceDpi / 96;
                size = new GLSizeInfo((int)(Width / pixelScale), (int)(Height / pixelScale), Width, Height, pixelScale, (float)Width / Height);

                colorBuffer = new uint[1];
                gl.GenRenderbuffersEXT(1, colorBuffer);
                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);

                depthBuffer = new uint[1];
                gl.GenRenderbuffersEXT(1, depthBuffer);
                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);

                frameBuffer = new uint[1];
                gl.GenFramebuffersEXT(1, frameBuffer);
                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);

                if (gl.CheckFramebufferStatusEXT(OpenGL.GL_FRAMEBUFFER_EXT) != OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT)
                {
                    onDestroy();
                    return;
                }

                hostBuffer = new byte[size.RealWidth * size.RealHeight * 4];
                bitmap = new Bitmap(size.RealWidth, size.RealHeight, PixelFormat.Format32bppArgb);

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

            if (frameBuffer != null)
            {
                gl.DeleteFramebuffersEXT(1, frameBuffer);
                frameBuffer = null;
            }
            if (colorBuffer != null)
            {
                gl.DeleteRenderbuffersEXT(1, colorBuffer);
                colorBuffer = null;
            }
            if (depthBuffer != null)
            {
                gl.DeleteRenderbuffersEXT(1, depthBuffer);
                depthBuffer = null;
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

            initOK = null;
        }

        private OpenGL gl = null;
        private GLView.GLViewCallback callback = null;
        private bool? initOK = null;
        private IntPtr hdc = IntPtr.Zero;
        private IntPtr context = IntPtr.Zero;
        private uint[] frameBuffer = null;
        private uint[] colorBuffer = null;
        private uint[] depthBuffer = null;
        private byte[] hostBuffer = null;
        private Bitmap bitmap = null;
        private GLSizeInfo size = null;
    }
}
