using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using SkiaSharp;

namespace ASEva.UIAvalonia
{
	/// \~English
	/// <summary>
	/// (api:avalonia=1.0.10) OpenGL view
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:avalonia=1.0.10) OpenGL绘制视图
	/// </summary>
    public partial class GLView : Panel
    {
		/// \~English
		/// <summary>
		/// Initialization event
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 初始化事件
		/// </summary>
		public event EventHandler<GLEventArgs> GLInitialize;

		/// \~English
		/// <summary>
		/// Resize event
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 缩放事件
		/// </summary>
		public event EventHandler<GLResizeEventArgs> GLResize;

		/// \~English
		/// <summary>
		/// Render event
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 渲染事件
		/// </summary>
		public event EventHandler<GLRenderEventArgs> GLRender;

		/// \~English
		/// <summary>
		/// Constructor
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 构造函数
		/// </summary>
        public GLView()
        {
            InitializeComponent();
            openglControl.OnRender += openglControl_OnRender;
        }

        // TODO: antialias?

        /// \~English
        /// <summary>
        /// Request new rendering
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 提交新的渲染请求
        /// </summary>
        public void QueueRender()
        {
            openglControl.RequestNextFrameRendering();

            if (textTasksUpdated)
            {
                textTasksUpdated = false;
                skiaView.QueueRender();
            }
        }

		/// \~English
		/// <summary>
		/// Frame rate (for the latest 3 seconds)
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 渲染帧率（统计最近3秒）
		/// </summary>
		public float FPS
		{
			get
			{
				lock (renderTime)
				{
					var now = DateTime.Now;
					renderTime.RemoveAll((t) => (now - t).TotalSeconds > 3);
					return (float)renderTime.Count / 3;
				}
			}
		}

        private void openglControl_OnRender(object sender, GLEventArgs e)
        {
            if (!initialized)
            {
                initialized = true;
                GLInitialize?.Invoke(this, e);
            }

            if (openglControl.Width <= 0 || openglControl.Height <= 0) return;

            // TODO
            if (openglControl.Width != lastRealSize.Width || openglControl.Height != lastRealSize.Height)
            {
                lastRealSize.Width = (int)openglControl.Width;
                lastRealSize.Height = (int)openglControl.Height;
                GLResize?.Invoke(this, new GLResizeEventArgs(e.GL, e.FB, new GLSizeInfo(lastRealSize.Width, lastRealSize.Height, (float)lastRealSize.Width / lastRealSize.Height)));
            }

            var newTextTasks = new GLTextTasks();
            GLRender?.Invoke(this, new GLRenderEventArgs(e.GL, e.FB, newTextTasks));
            textTasks = newTextTasks.Clear();
            textTasksUpdated = true;

            lock (renderTime) { renderTime.Add(DateTime.Now); }
        }

        private void skiaView_Render(object sender, SkiaRenderEventArgs e)
        {
            var tasks = textTasks;
            foreach (var task in tasks)
            {
                var font = task.fontName == null ? e.Canvas.GetDefaultFont(task.sizeScale) : e.Canvas.GetFont(task.fontName, task.sizeScale);
                var color = new SKColor(task.red, task.green, task.blue, task.alpha == 0 ? (byte)255 : task.alpha);
                var scale = task.isRealPos ? (1.0f / realPixelScale) : 1.0f;
                e.Canvas.DrawString(task.text, font, color, task.anchorX, task.anchorY, (int)(task.posX * scale), (int)(task.posY * scale));
            }
        }

        private List<DateTime> renderTime = new List<DateTime>();
        private bool initialized = false;
        private IntSize lastRealSize = new IntSize(0, 0);
        private GLTextTask[] textTasks = null;
        private bool textTasksUpdated = false;
        private float realPixelScale = 1.0f; // TODO
    }

    class OpenGLControl : OpenGlControlBase
    {
        public event EventHandler<GLEventArgs> OnRender;

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            OnRender?.Invoke(this, new GLEventArgs(gl, fb));
        }
    }
}