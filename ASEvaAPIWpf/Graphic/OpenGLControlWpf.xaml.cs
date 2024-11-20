using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SharpGL;
using ASEva.UIEto;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Globalization;
using ASEva.Utility;

namespace ASEva.UIWpf
{
    partial class OpenGLControlWpf : UserControl, GLBackend
    {
        public OpenGLControlWpf(GLCallback callback, GLAntialias antialias, bool useLegacyAPI)
        {
            XamlLoader.Load(this, "/ASEvaAPIWpf;component/graphic/openglcontrolwpf.xaml");

            this.callback = callback;
            this.antialias = antialias;
            this.useLegacyAPI = useLegacyAPI;

            if (globalGL == null) globalGL = OpenGL.Create(new WindowsFuncLoader());
            gl = globalGL;
        }

        public void ReleaseGL()
        {
            onDestroy();
        }

        public void QueueRender()
        {
            var rootWindow = Window.GetWindow(this);
            if (rootWindow != null && rootWindow.WindowState != WindowState.Minimized && Visibility == Visibility.Visible && DrawBeat.CallerBegin(this))
            {
                InvalidateVisual();
                DrawBeat.CallerEnd(this);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (initOK == null)
            {
                onInit();
                if (initOK == null) initOK = false;
            }
            if (!initOK.Value) return;

            if (colorBuffer == null || depthBuffer == null || frameBuffer == null) return;

            var moduleID = callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            var pixelScale = (float)PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;
            var curSize = new GLSizeInfo((int)ActualWidth, (int)ActualHeight, (int)(pixelScale * ActualWidth), (int)(pixelScale * ActualHeight), pixelScale, (float)(ActualWidth / ActualHeight), true);
            bool resized = size == null || curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            Win32.wglMakeCurrent(hdc, context);

            var clipGeometry = new RectangleGeometry(new Rect(0, 0, curSize.LogicalWidth, curSize.LogicalHeight));
            drawingContext.PushClip(clipGeometry);

            try
            {
                if (resized)
                {
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                    if (antialias != GLAntialias.Disabled)
                    {
                        gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                        gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[1]);
                        gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                    }
                    else gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);

                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                    if (antialias != GLAntialias.Disabled)
                    {
                        gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    }
                    else gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);

                    hostBuffer = new byte[size.RealWidth * size.RealHeight * 4];
                }

                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);

                if (resized) callback.OnGLResize(gl, size);

                var textTasks = new GLTextTasks();
                callback.OnGLRender(gl, textTasks);
                gl.Finish();

