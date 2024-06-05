using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace ASEva.UIAvalonia
{
    #pragma warning disable CS1571

	/// \~English
	/// <summary>
	/// (api:avalonia=1.0.9) Skia view render event argument
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:avalonia=1.0.9) SkiaView渲染事件参数
	/// </summary>
    public class SkiaRenderEventArgs : EventArgs
    {
        public SkiaRenderEventArgs(SKCanvas canvas)
		{
			Canvas = canvas;
		}

		/// \~English
		/// <summary>
		/// Skia canvas object
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// Skia画布对象
		/// </summary>
        public SKCanvas Canvas { get; private set; }
    }

    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.9) Skia view
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.9) Skia绘制视图
    /// </summary>
    public partial class SkiaView : UserControl
    {
		/// \~English
		/// <summary>
		/// Render event
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 渲染事件
		/// </summary>
        public event EventHandler<SkiaRenderEventArgs> RenderSkia;

		/// \~English
		/// <summary>
		/// Constructor
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 构造函数
		/// </summary>
        public SkiaView()
        {
            InitializeComponent();
            renderingLogic.OnRender += (canvas) => RenderSkia?.Invoke(this, new SkiaRenderEventArgs(canvas));
        }

        public override void Render(DrawingContext context)
        {
            if (Bounds.Width > 0 && Bounds.Height > 0)
            {
                renderingLogic.Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height);
                context.Custom(renderingLogic);
            }
        }

        private class RenderingLogic : ICustomDrawOperation
        {
            public Action<SKCanvas> OnRender;
            public Rect Bounds { get; set; }
            public void Dispose() { }
            public bool Equals(ICustomDrawOperation other) => other == this;
            public bool HitTest(Point p) { return false; }

            public void Render(ImmediateDrawingContext context)
            {
                var skia = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
                if (skia == null) return;
                
                using (var lease = skia.Lease())
                {
                    SKCanvas canvas = lease.SkCanvas;
                    if (canvas != null) OnRender?.Invoke(canvas);
                }
            }
        }

        private RenderingLogic renderingLogic = new RenderingLogic();
    }
}