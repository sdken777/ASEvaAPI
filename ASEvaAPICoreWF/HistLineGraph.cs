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
    /// (api:corewf=3.0.0) Histogram and poly line graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.0.0) 柱状折线图可视化控件
    /// </summary>
	public partial class HistLineGraph : BaseGraph
	{
		public HistLineGraph()
		{
			InitializeComponent();
		}

		public override void UpdateUIWithData()
		{
			if (Data == null || !(Data is HistAndLineData)) return;

			// 数据和验证条件显示
			if (DrawBeat.CallerBegin(pictureBox1))
			{
				pictureBox1.Refresh();
				DrawBeat.CallerEnd(pictureBox1);
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
				label2.Text = percentage == null ? "" : (percentage.Value.ToString("F1") + "% OK");
			}
			else if (vdResult.Value)
			{
				label2.ForeColor = Color.Green;
				label2.Text = percentage == null ? "OK" : (percentage.Value.ToString("F1") + "% OK");
			}
			else
			{
				label2.ForeColor = Color.Red;
				label2.Text = percentage == null ? "NG" : (percentage.Value.ToString("F1") + "% OK");
			}
		}

		private void Graph_Click(object sender, EventArgs e)
		{
			HandleGraphSelected();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			HandleGraphSelected();
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			if (Data == null || !(Data is HistAndLineData)) return;

			DrawBeat.CallbackBegin(pictureBox1, "ASEva.UICoreWF.HistLineGraph");

			//try
			//{
				var g = e.Graphics;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

				var blackPen = new Pen(Color.Black);
				var blackBrush = new SolidBrush(Color.Black);
				var histBrush = new SolidBrush(Color.DodgerBlue);
				var lineBrush = new SolidBrush(Color.Orange);
				var linePen = new Pen(Color.Orange, 3);

				var largeFont = new Font("微软雅黑", 9f);
				var smallFont = new Font("微软雅黑", 8f);

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
					double ystep = (maximum - minimum) / Math.Max(5, pictureBox1.Height / 75);
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
				var yGridTexts = new List<String>();
				var yGridTextsMaxWidth = 0f;
				foreach (var y in yGrids)
				{
					var text = (new Decimal(y)).ToString();
					yGridTexts.Add(text);
					yGridTextsMaxWidth = Math.Max(yGridTextsMaxWidth, g.MeasureString(text, smallFont).Width);
				}
				yGridTextsMaxWidth = Math.Min(100, yGridTextsMaxWidth + 2);

				// 生成x轴刻度值文字
				var xGridTexts = new List<String>();
				var xGridTextsMaxWidth = 0f;
				foreach (var sample in samples)
				{
					var text = sample.Name;
					xGridTexts.Add(text);
					xGridTextsMaxWidth = Math.Max(xGridTextsMaxWidth, g.MeasureString(text, smallFont).Width);
				}
				xGridTextsMaxWidth = Math.Min(100, xGridTextsMaxWidth + 1);
				var smallFontHeight = g.MeasureString("A", smallFont).Height;

				// 计算坐标转换参数
				float left = (float)15 * DeviceDpi / 96 + yGridTextsMaxWidth;
				float right = (float)pictureBox1.Width - (isEnableLine ? 16 : 1) * DeviceDpi / 96;
				float top = 0;
				float bottom = (float)pictureBox1.Height - 15 * DeviceDpi / 96 - xGridTextsMaxWidth;
				float step = (right - left) / samples.Length;

				//double yFactor = -(bottom - top) / (maximum - minimum);
				//double yBase = -maximum * yFactor;

				// 绘制y轴刻度线
				if (hasValue)
				{
					double yFactor = -(bottom - top) / (maximum - minimum);
					double yBase = -maximum * yFactor;

					var yGridPen = new Pen(isEnableLine ? Color.Orange : Color.DodgerBlue);
					foreach (var val in yGrids)
					{
						float pixelY = (float)(val * yFactor + yBase);
						g.DrawLine(yGridPen, left, pixelY, right, pixelY);
					}
				}

				// 绘制柱状图
				if (hasValue)
				{
					double yFactor = -(bottom - top) / (maximum - minimum);
					double yBase = -maximum * yFactor;

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

						g.FillRectangle(histBrush, rectX, (float)Math.Min(pixelY, yBase), rectW, (float)Math.Abs(pixelY - yBase));
					}
				}

				// 绘制折线图
				if (hasValue && isEnableLine && maximum > minimum)
				{
					double yFactor = -(bottom - top) / (maximum - minimum);
					double yBase = -maximum * yFactor;

					var linePts = new List<PointF>();
					for (int x = 0; x < samples.Length; x++)
					{
						float pixelY = (float)(samples[x].LineValue * yFactor + yBase);
						linePts.Add(new PointF() { X = (float)(left + (0.5 + x) * step), Y = pixelY });
					}

					g.DrawLines(linePen, linePts.ToArray());
				}

				// 绘制坐标轴
				g.DrawLine(blackPen, left, bottom, left, top);
				g.DrawLine(blackPen, left, bottom, right, bottom);
				if (isEnableLine) g.DrawLine(blackPen, right, bottom, right, top);

				// 绘制x轴刻度值文字
				for (int i = 0; i < xGridTexts.Count; i++)
				{
					g.DrawString(xGridTexts[i], smallFont, blackBrush, (float)(left + (0.5 + i) * step) - 0.5f * smallFontHeight + 1, (float)bottom + 1, new StringFormat(StringFormatFlags.DirectionVertical));
				}

				// 绘制y轴刻度值文字
				if (hasValue)
				{
					double yFactor = -(bottom - top) / (maximum - minimum);
					double yBase = -maximum * yFactor;

					for (int i = 0; i < yGridTexts.Count; i++)
					{
						float pixelY = (float)(yGrids[i] * yFactor + yBase);
						g.DrawString(yGridTexts[i], smallFont, blackBrush, left - g.MeasureString(yGridTexts[i], smallFont).Width - 2, (float)Math.Max(0, pixelY - 0.5f * smallFontHeight));
					}
				}

				// 绘制x轴标题
				var xTitleWidth = g.MeasureString(xTitle, largeFont).Width;
				g.DrawString(xTitle, largeFont, blackBrush, (float)(left + right - xTitleWidth) * 0.5f, pictureBox1.Height - 15 * DeviceDpi / 96);

				// 绘制左y轴标题
				var histTitleWith = g.MeasureString(histTitle, largeFont).Width;
				g.DrawString(histTitle, largeFont, histBrush, 0, (float)(top + bottom - histTitleWith) * 0.5f, new StringFormat(StringFormatFlags.DirectionVertical));

				// 绘制右y轴标题
				if (isEnableLine)
				{
					var lineTitleWith = g.MeasureString(lineTitle, largeFont).Width;
					g.DrawString(lineTitle, largeFont, lineBrush, pictureBox1.Width - 15 * DeviceDpi / 96, (float)(top + bottom - lineTitleWith) * 0.5f, new StringFormat(StringFormatFlags.DirectionVertical));
				}

				if (hasValue)
				{
					// 获取鼠标所在格
					Point curPoint = pictureBox1.PointToClient(System.Windows.Forms.Cursor.Position);
					int mouseIndex = (int)((curPoint.X - left) / step);

					if (mouseIndex >= 0 && mouseIndex < samples.Length && curPoint.Y >= top && curPoint.Y <= bottom)
					{
						// 绘制鼠标所在格
						var annotationPen = new Pen(Color.Gray);
						g.DrawRectangle(annotationPen, left + mouseIndex * step - 0.5f, top, step, bottom - top);

						// 绘制鼠标所在格信息
						var target = samples[mouseIndex];

						String histText = null, lineText = null;
						histText = Math.Abs(target.HistValue) >= 0.1 ? target.HistValue.ToString("F3") : (new Decimal(target.HistValue)).ToString();
						if (isEnableLine) lineText = Math.Abs(target.LineValue) >= 0.1 ? target.LineValue.ToString("F3") : (new Decimal(target.LineValue)).ToString();

						var annotationText = "(" + target.Name + " : " + histText + (isEnableLine ? ", " + lineText : "") + ")";
						var annotationWidth = g.MeasureString(annotationText, smallFont).Width;
						var annotationX = (float)(curPoint.X - (curPoint.X - left) * annotationWidth / (right - left));
						g.DrawString(annotationText, smallFont, blackBrush, annotationX, curPoint.Y - 15);
					}

				}

				// 绘制验证曲线
				var pen = new Pen(Color.LimeGreen, 2);
				var ptList = new List<PointF>();

				if (Data.Definition.Validation != null && hasValue)
				{
					if (Data.Definition.Validation is ValueAboveValidation)
					{
						var indices = (Data.Definition.Validation as ValueAboveValidation).GetHistLineValuesOKIndices((Data as HistAndLineData).GetXValuesOrLabels() as HistLineXValues);
						if (indices.Length > 0)
						{
							float thisLeft = left + step * indices[0];
							ptList.Add(new PointF(thisLeft, top));
							ptList.Add(new PointF(right, top));
							ptList.Add(new PointF(right, bottom));
							ptList.Add(new PointF(thisLeft, bottom));
						}
					}
					else if (Data.Definition.Validation is ValueBelowValidation)
					{
						var indices = (Data.Definition.Validation as ValueBelowValidation).GetHistLineValuesOKIndices((Data as HistAndLineData).GetXValuesOrLabels() as HistLineXValues);
						if (indices.Length > 0)
						{
							float thisRight = left + step * (indices.Last() + 1);
							ptList.Add(new PointF(left, top));
							ptList.Add(new PointF(thisRight, top));
							ptList.Add(new PointF(thisRight, bottom));
							ptList.Add(new PointF(left, bottom));
						}
					}
					else if (Data.Definition.Validation is PolyAboveValidation)
					{
						var thresholds = (Data.Definition.Validation as PolyAboveValidation).GetHistLineValuesThreshold((Data as HistAndLineData).GetXValuesOrLabels() as HistLineXValues);
						for (int i = 0; i < thresholds.Length; i++)
						{
							thresholds[i] = Math.Max(minimum, Math.Min(maximum, thresholds[i]));
						}

						ptList.Add(new PointF(right, top));
						ptList.Add(new PointF(left, top));
						for (int i = 0; i < thresholds.Length; i++)
						{
							var th = thresholds[i];
							var thisX = left + step * i;
							var thisY = (float)(top + (bottom - top) * (maximum - (float)th) / (maximum - minimum));

							if (i == 0 || th != thresholds[i - 1]) ptList.Add(new PointF(thisX, thisY));
							ptList.Add(new PointF(thisX + step, thisY));
						}
					}
					else if (Data.Definition.Validation is PolyBelowValidation)
					{
						var thresholds = (Data.Definition.Validation as PolyBelowValidation).GetHistLineValuesThreshold((Data as HistAndLineData).GetXValuesOrLabels() as HistLineXValues);
						for (int i = 0; i < thresholds.Length; i++)
						{
							thresholds[i] = Math.Max(minimum, Math.Min(maximum, thresholds[i]));
						}

						ptList.Add(new PointF(right, bottom));
						ptList.Add(new PointF(left, bottom));
						for (int i = 0; i < thresholds.Length; i++)
						{
							var th = thresholds[i];
							var thisX = left + step * i;
							var thisY = (float)(top + (bottom - top) * (maximum - (float)th) / (maximum - minimum));

							if (i == 0 || th != thresholds[i - 1]) ptList.Add(new PointF(thisX, thisY));
							ptList.Add(new PointF(thisX + step, thisY));
						}
					}
				}

				if (ptList.Count > 0)
				{
					g.DrawPolygon(pen, ptList.ToArray());
				}
			//}
			//catch (Exception) { }

			DrawBeat.CallbackEnd(pictureBox1);
		}
	}
}
