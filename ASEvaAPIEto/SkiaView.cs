using System;
using System.Collections.Generic;
using Eto;
using Eto.Forms;
using Eto.Drawing;
using SharpGL;
using SkiaSharp;
using ASEva.Samples;
using ASEva.Utility;

namespace ASEva.UIEto
{
	/// <summary>
	/// (api:eto=2.7.0) SkiaView渲染事件参数
	/// </summary>
    public class SkiaRenderEventArgs : EventArgs
    {
        public SkiaRenderEventArgs(SKCanvas canvas, Size logicalSize)
		{
			Canvas = canvas;
			LogicalSize = logicalSize;
		}

		/// <summary>
		/// Skia画布对象
		/// </summary>
        public SKCanvas Canvas { get; private set; }

		/// <summary>
		/// 画布的逻辑尺寸
		/// </summary>
		public Size LogicalSize { get; private set; }
    }

	/// <summary>
	/// (api:eto=2.7.0) Skia绘制视图（注意，此控件不支持在 ASEva.UIEto.OverlayLayout 中使用）
	/// </summary>
	public class SkiaView : Panel, GLCallback
	{
		/// <summary>
		/// 渲染事件
		/// </summary>
		public event EventHandler<SkiaRenderEventArgs> Render;

		/// <summary>
		/// 构造函数
		/// </summary>
		public SkiaView()
		{
			useGL = !Agency.IsGPURenderingDisabled();
			requestOnscreenRendering = false;
			initContent();
		}

		/// <summary>
		/// (api:eto=2.8.2) 构造函数
		/// </summary>
		/// <param name="disableGLRendering">是否禁用OpenGL渲染，禁用则使用CPU渲染</param>
		public SkiaView(bool disableGLRendering)
		{
			useGL = !Agency.IsGPURenderingDisabled() && !disableGLRendering;
			requestOnscreenRendering = false;
			initContent();
		}

		/// <summary>
		/// (api:eto=2.8.3) 构造函数
		/// </summary>
		/// <param name="moduleID">所属窗口组件或对话框组件ID，用于绘图时间记录与反馈</param>
		/// <param name="disableGLRendering">是否禁用OpenGL渲染，禁用则使用CPU渲染</param>
		public SkiaView(String moduleID, bool disableGLRendering)
		{
			this.moduleID = moduleID;
			useGL = !Agency.IsGPURenderingDisabled() && !disableGLRendering;
			requestOnscreenRendering = false;
			initContent();
		}

		/// <summary>
		/// (api:eto=2.8.7) 构造函数
		/// </summary>
		/// <param name="moduleID">所属窗口组件或对话框组件ID，用于绘图时间记录与反馈，若不使用可输入null</param>
		/// <param name="disableGLRendering">是否禁用OpenGL渲染，禁用则使用CPU渲染</param>
		/// <param name="requestOnscreenRendering">在未禁用OpenGL渲染时，是否请求启用在屏渲染(若不支持则仍使用离屏渲染)，默认为false</param>
		public SkiaView(String moduleID, bool disableGLRendering, bool requestOnscreenRendering)
		{
			this.moduleID = moduleID;
			useGL = !Agency.IsGPURenderingDisabled() && !disableGLRendering;
			requestOnscreenRendering = useGL && requestOnscreenRendering;
			initContent();
		}

		/// <summary>
		/// 关闭视图，释放资源
		/// </summary>
		public void Close()
		{
			if (closed) return;
			if (useGL)
			{
				if (skSurface != null)
				{
					skSurface.Dispose();
					skSurface = null;
				}
				if (grContext != null)
				{
					grContext.Dispose();
					grContext = null;
				}
				if (glBackend != null) glBackend.ReleaseGL();
			}
			else
			{
				commonImage = null;
			}
			closed = true;
		}

		/// <summary>
		/// 提交新的渲染请求
		/// </summary>
		public void QueueRender()
		{
			if (useGL)
			{
				if (!closed && glBackend != null) glBackend.QueueRender();
			}
			else
			{
				if (!drawableInvalidated && DrawBeat.CallerBegin(this))
				{
					drawable.Invalidate();
					drawableInvalidated = true;
					DrawBeat.CallerEnd(this);
				}
			}
		}

		/// <summary>
		/// 底层的OpenGL上下文信息 (null表示不使用或还未初始化完成)
		/// </summary>
		public GLContextInfo? ContextInfo { get; private set; }

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

		/// <summary>
		/// (api:eto=2.8.7) 是否支持被其他控件覆盖
		/// </summary>
		public bool SupportOverlay
		{
			get { return supportOverlay; }
		}

        public void OnGLInitialize(OpenGL gl, GLContextInfo contextInfo)
        {
			if (contextInfo.version == null) contextInfo.version = "";
			if (contextInfo.vendor == null) contextInfo.vendor = "";
			if (contextInfo.renderer == null) contextInfo.renderer = "";
			if (contextInfo.extensions == null) contextInfo.extensions = "";
			ContextInfo = contextInfo;

			funcLoader = gl.FunctionLoader;
			grContext = GRContext.CreateGl(GRGlInterface.Create((name) => { return funcLoader.GetFunctionAddress(name); }));
			
			if (grContext == null)
			{
				var options = new GRContextOptions();
				options.AvoidStencilBuffers = true;
				grContext = GRContext.CreateGl(options);
				if (grContext == null)
				{
					gl.ClearColor(0, 0, 0, 1);
				}
			}
        }

