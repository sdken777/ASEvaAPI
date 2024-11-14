using System;

namespace ASEva.Graph
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Mode of statistics for histogram and poly line
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 直方折线图统计模式
    /// </summary>
    public enum HistLineMode
    {
        /// \~English
        /// <summary>
        /// Sum for histogram, supporting the validations: ValueBelow, ValueAbove, PolyBelow, PolyAbove
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 求和直方图，图表数据验证支持ValueBelow, ValueAbove, PolyBelow, PolyAbove
        /// </summary>
        Sum,

        /// \~English
        /// <summary>
        /// Average value for histogram, supporting the validations: PolyBelow, PolyAbove
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 平均值直方图，图表数据验证支持PolyBelow, PolyAbove
        /// </summary>
        Aver,

        /// \~English
        /// <summary>
        /// Minimum value for histogram, supporting the validations: PolyBelow, PolyAbove
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最小值直方图，图表数据验证支持PolyBelow, PolyAbove
        /// </summary>
        Min,

        /// \~English
        /// <summary>
        /// Maximum value for histogram, supporting the validations: PolyBelow, PolyAbove
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最大值直方图，图表数据验证支持PolyBelow, PolyAbove
        /// </summary>
        Max,

        /// \~English
        /// <summary>
        /// Percentage for histogram, supporting the validations: ValueBelow, ValueAbove, PolyBelow, PolyAbove
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 百分比直方图，图表数据验证支持ValueBelow, ValueAbove, PolyBelow, PolyAbove
        /// </summary>
        Percentage,

        /// \~English
        /// <summary>
        /// Sum for both histogram and poly line, no validation supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 求和直方图，求和折线图，图表数据验证无效
        /// </summary>
        SumAndSum,

        /// \~English
        /// <summary>
        /// Minimum value for both histogram and poly line, no validation supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最小值直方图，最小值折线图，图表数据验证无效
        /// </summary>
        MinAndMin,

        /// \~English
        /// <summary>
        /// Minimum value for histogram and maximum value for poly line, no validation supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最小值直方图，最大值折线图，图表数据验证无效
        /// </summary>
        MinAndMax,

        /// \~English
        /// <summary>
        /// Maximum value for both histogram and poly line, no validation supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最大值直方图，最大值折线图，图表数据验证无效
        /// </summary>
        MaxAndMax,

        /// \~English
        /// <summary>
        /// Average value for histogram and minimum value for poly line, no validation supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 平均值直方图，最小值折线图，图表数据验证无效
        /// </summary>
        AverAndMin,

        /// \~English
        /// <summary>
        /// Average value for histogram and maximum value for poly line, no validation supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 平均值直方图，最大值折线图，图表数据验证无效
        /// </summary>
        AverAndMax,

        /// \~English
        /// <summary>
        /// Average value for both histogram and poly line, no validation supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 平均值直方图，平均值折线图，图表数据验证无效
        /// </summary>
        AverAndAver,

        /// \~English
        /// <summary>
        /// Average value for histogram and standard deviation value for poly line, no validation supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 平均值直方图，标准差折线图，图表数据验证无效
        /// </summary>
        AverAndDev,

        /// \~English
        /// <summary>
        /// Hit ratio for histogram, supporting the validations: PolyBelow, PolyAbove
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 命中率直方图，图表数据验证支持PolyBelow, PolyAbove
        /// </summary>
        HitRatio,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Numerical definition of histogram and poly line graph's X-axis
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 直方折线图中X轴数值描述
    /// </summary>
    public class HistLineXValues //（Validation supported / 图表数据验证有效）
    {
        public double Base { get; set; }
        public double Step { get; set; }
        public int Count { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Literal definition of histogram and poly line graph's X-axis
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 直方折线图中X轴文字描述
    /// </summary>
    public class HistLineXLabels //（Validation unsupported / 图表数据验证无效）
    {
        public String[] Labels { get; set; }

        public HistLineXLabels()
        {
            Labels = [];
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Sample of histogram and poly line graph
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 直方折线图中的样本
    /// </summary>
    public class HistLineSample
    {
        public String Name { get; set; }
        public double HistValue { get; set; }
        public double LineValue { get; set; }

        public HistLineSample()
        {
            Name = "";
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Histogram and poly line graph data
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 直方折线图数据
    /// </summary>
    public class HistAndLineData : GraphData
    {

        /// \~English
        /// <summary>
        /// Create graph definition (without validation)
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="xTitle">X axis title</param>
        /// <param name="xValuesOrLabels">X axis definition</param>
        /// <param name="mode">Mode of statistics</param>
        /// <param name="histTitle">Histogram title</param>
        /// <param name="defaultHistValue">Default value for histogram</param>
        /// <param name="lineTitle">Poly line title</param>
        /// <param name="defaultLineValue">Default value for poly line</param>
        /// <returns>Graph definition object</returns>
        /// \~Chinese
        /// <summary>
        /// 创建图表定义（不带数据验证）
        /// </summary>
        /// <param name="title">图表标题</param>
        /// <param name="xTitle">x轴标题</param>
        /// <param name="xValuesOrLabels">x轴的值或文字描述</param>
        /// <param name="mode">图表统计模式</param>
        /// <param name="histTitle">柱状图标题</param>
        /// <param name="defaultHistValue">柱状图初始值</param>
        /// <param name="lineTitle">折线图标题</param>
        /// <param name="defaultLineValue">折线图初始值</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition? CreateDefinition(String title, String xTitle, object xValuesOrLabels, HistLineMode mode, String histTitle, double defaultHistValue = 0, String? lineTitle = null, double defaultLineValue = 0)
        {
            return CreateDefinitionWithValidation(title, xTitle, xValuesOrLabels, mode, null, histTitle, defaultHistValue, lineTitle, defaultLineValue);
        }

        /// \~English
        /// <summary>
        /// Create graph definition
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="xTitle">X axis title</param>
        /// <param name="xValuesOrLabels">X axis definition</param>
        /// <param name="mode">Mode of statistics</param>
        /// <param name="validation">Graph validation, null means no validation. For details about the support types in different modes, see ASEva.Graph.HistLineMode </param>
        /// <param name="histTitle">Histogram title</param>
        /// <param name="defaultHistValue">Default value for histogram</param>
        /// <param name="lineTitle">Poly line title</param>
        /// <param name="defaultLineValue">Default value for poly line</param>
        /// <returns>Graph definition object</returns>
        /// \~Chinese
        /// <summary>
        /// 创建带数据验证的图表定义
        /// </summary>
        /// <param name="title">图表标题</param>
        /// <param name="xTitle">x轴标题</param>
        /// <param name="xValuesOrLabels">x轴的值或文字描述</param>
        /// <param name="mode">图表统计模式</param>
        /// <param name="validation">数据验证方式，null表示不验证。不同模式下的支持类型请参考 ASEva.Graph.HistLineMode </param>
        /// <param name="histTitle">柱状图标题</param>
        /// <param name="defaultHistValue">柱状图初始值</param>
        /// <param name="lineTitle">折线图标题</param>
        /// <param name="defaultLineValue">折线图初始值</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition? CreateDefinitionWithValidation(String title, String xTitle, object xValuesOrLabels, HistLineMode mode, GraphValidation? validation, String histTitle, double defaultHistValue = 0, String? lineTitle = null, double defaultLineValue = 0)
        {
            if (!IsHistogramOnlyMode(mode) && (lineTitle == null || lineTitle.Length == 0)) return null;

            var def = new GraphDefinition();
            def.Type = GraphType.HistAndLine;
            def.MainTitle = title;

            def.Config.Add(mode.ToString()); // 0
            def.Config.Add(xTitle); // 1
            def.Config.Add(defaultHistValue.ToString()); // 2
            def.Config.Add(defaultLineValue.ToString()); // 3
            if (xValuesOrLabels is HistLineXValues values)
            {
                def.Config.Add("XValues"); // 4
                def.Config.Add(values.Base.ToString()); // 5
                def.Config.Add(values.Step.ToString()); // 6
                def.Config.Add(values.Count.ToString()); // 7
            }
            else if (xValuesOrLabels is HistLineXLabels labels)
            {
                def.Config.Add("XLabels"); // 4
                foreach (var label in labels.Labels)
                {
                    def.Config.Add(label); // from 5
                }
            }
            else return null;

            def.ColumnTitles.Add(histTitle); // 0
            def.ColumnTitles.Add("Aux"); // 1
            if (!IsHistogramOnlyMode(mode))
            {
                def.ColumnTitles.Add(lineTitle); // 2
                def.ColumnTitles.Add("Aux"); // 3
            }

            if (validation != null && xValuesOrLabels is HistLineXValues)
            {
                var vdType = validation.GetValidationType();
                if (mode == HistLineMode.Sum || mode == HistLineMode.Percentage)
                {
                    if (vdType == GraphValidationType.ValueBelow ||
                        vdType == GraphValidationType.ValueAbove ||
                        vdType == GraphValidationType.PolyAbove ||
                        vdType == GraphValidationType.PolyBelow) def.Validation = validation;
                }
                else if (mode == HistLineMode.Aver || mode == HistLineMode.HitRatio || mode == HistLineMode.Min || mode == HistLineMode.Max)
                {
                    if (vdType == GraphValidationType.PolyAbove ||
                        vdType == GraphValidationType.PolyBelow) def.Validation = validation;
                }
            }

            return def;
        }

        /// \~English
        /// <summary>
        /// Get X axis title
        /// </summary>
        /// <returns>X axis title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取x轴标题
        /// </summary>
        /// <returns>x轴标题</returns>
        public String GetXTitle()
        {
            return Definition.Config[1];
        }

        /// \~English
        /// <summary>
        /// Get histogram title
        /// </summary>
        /// <returns>Histogram title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取柱状图标题
        /// </summary>
        /// <returns>柱状图标题</returns>
        public String GetHistTitle()
        {
            return Definition.ColumnTitles[0] ?? "";
        }

        /// \~English
        /// <summary>
        /// Get poly line title
        /// </summary>
        /// <returns>Poly line title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取折线图标题
        /// </summary>
        /// <returns>折线图标题</returns>
        public String? GetLineTitle()
        {
            return Definition.ColumnTitles.Count > 2 ? Definition.ColumnTitles[2] : null;
        }

        /// \~English
        /// <summary>
        /// Get whether poly line is enabled
        /// </summary>
        /// <returns>Whether poly line is enabled</returns>
        /// \~Chinese
        /// <summary>
        /// 获取折线图是否启用
        /// </summary>
        /// <returns>折线图是否启用</returns>
        public bool IsLineEnabled()
        {
            var mode = (HistLineMode)Enum.Parse(typeof(HistLineMode), Definition.Config[0]);
            return !IsHistogramOnlyMode(mode);
        }

        /// \~English
        /// <summary>
        /// Get X axis definition
        /// </summary>
        /// <returns>X axis definition</returns>
        /// \~Chinese
        /// <summary>
        /// 获取x轴的值或文字描述
        /// </summary>
        /// <returns>x轴的值或文字描述</returns>
        public object? GetXValuesOrLabels()
        {
            if (Definition.Config[4] == "XValues")
            {
                double b, s;
                int c;
                if (Double.TryParse(Definition.Config[5], out b) && Double.TryParse(Definition.Config[6], out s) && Int32.TryParse(Definition.Config[7], out c))
                {
                    var output = new HistLineXValues();
                    output.Base = b;
                    output.Step = s;
                    output.Count = c;
                    return output;
                }
                else return null;
            }
            else
            {
                var output = new HistLineXLabels();
                output.Labels = new string[Definition.Config.Count - 5];
                for (int i = 0; i < output.Labels.Length; i++)
                {
                    output.Labels[i] = Definition.Config[i + 5];
                }
                return output;
            }
        }

        /// \~English
        /// <summary>
        /// Add 1 in the interval where x is
        /// </summary>
        /// <param name="xValue">Input x, do nothing if it's over the range</param>
        /// \~Chinese
        /// <summary>
        /// 在输入x的所在区间添加1
        /// </summary>
        /// <param name="xValue">输入的x值，若超出x轴范围则不添加</param>
        public void AddTime(double xValue)
        {
            add(xValue, 0, 0, 1, false, AddMode.AddTime, false);
        }

        /// \~English
        /// <summary>
        /// Add 1 in the interval with the index
        /// </summary>
        /// <param name="xLabelIndex">Input index, do nothing if it's over the range</param>
        /// \~Chinese
        /// <summary>
        /// 在输入文本序号对应的区间添加1
        /// </summary>
        /// <param name="xLabelIndex">输入的x轴文本序号，若超出x轴范围则不添加</param>
        public void AddTime(int xLabelIndex)
        {
            add(xLabelIndex, 0, 0, 1, true, AddMode.AddTime, false);
        }

        /// \~English
        /// <summary>
        /// Add hits in the interval where x is
        /// </summary>
        /// <param name="xValue">Input x, do nothing if it's over the range</param>
        /// <param name="hit">Whether hit</param>
        /// <param name="times">Add times</param>
        /// \~Chinese
        /// <summary>
        /// 在输入x的所在区间添加若干次命中或不命中
        /// </summary>
        /// <param name="xValue">输入的x值，若超出x轴范围则不添加</param>
        /// <param name="hit">是否命中</param>
        /// <param name="times">添加次数</param>
        public void AddHits(double xValue, bool hit, uint times)
        {
            add(xValue, hit ? 1 : 0, 0, times, false, AddMode.AddHit, false);
        }

        /// \~English
        /// <summary>
        /// Add hits in the interval of the index
        /// </summary>
        /// <param name="xLabelIndex">Input index, do nothing if it's over the range</param>
        /// <param name="hit">Whether hit</param>
        /// <param name="times">Add times</param>
        /// \~Chinese
        /// <summary>
        /// 在输入文本序号对应的区间添加若干次命中或不命中
        /// </summary>
        /// <param name="xLabelIndex">输入的x轴文本序号，若超出x轴范围则不添加</param>
        /// <param name="hit">是否命中</param>
        /// <param name="times">添加次数</param>
        public void AddHits(int xLabelIndex, bool hit, uint times)
        {
            add(xLabelIndex, hit ? 1 : 0, 0, times, true, AddMode.AddHit, false);
        }

        /// \~English
        /// <summary>
        /// Add sample to the interval where x is
        /// </summary>
        /// <param name="xValue">Input x, do nothing if it's over the range</param>
        /// <param name="yValue">Sample for histogram</param>
        /// \~Chinese
        /// <summary>
        /// 在输入x的所在区间添加柱状图数值样本
        /// </summary>
        /// <param name="xValue">输入的x值，若超出x轴范围则不添加</param>
        /// <param name="yValue">柱状图数值样本</param>
        public void AddSample(double xValue, double yValue)
        {
            add(xValue, yValue, 0, 1, false, AddMode.AddNumber, false);
        }

        /// \~English
        /// <summary>
        /// Add samples to the interval where x is
        /// </summary>
        /// <param name="xValue">Input x, do nothing if it's over the range</param>
        /// <param name="histValue">Sample for histogram</param>
        /// <param name="lineValue">Sample for poly line</param>
        /// \~Chinese
        /// <summary>
        /// 在输入x的所在区间添加柱状图和折线图数值样本
        /// </summary>
        /// <param name="xValue">输入的x值，若超出x轴范围则不添加</param>
        /// <param name="histValue">柱状图数值样本</param>
        /// <param name="lineValue">折线图数值样本</param>
        public void AddSample(double xValue, double histValue, double lineValue)
        {
            add(xValue, histValue, lineValue, 1, false, AddMode.AddNumber, true);
        }

        /// \~English
        /// <summary>
        /// Add sample to the interval of the index
        /// </summary>
        /// <param name="xLabelIndex">Input index, do nothing if it's over the range</param>
        /// <param name="yValue">Sample for histogram</param>
        /// \~Chinese
        /// <summary>
        /// 在输入文本序号对应的区间添加柱状图数值样本
        /// </summary>
        /// <param name="xLabelIndex">输入的x轴文本序号，若超出x轴范围则不添加</param>
        /// <param name="yValue">柱状图数值样本</param>
        public void AddSample(int xLabelIndex, double yValue)
        {
            add(xLabelIndex, yValue, 0, 1, true, AddMode.AddNumber, false);
        }

        /// \~English
        /// <summary>
        /// Add samples to the interval of the index
        /// </summary>
        /// <param name="xLabelIndex">Input index, do nothing if it's over the range</param>
        /// <param name="histValue">Sample for histogram</param>
        /// <param name="lineValue">Sample for poly line</param>
        /// \~Chinese
        /// <summary>
        /// 在输入文本序号对应的区间添加柱状图和折线图数值样本
        /// </summary>
        /// <param name="xLabelIndex">输入的x轴文本序号，若超出x轴范围则不添加</param>
        /// <param name="histValue">柱状图数值样本</param>
        /// <param name="lineValue">折线图数值样本</param>
        public void AddSample(int xLabelIndex, double histValue, double lineValue)
        {
            add(xLabelIndex, histValue, lineValue, 1, true, AddMode.AddNumber, true);
        }

        /// \~English
        /// <summary>
        /// Get statistical data
        /// </summary>
        /// <returns>Statistical data</returns>
        /// \~Chinese
        /// <summary>
        /// 获取统计数据
        /// </summary>
        /// <returns>统计数据</returns>
        public HistLineSample[] GetSamples()
        {
            var samples = new HistLineSample[Data.GetLength(0)];
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = new HistLineSample();
            }

            if (Definition.Config[4] == "XLabels")
            {
                for (int i = 0; i < samples.Length; i++)
                {
                    samples[i].Name = Definition.Config[5 + i];
                }
            }
            else
            {
                double baseDouble = 0, stepDouble = 0;
                Double.TryParse(Definition.Config[5], out baseDouble);
                Double.TryParse(Definition.Config[6], out stepDouble);
                var baseDecimal = new Decimal(baseDouble);
                var stepDecimal = new Decimal(stepDouble);
                for (int i = 0; i < samples.Length; i++)
                {
                    var lower = baseDecimal + stepDecimal * i;
                    var upper = baseDecimal + stepDecimal * (i + 1);
                    samples[i].Name = lower + "~" + upper;
                }
            }

            double k = 1;
            var mode = (HistLineMode)Enum.Parse(typeof(HistLineMode), Definition.Config[0]);
            if (mode == HistLineMode.Percentage)
            {
                double sum = 0;
                for (int i = 0; i < samples.Length; i++)
                {
                    sum += Data[i, 0];
                }
                k = sum == 0 ? 0 : (100 / sum);
            }

            bool hasLine = Data.GetLength(1) >= 4;
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i].HistValue = Data[i, 0] * k;
                if (hasLine) samples[i].LineValue = Data[i, 2];
            }

            return samples;
        }

        public override void InitParamsAndData()
        {
            var rows = 0;
            if (Definition.Config[4] == "XValues")
            {
                Int32.TryParse(Definition.Config[7], out rows);
            }
            else if (Definition.Config[4] == "XLabels")
            {
                rows = Definition.Config.Count - 5;
            }

            var lineEnabled = IsLineEnabled();
            Data = new double[rows, lineEnabled ? 4 : 2];

            double defaultHistValue = 0, defaultLineValue = 0;
            Double.TryParse(Definition.Config[2], out defaultHistValue);
            Double.TryParse(Definition.Config[3], out defaultLineValue);
            for (int i = 0; i < rows; i++)
            {
                Data[i, 0] = defaultHistValue;
                if (lineEnabled) Data[i, 2] = defaultLineValue;
            }
        }

        public override void MergeWith(GraphData data)
        {
            var rows = Data.GetLength(0);

            var mode = (HistLineMode)Enum.Parse(typeof(HistLineMode), Definition.Config[0]);
            switch (mode)
            {
                case HistLineMode.Sum:
                case HistLineMode.Percentage:
                    for (int i = 0; i < rows; i++)
                    {
                        Data[i, 0] += data.Data[i, 0];
                    }
                    break;
                case HistLineMode.Aver:
                case HistLineMode.HitRatio:
                    for (int i = 0; i < rows; i++)
                    {
                        if (Data[i, 1] + data.Data[i, 1] == 0) Data[i, 0] = 0;
                        else Data[i, 0] = (Data[i, 0] * Data[i, 1] + data.Data[i, 0] * data.Data[i, 1]) / (Data[i, 1] + data.Data[i, 1]);
                        Data[i, 1] += data.Data[i, 1];
                    }
                    break;
                case HistLineMode.Min:
                    for (int i = 0; i < rows; i++)
                    {
                        if (Data[i, 1] == 0 || (data.Data[i, 1] > 0 && data.Data[i, 0] < Data[i, 0]))
                        {
                            Data[i, 0] = data.Data[i, 0];
                            Data[i, 1] = data.Data[i, 1];
                        }
                    }
                    break;
                case HistLineMode.Max:
                    for (int i = 0; i < rows; i++)
                    {
                        if (Data[i, 1] == 0 || (data.Data[i, 1] > 0 && data.Data[i, 0] > Data[i, 0]))
                        {
                            Data[i, 0] = data.Data[i, 0];
                            Data[i, 1] = data.Data[i, 1];
                        }
                    }
                    break;
                case HistLineMode.SumAndSum:
                    for (int i = 0; i < rows; i++)
                    {
                        Data[i, 0] += data.Data[i, 0];
                        Data[i, 2] += data.Data[i, 2];
                    }
                    break;
                case HistLineMode.MinAndMin:
                    for (int i = 0; i < rows; i++)
                    {
                        if (Data[i, 1] == 0 || (data.Data[i, 1] > 0 && data.Data[i, 0] < Data[i, 0]))
                        {
                            Data[i, 0] = data.Data[i, 0];
                            Data[i, 1] = data.Data[i, 1];
                        }
                        if (Data[i, 3] == 0 || (data.Data[i, 3] > 0 && data.Data[i, 2] < Data[i, 2]))
                        {
                            Data[i, 2] = data.Data[i, 2];
                            Data[i, 3] = data.Data[i, 3];
                        }
                    }
                    break;
                case HistLineMode.MinAndMax:
                    for (int i = 0; i < rows; i++)
                    {
                        if (Data[i, 1] == 0 || (data.Data[i, 1] > 0 && data.Data[i, 0] < Data[i, 0]))
                        {
                            Data[i, 0] = data.Data[i, 0];
                            Data[i, 1] = data.Data[i, 1];
                        }
                        if (Data[i, 3] == 0 || (data.Data[i, 3] > 0 && data.Data[i, 2] > Data[i, 2]))
                        {
                            Data[i, 2] = data.Data[i, 2];
                            Data[i, 3] = data.Data[i, 3];
                        }
                    }
                    break;
                case HistLineMode.MaxAndMax:
                    for (int i = 0; i < rows; i++)
                    {
                        if (Data[i, 1] == 0 || (data.Data[i, 1] > 0 && data.Data[i, 0] > Data[i, 0]))
                        {
                            Data[i, 0] = data.Data[i, 0];
                            Data[i, 1] = data.Data[i, 1];
                        }
                        if (Data[i, 3] == 0 || (data.Data[i, 3] > 0 && data.Data[i, 2] > Data[i, 2]))
                        {
                            Data[i, 2] = data.Data[i, 2];
                            Data[i, 3] = data.Data[i, 3];
                        }
                    }
                    break;
                case HistLineMode.AverAndMin:
                    for (int i = 0; i < rows; i++)
                    {
                        if (Data[i, 1] + data.Data[i, 1] == 0) Data[i, 0] = 0;
                        else Data[i, 0] = (Data[i, 0] * Data[i, 1] + data.Data[i, 0] * data.Data[i, 1]) / (Data[i, 1] + data.Data[i, 1]);
                        Data[i, 1] += data.Data[i, 1];

                        if (Data[i, 3] == 0 || (data.Data[i, 3] > 0 && data.Data[i, 2] < Data[i, 2]))
                        {
                            Data[i, 2] = data.Data[i, 2];
                            Data[i, 3] = data.Data[i, 3];
                        }
                    }
                    break;
                case HistLineMode.AverAndMax:
                    for (int i = 0; i < rows; i++)
                    {
                        if (Data[i, 1] + data.Data[i, 1] == 0) Data[i, 0] = 0;
                        else Data[i, 0] = (Data[i, 0] * Data[i, 1] + data.Data[i, 0] * data.Data[i, 1]) / (Data[i, 1] + data.Data[i, 1]);
                        Data[i, 1] += data.Data[i, 1];

                        if (Data[i, 3] == 0 || (data.Data[i, 3] > 0 && data.Data[i, 2] > Data[i, 2]))
                        {
                            Data[i, 2] = data.Data[i, 2];
                            Data[i, 3] = data.Data[i, 3];
                        }
                    }
                    break;
                case HistLineMode.AverAndAver:
                    for (int i = 0; i < rows; i++)
                    {
                        if (Data[i, 1] + data.Data[i, 1] == 0) Data[i, 0] = 0;
                        else Data[i, 0] = (Data[i, 0] * Data[i, 1] + data.Data[i, 0] * data.Data[i, 1]) / (Data[i, 1] + data.Data[i, 1]);
                        Data[i, 1] += data.Data[i, 1];

                        if (Data[i, 3] + data.Data[i, 3] == 0) Data[i, 2] = 0;
                        else Data[i, 2] = (Data[i, 2] * Data[i, 3] + data.Data[i, 2] * data.Data[i, 3]) / (Data[i, 3] + data.Data[i, 3]);
                        Data[i, 3] += data.Data[i, 3];
                    }
                    break;
                case HistLineMode.AverAndDev:
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            double srcAver = Data[i, 0];
                            double dstAver = data.Data[i, 0];
                            int srcCount = (int)Data[i, 1];
                            int dstCount = (int)data.Data[i, 1];
                            double srcDev = Data[i, 2];
                            double dstDev = data.Data[i, 2];

                            if (srcCount <= 0)
                            {
                                Data[i, 0] = dstAver;
                                Data[i, 1] = dstCount;
                                Data[i, 2] = dstDev;
                            }
                            else if (dstCount > 0)
                            {
                                double srcSum = srcAver * srcCount;
                                double dstSum = dstAver * dstCount;

                                double srcSumv = srcDev * srcDev * srcCount + srcSum * srcSum / srcCount;
                                double dstSumv = dstDev * dstDev * dstCount + dstSum * dstSum / dstCount;

                                var totalCount = srcCount + dstCount;
                                Data[i, 1] = totalCount;

                                var sum = srcSum + dstSum;
                                if (totalCount <= 0) Data[i, 0] = 0;
                                else Data[i, 0] = sum / totalCount;

                                if (totalCount <= 1) Data[i, 2] = 0;
                                else
                                {
                                    var vara = (srcSumv + dstSumv - sum * sum / totalCount) / totalCount;
                                    if (vara <= 0) Data[i, 2] = 0;
                                    else Data[i, 2] = Math.Sqrt(vara);
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public override bool HasData()
        {
            if (Data.Length == 0) return false;

            var rows = 0;
            if (Definition.Config[4] == "XValues")
            {
                if (!Int32.TryParse(Definition.Config[7], out rows)) return false;
            }
            else if (Definition.Config[4] == "XLabels")
            {
                rows = Definition.Config.Count - 5;
            }

            var lineEnabled = IsLineEnabled();
            double defaultHistValue = 0;
            double defaultLineValue = 0;
            Double.TryParse(Definition.Config[2], out defaultHistValue);
            Double.TryParse(Definition.Config[3], out defaultLineValue);
            for (int i = 0; i < rows; i++)
            {
                if (Data[i, 0] != defaultHistValue) return true;
                if (Data[i, 1] != 0) return true;
                if (lineEnabled)
                {
                    if (Data[i, 2] != defaultLineValue) return true;
                    if (Data[i, 3] != 0) return true;
                }
            }

            return false;
        }

        /// \~English
        /// <summary>
        /// Get whether the mode is for histogram only
        /// </summary>
        /// <param name="mode">Mode of statistics</param>
        /// <returns>Whether the mode is for histogram only</returns>
        /// \~Chinese
        /// <summary>
        /// 检查一种模式是否为仅柱状图模式
        /// </summary>
        /// <param name="mode">图表统计模式</param>
        /// <returns>是否为仅柱状图模式</returns>
        public static bool IsHistogramOnlyMode(HistLineMode mode)
        {
            return mode == HistLineMode.Sum ||
                mode == HistLineMode.Aver ||
                mode == HistLineMode.Min ||
                mode == HistLineMode.Max ||
                mode == HistLineMode.Percentage ||
                mode == HistLineMode.HitRatio;
        }

        private enum AddMode
        {
            AddNumber,
            AddTime,
            AddHit,
        }

        private void add(double x, double y, double y2, uint times, bool xIsIndex, AddMode addMode, bool y2IsEnable)
        {
            if (!xIsIndex && Definition.Config[4] != "XValues") return;
            if (times == 0) return;
            if (y2IsEnable && !IsLineEnabled()) return;
            if (addMode != AddMode.AddNumber && IsLineEnabled()) return;

            if (addMode == AddMode.AddTime) y = 1;
            else if (addMode == AddMode.AddHit)
            {
                if (y != 0) y = 100;
            }

            if (!y2IsEnable) y2 = y;

            if (xIsIndex)
            {
                if (x < 0 || x >= Data.GetLength(0)) return;
            }
            else
            {
                double bas, step, count;
                if (!Double.TryParse(Definition.Config[5], out bas) || !Double.TryParse(Definition.Config[6], out step) || !Double.TryParse(Definition.Config[7], out count)) return;

                var index = (x - bas) / step;
                if (index < 0 || index >= count) return;
                x = index;
                xIsIndex = true;
            }
            var xi = (int)x;

            var mode = (HistLineMode)Enum.Parse(typeof(HistLineMode), Definition.Config[0]);
            switch (mode)
            {
                case HistLineMode.Sum:
                case HistLineMode.Percentage:
                    Data[xi, 0] += y * times;
                    break;
                case HistLineMode.Aver:
                case HistLineMode.HitRatio:
                    Data[xi, 0] = (Data[xi, 0] * Data[xi, 1] + y * times) / (Data[xi, 1] + times);
                    Data[xi, 1] = Data[xi, 1] + times;
                    break;
                case HistLineMode.Min:
                    if (Data[xi, 1] == 0)
                    {
                        Data[xi, 0] = y;
                        Data[xi, 1] = 1;
                    }
                    else Data[xi, 0] = Math.Min(Data[xi, 0], y);
                    break;
                case HistLineMode.Max:
                    if (Data[xi, 1] == 0)
                    {
                        Data[xi, 0] = y;
                        Data[xi, 1] = 1;
                    }
                    else Data[xi, 0] = Math.Max(Data[xi, 0], y);
                    break;
                case HistLineMode.SumAndSum:
                    Data[xi, 0] += y * times;
                    Data[xi, 2] += y2 * times;
                    break;
                case HistLineMode.MinAndMin:
                    if (Data[xi, 1] == 0)
                    {
                        Data[xi, 0] = y;
                        Data[xi, 1] = 1;
                    }
                    else Data[xi, 0] = Math.Min(Data[xi, 0], y);
                    if (Data[xi, 3] == 0)
                    {
                        Data[xi, 2] = y2;
                        Data[xi, 3] = 1;
                    }
                    else Data[xi, 2] = Math.Min(Data[xi, 2], y2);
                    break;
                case HistLineMode.MinAndMax:
                    if (Data[xi, 1] == 0)
                    {
                        Data[xi, 0] = y;
                        Data[xi, 1] = 1;
                    }
                    else Data[xi, 0] = Math.Min(Data[xi, 0], y);
                    if (Data[xi, 3] == 0)
                    {
                        Data[xi, 2] = y2;
                        Data[xi, 3] = 1;
                    }
                    else Data[xi, 2] = Math.Max(Data[xi, 2], y2);
                    break;
                case HistLineMode.MaxAndMax:
                    if (Data[xi, 1] == 0)
                    {
                        Data[xi, 0] = y;
                        Data[xi, 1] = 1;
                    }
                    else Data[xi, 0] = Math.Max(Data[xi, 0], y);
                    if (Data[xi, 3] == 0)
                    {
                        Data[xi, 2] = y2;
                        Data[xi, 3] = 1;
                    }
                    else Data[xi, 2] = Math.Max(Data[xi, 2], y2);
                    break;
                case HistLineMode.AverAndMin:
                    Data[xi, 0] = (Data[xi, 0] * Data[xi, 1] + y * times) / (Data[xi, 1] + times);
                    Data[xi, 1] = Data[xi, 1] + times;
                    if (Data[xi, 3] == 0)
                    {
                        Data[xi, 2] = y2;
                        Data[xi, 3] = 1;
                    }
                    else Data[xi, 2] = Math.Min(Data[xi, 2], y2);
                    break;
                case HistLineMode.AverAndMax:
                    Data[xi, 0] = (Data[xi, 0] * Data[xi, 1] + y * times) / (Data[xi, 1] + times);
                    Data[xi, 1] = Data[xi, 1] + times;
                    if (Data[xi, 3] == 0)
                    {
                        Data[xi, 2] = y2;
                        Data[xi, 3] = 1;
                    }
                    else Data[xi, 2] = Math.Max(Data[xi, 2], y2);
                    break;
                case HistLineMode.AverAndAver:
                    Data[xi, 0] = (Data[xi, 0] * Data[xi, 1] + y * times) / (Data[xi, 1] + times);
                    Data[xi, 1] = Data[xi, 1] + times;
                    Data[xi, 2] = (Data[xi, 2] * Data[xi, 3] + y2 * times) / (Data[xi, 3] + times);
                    Data[xi, 3] = Data[xi, 3] + times;
                    break;
                case HistLineMode.AverAndDev:
                    for (int i = 0; i < times; i++)
                    {
                        double aver = Data[xi, 0];
                        double count = Data[xi, 1];
                        double dev = Data[xi, 2];
                        if (count == 0)
                        {
                            Data[xi, 0] = y;
                            Data[xi, 1] = 1;
                            Data[xi, 2] = 0;
                        }
                        else
                        {
                            var countBefore = count;
                            var countAfter = count + 1;
                            var sumBefore = aver * countBefore;
                            var sumAfter = sumBefore + y;
                            var sumv = dev * dev * countBefore + sumBefore * sumBefore / countBefore + y * y;
                            var vara = (sumv - sumAfter * sumAfter / countAfter) / countAfter;
                            var devFinal = vara > 0 ? Math.Sqrt(vara) : 0;
                            Data[xi, 0] = sumAfter / countAfter;
                            Data[xi, 1] = countAfter;
                            Data[xi, 2] = devFinal;
                        }
                    }
                    break;
            }
        }
    }
}
