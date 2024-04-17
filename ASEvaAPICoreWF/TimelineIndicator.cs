using System;
using System.Drawing;
using System.Windows.Forms;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=3.0.0) Timeline visualization control (horizontal)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.0.0) 时间线显示控件（横向）
    /// </summary>
    public partial class TimelineIndicator : UserControl
    {
        public double? Lower
        {
            get { return lower; }
            set
            {
                lower = value;
                DrawBeat.CallerBegin(pictureBox1);
                pictureBox1.Refresh();
                DrawBeat.CallerEnd(pictureBox1);
            }
        }

        public double? Upper
        {
            get { return upper; }
            set
            {
                upper = value;
                DrawBeat.CallerBegin(pictureBox1);
                pictureBox1.Refresh();
                DrawBeat.CallerEnd(pictureBox1);
            }
        }

        public double? Value
        {
            get { return val; }
            set
            {
                val = value;
                DrawBeat.CallerBegin(pictureBox1);
                pictureBox1.Refresh();
                DrawBeat.CallerEnd(pictureBox1);
            }
        }

        public TimelineIndicator()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawBeat.CallbackBegin(pictureBox1, "ASEva.UICoreWF.TimelineIndicator");

            try
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
            catch (Exception) { }

            DrawBeat.CallbackEnd(pictureBox1);
        }

        private double? lower, upper, val;
    }
}
