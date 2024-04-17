using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ASEva.Graph;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=3.0.0) Scatter points graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.0.0) 散点图数据可视化窗口
    /// </summary>
    public partial class ScatterPointsGraph : BaseGraph
    {
        private Pen blackPen = new Pen(Color.Black);
        private Pen grayPen = new Pen(Color.Gray);
        private Brush brushBlack = new SolidBrush(Color.Black);
        private Brush brushPurple = new SolidBrush(Color.MediumPurple);
        private Brush brushBlue = new SolidBrush(Color.DodgerBlue);

        private Font font7f = new Font("微软雅黑", 7f);
        private Font font8f = new Font("微软雅黑", 8f);

        private int mouseAtPointIndex = -1;

        private bool chinese = false;

        public ScatterPointsGraph()
        {
            InitializeComponent();

            chinese = Agency.GetAppLanguage() == Language.Chinese;
        }

        public override void UpdateUIWithData()
        {
            if (Data == null || !(Data is ScatterPointsData)) return;

            // 数据和验证条件显示
            if (DrawBeat.CallerBegin(pictureBox2))
            {
                pictureBox2.Refresh();
                DrawBeat.CallerEnd(pictureBox2);
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

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            DrawBeat.CallbackBegin(pictureBox2, "ASEva.UICoreWF.ScatterPointsGraph");

            try
            {
                pic_drawPoint(sender, e);
                pic_drawBarGraph(sender, e);
                pic_drawAxis(sender, e);
                pic_drawValidation(sender, e);
                pic_drawAnnotation(sender, e);
                pic_drawGuide(sender, e);
            }
            catch (Exception) { }

            DrawBeat.CallbackEnd(pictureBox2);
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

            var xRange = (Data as ScatterPointsData).GetXRange();
            var yRange = (Data as ScatterPointsData).GetYRange();

            var width = pictureBox2.Width;
            var height = pictureBox2.Height;
            var originPoint = new PointF((float)width / 4, (float)height / 3 * 2);

            for (int i = 0; i < outline.Length; i++)
            {
                var x = (float)((outline[i].X - xRange.lower) / (xRange.upper - xRange.lower) * ((float)width - originPoint.X) + originPoint.X);
                var y = (float)((yRange.upper - outline[i].Y) / (yRange.upper - yRange.lower) * originPoint.Y);
                outline[i] = new PointF(x, y);
            }

            var originMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var pen = new Pen(color, 2.0f);
            e.Graphics.DrawPolygon(pen, outline);

            e.Graphics.SmoothingMode = originMode;
        }

        private void pic_drawAnnotation(object sender, PaintEventArgs e)
        {
            if (!mouseInControl()) return;

            Point curPoint = pictureBox2.PointToClient(System.Windows.Forms.Cursor.Position);
            var width = pictureBox2.Width;
            var height = pictureBox2.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);
            var D = Data as ScatterPointsData;
            var xRangeLow = D.GetXRange().lower;
            var xRangeUpper = D.GetXRange().upper;
            var yRangeLow = D.GetYRange().lower;
            var yRangeUpper = D.GetYRange().upper;

            var g = e.Graphics;

            if (curPoint.X > originPoint.X && curPoint.X < width - 1 && curPoint.Y < originPoint.Y && curPoint.Y > 0)
            {
                var firstText = "(" + Math.Round((xRangeUpper - xRangeLow) * (curPoint.X - originPoint.X) / (width - 1 - originPoint.X) + xRangeLow, 3) + "," + Math.Round((yRangeUpper - yRangeLow) * (originPoint.Y - curPoint.Y) / originPoint.Y + yRangeLow, 3) + ")";
                String secondText = "";
                if (mouseAtPointIndex >= 0)
                {
                    DateTime session = DateTime.Now;
                    double offset = 0;
                    if (D.GetPointTimeInfo(mouseAtPointIndex, ref session, ref offset))
                    {
                        secondText = session.ToString(chinese ? "yyyy/MM/dd HH:mm:ss" : "MM/dd/yyyy HH:mm:ss") + " + " + offset.ToString("F3");
                    }
                }

                var firstTextSize = g.MeasureString(firstText, font8f);
                var secondTextSize = g.MeasureString(secondText, font7f);
                if (curPoint.X - originPoint.X <= Math.Max(firstTextSize.Width, secondTextSize.Width))
                {
                    if (curPoint.Y <= firstTextSize.Height + secondTextSize.Height)
                    {
                        g.DrawString(firstText, font8f, brushBlack, new PointF(curPoint.X, curPoint.Y));
                        if (secondText.Length > 0) g.DrawString(secondText, font7f, brushBlack, new PointF(curPoint.X, curPoint.Y + firstTextSize.Height));
                    }
                    else
                    {
                        g.DrawString(firstText, font8f, brushBlack, new PointF(curPoint.X, curPoint.Y - firstTextSize.Height));
                        if (secondText.Length > 0) g.DrawString(secondText, font7f, brushBlack, new PointF(curPoint.X, curPoint.Y - firstTextSize.Height - secondTextSize.Height));
                    }
                }
                else
                {
                    if (curPoint.Y <= firstTextSize.Height + secondTextSize.Height)
                    {
                        g.DrawString(firstText, font8f, brushBlack, new PointF(curPoint.X - firstTextSize.Width, curPoint.Y));
                        if (secondText.Length > 0) g.DrawString(secondText, font7f, brushBlack, new PointF(curPoint.X - secondTextSize.Width, curPoint.Y + firstTextSize.Height));
                    }
                    else
                    {
                        g.DrawString(firstText, font8f, brushBlack, new PointF(curPoint.X - firstTextSize.Width, curPoint.Y - firstTextSize.Height));
                        if (secondText.Length > 0) g.DrawString(secondText, font7f, brushBlack, new PointF(curPoint.X - secondTextSize.Width, curPoint.Y - firstTextSize.Height - secondTextSize.Height));
                    }
                }
            }

            int[] xHeights = D.GetXHistValues();
            float histWidth = (float)(width - originPoint.X) / xHeights.Length;
            var sumX = xHeights.Sum();

            KeyValuePair<PointF, string>? barAnnotationX = null;
            for (int i = 0; i < xHeights.Length; i++)
            {
                var percent = 0.0;
                if (sumX != 0) percent = (double)xHeights[i] / sumX * 100;
                var ax = originPoint.X + histWidth * i;
                var bx = originPoint.X + histWidth * (i + 1);
                if (curPoint.Y > originPoint.Y && curPoint.X >= ax && curPoint.X <= bx)
                {
                    barAnnotationX = new KeyValuePair<PointF, string>(new PointF(curPoint.X, curPoint.Y), "(" + xHeights[i] + " : " + percent.ToString("F1") + "%)");
                }
            }
            if (barAnnotationX != null)
            {
                var sizef = g.MeasureString(barAnnotationX.Value.Value, font8f);
                g.DrawString(barAnnotationX.Value.Value, font8f, brushBlack, new PointF(barAnnotationX.Value.Key.X - sizef.Width, label2.Location.Y - 50));
            }

            int[] YHeights = D.GetYHistValues();
            float histHeight = originPoint.Y / YHeights.Length;
            var sumY = YHeights.Sum();

            KeyValuePair<PointF, string>? barAnnotationY = null;
            for (int i = 0; i < YHeights.Length; i++)
            {
                var percent = 0.0;
                if (sumY != 0) percent = (double)YHeights[i] / sumY * 100;
                var ay = originPoint.Y - histHeight * (i + 1);
                var by = originPoint.Y - histHeight * i;
                if (curPoint.X < originPoint.X && curPoint.X > 0 && curPoint.Y >= ay && curPoint.Y <= by)
                {
                    barAnnotationY = new KeyValuePair<PointF, string>(new PointF(curPoint.X, curPoint.Y), "(" + YHeights[i] + " : " + percent.ToString("F1") + "%)");
                }
            }
            if (barAnnotationY != null)
            {
                g.DrawString(barAnnotationY.Value.Value, font8f, brushBlack, new PointF(barAnnotationY.Value.Key.X + 15, barAnnotationY.Value.Key.Y));
            }
        }

        private void pic_drawAxis(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var width = pictureBox2.Width;
            var height = pictureBox2.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);

            //画坐标轴
            PointF pointx1 = new PointF(0, originPoint.Y);
            PointF pointx2 = new PointF(width, originPoint.Y);
            PointF pointy1 = new PointF(originPoint.X, 0);
            PointF pointy2 = new PointF(originPoint.X, height);
            g.DrawLine(blackPen, pointx1, pointx2);
            g.DrawLine(blackPen, pointy1, pointy2);
            g.DrawLine(grayPen, new PointF(originPoint.X, originPoint.Y + 15),new PointF(width-1, originPoint.Y + 15));
            g.DrawLine(grayPen, new PointF(originPoint.X-15, originPoint.Y), new PointF(originPoint.X - 15,0));
            String xTitle = null;
            String yTitle = null;
            var D = Data as ScatterPointsData;
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
            g.DrawLine(blackPen, new PointF(originPoint.X, 0), new PointF(originPoint.X - 2, 0));
            g.DrawLine(blackPen, new PointF(width-1, originPoint.Y), new PointF(width-1, originPoint.Y+2));
            g.DrawString((new Decimal(xrange.upper)).ToString(), font7f, brushBlack, new PointF(width-20, originPoint.Y));
            g.DrawString((new Decimal(xrange.lower)).ToString(), font7f, brushBlack, new PointF(originPoint.X, originPoint.Y));
            g.DrawString((new Decimal(yrange.upper)).ToString(), font7f, brushBlack, new PointF(originPoint.X - 15, 0), new StringFormat(StringFormatFlags.DirectionVertical));
            g.DrawString((new Decimal(yrange.lower)).ToString(), font7f, brushBlack, new PointF(originPoint.X - 15, originPoint.Y - g.MeasureString(yrange.lower.ToString(), font7f).Width), new StringFormat(StringFormatFlags.DirectionVertical));
        }

        private void pic_drawPoint(object sender, PaintEventArgs e)
        {
            var originMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Graphics g = e.Graphics;
            var D = Data as ScatterPointsData;
            var points = D.GetPoints();
            processPoint(points);

            var mouse = pictureBox2.PointToClient(Cursor.Position);
            mouseAtPointIndex = -1;
            for (int i = 0; i < points.Length; i++)
            {
                var point = points[i];
                g.FillEllipse(brushPurple, (float)(point.X - 1.75f), (float)(point.Y - 1.5f), 3, 3);
                if (Math.Abs(Math.Round(point.X) - mouse.X) <= 1 && Math.Abs(Math.Round(point.Y) - mouse.Y) <= 1)
                {
                    mouseAtPointIndex = i;
                }
            }

            e.Graphics.SmoothingMode = originMode;
        }

        private void pic_drawBarGraph(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            var width = pictureBox2.Width;
            var height = pictureBox2.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);
            var D = Data as ScatterPointsData;

            var xRangeLow = D.GetXRange().lower;
            var xRangeUpper = D.GetXRange().upper;
            var yRangeLow = D.GetYRange().lower;
            var yRangeUpper = D.GetYRange().upper;

            int barXOffset = 0;

            //X轴柱状图生成
            int[] xHeights = D.GetXHistValues();
            double xStep = (xRangeUpper - xRangeLow) / xHeights.Length;
            var maxHeightx = xHeights.Max();
            float histWidth = (float)(width - originPoint.X) / xHeights.Length;
            for (int i = 0; i < xHeights.Length; i++)
            {
                var ax = originPoint.X + histWidth * i;
				g.FillRectangle(brushBlue, ax + barXOffset, originPoint.Y + 1 + 15, histWidth, (float)xHeights[i] / maxHeightx * (height - originPoint.Y - 1 - 15));

                if (histWidth > 12)
                {
                    var a = i * xStep + xRangeLow;
                    var b = (i + 1) * xStep + xRangeLow;
                    var text = (new Decimal(a)).ToString() + " " + (new Decimal(b)).ToString();
                    g.DrawString(text, font7f, brushBlack, new PointF(ax, originPoint.Y + 15), new StringFormat(StringFormatFlags.DirectionVertical));
                }
            }

            //Y轴柱状图生成
            int[] YHeights = D.GetYHistValues();
            double yStep = (yRangeUpper - yRangeLow) / YHeights.Length;
            var maxHeighty = YHeights.Max();
            float histHeight = originPoint.Y / YHeights.Length;
            for (int i = 0; i < YHeights.Length; i++)
            {
                var ay = originPoint.Y - histHeight * (i + 1);
				g.FillRectangle(brushBlue, originPoint.X - ((float)YHeights[i] / maxHeighty * (originPoint.X - 1 - 15)) - 15 + barXOffset, ay, (float)YHeights[i] / maxHeighty * (originPoint.X - 1 - 15) - 1, histHeight);

                if (histHeight > 12)
                {
                    var a = i * yStep + yRangeLow;
                    var b = (i + 1) * yStep + yRangeLow;
                    var text = (new Decimal(a)).ToString() + " " + (new Decimal(b)).ToString();
                    var textWidth = g.MeasureString(text, font7f).Width;
                    g.DrawString(text, font7f, brushBlack, new PointF(originPoint.X - textWidth - 15, ay));
                }
            }
        }

        private void pic_drawGuide(object sender, PaintEventArgs e)
        {
            if (!mouseInControl()) return;

            Graphics g = e.Graphics;

            Pen crossPen = new Pen(Color.FromArgb(255, 65, 140, 240), 1);
            crossPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            crossPen.DashPattern = new float[] { 1f, 1f };

            var D = Data as ScatterPointsData;
            Point curPoint = pictureBox2.PointToClient(System.Windows.Forms.Cursor.Position);
            var width = pictureBox2.Width;
            var height = pictureBox2.Height;
            PointF originPoint = new PointF((float)width / 4, (float)height / 3 * 2);

            //十字虚线
            if (curPoint.X > originPoint.X && curPoint.X < width - 1 && curPoint.Y < originPoint.Y && curPoint.Y > 0)
            {
                g.DrawLine(crossPen, new PointF(originPoint.X, curPoint.Y), new PointF(width - 1, curPoint.Y));
                g.DrawLine(crossPen, new PointF(curPoint.X, 0), new PointF(curPoint.X, originPoint.Y));
            }

            //X轴矩形图虚线
            var Xcount = D.GetXHistCount();
            var histWidth = (float)(width - originPoint.X) / Xcount;
            for (int i = 0; i < Xcount; i++)
            {
                var ax = originPoint.X + histWidth * i;
                var bx = originPoint.X + histWidth * (i + 1);
                if (curPoint.Y > originPoint.Y && curPoint.Y < height - 1 && curPoint.X >= ax && curPoint.X <= bx)
                {
                    g.DrawRectangle(grayPen, ax, originPoint.Y + 15, histWidth - (i == Xcount - 1 ? 1 : 0), height - originPoint.Y - 15 - 1);
                }
            }

            //Y轴矩形图虚线
            var Ycount = D.GetYHistCount();
            var histHeight = originPoint.Y / Ycount;
            for (int i = 0; i < Ycount; i++)
            {
                var ay = originPoint.Y - histHeight * (i + 1);
                var by = originPoint.Y - histHeight * i;
                if (curPoint.X < originPoint.X && curPoint.X > 0 && curPoint.Y >= ay && curPoint.Y <= by)
                {
                    g.DrawRectangle(grayPen, 0, ay, originPoint.X - 15, histHeight);
                }
            }
        }

        private void processPoint(FloatPoint[] points)
        {
            var width = pictureBox2.Width;
            var height = pictureBox2.Height;
            var originPoint = new FloatPoint((float)width / 4, (float)height / 3 * 2);
            var D = Data as ScatterPointsData;
            ScatterRange xRange = D.GetXRange();
            ScatterRange yRange = D.GetYRange();

            for (var i = 0; i < points.Length; i++)
            {
                points[i].X = originPoint.X + (float)((width - originPoint.X) * ((points[i].X-xRange.lower) / (xRange.upper - xRange.lower)));
                points[i].Y = originPoint.Y - (float)(originPoint.Y * ((points[i].Y - yRange.lower) / (yRange.upper - yRange.lower)));
            }               
        }

        private bool mouseInControl()
        {
            var pt = this.PointToClient(Cursor.Position);
            if (pt.X < 0 || pt.X >= this.Width || pt.Y < 0 || pt.Y >= this.Height) return false;
            else return true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            HandleGraphSelected();
        }
    }
}
