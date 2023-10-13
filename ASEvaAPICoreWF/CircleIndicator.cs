using System;
using System.Drawing;
using System.Windows.Forms;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=2.0.0) Circle indicator
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=2.0.0) 圆形指示器
    /// </summary>
    public partial class CircleIndicator : UserControl
    {
        private Color borderColor;
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                DrawBeat.CallerBegin(pictureBox1);
                pictureBox1.Refresh();
                DrawBeat.CallerEnd(pictureBox1);
            }
        }

        private Color indicatorColor;
        public Color IndicatorColor
        {
            get { return indicatorColor; }
            set
            {
                indicatorColor = value;
                DrawBeat.CallerBegin(pictureBox1);
                pictureBox1.Refresh();
                DrawBeat.CallerEnd(pictureBox1);
            }
        }

        public CircleIndicator()
        {
            InitializeComponent();
            borderColor = Color.White;
            indicatorColor = Color.Black;
            DrawBeat.CallerBegin(pictureBox1);
            pictureBox1.Refresh();
            DrawBeat.CallerEnd(pictureBox1);
        }

        public delegate void CircleIndicatorHandler(object sender);
        public event CircleIndicatorHandler IndicatorClicked;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawBeat.CallbackBegin(pictureBox1, "ASEva.UICoreWF.CircleIndicator");

            try
            {
                var dpiRatio = (float)DeviceDpi / 96;

                var sizeLarge = Math.Min(this.Width, this.Height) - 2;
                int sizeBorder = (int)(dpiRatio * 2);
                var sizeSmall = sizeLarge - sizeBorder * 2;

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(BorderColor), new Rectangle(1, 1, sizeLarge, sizeLarge));
                e.Graphics.FillEllipse(new SolidBrush(IndicatorColor), new Rectangle(1 + sizeBorder, 1 + sizeBorder, sizeSmall, sizeSmall));
            }
            catch (Exception) { }

            DrawBeat.CallbackEnd(pictureBox1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (IndicatorClicked != null) IndicatorClicked(this);
        }
    }
}
