using System;
using System.Threading;
using ASEva.Graph;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    class HistogramValueGraph : Panel, GraphPanel
    {
        public HistogramValueGraph()
        {
            BackgroundColor = Colors.White;

            var mainLayout = this.SetContentAsColumnLayout(2, 0);
            labelTitle = mainLayout.AddLabel("");
            labelSubTitle = mainLayout.AddLabel("");
            var rowLayout = mainLayout.AddRowLayout(true, 0, VerticalAlignment.Bottom);
            labelValidation = rowLayout.AddLabel("", TextAlignment.Left);
            labelValue1 = rowLayout.AddLabel("", TextAlignment.Right, true);
            labelValue1.Font = App.DefaultFont(1.4f);
            labelValue2 = rowLayout.AddLabel("", TextAlignment.Left, false, 90);
            
            MouseDown += delegate { click.Set(); };
        }
        
        public int? GetFixedHeight()
        {
            return 61;
        }

        public void ReleaseResources()
        {
        }

        public void UseClickEvent(ManualResetEventSlim ev)
        {
            click = ev;
        }

        public void UpdateWithGraphData(GraphData data)
        {
            // 验证
            var mainTitle = data == null ? "" : data.Definition.MainTitle;
            if (data == null || !(data is HistAndLineData) || !data.HasData())
            {
                labelTitle.Text = mainTitle;
                labelSubTitle.Text = "";
                labelValue1.TextColor = labelValue2.TextColor = Colors.Black;
                labelValue1.Text = "No data.";
                labelValue2.Text = "";
                return;
            }

            // 标题显示
            var histLineData = data as HistAndLineData;
            var xTitle = histLineData.GetXTitle();
            var histTitle = histLineData.GetHistTitle();
            var lineTitle = histLineData.GetLineTitle();
            var withLine = histLineData.IsLineEnabled();
            var curIndex = (int)((DateTime.Now - t0).TotalSeconds / 3); // 3秒切换
            var lineTarget = withLine && curIndex % 2 == 1;

            var xValuesOrLabels = histLineData.GetXValuesOrLabels();
            var targetIndex = -1;
            var indexTitle = "";
            if (xValuesOrLabels is HistLineXValues)
            {
                var xValues = xValuesOrLabels as HistLineXValues;
                targetIndex = curIndex / (withLine ? 2 : 1) % xValues.Count;
                
                var lower = xValues.Base + targetIndex * xValues.Step;
                var upper = xValues.Base + (targetIndex + 1) * xValues.Step;
                indexTitle = ((decimal)lower).ToString() + "~" + ((decimal)upper).ToString();
            }
            else // HistLineXLabels
            {
                var xLabels = xValuesOrLabels as HistLineXLabels;
                targetIndex = curIndex / (withLine ? 2 : 1) % xLabels.Labels.Length;
                indexTitle = xLabels.Labels[targetIndex];
            }

            labelTitle.Text = mainTitle + " : " + (lineTarget ? lineTitle : histTitle);
            labelSubTitle.Text = xTitle + " = " + indexTitle;

            // 数据显示
            var sample = histLineData.GetSamples()[targetIndex];
            var val = lineTarget ? sample.LineValue : sample.HistValue;

            var valAbs = Math.Abs(val);
            var valAbsInt = (ulong)Math.Floor(valAbs);
            labelValue1.Text = (val < 0 ? "-" : "") + valAbsInt + ".";

            long digits = (int)((valAbs - valAbsInt) * 1000000000);
            if (digits == 0 || digits == 1 || digits == 1000000000 - 1)
            {
                labelValue2.Text = "0";
                return;
            }

            long[] src = new long[] { digits, digits + 1, digits - 1 };
            String[] dst = new String[3];
            for (int i = 0; i < 3; i++)
            {
                dst[i] = trimDigits(src[i]);
            }

            String target = dst[0];
            if (dst[1].Length < target.Length) target = dst[1];
            if (dst[2].Length < target.Length) target = dst[2];
            labelValue2.Text = target;

            // 验证条件显示
            bool? vdResult = null;
            if (data.Definition.Validation == null || !(xValuesOrLabels is HistLineXValues))
            {
                labelValidation.Text = "";
            }
            else
            {
                var vd = data.Definition.Validation;
                var xValues = xValuesOrLabels as HistLineXValues;
                if (vd is ValueBelowValidation && xValuesOrLabels is HistLineXValues)
                {
                    var threshold = (vd as ValueBelowValidation).GetThreshold();
                    labelValidation.Text = xTitle + " ≤ " + threshold;
                    vdResult = xValues.Base + (targetIndex + 1) * xValues.Step <= threshold;
                }
                else if (vd is ValueAboveValidation && xValuesOrLabels is HistLineXValues)
                {
                    var threshold = (vd as ValueAboveValidation).GetThreshold();
                    labelValidation.Text = xTitle + " ≥ " + threshold;
                    vdResult = xValues.Base + targetIndex * xValues.Step >= threshold;
                }
                else if (vd is PolyBelowValidation && xValuesOrLabels is HistLineXValues)
                {
                    var thresholds = (vd as PolyBelowValidation).GetHistLineValuesThreshold(xValues);
                    labelValidation.Text = "≤ " + thresholds[targetIndex];
                    vdResult = val <= thresholds[targetIndex];
                }
                else if (vd is PolyAboveValidation && xValuesOrLabels is HistLineXValues)
                {
                    var thresholds = (vd as PolyAboveValidation).GetHistLineValuesThreshold(xValues);
                    labelValidation.Text = "≥ " + thresholds[targetIndex];
                    vdResult = val >= thresholds[targetIndex];
                }
            }

            // 验证结果显示
            if (vdResult == null)
            {
                labelValue1.TextColor = labelValue2.TextColor = Colors.Black;
            }
            else if (vdResult.Value)
            {
                labelValue1.TextColor = labelValue2.TextColor = Colors.Green;
            }
            else
            {
                labelValue1.TextColor = labelValue2.TextColor = Colors.Red;
            }
        }

        private String trimDigits(long src)
        {
            int zeroCount = 0;
            while (true)
            {
                src *= 10;
                if (src >= 1000000000) break;
                zeroCount++;
            }
            while (src != 0 && src % 10 == 0) src /= 10;
            String text = "";
            for (int i = 0; i < zeroCount; i++) text = text + "0";
            return text + src;
        }

        private Label labelTitle, labelSubTitle, labelValue1, labelValue2, labelValidation;
        private ManualResetEventSlim click;
        private DateTime t0 = DateTime.Now;
    }
}