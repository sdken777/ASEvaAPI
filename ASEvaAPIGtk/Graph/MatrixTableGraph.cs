using System;
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
    /// (api:gtk=3.0.0) Matrix table graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 矩阵热力图数据可视化窗口
    /// </summary>
    public class MatrixTableGraph : BaseGraph
    {
        [UI] Label? labelTitle, labelValidation, labelValidationSub;
        [UI] Overlay? overlay;
        [UI] DrawingArea? draw;
        [UI] EventBox? eventBox;
        [UI] Box? boxValidation;

        EventBoxHelper eventBoxHelper = new EventBoxHelper();
        DrawSwap? drawSwap = null;

        public MatrixTableGraph() : this(new Builder("MatrixTableGraph.glade"))
        {}

        private MatrixTableGraph(Builder builder) : base(builder.GetRawOwnedObject("MatrixTableGraph"))
        {
            builder.Autoconnect(this);

            this.SetBackColor(ColorRGBA.White);

            overlay?.AddOverlay(boxValidation);
            if (eventBox != null) eventBoxHelper.Add(eventBox);
            if (draw != null) drawSwap = new DrawSwap(draw, "ASEva.UIGtk.MatrixTableGraph");

            eventBoxHelper.LeftDown += eventBox_LeftDown;
            if (drawSwap != null) drawSwap.Paint += draw_Paint;
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
			drawSwap?.Close();
		}

        public override void UpdateUIWithData()
        {
            if (Data == null || !(Data is MatrixTableData)) return;

            // 数据和验证条件显示
            drawSwap?.Refresh();

            if (labelValidationSub != null)
            {
                if (Data.Definition.Validation == null)
                {
                    labelValidationSub.Text = "";
                }
                else
                {
                    var vd = Data.Definition.Validation;
                    if (vd is ValueBelowValidation vbv)
                    {
                        labelValidationSub.Text = "≤ " + vbv.GetThreshold();
                    }
                    else if (vd is ValueAboveValidation vav)
                    {
                        labelValidationSub.Text = "≥ " + vav.GetThreshold();
                    }
                    else
                    {
                        labelValidationSub.Text = "";
                    }
                }
            }

            // 标题显示
            if (labelTitle != null) labelTitle.Text = Data.Definition.MainTitle;
            if (!Data.HasData())
            {
                if (labelValidation != null)
                {
                    labelValidation.SetForeColor(ColorRGBA.Black);
                    labelValidation.Text = "No Data.";
                }
                return;
            }

            // 验证结果显示
            if (labelValidation != null)
            {
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
        }

        private ColorRGBA getColorByValue(double upper, double lower, double value)
        {
            var color = new ColorRGBA();
            ColorRGBA[] colors = new ColorRGBA[17];
            colors[0] = new ColorRGBA(0, 0, 128);
            colors[1] = new ColorRGBA(0, 0, 192);
            colors[2] = new ColorRGBA(0, 0, 255);
            colors[3] = new ColorRGBA(0, 64, 255);
            colors[4] = new ColorRGBA(0, 128, 255);
            colors[5] = new ColorRGBA(0, 192, 255);
            colors[6] = new ColorRGBA(0, 255, 255);
            colors[7] = new ColorRGBA(64, 255, 192);
            colors[8] = new ColorRGBA(128, 255, 128);
            colors[9] = new ColorRGBA(192, 255, 64);
            colors[10] = new ColorRGBA(255, 255, 0);
            colors[11] = new ColorRGBA(255, 192, 0);
            colors[12] = new ColorRGBA(255, 128, 0);
            colors[13] = new ColorRGBA(255, 64, 0);
            colors[14] = new ColorRGBA(255, 0, 0);
            colors[15] = new ColorRGBA(192, 0, 0);
            colors[16] = new ColorRGBA(128, 0, 0);

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
                        color = new ColorRGBA((byte)(colors[i].R + (int)(scale_red * (index - i))), (byte)(colors[i].G + (int)(scale_green * (index - i))), (byte)(colors[i].B + (int)(scale_blue * (index - i))));
                    }
                }
            }
            return color;
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
            var D = Data as MatrixTableData;
            if (D == null || draw == null) return;

            var xrange = D.GetXRange();
            var yrange = D.GetYRange();
            if (xrange == null || yrange == null) return;

            try
            {
                cc.LineWidth = 1;
                cc.SelectFontFace(CairoContextExtension.NotoFontName, Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);

                cc.Translate(0, 2);

                var width = draw.AllocatedWidth;
                var height = draw.AllocatedHeight - 2;
                var originPoint = new FloatPoint((float)width / 4, (float)height / 3 * 2);

                var xTitle = D.GetXTitle();
                var yTitle = D.GetYTitle();
                var xHeights = D.GetXHistValues();
                var yHeights = D.GetYHistValues();
                var values = D.GetValues();
                var valueRange = D.GetValueRefRange();
                var isPercentage = D.GetMode() == MatrixTableMode.Percentage;

                var intervalX = (width - originPoint.X) / xrange.Count;
                var intervalY = originPoint.Y / yrange.Count;
                var xRangeLower = xrange.Base;
                var xRangeUpper = xRangeLower + xrange.Count * xrange.Step;
                var yRangeLower = yrange.Base;
                var yRangeUpper = yRangeLower + yrange.Count * yrange.Step;

                //X轴柱状图生成
                cc.SetFontSize(10);
                
                double maxHeightx = 0;
                foreach (var val in xHeights) maxHeightx = Math.Max(maxHeightx, Math.Abs(val));
                
                if (maxHeightx > 0)
                {
                    for (int i = 0; i < xHeights.Length; i++)
                    {
                        cc.SetSourceColor(xHeights[i] >= 0 ? ColorRGBA.DodgerBlue : ColorRGBA.MediumPurple);
                        cc.FillRectangle(i * intervalX + originPoint.X, originPoint.Y + 15, intervalX + 1, (float)Math.Abs(xHeights[i]) / (float)maxHeightx * (height - originPoint.Y - 15));
                    }
                }

                //Y轴柱状图生成
                double maxHeighty = 0;
                foreach (var val in yHeights) maxHeighty = Math.Max(maxHeighty, Math.Abs(val));
                
                if (maxHeighty > 0)
                {
                    for (int i = 0; i < yHeights.Length; i++)
                    {
                        cc.SetSourceColor(yHeights[i] >= 0 ? ColorRGBA.DodgerBlue : ColorRGBA.MediumPurple);
                        cc.FillRectangle(originPoint.X - 15 - (originPoint.X - 15) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), originPoint.Y - (i + 1) * intervalY, (originPoint.X - 15) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), intervalY + 1);
                    }
                }

                // 热力图
                for (int i = 0; i < xrange.Count; i++)
                {
                    var x = i * intervalX;
                    for (int j = 0; j < yrange.Count; j++)
                    {
                        var y = (j + 1) * intervalY;
                        cc.SetSourceColor(getColorByValue(valueRange.upper, valueRange.lower, values[i,j]));
                        cc.FillRectangle(x + originPoint.X, originPoint.Y - y, intervalX + 1, intervalY + 1);
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
                if (D.Definition.Validation != null)
                {
                    ColorRGBA color = ColorRGBA.Black;
                    FloatPoint[]? genericOutline = null;
                    if (D.Definition.Validation is OutlineInsideValidation oiv)
                    {
                        color = ColorRGBA.LimeGreen;
                        genericOutline = oiv.GetOutline();
                    }
                    else if (D.Definition.Validation is OutlineOutsideValidation oov)
                    {
                        color = ColorRGBA.Red;
                        genericOutline = oov.GetOutline();
                    }

                    if (genericOutline != null)
                    {
                        var outline = new FloatPoint[genericOutline.Length];
                        for (int i = 0; i < outline.Length; i++) outline[i] = new FloatPoint(genericOutline[i].X, genericOutline[i].Y);

                        for (int i = 0; i < outline.Length; i++)
                        {
                            var x = (float)((outline[i].X - xRangeLower) / (xRangeUpper - xRangeLower) * ((float)width - originPoint.X) + originPoint.X);
                            var y = (float)((yRangeUpper - outline[i].Y) / (yRangeUpper - yRangeLower) * originPoint.Y);
                            outline[i] = new FloatPoint(x, y);
                        }

                        cc.SetSourceColor(color);

                        cc.LineWidth = 2;
                        cc.DrawPolygon(outline);
                        cc.LineWidth = 1;
                    }
                }

                // 验证鼠标位置
                var mouse = draw.GetPointer();
                if (mouse.X < 0 || mouse.X >= width || mouse.Y < 0 || mouse.Y >= height) {}
                else
                {
                    // 标注信息
                    cc.SetFontSize(11);

                    if (mouse.X > originPoint.X && mouse.X < width - 1 && mouse.Y < originPoint.Y && mouse.Y > 0)
                    {
                        var Xindex = (int)Math.Floor((mouse.X - originPoint.X) / intervalX);
                        var Yindex = (int)Math.Floor((originPoint.Y - mouse.Y) / intervalY);
                        var text = isPercentage ? (values[Xindex, Yindex].ToString("F1") + "%") : (Math.Abs(values[Xindex, Yindex]) >= 0.1 ? values[Xindex, Yindex].ToString("F3") : (new Decimal(values[Xindex, Yindex])).ToString());
                        var fullText = "(" + Math.Round((xRangeUpper - xRangeLower) * (mouse.X - originPoint.X) / (width - 1 - originPoint.X) + xRangeLower, 3) + "," + Math.Round((yRangeUpper - yRangeLower) * (originPoint.Y - mouse.Y) / originPoint.Y + yRangeLower, 3) + " : " + text + ")";

                        var sizef = cc.TextExtents(fullText);
                        if (mouse.Y <= sizef.Height) cc.MoveTo(mouse.X - sizef.Width - 4, mouse.Y + sizef.Height - 2);
                        else cc.MoveTo(mouse.X - sizef.Width - 4, mouse.Y - 4);

                        cc.SetSourceColor(ColorRGBA.Red);
                        cc.ShowText(fullText);

                        cc.SetDash(new double[] {1, 1}, 0);
                        cc.SetSourceColor(ColorRGBA.DodgerBlue);

                        cc.DrawLine(originPoint.X, mouse.Y, width - 1, mouse.Y);
                        cc.DrawLine(mouse.X, 0, mouse.X, originPoint.Y);
                    }

                    for (int i = 0; i < xHeights.Length; i++)
                    {
                        var a = i * xrange.Step + xrange.Base;
                        var b = (i + 1) * xrange.Step + xrange.Base;
                        var ax = originPoint.X + i * intervalX;
                        var bx = originPoint.X + (i + 1) * intervalX;
                        if (mouse.Y > originPoint.Y && mouse.X >= ax && mouse.X < bx)
                        {
                            cc.SetSourceColor(ColorRGBA.Gray);
                            cc.DrawRectangle(i * intervalX + originPoint.X, originPoint.Y + 15, intervalX - 1, height - originPoint.Y - 15 - 1);

                            var text = isPercentage ? (xHeights[i].ToString("F1") + "%") : (Math.Abs(xHeights[i]) >= 0.1 ? xHeights[i].ToString("F3") : (new Decimal(xHeights[i])).ToString());
                            var fullText = "(" + (new Decimal(a)).ToString() + "~" + (new Decimal(b)).ToString() + " : " + text + ")";

                            var sizef = cc.TextExtents(fullText);
                            cc.MoveTo(mouse.X - sizef.Width, originPoint.Y + 15 + sizef.Height);
                            cc.SetSourceColor(ColorRGBA.Red);
                            cc.ShowText(fullText);
                        }
                    }

                    for (int i = 0; i < yHeights.Length; i++)
                    {
                        var a = i * yrange.Step + yrange.Base;
                        var b = (i + 1) * yrange.Step + yrange.Base;
                        var ay = originPoint.Y - i * intervalY;
                        var by = originPoint.Y - (i + 1) * intervalY;
                        if (mouse.X < originPoint.X && mouse.X > 0 && mouse.Y >= by && mouse.Y < ay)
                        {
                            cc.SetSourceColor(ColorRGBA.Gray);
                            cc.DrawRectangle(1, originPoint.Y - (i + 1) * intervalY, originPoint.X - 16, intervalY);

                            var text = isPercentage ? (yHeights[i].ToString("F1") + "%") : (Math.Abs(yHeights[i]) >= 0.1 ? yHeights[i].ToString("F3") : (new Decimal(yHeights[i])).ToString());
                            var fullText = "(" + (new Decimal(a)).ToString() + "~" + (new Decimal(b)).ToString() + " : " + text + ")";

                            var sizef = cc.TextExtents(fullText);
                            cc.MoveTo(mouse.X + 15, mouse.Y + sizef.Height);
                            cc.SetSourceColor(ColorRGBA.Red);
                            cc.ShowText(fullText);
                        }
                    }
                }
            }
            catch (Exception ex) { Dump.Exception(ex); }
        }
    }
}
