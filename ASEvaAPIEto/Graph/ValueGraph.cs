using System;
using System.Threading;
using ASEva.Graph;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    class ValueGraph : Panel, GraphPanel
    {
        public ValueGraph()
        {
            BackgroundColor = Colors.White;

            var mainLayout = this.SetContentAsColumnLayout(4, 0);
            labelTitle = mainLayout.AddLabel("");
            var rowLayout = mainLayout.AddRowLayout(true, 0, VerticalAlignment.Bottom);
            labelValidation = rowLayout.AddLabel("", TextAlignment.Left);
            labelValue1 = rowLayout.AddLabel("", TextAlignment.Right, true);
            labelValue1.Font = App.DefaultFont(2);
            labelValue2 = rowLayout.AddLabel("", TextAlignment.Left, false, 90);
            labelValue2.Font = App.DefaultFont(1.4f);
            
            MouseDown += delegate { click?.Set(); };
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
            // 标题显示
            labelTitle.Text = data == null ? "" : data.Definition.MainTitle;
            if (data == null || !(data is SingleValueData singleValueData) || !data.HasData())
            {
                labelValue1.TextColor = Colors.Black;
                labelValue1.Text = "No data.";
                labelValue2.Text = "";
                return;
            }

            // 数据显示
            double val = singleValueData.GetValue();

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
            if (data.Definition.Validation == null)
            {
                labelValidation.Text = "";
            }
            else
            {
                var vd = data.Definition.Validation;
                if (vd is ValueBelowValidation vbv)
                {
                    labelValidation.Text = "≤ " + vbv.GetThreshold();
                }
                else if (vd is ValueAboveValidation vav)
                {
                    labelValidation.Text = "≥ " + vav.GetThreshold();
                }
                else if (vd is ValueInRangeValidation vrv)
                {
                    double lower = 0, upper = 0;
                    vrv.GetRange(out lower, out upper);
                    labelValidation.Text = "[ " + lower + " , " + upper + " ]";
                }
            }

            // 验证结果显示
            double? dummy = null;
            var vdResult = data.Validate(out dummy);
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

        private Label labelTitle, labelValue1, labelValue2, labelValidation;
        private ManualResetEventSlim? click;
    }
}