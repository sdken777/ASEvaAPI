using System;
using System.Collections.Generic;
using Eto;
using Eto.Forms;
using SharpGL;

namespace ASEva.UIEto
{
	#pragma warning disable CS1571
	
	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) Anchor position of a text
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) 文本锚点坐标位置类型
	/// </summary>
    public enum TextAnchor
    {
		/// \~English
		/// <summary>
		/// Text center point
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本中心
		/// </summary>
        Center = 0,

		/// \~English
		/// <summary>
		/// Top left point of text
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本左上角
		/// </summary>
        TopLeft = 1,

		/// \~English
		/// <summary>
		/// Left center point of text
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本左侧中心
		/// </summary>
        LeftCenter = 2,

		/// \~English
		/// <summary>
		/// Bottom left point of text
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本左下角
		/// </summary>
        BottomLeft = 3,

		/// \~English
		/// <summary>
		/// Top right point of text
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本右上角
		/// </summary>
        TopRight = 4,

		/// \~English
		/// <summary>
		/// Right center point of text
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本右侧中心
		/// </summary>
        RightCenter = 5,
		
		/// \~English
		/// <summary>
		/// Bottom right point of text
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本右下角
		/// </summary>
        BottomRight = 6,

		/// \~English
		/// <summary>
		/// Top center point of text
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本上方中心
		/// </summary>
        TopCenter = 7,

		/// \~English
		/// <summary>
		/// Bottom center point of text
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本下方中心
		/// </summary>
        BottomCenter = 8,
    }

	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) OpenGL text drawing task
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) OpenGL文本绘制任务
	/// </summary>
    public struct GLTextTask
    {
		/// \~English
		/// <summary>
		/// Text
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本
		/// </summary>
        public String text;

		/// \~English
		/// <summary>
		/// X-coordinate of anchor point
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本锚点X轴坐标
		/// </summary>
        public int posX;

		/// \~English
		/// <summary>
		/// Y-coordinate of anchor point
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本锚点Y轴坐标
		/// </summary>
        public int posY;

		/// \~English
		/// <summary>
		/// Whether the coordination of anchor point is in physical pixels, or else logical units
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本锚点坐标是否为物理像素坐标系，否则为逻辑坐标
		/// </summary>
        public bool isRealPos;

		/// \~English
		/// <summary>
		/// Anchor point's type
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本锚点坐标位置类型
		/// </summary>
        public TextAnchor anchor;

		/// \~English
		/// <summary>
		/// Red component of text color
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本绘制颜色红色分量
		/// </summary>
        public byte red;

		/// \~English
		/// <summary>
		/// Green component of text color
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本绘制颜色绿色分量
		/// </summary>
        public byte green;

		/// \~English
		/// <summary>
		/// Blue component of text color
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本绘制颜色蓝色分量
		/// </summary>
        public byte blue;

		/// \~English
		/// <summary>
		/// Alpha component of text color, 0 as 255
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本绘制颜色Alpha通道分量，0将被视为255
		/// </summary>
        public byte alpha;

		/// \~English
		/// <summary>
		/// Font name, null as default font
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本字体，空则使用默认字体
		/// </summary>
        public String? fontName;

		/// \~English
		/// <summary>
		/// Ratio to the default size, 0 as 1
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本相对于默认尺寸的比例，0将被视为1
		/// </summary>
        public float sizeScale;
    }

	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) OpenGL text drawing task list
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) OpenGL文本绘制任务对象
	/// </summary>
    public class GLTextTasks
    {
		/// \~English
		/// <summary>
		/// Add task
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 添加文本绘制任务
		/// </summary>
        public void Add(String text, int posX, int posY, byte red, byte green, byte blue, float sizeScale)
        {
            tasks.Add(new GLTextTask
            {
                text = text,
                posX = posX,
                posY = posY,
                isRealPos = false,
                anchor = TextAnchor.Center,
                red = red,
                green = green,
                blue = blue,
                alpha = 255,
                fontName = null,
                sizeScale = sizeScale,
            });
        }

		/// \~English
		/// <summary>
		/// Add task
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 添加文本绘制任务
		/// </summary>
        public void Add(GLTextTask task)
        {
            tasks.Add(task);
        }

        public GLTextTask[] Clear()
        {
            var output = tasks.ToArray();
            tasks.Clear();
            return output;
        }

        private List<GLTextTask> tasks = [];
    }

	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) OpenGL rendering size info
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) OpenGL绘制尺寸信息
	/// </summary>
    public class GLSizeInfo
    {
		/// \~English
		/// <summary>
		/// Logical width
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 逻辑宽度
		/// </summary>
        public int LogicalWidth { get; private set; }

		/// \~English
		/// <summary>
		/// Logical height
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 逻辑高度
		/// </summary>
        public int LogicalHeight { get; private set; }

		/// \~English
		/// <summary>
		/// Width in physical pixels
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 物理像素宽度
		/// </summary>
        public int RealWidth { get; private set; }

		/// \~English
		/// <summary>
		/// Height in physical pixels
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 物理像素高度
		/// </summary>
        public int RealHeight { get; private set; }

		/// \~English
		/// <summary>
		/// Ratio of physical pixel to logical unit
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 物理像素与逻辑像素的换算比
		/// </summary>
		public double RealPixelScale { get; private set; }

		/// \~English
		/// <summary>
		/// Ratio of width to height
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 宽度与高度之比
		/// </summary>
        public float AspectRatio { get; private set; }

		/// \~English
		/// <summary>
		/// Whether the coordinate system is inverted
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 图像坐标系垂直方向是否反转
		/// </summary>
		public bool VerticalInverted { get; private set; }

		/// \~English
		/// <summary>
		/// Constructor
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 构造函数
		/// </summary>
        public GLSizeInfo(int logicalWidth, int logicalHeight, int realWidth, int realHeight, double realPixelScale, float aspectRatio)
        {
            LogicalWidth = logicalWidth;
            LogicalHeight = logicalHeight;
            RealWidth = realWidth;
            RealHeight = realHeight;
			RealPixelScale = realPixelScale;
            AspectRatio = aspectRatio;
			VerticalInverted = false;
        }

		/// \~English
		/// <summary>
		/// Constructor
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 构造函数
		/// </summary>
		public GLSizeInfo(int logicalWidth, int logicalHeight, int realWidth, int realHeight, double realPixelScale, float aspectRatio, bool verticalInverted)
        {
            LogicalWidth = logicalWidth;
            LogicalHeight = logicalHeight;
            RealWidth = realWidth;
            RealHeight = realHeight;
			RealPixelScale = realPixelScale;
            AspectRatio = aspectRatio;
			VerticalInverted = verticalInverted;
        }
    }

	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) GLView event argument base class
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) GLView事件参数基类
	/// </summary>
	public class GLEventArgs : EventArgs
	{
		public GLEventArgs(OpenGL gl)
		{
			GL = gl;
		}

		/// \~English
		/// <summary>
		/// OpenGL object (From SharpGL)
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// OpenGL对象（来自SharpGL）
		/// </summary>
		public OpenGL GL { get; private set; }
	}

	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) GLView resize event argument
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) GLView缩放事件参数
	/// </summary>
	public class GLResizeEventArgs : GLEventArgs
	{
		public GLResizeEventArgs(OpenGL gl, GLSizeInfo sizeInfo) : base(gl)
		{
			SizeInfo = sizeInfo;
		}

		/// \~English
		/// <summary>
		/// Resized size info
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 缩放后的尺寸信息
		/// </summary>
		public GLSizeInfo SizeInfo { get; private set; }
	}

	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) GLView render event argument
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) GLView渲染事件参数
	/// </summary>
	public class GLRenderEventArgs : GLEventArgs
	{
		public GLRenderEventArgs(OpenGL gl, GLTextTasks textTasks) : base(gl)
		{
			TextTasks = textTasks;
		}

		/// \~English
		/// <summary>
		/// Text drawing task list
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 文本绘制任务对象
		/// </summary>
		public GLTextTasks TextTasks { get; private set; }
	}

	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) OpenGL context info
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) OpenGL上下文信息
	/// </summary>
	public struct GLContextInfo
	{
		/// \~English
		/// <summary>
		/// OpenGL version
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// OpenGL版本
		/// </summary>
        public String version;

		/// \~English
		/// <summary>
		/// Vendor
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 厂商
		/// </summary>
        public String vendor;

		/// \~English
		/// <summary>
		/// Renderer
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 渲染器
		/// </summary>
        public String renderer;

		/// \~English
		/// <summary>
		/// Extension list
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 扩展
		/// </summary>
        public String extensions;

		/// \~English
		/// <summary>
		/// Convert to a list of extension strings
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 转换为扩展字符串的列表
		/// </summary>
		public List<String> ToExtensionList()
		{
			var list = new List<String>();
			if (!String.IsNullOrEmpty(extensions))
			{
				foreach (var row in extensions.Split('\n', StringSplitOptions.RemoveEmptyEntries))
				{
					list.AddRange(row.Split(' ', StringSplitOptions.RemoveEmptyEntries));
				}
			}
			list.Sort();
			return list;
		}
	}

	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) Antialias option
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) 抗锯齿选项
	/// </summary>
	public enum GLAntialias
	{
		/// \~English
		/// <summary>
		/// Disabled
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 禁用
		/// </summary>
		Disabled = 0,

		/// \~English
		/// <summary>
		/// 2x sampling
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 2x抗锯齿
		/// </summary>
		Sample2x = 1,

		/// \~English
		/// <summary>
		/// 4x sampling
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 4x抗锯齿
		/// </summary>
		Sample4x = 2,

		/// \~English
		/// <summary>
		/// 8x sampling
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 8x抗锯齿
		/// </summary>
		Sample8x = 3,

		/// \~English
		/// <summary>
		/// 16x sampling
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 16x抗锯齿
		/// </summary>
		Sample16x = 4,
	}

	/// \~English
	/// <summary>
	/// (api:eto=3.0.0) OpenGL view
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:eto=3.0.0) OpenGL绘制视图
	/// </summary>
	public class GLView : Panel, GLCallback
	{
		/// \~English
		/// <summary>
		/// Initialization event
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 初始化事件
		/// </summary>
		public event EventHandler<GLEventArgs>? GLInitialize;

		/// \~English
		/// <summary>
		/// Resize event
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 缩放事件
		/// </summary>
		public event EventHandler<GLResizeEventArgs>? GLResize;

		/// \~English
		/// <summary>
		/// Render event
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 渲染事件
		/// </summary>
		public event EventHandler<GLRenderEventArgs>? GLRender;

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
			this.moduleID = null;
			this.requestAntialias = GLAntialias.Sample4x;
			this.requestOnscreenRendering = false;
			this.requestOverlay = true;
			this.drawText = true;
			this.useLegacyAPI = true;
			initContent();
		}

		/// \~English
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="moduleID">Component ID of owner window or dialog class, for statistics of rendering time</param>
		/// \~Chinese
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="moduleID">所属窗口组件或对话框组件ID，用于绘图时间记录与反馈</param>
		public GLView(String moduleID)
		{
			this.moduleID = moduleID;
			this.requestAntialias = GLAntialias.Sample4x;
			this.requestOnscreenRendering = false;
			this.requestOverlay = true;
			this.drawText = true;
			this.useLegacyAPI = true;
			initContent();
		}

		/// \~English
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="moduleID">Component ID of owner window or dialog class, for statistics of rendering time, set to null if not using</param>
		/// <param name="requestOnscreenRendering">Whether to request onscreen rendering (Use offscreen if unsupported), default is false</param>
		/// <param name="drawText">Whether to request drawing text, default is true</param>
		/// <param name="useLegacyAPI">Whether to use OpenGL legacy API, default is true</param>
		/// \~Chinese
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="moduleID">所属窗口组件或对话框组件ID，用于绘图时间记录与反馈，若不使用可输入null</param>
		/// <param name="requestOnscreenRendering">是否请求启用在屏渲染(若不支持则仍使用离屏渲染)，默认为false</param>
		/// <param name="drawText">是否需要绘制文本，默认为true</param>
		/// <param name="useLegacyAPI">是否需要使用OpenGL传统API，默认为true</param>
		public GLView(String? moduleID, bool requestOnscreenRendering, bool drawText = true, bool useLegacyAPI = true)
		{
			this.moduleID = moduleID;
			this.requestAntialias = GLAntialias.Sample4x;
			this.requestOnscreenRendering = requestOnscreenRendering;
			this.requestOverlay = true;
			this.drawText = drawText;
			this.useLegacyAPI = useLegacyAPI;
			initContent();
		}

        /// \~English
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleID">Component ID of owner window or dialog class, for statistics of rendering time, set to null if not using</param>
        /// <param name="requestAntialias">Antialias option (Use the nearest option if not supported), default is 4x sampling</param>
        /// <param name="requestOnscreenRendering">Whether to request onscreen rendering (Use offscreen if unsupported), default is false</param>
        /// <param name="requestOverlay">Whether to request support of overlay (SupportOverlay will be false if unsupported), default is true</param>
        /// <param name="drawText">Whether to request drawing text, default is true</param>
        /// <param name="useLegacyAPI">Whether to use OpenGL legacy API, default is true</param>
        /// \~Chinese
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="moduleID">所属窗口组件或对话框组件ID，用于绘图时间记录与反馈，若不使用可输入null</param>
        /// <param name="requestAntialias">请求抗锯齿选项(若不满足条件则使用最接近的选项)，默认为4倍抗锯齿</param>
        /// <param name="requestOnscreenRendering">是否请求启用在屏渲染(若不支持则仍使用离屏渲染)，默认为false</param>
        /// <param name="requestOverlay">是否需要支持被其他控件覆盖(若不支持则SupportOverlay属性为false)，默认为true</param>
        /// <param name="drawText">是否需要绘制文本，默认为true</param>
        /// <param name="useLegacyAPI">是否需要使用OpenGL传统API，默认为true</param>
        public GLView(String? moduleID, GLAntialias requestAntialias, bool requestOnscreenRendering = false, bool requestOverlay = true, bool drawText = true, bool useLegacyAPI = true)
		{
			this.moduleID = moduleID;
			this.requestAntialias = requestAntialias;
			this.requestOnscreenRendering = requestOnscreenRendering;
			this.requestOverlay = requestOverlay;
			this.drawText = drawText;
			this.useLegacyAPI = useLegacyAPI;
			initContent();
		}

		/// \~English
		/// <summary>
		/// Release resources
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 关闭视图，释放资源
		/// </summary>
		public void Close()
		{
			if (closed) return;
			if (glBackend != null) glBackend.ReleaseGL();
			closed = true;
		}

		~GLView()
		{
			if (!closed) AgencyLocal.Print("GLView.Close not called. Memory leaking.");
		}

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
			if (!Visible) return;
			if (!closed && glBackend != null) glBackend.QueueRender();
		}

		/// \~English
		/// <summary>
		/// Context info (null if not initialized yet)
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 上下文信息 (null表示还未初始化完成)
		/// </summary>
		public GLContextInfo? ContextInfo { get; private set; }

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

		/// \~English
		/// <summary>
		/// Whether overlay is supported
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 是否支持被其他控件覆盖
		/// </summary>
		public bool SupportOverlay
		{
			get { return supportOverlay; }
		}

        public void OnGLInitialize(OpenGL gl, GLContextInfo contextInfo)
        {
			ContextInfo = contextInfo;
            GLInitialize?.Invoke(this, new GLEventArgs(gl));
        }

        public void OnGLResize(OpenGL gl, GLSizeInfo sizeInfo)
        {
            GLResize?.Invoke(this, new GLResizeEventArgs(gl, sizeInfo));
        }

        public void OnGLRender(OpenGL gl, GLTextTasks textTasks)
        {
            GLRender?.Invoke(this, new GLRenderEventArgs(gl, textTasks));
			lock (renderTime)
			{
				renderTime.Add(DateTime.Now);
			}
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

		public void OnRaiseMouseDoubleClick(MouseEventArgs args)
		{
			OnMouseDoubleClick(args);
		}

        public string? OnGetModuleID()
        {
            return moduleID;
        }

		private void initContent()
		{
			if (!GPUOptions.IsGPURenderingDisabled && Factory != null)
			{
				var options = new GLOptions
				{
					EnableOnscreenRendering = requestOnscreenRendering || GPUOptions.IsOnscreenGPURenderingEnabled,
					UseTextTasks = drawText,
					UseLegacyAPI = useLegacyAPI,
					RequestAntialias = requestAntialias,
					RequestOverlay = requestOverlay,
				};
				Factory.CreateGLBackend(this, options, out etoControl, out glBackend, out supportOverlay);
				Content = etoControl;
			}
		}

        public static GLBackendFactory? Factory { private get; set; }

		private Control? etoControl;
		private GLBackend? glBackend;
		private List<DateTime> renderTime = [];
		private String? moduleID;
		private GLAntialias requestAntialias;
		private bool requestOnscreenRendering;
		private bool requestOverlay;
		private bool drawText;
		private bool useLegacyAPI;
		private bool supportOverlay = false;
		private bool closed = false;
	}

	public interface GLCallback
	{
		void OnGLInitialize(OpenGL gl, GLContextInfo contextInfo);
		void OnGLResize(OpenGL gl, GLSizeInfo sizeInfo);
		void OnGLRender(OpenGL gl, GLTextTasks textTasks);
		void OnRaiseMouseDown(MouseEventArgs args);
		void OnRaiseMouseMove(MouseEventArgs args);
		void OnRaiseMouseUp(MouseEventArgs args);
		void OnRaiseMouseWheel(MouseEventArgs args);
		void OnRaiseMouseDoubleClick(MouseEventArgs args);
		String? OnGetModuleID();
	}

	public interface GLBackend
	{
		void QueueRender();
		void ReleaseGL();
	}

	public class GLOptions
	{
		public bool EnableOnscreenRendering { get; set; }
		public bool UseTextTasks { get; set; }
		public bool UseLegacyAPI { get; set; }
		public bool RequestOverlay { get; set; }
		public GLAntialias RequestAntialias { get; set; }
	}

	public interface GLBackendFactory
	{
		void CreateGLBackend(GLCallback glCallback, GLOptions options, out Control etoControl, out GLBackend glBackend, out bool supportOverlay);
	}
}
