using System;
using System.Drawing;
using ASEva.Graph;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=2.0.0) Single value graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=2.0.0) 单值数据可视化窗口
    /// </summary>
    public partial class ValueGraph : BaseGraph
    {
        public ValueGraph()
        {
            InitializeComponent();
        }

        public override bool IsHeightFixed()
        {
            return true;
        }

        public override void UpdateUIWithData()
        {
            // 标题显示
            label1.Text = Data == null ? "" : Data.Definition.MainTitle;
            if (Data == null || !(Data is SingleValueData) || !Data.HasData())
            {
                label2.ForeColor = Color.Black;
                label2.Text = "No Data.";
                label3.Text = "";
                return;
            }

            // 数据显示
            double val = 0;
            if (Data is SingleValueData)
            {
                val = (Data as SingleValueData).GetValue();
            }

            var valAbs = Math.Abs(val);
            var valAbsInt = (ulong)Math.Floor(valAbs);
            label2.Text = (val < 0 ? "-" : "") + valAbsInt + ".";

            long digits = (int)((valAbs - valAbsInt) * 1000000000);
            if (digits == 0 || digits == 1 || digits == 1000000000 - 1)
            {
                label3.Text = "0";
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
            label3.Text = target;

            // 验证条件显示
            if (Data.Definition.Validation == null)
            {
                label4.Text = "";
            }
            else
            {
                var vd = Data.Definition.Validation;
                if (vd is ValueBelowValidation)
                {
                    label4.Text = "≤ " + (vd as ValueBelowValidation).GetThreshold();
                }
                else if (vd is ValueAboveValidation)
                {
                    label4.Text = "≥ " + (vd as ValueAboveValidation).GetThreshold();
                }
                else if (vd is ValueInRangeValidation)
                {
                    double lower = 0, upper = 0;
                    (vd as ValueInRangeValidation).GetRange(out lower, out upper);
                    label4.Text = "[ " + lower + " , " + upper + " ]";
                }
            }

            // 验证结果显示
            double? dummy = null;
            var vdResult = Data.Validate(out dummy);
            if (vdResult == null)
            {
                label2.ForeColor = label3.ForeColor = Color.Black;
            }
            else if (vdResult.Value)
            {
                label2.ForeColor = label3.ForeColor = Color.Green;
            }
            else
            {
                label2.ForeColor = label3.ForeColor = Color.Red;
            }
        }

        private void ValueGraph_Click(object sender, EventArgs e)
        {
            HandleGraphSelected();
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
    }
}
