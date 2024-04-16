using System;
using Gtk;
using ASEva.Graph;
using UI = Gtk.Builder.ObjectAttribute;
using Gdk;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649

    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Single value graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 单值数据可视化窗口
    /// </summary>
    public class ValueGraph : BaseGraph
    {
        [UI] EventBox eventBox;
        [UI] Label labelTitle, labelValidation, labelValueMain, labelValueSub;

        EventBoxHelper eventBoxHelper = new EventBoxHelper();

        public ValueGraph() : this(new Builder("ValueGraph.glade"))
        {
            eventBoxHelper.Add(eventBox);

            this.SetBackColor(ColorRGBA.White);

            eventBoxHelper.LeftDown += eventBoxHelper_LeftDown;
        }

        private ValueGraph(Builder builder) : base(builder.GetRawOwnedObject("ValueGraph"))
        {
            builder.Autoconnect(this);
        }

        public override bool IsHeightFixed()
        {
            return true;
        }

        public override void UpdateUIWithData()
        {
            // 标题显示
            labelTitle.Text = Data == null ? "" : Data.Definition.MainTitle;
            if (Data == null || !(Data is SingleValueData) || !Data.HasData())
            {
                labelValueMain.SetForeColor(ColorRGBA.Black);
                labelValueMain.Text = "No Data.";
                labelValueSub.Text = "";
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
            labelValueMain.Text = (val < 0 ? "-" : "") + valAbsInt + ".";

            long digits = (int)((valAbs - valAbsInt) * 1000000000);
            if (digits == 0 || digits == 1 || digits == 1000000000 - 1)
            {
                labelValueSub.Text = "0";
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
            labelValueSub.Text = target;

            // 验证条件显示
            if (Data.Definition.Validation == null)
            {
                labelValidation.Text = "";
            }
            else
            {
                var vd = Data.Definition.Validation;
                if (vd is ValueBelowValidation)
                {
                    labelValidation.Text = "≤ " + (vd as ValueBelowValidation).GetThreshold();
                }
                else if (vd is ValueAboveValidation)
                {
                    labelValidation.Text = "≥ " + (vd as ValueAboveValidation).GetThreshold();
                }
                else if (vd is ValueInRangeValidation)
                {
                    double lower = 0, upper = 0;
                    (vd as ValueInRangeValidation).GetRange(out lower, out upper);
                    labelValidation.Text = "[ " + lower + " , " + upper + " ]";
                }
            }

            // 验证结果显示
            double? dummy = null;
            var vdResult = Data.Validate(out dummy);
            if (vdResult == null)
            {
                labelValueMain.SetForeColor(ColorRGBA.Black);
                labelValueSub.SetForeColor(ColorRGBA.Black);
            }
            else if (vdResult.Value)
            {
                labelValueMain.SetForeColor(ColorRGBA.Green);
                labelValueSub.SetForeColor(ColorRGBA.Green);
            }
            else
            {
                labelValueMain.SetForeColor(ColorRGBA.Red);
                labelValueSub.SetForeColor(ColorRGBA.Red);
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

        private void eventBoxHelper_LeftDown(EventBox box, EventButton ev)
        {
            HandleGraphSelected();
        }
    }
}
