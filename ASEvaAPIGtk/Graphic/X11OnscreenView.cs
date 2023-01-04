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
        public X11OnscreenView(GLCallback callback, GLAntialias antialias)
        {
            this.callback = callback;
            this.gl = OpenGL.Create(new LinuxFuncLoader());

            DoubleBuffered = false;

            Gdk.Visual targetVisual;
            this.enableAntialias = chooseAntialiasVisual(antialias, out targetVisual, out targetVisualInfo);
            Visual = targetVisual;

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
            if (context != IntPtr.Zero)
            {
                Linux.glXDestroyContext(display, context);
                context = IntPtr.Zero;
            }

            rendererStatusOK = false;
        }

        private void onRealized(object sender, EventArgs e)
        {
            if (targetVisualInfo == IntPtr.Zero) return;

            IntPtr display = Linux.gdk_x11_display_get_xdisplay(Window.Display.Handle);
            if (display == IntPtr.Zero) return;

            context = Linux.glXCreateContext(display, targetVisualInfo, IntPtr.Zero, true);
            if (context == IntPtr.Zero) return;

            xid = Linux.gdk_x11_window_get_xid(Window.Handle);
            Linux.glXMakeCurrent(display, xid, context);

            try
            {
                var ctxInfo = new GLContextInfo();
                ctxInfo.version = gl.Version;
                ctxInfo.vendor = gl.Vendor;
                ctxInfo.renderer = gl.Renderer;
                ctxInfo.extensions = gl.Extensions;
                
                size = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);

                callback.OnGLInitialize(gl, ctxInfo);
                if (enableAntialias) gl.Enable(OpenGL.GL_MULTISAMPLE);

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

            var curSize = new GLSizeInfo(AllocatedWidth, AllocatedHeight, AllocatedWidth * ScaleFactor, AllocatedHeight * ScaleFactor, ScaleFactor, (float)AllocatedWidth / AllocatedHeight);
            bool resized = curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight;
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
            catch (Exception)
            {
                onDestroy();
            }

            if (!swapIntervalFailed)
            {
                try
                {
                    Linux.glXSwapIntervalEXT(display, xid, 0);
                }
                catch (Exception)
                {
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

        private bool chooseAntialiasVisual(GLAntialias antialias, out Gdk.Visual gdkVisual, out IntPtr visualInfo)
        {
            gdkVisual = Gdk.Visual.Best;
            visualInfo = IntPtr.Zero;

            IntPtr x11Display = Linux.gdk_x11_display_get_xdisplay(Display.Handle);
            if (x11Display == IntPtr.Zero) return false;

            IntPtr x11OriginVisual = Linux.gdk_x11_visual_get_xvisual(Gdk.Visual.Best.Handle);
            if (x11OriginVisual == IntPtr.Zero) return false;

            var x11OriginVisualInfo = getVisualInfo(Linux.XVisualIDFromVisual(x11OriginVisual), x11Display);
            if (x11OriginVisualInfo == IntPtr.Zero) return false;

            visualInfo = x11OriginVisualInfo;

            if (antialias == GLAntialias.Disabled) return false;

            var bufferSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_BUFFER_SIZE);
            var level = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_LEVEL);
            var rgba = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_RGBA);
            var doubleBuffer = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_DOUBLEBUFFER);
            var stereo = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_STEREO);
            var auxBuffers = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_AUX_BUFFERS);
            var redSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_RED_SIZE);
            var greenSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_GREEN_SIZE);
            var blueSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_BLUE_SIZE);
            var alphaSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_ALPHA_SIZE);
            var depthSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_DEPTH_SIZE);
            var stencilSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_STENCIL_SIZE);
            var accumRedSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_ACCUM_RED_SIZE);
            var accumGreenSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_ACCUM_GREEN_SIZE);
            var accumBlueSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_ACCUM_BLUE_SIZE);
            var accumAlphaSize = getVisualAttrib(x11Display, x11OriginVisualInfo, Linux.GLX_ACCUM_ALPHA_SIZE);

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
            if (rgba != null && rgba.Value == 1)
            {
                attribList.Add(Linux.GLX_RGBA);
            }
            if (doubleBuffer != null && doubleBuffer.Value == 1)
            {
                attribList.Add(Linux.GLX_DOUBLEBUFFER);
            }
            if (stereo != null && stereo.Value == 1)
            {
                attribList.Add(Linux.GLX_STEREO);
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

            attribList.Add(Linux.GLX_SAMPLE_BUFFERS);
            attribList.Add(1);
            attribList.Add(Linux.GLX_SAMPLES);
            attribList.Add(0); // 稍后写入
            attribList.Add(0);

            int snumber = Linux.gdk_x11_screen_get_screen_number(Display.DefaultScreen.Handle);

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
                default:
                    return false;
            }

            IntPtr antialiasVisualInfo = IntPtr.Zero;
            uint antialiasVisualID = 0;
            foreach (var sampleCount in sampleCounts)
            {
                attribList[attribList.Count - 2] = sampleCount;
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
            visualInfo = antialiasVisualInfo;
            return true;
        }

        private OpenGL gl = null;
        private GLCallback callback;
        private IntPtr context = IntPtr.Zero;
        private uint xid = 0;
        private bool rendererStatusOK = false;
        private GLSizeInfo size = null;
        private bool drawQueued = false;
        private bool swapIntervalFailed = false;
        private IntPtr targetVisualInfo = IntPtr.Zero;
        private bool enableAntialias = false;
    }
}