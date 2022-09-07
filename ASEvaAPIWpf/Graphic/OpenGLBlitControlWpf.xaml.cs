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
    /// <summary>
    /// OpenGLBlitControlWpf.xaml 的交互逻辑
    /// </summary>
    partial class OpenGLBlitControlWpf : UserControl, GLBackend
    {
        private const int D3DDefaultAdapter = 0;

        public OpenGLBlitControlWpf()
        {
            XamlLoader.Load(this, "/ASEvaAPIWpf;component/graphic/openglblitcontrolwpf.xaml");
            grid.Children.Add(textDraw);

            gl = OpenGL.Create(funcLoader);

            CompositionTarget.Rendering += CompositionTarget_Rendering;
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
            var rootWindow = Window.GetWindow(this);
            if (rootWindow != null && rootWindow.WindowState != WindowState.Minimized && Visibility == Visibility.Visible && DrawBeat.CallerBegin(this))
            {
                drawQueued = true;
                textDraw.InvalidateVisual();
                DrawBeat.CallerEnd(this);
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var ct = sender as CompositionTarget;
            var args = (RenderingEventArgs)e;

            if (!drawQueued || !d3dimg.IsFrontBufferAvailable || ActualWidth <= 0 || ActualHeight <= 0 || textDraw.RealPixelScale == 0) return;

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

            var pixelScale = textDraw.RealPixelScale;
            var curSize = new GLSizeInfo((int)ActualWidth, (int)ActualHeight, (int)(pixelScale * ActualWidth), (int)(pixelScale * ActualHeight), pixelScale, (float)(ActualWidth / ActualHeight), true);
            bool resized = size == null || curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            var moduleID = callback == null ? null : callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            Win32.wglMakeCurrent(hdc, context);

            try
            {
                if (resized)
                {
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);
                    gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                    gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);
                    destroyD3DSurfaces();
                    createD3DSurfaces(frameBuffer[1], colorBuffer[1]);
                    img.Width = ActualWidth;
                    img.Height = ActualHeight;
                }

                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);

                if (resized) callback.OnGLResize(gl, size);

                var textTasks = new GLTextTasks();
                callback.OnGLRender(gl, textTasks);
                textDraw.Texts = textTasks.Clear();

                gl.Finish();
                
                gl.DXLockObjectsNV(interopDevice, interopSurface);
                gl.BindFramebufferEXT(GL_READ_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.BindFramebufferEXT(GL_DRAW_FRAMEBUFFER_EXT, frameBuffer[1]);
                glBlitFramebufferEXT(0, 0, size.RealWidth, size.RealHeight, 0, 0, size.RealWidth, size.RealHeight, (int)OpenGL.GL_COLOR_BUFFER_BIT, (int)OpenGL.GL_NEAREST);
                gl.Finish();
                gl.DXUnlockObjectsNV(interopDevice, interopSurface);

                d3dimg.Lock();
                d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, (IntPtr)d3dSurfaceDraw);
                d3dDevice.StretchRectangle(d3dSurfaceBuffer, d3dSurfaceDraw, TextureFilter.Point);
                d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                d3dimg.Unlock();
            }
            catch (Exception)
            {
                onDestroy();
                initOK = false;
                return;
            }

            drawQueued = false;
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

            context = Win32.wglCreateContext(hdc);
            if (context == IntPtr.Zero) return;

            try { d3d = new Direct3DEx(); }
            catch (Exception) { }
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
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;

                var wglExtensionsString = WGL.GetWglExtensionsString(gl, hdc);
                ctxInfo.extensions = gl.Extensions + (wglExtensionsString.Length == 0 ? "" : (" " + wglExtensionsString));

                var extensionList = new List<String>();
                extensionList.AddRange(gl.ExtensionList);
                extensionList.AddRange(wglExtensionsString.Split(' '));

                if (!extensionList.Contains("GL_EXT_framebuffer_object") ||
                    !extensionList.Contains("GL_EXT_framebuffer_blit") ||
                    !extensionList.Contains("WGL_NV_DX_interop"))
                {
                    onDestroy();
                    return;
                }

                if (glBlitFramebufferEXT == null)
                {
                    IntPtr funcAddress = funcLoader.GetFunctionAddress("glBlitFramebufferEXT");
                    if (funcAddress != IntPtr.Zero)
                    {
                        Type delegateType = typeof(BlitFramebufferDelegate);
                        glBlitFramebufferEXT = Marshal.GetDelegateForFunctionPointer(funcAddress, delegateType) as BlitFramebufferDelegate;
                    }
                }
                if (glBlitFramebufferEXT == null)
                {
                    onDestroy();
                    return;
                }

                interopDevice = gl.DXOpenDeviceNV((IntPtr)d3dDevice);
                if (interopDevice == IntPtr.Zero)
                {
                    onDestroy();
                    return;
                }

                colorBuffer = new uint[2];
                gl.GenRenderbuffersEXT(2, colorBuffer);

                frameBuffer = new uint[2];
                gl.GenFramebuffersEXT(2, frameBuffer);

                var pixelScale = textDraw.RealPixelScale;
                size = new GLSizeInfo((int)ActualWidth, (int)ActualHeight, (int)(pixelScale * ActualWidth), (int)(pixelScale * ActualHeight), pixelScale, (float)(ActualWidth / ActualHeight), true);
                img.Width = ActualWidth;
                img.Height = ActualHeight;

                if (!createD3DSurfaces(frameBuffer[1], colorBuffer[1]))
                {
                    onDestroy();
                    return;
                }

                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, colorBuffer[0]);
                gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGB8, size.RealWidth, size.RealHeight);

                depthBuffer = new uint[1];
                gl.GenRenderbuffersEXT(1, depthBuffer);
                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, depthBuffer[0]);
                gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT16, size.RealWidth, size.RealHeight);

                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBuffer[0]);
                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, depthBuffer[0]);
                if (gl.CheckFramebufferStatusEXT(OpenGL.GL_FRAMEBUFFER_EXT) != OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT)
                {
                    onDestroy();
                    return;
                }

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
                gl.DeleteRenderbuffersEXT(1, depthBuffer);
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
        }

        private DeviceEx createD3DDevice(Direct3DEx d3d, IntPtr hwnd, int adapter)
        {
            if (d3d == null || hwnd == IntPtr.Zero || adapter < 0) return null;

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

        private Surface createD3DSurface(Direct3DEx d3d, DeviceEx d3dDevice, int adapter, uint surfaceWidth, uint surfaceHeight)
        {
            if (d3d == null || d3dDevice == null || adapter < 0) return null;

            if (!d3d.CheckDeviceType(adapter, DeviceType.Hardware, Format.X8R8G8B8, Format.X8R8G8B8, true)) return null;
            if (!d3d.CheckDeviceFormat(adapter, DeviceType.Hardware, Format.X8R8G8B8, Usage.RenderTarget | Usage.Dynamic, ResourceType.Surface, Format.X8R8G8B8)) return null;

            var d3dSurface = Surface.CreateRenderTarget(d3dDevice, (int)surfaceWidth, (int)surfaceHeight, Format.X8R8G8B8, MultisampleType.None, 0, false);
            if (d3dSurface == null) return null;

            return d3dSurface;
        }

        private bool createD3DSurfaces(uint frameBufferName, uint colorBufferName)
        {
            d3dSurfaceBuffer = createD3DSurface(d3d, d3dDevice, D3DDefaultAdapter, (uint)size.RealWidth, (uint)size.RealHeight);
            d3dSurfaceDraw = createD3DSurface(d3d, d3dDevice, D3DDefaultAdapter, (uint)size.RealWidth, (uint)size.RealHeight);
            if (d3dSurfaceBuffer == null || d3dSurfaceDraw == null) return false;

            interopSurface[0] = gl.DXRegisterObjectNV(interopDevice, (IntPtr)d3dSurfaceBuffer, colorBufferName, OpenGL.GL_RENDERBUFFER, OpenGL.WGL_ACCESS_READ_WRITE_NV);
            if (interopSurface[0] == IntPtr.Zero) return false;

            gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBufferName);
            gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, colorBufferName);

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
            if (d3dSurfaceDraw != null)
            {
                d3dSurfaceDraw.Dispose();
                d3dSurfaceDraw = null;
            }
        }

        private const int GL_READ_FRAMEBUFFER_EXT = 0x8CA8;
        private const int GL_DRAW_FRAMEBUFFER_EXT = 0x8CA9;

        private delegate void BlitFramebufferDelegate(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, int mask, int filter);
        private BlitFramebufferDelegate glBlitFramebufferEXT = null;

        private IntPtr hwnd = IntPtr.Zero;
        private Direct3DEx d3d;
        private DeviceEx d3dDevice;
        private Surface d3dSurfaceBuffer, d3dSurfaceDraw;
        private IntPtr interopDevice = IntPtr.Zero;
        private IntPtr[] interopSurface = new IntPtr[] { IntPtr.Zero };

        private WindowsFuncLoader funcLoader = new WindowsFuncLoader();
        private OpenGL gl = null;
        private GLSizeInfo size = null;
        private IntPtr hdc = IntPtr.Zero;
        private IntPtr context = IntPtr.Zero;
        private GLCallback callback = null;
        private uint[] frameBuffer = null; // Length=2
        private uint[] colorBuffer = null; // Length=2
        private uint[] depthBuffer = null; // Length=1
        private bool? initOK = null;
        private bool drawQueued = false;

        private TextDraw textDraw = new TextDraw();
    }
}
