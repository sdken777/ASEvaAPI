using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ASEva.Graph;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=2.0.0) Matrix table graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=2.0.0) 矩阵热力图数据可视化窗口
    /// </summary>
    public partial class MatrixTableGraph : BaseGraph
    {
        private Pen blackPen = new Pen(Color.Black);
        private Pen grayPen = new Pen(Color.Gray);
        private Brush brushBlack = new SolidBrush(Color.Black);
        private Brush brushRed = new SolidBrush(Color.Red);
        private Brush brushBlue = new SolidBrush(Color.DodgerBlue);
        private Brush brushPurple = new SolidBrush(Color.MediumPurple);

        private Font font7f = new Font("微软雅黑", 7f);
        private Font font8f = new Font("微软雅黑", 8f);

        private KeyValuePair<PointF, string> pointAnnotation;
        private KeyValuePair<PointF, string> barAnnotationX;
        private KeyValuePair<PointF, string> barAnnotationY;

        public MatrixTableGraph()
        {
            InitializeComponent();
        }

        public override void UpdateUIWithData()
        {
            if (Data == null || !(Data is MatrixTableData)) return;

            // 数据和验证条件显示
            if (DrawBeat.CallerBegin(pictureBox1))
            {
                pictureBox1.Refresh();
                DrawBeat.CallerEnd(pictureBox1);
            }

            if (Data.Definition.Validation == null)
            {
                label3.Text = "";
            }
            else
            {
                var vd = Data.Definition.Validation;
                if (vd is ValueBelowValidation)
                {
                    label3.Text = "≤ " + (vd as ValueBelowValidation).GetThreshold();
                }
                else if (vd is ValueAboveValidation)
                {
                    label3.Text = "≥ " + (vd as ValueAboveValidation).GetThreshold();
                }
                else
                {
                    label3.Text = "";
                }
            }

            // 标题显示
            label1.Text = Data == null ? "" : Data.Definition.MainTitle;
            if (!Data.HasData())
            {
                label2.ForeColor = Color.Black;
                label2.Text = "No Data.";
                return;
            }

            // 验证结果显示
            double? percentage = null;
            var vdResult = Data.Validate(out percentage);
            if (vdResult == null)
            {
                label2.ForeColor = Color.Black;
                label2.Text = percentage == null ? "" : (getPercentageText(percentage.Value) + "% OK");
            }
            else if (vdResult.Value)
            {
                label2.ForeColor = Color.Green;
                label2.Text = percentage == null ? "OK" : (getPercentageText(percentage.Value) + "% OK");
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Text = percentage == null ? "NG" : (getPercentageText(percentage.Value) + "% OK");
            }
        }

        private String getPercentageText(double percentage)
        {
            return percentage >= 100 ? percentage.ToString("F0") : percentage.ToString("F1");
        }

        private void pic_drawAxis(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);

            //画坐标轴
            PointF pointx1 = new PointF(0, originPoint.Y);
            PointF pointx2 = new PointF(width, originPoint.Y);
            PointF pointy1 = new PointF(originPoint.X, 0);
            PointF pointy2 = new PointF(originPoint.X, height);
            g.DrawLine(blackPen, pointx1, pointx2);
            g.DrawLine(blackPen, pointy1, pointy2);
            g.DrawLine(grayPen, new PointF(originPoint.X, originPoint.Y + 15), new PointF(width - 1, originPoint.Y + 15));
            g.DrawLine(grayPen, new PointF(originPoint.X - 15, originPoint.Y), new PointF(originPoint.X - 15, 0));
            String xTitle = null;
            String yTitle = null;
            var D = Data as MatrixTableData;
            xTitle = D.GetXTitle();
            yTitle = D.GetYTitle();
            var xTitleWidth = g.MeasureString(xTitle, font7f).Width;
            var yTitleWidth = g.MeasureString(yTitle, font7f).Width;
            PointF xTitlePoint = new PointF((width + originPoint.X - xTitleWidth) / 2, originPoint.Y);
            PointF yTitlePoint = new PointF(originPoint.X - 15, (originPoint.Y - yTitleWidth) / 2);
            g.DrawString(xTitle, font7f, brushBlue, xTitlePoint);
            g.DrawString(yTitle, font7f, brushBlue, yTitlePoint, new StringFormat(StringFormatFlags.DirectionVertical));

            //画坐标
            var xrange = D.GetXRange();
            var yrange = D.GetYRange();

            var intervalX = (width - 1 - originPoint.X) /xrange.Count;
            var xString = (new Decimal(xrange.Base + xrange.Step * xrange.Count)).ToString();
            g.DrawLine(blackPen, new PointF(intervalX* xrange.Count+originPoint.X, originPoint.Y), new PointF(intervalX * xrange.Count + originPoint.X, originPoint.Y+2));
            g.DrawString(xString, font7f, brushBlack, new PointF(intervalX * xrange.Count + originPoint.X - g.MeasureString(xString, font7f).Width, originPoint.Y));
            g.DrawString((new Decimal(xrange.Base)).ToString(), font7f, brushBlack, new PointF(originPoint.X, originPoint.Y));

            var intervalY = originPoint.Y / yrange.Count;
            var yString = (new Decimal(yrange.Base + yrange.Step * yrange.Count)).ToString();
            g.DrawLine(blackPen, new PointF(originPoint.X, originPoint.Y - intervalY * yrange.Count), new PointF(originPoint.X-2,originPoint.Y - intervalY * yrange.Count));
            g.DrawString(yString, font7f, brushBlack, new PointF(originPoint.X - 15, 0), new StringFormat(StringFormatFlags.DirectionVertical));
            g.DrawString((new Decimal(yrange.Base)).ToString(), font7f, brushBlack, new PointF(originPoint.X - 15, originPoint.Y - g.MeasureString(yrange.Base.ToString(), font7f).Width), new StringFormat(StringFormatFlags.DirectionVertical));
        }

        private void pic_drawBarGraph(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);
            var D = Data as MatrixTableData;

            int barXOffset = 0;

            //X轴柱状图生成
            double[] xHeights = D.GetXHistValues();
            double maxHeightx = 0;
            foreach (var val in xHeights) maxHeightx = Math.Max(maxHeightx, Math.Abs(val));
            if (maxHeightx > 0)
            {
                float histWidth = (float)(width - originPoint.X) / xHeights.Length;
                for (int i = 0; i < xHeights.Length; i++)
                {
                    Brush brush = xHeights[i] >= 0 ? brushBlue : brushPurple;
					g.FillRectangle(brush, barXOffset + i * histWidth + originPoint.X, originPoint.Y + 15, histWidth, (float)Math.Abs(xHeights[i]) / (float)maxHeightx * (height - originPoint.Y - 15));
                }
            }

            //Y轴柱状图生成
            double[] yHeights = D.GetYHistValues();
            double maxHeighty = 0;
            foreach (var val in yHeights) maxHeighty = Math.Max(maxHeighty, Math.Abs(val));
            if (maxHeighty > 0)
            {
                float histHeight = originPoint.Y / yHeights.Length;
                for (int i = 0; i < yHeights.Length; i++)
                {
                    Brush brush = yHeights[i] >= 0 ? brushBlue : brushPurple;
					g.FillRectangle(brush, barXOffset + originPoint.X - 15 - (originPoint.X - 15) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), originPoint.Y - (i + 1) * histHeight, (originPoint.X - 15) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), histHeight);
                }
            }
        }

        private void pic_drawHeatMap(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            var D = Data as MatrixTableData;
            var values = D.GetValues();
            var xrange = D.GetXRange();
            var yrange = D.GetYRange();
            var valueRange = D.GetValueRefRange();
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);
            var Xlength = width - 1 - originPoint.X;
            var intervalX = Xlength / xrange.Count;
            var Ylength = originPoint.Y;
            var intervalY = originPoint.Y / yrange.Count;
            for (int i = 0; i < xrange.Count; i++)
            {
                var x = i * intervalX;
                for (int j = 0; j < yrange.Count; j++)
                {
                    var y = (j + 1) * intervalY;
                    Brush brush = new SolidBrush(getColorByValue(valueRange.upper, valueRange.lower, values[i,j]));
                    g.FillRectangle(brush, x + originPoint.X, originPoint.Y - y, intervalX, intervalY);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HandleGraphSelected();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawBeat.CallbackBegin(pictureBox1, "ASEva.UICoreWF.MatrixTableGraph");

            try
            {
                pic_drawAxis(sender, e);
                pic_drawBarGraph(sender, e);
                pic_drawHeatMap(sender, e);
                pic_drawValidation(sender, e);
                pic_drawAnnotation(sender, e);
                pic_drawGuide(sender, e);
            }
            catch (Exception) { }

            DrawBeat.CallbackEnd(pictureBox1);
        }

        private void pic_drawValidation(object sender, PaintEventArgs e)
        {
            if (Data.Definition.Validation == null) return;

            Color color;
            FloatPoint[] genericOutline;
            if (Data.Definition.Validation is OutlineInsideValidation)
            {
                var vd = Data.Definition.Validation as OutlineInsideValidation;
                color = Color.LimeGreen;
                genericOutline = vd.GetOutline();
            }
            else if (Data.Definition.Validation is OutlineOutsideValidation)
            {
                var vd = Data.Definition.Validation as OutlineOutsideValidation;
                color = Color.Red;
                genericOutline = vd.GetOutline();
            }
            else return;

            var outline = new PointF[genericOutline.Length];
            for (int i = 0; i < outline.Length; i++) outline[i] = new PointF(genericOutline[i].X, genericOutline[i].Y);

            var xRange = (Data as MatrixTableData).GetXRange();
            var yRange = (Data as MatrixTableData).GetYRange();

            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            var originPoint = new PointF((float)width / 4, (float)height / 3 * 2);

            for (int i = 0; i < outline.Length; i++)
            {
                var x = (float)((outline[i].X - xRange.Base) / (xRange.Step * xRange.Count) * ((float)width - originPoint.X) + originPoint.X);
                var y = (float)((yRange.Base + yRange.Step * yRange.Count - outline[i].Y) / (yRange.Step * yRange.Count) * originPoint.Y);
                outline[i] = new PointF(x, y);
            }

            var originMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var pen = new Pen(color, 2.0f);
            e.Graphics.DrawPolygon(pen, outline);

            e.Graphics.SmoothingMode = originMode;
        }

        private void pic_drawGuide(object sender, PaintEventArgs e)
        {
            if (!mouseInControl()) return;

            Graphics g = e.Graphics;

            Pen crossPen = new Pen(Color.FromArgb(255, 65, 140, 240), 1);
            crossPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            crossPen.DashPattern = new float[] { 1f, 1f };

            var D = Data as MatrixTableData;
            Point curPoint = pictureBox1.PointToClient(System.Windows.Forms.Cursor.Position);
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);

            //十字虚线
            if (curPoint.X > originPoint.X && curPoint.X < width - 1 && curPoint.Y < originPoint.Y && curPoint.Y > 0)
            {
                g.DrawLine(crossPen, new PointF(originPoint.X, curPoint.Y), new PointF(width - 1, curPoint.Y));
                g.DrawLine(crossPen, new PointF(curPoint.X, 0), new PointF(curPoint.X, originPoint.Y));
            }

            //X轴矩形图虚线
            var Xcount = D.GetXRange().Count;
            float histWidth = (float)(width - originPoint.X) / Xcount;
            for (int i = 0; i < Xcount; i++)
            {
                var ax = originPoint.X + i * histWidth;
                var bx = originPoint.X + (i + 1) * histWidth;
                if (curPoint.Y > originPoint.Y && curPoint.Y < height - 1 && curPoint.X >= ax && curPoint.X < bx)
                {
                    g.DrawRectangle(grayPen, ax, originPoint.Y + 15, histWidth - (i == Xcount - 1 ? 1 : 0), height - originPoint.Y - 15 - 1);
                }
            }

            //Y轴矩形图虚线
            var Ycount = D.GetYRange().Count;
            float histHeight = originPoint.Y / Ycount;
            for (int i = 0; i < Ycount; i++)
            {
                var ay = originPoint.Y - i * histHeight;
                var by = originPoint.Y - (i + 1) * histHeight;
                if (curPoint.X < originPoint.X && curPoint.X > 0 && curPoint.Y >= by && curPoint.Y < ay)
                {
                    g.DrawRectangle(grayPen, 0, by, originPoint.X - 15, histHeight);
                }
            }
        }
        private void pic_drawAnnotation(object sender, PaintEventArgs e)
        {
            if (!mouseInControl()) return;

            Point curPoint = pictureBox1.PointToClient(System.Windows.Forms.Cursor.Position);
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);
            var D = Data as MatrixTableData;
            var xrange = D.GetXRange();
            var yrange = D.GetYRange();
            var xRangeLow = D.GetXRange().Base;
            var xRangeUpper = xRangeLow + D.GetXRange().Count * D.GetXRange().Step;
            var yRangeLow = D.GetYRange().Base;
            var yRangeUpper = yRangeLow + D.GetYRange().Count * D.GetYRange().Step;
            var Xlength = width - 1 - originPoint.X;
            var intervalX = Xlength / xrange.Count;
            var Ylength = originPoint.Y;
            var intervalY = originPoint.Y / yrange.Count;
            var values = D.GetValues();
            var isPercentage = D.GetMode() == MatrixTableMode.Percentage;

            if (curPoint.X > originPoint.X && curPoint.X < width - 1 && curPoint.Y < originPoint.Y && curPoint.Y > 0)
            {
                var Xindex = (int)Math.Floor((curPoint.X - originPoint.X) / intervalX);
                var Yindex = (int)Math.Floor((originPoint.Y - curPoint.Y) / intervalY);
                var text = isPercentage ? (values[Xindex, Yindex].ToString("F1") + "%") : (Math.Abs(values[Xindex, Yindex]) >= 0.1 ? values[Xindex, Yindex].ToString("F3") : (new Decimal(values[Xindex, Yindex])).ToString());
                pointAnnotation = new KeyValuePair<PointF, string>(curPoint, "(" + Math.Round((xRangeUpper - xRangeLow) * (curPoint.X - originPoint.X) / (width - 1 - originPoint.X) + xRangeLow, 3) + "," + Math.Round((yRangeUpper - yRangeLow) * (originPoint.Y - curPoint.Y) / originPoint.Y + yRangeLow, 3) + " : " + text + ")");
            }

            double[] xHeights = D.GetXHistValues();
            for (int i = 0; i < xHeights.Length; i++)
            {
                var a = i * xrange.Step + xrange.Base;
                var b = (i + 1) * xrange.Step + xrange.Base;
                var ax = originPoint.X + i * intervalX;
                var bx = originPoint.X + (i + 1) * intervalX;
                if (curPoint.Y > originPoint.Y && curPoint.X >= ax && curPoint.X < bx)
                {
                    var text = isPercentage ? (xHeights[i].ToString("F1") + "%") : (Math.Abs(xHeights[i]) >= 0.1 ? xHeights[i].ToString("F3") : (new Decimal(xHeights[i])).ToString());
                    barAnnotationX = new KeyValuePair<PointF, string>(new PointF(curPoint.X, curPoint.Y), "(" + (new Decimal(a)).ToString() + "~" + (new Decimal(b)).ToString() + " : " + text + ")");
                }
            }

            double[] yHeights = D.GetYHistValues();
            for (int i = 0; i < yHeights.Length; i++)
            {
                var a = i * yrange.Step + yrange.Base;
                var b = (i + 1) * yrange.Step + yrange.Base;
                var ay = originPoint.Y - i * intervalY;
                var by = originPoint.Y - (i + 1) * intervalY;
                if (curPoint.X < originPoint.X && curPoint.X > 0 && curPoint.Y >= by && curPoint.Y < ay)
                {
                    var text = isPercentage ? (yHeights[i].ToString("F1") + "%") : (Math.Abs(yHeights[i]) >= 0.1 ? yHeights[i].ToString("F3") : (new Decimal(yHeights[i])).ToString());
                    barAnnotationY = new KeyValuePair<PointF, string>(new PointF(curPoint.X, curPoint.Y), "(" + (new Decimal(a)).ToString() + "~" + (new Decimal(b)).ToString() + " : " + text + ")");
                }
            }

            Graphics g = e.Graphics;

			var sizef = g.MeasureString(pointAnnotation.Value, font8f);
			if (pointAnnotation.Key.Y <= sizef.Height)
			{
				g.DrawString(pointAnnotation.Value, font8f, brushRed, new PointF(pointAnnotation.Key.X - sizef.Width, pointAnnotation.Key.Y));
			}
			else
			{
				g.DrawString(pointAnnotation.Value, font8f, brushRed, new PointF(pointAnnotation.Key.X - sizef.Width, pointAnnotation.Key.Y - sizef.Height));
			}
            pointAnnotation = new KeyValuePair<PointF, string>();

			sizef = g.MeasureString(barAnnotationX.Value, font8f);
			g.DrawString(barAnnotationX.Value, font8f, brushRed, new PointF(barAnnotationX.Key.X - sizef.Width, label2.Location.Y - 50));
            barAnnotationX = new KeyValuePair<PointF, string>();

            g.DrawString(barAnnotationY.Value, font8f, brushRed, new PointF(barAnnotationY.Key.X + 15, barAnnotationY.Key.Y));
            barAnnotationY = new KeyValuePair<PointF, string>();
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
            Color[] colors = new Color[17];
            colors[0] = Color.FromArgb(0, 0, 128);
            colors[1] = Color.FromArgb(0, 0, 192);
            colors[2] = Color.FromArgb(0, 0, 255);
            colors[3] = Color.FromArgb(0, 64, 255);
            colors[4] = Color.FromArgb(0, 128, 255);
            colors[5] = Color.FromArgb(0, 192, 255);
            colors[6] = Color.FromArgb(0, 255, 255);
            colors[7] = Color.FromArgb(64, 255, 192);
            colors[8] = Color.FromArgb(128, 255, 128);
            colors[9] = Color.FromArgb(192, 255, 64);
            colors[10] = Color.FromArgb(255, 255, 0);
            colors[11] = Color.FromArgb(255, 192, 0);
            colors[12] = Color.FromArgb(255, 128, 0);
            colors[13] = Color.FromArgb(255, 64, 0);
            colors[14] = Color.FromArgb(255, 0, 0);
            colors[15] = Color.FromArgb(192, 0, 0);
            colors[16] = Color.FromArgb(128, 0, 0);

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