                if (antialias != GLAntialias.Disabled)
                {
                    gl.BindFramebufferEXT(OpenGL.GL_READ_FRAMEBUFFER_EXT, frameBuffer[0]);
                    gl.BindFramebufferEXT(OpenGL.GL_DRAW_FRAMEBUFFER_EXT, frameBuffer[1]);
                    gl.BlitFramebufferEXT(0, 0, size.RealWidth, size.RealHeight, 0, 0, size.RealWidth, size.RealHeight, OpenGL.GL_COLOR_BUFFER_BIT, OpenGL.GL_NEAREST);
                    gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[1]);
                }

                gl.ReadPixels(0, 0, size.RealWidth, size.RealHeight, OpenGL.GL_BGRA, hostBuffer);

                var bitmap = BitmapSource.Create(size.RealWidth, size.RealHeight, 96, 96, PixelFormats.Bgra32, null, hostBuffer, size.RealWidth * 4);

                double dx = 0, dy = 0;
                var rootWindow = Window.GetWindow(this);
                if (this.IsDescendantOf(rootWindow))
                {
                    var origin = this.TransformToAncestor(rootWindow).Transform(new Point(0, 0));
                    origin = new Point(origin.X * pixelScale, origin.Y * pixelScale);
                    dx = Math.Ceiling(origin.X) - origin.X;
                    dy = Math.Ceiling(origin.Y) - origin.Y;
                }

                drawingContext.PushTransform(new ScaleTransform(1.0f / pixelScale, 1.0f / pixelScale));
                drawingContext.DrawImage(bitmap, new Rect(dx, dy, bitmap.PixelWidth, bitmap.PixelHeight));
                drawingContext.Pop();

                foreach (var task in textTasks.Clear())
                {
                    if (String.IsNullOrEmpty(task.text)) continue;

                    var fontName = String.IsNullOrEmpty(task.fontName) ? "Microsoft Yahei" : task.fontName;
                    var fontSize = (task.sizeScale <= 0 ? 1.0f : task.sizeScale) * 11.0f;

                    var brush = new SolidColorBrush(Color.FromArgb(task.alpha == 0 ? (byte)255 : task.alpha, task.red, task.green, task.blue));
                    var text = new FormattedText(task.text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(fontName), fontSize, brush, 96);
                    var textSize = new Size((int)text.Width, (int)text.Height);

                    int posX = task.posX;
                    int posY = task.posY;
                    if (task.isRealPos)
                    {
                        posX = (int)((float)posX / size.RealPixelScale);
                        posY = (int)((float)posY / size.RealPixelScale);
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

                    drawingContext.DrawText(text, new Point(posX, posY));
                }
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                initOK = false;
            }

            drawingContext.Pop();

            DrawBeat.CallbackEnd(this);
        }

        private void onInit()
        {
            var rootWindow = Window.GetWindow(this);
            if (rootWindow == null) return;

            var helper = new WindowInteropHelper(rootWindow);
            hwnd = helper.Handle;
            if (hwnd == IntPtr.Zero) return;

            hdc = Win32.GetDC(hwnd);
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

            if (!createContext()) return;

            Win32.wglMakeCurrent(hdc, context);

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;
                if (String.IsNullOrEmpty(ctxInfo.extensions)) ctxInfo.extensions = String.Join(' ', gl.ExtensionList);

                var pixelScale = (float)PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;
                size = new GLSizeInfo((int)ActualWidth, (int)ActualHeight, (int)(pixelScale * ActualWidth), (int)(pixelScale * ActualHeight), pixelScale, (float)(ActualWidth / ActualHeight), true);

                if (!gl.ExtensionList.Contains("GL_EXT_framebuffer_object"))
                {
                    onDestroy();
                    return;
                }

                gl.PreloadFunction("glDeleteFramebuffersEXT");
                gl.PreloadFunction("glDeleteRenderbuffersEXT");

                if (antialias != GLAntialias.Disabled)
                {
                    if (gl.ExtensionList.Contains("GL_EXT_framebuffer_multisample") && gl.ExtensionList.Contains("GL_EXT_framebuffer_blit"))
                    {
                        var maxSamples = new int[1];
                        gl.GetInteger(OpenGL.GL_MAX_SAMPLES_EXT, maxSamples);

                        if (antialias == GLAntialias.Sample16x && maxSamples[0] < 16) antialias = GLAntialias.Sample8x;
                        if (antialias == GLAntialias.Sample8x && maxSamples[0] < 8) antialias = GLAntialias.Sample4x;
                        if (antialias == GLAntialias.Sample4x && maxSamples[0] < 4) antialias = GLAntialias.Sample2x;
                        if (antialias == GLAntialias.Sample2x && maxSamples[0] < 2) antialias = GLAntialias.Disabled;
                    }
                    else antialias = GLAntialias.Disabled;
                }

                colorBuffer = new uint[antialias == GLAntialias.Disabled ? 1 : 2];
                gl.GenRenderbuffersEXT((uint)colorBuffer.Length, colorBuffer);
                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                if (antialias != GLAntialias.Disabled)
                {
                    gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[1]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                }
                else gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);

                depthBuffer = new uint[1];
                gl.GenRenderbuffersEXT(1, depthBuffer);
                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                if (antialias == GLAntialias.Disabled) gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                else gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);

                frameBuffer = new uint[antialias == GLAntialias.Disabled ? 1 : 2];
                gl.GenFramebuffersEXT((uint)frameBuffer.Length, frameBuffer);
                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);
                if (antialias != GLAntialias.Disabled)
                {
                    gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[1]);
                    gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[1]);
                }

                if (gl.CheckFramebufferStatusEXT(OpenGL.GL_FRAMEBUFFER_EXT) != OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT)
                {
                    onDestroy();
                    return;
                }

                hostBuffer = new byte[size.RealWidth * size.RealHeight * 4];

                callback.OnGLInitialize(gl, ctxInfo);
                callback.OnGLResize(gl, size);

                gl.Flush();
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
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
                gl.DeleteFramebuffersEXT((uint)frameBuffer.Length, frameBuffer);
                frameBuffer = null;
            }
            if (colorBuffer != null)
            {
                gl.DeleteRenderbuffersEXT((uint)colorBuffer.Length, colorBuffer);
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
                Win32.ReleaseDC(hwnd, hdc);
                hdc = IntPtr.Zero;
            }

            initOK = null;
        }

        private int getSampleCount(GLAntialias antialias)
        {
            switch (antialias)
            {
                case GLAntialias.Sample2x:
                    return 2;
                case GLAntialias.Sample4x:
                    return 4;
                case GLAntialias.Sample8x:
                    return 8;
                case GLAntialias.Sample16x:
                    return 16;
                default:
                    return 0;
            }
        }

        private bool createContext()
        {
            bool contextCreated = false;
            if (!useLegacyAPI && !createContextAttribsARBUnsupported)
            {
                if (!gl.IsFunctionSupported("wglCreateContextAttribsARB"))
                {
                    var tempContext = Win32.wglCreateContext(hdc);
                    if (tempContext == IntPtr.Zero) return false;

                    Win32.wglMakeCurrent(hdc, tempContext);

                    if (!gl.PreloadFunction("wglCreateContextAttribsARB")) createContextAttribsARBUnsupported = true;

                    Win32.wglDeleteContext(tempContext);
                }

                if (!createContextAttribsARBUnsupported)
                {
                    var glCoreVersions = new Version[]
                    {
                        new Version(4, 6),
                        new Version(3, 3)
                    };

                    foreach (var ver in glCoreVersions)
                    {
                        var attribs = new int[]
                        {
                            0x2091, // WGL_CONTEXT_MAJOR_VERSION_ARB
                            ver.Major,
                            0x2092, // WGL_CONTEXT_MINOR_VERSION_ARB
                            ver.Minor,
                            (int)OpenGL.GL_CONTEXT_PROFILE_MASK,
                            (int)OpenGL.GL_CONTEXT_CORE_PROFILE_BIT,
                            0
                        };

                        context = gl.CreateContextAttribsARB(hdc, IntPtr.Zero, attribs);
                        if (context != IntPtr.Zero)
                        {
                            contextCreated = true;
                            break;
                        }
                    }
                }
            }

            if (!contextCreated)
            {
                context = Win32.wglCreateContext(hdc);
            }

            return context != IntPtr.Zero;
        }

        private GLCallback callback;
        private GLAntialias antialias;
        private bool useLegacyAPI;
        private bool? initOK = null;
        private IntPtr hwnd = IntPtr.Zero;
        private IntPtr hdc = IntPtr.Zero;
        private IntPtr context = IntPtr.Zero;
        private uint[]? frameBuffer = null;
        private uint[]? colorBuffer = null;
        private uint[]? depthBuffer = null;
        private byte[]? hostBuffer = null;
        private GLSizeInfo? size = null;
        private OpenGL gl;

        private static OpenGL? globalGL = null;
        private static bool createContextAttribsARBUnsupported = false;
    }
}
