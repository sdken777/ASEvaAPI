using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.3.3) 面板式按钮
    /// </summary>
    public class ButtonPanel : Panel
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 初始化面板式文字按钮
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

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 初始化面板式图像按钮
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

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.17) 更新图像
        /// </summary>
        /// <param name="image">新图像，尺寸应与原图像一致，否则不更新</param>
        public void UpdateImage(Bitmap image)
        {
            if (image == null || image.Width != defaultBitmap.Width || image.Height != defaultBitmap.Height) return;
            if (image == defaultBitmap) return;

            defaultBitmap = image;
            disableBitmapObj = null;
            imageView.Image = image;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 点击按钮事件
        /// </summary>
        public event EventHandler Click;

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 默认状态下的背景颜色（默认为Colors.Transparent）
        /// </summary>
        public Color DefaultBackgroundColor
        {
            get { return defaultBackgroundColor; }
            set
            {
                if (defaultBackgroundColor == value) return;
                defaultBackgroundColor = value;
                if (!mouseInside && !mouseDown) BackgroundColor = value;
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 鼠标在按钮范围内的背景颜色（默认为Colors.LightSteelBlue）
        /// </summary>
        public Color MouseInsideColor
        {
            get { return mouseInsideColor; }
            set
            {
                if (mouseInsideColor == value) return;
                mouseInsideColor = value;
                if (mouseInside) BackgroundColor = value;
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 鼠标按下后的背景颜色（默认为Colors.SteelBlue）
        /// </summary>
        public Color MouseDownColor
        {
            get { return mouseDownColor; }
            set
            {
                if (mouseDownColor == value) return;
                mouseDownColor = value;
                if (mouseDown) BackgroundColor = value;
            }
        }
        
        /// \~English
        /// 
        /// \~Chinese
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
                if (textColor == value) return;
                textColor = value;
                if (label != null) setLabelTextColor();
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.11) 提示文字
        /// </summary>
        public override string ToolTip
        {
            get
            {
                if (label != null) return label.ToolTip;
                else if (imageView != null) return imageView.ToolTip;
                else return null;
            }
            set
            {
                if (label != null)
                {
                    if (label.ToolTip == value) return;
                    label.ToolTip = value;
                }
                else if (imageView != null)
                {
                    if (imageView.ToolTip == value) return;
                    imageView.ToolTip = value;
                }
            }
        }

        private void setLabelTextColor()
        {
            if (Enabled) label.TextColor = textColor;
            else
            {
                var grayScale = textColor.ToHSL().L;
                if (TextAlphaUnsupported)
                {
                    var grayScaleAlpha = grayScale * 0.3f +  (grayScale < 0.5f ? 0.7f : 0);
                    label.TextColor = new Color(grayScaleAlpha, grayScaleAlpha, grayScaleAlpha);
                }
                else label.TextColor = new Color(grayScale, grayScale, grayScale, textColor.A / 3);
            }
        }

        private void initialize()
        {
            if (UseInnerEnterLeave)
            {
                Control innerControl = null;
                if (label != null) innerControl = label;
                if (imageView != null) innerControl = imageView;
                innerControl.MouseEnter += delegate
                {
                    if (!Enabled) return;
                    mouseInside = true;
                    BackgroundColor = mouseInsideColor;
                };
                innerControl.MouseLeave += delegate
                {
                    mouseInside = false;
                    BackgroundColor = mouseDown ? mouseDownColor : defaultBackgroundColor;
                };
            }
            else
            {
                MouseEnter += delegate
                {
                    if (!Enabled) return;
                    mouseInside = true;
                    BackgroundColor = mouseInsideColor;
                };
                MouseLeave += delegate
                {
                    mouseInside = false;
                    BackgroundColor = mouseDown ? mouseDownColor : defaultBackgroundColor;
                };
            }

            MouseDown += delegate
            {
                if (!Enabled) return;
                mouseDown = true;
                BackgroundColor = mouseDownColor;
            };
            MouseUp += delegate
            {
                mouseDown = false;
                BackgroundColor = mouseInside ? mouseInsideColor : defaultBackgroundColor;
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
        private Color defaultBackgroundColor = Colors.Transparent;
        private Color mouseInsideColor = Colors.LightSteelBlue;
        private Color mouseDownColor = Colors.SteelBlue;
        private Color textColor = Colors.Black;
        private Label label = null;
        private ImageView imageView = null;
        private Bitmap defaultBitmap = null, disableBitmapObj = null;

        public static bool UseInnerEnterLeave { private get; set; }
        public static bool TextAlphaUnsupported { private get; set; }
    }
}