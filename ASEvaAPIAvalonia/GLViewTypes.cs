using System;
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia.OpenGL;

namespace ASEva.UIAvalonia
{
	/// \~English
	/// <summary>
	/// (api:avalonia=1.0.10) OpenGL text drawing task
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:avalonia=1.0.10) OpenGL文本绘制任务
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
		/// Anchor point's type along X-axis
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// X轴上的文本锚点坐标位置类型
		/// </summary>
        public AlignmentX anchorX;

        /// \~English
		/// <summary>
		/// Anchor point's type along Y-axis
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// Y轴上的文本锚点坐标位置类型
		/// </summary>
        public AlignmentY anchorY;

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
        public String fontName;

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
	/// (api:avalonia=1.0.10) OpenGL text drawing task list
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:avalonia=1.0.10) OpenGL文本绘制任务对象
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
                anchorX = AlignmentX.Center,
                anchorY = AlignmentY.Center,
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

        private List<GLTextTask> tasks = new List<GLTextTask>();
    }

	/// \~English
	/// <summary>
	/// (api:avalonia=1.0.10) OpenGL rendering size info
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:avalonia=1.0.10) OpenGL绘制尺寸信息
	/// </summary>
    public class GLSizeInfo
    {
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
		/// Ratio of width to height
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 宽度与高度之比
		/// </summary>
        public float AspectRatio { get; private set; }

		/// \~English
		/// <summary>
		/// Constructor
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 构造函数
		/// </summary>
        public GLSizeInfo(int realWidth, int realHeight, float aspectRatio)
        {
            RealWidth = realWidth;
            RealHeight = realHeight;
            AspectRatio = aspectRatio;
        }
    }

	/// \~English
	/// <summary>
	/// (api:avalonia=1.0.10) GLView event argument base class
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:avalonia=1.0.10) GLView事件参数基类
	/// </summary>
	public class GLEventArgs : EventArgs
	{
		public GLEventArgs(GlInterface gl, int fb)
		{
			GL = gl;
            FB = fb;
		}

		/// \~English
		/// <summary>
		/// OpenGL object
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// OpenGL对象
		/// </summary>
		public GlInterface GL { get; private set; }

		/// \~English
		/// <summary>
		/// Framebuffer ID
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 帧缓冲ID
		/// </summary>
        public int FB { get; private set; }
	}

	/// \~English
	/// <summary>
	/// (api:avalonia=1.0.10) GLView resize event argument
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:avalonia=1.0.10) GLView缩放事件参数
	/// </summary>
	public class GLResizeEventArgs : GLEventArgs
	{
		public GLResizeEventArgs(GlInterface gl, int fb, GLSizeInfo sizeInfo) : base(gl, fb)
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
	/// (api:avalonia=1.0.10) GLView render event argument
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:avalonia=1.0.10) GLView渲染事件参数
	/// </summary>
	public class GLRenderEventArgs : GLEventArgs
	{
		public GLRenderEventArgs(GlInterface gl, int fb, GLTextTasks textTasks) : base(gl, fb)
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
}