using System;
using System.Collections.Generic;
using Eto;
using Eto.Forms;
using SharpGL;

namespace ASEva.UIEto
{
	/// <summary>
	/// (api:eto=2.6.0) 文本锚点坐标位置类型
	/// </summary>
    public enum TextAnchor
    {
		/// <summary>
		/// 文本中心
		/// </summary>
        Center = 0,

		/// <summary>
		/// 文本左上角
		/// </summary>
        TopLeft = 1,

		/// <summary>
		/// 文本左侧中心
		/// </summary>
        LeftCenter = 2,

		/// <summary>
		/// 文本左下角
		/// </summary>
        BottomLeft = 3,

		/// <summary>
		/// 文本右上角
		/// </summary>
        TopRight = 4,

		/// <summary>
		/// 文本右侧中心
		/// </summary>
        RightCenter = 5,
		
		/// <summary>
		/// 文本右下角
		/// </summary>
        BottomRight = 6,

		/// <summary>
		/// 文本上方中心
		/// </summary>
        TopCenter = 7,

		/// <summary>
		/// 文本下方中心
		/// </summary>
        BottomCenter = 8,
    }

	/// <summary>
	/// (api:eto=2.6.0) OpenGL文本绘制任务
	/// </summary>
    public struct GLTextTask
    {
		/// <summary>
		/// 文本
		/// </summary>
        public String text;

		/// <summary>
		/// 文本锚点X轴坐标
		/// </summary>
        public int posX;

		/// <summary>
		/// 文本锚点Y轴坐标
		/// </summary>
        public int posY;

		/// <summary>
		/// 文本锚点坐标是否为物理像素坐标系，否则为逻辑坐标
		/// </summary>
        public bool isRealPos;

		/// <summary>
		/// 文本锚点坐标位置类型
		/// </summary>
        public TextAnchor anchor;

		/// <summary>
		/// 文本绘制颜色红色分量
		/// </summary>
        public byte red;

		/// <summary>
		/// 文本绘制颜色绿色分量
		/// </summary>
        public byte green;

		/// <summary>
		/// 文本绘制颜色蓝色分量
		/// </summary>
        public byte blue;

		/// <summary>
		/// 文本绘制颜色Alpha通道分量，0将被视为255
		/// </summary>
        public byte alpha;

		/// <summary>
		/// 文本字体，空则使用默认字体
		/// </summary>
        public String fontName;

		/// <summary>
		/// 文本相对于默认尺寸的比例，0将被视为1
		/// </summary>
        public float sizeScale; // 0 as 1
    }

	/// <summary>
	/// (api:eto=2.6.0) OpenGL文本绘制任务对象
	/// </summary>
    public class GLTextTasks
    {
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

        private List<GLTextTask> tasks = new List<GLTextTask>();
    }

	/// <summary>
	/// (api:eto=2.6.0) OpenGL绘制尺寸信息
	/// </summary>
    public class GLSizeInfo
    {
		/// <summary>
		/// 逻辑宽度
		/// </summary>
        public int LogicalWidth { get; private set; }

		/// <summary>
		/// 逻辑高度
		/// </summary>
        public int LogicalHeight { get; private set; }

		/// <summary>
		/// 物理像素宽度
		/// </summary>
        public int RealWidth { get; private set; }

		/// <summary>
		/// 物理像素高度
		/// </summary>
        public int RealHeight { get; private set; }

		/// <summary>
		/// 物理像素与逻辑
		/// </summary>
		public double RealPixelScale { get; private set; }

		/// <summary>
		/// 宽度与高度之比
		/// </summary>
        public float AspectRatio { get; private set; }

        public GLSizeInfo(int logicalWidth, int logicalHeight, int realWidth, int realHeight, double realPixelScale, float aspectRatio)
        {
            LogicalWidth = logicalWidth;
            LogicalHeight = logicalHeight;
            RealWidth = realWidth;
            RealHeight = realHeight;
			RealPixelScale = realPixelScale;
            AspectRatio = aspectRatio;
        }
    }

	/// <summary>
	/// (api:eto=2.6.0) GLView事件参数基类
	/// </summary>
	public class GLEventArgs : EventArgs
	{
		public GLEventArgs(OpenGL gl)
		{
			GL = gl;
		}

		/// <summary>
		/// OpenGL对象
		/// </summary>
		public OpenGL GL { get; private set; }
	}

	/// <summary>
	/// (api:eto=2.6.0) GLView缩放事件参数
	/// </summary>
	public class GLResizeEventArgs : GLEventArgs
	{
		public GLResizeEventArgs(OpenGL gl, GLSizeInfo sizeInfo) : base(gl)
		{
			SizeInfo = sizeInfo;
		}

		/// <summary>
		/// 缩放后的尺寸信息
		/// </summary>
		public GLSizeInfo SizeInfo { get; private set; }
	}

	/// <summary>
	/// (api:eto=2.6.0) GLView渲染事件参数
	/// </summary>
	public class GLRenderEventArgs : GLEventArgs
	{
		public GLRenderEventArgs(OpenGL gl, GLTextTasks textTasks) : base(gl)
		{
			TextTasks = textTasks;
		}

