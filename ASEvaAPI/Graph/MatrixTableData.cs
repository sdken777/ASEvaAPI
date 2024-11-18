using System;

namespace ASEva.Graph
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Mode of statistics for matrix table
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 矩阵表统计模式
    /// </summary>
    public enum MatrixTableMode
    {
        /// \~English
        /// <summary>
        /// Sum, supporting the validations: ValueBelow, ValueAbove, OutlineInside, OutlineOutside
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 求和矩阵表，图表数据验证支持ValueBelow, ValueAbove, OutlineInside, OutlineOutside
        /// </summary>
        Sum,

        /// \~English
        /// <summary>
        /// Average value, supporting the validations: ValueBelow, ValueAbove
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 平均值矩阵表，图表数据验证支持ValueBelow, ValueAbove
        /// </summary>
        Aver,

        /// \~English
        /// <summary>
        /// Minimum value, supporting the validations: ValueBelow, ValueAbove
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最小值矩阵表，图表数据验证支持ValueBelow, ValueAbove
        /// </summary>
        Min,

        /// \~English
        /// <summary>
        /// Maximum value, supporting the validations: ValueBelow, ValueAbove
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最大值矩阵表，图表数据验证支持ValueBelow, ValueAbove
        /// </summary>
        Max,

        /// \~English
        /// <summary>
        /// Percentage, supporting the validations: ValueBelow, ValueAbove, OutlineInside, OutlineOutside
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 百分比矩阵表，图表数据验证支持ValueBelow, ValueAbove, OutlineInside, OutlineOutside
        /// </summary>
        Percentage,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Single axis' range of matrix table
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 矩阵表单轴范围
    /// </summary>
    public class MatrixTableRange
    {
        public double Base { get; set; }
        public double Step { get; set; }
        public int Count { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.7.0) Numeric range of matrix table
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.0) 矩阵表中数值的范围
    /// </summary>
    public struct MatrixTableValueRefRange(double lower, double upper)
    {
        public double lower = lower;
        public double upper = upper;
    }

    /// \~English
    /// <summary>
    /// (api:app=3.7.0) Matrix table graph data
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.0) 矩阵表数据
    /// </summary>
    public class MatrixTableData(GraphDefinition def) : GraphData(def)
    {
        /// \~English
        /// <summary>
        /// Create graph definition (without validation)
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="xTitle">X axis title</param>
        /// <param name="yTitle">Y axis title</param>
        /// <param name="mode">Mode of statistics</param>
        /// <param name="xRange">X axis range</param>
        /// <param name="yRange">Y axis range</param>
        /// <param name="valueRefRange">Reference range of values</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Graph definition object</returns>
        /// \~Chinese
        /// <summary>
        /// 创建图表定义（不带数据验证）
        /// </summary>
        /// <param name="title">图表标题</param>
        /// <param name="xTitle">x轴标题</param>
        /// <param name="yTitle">y轴标题</param>
        /// <param name="mode">图表统计模式</param>
        /// <param name="xRange">x轴范围</param>
        /// <param name="yRange">y轴范围</param>
        /// <param name="valueRefRange">矩阵表数据参考范围</param>
        /// <param name="defaultValue">矩阵表数据初始值</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition CreateDefinition(String title, String xTitle, String yTitle, MatrixTableMode mode, MatrixTableRange xRange, MatrixTableRange yRange, MatrixTableValueRefRange valueRefRange, double defaultValue = 0)
        {
            return CreateDefinitionWithValidation(title, xTitle, yTitle, mode, null, xRange, yRange, valueRefRange, defaultValue);
        }

        /// \~English
        /// <summary>
        /// Create graph definition
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="xTitle">X axis title</param>
        /// <param name="yTitle">Y axis title</param>
        /// <param name="mode">Mode of statistics</param>
        /// <param name="validation">Graph validation, null means no validation. For details about the support types in different modes, see ASEva.Graph.MatrixTableMode </param>
        /// <param name="xRange">X axis range</param>
        /// <param name="yRange">Y axis range</param>
        /// <param name="valueRefRange">Reference range of values</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Graph definition object</returns>
        /// \~Chinese
        /// <summary>
        /// 创建带数据验证的图表定义
        /// </summary>
        /// <param name="title">图表标题</param>
        /// <param name="xTitle">x轴标题</param>
        /// <param name="yTitle">y轴标题</param>
        /// <param name="mode">图表统计模式</param>
        /// <param name="validation">数据验证方式，null表示不验证。不同模式下的支持类型请参考 ASEva.Graph.MatrixTableMode </param>
        /// <param name="xRange">x轴范围</param>
        /// <param name="yRange">y轴范围</param>
        /// <param name="valueRefRange">矩阵表数据参考范围</param>
        /// <param name="defaultValue">矩阵表数据初始值</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition CreateDefinitionWithValidation(String title, String xTitle, String yTitle, MatrixTableMode mode, GraphValidation? validation, MatrixTableRange xRange, MatrixTableRange yRange, MatrixTableValueRefRange valueRefRange, double defaultValue = 0)
        {
            var def = new GraphDefinition(GraphType.MatrixTable, title);
            def.Config.Add(mode.ToString()); // 0
            def.Config.Add(xRange.Base.ToString()); // 1
            def.Config.Add(xRange.Step.ToString()); // 2
            def.Config.Add(xRange.Count.ToString()); // 3
            def.Config.Add(yRange.Base.ToString()); // 4
            def.Config.Add(yRange.Step.ToString()); // 5
            def.Config.Add(yRange.Count.ToString()); // 6
            def.Config.Add(valueRefRange.lower.ToString()); // 7
            def.Config.Add(valueRefRange.upper.ToString()); // 8
            def.Config.Add(xTitle); // 9
            def.Config.Add(yTitle); // 10
            def.Config.Add(defaultValue.ToString()); // 11

            if (validation != null)
            {
                var vdType = validation.GetValidationType();
                if (mode == MatrixTableMode.Sum || mode == MatrixTableMode.Percentage)
                {
                    if (vdType == GraphValidationType.ValueBelow ||
                        vdType == GraphValidationType.ValueAbove ||
                        vdType == GraphValidationType.OutlineInside ||
                        vdType == GraphValidationType.OutlineOutside) def.Validation = validation;
                }
                else if (mode == MatrixTableMode.Aver || mode == MatrixTableMode.Min || mode == MatrixTableMode.Max)
                {
                    if (vdType == GraphValidationType.ValueBelow ||
                        vdType == GraphValidationType.ValueAbove) def.Validation = validation;
                }
            }

            def.ColumnTitles.Add("Value");
            def.ColumnTitles.Add("Aux");
            return def;
        }

        /// \~English
        /// <summary>
        /// Get mode of statistics
        /// </summary>
        /// <returns>Mode of statistics</returns>
        /// \~Chinese
        /// <summary>
        /// 获取矩阵表统计模式
        /// </summary>
        /// <returns>矩阵表统计模式</returns>
        public MatrixTableMode GetMode()
        {
            return (MatrixTableMode)Enum.Parse(typeof(MatrixTableMode), Definition.Config[0]);
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
            return Definition.Config[9];
        }

        /// \~English
        /// <summary>
        /// Get Y axis title
        /// </summary>
        /// <returns>Y axis title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取y轴标题
        /// </summary>
        /// <returns>y轴标题</returns>
        public String GetYTitle()
        {
            return Definition.Config[10];
        }

        /// \~English
        /// <summary>
        /// Get X axis range
        /// </summary>
        /// <returns>X axis range</returns>
        /// \~Chinese
        /// <summary>
        /// 获取x轴范围
        /// </summary>
        /// <returns>x轴范围</returns>
        public MatrixTableRange? GetXRange()
        {
            double b, s;
            int c;
            if (Double.TryParse(Definition.Config[1], out b) && Double.TryParse(Definition.Config[2], out s) && Int32.TryParse(Definition.Config[3], out c))
            {
                return new MatrixTableRange()
                {
                    Base = b,
                    Step = s,
                    Count = c,
                };
            }
            else return null;
        }

        /// \~English
        /// <summary>
        /// Get Y axis range
        /// </summary>
        /// <returns>Y axis range</returns>
        /// \~Chinese
        /// <summary>
        /// 获取y轴范围
        /// </summary>
        /// <returns>y轴范围</returns>
        public MatrixTableRange? GetYRange()
        {
            double b, s;
            int c;
            if (Double.TryParse(Definition.Config[4], out b) && Double.TryParse(Definition.Config[5], out s) && Int32.TryParse(Definition.Config[6], out c))
            {
                return new MatrixTableRange()
                {
                    Base = b,
                    Step = s,
                    Count = c,
                };
            }
            else return null;
        }

        /// \~English
        /// <summary>
        /// Get reference range of values
        /// </summary>
        /// <returns>Reference range of values</returns>
        /// \~Chinese
        /// <summary>
        /// 获取矩阵表数据参考范围
        /// </summary>
        /// <returns>矩阵表数据参考范围</returns>
        public MatrixTableValueRefRange GetValueRefRange()
        {
            double lower = 0, upper = 0;
            Double.TryParse(Definition.Config[7], out lower);
            Double.TryParse(Definition.Config[8], out upper);
            return new MatrixTableValueRefRange(lower, upper);
        }

        /// \~English
        /// <summary>
        /// Get statistical value of each interval along X axis
        /// </summary>
        /// <returns>Statistical value of each interval along X axis</returns>
        /// \~Chinese
        /// <summary>
        /// 获取矩阵表中x轴各区间统计值
        /// </summary>
        /// <returns>x轴各区间统计值</returns>
        public double[] GetXHistValues()
        {
            var mode = (MatrixTableMode)Enum.Parse(typeof(MatrixTableMode), Definition.Config[0]);
            double defaultValue;
            Double.TryParse(Definition.Config[11], out defaultValue);
            var xcount = GetXRange()?.Count ?? 0;
            var ycount = GetYRange()?.Count ?? 0;

            if (mode == MatrixTableMode.Percentage)
            {
                var values = GetValues();
                var outputPercentage = new double[xcount];
                for (int i = 0; i < xcount; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < ycount; j++)
                    {
                        sum += values[i, j];
                    }
                    outputPercentage[i] = sum;
                }
                return outputPercentage;
            }

            var output = new double[xcount];
            for (int i = 0; i < xcount; i++)
            {
                switch (mode)
                {
                    case MatrixTableMode.Sum:
                    case MatrixTableMode.Percentage:
                        {
                            double sum = 0;
                            for (int j = 0; j < ycount; j++)
                            {
                                sum += Data[i, j * 2];
                            }
                            output[i] = sum;
                        }
                        break;
                    case MatrixTableMode.Max:
                        {
                            double maximum = Double.NegativeInfinity;
                            for (int j = 0; j < ycount; j++)
                            {
                                if (Data[i, j * 2 + 1] > 0) maximum = Math.Max(maximum, Data[i, j * 2]);
                            }
                            output[i] = maximum == Double.NegativeInfinity ? defaultValue : maximum;
                        }
                        break;
                    case MatrixTableMode.Min:
                        {
                            double minimum = Double.PositiveInfinity;
                            for (int j = 0; j < ycount; j++)
                            {
                                if (Data[i, j * 2 + 1] > 0) minimum = Math.Min(minimum, Data[i, j * 2]);
                            }
                            output[i] = minimum == Double.PositiveInfinity ? defaultValue : minimum;
                        }
                        break;
                    case MatrixTableMode.Aver:
                        {
                            double sum = 0;
                            double weight = 0;
                            for (int j = 0; j < ycount; j++)
                            {
                                var thisWeight = Data[i, j * 2 + 1];
                                sum += Data[i, j * 2] * thisWeight;
                                weight += thisWeight;
                            }
                            output[i] = weight == 0 ? defaultValue : sum / weight;
                        }
                        break;
                }
            }
            return output;
        }

        /// \~English
        /// <summary>
        /// Get statistical value of each interval along Y axis
        /// </summary>
        /// <returns>Statistical value of each interval along Y axis</returns>
        /// \~Chinese
        /// <summary>
        /// 获取矩阵表中y轴各区间统计值
        /// </summary>
        /// <returns>y轴各区间统计值</returns>
        public double[] GetYHistValues()
        {
            var mode = (MatrixTableMode)Enum.Parse(typeof(MatrixTableMode), Definition.Config[0]);
            double defaultValue;
            Double.TryParse(Definition.Config[11], out defaultValue);
            var xcount = GetXRange()?.Count ?? 0;
            var ycount = GetYRange()?.Count ?? 0;

            if (mode == MatrixTableMode.Percentage)
            {
                var values = GetValues();
                var outputPercentage = new double[ycount];
                for (int j = 0; j < ycount; j++)
                {
                    double sum = 0;
                    for (int i = 0; i < xcount; i++)
                    {
                        sum += values[i, j];
                    }
                    outputPercentage[j] = sum;
                }
                return outputPercentage;
            }

            var output = new double[ycount];
            for (int j = 0; j < ycount; j++)
            {
                switch (mode)
                {
                    case MatrixTableMode.Sum:
                    case MatrixTableMode.Percentage:
                        {
                            double sum = 0;
                            for (int i = 0; i < xcount; i++)
                            {
                                sum += Data[i, j * 2];
                            }
                            output[j] = sum;
                        }
                        break;
                    case MatrixTableMode.Max:
                        {
                            double maximum = Double.NegativeInfinity;
                            for (int i = 0; i < xcount; i++)
                            {
                                if (Data[i, j * 2 + 1] > 0) maximum = Math.Max(maximum, Data[i, j * 2]);
                            }
                            output[j] = maximum == Double.NegativeInfinity ? defaultValue : maximum;
                        }
                        break;
                    case MatrixTableMode.Min:
                        {
                            double minimum = Double.PositiveInfinity;
                            for (int i = 0; i < xcount; i++)
                            {
                                if (Data[i, j * 2 + 1] > 0) minimum = Math.Min(minimum, Data[i, j * 2]);
                            }
                            output[j] = minimum == Double.PositiveInfinity ? defaultValue : minimum;
                        }
                        break;
                    case MatrixTableMode.Aver:
                        {
                            double sum = 0;
                            double weight = 0;
                            for (int i = 0; i < xcount; i++)
                            {
                                var thisWeight = Data[i, j * 2 + 1];
                                sum += Data[i, j * 2] * thisWeight;
                                weight += thisWeight;
                            }
                            output[j] = weight == 0 ? defaultValue : sum / weight;
                        }
                        break;
                }
            }
            return output;
        }

        /// \~English
        /// <summary>
        /// Add 1 to the interval where (x，y) is
        /// </summary>
        /// <param name="x">Input x, do nothing if over the range</param>
        /// <param name="y">Input y, do nothing if over the range</param>
        /// \~Chinese
        /// <summary>
        /// 在输入x，y坐标所在区间添加1
        /// </summary>
        /// <param name="x">输入的x值，若超出x轴范围则不添加</param>
        /// <param name="y">输入的y值，若超出y轴范围则不添加</param>
        public void AddTime(double x, double y)
        {
            AddSample(x, y, 1);
        }

        /// \~English
        /// <summary>
        /// Add sample to the interval where (x，y) is
        /// </summary>
        /// <param name="x">Input x, do nothing if over the range</param>
        /// <param name="y">Input y, do nothing if over the range</param>
        /// <param name="value">Sample value</param>
        /// \~Chinese
        /// <summary>
        /// 在输入x，y坐标所在区间添加数值样本
        /// </summary>
        /// <param name="x">输入的x值，若超出x轴范围则不添加</param>
        /// <param name="y">输入的y值，若超出y轴范围则不添加</param>
        /// <param name="value">数值样本</param>
        public void AddSample(double x, double y, double value)
        {
            var xr = GetXRange();
            var yr = GetYRange();
            if (xr == null || yr == null) return;

            x = (x - xr.Base) / xr.Step;
            y = (y - yr.Base) / yr.Step;
            if (x < 0 || x >= xr.Count || y < 0 || y >= yr.Count) return;

            var xi = (int)x;
            var yi = (int)y;

            var mode = (MatrixTableMode)Enum.Parse(typeof(MatrixTableMode), Definition.Config[0]);
            switch (mode)
            {
                case MatrixTableMode.Sum:
                case MatrixTableMode.Percentage:
                    Data[xi, yi * 2] += value;
                    break;
                case MatrixTableMode.Min:
                    if (Data[xi, yi * 2 + 1] == 0)
                    {
                        Data[xi, yi * 2] = value;
                        Data[xi, yi * 2 + 1] = 1;
                    }
                    else Data[xi, yi * 2] = Math.Min(Data[xi, yi * 2], value);
                    break;
                case MatrixTableMode.Max:
                    if (Data[xi, yi * 2 + 1] == 0)
                    {
                        Data[xi, yi * 2] = value;
                        Data[xi, yi * 2 + 1] = 1;
                    }
                    else Data[xi, yi * 2] = Math.Max(Data[xi, yi * 2], value);
                    break;
                case MatrixTableMode.Aver:
                    Data[xi, yi * 2] = (Data[xi, yi * 2] * Data[xi, yi * 2 + 1] + value) / (Data[xi, yi * 2 + 1] + 1);
                    Data[xi, yi * 2 + 1] += 1;
                    break;
            }
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
        public double[,] GetValues()
        {
            var xc = GetXRange()?.Count ?? 0;
            var yc = GetYRange()?.Count ?? 0;

            double k = 1;
            var mode = (MatrixTableMode)Enum.Parse(typeof(MatrixTableMode), Definition.Config[0]);
            if (mode == MatrixTableMode.Percentage)
            {
                double sum = 0;
                for (int x = 0; x < xc; x++)
                {
                    for (int y = 0; y < yc; y++)
                    {
                        sum += Data[x, y * 2];
                    }
                }
                k = sum == 0 ? 0 : (100 / sum);
            }

            var output = new double[xc, yc];
            for (int x = 0; x < xc; x++)
            {
                for (int y = 0; y < yc; y++)
                {
                    output[x, y] = Data[x, y * 2] * k;
                }
            }
            return output;
        }

        public override void InitParamsAndData()
        {
            var xr = GetXRange()?.Count ?? 0;
            var yr = GetYRange()?.Count ?? 0;
            Data = new double[xr, yr * 2];

            double defaultValue;
            Double.TryParse(Definition.Config[11], out defaultValue);
            for (int x = 0; x < xr; x++)
            {
                for (int y = 0; y < yr; y++)
                {
                    Data[x, y * 2] = defaultValue;
                }
            }
        }

        public override void MergeWith(GraphData data)
        {
            var xc = GetXRange()?.Count ?? 0;
            var yc = GetYRange()?.Count ?? 0;

            var mode = (MatrixTableMode)Enum.Parse(typeof(MatrixTableMode), Definition.Config[0]);
            switch (mode)
            {
                case MatrixTableMode.Sum:
                case MatrixTableMode.Percentage:
                    for (int x = 0; x < xc; x++)
                    {
                        for (int y = 0; y < yc; y++)
                        {
                            Data[x, y * 2] += data.Data[x, y * 2];
                        }
                    }
                    break;
                case MatrixTableMode.Min:
                    for (int x = 0; x < xc; x++)
                    {
                        for (int y = 0; y < yc; y++)
                        {
                            if (Data[x, y * 2 + 1] == 0 || (data.Data[x, y * 2 + 1] > 0 && data.Data[x, y * 2] < Data[x, y * 2]))
                            {
                                Data[x, y * 2] = data.Data[x, y * 2];
                                Data[x, y * 2 + 1] = data.Data[x, y * 2 + 1];
                            }
                        }
                    }
                    break;
                case MatrixTableMode.Max:
                    for (int x = 0; x < xc; x++)
                    {
                        for (int y = 0; y < yc; y++)
                        {
                            if (Data[x, y * 2 + 1] == 0 || (data.Data[x, y * 2 + 1] > 0 && data.Data[x, y * 2] > Data[x, y * 2]))
                            {
                                Data[x, y * 2] = data.Data[x, y * 2];
                                Data[x, y * 2 + 1] = data.Data[x, y * 2 + 1];
                            }
                        }
                    }
                    break;
                case MatrixTableMode.Aver:
                    for (int x = 0; x < xc; x++)
                    {
                        for (int y = 0; y < yc; y++)
                        {
                            if (Data[x, y * 2 + 1] + data.Data[x, y * 2 + 1] == 0) Data[x, y * 2] = 0;
                            else Data[x, y * 2] = (Data[x, y * 2] * Data[x, y * 2 + 1] + data.Data[x, y * 2] * data.Data[x, y * 2 + 1]) / (Data[x, y * 2 + 1] + data.Data[x, y * 2 + 1]);
                            Data[x, y * 2 + 1] += data.Data[x, y * 2 + 1];
                        }
                    }
                    break;
            }
        }

        public override bool HasData()
        {
            if (Data.Length == 0) return false;
            var xr = GetXRange()?.Count ?? 0;
            var yr = GetYRange()?.Count ?? 0;
            double defaultValue;
            Double.TryParse(Definition.Config[11], out defaultValue);
            for (int x = 0; x < xr; x++)
            {
                for (int y = 0; y < yr; y++)
                {
                    if (Data[x, y * 2] != defaultValue) return true;
                    if (Data[x, y * 2 + 1] != 0) return true;
                }
            }
            return false;
        }
    }
}
