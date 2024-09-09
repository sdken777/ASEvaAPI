using System;
using System.Collections.Generic;
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
    /// (api:gtk=3.0.0) Histogram and poly line graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 柱状折线图可视化控件
    /// </summary>
    public class HistLineGraph : BaseGraph
    {
        [UI] Label labelTitle, labelValidation;
        [UI] Overlay overlay;
        [UI] DrawingArea draw;
        [UI] EventBox eventBox;

        EventBoxHelper eventBoxHelper = new EventBoxHelper();
		DrawSwap drawSwap;

        public HistLineGraph() : this(new Builder("HistLineGraph.glade"))
        {
            this.SetBackColor(ColorRGBA.White);

            overlay.AddOverlay(labelValidation);
            eventBoxHelper.Add(eventBox);
			drawSwap = new DrawSwap(draw, "ASEva.UIGtk.HistLineGraph");

            eventBoxHelper.LeftDown += eventBox_LeftDown;
            drawSwap.Paint += draw_Paint;
        }

        private HistLineGraph(Builder builder) : base(builder.GetRawOwnedObject("HistLineGraph"))
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
			if (Data == null || !(Data is HistAndLineData)) return;

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
				labelValidation.Text = percentage == null ? "" : (percentage.Value.ToString("F1") + "% OK");
			}
			else if (vdResult.Value)
			{
				labelValidation.SetForeColor(ColorRGBA.Green);
				labelValidation.Text = percentage == null ? "OK" : (percentage.Value.ToString("F1") + "% OK");
			}
			else
			{
				labelValidation.SetForeColor(ColorRGBA.Red);
				labelValidation.Text = percentage == null ? "NG" : (percentage.Value.ToString("F1") + "% OK");
			}
		}

        private void eventBox_LeftDown(EventBox box, Gdk.EventButton ev)
        {
            HandleGraphSelected();
        }

        private void draw_Paint(DrawSwap swap, Cairo.Context cc)
        {
			if (Data == null || !(Data is HistAndLineData)) return;

			try
			{
				cc.LineWidth = 1;
				cc.SelectFontFace("Noto Sans CJK SC", Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);

				cc.Translate(0, 2);

				var drawWidth = draw.AllocatedWidth;
				var drawHeight = draw.AllocatedHeight - 2;

				// var histBrush = new SolidBrush(Color.DodgerBlue);
				// var lineBrush = new SolidBrush(Color.Orange);

				var D = Data as HistAndLineData;
				String xTitle = D.GetXTitle();
				String histTitle = D.GetHistTitle();
				String lineTitle = D.GetLineTitle();
				bool isEnableLine = D.IsLineEnabled();
				HistLineSample[] samples = D.GetSamples();

				// 计算y轴范围
				double maximum = Double.NegativeInfinity;
				double minimum = Double.PositiveInfinity;
				foreach (var sample in samples)
				{
					maximum = Math.Max(maximum, sample.HistValue);
					minimum = Math.Min(minimum, sample.HistValue);
					if (isEnableLine)
					{
						maximum = Math.Max(maximum, sample.LineValue);
						minimum = Math.Min(minimum, sample.LineValue);
					}
				}

				bool hasValue = maximum >= minimum;
				if (hasValue)
				{
					if (maximum == minimum)
					{
						if (minimum > 0)
                        {
							maximum = minimum * 1.25;
							minimum = 0;
                        }
						else if (minimum < 0)
                        {
							minimum = minimum * 1.25;
							maximum = 0;
                        }
					}
					else
					{
						var maxreal = maximum + (maximum - minimum) * 0.02;
						var minreal = minimum - (maximum - minimum) * 0.02;
						maximum = maxreal;
						minimum = minreal;

						if (minimum > -0.001) minimum = -0.001;
						if (maximum < 0.001) maximum = 0.001;
					}
				}

				var yGrids = new List<double>();
				if (hasValue)
				{
					double ystep = (maximum - minimum) / Math.Max(5, drawHeight / 75);
					int val = 1;
					double bas = -3;
					double yScale = 0;
					while (true)
					{
						var target = val * Math.Pow(10, bas);
						if (target > ystep)
						{
							yScale = target;
							break;
						}
						if (val == 1) val = 2;
						else if (val == 2) val = 5;
						else if (val == 5)
						{
							val = 1;
							bas++;
						}
					}

					var yStart = Math.Ceiling(minimum / yScale) * yScale;
					int k = 0;
					while (true)
					{
						var target = yStart + k++ * yScale;
						if (target >= maximum) break;
						else if (target > minimum) yGrids.Add(target);
					}
				}

				// 生成y轴刻度值文字
				cc.SetFontSize(10);

				var yGridTexts = new List<String>();
				var yGridTextsMaxWidth = 0f;
				foreach (var y in yGrids)
				{
					var text = (new Decimal(y)).ToString();
					yGridTexts.Add(text);
					yGridTextsMaxWidth = Math.Max(yGridTextsMaxWidth, (float)cc.TextExtents(text).Width);
				}
				yGridTextsMaxWidth = Math.Min(100, yGridTextsMaxWidth + 2);

				// 生成x轴刻度值文字
				var xGridTexts = new List<String>();
				var xGridTextsMaxWidth = 0f;
				foreach (var sample in samples)
				{
					var text = sample.Name;
					xGridTexts.Add(text);
					xGridTextsMaxWidth = Math.Max(xGridTextsMaxWidth, (float)cc.TextExtents(text).Width);
				}
				xGridTextsMaxWidth = Math.Min(100, xGridTextsMaxWidth + 1);
				var smallFontHeight = (float)cc.TextExtents("A").Height;

				// 计算坐标转换参数
				float left = (float)15 + yGridTextsMaxWidth;
				float right = (float)drawWidth - (isEnableLine ? 16 : 1);
				float top = 0;
				float bottom = (float)drawHeight - 15 - xGridTextsMaxWidth;
				float step = (right - left) / samples.Length;

				//double yFactor = -(bottom - top) / (maximum - minimum);
				//double yBase = -maximum * yFactor;

				// 绘制y轴刻度线
				if (hasValue)
				{
					double yFactor = -(bottom - top) / (maximum - minimum);
					double yBase = -maximum * yFactor;

					cc.SetSourceColor(ColorConv.ConvCairo(isEnableLine ? ColorRGBA.Orange : ColorRGBA.DodgerBlue));
					foreach (var val in yGrids)
					{
						float pixelY = (float)(val * yFactor + yBase);
						cc.MoveTo(left, pixelY);
						cc.LineTo(right, pixelY);
						cc.Stroke();
					}
				}

				// 绘制柱状图
				if (hasValue)
				{
					double yFactor = -(bottom - top) / (maximum - minimum);
					double yBase = -maximum * yFactor;

					cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.DodgerBlue));
					for (int x = 0; x < samples.Length; x++)
					{
						float pixelY = (float)(samples[x].HistValue * yFactor + yBase);
						float rectX = 0, rectW = 0;
						if (samples.Length == 1)
                        {
							rectX = left + x * step + 0.25f * step;
							rectW = step - 0.5f * step;
						}
						else if (step > 8)
						{
							rectX = left + x * step + 2;
							rectW = step - 4;
						}
						else if (step > 4)
						{
							rectX = left + x * step + 1;
							rectW = step - 2;
						}
						else
						{
							rectX = left + x * step;
							rectW = step;
						}

						cc.Rectangle(rectX, (float)Math.Min(pixelY, yBase), rectW, (float)Math.Abs(pixelY - yBase));
						cc.Fill();
					}
				}

				// 绘制折线图
				if (hasValue && isEnableLine)
				{
					cc.LineWidth = 3;
					cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.Orange));

					double yFactor = -(bottom - top) / (maximum - minimum);
					double yBase = -maximum * yFactor;

					var linePts = new List<FloatPoint>();
					for (int x = 0; x < samples.Length; x++)
					{
						float pixelY = (float)(samples[x].LineValue * yFactor + yBase);
						linePts.Add(new FloatPoint() {X = (float)(left + (0.5 + x) * step), Y = pixelY});
					}

					if (linePts.Count >= 2)
					{
						cc.MoveTo(linePts[0].X, linePts[0].Y);
						for (int i = 1; i < linePts.Count; i++)
						{
							cc.LineTo(linePts[i].X, linePts[i].Y);
						}
						cc.Stroke();
					}

					cc.LineWidth = 1;
				}

				// 绘制坐标轴
				cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.Black));

				cc.MoveTo(left, bottom);
				cc.LineTo(left, top);
				cc.Stroke();

				cc.MoveTo(left, bottom);
				cc.LineTo(right, bottom);
				cc.Stroke();

				if (isEnableLine)
				{
					cc.MoveTo(right, bottom);
					cc.LineTo(right, top);
					cc.Stroke();
				}

				// 绘制x轴刻度值文字
				for (int i = 0; i < xGridTexts.Count; i++)
				{
					cc.MoveTo((float)(left + (0.5 + i) * step) - 0.5f * smallFontHeight + 1, (float)bottom + 2);
					cc.Rotate(0.5 * Math.PI);
					cc.ShowText(xGridTexts[i]);
					cc.Rotate(-0.5 * Math.PI);
				}

				// 绘制y轴刻度值文字
				if (hasValue)
				{
					double yFactor = -(bottom - top) / (maximum - minimum);
					double yBase = -maximum * yFactor;

					for (int i = 0; i < yGridTexts.Count; i++)
					{
						float pixelY = (float)(yGrids[i] * yFactor + yBase);
						cc.MoveTo(left - cc.TextExtents(yGridTexts[i]).Width - 2, (float)Math.Max(0, pixelY + 0.5f * smallFontHeight));
						cc.ShowText(yGridTexts[i]);
					}	
				}

				// 绘制x轴标题
				cc.SetFontSize(11);

				var xTitleWidth = cc.TextExtents(xTitle).Width;
				cc.MoveTo((float)(left + right - xTitleWidth) * 0.5f, drawHeight - 4);
				cc.ShowText(xTitle);

				// 绘制左y轴标题
				var histTitleWith = cc.TextExtents(histTitle).Width;
				cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.DodgerBlue));
				cc.MoveTo(4, (float)(top + bottom - histTitleWith) * 0.5f);
				cc.Rotate(0.5 * Math.PI);
				cc.ShowText(histTitle);
				cc.Rotate(-0.5 * Math.PI);

				// 绘制右y轴标题
				if (isEnableLine)
				{
					var lineTitleWith = cc.TextExtents(lineTitle).Width;
					cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.Orange));
					cc.MoveTo(drawWidth - 11, (float)(top + bottom - lineTitleWith) * 0.5f);
					cc.Rotate(0.5 * Math.PI);
					cc.ShowText(lineTitle);
					cc.Rotate(-0.5 * Math.PI);
				}

				if (hasValue)
				{
					cc.SetFontSize(10);
					
					// 获取鼠标所在格
					int mouseX, mouseY;
					draw.GetPointer(out mouseX, out mouseY);

					int mouseIndex = (int)((mouseX - left) / step);

					if (mouseIndex >= 0 && mouseIndex < samples.Length && mouseY >= top && mouseY <= bottom)
					{				
						// 绘制鼠标所在格
						cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.Gray));
						cc.Rectangle(left + mouseIndex * step - 0.5f, top, step, bottom - top);
						cc.Stroke();

						// 绘制鼠标所在格信息
						var target = samples[mouseIndex];

						String histText = null, lineText = null;
						histText = Math.Abs(target.HistValue) >= 0.1 ? target.HistValue.ToString("F3") : (new Decimal(target.HistValue)).ToString();
						if (isEnableLine) lineText = Math.Abs(target.LineValue) >= 0.1 ? target.LineValue.ToString("F3") : (new Decimal(target.LineValue)).ToString();

						var annotationText = "(" + target.Name + " : " + histText + (isEnableLine ? ", " + lineText : "") + ")";
						var annotationWidth = cc.TextExtents(annotationText).Width;
						var annotationX = (float)(mouseX - (mouseX - left) * annotationWidth / (right - left));
						cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.Black));
						cc.MoveTo(annotationX, mouseY - 8);
						cc.ShowText(annotationText);
					}

				}

				// 绘制验证曲线
				cc.LineWidth = 2;
				cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.LimeGreen));
				
				var ptList = new List<FloatPoint>();

				if (Data.Definition.Validation != null && hasValue)
				{
					if (Data.Definition.Validation is ValueAboveValidation)
					{
						var indices = (Data.Definition.Validation as ValueAboveValidation).GetHistLineValuesOKIndices((Data as HistAndLineData).GetXValuesOrLabels() as HistLineXValues);
						if (indices.Length > 0)
						{
							float thisLeft = left + step * indices[0];
							ptList.Add(new FloatPoint(thisLeft, top));
							ptList.Add(new FloatPoint(right, top));
							ptList.Add(new FloatPoint(right, bottom));
							ptList.Add(new FloatPoint(thisLeft, bottom));
						}
					}
					else if (Data.Definition.Validation is ValueBelowValidation)
					{
						var indices = (Data.Definition.Validation as ValueBelowValidation).GetHistLineValuesOKIndices((Data as HistAndLineData).GetXValuesOrLabels() as HistLineXValues);
						if (indices.Length > 0)
						{
							float thisRight = left + step * (indices.Last() + 1);
							ptList.Add(new FloatPoint(left, top));
							ptList.Add(new FloatPoint(thisRight, top));
							ptList.Add(new FloatPoint(thisRight, bottom));
							ptList.Add(new FloatPoint(left, bottom));
						}
					}
					else if (Data.Definition.Validation is PolyAboveValidation && Data.HasData())
					{
						var thresholds = (Data.Definition.Validation as PolyAboveValidation).GetHistLineValuesThreshold((Data as HistAndLineData).GetXValuesOrLabels() as HistLineXValues);
						for (int i = 0; i < thresholds.Length; i++)
						{
							thresholds[i] = Math.Max(minimum, Math.Min(maximum, thresholds[i]));
						}

						ptList.Add(new FloatPoint(right, top));
						ptList.Add(new FloatPoint(left, top));
						for (int i = 0; i < thresholds.Length; i++)
						{
							var th = thresholds[i];
							var thisX = left + step * i;
							var thisY = (float)(top + (bottom - top) * (maximum - (float)th) / (maximum - minimum));

							if (i == 0 || th != thresholds[i - 1]) ptList.Add(new FloatPoint(thisX, thisY));
							ptList.Add(new FloatPoint(thisX + step, thisY));
						}
					}
					else if (Data.Definition.Validation is PolyBelowValidation && Data.HasData())
					{
						var thresholds = (Data.Definition.Validation as PolyBelowValidation).GetHistLineValuesThreshold((Data as HistAndLineData).GetXValuesOrLabels() as HistLineXValues);
						for (int i = 0; i < thresholds.Length; i++)
						{
							thresholds[i] = Math.Max(minimum, Math.Min(maximum, thresholds[i]));
						}

						ptList.Add(new FloatPoint(right, bottom));
						ptList.Add(new FloatPoint(left, bottom));
						for (int i = 0; i < thresholds.Length; i++)
						{
							var th = thresholds[i];
							var thisX = left + step * i;
							var thisY = (float)(top + (bottom - top) * (maximum - (float)th) / (maximum - minimum));

							if (i == 0 || th != thresholds[i - 1]) ptList.Add(new FloatPoint(thisX, thisY));
							ptList.Add(new FloatPoint(thisX + step, thisY));
						}
					}
				}

				if (ptList.Count >= 2)
				{
					cc.MoveTo(ptList[0].X, ptList[0].Y);
					for (int i = 1; i < ptList.Count; i++) cc.LineTo(ptList[i].X, ptList[i].Y);
					cc.LineTo(ptList[0].X, ptList[0].Y);
					cc.Stroke();
				}
			}
			catch (Exception) {}
        }
    }
}