        public void OnGLResize(OpenGL gl, GLSizeInfo sizeInfo)
        {
			if (grContext == null) return;

            this.sizeInfo = sizeInfo;

			if (skSurface != null)
			{
				skSurface.Dispose();
				skSurface = null;
			}

			gl.GetInteger(OpenGL.GL_FRAMEBUFFER_BINDING_EXT, framebuffer);
			gl.GetInteger(OpenGL.GL_STENCIL_BITS, stencil);
			gl.GetInteger(OpenGL.GL_SAMPLES, samples);

			var maxSamples = grContext.GetMaxSurfaceSampleCount(colorType);
			if (samples[0] > maxSamples) samples[0] = maxSamples;

			var glInfo = new GRGlFramebufferInfo((uint)framebuffer[0], colorType.ToGlSizedFormat());
			var renderTarget = new GRBackendRenderTarget(sizeInfo.RealWidth, sizeInfo.RealHeight, samples[0], stencil[0], glInfo);
			skSurface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.BottomLeft, colorType);
			if (skSurface != null) skSurface.Canvas.Scale((float)sizeInfo.RealPixelScale);
        }

        public void OnGLRender(OpenGL gl, GLTextTasks textTasks)
        {
			if (grContext == null)
			{
				gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
				return;
			}

            if (Render != null && skSurface != null)
			{
				Render(this, new SkiaRenderEventArgs(skSurface.Canvas, new Size(sizeInfo.LogicalWidth, sizeInfo.LogicalHeight)));
				skSurface.Canvas.Flush();
				skSurface.Flush();
				grContext.Flush();
			}

			lock (renderTime)
			{
				renderTime.Add(DateTime.Now);
			}
        }

        private void drawable_Paint(object sender, PaintEventArgs e)
        {
			if (closed) return;

			DrawBeat.CallbackBegin(this, moduleID);

			var targetSize = drawable.GetLogicalSize();
			if (commonImage == null || commonImage.Width != targetSize.Width || commonImage.Height != targetSize.Height)
			{
				commonImage = CommonImage.Create(targetSize.Width, targetSize.Height, true);
			}

			unsafe
			{
				fixed (byte *commonImageData = &(commonImage.Data[0]))
				{
					var imageInfo = new SKImageInfo(commonImage.Width, commonImage.Height, SKColorType.Bgra8888, SKAlphaType.Opaque);
					var surface = SKSurface.Create(imageInfo, (IntPtr)commonImageData, commonImage.RowBytes);

					Render(this, new SkiaRenderEventArgs(surface.Canvas, targetSize));
					surface.Canvas.Flush();
					surface.Flush();

					surface.Dispose();
				}
			}

			e.Graphics.DrawImage(commonImage.ToEtoBitmap(), 0, 0);

			lock (renderTime)
			{
				renderTime.Add(DateTime.Now);
			}
			drawableInvalidated = false;

			DrawBeat.CallbackEnd(this);
        }

		public void OnRaiseMouseDown(MouseEventArgs args)
		{
			OnMouseDown(args);
		}

		public void OnRaiseMouseMove(MouseEventArgs args)
		{
			OnMouseMove(args);
		}

		public void OnRaiseMouseUp(MouseEventArgs args)
		{
			OnMouseUp(args);
		}

		public void OnRaiseMouseWheel(MouseEventArgs args)
		{
			OnMouseWheel(args);
		}

        public string OnGetModuleID()
        {
            return moduleID;
        }

		private void initContent()
		{
			if (useGL)
			{
				if (Factory != null)
				{
					var options = new GLOptions
					{
						EnableOnscreenRendering = requestOnscreenRendering || Agency.IsOnscreenGPURenderingEnabled(),
						UseTextTasks = false,
						UseLegacyAPI = false,
					};
					Factory.CreateGLBackend(this, options, out etoControl, out glBackend, out supportOverlay);
					if (etoControl != null) Content = etoControl;
				}
			}
			else
			{
				drawable = new Drawable();
				drawable.Paint += drawable_Paint;
				Content = drawable;
			}
		}

        public static GLBackendFactory Factory { private get; set; }

		private Control etoControl;
		private GLBackend glBackend;
		private Drawable drawable;
		private bool drawableInvalidated = false;
		private List<DateTime> renderTime = new List<DateTime>();

		private FuncLoader funcLoader;
		private GRContext grContext;
		private SKSurface skSurface;

		private GLSizeInfo sizeInfo;

		private SKColorType colorType = SKColorType.Rgba8888;
		private int[] framebuffer = new int[1];
		private int[] stencil = new int[1];
		private int[] samples = new int[1];

		private CommonImage commonImage;

		private bool useGL;
		private bool closed = false;
		private String moduleID;
		private bool requestOnscreenRendering;
		private bool supportOverlay = false;
	}
}