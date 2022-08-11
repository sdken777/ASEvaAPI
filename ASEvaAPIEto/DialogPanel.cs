using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.8.0) 对话框主面板（使用 ASEva.UIEto.App.RunDialog 弹出对话框）
    /// </summary>
    public class DialogPanel : Panel
    {
        /// <summary>
        /// 对话框图标
        /// </summary>
        public Icon Icon { get; protected set; }

        /// <summary>
        /// 对话框标题
        /// </summary>
        public String Title { get; protected set; }

        /// <summary>
        /// 初始化为固定大小模式
        /// </summary>
        /// <param name="fixWidth">固定逻辑宽度，至少100</param>
        /// <param name="fixHeight">固定逻辑高度，至少20</param>
        /// <param name="withBorder">是否带窗口边框</param>
        protected void SetFixMode(int fixWidth, int fixHeight, bool withBorder)
        {
            if (this.mode != DialogMode.Invalid) return;

            this.mode = DialogMode.FixMode;
            this.minWidth = this.defaultWidth = Math.Max(100, fixWidth);
            this.minHeight = this.defaultHeight = Math.Max(20, fixHeight);
            this.withBorder = withBorder;
        }

        /// <summary>
        /// 初始化为可变大小模式
        /// </summary>
        /// <param name="minWidth">最小逻辑宽度，至少100</param>
        /// <param name="minHeight">最小逻辑高度，至少20</param>
        /// <param name="defaultWidth">初始逻辑宽度</param>
        /// <param name="defaultHeight">初始逻辑高度</param>
        protected void SetResizableMode(int minWidth, int minHeight, int defaultWidth, int defaultHeight)
        {
            if (this.mode != DialogMode.Invalid) return;

            this.mode = DialogMode.ResizableMode;
            this.minWidth = Math.Max(100, minWidth);
            this.minHeight = Math.Max(20, minHeight);
            this.defaultWidth = Math.Max(this.minWidth, defaultWidth);
            this.defaultHeight = Math.Max(this.minHeight, defaultHeight);
            this.withBorder = true;
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        protected void Close()
        {
            if (OnDialogClose != null) OnDialogClose(this, null);
        }

        /// <summary>
        /// 对话框即将关闭回调函数
        /// </summary>
        public virtual void OnClosing()
        {
        }

        /// <summary>
        /// 布尔类型的对话框运行结果
        /// </summary>
        public bool BoolResult { get; protected set; }

        /// <summary>
        /// 整数类型的对话框运行结果
        /// </summary>
        public int IntResult { get; protected set; }

        /// <summary>
        /// 浮点类型的对话框运行结果
        /// </summary>
        public double DoubleResult { get; protected set; }

        /// <summary>
        /// 字符串类型的对话框运行结果
        /// </summary>
        public String StringResult { get; protected set; }

        /// <summary>
        /// 任意类型的对话框运行结果
        /// </summary>
        public object ObjectResult { get; protected set; }

        /// <summary>
        /// 对话框模式
        /// </summary>
        public enum DialogMode
        {
            /// <summary>
            /// 未初始化
            /// </summary>
            Invalid,

            /// <summary>
            /// 固定大小模式
            /// </summary>
            FixMode,

            /// <summary>
            /// 可变大小模式
            /// </summary>
            ResizableMode,
        }

        /// <summary>
        /// 获取对话框模式
        /// </summary>
        public DialogMode Mode
        {
            get { return mode; }
        }

        /// <summary>
        /// 获取对话框最小逻辑尺寸（固定大小模式下即该固定逻辑尺寸）
        /// </summary>
        public Size MinSize
        {
            get { return new Size(minWidth, minHeight); }
        }

        /// <summary>
        /// 获取对话框初始逻辑尺寸
        /// </summary>
        public Size DefaultSize
        {
            get { return new Size(defaultWidth, defaultHeight); }
        }

        /// <summary>
        /// 获取是否带窗口边框
        /// </summary>
        public bool WithBorder
        {
            get { return withBorder; }
        }

        public event EventHandler OnDialogClose;

        private DialogMode mode = DialogMode.Invalid;
        private int minWidth;
        private int minHeight;
        private int defaultWidth;
        private int defaultHeight;
        private bool withBorder = true;
    }
}
