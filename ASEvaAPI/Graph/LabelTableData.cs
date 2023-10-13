using System;

namespace ASEva.Graph
{
    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Mode of statistics for label table
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 标签表模式
    /// </summary>
    public enum LabelTableMode
    {
        /// \~English
        /// <summary>
        /// Sum
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 求和标签表
        /// </summary>
        Sum,

        /// \~English
        /// <summary>
        /// Average value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 平均值标签表
        /// </summary>
        Aver,

        /// \~English
        /// <summary>
        /// Minimum value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最小值标签表
        /// </summary>
        Min,

        /// \~English
        /// <summary>
        /// Maximum value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最大值标签表
        /// </summary>
        Max,

        /// \~English
        /// <summary>
        /// Percentage
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 百分比标签表
        /// </summary>
        Percentage,
    }

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Value direction of label table
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 标签表数值方向
    /// </summary>
    public enum LabelTableValueDirection
    {
        /// \~English
        /// <summary>
        /// Positive, valid range is from the default value to the maximum value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 正向，数值可视化范围为默认值至最大值
        /// </summary>
        Positive,

        /// \~English
        /// <summary>
        /// Negative, valid range is from the default value to the minimum value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 负向，数值可视化范围为默认值至最小值
        /// </summary>
        Negative,

        /// \~English
        /// <summary>
        /// Bidirectional, valid range is a bidirectional symmetric interval centered by the default value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 双向，数值可视化范围为默认值为中心的双向对称区间
        /// </summary>
        Bidirectional,
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 标签表数据
    /// </summary>
    public class LabelTableData : GraphData
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 创建图表定义
        /// </summary>
        /// <param name="title">图表标题</param>
        /// <param name="xTitle">x轴标题</param>
        /// <param name="yTitle">y轴标题</param>
        /// <param name="mode">图表模式</param>
        /// <param name="xLabels">x轴标签列表</param>
        /// <param name="yLabels">y轴标签列表</param>
        /// <param name="valueDirection">以默认值为基准值的数值方向，仅影响可视化效果</param>
        /// <param name="defaultValue">标签表数据初始值</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition CreateDefinition(String title, String xTitle, String yTitle, LabelTableMode mode, String[] xLabels, String[] yLabels, LabelTableValueDirection valueDirection, double defaultValue = 0)
        {
            var def = new GraphDefinition();
            def.Type = GraphType.LabelTable;
            def.MainTitle = title;
            def.Config.Add(mode.ToString()); // 0
            def.Config.Add(xLabels.Length.ToString()); // 1
            def.Config.Add(yLabels.Length.ToString()); // 2
            def.Config.Add(valueDirection.ToString()); // 3
            def.Config.Add(defaultValue.ToString()); // 4
            def.Config.Add(xTitle); // 5
            def.Config.Add(yTitle); // 6

            // 7~
            foreach (var label in xLabels) def.Config.Add(label);
            foreach (var label in yLabels) def.Config.Add(label);

            def.ColumnTitles.Add("Value");
            def.ColumnTitles.Add("Aux");
            return def;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取标签表模式
        /// </summary>
        /// <returns>标签表模式</returns>
        public LabelTableMode GetMode()
        {
            return (LabelTableMode)Enum.Parse(typeof(LabelTableMode), Definition.Config[0]);
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取x轴标题
        /// </summary>
        /// <returns>x轴标题</returns>
        public String GetXTitle()
        {
            return Definition.Config[5];
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取y轴标题
        /// </summary>
        /// <returns>y轴标题</returns>
        public String GetYTitle()
        {
            return Definition.Config[6];
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取x轴标签个数
        /// </summary>
        /// <returns>x轴标签个数</returns>
        public int GetXLabelCount()
        {
            int val;
            Int32.TryParse(Definition.Config[1], out val);
            return val;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取y轴标签个数
        /// </summary>
        /// <returns>y轴标签个数</returns>
        public int GetYLabelCount()
        {
            int val;
            Int32.TryParse(Definition.Config[2], out val);
            return val;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取x轴标签列表
        /// </summary>
        /// <returns>x轴标签列表</returns>
        public String[] GetXLabels()
        {
            int count = 0;
            Int32.TryParse(Definition.Config[1], out count);
            var output = new String[count];
            for (int i = 0; i < count; i++)
            {
                output[i] = Definition.Config[7 + i];
            }
            return output;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取y轴标签列表
        /// </summary>
        /// <returns>y轴标签列表</returns>
        public String[] GetYLabels()
        {
            int xcount = 0, ycount = 0;
            Int32.TryParse(Definition.Config[1], out xcount);
            Int32.TryParse(Definition.Config[2], out ycount);
            var output = new String[ycount];
            for (int i = 0; i < ycount; i++)
            {
                output[i] = Definition.Config[7 + xcount + i];
            }
            return output;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取标签表数据方向
        /// </summary>
        /// <returns>标签表数据方向</returns>
        public LabelTableValueDirection GetValueDirection()
        {
            return (LabelTableValueDirection)Enum.Parse(typeof(LabelTableValueDirection), Definition.Config[3]);
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取标签表默认值
        /// </summary>
        /// <returns>默认值</returns>
        public double GetDefaultValue()
        {
            double val = 0;
            Double.TryParse(Definition.Config[4], out val);
            return val;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取x轴各标签统计值
        /// </summary>
        /// <returns></returns>
        public double[] GetXHistValues()
        {
            var mode = (LabelTableMode)Enum.Parse(typeof(LabelTableMode), Definition.Config[0]);

            double defaultValue = 0;
            int xcount = 0, ycount = 0;
            Double.TryParse(Definition.Config[4], out defaultValue);
            Int32.TryParse(Definition.Config[1], out xcount);
            Int32.TryParse(Definition.Config[2], out ycount);

            if (mode == LabelTableMode.Percentage)
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
                    case LabelTableMode.Sum:
                        {
                            double sum = 0;
                            for (int j = 0; j < ycount; j++)
                            {
                                sum += Data[i, j * 2];
                            }
                            output[i] = sum;
                        }
                        break;
                    case LabelTableMode.Max:
                        {
                            double maximum = Double.NegativeInfinity;
                            for (int j = 0; j < ycount; j++)
                            {
                                if (Data[i, j * 2 + 1] > 0) maximum = Math.Max(maximum, Data[i, j * 2]);
                            }
                            output[i] = maximum == Double.NegativeInfinity ? defaultValue : maximum;
                        }
                        break;
                    case LabelTableMode.Min:
                        {
                            double minimum = Double.PositiveInfinity;
                            for (int j = 0; j < ycount; j++)
                            {
                                if (Data[i, j * 2 + 1] > 0) minimum = Math.Min(minimum, Data[i, j * 2]);
                            }
                            output[i] = minimum == Double.PositiveInfinity ? defaultValue : minimum;
                        }
                        break;
                    case LabelTableMode.Aver:
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
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取y轴各标签统计值
        /// </summary>
        /// <returns></returns>
        public double[] GetYHistValues()
        {
            var mode = (LabelTableMode)Enum.Parse(typeof(LabelTableMode), Definition.Config[0]);

            double defaultValue = 0;
            int xcount = 0, ycount = 0;
            Double.TryParse(Definition.Config[4], out defaultValue);
            Int32.TryParse(Definition.Config[1], out xcount);
            Int32.TryParse(Definition.Config[2], out ycount);

            if (mode == LabelTableMode.Percentage)
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
                    case LabelTableMode.Sum:
                        {
                            double sum = 0;
                            for (int i = 0; i < xcount; i++)
                            {
                                sum += Data[i, j * 2];
                            }
                            output[j] = sum;
                        }
                        break;
                    case LabelTableMode.Max:
                        {
                            double maximum = Double.NegativeInfinity;
                            for (int i = 0; i < xcount; i++)
                            {
                                if (Data[i, j * 2 + 1] > 0) maximum = Math.Max(maximum, Data[i, j * 2]);
                            }
                            output[j] = maximum == Double.NegativeInfinity ? defaultValue : maximum;
                        }
                        break;
                    case LabelTableMode.Min:
                        {
                            double minimum = Double.PositiveInfinity;
                            for (int i = 0; i < xcount; i++)
                            {
                                if (Data[i, j * 2 + 1] > 0) minimum = Math.Min(minimum, Data[i, j * 2]);
                            }
                            output[j] = minimum == Double.PositiveInfinity ? defaultValue : minimum;
                        }
                        break;
                    case LabelTableMode.Aver:
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
        /// 
        /// \~Chinese
        /// <summary>
        /// 在x轴，y轴标签序号的位置添加1
        /// </summary>
        /// <param name="x">x轴标签序号，若超出范围则不添加</param>
        /// <param name="y">y轴标签序号，若超出范围则不添加</param>
        public void AddTime(int x, int y)
        {
            AddSample(x, y, 1);
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 在x轴，y轴标签序号的位置添加数值样本
        /// </summary>
        /// <param name="x">x轴标签序号，若超出范围则不添加</param>
        /// <param name="y">y轴标签序号，若超出范围则不添加</param>
        /// <param name="value">数值样本</param>
        public void AddSample(int x, int y, double value)
        {
            int xcount = 0, ycount = 0;
            Int32.TryParse(Definition.Config[1], out xcount);
            Int32.TryParse(Definition.Config[2], out ycount);

            if (x < 0 || x >= xcount || y < 0 || y >= ycount) return;

            var xi = x;
            var yi = y;
            var mode = (LabelTableMode)Enum.Parse(typeof(LabelTableMode), Definition.Config[0]);
            switch (mode)
            {
                case LabelTableMode.Sum:
                case LabelTableMode.Percentage:
                    Data[xi, yi * 2] += value;
                    break;
                case LabelTableMode.Min:
                    if (Data[xi, yi * 2 + 1] == 0)
                    {
                        Data[xi, yi * 2] = value;
                        Data[xi, yi * 2 + 1] = 1;
                    }
                    else Data[xi, yi * 2] = Math.Min(Data[xi, yi * 2], value);
                    break;
                case LabelTableMode.Max:
                    if (Data[xi, yi * 2 + 1] == 0)
                    {
                        Data[xi, yi * 2] = value;
                        Data[xi, yi * 2 + 1] = 1;
                    }
                    else Data[xi, yi * 2] = Math.Max(Data[xi, yi * 2], value);
                    break;
                case LabelTableMode.Aver:
                    Data[xi, yi * 2] = (Data[xi, yi * 2] * Data[xi, yi * 2 + 1] + value) / (Data[xi, yi * 2 + 1] + 1);
                    Data[xi, yi * 2 + 1] += 1;
                    break;
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取标签表数据
        /// </summary>
        /// <returns>标签表数据</returns>
        public double[,] GetValues()
        {
            int xc = 0, yc = 0;
            Int32.TryParse(Definition.Config[1], out xc);
            Int32.TryParse(Definition.Config[2], out yc);

            double k = 1;
            var mode = (LabelTableMode)Enum.Parse(typeof(LabelTableMode), Definition.Config[0]);
            if (mode == LabelTableMode.Percentage)
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
            int xr = 0, yr = 0;
            Int32.TryParse(Definition.Config[1], out xr);
            Int32.TryParse(Definition.Config[2], out yr);
            Data = new double[xr, yr * 2];

            double defaultValue = 0;
            Double.TryParse(Definition.Config[4], out defaultValue);
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
            int xc = 0, yc = 0;
            Int32.TryParse(Definition.Config[1], out xc);
            Int32.TryParse(Definition.Config[2], out yc);

            var mode = (LabelTableMode)Enum.Parse(typeof(LabelTableMode), Definition.Config[0]);
            switch (mode)
            {
                case LabelTableMode.Sum:
                case LabelTableMode.Percentage:
                    for (int x = 0; x < xc; x++)
                    {
                        for (int y = 0; y < yc; y++)
                        {
                            Data[x, y * 2] += data.Data[x, y * 2];
                        }
                    }
                    break;
                case LabelTableMode.Min:
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
                case LabelTableMode.Max:
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
                case LabelTableMode.Aver:
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

            int xr = 0, yr = 0;
            Int32.TryParse(Definition.Config[1], out xr);
            Int32.TryParse(Definition.Config[2], out yr);
            
            double defaultValue = 0;
            Double.TryParse(Definition.Config[4], out defaultValue);

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
