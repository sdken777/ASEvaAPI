using System;
using System.Drawing;
using System.Windows.Forms;
using ASEva.Graph;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=3.0.0) Label table graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.0.0) 标签热力图数据可视化窗口
    /// </summary>
    public partial class LabelTableGraph : BaseGraph
    {
        private Pen blackPen = new Pen(Color.Black);
        private Pen grayPen = new Pen(Color.Gray);
        private Pen bluePen = new Pen(Color.DodgerBlue);
        private Brush brushBlack = new SolidBrush(Color.Black);
        private Brush brushBlue = new SolidBrush(Color.DodgerBlue);
        private Brush brushPurple = new SolidBrush(Color.MediumPurple);

        private Font font7f = new Font("微软雅黑", 7f);
        private Font font8f = new Font("微软雅黑", 8f);

        public LabelTableGraph()
        {
            InitializeComponent();
        }

        public override void UpdateUIWithData()
        {
            if (Data == null || !(Data is LabelTableData)) return;

            // Data display / 数据显示
            if (DrawBeat.CallerBegin(pictureBox1))
            {
                pictureBox1.Refresh();
                DrawBeat.CallerEnd(pictureBox1);
            }

            // Title display / 标题显示
            label1.Text = Data.Definition.MainTitle;
            if (!Data.HasData())
            {
                label2.ForeColor = Color.Black;
                label2.Text = "No Data.";
                return;
            }
            else label2.Text = "";
        }

        private void pictureBox1_Click(object? sender, EventArgs e)
        {
            HandleGraphSelected();
        }

        private void pictureBox1_Paint(object? sender, PaintEventArgs e)
        {
            DrawBeat.CallbackBegin(pictureBox1, "ASEva.UICoreWF.LabelTableGraph");

            try
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                pic_drawAxis(sender, e);
                pic_drawBarGraph(sender, e);
                pic_drawHeatMap(sender, e);
                pic_drawAnnotation(sender, e);
                pic_drawGuide(sender, e);
            }
            catch (Exception ex) { Dump.Exception(ex); }

            DrawBeat.CallbackEnd(pictureBox1);
        }

        private void pic_drawAxis(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);
            var margin = 15.0f * DeviceDpi / 96;

            //画坐标轴
            PointF pointx1 = new PointF(0, originPoint.Y);
            PointF pointx2 = new PointF(width, originPoint.Y);
            PointF pointy1 = new PointF(originPoint.X, 0);
            PointF pointy2 = new PointF(originPoint.X, height);
            g.DrawLine(blackPen, pointx1, pointx2);
            g.DrawLine(blackPen, pointy1, pointy2);
            g.DrawLine(grayPen, new PointF(originPoint.X, originPoint.Y + margin), new PointF(width - 1, originPoint.Y + margin));
            g.DrawLine(grayPen, new PointF(originPoint.X - margin, originPoint.Y), new PointF(originPoint.X - margin, 0));

            var D = Data as LabelTableData;
            if (D == null) return;

            var xTitle = D.GetXTitle();
            var yTitle = D.GetYTitle();
            var xTitleWidth = g.MeasureString(xTitle, font7f).Width;
            var yTitleWidth = g.MeasureString(yTitle, font7f).Width;
            PointF xTitlePoint = new PointF((width + originPoint.X - xTitleWidth) / 2, originPoint.Y);
            PointF yTitlePoint = new PointF(originPoint.X - margin, (originPoint.Y - yTitleWidth) / 2);
            g.DrawString(xTitle, font7f, brushBlue, xTitlePoint);
            g.DrawString(yTitle, font7f, brushBlue, yTitlePoint, new StringFormat(StringFormatFlags.DirectionVertical));

            //画坐标
            g.DrawLine(blackPen, new PointF(width - 1, originPoint.Y), new PointF(width - 1, originPoint.Y + 2));
            g.DrawLine(blackPen, new PointF(originPoint.X, 0), new PointF(originPoint.X - 2, 0));
        }

        private void pic_drawBarGraph(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);

            var D = Data as LabelTableData;
            if (D == null) return;

            var xLabels = D.GetXLabels();
            var yLabels = D.GetYLabels();
            var margin = 15.0f * DeviceDpi / 96;

            int barXOffset = 0;

            //X轴柱状图生成
            double[] xHeights = D.GetXHistValues();
            double maxHeightx = 0;
            foreach (var val in xHeights) maxHeightx = Math.Max(maxHeightx, Math.Abs(val));

            float histWidth = (float)(width - originPoint.X) / xHeights.Length;
            for (int i = 0; i < xHeights.Length; i++)
            {
                Brush brush = xHeights[i] >= 0 ? brushBlue : brushPurple;
                var ax = originPoint.X + histWidth * i;
				if (maxHeightx > 0) g.FillRectangle(brush, barXOffset + ax, originPoint.Y + margin, histWidth, (float)Math.Abs(xHeights[i]) / (float)maxHeightx * (height - originPoint.Y - margin));

                var text = xLabels[i];
                g.DrawString(text, font7f, brushBlack, new PointF(ax, originPoint.Y + margin), new StringFormat(StringFormatFlags.DirectionVertical));
            }

            //Y轴柱状图生成
            double[] yHeights = D.GetYHistValues();
            double maxHeighty = 0;
            foreach (var val in yHeights) maxHeighty = Math.Max(maxHeighty, Math.Abs(val));

            float histHeight = originPoint.Y / yHeights.Length;
            for (int i = 0; i < yHeights.Length; i++)
            {
                Brush brush = yHeights[i] >= 0 ? brushBlue : brushPurple;
                var ay = originPoint.Y - histHeight * (i + 1);
				if (maxHeighty > 0) g.FillRectangle(brush, barXOffset + originPoint.X - margin - (originPoint.X - margin) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), ay, (originPoint.X - margin) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), histHeight);

                var text = yLabels[i];
                var textWidth = g.MeasureString(text, font7f).Width;
                g.DrawString(text, font7f, brushBlack, new PointF(originPoint.X - textWidth - margin, ay));
            }
        }

        private void pic_drawHeatMap(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;

            var D = Data as LabelTableData;
            if (D == null) return;

            var values = D.GetValues();
            var xrange = values.GetLength(0);
            var yrange = values.GetLength(1);
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);
            var intervalX = (float)(width - originPoint.X) / xrange;
            var Ylength = originPoint.Y;
            var intervalY = originPoint.Y / yrange;
            var direction = D.GetValueDirection();
            var defaultValue = D.GetDefaultValue();

            double minimum = Double.PositiveInfinity;
            double maximum = Double.NegativeInfinity;
            foreach (var val in values)
            {
                minimum = Math.Min(minimum, val);
                maximum = Math.Max(maximum, val);
            }

            double lower = 0, upper = 0;
            bool invert = false;
            switch (direction)
            {
                case LabelTableValueDirection.Positive:
                    lower = defaultValue;
                    upper = maximum <= defaultValue ? defaultValue + 1 : maximum;
                    break;
                case LabelTableValueDirection.Negative:
                    lower = defaultValue;
                    upper = minimum >= defaultValue ? defaultValue + 1 : (2 * defaultValue - minimum);
                    invert = true;
                    break;
                case LabelTableValueDirection.Bidirectional:
                    {
                        double range = Math.Max(defaultValue - minimum, maximum - defaultValue);
                        if (range == 0) range = 1;
                        lower = defaultValue - range;
                        upper = defaultValue + range;
                    }
                    break;
            }

            for (int i = 0; i < xrange; i++)
            {
                var x = i * intervalX;
                for (int j = 0; j < yrange; j++)
                {
                    var y = (j + 1) * intervalY;
                    double val = values[i, j];
                    if (invert) val = 2 * defaultValue - val;
                    Brush brush = new SolidBrush(getColorByValue(upper, lower, val));
                    g.FillRectangle(brush, x + originPoint.X, originPoint.Y - y, intervalX, intervalY);
                }
            }
        }

        private void pic_drawGuide(object? sender, PaintEventArgs e)
        {
            if (!mouseInControl()) return;

            Graphics g = e.Graphics;

            Pen crossPen = new Pen(Color.FromArgb(255, 65, 140, 240), 1);
            crossPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            crossPen.DashPattern = [1f, 1f];

            var D = Data as LabelTableData;
            if (D == null) return;

            Point curPoint = pictureBox1.PointToClient(System.Windows.Forms.Cursor.Position);
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);
            var Xlength = width - 1 - originPoint.X;
            var Xcount = D.GetXLabelCount();
            var Ycount = D.GetYLabelCount();
            var intervalX = Xlength / Xcount;
            var Ylength = originPoint.Y;
            var intervalY = originPoint.Y / Ycount;
            var margin = 15.0f * DeviceDpi / 96;

            //格子框
            int xIndex = (int)((curPoint.X - originPoint.X) / intervalX);
            int yIndex = (int)((originPoint.Y - curPoint.Y) / intervalY);
            if (xIndex >= 0 && xIndex < Xcount && yIndex >= 0 && yIndex < Ycount)
            {
                g.DrawRectangle(bluePen, xIndex * intervalX + originPoint.X, originPoint.Y - (yIndex + 1) * intervalY, intervalX, intervalY);
            }

            //X轴矩形图虚线
            for (int i = 0; i < Xcount; i++)
            {
                var ax = originPoint.X + i * intervalX;
                var bx = originPoint.X + (i + 1) * intervalX;
                if (curPoint.Y > originPoint.Y && curPoint.Y < height - 1 && curPoint.X >= ax && curPoint.X < bx)
                {
                    g.DrawRectangle(grayPen, i * intervalX + originPoint.X, originPoint.Y + margin, intervalX, height - originPoint.Y - margin - 1);
                }
            }

            //Y轴矩形图虚线
            for (int i = 0; i < Ycount; i++)
            {
                var ay = originPoint.Y - i * intervalY;
                var by = originPoint.Y - (i + 1) * intervalY;
                if (curPoint.X < originPoint.X && curPoint.X > 0 && curPoint.Y >= by && curPoint.Y < ay)
                {
                    g.DrawRectangle(grayPen, 0, originPoint.Y - (i + 1) * intervalY, originPoint.X - margin, intervalY);
                }
            }
        }
        private void pic_drawAnnotation(object? sender, PaintEventArgs e)
        {
            if (!mouseInControl()) return;

            Graphics g = e.Graphics;

            Point curPoint = pictureBox1.PointToClient(System.Windows.Forms.Cursor.Position);
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);

            var D = Data as LabelTableData;
            if (D == null) return;

            var xLabels = D.GetXLabels();
            var yLabels = D.GetYLabels();
            var intervalX = (width - originPoint.X) / xLabels.Length;
            var intervalY = originPoint.Y / yLabels.Length;
            var values = D.GetValues();
            var isPercentage = D.GetMode() == LabelTableMode.Percentage;

            if (curPoint.X > originPoint.X && curPoint.X < width - 1 && curPoint.Y < originPoint.Y && curPoint.Y > 0)
            {
                var Xindex = (int)Math.Floor((curPoint.X - originPoint.X) / intervalX);
                var Yindex = (int)Math.Floor((originPoint.Y - curPoint.Y) / intervalY);
                var text = isPercentage ? (values[Xindex, Yindex].ToString("F1") + "%") : (Math.Abs(values[Xindex, Yindex]) >= 0.1 ? values[Xindex, Yindex].ToString("F3") : (new Decimal(values[Xindex, Yindex])).ToString());
                var fullText = "(" + xLabels[Xindex] + "," + yLabels[Yindex] + " : " + text + ")";

                var sizef = g.MeasureString(fullText, font8f);
                if (curPoint.Y <= sizef.Height)
                {
                    g.DrawString(fullText, font8f, brushBlack, new PointF(curPoint.X - sizef.Width, curPoint.Y));
                }
                else
                {
                    g.DrawString(fullText, font8f, brushBlack, new PointF(curPoint.X - sizef.Width, curPoint.Y - sizef.Height));
                }
            }

            double[] xHeights = D.GetXHistValues();
            for (int i = 0; i < xHeights.Length; i++)
            {
                var ax = originPoint.X + i * intervalX;
                var bx = originPoint.X + (i + 1) * intervalX;
                if (curPoint.Y > originPoint.Y && curPoint.X >= ax && curPoint.X < bx)
                {
                    var text = isPercentage ? (xHeights[i].ToString("F1") + "%") : (Math.Abs(xHeights[i]) >= 0.1 ? xHeights[i].ToString("F3") : (new Decimal(xHeights[i])).ToString());
                    var fullText = "(" + xLabels[i] + " : " + text + ")";

                    var sizef = g.MeasureString(fullText, font8f);
                    g.DrawString(fullText, font8f, brushBlack, new PointF(curPoint.X - sizef.Width, (float)label2.Location.Y - 62));
                }
            }

            double[] yHeights = D.GetYHistValues();
            for (int i = 0; i < yHeights.Length; i++)
            {
                var ay = originPoint.Y - i * intervalY;
                var by = originPoint.Y - (i + 1) * intervalY;
                if (curPoint.X < originPoint.X && curPoint.X > 0 && curPoint.Y >= by && curPoint.Y < ay)
                {
                    var text = isPercentage ? (yHeights[i].ToString("F1") + "%") : (Math.Abs(yHeights[i]) >= 0.1 ? yHeights[i].ToString("F3") : (new Decimal(yHeights[i])).ToString());
                    var fullText = "(" + yLabels[i] + " : " + text + ")";
                    g.DrawString(fullText, font8f, brushBlack, new PointF((float)curPoint.X + 15, (float)curPoint.Y));
                }
            }
        }

        private bool mouseInControl()
        {
            var pt = this.PointToClient(Cursor.Position);
            if (pt.X < 0 || pt.X >= this.Width || pt.Y < 0 || pt.Y >= this.Height) return false;
            else return true;
        }

        private Color getColorByValue(double upper, double lower, double value)
        {
            Color color = new Color();
            Color[] colors =
            [
                Color.FromArgb(64, 192, 32),
                Color.FromArgb(72, 184, 32),
                Color.FromArgb(80, 176, 32),
                Color.FromArgb(88, 168, 32),
                Color.FromArgb(96, 160, 32),
                Color.FromArgb(104, 152, 32),
                Color.FromArgb(112, 144, 32),
                Color.FromArgb(120, 136, 32),
                Color.FromArgb(128, 128, 32),
                Color.FromArgb(136, 120, 32),
                Color.FromArgb(144, 112, 32),
                Color.FromArgb(152, 104, 32),
                Color.FromArgb(160, 96, 32),
                Color.FromArgb(168, 88, 32),
                Color.FromArgb(176, 80, 32),
                Color.FromArgb(184, 72, 32),
                Color.FromArgb(192, 64, 32),
            ];
            if (value <= lower)
            {
                color =  colors[0];
            }
            else if (value >= upper)
            {
                color = colors[16];
            }
            else
            {
                var scale = (upper - lower) / 16;
                var index = (value - lower) / scale;
                for (int i = 0; i < colors.Length; i++)
                {
                    if (index > i && index <= i + 1)
                    {
                        var scale_red = colors[i + 1].R - colors[i].R;
                        var scale_green = colors[i + 1].G - colors[i].G;
                        var scale_blue = colors[i + 1].B - colors[i].B;
                        color = Color.FromArgb(colors[i].R + (int)(scale_red * (index - i)), colors[i].G + (int)(scale_green * (index - i)), colors[i].B + (int)(scale_blue * (index - i)));
                    }
                }
            }
            return color;
        }
    }
}
