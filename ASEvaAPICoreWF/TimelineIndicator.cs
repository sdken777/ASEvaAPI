using System;
using System.Drawing;
using System.Windows.Forms;

namespace ASEva.CoreWF
{
    /// <summary>
    /// (api:corewf=1.0.0) 时间线显示控件（横向）
    /// </summary>
    public partial class TimelineIndicator : UserControl
    {
        public double? Lower { get; set; }
        public double? Upper { get; set; }
        public double? Value { get; set; }

        public TimelineIndicator()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            double width = pictureBox1.Width;
            double height = pictureBox1.Height;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Color rangeColor = Color.FromArgb(192, Color.Orange);
            Color valueColor = Color.FromArgb(192, Color.Blue);

            if (Lower != null && Upper != null)
            {
                var lower = Math.Max(0, Math.Min(1, Lower.Value));
                var upper = Math.Max(0, Math.Min(1, Upper.Value));
                e.Graphics.FillRectangle(new SolidBrush(rangeColor), (float)(lower * width) - 0.5f, -1.0f, (float)((upper - lower) * width), (float)height + 1.0f);
            }

            if (Value != null)
            {
                var value = Math.Max(0, Math.Min(1, Value.Value));
                e.Graphics.FillRectangle(new SolidBrush(valueColor), (float)(value * width) - 2.5f, -1.0f, 4.0f, (float)height + 1.0f);
            }
        }

    }
}
