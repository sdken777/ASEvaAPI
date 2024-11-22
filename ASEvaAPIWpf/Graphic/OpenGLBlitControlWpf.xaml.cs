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
using SharpDX.Direct3D9;
using System.Runtime.InteropServices;

namespace ASEva.UIWpf
{
    partial class OpenGLBlitControlWpf : UserControl, GLBackend
    {
        private const int D3DDefaultAdapter = 0;

        public OpenGLBlitControlWpf(GLCallback callback, GLAntialias antialias, bool useLegacyAPI)
        {
            XamlLoader.Load(this, "/ASEvaAPIWpf;component/graphic/openglblitcontrolwpf.xaml");
            grid.Children.Add(textDraw);

            this.callback = callback;
            this.antialias = antialias;
            this.useLegacyAPI = useLegacyAPI;

            if (globalGL == null) globalGL = OpenGL.Create(funcLoader);
            gl = globalGL;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        public void ReleaseGL()
        {
            onDestroy();
        }

        public void QueueRender()
        {
            var rootWindow = Window.GetWindow(this);
            if (rootWindow != null && rootWindow.WindowState != WindowState.Minimized && Visibility == Visibility.Visible && DrawBeat.CallerBegin(this) && drawQueued < 2)
            {
                if (this.IsDescendantOf(rootWindow))
                {
                    var pixelScale = textDraw.RealPixelScale;
                    var origin = this.TransformToAncestor(rootWindow).Transform(new Point(0, 0));
                    origin = new Point(origin.X * pixelScale, origin.Y * pixelScale);
                    var dx = Math.Ceiling(origin.X) - origin.X;
                    var dy = Math.Ceiling(origin.Y) - origin.Y;
                    if (img.Margin.Left != dx || img.Margin.Top != dy) img.Margin = new Thickness(dx, dy, 0, 0);
                }

                drawQueued = Math.Min(2, drawQueued + 2);
                textDraw.QueueRender();
                DrawBeat.CallerEnd(this);
            }
        }

        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            var ct = sender as CompositionTarget;
            var args = (RenderingEventArgs)e;

            if (drawQueued == 0 || !d3dimg.IsFrontBufferAvailable || ActualWidth <= 0 || ActualHeight <= 0 || textDraw.RealPixelScale == 0) return;

            if (d3dDevice != null && d3dDevice.CheckDeviceState(IntPtr.Zero) != DeviceState.Ok)
            {
                onDestroy();
                size = null;
                initOK = null;
            }

            if (initOK == null)
            {
                onInit();
                if (initOK == null) initOK = false;
            }
            if (!initOK.Value) return;

            if (colorBuffer == null || depthBuffer == null || frameBuffer == null) return;

            var pixelScale = textDraw.RealPixelScale;
            var curSize = new GLSizeInfo((int)ActualWidth, (int)ActualHeight, (int)(pixelScale * ActualWidth), (int)(pixelScale * ActualHeight), pixelScale, (float)(ActualWidth / ActualHeight), true);
            bool resized = size == null || curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            var moduleID = callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            Win32.wglMakeCurrent(hdc, context);

            try
            {
                if (resized)
                {
                    if (fboFallback)
                    {
                        gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[1]);
                        if (antialias == GLAntialias.Disabled) gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                        else gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);

                        gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[1]);
                        if (antialias == GLAntialias.Disabled) gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                        else gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    }

                    destroyD3DSurfaces();

                    bool dummy = false;
                    createD3DSurfaces(frameBuffer[0], colorBuffer[0], depthBuffer[0], out dummy);

                    img.Width = ActualWidth;
                    img.Height = ActualHeight;
                }

                if (fboFallback)
                {
                    gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[1]);

                    if (resized) callback.OnGLResize(gl, size);

                    var textTasks = new GLTextTasks();
                    callback.OnGLRender(gl, textTasks);
                    textDraw.Texts = textTasks.Clear();

                    gl.Finish();

