using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.3.3) 按钮面板
    /// </summary>
    public class ButtonPanel : Panel
    {
        /// <summary>
        /// 初始化文字按钮面板
        /// </summary>
        /// <param name="text">文字</param>
        /// <param name="logicalPadding">按钮边框与文字的间距</param>
        public ButtonPanel(String text, int logicalPadding)
        {
            if (text == null) text = "";
            label = this.SetContentAsColumnLayout(logicalPadding).AddLabel(text, TextAlignment.Center, true);
            label.TextColor = textColor;
            initialize();
        }

        /// <summary>
        /// 初始化图像按钮面板
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="logicalPadding">按钮边框与文字的间距</param>
        public ButtonPanel(Bitmap image, int logicalPadding)
        {
            if (image == null) throw new NullReferenceException("Null image.");
            defaultBitmap = image;
            imageView = this.SetContentAsColumnLayout(logicalPadding).AddControl(new ImageView{ Image = image }, true) as ImageView;
            initialize();
        }

        /// <summary>
        /// 点击按钮事件
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// 默认状态下的背景颜色（默认为Colors.Transparent）
        /// </summary>
        public Color DefaultBackgroundColor { get; set; }

        /// <summary>
        /// 鼠标在按钮范围内的背景颜色（默认为Colors.LightSteelBlue）
        /// </summary>
        public Color MouseInsideColor { get; set; }

        /// <summary>
        /// 鼠标按下后的背景颜色（默认为Colors.SteelBlue）
        /// </summary>
        public Color MouseDownColor { get; set; }
        
        /// <summary>
        /// 文字颜色（默认为Colors.Black）
        /// </summary>
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                if (label != null) setLabelTextColor();
            }
        }

        private void setLabelTextColor()
        {
            if (Enabled) label.TextColor = textColor;
            else
            {
                var grayScale = textColor.ToHSL().L;
                label.TextColor = new Color(grayScale, grayScale, grayScale, textColor.A / 3);
            }
        }

        private void initialize()
        {
            DefaultBackgroundColor = Colors.Transparent;
            MouseInsideColor = Colors.LightSteelBlue;
            MouseDownColor = Colors.SteelBlue;

            if (UseInnerEnterLeave)
            {
                Control innerControl = null;
                if (label != null) innerControl = label;
                if (imageView != null) innerControl = imageView;
                innerControl.MouseEnter += delegate
                {
                    mouseInside = true;
                    BackgroundColor = MouseInsideColor;
                };
                innerControl.MouseLeave += delegate
                {
                    mouseInside = false;
                    BackgroundColor = mouseDown ? MouseDownColor : DefaultBackgroundColor;
                };
            }
            else
            {
                MouseEnter += delegate
                {
                    mouseInside = true;
                    BackgroundColor = MouseInsideColor;
                };
                MouseLeave += delegate
                {
                    mouseInside = false;
                    BackgroundColor = mouseDown ? MouseDownColor : DefaultBackgroundColor;
                };
            }

            MouseDown += delegate
            {
                mouseDown = true;
                BackgroundColor = MouseDownColor;
            };
            MouseUp += delegate
            {
                mouseDown = false;
                BackgroundColor = mouseInside ? MouseInsideColor : DefaultBackgroundColor;
                if (Enabled && Click != null) Click(this, null);
            };

            EnabledChanged += delegate
            {
                if (label != null) setLabelTextColor();
                if (imageView != null) imageView.Image = Enabled ? defaultBitmap : disableBitmap;
            };
        }

        private Bitmap disableBitmap
        {
            get
            {
                if (disableBitmapObj == null)
                {
                    disableBitmapObj = defaultBitmap.Clone();
                    for (int i = 0; i < disableBitmapObj.Width; i++)
                    {
                        for (int j = 0; j < disableBitmapObj.Height; j++)
                        {
                            var color = disableBitmapObj.GetPixel(i, j);
                            var grayScale = color.ToHSL().L;
                            disableBitmapObj.SetPixel(i, j, new Color(grayScale, grayScale, grayScale, color.A / 3));
                        }
                    }
                }
                return disableBitmapObj;
            }
        }

        private bool mouseInside = false, mouseDown = false;
        private Color textColor = Colors.Black;
        private Label label = null;
        private ImageView imageView = null;
        private Bitmap defaultBitmap = null, disableBitmapObj = null;

        public static bool UseInnerEnterLeave { private get; set; }
    }
}