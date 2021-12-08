using System;
using Gtk;
using ASEva.Utility;
using ASEva.UIGtk;
using ASEva.Graph;
using UI = Gtk.Builder.ObjectAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649

    /// <summary>
    /// (api:gtk=2.0.0) 标签热力图数据可视化窗口
    /// </summary>
    public class LabelTableGraph : BaseGraph
    {
        [UI] Label labelTitle, labelValidation;
        [UI] Overlay overlay;
        [UI] DrawingArea draw;
        [UI] EventBox eventBox;

        EventBoxHelper eventBoxHelper = new EventBoxHelper();

        public LabelTableGraph() : this(new Builder("LabelTableGraph.glade"))
        {
            this.SetBackColor(ColorRGBA.White);

            overlay.AddOverlay(labelValidation);
            eventBoxHelper.Add(eventBox);

            eventBoxHelper.LeftDown += eventBox_LeftDown;
            draw.Drawn += draw_Drawn;
        }

        private LabelTableGraph(Builder builder) : base(builder.GetObject("LabelTableGraph").Handle)
        {
            builder.Autoconnect(this);
        }

        public override void UpdateUIWithData()
        {
            if (Data == null || !(Data is LabelTableData)) return;

            // 数据显示
            if (DrawBeat.CallerBegin(draw))
            {
                draw.QueueDraw();
                DrawBeat.CallerEnd(draw);
            }

            // 标题显示
            labelTitle.Text = Data == null ? "" : Data.Definition.MainTitle;
            if (!Data.HasData())
            {
                labelValidation.SetForeColor(ColorRGBA.Black);
                labelValidation.Text = "No Data.";
                return;
            }
            else labelValidation.Text = "";
        }

        private ColorRGBA getColorByValue(double upper, double lower, double value)
        {
            var color = new ColorRGBA();
            ColorRGBA[] colors = new ColorRGBA[17];
            colors[0] = new ColorRGBA(64, 192, 32);
            colors[1] = new ColorRGBA(72, 184, 32);
            colors[2] = new ColorRGBA(80, 176, 32);
            colors[3] = new ColorRGBA(88, 168, 32);
            colors[4] = new ColorRGBA(96, 160, 32);
            colors[5] = new ColorRGBA(104, 152, 32);
            colors[6] = new ColorRGBA(112, 144, 32);
            colors[7] = new ColorRGBA(120, 136, 32);
            colors[8] = new ColorRGBA(128, 128, 32);
            colors[9] = new ColorRGBA(136, 120, 32);
            colors[10] = new ColorRGBA(144, 112, 32);
            colors[11] = new ColorRGBA(152, 104, 32);
            colors[12] = new ColorRGBA(160, 96, 32);
            colors[13] = new ColorRGBA(168, 88, 32);
            colors[14] = new ColorRGBA(176, 80, 32);
            colors[15] = new ColorRGBA(184, 72, 32);
            colors[16] = new ColorRGBA(192, 64, 32);

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

        private void eventBox_LeftDown(EventBox box, Gdk.EventButton ev)
        {
            HandleGraphSelected();
        }

        private void draw_Drawn(object o, DrawnArgs args)
        {
            DrawBeat.CallbackBegin(draw, "ASEva.UIGtk.LabelTableGraph");

            try
            {
                var cc = args.Cr;
                cc.LineWidth = 1;
                cc.SelectFontFace(CairoContextExtension.NotoFontName, Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);

                cc.Translate(0, 2);

                var width = draw.AllocatedWidth;
                var height = draw.AllocatedHeight - 2;
                var originPoint = new FloatPoint((float)width / 4, (float)height / 3 * 2);

                var D = Data as LabelTableData;
                var xTitle = D.GetXTitle();
                var yTitle = D.GetYTitle();
                var xLabels = D.GetXLabels();
                var yLabels = D.GetYLabels();
                var values = D.GetValues();
                var xrange = values.GetLength(0);
                var yrange = values.GetLength(1);
                var direction = D.GetValueDirection();
                var defaultValue = D.GetDefaultValue();
                var isPercentage = D.GetMode() == LabelTableMode.Percentage;
                var xHeights = D.GetXHistValues();
                var yHeights = D.GetYHistValues();
                var xcount = D.GetXLabelCount();
                var ycount = D.GetYLabelCount();

                var intervalX = (width - originPoint.X) / xcount;
                var intervalY = originPoint.Y / ycount;

                //X轴柱状图生成
                cc.SetFontSize(10);

                double maxHeightx = 0;
                foreach (var val in xHeights) maxHeightx = Math.Max(maxHeightx, Math.Abs(val));

                for (int i = 0; i < xHeights.Length; i++)
                {
                    cc.SetSourceColor(xHeights[i] >= 0 ? ColorRGBA.DodgerBlue : ColorRGBA.MediumPurple);
                    var ax = originPoint.X + intervalX * i;
                    if (maxHeightx > 0)
                    {
                        cc.FillRectangle(ax, originPoint.Y + 15, intervalX + 1, (float)Math.Abs(xHeights[i]) / (float)maxHeightx * (height - originPoint.Y - 15));
                    }

                    var text = xLabels[i];
                    cc.SetSourceColor(ColorRGBA.Black);
                    cc.MoveTo(ax + 4, originPoint.Y + 16);
                    cc.Rotate(0.5 * Math.PI);
                    cc.ShowText(text);
                    cc.Rotate(-0.5 * Math.PI);
                }

                //Y轴柱状图生成
                double maxHeighty = 0;
                foreach (var val in yHeights) maxHeighty = Math.Max(maxHeighty, Math.Abs(val));

                for (int i = 0; i < yHeights.Length; i++)
                {
                    cc.SetSourceColor(yHeights[i] >= 0 ? ColorRGBA.DodgerBlue : ColorRGBA.MediumPurple);
                    var ay = originPoint.Y - intervalY * (i + 1);
                    if (maxHeighty > 0)
                    {
                        cc.FillRectangle(originPoint.X - 15 - (originPoint.X - 15) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), ay, (originPoint.X - 15) * ((float)Math.Abs(yHeights[i]) / (float)maxHeighty), intervalY + 1);
                    }

                    var text = yLabels[i];
                    var textWidth = cc.TextExtents(text).Width;
                    cc.SetSourceColor(ColorRGBA.Black);
                    cc.MoveTo(originPoint.X - textWidth - 16, ay + 11);
                    cc.ShowText(text);
                }

                // 画热力图
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

                        cc.SetSourceColor(getColorByValue(upper, lower, val));
                        cc.Rectangle(x + originPoint.X, originPoint.Y - y, intervalX + 1, intervalY + 1);
                        cc.Fill();
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

                // 验证鼠标位置
                var mouse = draw.GetPointer();
                if (mouse.X < 0 || mouse.X >= width || mouse.Y < 0 || mouse.Y >= height) {}
                else
                {
                    // 标注信息
                    cc.SetFontSize(11);

                    if (mouse.X > originPoint.X && mouse.X < width - 1 && mouse.Y < originPoint.Y && mouse.Y > 0)
                    {
                        cc.SetSourceColor(ColorRGBA.DodgerBlue);

                        int xIndex = (int)((mouse.X - originPoint.X) / intervalX);
                        int yIndex = (int)((originPoint.Y - mouse.Y) / intervalY);
                        if (xIndex >= 0 && xIndex < xcount && yIndex >= 0 && yIndex < ycount)
                        {
                            cc.DrawRectangle(xIndex * intervalX + originPoint.X, originPoint.Y - (yIndex + 1) * intervalY, intervalX, intervalY);
                        }

                        var text = isPercentage ? (values[xIndex, yIndex].ToString("F1") + "%") : (Math.Abs(values[xIndex, yIndex]) >= 0.1 ? values[xIndex, yIndex].ToString("F3") : values[xIndex, yIndex].ToString());
                        var fullText = "(" + xLabels[xIndex] + "," + yLabels[yIndex] + " : " + text + ")";

                        var sizef = cc.TextExtents(fullText);
                        if (mouse.Y <= sizef.Height + 4) cc.MoveTo(mouse.X - sizef.Width - 4, mouse.Y + sizef.Height - 4);
                        else cc.MoveTo(mouse.X - sizef.Width - 4, mouse.Y - 4);
                        
                        cc.SetSourceColor(ColorRGBA.Black);
                        cc.ShowText(fullText);
                    }

                    for (int i = 0; i < xHeights.Length; i++)
                    {
                        var ax = originPoint.X + i * intervalX;
                        var bx = originPoint.X + (i + 1) * intervalX;
                        if (mouse.Y > originPoint.Y && mouse.X >= ax && mouse.X < bx)
                        {
                            cc.SetSourceColor(ColorRGBA.Gray);
                            cc.DrawRectangle(i * intervalX + originPoint.X, originPoint.Y + 15, intervalX - 1, height - originPoint.Y - 15 - 1);

                            var text = isPercentage ? (xHeights[i].ToString("F1") + "%") : (Math.Abs(xHeights[i]) >= 0.1 ? xHeights[i].ToString("F3") : xHeights[i].ToString());
                            var fullText = "(" + xLabels[i] + " : " + text + ")";

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

                            var text = isPercentage ? (yHeights[i].ToString("F1") + "%") : (Math.Abs(yHeights[i]) >= 0.1 ? yHeights[i].ToString("F3") : yHeights[i].ToString());
                            var fullText = "(" + yLabels[i] + " : " + text + ")";

                            var sizef = cc.TextExtents(fullText);
                            cc.MoveTo(mouse.X + 15, mouse.Y + sizef.Height);
                            cc.SetSourceColor(ColorRGBA.Black);
                            cc.ShowText(fullText);
                        }
                    }
                }
            }
            catch (Exception) {}

            DrawBeat.CallbackEnd(draw);
        }
    }
}
