using System;
using System.Collections.Generic;
using Gtk;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEva.UIGtk
{
    #pragma warning disable 612
    class X11OnscreenView : DrawingArea, GLBackend
    {
        public X11OnscreenView(GLCallback callback, GLAntialias antialias, bool useLegacyAPI)
        {
            this.callback = callback;
            this.gl = OpenGL.Create(new LinuxFuncLoader());
            this.useLegacyAPI = useLegacyAPI;

            DoubleBuffered = false;

            Gdk.Visual visualToSet;
            IntPtr basicVisualInfo, basicFBConfig, antialiasVisualInfo, antialiasFBConfig;
            this.enableAntialias = chooseVisualAndFBConfig(antialias, out visualToSet, out basicVisualInfo, out basicFBConfig, out antialiasVisualInfo, out antialiasFBConfig);

            Visual = visualToSet;
            if (this.enableAntialias && antialiasVisualInfo != IntPtr.Zero)
            {
                targetVisualInfo = antialiasVisualInfo;
                targetFBConfig = antialiasFBConfig;
            }
            else
            {
                targetVisualInfo = basicVisualInfo;
                targetFBConfig = basicFBConfig;
            }

            Realized += onRealized;
            Drawn += onDraw;
        }

        public void ReleaseGL()
        {
            if (!rendererStatusOK) return;
            onDestroy();
        }

        public void QueueRender()
        {
            if (Toplevel != null && Toplevel is Window window && !window.Window.State.HasFlag(Gdk.WindowState.Iconified) && !drawQueued && DrawBeat.CallerBegin(this))
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
            if (context != IntPtr.Zero)
            {
                WebViewHandler.PreInitializeSettings();
                Linux.glXDestroyContext(display, context);
                context = IntPtr.Zero;
            }

            rendererStatusOK = false;
        }

        private void onRealized(object? sender, EventArgs e)
        {
            if (targetVisualInfo == IntPtr.Zero) return;

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Window.Display.Handle);
            if (display == IntPtr.Zero) return;

            bool contextCreated = false;
            if (!useLegacyAPI && targetFBConfig != IntPtr.Zero)
            {
                try
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
                                context = Linux.glXCreateContextAttribsARB(display, targetFBConfig, IntPtr.Zero, true, attribsPtr);
                                if (context != IntPtr.Zero)
                                {
                                    contextCreated = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { Dump.Exception(ex); }
            }
            if (!contextCreated)
            {
                context = Linux.glXCreateContext(display, targetVisualInfo, IntPtr.Zero, true);
                if (context == IntPtr.Zero) return;
            }

            xid = Linux.gdk_x11_window_get_xid(Window.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            try
            {
                var ctxInfo = new GLContextInfo(gl.Version, gl.Vendor, gl.Renderer, String.IsNullOrEmpty(gl.Extensions) ? String.Join(' ', gl.ExtensionList) : gl.Extensions);
                
                size = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);

                callback.OnGLInitialize(gl, ctxInfo);
                if (enableAntialias) gl.Enable(OpenGL.GL_MULTISAMPLE);

                callback.OnGLResize(gl, size);

                gl.Flush();
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                onDestroy();
                return;
            }

            rendererStatusOK = true;
        }

        private void onDraw(object? o, DrawnArgs args)
        {
            if (!rendererStatusOK) return;

            var moduleID = callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            var curSize = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);
            bool resized = size == null || curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
            size = curSize;

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            try
            {
                if (resized) callback.OnGLResize(gl, size);

                var dummy = new GLTextTasks();
                callback.OnGLRender(gl, dummy);
                gl.Finish();
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                onDestroy();
            }

            if (!swapIntervalFailed)
            {
                try
                {
                    Linux.glXSwapIntervalEXT(display, xid, 0);
                }
                catch (Exception ex)
                {
                    Dump.Exception(ex);
                    swapIntervalFailed = true;
                }
            }
            
            Linux.glXSwapBuffers(display, xid);
            Linux.glXMakeCurrent(display, 0, IntPtr.Zero);

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

        private IntPtr getVisualInfo(uint visualID, IntPtr x11Display)
        {
            var vinfoTemplate = new XVisualInfo[1];
            vinfoTemplate[0].visualid = visualID;

            var dummy = new int[1];
            IntPtr visualInfo = IntPtr.Zero;
            unsafe
            {
                fixed (XVisualInfo *vtemplate = &(vinfoTemplate[0]))
                {
                    fixed (int *count = &(dummy[0]))
                    {
                        visualInfo = Linux.XGetVisualInfo(x11Display, 1, vtemplate, count);
                    }
                }
            }
            return visualInfo;
        }

        private bool chooseVisualAndFBConfig(GLAntialias antialias, out Gdk.Visual gdkVisual, out IntPtr basicVisualInfo, out IntPtr basicFBConfig, out IntPtr antialiasVisualInfo, out IntPtr antialiasFBConfig)
        {
            gdkVisual = Gdk.Visual.Best;
            basicVisualInfo = IntPtr.Zero;
            basicFBConfig = IntPtr.Zero;
            antialiasVisualInfo = IntPtr.Zero;
            antialiasFBConfig = IntPtr.Zero;

            // 获取相关变量
            IntPtr x11Display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            if (x11Display == IntPtr.Zero) return false;

            IntPtr x11OriginVisual = Linux.gdk_x11_visual_get_xvisual(Gdk.Visual.Best.Handle);
            if (x11OriginVisual == IntPtr.Zero) return false;

            basicVisualInfo = getVisualInfo(Linux.XVisualIDFromVisual(x11OriginVisual), x11Display);
            if (basicVisualInfo == IntPtr.Zero) return false;

            int snumber = Linux.gdk_x11_screen_get_screen_number(Display.DefaultScreen.Handle);

            int[]? sampleCounts = null;
            switch (antialias)
            {
                case GLAntialias.Sample2x:
                    sampleCounts = [2];
                    break;
                case GLAntialias.Sample4x:
                    sampleCounts = [4, 2];
                    break;
                case GLAntialias.Sample8x:
                    sampleCounts = [8, 4, 2];
                    break;
                case GLAntialias.Sample16x:
                    sampleCounts = [16, 8, 4, 2];
                    break;
            }

            // 初始化通用属性配置
            var bufferSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_BUFFER_SIZE);
            var level = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_LEVEL);
            var rgba = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_RGBA);
            var doubleBuffer = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_DOUBLEBUFFER);
            var stereo = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_STEREO);
            var auxBuffers = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_AUX_BUFFERS);
            var redSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_RED_SIZE);
            var greenSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_GREEN_SIZE);
            var blueSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_BLUE_SIZE);
            var alphaSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_ALPHA_SIZE);
            var depthSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_DEPTH_SIZE);
            var stencilSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_STENCIL_SIZE);
            var accumRedSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_ACCUM_RED_SIZE);
            var accumGreenSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_ACCUM_GREEN_SIZE);
            var accumBlueSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_ACCUM_BLUE_SIZE);
            var accumAlphaSize = getVisualAttrib(x11Display, basicVisualInfo, Linux.GLX_ACCUM_ALPHA_SIZE);

            var attribListForVisual = new List<int>();
            var attribListForFBConfig = new List<int>();
            if (bufferSize != null)
            {
                attribListForVisual.Add(Linux.GLX_BUFFER_SIZE);
                attribListForVisual.Add(bufferSize.Value);
                attribListForFBConfig.Add(Linux.GLX_BUFFER_SIZE);
                attribListForFBConfig.Add(bufferSize.Value);
            }
            if (level != null)
            {
                attribListForVisual.Add(Linux.GLX_LEVEL);
                attribListForVisual.Add(level.Value);
                attribListForFBConfig.Add(Linux.GLX_LEVEL);
                attribListForFBConfig.Add(level.Value);
            }
            if (rgba != null)
            {
                if (rgba.Value == 1) attribListForVisual.Add(Linux.GLX_RGBA);
            }
            if (doubleBuffer != null)
            {
                if (doubleBuffer.Value == 1) attribListForVisual.Add(Linux.GLX_DOUBLEBUFFER);
                attribListForFBConfig.Add(Linux.GLX_DOUBLEBUFFER);
                attribListForFBConfig.Add(doubleBuffer.Value);
            }
            if (stereo != null)
            {
                if (stereo.Value == 1) attribListForVisual.Add(Linux.GLX_STEREO);
                attribListForFBConfig.Add(Linux.GLX_STEREO);
                attribListForFBConfig.Add(stereo.Value);
            }
            if (auxBuffers != null)
            {
                attribListForVisual.Add(Linux.GLX_AUX_BUFFERS);
                attribListForVisual.Add(auxBuffers.Value);
                attribListForFBConfig.Add(Linux.GLX_AUX_BUFFERS);
                attribListForFBConfig.Add(auxBuffers.Value);
            }
            if (redSize != null)
            {
                attribListForVisual.Add(Linux.GLX_RED_SIZE);
                attribListForVisual.Add(redSize.Value);
                attribListForFBConfig.Add(Linux.GLX_RED_SIZE);
                attribListForFBConfig.Add(redSize.Value);
            }
            if (greenSize != null)
            {
                attribListForVisual.Add(Linux.GLX_GREEN_SIZE);
                attribListForVisual.Add(greenSize.Value);
                attribListForFBConfig.Add(Linux.GLX_GREEN_SIZE);
                attribListForFBConfig.Add(greenSize.Value);
            }
            if (blueSize != null)
            {
                attribListForVisual.Add(Linux.GLX_BLUE_SIZE);
                attribListForVisual.Add(blueSize.Value);
                attribListForFBConfig.Add(Linux.GLX_BLUE_SIZE);
                attribListForFBConfig.Add(blueSize.Value);
            }
            if (alphaSize != null)
            {
                attribListForVisual.Add(Linux.GLX_ALPHA_SIZE);
                attribListForVisual.Add(alphaSize.Value);
                attribListForFBConfig.Add(Linux.GLX_ALPHA_SIZE);
                attribListForFBConfig.Add(alphaSize.Value);
            }
            if (depthSize != null)
            {
                attribListForVisual.Add(Linux.GLX_DEPTH_SIZE);
                attribListForVisual.Add(depthSize.Value);
                attribListForFBConfig.Add(Linux.GLX_DEPTH_SIZE);
                attribListForFBConfig.Add(depthSize.Value);
            }
            if (stencilSize != null)
            {
                attribListForVisual.Add(Linux.GLX_STENCIL_SIZE);
                attribListForVisual.Add(stencilSize.Value);
                attribListForFBConfig.Add(Linux.GLX_STENCIL_SIZE);
                attribListForFBConfig.Add(stencilSize.Value);
            }
            if (accumRedSize != null)
            {
                attribListForVisual.Add(Linux.GLX_ACCUM_RED_SIZE);
                attribListForVisual.Add(accumRedSize.Value);
                attribListForFBConfig.Add(Linux.GLX_ACCUM_RED_SIZE);
                attribListForFBConfig.Add(accumRedSize.Value);
            }
            if (accumGreenSize != null)
            {
                attribListForVisual.Add(Linux.GLX_ACCUM_GREEN_SIZE);
                attribListForVisual.Add(accumGreenSize.Value);
                attribListForFBConfig.Add(Linux.GLX_ACCUM_GREEN_SIZE);
                attribListForFBConfig.Add(accumGreenSize.Value);
            }
            if (accumBlueSize != null)
            {
                attribListForVisual.Add(Linux.GLX_ACCUM_BLUE_SIZE);
                attribListForVisual.Add(accumBlueSize.Value);
                attribListForFBConfig.Add(Linux.GLX_ACCUM_BLUE_SIZE);
                attribListForFBConfig.Add(accumBlueSize.Value);
            }
            if (accumAlphaSize != null)
            {
                attribListForVisual.Add(Linux.GLX_ACCUM_ALPHA_SIZE);
                attribListForVisual.Add(accumAlphaSize.Value);
                attribListForFBConfig.Add(Linux.GLX_ACCUM_ALPHA_SIZE);
                attribListForFBConfig.Add(accumAlphaSize.Value);
            }

            // 查找基础FBConfig
            if (attribListForFBConfig.Count > 0)
            {
                var attribList = new List<int>();
                attribList.AddRange(attribListForFBConfig);
                attribList.Add(0);

                var attribs = attribList.ToArray();
                unsafe
                {
                    fixed (int *attribsPtr = &(attribs[0]))
                    {
                        int nElements = 0;
                        IntPtr* fbConfigs = Linux.glXChooseFBConfig(x11Display, snumber, attribsPtr, &nElements);
                        if (nElements > 0) basicFBConfig = fbConfigs[0];
                    }
                }
            }

            // 多重采样验证
            if (sampleCounts == null) return false;
            if (attribListForVisual.Count == 0) return false;

            // 查找多重采样VisualInfo，并更新gdkVisual
            uint antialiasVisualID = 0;
            foreach (var sampleCount in sampleCounts)
            {
                var attribList = new List<int>();
                attribList.AddRange(attribListForVisual);
                attribList.Add(Linux.GLX_SAMPLE_BUFFERS);
                attribList.Add(1);
                attribList.Add(Linux.GLX_SAMPLES);
                attribList.Add(sampleCount);
                attribList.Add(0);

                var attribs = attribList.ToArray();
                unsafe
                {
                    fixed (int *attribsPtr = &(attribs[0]))
                    {
                        antialiasVisualInfo = Linux.glXChooseVisual(x11Display, snumber, attribsPtr);
                    }
                    if (antialiasVisualInfo != IntPtr.Zero)
                    {
                        var antialiasVisualInfoPtr = (XVisualInfo*)antialiasVisualInfo;
                        antialiasVisualID = antialiasVisualInfoPtr->visualid;
                        break;
                    }
                }
            }

            if (antialiasVisualInfo == IntPtr.Zero) return false;

            var antialiasGdkVisual = Linux.gdk_x11_screen_lookup_visual(Gdk.Screen.Default.Handle, antialiasVisualID);
            if (antialiasGdkVisual == IntPtr.Zero) return false;

            gdkVisual = new Gdk.Visual(antialiasGdkVisual);

            // 查找多重采样FBConfig
            if (attribListForFBConfig.Count > 0)
            {
                foreach (var sampleCount in sampleCounts)
                {
                    var attribList = new List<int>();
                    attribList.AddRange(attribListForFBConfig);
                    attribList.Add(Linux.GLX_SAMPLE_BUFFERS);
                    attribList.Add(1);
                    attribList.Add(Linux.GLX_SAMPLES);
                    attribList.Add(sampleCount);
                    attribList.Add(0);

                    var attribs = attribListForFBConfig.ToArray();
                    unsafe
                    {
                        fixed (int *attribsPtr = &(attribs[0]))
                        {
                            int nElements = 0;
                            IntPtr* fbConfigs = Linux.glXChooseFBConfig(x11Display, snumber, attribsPtr, &nElements);
                            if (nElements > 0)
                            {
                                antialiasFBConfig = fbConfigs[0];
                                break;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private OpenGL gl;
        private GLCallback callback;
        private IntPtr context = IntPtr.Zero;
        private uint xid = 0;
        private bool rendererStatusOK = false;
        private GLSizeInfo? size = null;
        private bool drawQueued = false;
        private bool swapIntervalFailed = false;
        private IntPtr targetVisualInfo = IntPtr.Zero;
        private IntPtr targetFBConfig = IntPtr.Zero; // IntPtr.Zero时不启用core profile
        private bool enableAntialias = false;
        private bool useLegacyAPI;
    }
}