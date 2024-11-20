using System;
using System.Drawing;
using System.Windows.Forms;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=3.0.0) Timeline visualization control (vertical)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.0.0) 时间线显示控件（纵向）
    /// </summary>
    public partial class TimelineIndicatorV : UserControl
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

        public TimelineIndicatorV()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object? sender, PaintEventArgs e)
        {
            DrawBeat.CallbackBegin(pictureBox1, "ASEva.UICoreWF.TimelineIndicatorV");

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
                    e.Graphics.FillRectangle(new SolidBrush(rangeColor), -1.0f, (float)(lower * height) - 0.5f, (float)width + 1.0f, (float)((upper - lower) * height));
                }

                if (Value != null)
                {
                    var value = Math.Max(0, Math.Min(1, Value.Value));
                    e.Graphics.FillRectangle(new SolidBrush(valueColor), -1.0f, (float)(value * height) - 2.5f, (float)width + 1.0f, 4.0f);
                }
            }
            catch (Exception ex) { Dump.Exception(ex); }

            DrawBeat.CallbackEnd(pictureBox1);
        }

        private double? lower, upper, val;
    }
}
