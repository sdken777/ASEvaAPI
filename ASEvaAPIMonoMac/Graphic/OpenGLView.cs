using System;
using System.Collections.Generic;
using MonoMac.AppKit;
using MonoMac.CoreGraphics;
using MonoMac.Foundation;
using MonoMac.CoreText;
using SharpGL;
using ASEva.UIEto;

namespace ASEva.UIMonoMac
{
    class OpenGLView : NSOpenGLView, GLView.GLViewBackend
    {
        public OpenGLView(CGRect frameRect) : base(frameRect)
        {
            PixelFormat = new NSOpenGLPixelFormat(new NSOpenGLPixelFormatAttribute[]
            {
                NSOpenGLPixelFormatAttribute.Window,
                NSOpenGLPixelFormatAttribute.Accelerated,
                NSOpenGLPixelFormatAttribute.DoubleBuffer,
                NSOpenGLPixelFormatAttribute.MinimumPolicy,
                NSOpenGLPixelFormatAttribute.ColorSize, (NSOpenGLPixelFormatAttribute)24,
                NSOpenGLPixelFormatAttribute.AlphaSize, (NSOpenGLPixelFormatAttribute)8,
                NSOpenGLPixelFormatAttribute.DepthSize, (NSOpenGLPixelFormatAttribute)16,
                NSOpenGLPixelFormatAttribute.OpenGLProfile, (NSOpenGLPixelFormatAttribute)NSOpenGLProfile.VersionLegacy,
                (NSOpenGLPixelFormatAttribute)0
            });
        }

        public void SetCallback(GLView.GLViewCallback callback)
        {
            this.callback = callback;
        }

        public void InitializeGL()
        {
            // 在首次绘制时初始化
        }

        public void ReleaseGL()
        {
            if (initStatus != InitStatus.InitOK) return;

            ClearGLContext();
            initStatus = InitStatus.NotInitialized;
        }

        public void QueueRender()
        {
            if (Window != null && !Window.IsMiniaturized && !Hidden && !drawQueued)
            {
                NeedsDisplay = true;
                drawQueued = true;
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
                    callback.OnGLInitialize(gl);
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
                return;
            }

            OpenGLContext.FlushBuffer();

            var textTasks = texts.Clear();
            for (int i = 0; i < textTasks.Length; i++)
            {
                if (i >= textViews.Count)
                {
                    var newView = new NSTextView { Editable = false, Selectable = false, DrawsBackground = false };
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
        }

        public override void Reshape()
        {
            QueueRender();
        }

        private class TextViewContext
        {
            public NSTextView TextView { get; set; }
            public GLTextTask Task { get; set; }
        }

        enum InitStatus
        {
            NotInitialized = 0,
            InitOK = 1,
            InitFailed = 2,
        }

        private OpenGL gl = null;
        private GLView.GLViewCallback callback = null;
        private InitStatus initStatus = InitStatus.NotInitialized;
        private GLSizeInfo size = null;
        private List<TextViewContext> textViews = new List<TextViewContext>();
        private bool drawQueued = false;
    }
}