using System;
using System.Collections.Generic;
using MonoMac.AppKit;
using MonoMac.CoreGraphics;
using MonoMac.Foundation;
using MonoMac.CoreText;
using SharpGL;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEva.UIMonoMac
{
    class OpenGLView : NSOpenGLView, GLBackend
    {
        public OpenGLView(GLCallback callback, GLAntialias antialias, bool useLegacyAPI) : base(new CGRect(0, 0, 100, 100))
        {
            this.callback = callback;

            if (antialias == GLAntialias.Sample16x) antialias = GLAntialias.Sample8x; // 可能不支持16x

            var attribs = new List<NSOpenGLPixelFormatAttribute>();
            attribs.Add(NSOpenGLPixelFormatAttribute.Window);
            attribs.Add(NSOpenGLPixelFormatAttribute.Accelerated);
            attribs.Add(NSOpenGLPixelFormatAttribute.DoubleBuffer);
            attribs.Add(NSOpenGLPixelFormatAttribute.MinimumPolicy);
            attribs.Add(NSOpenGLPixelFormatAttribute.ColorSize); attribs.Add((NSOpenGLPixelFormatAttribute)24);
            attribs.Add(NSOpenGLPixelFormatAttribute.AlphaSize); attribs.Add((NSOpenGLPixelFormatAttribute)8);
            attribs.Add(NSOpenGLPixelFormatAttribute.DepthSize); attribs.Add((NSOpenGLPixelFormatAttribute)16);
            attribs.Add(NSOpenGLPixelFormatAttribute.OpenGLProfile); attribs.Add(useLegacyAPI ? (NSOpenGLPixelFormatAttribute)NSOpenGLProfile.VersionLegacy : (NSOpenGLPixelFormatAttribute)NSOpenGLProfile.Version3_2Core);
            if (antialias != GLAntialias.Disabled)
            {
                attribs.Add(NSOpenGLPixelFormatAttribute.SampleBuffers); attribs.Add((NSOpenGLPixelFormatAttribute)1);
                attribs.Add(NSOpenGLPixelFormatAttribute.Samples); attribs.Add((NSOpenGLPixelFormatAttribute)getSampleCount(antialias));
            }
            attribs.Add((NSOpenGLPixelFormatAttribute)0);

            PixelFormat = new NSOpenGLPixelFormat(attribs.ToArray());
        }

        public void ReleaseGL()
        {
            if (initStatus != InitStatus.InitOK) return;

            ClearGLContext();
            initStatus = InitStatus.NotInitialized;
        }

        public void QueueRender()
        {
            if (Window != null && !Window.IsMiniaturized && !Hidden && !drawQueued && DrawBeat.CallerBegin(this))
            {
                NeedsDisplay = true;
                drawQueued = true;
                DrawBeat.CallerEnd(this);
            }
        }

        public override void DrawRect(CGRect dirty)
        {
            if (initStatus == InitStatus.InitFailed) return;
            else if (initStatus == InitStatus.NotInitialized)
            {
                gl = OpenGL.Create(new MacOSFuncLoader());

                if (OpenGLContext == null || OpenGLContext.ClassHandle == IntPtr.Zero)
                {
                    initStatus = InitStatus.InitFailed;
                    return;
                }

                OpenGLContext.MakeCurrentContext();
                
                if (MacOS.glewInit() != 0)
                {
                    initStatus = InitStatus.InitFailed;
                    return;
                }

                try
                {
                    var ctxInfo = new GLContextInfo();
                    ctxInfo.version = gl.Version;
                    ctxInfo.vendor = gl.Vendor;
                    ctxInfo.renderer = gl.Renderer;
                    ctxInfo.extensions = gl.Extensions;

                    callback.OnGLInitialize(gl, ctxInfo);
                    gl.Flush();
                }
                catch (Exception)
                {
                    ClearGLContext();
                    initStatus = InitStatus.InitFailed;
                    return;
                }

                initStatus = InitStatus.InitOK;
            }

            var moduleID = callback == null ? null : callback.OnGetModuleID();
            DrawBeat.CallbackBegin(this, moduleID);

            OpenGLContext.MakeCurrentContext();

            var texts = new GLTextTasks();
            try
            {
                var curWidth = (int)Frame.Width;
                var curHeight = (int)Frame.Height;
                var curSize = new GLSizeInfo(curWidth, curHeight, curWidth, curHeight, 1, (float)curWidth / curHeight);
                if (size == null || curSize.RealWidth != size.RealWidth || curSize.RealHeight != size.RealHeight)
                {
                    size = curSize;
                    callback.OnGLResize(gl, size);
                }

                callback.OnGLRender(gl, texts);
                gl.Finish();
            }
            catch (Exception)
            {
                ClearGLContext();
                initStatus = InitStatus.InitFailed;
                DrawBeat.CallbackEnd(this);
                return;
            }

            OpenGLContext.FlushBuffer();

            var textTasks = texts.Clear();
            for (int i = 0; i < textTasks.Length; i++)
            {
                if (i >= textViews.Count)
                {
                    var newView = new TextView(callback, this) { Editable = false, Selectable = false, DrawsBackground = false };
                    this.AddSubview(newView);
                    textViews.Add(new TextViewContext
                    {
                        TextView = newView,
                        Task = textTasks[i],
                    });
                }
                else
                {
                    textViews[i].Task = textTasks[i];
                }

                var target = textViews[i];
                var textView = target.TextView;
                var task = target.Task;

                var targetText = String.IsNullOrEmpty(task.text) ? "" : task.text;
                var targetFontName = String.IsNullOrEmpty(task.fontName) ? ".AppleSystemUIFont" : task.fontName;
                var targetFontSize = (task.sizeScale <= 0 ? 1.0f : task.sizeScale) * 11;
                var colorCoef = 1.0 / 255;

                textView.TextStorage.SetString(new NSAttributedString(targetText, new CTStringAttributes{ Font = new CTFont(targetFontName, targetFontSize) }));
                textView.TextColor = NSColor.FromRgba(colorCoef * task.red, colorCoef * task.green, colorCoef * task.blue, task.alpha == 0 ? 1.0 : (colorCoef * task.alpha));
                textView.TextContainer.ContainerSize = new CGSize(10000, 10000);
                textView.LayoutManager.EnsureLayoutForTextContainer(textView.TextContainer);
                var textSize = textView.LayoutManager.GetUsedRectForTextContainer(textView.TextContainer);

                var posX = (double)task.posX;
                var posY = (double)task.posY;
                if (task.isRealPos)
                {
                    posX /= size.RealPixelScale;
                    posY /= size.RealPixelScale;
                }

                var fullWidth = textSize.Width;
                var fullHeight = textSize.Height;
                var halfWidth = textSize.Width / 2;
                var halfHeight = textSize.Height / 2;
                switch (task.anchor)
                {
                case TextAnchor.TopLeft:
                    posY = size.LogicalHeight - posY - fullHeight;
                    break;
                case TextAnchor.LeftCenter:
                    posY = size.LogicalHeight - posY - halfHeight;
                    break;
                case TextAnchor.BottomLeft:
                    posY = size.LogicalHeight - posY;
                    break;
                case TextAnchor.TopCenter:
                    posX -= halfWidth;
                    posY = size.LogicalHeight - posY - fullHeight;
                    break;
                case TextAnchor.Center:
                    posX -= halfWidth;
                    posY = size.LogicalHeight - posY - halfHeight;
                    break;
                case TextAnchor.BottomCenter:
                    posX -= halfWidth;
                    posY = size.LogicalHeight - posY;
                    break;
                case TextAnchor.TopRight:
                    posX -= fullWidth;
                    posY = size.LogicalHeight - posY - fullHeight;
                    break;
                case TextAnchor.RightCenter:
                    posX -= fullWidth;
                    posY = size.LogicalHeight - posY - halfHeight;
                    break;
                case TextAnchor.BottomRight:
                    posX -= fullWidth;
                    posY = size.LogicalHeight - posY;
                    break;
                }

                textView.Frame = new CGRect(posX, posY, textSize.Width, textSize.Height);

                if (textView.Hidden) textView.Hidden = false;
            }
            for (int i = textTasks.Length; i < textViews.Count; i++)
            {
                var textView = textViews[i].TextView;
                if (!textView.Hidden) textView.Hidden = true;
            }

            drawQueued = false;
            DrawBeat.CallbackEnd(this);
        }

        public override void Reshape()
        {
            QueueRender();
        }

        private class TextView : NSTextView
        {
            public TextView(GLCallback callback, NSView parent)
            {
                this.callback = callback;
                this.parent = parent;
            }

            public override void MouseDown(NSEvent ev)
            {
                base.MouseDown(ev);
                callback.OnRaiseMouseDown(toEto(ev, false));
            }

            public override void MouseDragged(NSEvent ev)
            {
                base.MouseDragged(ev);
                callback.OnRaiseMouseMove(toEto(ev, false));
            }

            public override void MouseUp(NSEvent ev)
            {
                base.MouseUp(ev);
                callback.OnRaiseMouseUp(toEto(ev, false));
            }

            public override void RightMouseDown(NSEvent ev)
            {
                base.RightMouseDown(ev);
                callback.OnRaiseMouseDown(toEto(ev, true));
            }

            public override void RightMouseDragged(NSEvent ev)
            {
                base.RightMouseDragged(ev);
                callback.OnRaiseMouseMove(toEto(ev, true));
            }

            public override void RightMouseUp(NSEvent ev)
            {
                base.RightMouseUp(ev);
                callback.OnRaiseMouseUp(toEto(ev, true));
            }

            public override void ScrollWheel(NSEvent ev)
            {
                base.ScrollWheel(ev);
                callback.OnRaiseMouseWheel(toEto(ev, null));
            }

            private Eto.Forms.MouseEventArgs toEto(NSEvent ev, bool? rightMouse)
            {
                var pt = ConvertPointFromView(ev.LocationInWindow, null);
                var location = new Eto.Drawing.PointF((int)(Frame.X + pt.X), (int)(parent.Frame.Height - Frame.Y - Frame.Height + pt.Y));
                var buttons = rightMouse == null ? Eto.Forms.MouseButtons.None : (rightMouse.Value ? Eto.Forms.MouseButtons.Alternate : Eto.Forms.MouseButtons.Primary);
                var delta = new Eto.Drawing.SizeF(0, rightMouse == null ? (float)ev.ScrollingDeltaY : 0);
                return new Eto.Forms.MouseEventArgs(buttons, Eto.Forms.Keys.None, location, delta);
            }

            private GLCallback callback;
            private NSView parent;
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

        private class TextViewContext
        {
            public TextView TextView { get; set; }
            public GLTextTask Task { get; set; }
        }

        enum InitStatus
        {
            NotInitialized = 0,
            InitOK = 1,
            InitFailed = 2,
        }

        private OpenGL gl = null;
        private GLCallback callback = null;
        private InitStatus initStatus = InitStatus.NotInitialized;
        private GLSizeInfo size = null;
        private List<TextViewContext> textViews = new List<TextViewContext>();
        private bool drawQueued = false;
    }
}