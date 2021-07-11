using System;
using System.Drawing;
using System.Windows.Forms;

namespace ASEva.CoreWF
{
    /// <summary>
    /// (api:corewf=1.0.0) 圆形指示器
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
                this.Refresh();
            }
        }

        private Color indicatorColor;
        public Color IndicatorColor
        {
            get { return indicatorColor; }
            set
            {
                indicatorColor = value;
                this.Refresh();
            }
        }

        public CircleIndicator()
        {
            InitializeComponent();
            borderColor = Color.White;
            indicatorColor = Color.Black;
            this.Refresh();
        }

        public delegate void CircleIndicatorHandler(object sender);
        public event CircleIndicatorHandler IndicatorClicked;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var dpiRatio = (float)DeviceDpi / 96;

            var sizeLarge = Math.Min(this.Width, this.Height) - 2;
            int sizeBorder = (int)(dpiRatio * 2);
            var sizeSmall = sizeLarge - sizeBorder * 2;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(BorderColor), new Rectangle(1, 1, sizeLarge, sizeLarge));
            e.Graphics.FillEllipse(new SolidBrush(IndicatorColor), new Rectangle(1 + sizeBorder, 1 + sizeBorder, sizeSmall, sizeSmall));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (IndicatorClicked != null) IndicatorClicked(this);
        }
    }
}