                    d3dimg.Lock();
                    d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, (IntPtr)d3dSurfaceBuffer);
                    gl.DXLockObjectsNV(interopDevice, interopSurface);

                    gl.BindFramebufferEXT(OpenGL.GL_READ_FRAMEBUFFER_EXT, frameBuffer[1]);
                    gl.BindFramebufferEXT(OpenGL.GL_DRAW_FRAMEBUFFER_EXT, frameBuffer[0]);
                    gl.BlitFramebufferEXT(0, 0, size.RealWidth, size.RealHeight, 0, 0, size.RealWidth, size.RealHeight, (int)OpenGL.GL_COLOR_BUFFER_BIT, (int)OpenGL.GL_NEAREST);
                    gl.Finish();

                    gl.DXUnlockObjectsNV(interopDevice, interopSurface);
                    d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                    d3dimg.Unlock();
                }
                else
                {
                    d3dimg.Lock();
                    d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, (IntPtr)d3dSurfaceBuffer);
                    gl.DXLockObjectsNV(interopDevice, interopSurface);

                    gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);

                    if (resized) callback.OnGLResize(gl, size);

                    var textTasks = new GLTextTasks();
                    callback.OnGLRender(gl, textTasks);
                    textDraw.Texts = textTasks.Clear();

                    gl.Finish();

                    gl.DXUnlockObjectsNV(interopDevice, interopSurface);
                    d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                    d3dimg.Unlock();
                }
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                onDestroy();
                initOK = false;
                return;
            }

            drawQueued--;
            DrawBeat.CallbackEnd(this);
        }

        private void onInit()
        {
            if (hwnd == IntPtr.Zero)
            {
                var rootWindow = Window.GetWindow(this);
                if (rootWindow == null) return;

                var helper = new WindowInteropHelper(rootWindow);
                hwnd = helper.Handle;
                if (hwnd == IntPtr.Zero) return;
            }

            if (hdc == IntPtr.Zero) hdc = Win32.GetDC(hwnd);
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

            try { d3d = new Direct3DEx(); }
            catch (Exception ex) { Dump.Exception(ex); }
            if (d3d == null)
            {
                onDestroy();
                return;
            }

            d3dDevice = createD3DDevice(d3d, hwnd, D3DDefaultAdapter);
            if (d3dDevice == null)
            {
                onDestroy();
                return;
            }

            Win32.wglMakeCurrent(hdc, context);

            try
            {
                var ctxInfo = new GLContextInfo(gl.Version, gl.Vendor, gl.Renderer, String.IsNullOrEmpty(gl.Extensions) ? String.Join(' ', gl.ExtensionList) : gl.Extensions);

                if (!ctxInfo.Extensions.Contains("GL_EXT_framebuffer_object") ||
                    !ctxInfo.Extensions.Contains("GL_EXT_framebuffer_blit") ||
                    !gl.IsFunctionSupported("wglDXCloseDeviceNV"))
                {
                    onDestroy();
                    return;
                }

                gl.PreloadFunction("glDeleteFramebuffersEXT");
                gl.PreloadFunction("glDeleteRenderbuffersEXT");
                gl.PreloadFunction("wglDXCloseDeviceNV");
                gl.PreloadFunction("wglDXUnregisterObjectNV");

                interopDevice = gl.DXOpenDeviceNV((IntPtr)d3dDevice);
                if (interopDevice == IntPtr.Zero)
                {
                    onDestroy();
                    return;
                }

                colorBuffer = new uint[2];
                gl.GenRenderbuffersEXT(2, colorBuffer);

                depthBuffer = new uint[2];
                gl.GenRenderbuffersEXT(2, depthBuffer);

                frameBuffer = new uint[2];
                gl.GenFramebuffersEXT(2, frameBuffer);

                var pixelScale = textDraw.RealPixelScale;
                size = new GLSizeInfo((int)ActualWidth, (int)ActualHeight, (int)(pixelScale * ActualWidth), (int)(pixelScale * ActualHeight), pixelScale, (float)(ActualWidth / ActualHeight), true);
                img.Width = ActualWidth;
                img.Height = ActualHeight;

                bool fboComplete = false;
                if (!createD3DSurfaces(frameBuffer[0], colorBuffer[0], depthBuffer[0], out fboComplete))
                {
                    onDestroy();
                    return;
                }

                if (antialias != GLAntialias.Disabled)
                {
                    if (gl.ExtensionList.Contains("GL_EXT_framebuffer_multisample"))
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

                if (!fboComplete || antialias != GLAntialias.Disabled)
                {
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[1]);
                    if (antialias == GLAntialias.Disabled) gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                    else gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);

                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[1]);
                    if (antialias == GLAntialias.Disabled) gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    else gl.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, getSampleCount(antialias), OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);

                    gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[1]);
                    gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[1]);
                    gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[1]);
                    if (gl.CheckFramebufferStatusEXT(OpenGL.GL_FRAMEBUFFER_EXT) != OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT)
                    {
                        onDestroy();
                        return;
                    }

                    fboFallback = true;
                }

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

            destroyD3DSurfaces();

            if (frameBuffer != null)
            {
                gl.DeleteFramebuffersEXT(2, frameBuffer);
                frameBuffer = null;
            }
            if (colorBuffer != null)
            {
                gl.DeleteRenderbuffersEXT(2, colorBuffer);
                colorBuffer = null;
            }
            if (depthBuffer != null)
            {
                gl.DeleteRenderbuffersEXT(2, depthBuffer);
                depthBuffer = null;
            }

            if (interopDevice != IntPtr.Zero)
            {
                gl.DXCloseDeviceNV(interopDevice);
                interopDevice = IntPtr.Zero;
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
            if (d3dDevice != null)
            {
                d3dDevice.Dispose();
                d3dDevice = null;
            }
            if (d3d != null)
            {
                d3d.Dispose();
                d3d = null;
            }

            initOK = null;
            fboFallback = false;
        }

        private DeviceEx? createD3DDevice(Direct3DEx d3d, IntPtr hwnd, int adapter)
        {
            if (hwnd == IntPtr.Zero || adapter < 0) return null;

            var caps = d3d.GetDeviceCaps(adapter, DeviceType.Hardware);
            var dwVertexProcessing = caps.DeviceCaps.HasFlag(DeviceCaps.HWTransformAndLight) ? CreateFlags.HardwareVertexProcessing : CreateFlags.SoftwareVertexProcessing;

            var d3dpp = new PresentParameters();
            d3dpp.Windowed = true;
            d3dpp.BackBufferFormat = Format.Unknown;
            d3dpp.BackBufferHeight = 1;
            d3dpp.BackBufferWidth = 1;
            d3dpp.SwapEffect = SwapEffect.Discard;

            return new DeviceEx(d3d, adapter, DeviceType.Hardware, hwnd, dwVertexProcessing | CreateFlags.Multithreaded | CreateFlags.FpuPreserve, d3dpp);
        }

        private Surface? createD3DSurface(Direct3DEx d3d, DeviceEx d3dDevice, int adapter, uint surfaceWidth, uint surfaceHeight, ref IntPtr shareHandle)
        {
            if (adapter < 0) return null;

            if (!d3d.CheckDeviceType(adapter, DeviceType.Hardware, Format.X8R8G8B8, Format.X8R8G8B8, true)) return null;
            if (!d3d.CheckDeviceFormat(adapter, DeviceType.Hardware, Format.X8R8G8B8, Usage.RenderTarget | Usage.Dynamic, ResourceType.Surface, Format.X8R8G8B8)) return null;

            var d3dSurface = Surface.CreateRenderTarget(d3dDevice, (int)surfaceWidth, (int)surfaceHeight, Format.X8R8G8B8, MultisampleType.None, 0, false, ref shareHandle);
            if (d3dSurface == null) return null;

            return d3dSurface;
        }

        private bool createD3DSurfaces(uint frameBufferName, uint colorBufferName, uint depthBufferName, out bool fboComplete)
        {
            fboComplete = false;

            if (d3d == null || d3dDevice == null || size == null) return false;

            IntPtr d3dSurfaceBufferSH = IntPtr.Zero, dummy = IntPtr.Zero;
            d3dSurfaceBuffer = createD3DSurface(d3d, d3dDevice, D3DDefaultAdapter, (uint)size.RealWidth, (uint)size.RealHeight, ref d3dSurfaceBufferSH);
            if (d3dSurfaceBuffer == null) return false;

            if (d3dSurfaceBufferSH != IntPtr.Zero) gl.DXSetResourceShareHandleNV((IntPtr)d3dSurfaceBuffer, d3dSurfaceBufferSH);

            interopSurface[0] = gl.DXRegisterObjectNV(interopDevice, (IntPtr)d3dSurfaceBuffer, colorBufferName, OpenGL.GL_RENDERBUFFER, OpenGL.WGL_ACCESS_READ_WRITE_NV);
            if (interopSurface[0] == IntPtr.Zero) return false;

            gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBufferName);
            gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);

            gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBufferName);
            gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBufferName);
            gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBufferName);

            fboComplete = gl.CheckFramebufferStatusEXT(OpenGL.GL_FRAMEBUFFER_EXT) == OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT;

            return true;
        }

        private void destroyD3DSurfaces()
        {
            if (interopDevice != IntPtr.Zero && interopSurface[0] != IntPtr.Zero)
            {
                gl.DXUnregisterObjectNV(interopDevice, interopSurface[0]);
                interopSurface[0] = IntPtr.Zero;
            }

            if (d3dSurfaceBuffer != null)
            {
                d3dSurfaceBuffer.Dispose();
                d3dSurfaceBuffer = null;
            }
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
                    var glCoreVersions = new[]
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

        private IntPtr hwnd = IntPtr.Zero;
        private Direct3DEx? d3d;
        private DeviceEx? d3dDevice;
        private Surface? d3dSurfaceBuffer;
        private IntPtr interopDevice = IntPtr.Zero;
        private IntPtr[] interopSurface = [IntPtr.Zero];

        private WindowsFuncLoader funcLoader = new WindowsFuncLoader();
        private GLSizeInfo? size = null;
        private IntPtr hdc = IntPtr.Zero;
        private IntPtr context = IntPtr.Zero;
        private GLCallback callback;
        private GLAntialias antialias;
        private bool useLegacyAPI;

        private uint[]? frameBuffer = null; // [interop, fallback/multisample]
        private uint[]? colorBuffer = null; // [interop, fallback/multisample]
        private uint[]? depthBuffer = null; // [interop, fallback/multisample]
        private bool fboFallback = false;
        private bool? initOK = null;
        private int drawQueued = 0;

        private TextDraw textDraw = new TextDraw();
        private OpenGL gl;

        private static OpenGL? globalGL;
        private static bool createContextAttribsARBUnsupported = false;
    }
}
