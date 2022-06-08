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
	/// (api:eto=2.6.0) GLView的文本绘制任务
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
	/// (api:eto=2.6.0) GLView的文本绘制任务对象
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
	/// (api:eto=2.6.0) GLView的尺寸信息
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
	/// (api:eto=2.6.0) OpenGL绘制视图（注意，此控件不支持在 ASEva.UIEto.OverlayLayout 中使用）
	/// </summary>
	public class GLView : Panel, GLView.GLViewCallback
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
			if (Factory != null)
			{
				Factory.CreateGLViewBackend(this, out etoControl, out glViewBackend);
				if (etoControl != null) Content = etoControl;
				if (glViewBackend != null) glViewBackend.InitializeGL();
			}
		}

		/// <summary>
		/// 关闭视图，释放资源
		/// </summary>
		public void Close()
		{
			if (closed) return;
			if (glViewBackend != null) glViewBackend.ReleaseGL();
			closed = true;
		}
		private bool closed = false;

		/// <summary>
		/// 提交新的渲染请求
		/// </summary>
		public void QueueRender()
		{
			if (!closed && glViewBackend != null) glViewBackend.QueueRender();
		}

        public void OnGLInitialize(OpenGL gl)
        {
            if (GLInitialize != null) GLInitialize(this, new GLEventArgs(gl));
        }

        public void OnGLResize(OpenGL gl, GLSizeInfo sizeInfo)
        {
            if (GLResize != null) GLResize(this, new GLResizeEventArgs(gl, sizeInfo));
        }

        public void OnGLRender(OpenGL gl, GLTextTasks textTasks)
        {
            if (GLRender != null) GLRender(this, new GLRenderEventArgs(gl, textTasks));
        }

        public interface GLViewCallback
		{
			void OnGLInitialize(OpenGL gl);

			void OnGLResize(OpenGL gl, GLSizeInfo sizeInfo);

			void OnGLRender(OpenGL gl, GLTextTasks textTasks);
		}

		public interface GLViewBackend
		{
			void InitializeGL();

			void QueueRender();

			void ReleaseGL();
		}

		public interface GLViewBackendFactory
		{
			void CreateGLViewBackend(GLViewCallback glView, out Control etoControl, out GLViewBackend glViewBackend);
		}

		public static GLViewBackendFactory Factory { private get; set; }

		private Control etoControl;
		private GLViewBackend glViewBackend;
	}
}
