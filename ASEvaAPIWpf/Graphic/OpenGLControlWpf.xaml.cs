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
using System.IO.Packaging;
using System.Reflection;
using System.Windows.Navigation;
using System.Windows.Markup;

namespace ASEva.UIWpf
{
    /// <summary>
    /// OpenGLControlWpf.xaml 的交互逻辑
    /// </summary>
    partial class OpenGLControlWpf : UserControl, GLView.GLViewBackend
    {
        public OpenGLControlWpf()
        {
            loadViewFromUri(this, "/ASEvaAPIWpf;component/graphic/openglcontrolwpf.xaml");
            gl = OpenGL.Create(new WindowsFuncLoader());
        }

        private void loadViewFromUri(object self, string baseUri)
        {
            try
            {
                var resourceLocater = new Uri(baseUri, UriKind.Relative);
                var exprCa = (PackagePart)typeof(Application).GetMethod("GetResourceOrContentPart", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { resourceLocater });
                var stream = exprCa.GetStream();
                var uri = new Uri((Uri)typeof(BaseUriHelper).GetProperty("PackAppBaseUri", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null), resourceLocater);
                var parserContext = new ParserContext
                {
                    BaseUri = uri
                };
                typeof(XamlReader).GetMethod("LoadBaml", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { stream, parserContext, self, true });
            }
            catch (Exception)
            {}
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
            var rootWindow = Window.GetWindow(this);
            if (rootWindow != null && rootWindow.WindowState != WindowState.Minimized && Visibility == Visibility.Visible)
            {
                InvalidateVisual();
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

            var pixelScale = (float)PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;
            var curSize = new GLSizeInfo((int)ActualWidth, (int)ActualHeight, (int)(pixelScale * ActualWidth), (int)(pixelScale * ActualHeight), pixelScale, (float)(ActualWidth / ActualHeight));
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            Win32.wglMakeCurrent(hdc, context);

            var clipGeometry = new RectangleGeometry(new Rect(0, 0, curSize.LogicalWidth, curSize.LogicalHeight));
            drawingContext.PushClip(clipGeometry);

            try
            {
                if (resized)
                {
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    hostBuffer = new byte[size.RealWidth * size.RealHeight * 4];
                    bitmap = new WriteableBitmap(size.RealWidth, size.RealHeight, 96, 96, PixelFormats.Bgra32, null);
                }

                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);

                if (resized) callback.OnGLResize(gl, size);

                var textTasks = new GLTextTasks();
                callback.OnGLRender(gl, textTasks);
                gl.Finish();

                gl.ReadPixels(0, 0, bitmap.PixelWidth, bitmap.PixelHeight, OpenGL.GL_RGBA, hostBuffer);

                bitmap.Lock();
                {
                    var bitmapWidth = bitmap.PixelWidth;
                    var bitmapHeight = bitmap.PixelHeight;
                    var bitmapStep = bitmap.BackBufferStride;
                    unsafe
                    {
                        byte* surfaceData = (byte*)bitmap.BackBuffer;
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
                bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
                bitmap.Unlock();

                drawingContext.DrawImage(bitmap, new Rect(0, 0, size.LogicalWidth, size.LogicalHeight));

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
            catch (Exception)
            {
                initOK = false;
            }

            drawingContext.Pop();
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

                var pixelScale = (float)PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;
                size = new GLSizeInfo((int)ActualWidth, (int)ActualHeight, (int)(pixelScale * ActualWidth), (int)(pixelScale * ActualHeight), pixelScale, (float)(ActualWidth / ActualHeight));

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
                bitmap = new WriteableBitmap(size.RealWidth, size.RealHeight, 96, 96, PixelFormats.Bgra32, null);

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
                Win32.ReleaseDC(hwnd, hdc);
                hdc = IntPtr.Zero;
            }

            initOK = null;
        }

        private OpenGL gl = null;
        private GLView.GLViewCallback callback = null;
        private bool? initOK = null;
        private IntPtr hwnd = IntPtr.Zero;
        private IntPtr hdc = IntPtr.Zero;
        private IntPtr context = IntPtr.Zero;
        private uint[] frameBuffer = null;
        private uint[] colorBuffer = null;
        private uint[] depthBuffer = null;
        private byte[] hostBuffer = null;
        private WriteableBitmap bitmap = null;
        private GLSizeInfo size = null;
    }
}
