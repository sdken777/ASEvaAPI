using System;
using System.Collections.Generic;
using Gtk;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEva.UIGtk
{
    #pragma warning disable 612
    class X11OffscreenView : Box, GLBackend
    {
        public X11OffscreenView(GLCallback callback, GLAntialias antialias, bool useLegacyAPI) : base(Orientation.Vertical, 0)
        {
            this.callback = callback;
            this.antialias = antialias;
            this.useLegacyAPI = useLegacyAPI;
            this.gl = OpenGL.Create(new LinuxFuncLoader());

            dummyArea.WidthRequest = 1;
            dummyArea.HeightRequest = 1;

            PackStart(realArea, true, true, 0);

            var dummyBox = new Box(Orientation.Horizontal, 0);
            dummyBox.PackStart(dummyArea, false, false, 0);
            PackStart(dummyBox, false, false, 0);

            dummyArea.Realized += onRealized;
            realArea.Drawn += onDraw;
        }

        public void ReleaseGL()
        {
            if (!rendererStatusOK) return;
            onDestroy();
        }

        public void QueueRender()
        {
            if (Toplevel != null && Toplevel is Window && !(Toplevel as Window).Window.State.HasFlag(Gdk.WindowState.Iconified) && !drawQueued && DrawBeat.CallerBegin(this))
            {
                QueueDraw();
                drawQueued = true;
                DrawBeat.CallerEnd(this);
            }
        }

        private void onDestroy()
        {
            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);

            if (context != IntPtr.Zero)
            {
                Linux.glXMakeCurrent(display, xid, context);
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
            if (cairoSurface != null)
            {
                cairoSurface.Dispose();
                cairoSurface = null;
            }
            if (context != IntPtr.Zero)
            {
                Linux.glXDestroyContext(display, context);
                context = IntPtr.Zero;
            }

            rendererStatusOK = false;
        }

        private void onRealized(object sender, EventArgs e)
        {
            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            if (display == IntPtr.Zero) return;

            IntPtr visual = Linux.gdk_x11_visual_get_xvisual(dummyArea.Window.Visual.Handle);
            if (visual == IntPtr.Zero) return;

            int screen = Linux.gdk_x11_screen_get_screen_number(dummyArea.Window.Screen.Handle);
            uint visualID = Linux.XVisualIDFromVisual(visual);

            var vinfoTemplate = new XVisualInfo[1];
            vinfoTemplate[0].visualid = visualID;
            vinfoTemplate[0].screen = screen;

            var dummy = new int[1];
            IntPtr vinfo = IntPtr.Zero;
            unsafe
            {
                fixed (XVisualInfo *vtemplate = &(vinfoTemplate[0]))
                {
                    fixed (int *count = &(dummy[0]))
                    {
                        vinfo = Linux.XGetVisualInfo(display, 3, vtemplate, count);
                    }
                }
            }
            if (vinfo == IntPtr.Zero) return;

            IntPtr fbConfig = IntPtr.Zero;
            bool contextCreated = false;
            if (!useLegacyAPI && chooseFBConfigForVisual(vinfo, out fbConfig))
            {
                try
                {
                    var glCoreVersions = new Version[]
                    {
                        // new Version(4, 6), // 可能导致崩溃
                        new Version(3, 3)
                    };

                    foreach (var ver in glCoreVersions)
                    {
                        var attribs = new int[]
                        {
                            0x2091, // GLX_CONTEXT_MAJOR_VERSION_ARB
                            ver.Major,
                            0x2092, // GLX_CONTEXT_MINOR_VERSION_ARB
                            ver.Minor,
                            (int)OpenGL.GL_CONTEXT_PROFILE_MASK,
                            (int)OpenGL.GL_CONTEXT_CORE_PROFILE_BIT,
                            0
                        };
                        unsafe
                        {
                            fixed (int *attribsPtr = &(attribs[0]))
                            {
                                context = Linux.glXCreateContextAttribsARB(display, fbConfig, IntPtr.Zero, true, attribsPtr);
                                if (context != IntPtr.Zero)
                                {
                                    contextCreated = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception) {}
            }
            if (!contextCreated)
            {
                context = Linux.glXCreateContext(display, vinfo, IntPtr.Zero, true);
                if (context == IntPtr.Zero) return;
            }

            xid = Linux.gdk_x11_window_get_xid(dummyArea.Window.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;
                if (String.IsNullOrEmpty(ctxInfo.extensions)) ctxInfo.extensions = String.Join(' ', gl.ExtensionList);
                
                size = new GLSizeInfo(realArea.AllocatedWidth, realArea.AllocatedHeight, realArea.AllocatedWidth * realArea.ScaleFactor, realArea.AllocatedHeight * realArea.ScaleFactor, realArea.ScaleFactor, (float)realArea.AllocatedWidth / realArea.AllocatedHeight);

                if (!gl.ExtensionList.Contains("GL_EXT_framebuffer_object"))
                {
                    onDestroy();
                    return;
                }

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

                if (cairoSurface == null || cairoSurface.Width != size.RealWidth || cairoSurface.Height != size.RealHeight)
                {
                    if (cairoSurface != null)
                    {
                        cairoSurface.Dispose();
                        cairoSurface = null;
                    }
                    cairoSurface = new Cairo.ImageSurface(Cairo.Format.RGB24, size.RealWidth, size.RealHeight);
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

            rendererStatusOK = true;
        }

        private void onDraw(object o, DrawnArgs args)
        {
            if (!rendererStatusOK) return;

            var moduleID = callback == null ? null : callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            var curSize = new GLSizeInfo(realArea.AllocatedWidth, realArea.AllocatedHeight, realArea.AllocatedWidth * realArea.ScaleFactor, realArea.AllocatedHeight * realArea.ScaleFactor, realArea.ScaleFactor, (float)realArea.AllocatedWidth / realArea.AllocatedHeight);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            Linux.glXMakeCurrent(display, xid, context);

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
                    if (cairoSurface != null)
                    {
                        cairoSurface.Dispose();
                        cairoSurface = null;
                    }
                    cairoSurface = new Cairo.ImageSurface(Cairo.Format.RGB24, size.RealWidth, size.RealHeight);
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

                var cairoWidth = size.RealWidth;
                var cairoHeight = size.RealHeight;
                var cairoSurfaceStep = cairoSurface.Stride;

                gl.ReadPixels(0, 0, cairoWidth, cairoHeight, OpenGL.GL_RGBA, hostBuffer);

                unsafe
                {
                    byte *surfaceData = (byte*)cairoSurface.DataPtr;
                    fixed (byte *srcData = &(hostBuffer[0]))
                    {
                        for (int v = 0; v < cairoHeight; v++)
                        {
                            uint *srcRow = (uint*)&srcData[v * cairoWidth * 4];
                            uint *dstRow = (uint*)&surfaceData[(cairoHeight - 1 - v) * cairoSurfaceStep];
                            for (int u = 0; u < cairoWidth; u++)
                            {
                                dstRow[u] = ((srcRow[u] & 0x000000ff) << 16) | (srcRow[u] & 0x0000ff00) | ((srcRow[u] & 0x00ff0000) >> 16) | 0xff000000;
                            }
                        }
                    }
                }

                var cairo = args.Cr;
                cairo.Save();
                var cairoScale = 1.0 / size.RealPixelScale;
                cairo.Scale(cairoScale, cairoScale);
                cairo.SetSourceSurface(cairoSurface, 0, 0);
                cairo.Paint();
                cairo.Restore();

                CairoDrawText.Draw(cairo, textTasks.Clear(), size);
            }
            catch (Exception)
            {
                onDestroy();
            }

            drawQueued = false;
            DrawBeat.CallbackEnd(this);
        }

        private int? getVisualAttrib(IntPtr x11Display, IntPtr x11Visual, int attrib)
        {
            unsafe
            {
                var val = new int[1];
                fixed (int *valPtr = &val[0])
                {
                    int res = Linux.glXGetConfig(x11Display, x11Visual, attrib, valPtr);
                    return res == 0 ? (int?)val[0] : null;
                }
            }
        }

        private bool chooseFBConfigForVisual(IntPtr x11VisualInfo, out IntPtr fbConfig)
        {
            fbConfig = IntPtr.Zero;

            IntPtr x11Display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            if (x11Display == IntPtr.Zero) return false;

            var bufferSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_BUFFER_SIZE);
            var level = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_LEVEL);
            var doubleBuffer = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_DOUBLEBUFFER);
            var stereo = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_STEREO);
            var auxBuffers = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_AUX_BUFFERS);
            var redSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_RED_SIZE);
            var greenSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_GREEN_SIZE);
            var blueSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_BLUE_SIZE);
            var alphaSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_ALPHA_SIZE);
            var depthSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_DEPTH_SIZE);
            var stencilSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_STENCIL_SIZE);
            var accumRedSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_ACCUM_RED_SIZE);
            var accumGreenSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_ACCUM_GREEN_SIZE);
            var accumBlueSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_ACCUM_BLUE_SIZE);
            var accumAlphaSize = getVisualAttrib(x11Display, x11VisualInfo, Linux.GLX_ACCUM_ALPHA_SIZE);

            var attribList = new List<int>();
            if (bufferSize != null)
            {
                attribList.Add(Linux.GLX_BUFFER_SIZE);
                attribList.Add(bufferSize.Value);
            }
            if (level != null)
            {
                attribList.Add(Linux.GLX_LEVEL);
                attribList.Add(level.Value);
            }
            if (doubleBuffer != null)
            {
                attribList.Add(Linux.GLX_DOUBLEBUFFER);
                attribList.Add(doubleBuffer.Value);
            }
            if (stereo != null)
            {
                attribList.Add(Linux.GLX_STEREO);
                attribList.Add(stereo.Value);
            }
            if (auxBuffers != null)
            {
                attribList.Add(Linux.GLX_AUX_BUFFERS);
                attribList.Add(auxBuffers.Value);
            }
            if (redSize != null)
            {
                attribList.Add(Linux.GLX_RED_SIZE);
                attribList.Add(redSize.Value);
            }
            if (greenSize != null)
            {
                attribList.Add(Linux.GLX_GREEN_SIZE);
                attribList.Add(greenSize.Value);
            }
            if (blueSize != null)
            {
                attribList.Add(Linux.GLX_BLUE_SIZE);
                attribList.Add(blueSize.Value);
            }
            if (alphaSize != null)
            {
                attribList.Add(Linux.GLX_ALPHA_SIZE);
                attribList.Add(alphaSize.Value);
            }
            if (depthSize != null)
            {
                attribList.Add(Linux.GLX_DEPTH_SIZE);
                attribList.Add(depthSize.Value);
            }
            if (stencilSize != null)
            {
                attribList.Add(Linux.GLX_STENCIL_SIZE);
                attribList.Add(stencilSize.Value);
            }
            if (accumRedSize != null)
            {
                attribList.Add(Linux.GLX_ACCUM_RED_SIZE);
                attribList.Add(accumRedSize.Value);
            }
            if (accumGreenSize != null)
            {
                attribList.Add(Linux.GLX_ACCUM_GREEN_SIZE);
                attribList.Add(accumGreenSize.Value);
            }
            if (accumBlueSize != null)
            {
                attribList.Add(Linux.GLX_ACCUM_BLUE_SIZE);
                attribList.Add(accumBlueSize.Value);
            }
            if (accumAlphaSize != null)
            {
                attribList.Add(Linux.GLX_ACCUM_ALPHA_SIZE);
                attribList.Add(accumAlphaSize.Value);
            }
            if (attribList.Count == 0) return false;

            attribList.Add(0);

            int snumber = Linux.gdk_x11_screen_get_screen_number(Display.DefaultScreen.Handle);
            var attribs = attribList.ToArray();

            unsafe
            {
                fixed (int *attribsPtr = &(attribs[0]))
                {
                    int nElements = 0;
                    IntPtr *fbConfigs = Linux.glXChooseFBConfig(x11Display, snumber, attribsPtr, &nElements);
                    if (nElements == 0 || fbConfigs == null) return false;

                    fbConfig = fbConfigs[0];
                    return true;
                }
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

        private OpenGL gl = null;
        private GLCallback callback;
        private IntPtr context = IntPtr.Zero;
        private uint xid = 0;
        private uint[] frameBuffer = null;
        private uint[] colorBuffer = null;
        private uint[] depthBuffer = null;
        private byte[] hostBuffer = null;
        private Cairo.ImageSurface cairoSurface = null;
        private bool rendererStatusOK = false;
        private GLSizeInfo size = null;
        private bool drawQueued = false;
        private DrawingArea realArea = new DrawingArea();
        private DrawingArea dummyArea = new DrawingArea();
        private GLAntialias antialias;
        private bool useLegacyAPI;
    }
}