		/// <summary>
		/// 文本绘制任务对象
		/// </summary>
		public GLTextTasks TextTasks { get; private set; }
	}

	/// <summary>
	/// (api:eto=2.6.1) OpenGL上下文信息
	/// </summary>
	public struct GLContextInfo
	{
		/// <summary>
		/// 版本
		/// </summary>
        public String version;

		/// <summary>
		/// 厂商
		/// </summary>
        public String vendor;

		/// <summary>
		/// 渲染器
		/// </summary>
        public String renderer;

		/// <summary>
		/// 扩展
		/// </summary>
        public String extensions;

		/// <summary>
		/// (api:eto=2.8.5)
		/// </summary>
		/// <returns></returns>
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

	/// <summary>
	/// (api:eto=2.6.0) OpenGL绘制视图
	/// </summary>
	public class GLView : Panel, GLCallback
	{
		/// <summary>
		/// 初始化事件
		/// </summary>
		public event EventHandler<GLEventArgs> GLInitialize;

		/// <summary>
		/// 缩放事件
		/// </summary>
		public event EventHandler<GLResizeEventArgs> GLResize;

		/// <summary>
		/// 渲染事件
		/// </summary>
		public event EventHandler<GLRenderEventArgs> GLRender;

		/// <summary>
		/// 构造函数
		/// </summary>
		public GLView()
		{
			this.moduleID = null;
			this.requestOnscreenRendering = false;
			this.drawText = true;
			this.useLegacyAPI = true;
			initContent();
		}

		/// <summary>
		/// (api:eto=2.8.3) 构造函数
		/// </summary>
		/// <param name="moduleID">所属窗口组件或对话框组件ID，用于绘图时间记录与反馈</param>
		public GLView(String moduleID)
		{
			this.moduleID = moduleID;
			this.requestOnscreenRendering = false;
			this.drawText = true;
			this.useLegacyAPI = true;
			initContent();
		}

		/// <summary>
		/// (api:eto=2.8.7) 构造函数
		/// </summary>
		/// <param name="moduleID">所属窗口组件或对话框组件ID，用于绘图时间记录与反馈，若不使用可输入null</param>
		/// <param name="requestOnscreenRendering">是否请求启用在屏渲染(若不支持则仍使用离屏渲染)，默认为false</param>
		/// <param name="drawText">是否需要绘制文本，默认为true</param>
		/// <param name="useLegacyAPI">是否需要使用OpenGL传统API，默认为true</param>
		public GLView(String moduleID, bool requestOnscreenRendering, bool drawText, bool useLegacyAPI)
		{
			this.moduleID = moduleID;
			this.requestOnscreenRendering = requestOnscreenRendering;
			this.drawText = drawText;
			this.useLegacyAPI = useLegacyAPI;
			initContent();
		}

		/// <summary>
		/// 关闭视图，释放资源
		/// </summary>
		public void Close()
		{
			if (closed) return;
			if (glBackend != null) glBackend.ReleaseGL();
			closed = true;
		}
		private bool closed = false;

		/// <summary>
		/// 提交新的渲染请求
		/// </summary>
		public void QueueRender()
		{
			if (!closed && glBackend != null) glBackend.QueueRender();
		}

		/// <summary>
		/// (api:eto=2.6.1) 上下文信息 (null表示还未初始化完成)
		/// </summary>
		public GLContextInfo? ContextInfo { get; private set; }

		/// <summary>
		/// (api:eto=2.6.1) 渲染帧率（统计最近3秒）
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

            if (GLInitialize != null) GLInitialize(this, new GLEventArgs(gl));
        }

        public void OnGLResize(OpenGL gl, GLSizeInfo sizeInfo)
        {
            if (GLResize != null) GLResize(this, new GLResizeEventArgs(gl, sizeInfo));
        }

        public void OnGLRender(OpenGL gl, GLTextTasks textTasks)
        {
            if (GLRender != null) GLRender(this, new GLRenderEventArgs(gl, textTasks));
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

        public string OnGetModuleID()
        {
            return moduleID;
        }

		private void initContent()
		{
			if (!Agency.IsGPURenderingDisabled() && Factory != null)
			{
				var options = new GLOptions
				{
					EnableOnscreenRendering = requestOnscreenRendering || Agency.IsOnscreenGPURenderingEnabled(),
					UseTextTasks = drawText,
					UseLegacyAPI = useLegacyAPI,
				};
				Factory.CreateGLBackend(this, options, out etoControl, out glBackend, out supportOverlay);
				if (etoControl != null) Content = etoControl;
			}
		}

        public static GLBackendFactory Factory { private get; set; }

		private Control etoControl;
		private GLBackend glBackend;
		private List<DateTime> renderTime = new List<DateTime>();
		private String moduleID;
		private bool requestOnscreenRendering;
		private bool drawText;
		private bool useLegacyAPI;
		private bool supportOverlay = false;
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
		String OnGetModuleID();
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
	}

	public interface GLBackendFactory
	{
		void CreateGLBackend(GLCallback glCallback, GLOptions options, out Control etoControl, out GLBackend glBackend, out bool supportOverlay);
	}
}
