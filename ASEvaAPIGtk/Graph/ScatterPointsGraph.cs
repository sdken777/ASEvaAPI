using System;
using System.Linq;
using Gtk;
using ASEva.Utility;
using ASEva.UIGtk;
using ASEva.Graph;
using UI = Gtk.Builder.ObjectAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649

    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Scatter points graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 散点图数据可视化窗口
    /// </summary>
    public class ScatterPointsGraph : BaseGraph
    {
        [UI] Label labelTitle, labelValidation;
        [UI] Overlay overlay;
        [UI] DrawingArea draw;
        [UI] EventBox eventBox;

        EventBoxHelper eventBoxHelper = new EventBoxHelper();
        DrawSwap drawSwap;
        bool chinese = false;

        public ScatterPointsGraph() : this(new Builder("ScatterPointsGraph.glade"))
        {
            this.SetBackColor(ColorRGBA.White);

            overlay.AddOverlay(labelValidation);
            eventBoxHelper.Add(eventBox);
            drawSwap = new DrawSwap(draw, "ASEva.UIGtk.ScatterPointsGraph");

            chinese = Agency.GetAppLanguage() == Language.Chinese;

            eventBoxHelper.LeftDown += eventBox_LeftDown;
            drawSwap.Paint += draw_Paint;
        }

        private ScatterPointsGraph(Builder builder) : base(builder.GetRawOwnedObject("ScatterPointsGraph"))
        {
            builder.Autoconnect(this);
        }

        /// \~English
        /// <summary>
        /// Release resources
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 释放相关资源
        /// </summary>
        public override void Close()
		{
			drawSwap.Close();
		}

        public override void UpdateUIWithData()
        {
            if (Data == null || !(Data is ScatterPointsData)) return;

            // 数据和验证条件显示
            drawSwap.Refresh();

            // 标题显示
            labelTitle.Text = Data == null ? "" : Data.Definition.MainTitle;
            if (!Data.HasData())
            {
                labelValidation.SetForeColor(ColorRGBA.Black);
                labelValidation.Text = "No Data.";
                return;
            }

            // 验证结果显示
            double? percentage = null;
            var vdResult = Data.Validate(out percentage);
            if (vdResult == null)
            {
                labelValidation.SetForeColor(ColorRGBA.Black);
                labelValidation.Text = percentage == null ? "" : (getPercentageText(percentage.Value) + "% OK");
            }
            else if (vdResult.Value)
            {
                labelValidation.SetForeColor(ColorRGBA.Green);
                labelValidation.Text = percentage == null ? "OK" : (getPercentageText(percentage.Value) + "% OK");
            }
            else
            {
                labelValidation.SetForeColor(ColorRGBA.Red);
                labelValidation.Text = percentage == null ? "NG" : (getPercentageText(percentage.Value) + "% OK");
            }
        }

        private String getPercentageText(double percentage)
        {
            return percentage >= 100 ? percentage.ToString("F0") : percentage.ToString("F1");
        }

        private void eventBox_LeftDown(EventBox box, Gdk.EventButton ev)
        {
            HandleGraphSelected();
        }

        private void draw_Paint(DrawSwap swap, Cairo.Context cc)
        {
            try
            {
                cc.LineWidth = 1;
                cc.SelectFontFace(CairoContextExtension.NotoFontName, Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);

                cc.Translate(0, 2);

                var width = draw.AllocatedWidth;
                var height = draw.AllocatedHeight - 2;
                var originPoint = new FloatPoint((float)width / 4, (float)height / 3 * 2);

                var D = Data as ScatterPointsData;
                var points = D.GetPoints();
                var xRange = D.GetXRange();
                var yRange = D.GetYRange();
                var xHeights = D.GetXHistValues();
                var yHeights = D.GetYHistValues();
                var xTitle = D.GetXTitle();
                var yTitle = D.GetYTitle();
                var xCount = D.GetXHistCount();
                var yCount = D.GetYHistCount();

                var xRangeLower = xRange.lower;
                var xRangeUpper = xRange.upper;
                var yRangeLower = yRange.lower;
                var yRangeUpper = yRange.upper;
                var xStep = (xRangeUpper - xRangeLower) / xCount;
                var yStep = (yRangeUpper - yRangeLower) / yCount;
                var intervalX = (width - originPoint.X) / xCount;
                var intervalY = originPoint.Y / yCount;
                var xSum = xHeights.Sum();
                var ySum = yHeights.Sum();

                var mouse = draw.GetPointer();

                // 画点
                for (var i = 0; i < points.Length; i++)
                {
                    points[i].X = originPoint.X + (float)((width - originPoint.X) * ((points[i].X-xRange.lower) / (xRange.upper - xRange.lower)));
                    points[i].Y = originPoint.Y - (float)(originPoint.Y * ((points[i].Y - yRange.lower) / (yRange.upper - yRange.lower)));
                }  

                cc.SetSourceColor(ColorRGBA.MediumPurple);

                var mouseAtPointIndex = -1;
                for (int i = 0; i < points.Length; i++)
                {
                    var point = points[i];
                    cc.FillCircle(point, 1.5);
                    if (Math.Abs(Math.Round(point.X) - mouse.X) <= 1 && Math.Abs(Math.Round(point.Y) - mouse.Y) <= 1)
                    {
                        mouseAtPointIndex = i;
                    }
                }

                // X轴柱状图生成
                cc.SetFontSize(10);

                var maxHeightx = xHeights.Max();
                for (int i = 0; i < xHeights.Length; i++)
                {
                    var ax = originPoint.X + intervalX * i;
                    if (maxHeightx > 0)
                    {
                        cc.SetSourceColor(ColorRGBA.DodgerBlue);
                        cc.FillRectangle(ax, originPoint.Y + 15, intervalX + 1, (float)xHeights[i] / maxHeightx * (height - originPoint.Y - 15));
                    }
                    if (intervalX > 12)
                    {
                        var a = i * xStep + xRangeLower;
                        var b = (i + 1) * xStep + xRangeLower;
                        cc.SetSourceColor(ColorRGBA.Black);
                        cc.MoveTo(ax + 4, originPoint.Y + 16);
                        cc.Rotate(0.5 * Math.PI);
                        cc.ShowText((new Decimal(a)).ToString() + " " + (new Decimal(b)).ToString());
                        cc.Rotate(-0.5 * Math.PI);
                    }
                }

                // Y轴柱状图生成
                var maxHeighty = yHeights.Max();
                for (int i = 0; i < yHeights.Length; i++)
                {
                    var ay = originPoint.Y - intervalY * (i + 1);
                    if (maxHeighty > 0)
                    {
                        cc.SetSourceColor(ColorRGBA.DodgerBlue);
                        cc.FillRectangle(originPoint.X - 15 - (originPoint.X - 15) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), ay, (originPoint.X - 15) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), intervalY + 1);
                    }
                    if (intervalY > 12)
                    {
                        var a = i * yStep + yRangeLower;
                        var b = (i + 1) * yStep + yRangeLower;
                        var text = (new Decimal(a)).ToString() + " " + (new Decimal(b)).ToString();
                        var textWidth = cc.TextExtents(text).Width;
                        cc.SetSourceColor(ColorRGBA.Black);
                        cc.MoveTo(originPoint.X - textWidth - 16, ay + 11);
                        cc.ShowText(text);
                    }
                }

                //画坐标轴
                var pointx1 = new FloatPoint(0, originPoint.Y);
                var pointx2 = new FloatPoint(width, originPoint.Y);
                var pointy1 = new FloatPoint(originPoint.X, 0);
                var pointy2 = new FloatPoint(originPoint.X, height);

                cc.SetSourceColor(ColorRGBA.Black);
                cc.DrawLine(pointx1, pointx2);
                cc.DrawLine(pointy1, pointy2);

                cc.SetSourceColor(ColorRGBA.Gray);
                cc.DrawLine(originPoint.X, originPoint.Y + 15, width - 1, originPoint.Y + 15);
                cc.DrawLine(originPoint.X - 15, originPoint.Y, originPoint.X - 15, 0);

                var xTitleWidth = cc.TextExtents(xTitle).Width;
                var yTitleWidth = cc.TextExtents(yTitle).Width;

                cc.SetSourceColor(ColorRGBA.DodgerBlue);
                cc.MoveTo((width + originPoint.X - xTitleWidth) / 2, originPoint.Y + 11);
                cc.ShowText(xTitle);

                cc.MoveTo(originPoint.X - 11, (originPoint.Y - yTitleWidth) / 2);
                cc.Rotate(0.5 * Math.PI);
                cc.ShowText(yTitle);
                cc.Rotate(-0.5 * Math.PI);

                cc.SetSourceColor(ColorRGBA.Black);

                cc.DrawLine(width - 0.5f, originPoint.Y, width - 0.5f, originPoint.Y + 2);
                cc.DrawLine(originPoint.X, 0.5f, originPoint.X - 2, 0.5f);

                var xRangeUpperText = (new Decimal(xRangeUpper)).ToString();
                cc.MoveTo(width - cc.TextExtents(xRangeUpperText).Width - 4, originPoint.Y + 11);
                cc.ShowText(xRangeUpperText);

                var xRangeLowerText = (new Decimal(xRangeLower)).ToString();
                cc.MoveTo(originPoint.X + 4, originPoint.Y + 11);
                cc.ShowText(xRangeLowerText);

                var yRangeUpperText = (new Decimal(yRangeUpper)).ToString();
                cc.MoveTo(originPoint.X - 11, 4);
                cc.Rotate(0.5 * Math.PI);
                cc.ShowText(yRangeUpperText);
                cc.Rotate(-0.5 * Math.PI);

                var yRangeLowerText = (new Decimal(yRangeLower)).ToString();
                cc.MoveTo(originPoint.X - 11, originPoint.Y - cc.TextExtents(yRangeLowerText).Width - 4);
                cc.Rotate(0.5 * Math.PI);
                cc.ShowText(yRangeLowerText);
                cc.Rotate(-0.5 * Math.PI);
                
                // 验证框
                if (Data.Definition.Validation != null)
                {
                    ColorRGBA color = ColorRGBA.Black;
                    FloatPoint[] genericOutline = null;
                    if (Data.Definition.Validation is OutlineInsideValidation)
                    {
                        var vd = Data.Definition.Validation as OutlineInsideValidation;
                        color = ColorRGBA.LimeGreen;
                        genericOutline = vd.GetOutline();
                    }
                    else if (Data.Definition.Validation is OutlineOutsideValidation)
                    {
                        var vd = Data.Definition.Validation as OutlineOutsideValidation;
                        color = ColorRGBA.Red;
                        genericOutline = vd.GetOutline();
                    }

                    if (genericOutline != null)
                    {
                        var outline = new FloatPoint[genericOutline.Length];
                        for (int i = 0; i < outline.Length; i++) outline[i] = new FloatPoint(genericOutline[i].X, genericOutline[i].Y);

                        for (int i = 0; i < outline.Length; i++)
                        {
                            var x = (float)((outline[i].X - xRange.lower) / (xRange.upper - xRange.lower) * ((float)width - originPoint.X) + originPoint.X);
                            var y = (float)((yRange.upper - outline[i].Y) / (yRange.upper - yRange.lower) * originPoint.Y);
                            outline[i] = new FloatPoint(x, y);
                        }

                        cc.SetSourceColor(color);

                        cc.LineWidth = 2;
                        cc.DrawPolygon(outline);
                        cc.LineWidth = 1;
                    }
                }

                if (mouse.X < 0 || mouse.X >= width || mouse.Y < 0 || mouse.Y >= height) {}
                else
                {
                    // 标注信息
                    cc.SetFontSize(11);

                    if (mouse.X > originPoint.X && mouse.X < width - 1 && mouse.Y < originPoint.Y && mouse.Y > 0)
                    {
                        var firstText = "(" + Math.Round((xRangeUpper - xRangeLower) * (mouse.X - originPoint.X) / (width - 1 - originPoint.X) + xRangeLower, 3) + "," + Math.Round((yRangeUpper - yRangeLower) * (originPoint.Y - mouse.Y) / originPoint.Y + yRangeLower, 3) + ")";
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

                        cc.SetSourceColor(ColorRGBA.Black);

                        cc.SetFontSize(11);
                        var firstTextSize = cc.TextExtents(firstText);

                        cc.SetFontSize(10);
                        var secondTextSize = cc.TextExtents(secondText);
                        
                        var firstTextLoc = new FloatPoint();
                        FloatPoint? secondTextLoc = null;
                        if (mouse.X - originPoint.X <= Math.Max(firstTextSize.Width, secondTextSize.Width))
                        {
                            if (mouse.Y <= firstTextSize.Height + secondTextSize.Height)
                            {
                                firstTextLoc = new FloatPoint(mouse.X, mouse.Y);
                                if (secondText.Length > 0) secondTextLoc = new FloatPoint(mouse.X, mouse.Y + (float)firstTextSize.Height);
                            }
                            else
                            {
                                firstTextLoc = new FloatPoint(mouse.X, mouse.Y - (float)firstTextSize.Height);
                                if (secondText.Length > 0) secondTextLoc = new FloatPoint(mouse.X, mouse.Y - (float)firstTextSize.Height - (float)secondTextSize.Height);
                            }
                        }
                        else
                        {
                            if (mouse.Y <= firstTextSize.Height + secondTextSize.Height)
                            {
                                firstTextLoc = new FloatPoint(mouse.X - (float)firstTextSize.Width, mouse.Y);
                                if (secondText.Length > 0) secondTextLoc = new FloatPoint(mouse.X - (float)secondTextSize.Width, mouse.Y + (float)firstTextSize.Height);
                            }
                            else
                            {
                                firstTextLoc = new FloatPoint(mouse.X - (float)firstTextSize.Width, mouse.Y - (float)firstTextSize.Height);
                                if (secondText.Length > 0) secondTextLoc = new FloatPoint(mouse.X - (float)secondTextSize.Width, mouse.Y - (float)firstTextSize.Height - (float)secondTextSize.Height);
                            }
                        }

                        cc.SetFontSize(11);
                        cc.MoveTo(firstTextLoc.X, firstTextLoc.Y + 10);
                        cc.ShowText(firstText);

                        if (secondTextLoc != null)
                        {
                            cc.SetFontSize(10);
                            cc.MoveTo(secondTextLoc.Value.X, secondTextLoc.Value.Y + 10);
                            cc.ShowText(secondText);
                        }

                        cc.SetDash(new double[] {1, 1}, 0);
                        cc.SetSourceColor(ColorRGBA.DodgerBlue);

                        cc.DrawLine(originPoint.X, mouse.Y, width - 1, mouse.Y);
                        cc.DrawLine(mouse.X, 0, mouse.X, originPoint.Y);
                    }

                    for (int i = 0; i < xHeights.Length; i++)
                    {
                        var ax = originPoint.X + i * intervalX;
                        var bx = originPoint.X + (i + 1) * intervalX;
                        if (mouse.Y > originPoint.Y && mouse.X >= ax && mouse.X < bx)
                        {
                            cc.SetSourceColor(ColorRGBA.Gray);
                            cc.DrawRectangle(i * intervalX + originPoint.X, originPoint.Y + 15, intervalX - 1, height - originPoint.Y - 15 - 1);

                            var percent = 0.0;
                            if (xSum != 0) percent = (double)xHeights[i] / xSum * 100;
                            var fullText = "(" + xHeights[i] + " : " + percent.ToString("F1") + "%)";

                            var sizef = cc.TextExtents(fullText);
                            cc.MoveTo(mouse.X - sizef.Width, originPoint.Y + sizef.Height);
                            cc.SetSourceColor(ColorRGBA.Black);
                            cc.ShowText(fullText);
                        }
                    }

                    for (int i = 0; i < yHeights.Length; i++)
                    {
                        var ay = originPoint.Y - i * intervalY;
                        var by = originPoint.Y - (i + 1) * intervalY;
                        if (mouse.X < originPoint.X && mouse.X > 0 && mouse.Y >= by && mouse.Y < ay)
                        {
                            cc.SetSourceColor(ColorRGBA.Gray);
                            cc.DrawRectangle(1, originPoint.Y - (i + 1) * intervalY, originPoint.X - 16, intervalY);

                            var percent = 0.0;
                            if (ySum != 0) percent = (double)yHeights[i] / ySum * 100;

                            var fullText = "(" + yHeights[i] + " : " + percent.ToString("F1") + "%)";

                            var sizef = cc.TextExtents(fullText);
                            cc.MoveTo(mouse.X + 15, mouse.Y + sizef.Height);
                            cc.SetSourceColor(ColorRGBA.Black);
                            cc.ShowText(fullText);
                        }
                    }
                }
            }
            catch (Exception) {}
        }
    }
}
