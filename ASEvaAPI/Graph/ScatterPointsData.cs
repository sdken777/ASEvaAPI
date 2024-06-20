using System;

namespace ASEva.Graph
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Single axis' range of scatter points graph
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 散点图单轴范围
    /// </summary>
    public struct ScatterRange
    {
        public double lower;
        public double upper;

        public ScatterRange(double lower, double upper)
        {
            this.lower = lower;
            this.upper = upper;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Additional options for definition of scatter points graph
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 散点图定义的附加选项
    /// </summary>
    public class ScatterOptions
    {
        /// \~English
        /// <summary>
        /// Number of intervals along X axis, set to null to determine automatically
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// x轴柱状图区间个数，null表示自动计算
        /// </summary>
        public int? XHistCount { get; set; }

        /// \~English
        /// <summary>
        /// Number of intervals along Y axis, set to null to determine automatically
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// y轴柱状图区间个数，null表示自动计算
        /// </summary>
        public int? YHistCount { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Scatter points graph data
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 散点图数据
    /// </summary>
    public class ScatterPointsData : GraphData
    {
        /// \~English
        /// <summary>
        /// Create graph definition (without validation, and the number of intervals is determined automatically)
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="xTitle">X axis title</param>
        /// <param name="yTitle">Y axis title</param>
        /// <param name="xRange">X axis range</param>
        /// <param name="yRange">Y axis range</param>
        /// <returns>Graph definition object</returns>
        /// \~Chinese
        /// <summary>
        /// 创建图表定义（不带数据验证，柱状图区间个数自动计算）
        /// </summary>
        /// <param name="title">图表标题</param>
        /// <param name="xTitle">x轴标题</param>
        /// <param name="yTitle">y轴标题</param>
        /// <param name="xRange">x轴范围</param>
        /// <param name="yRange">y轴范围</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition CreateDefinition(String title, String xTitle, String yTitle, ScatterRange xRange, ScatterRange yRange)
        {
            return CreateDefinitionWithValidationAndOptions(title, xTitle, yTitle, xRange, yRange, null, null);
        }

        /// \~English
        /// <summary>
        /// Create graph definition (the number of intervals is determined automatically)
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="xTitle">X axis title</param>
        /// <param name="yTitle">Y axis title</param>
        /// <param name="xRange">X axis range</param>
        /// <param name="yRange">Y axis range</param>
        /// <param name="validation">Graph validation, null means no validation. Supported OutlineInside, OutlineOutside</param>
        /// <returns>Graph definition object</returns>
        /// \~Chinese
        /// <summary>
        /// 创建带数据验证的图表定义（柱状图区间个数自动计算）
        /// </summary>
        /// <param name="title">图表标题</param>
        /// <param name="xTitle">x轴标题</param>
        /// <param name="yTitle">y轴标题</param>
        /// <param name="xRange">x轴范围</param>
        /// <param name="yRange">y轴范围</param>
        /// <param name="validation">数据验证方式，null表示不验证。支持OutlineInside, OutlineOutside</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition CreateDefinitionWithValidation(String title, String xTitle, String yTitle, ScatterRange xRange, ScatterRange yRange, GraphValidation validation)
        {
            return CreateDefinitionWithValidationAndOptions(title, xTitle, yTitle, xRange, yRange, validation, null);
        }

        /// \~English
        /// <summary>
        /// Create graph definition
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="xTitle">X axis title</param>
        /// <param name="yTitle">Y axis title</param>
        /// <param name="xRange">X axis range</param>
        /// <param name="yRange">Y axis range</param>
        /// <param name="validation">Graph validation, null means no validation. Supported OutlineInside, OutlineOutside</param>
        /// <param name="options">Additional options</param>
        /// <returns>Graph definition object</returns>
        /// \~Chinese
        /// <summary>
        /// 创建带数据验证和附加选项的图表定义
        /// </summary>
        /// <param name="title">图表标题</param>
        /// <param name="xTitle">x轴标题</param>
        /// <param name="yTitle">y轴标题</param>
        /// <param name="xRange">x轴范围</param>
        /// <param name="yRange">y轴范围</param>
        /// <param name="validation">数据验证方式，null表示不验证。支持OutlineInside, OutlineOutside</param>
        /// <param name="options">附加选项</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition CreateDefinitionWithValidationAndOptions(String title, String xTitle, String yTitle, ScatterRange xRange, ScatterRange yRange, GraphValidation validation, ScatterOptions options)
        {
            var def = new GraphDefinition();
            def.Type = GraphType.ScatterPoints;
            def.MainTitle = title;
            def.Config.Add(xRange.lower.ToString());
            def.Config.Add(xRange.upper.ToString());
            def.Config.Add(yRange.lower.ToString());
            def.Config.Add(yRange.upper.ToString());
            def.ColumnTitles.Add(xTitle);
            def.ColumnTitles.Add(yTitle);

            if (options != null)
            {
                def.Config.Add((options.XHistCount == null ? 0 : Math.Max(0, options.XHistCount.Value)).ToString());
                def.Config.Add((options.YHistCount == null ? 0 : Math.Max(0, options.YHistCount.Value)).ToString());
            }

            if (validation != null)
            {
                var vdType = validation.GetValidationType();
                if (vdType == GraphValidationType.OutlineInside ||
                    vdType == GraphValidationType.OutlineOutside) def.Validation = validation;
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
            return Definition.ColumnTitles[0];
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
            return Definition.ColumnTitles[1];
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
        public ScatterRange GetXRange()
        {
            double lower = 0, upper = 0;
            Double.TryParse(Definition.Config[0], out lower);
            Double.TryParse(Definition.Config[1], out upper);
            return new ScatterRange(lower, upper);
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
        public ScatterRange GetYRange()
        {
            double lower = 0, upper = 0;
            Double.TryParse(Definition.Config[2], out lower);
            Double.TryParse(Definition.Config[3], out upper);
            return new ScatterRange(lower, upper);
        }

        /// \~English
        /// <summary>
        /// Get number of intervals along X axis
        /// </summary>
        /// <returns>Number of intervals along X axis</returns>
        /// \~Chinese
        /// <summary>
        /// 获取x轴柱状图区间个数
        /// </summary>
        /// <returns>x轴柱状图区间个数</returns>
        public int GetXHistCount()
        {
            int? xHistCount = null;
            if (Definition.Config.Count >= 6)
            {
                int val = 0;
                Int32.TryParse(Definition.Config[4], out val);
                if (val > 0) xHistCount = val;
            }

            if (xHistCount == null)
            {
                var xRange = GetXRange();
                var range = xRange.upper - xRange.lower;
                for (int i = 20; i >= 5; i--)
                {
                    var step = range / i;
                    
                    var logVal = Math.Log10(step);
                    if (logVal == (int)logVal)
                    {
                        xHistCount = i;
                        break;
                    }

                    logVal = Math.Log10(step * 0.5);
                    if (logVal == (int)logVal)
                    {
                        xHistCount = i;
                        break;
                    }

                    logVal = Math.Log10(step * 0.2);
                    if (logVal == (int)logVal)
                    {
                        xHistCount = i;
                        break;
                    }
                }
            }

            if (xHistCount == null) xHistCount = 5;

            return xHistCount.Value;
        }

        /// \~English
        /// <summary>
        /// Get number of intervals along Y axis
        /// </summary>
        /// <returns>Number of intervals along Y axis</returns>
        /// \~Chinese
        /// <summary>
        /// 获取y轴柱状图区间个数
        /// </summary>
        /// <returns>y轴柱状图区间个数</returns>
        public int GetYHistCount()
        {
            int? yHistCount = null;
            if (Definition.Config.Count >= 6)
            {
                int val = 0;
                Int32.TryParse(Definition.Config[5], out val);
                if (val > 0) yHistCount = val;
            }

            if (yHistCount == null)
            {
                var yRange = GetYRange();
                var range = yRange.upper - yRange.lower;
                for (int i = 20; i >= 5; i--)
                {
                    var step = range / i;

                    var logVal = Math.Log10(step);
                    if (logVal == (int)logVal)
                    {
                        yHistCount = i;
                        break;
                    }

                    logVal = Math.Log10(step * 0.5);
                    if (logVal == (int)logVal)
                    {
                        yHistCount = i;
                        break;
                    }

                    logVal = Math.Log10(step * 0.2);
                    if (logVal == (int)logVal)
                    {
                        yHistCount = i;
                        break;
                    }
                }
            }

            if (yHistCount == null) yHistCount = 5;

            return yHistCount.Value;
        }

        /// \~English
        /// <summary>
        /// Get number of scatter points in each interval along X axis
        /// </summary>
        /// <returns>Number of scatter points in each interval along X axis</returns>
        /// \~Chinese
        /// <summary>
        /// 获取x轴柱状图各区间散点数
        /// </summary>
        /// <returns>x轴柱状图各区间散点数</returns>
        public int[] GetXHistValues()
        {
            var xRange = GetXRange();
            var xCount = GetXHistCount();
            var stepInv = (double)xCount / (xRange.upper - xRange.lower);
            var pts = GetPoints();

            var output = new int[xCount];
            foreach (var pt in pts)
            {
                double target = (pt.X - xRange.lower) * stepInv;
                if (target < 0 || target > xCount) continue;
                int targetI = Math.Min(xCount - 1, (int)target);
                output[targetI]++;
            }

            return output;
        }

        /// \~English
        /// <summary>
        /// Get number of scatter points in each interval along Y axis
        /// </summary>
        /// <returns>Number of scatter points in each interval along Y axis</returns>
        /// \~Chinese
        /// <summary>
        /// 获取y轴柱状图各区间散点数
        /// </summary>
        /// <returns>y轴柱状图各区间散点数</returns>
        public int[] GetYHistValues()
        {
            var yRange = GetYRange();
            var yCount = GetYHistCount();
            var stepInv = (double)yCount / (yRange.upper - yRange.lower);
            var pts = GetPoints();

            var output = new int[yCount];
            foreach (var pt in pts)
            {
                double target = (pt.Y - yRange.lower) * stepInv;
                if (target < 0 || target > yCount) continue;
                int targetI = Math.Min(yCount - 1, (int)target);
                output[targetI]++;
            }

            return output;
        }

        /// \~English
        /// <summary>
        /// Add sample point
        /// </summary>
        /// <param name="point">Sample point, do nothing if over the range</param>
        /// \~Chinese
        /// <summary>
        /// 添加样本点
        /// </summary>
        /// <param name="point">样本点，若超出范围则不添加</param>
        public void AddPoint(FloatPoint point)
        {
            var xRange = GetXRange();
            var yRange = GetYRange();
            if (point.X < xRange.lower || point.X > xRange.upper) return;
            if (point.Y < yRange.lower || point.Y > yRange.upper) return;

            uint count = 0;
            if (!UInt32.TryParse(Params[0], out count))
            {
                Params[0] = "0";
            }

            int columns = Data.GetLength(1);
            if (Data.GetLength(0) <= count)
            {
                var newData = new double[count * 2, columns];
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        newData[i, j] = Data[i, j];
                    }
                }
                Data = newData;
            }

            Data[count, 0] = point.X;
            Data[count, 1] = point.Y;
            if (columns > 2)
            {
                Data[count, 2] = 0;
                Data[count, 3] = 0;
            }
            Params[0] = (count + 1).ToString();
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.0) Add sample point with time info
        /// </summary>
        /// <param name="point">Sample point, do nothing if over the range</param>
        /// <param name="session">The session that it belongs to</param>
        /// <param name="offset">The time offset of the sample in the session</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.0) 添加含时间信息的样本点
        /// </summary>
        /// <param name="point">样本点，若超出范围则不添加</param>
        /// <param name="session">样本点所在session</param>
        /// <param name="offset">样本点在该session中的时间戳</param>
        public void AddPointWithTimeInfo(FloatPoint point, SessionIdentifier session, double offset)
        {
            var xRange = GetXRange();
            var yRange = GetYRange();
            if (point.X < xRange.lower || point.X > xRange.upper) return;
            if (point.Y < yRange.lower || point.Y > yRange.upper) return;

            uint count = 0;
            if (!UInt32.TryParse(Params[0], out count))
            {
                Params[0] = "0";
            }

            int originColumns = Data.GetLength(1); // may < 4
            if (Data.GetLength(0) <= count || originColumns < 4)
            {
                var newData = new double[count * (Data.GetLength(0) <= count ? 2 : 1), 4];
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < originColumns; j++)
                    {
                        newData[i, j] = Data[i, j];
                    }
                }
                Data = newData;
            }

            Data[count, 0] = point.X;
            Data[count, 1] = point.Y;
            Data[count, 2] = (long)(session.ToDateTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            Data[count, 3] = offset;

            Params[0] = (count + 1).ToString();
        }

        /// \~English
        /// <summary>
        /// Get all scatter points
        /// </summary>
        /// <returns>All scatter points</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有散点样本
        /// </summary>
        /// <returns>散点样本数组</returns>
        public FloatPoint[] GetPoints()
        {
            uint count = 0;
            if (!UInt32.TryParse(Params[0], out count))
            {
                return new FloatPoint[0];
            }

            var pts = new FloatPoint[count];
            for (int i = 0; i < count; i++)
            {
                pts[i].X = (float)Data[i, 0];
                pts[i].Y = (float)Data[i, 1];
            }
            return pts;
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.0) Get time info of a scatter point
        /// </summary>
        /// <param name="index">Index of scatter point</param>
        /// <param name="session">Output the session that it belongs to</param>
        /// <param name="offset">Output the time offset of the sample in the session</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.0) 获取某个散点样本的时间信息
        /// </summary>
        /// <param name="index">样本在数组中的序号</param>
        /// <param name="session">输出样本点所在session</param>
        /// <param name="offset">输出样本点在该session中的时间戳</param>
        /// <returns>是否成功获取时间信息</returns>
        public bool GetPointTimeInfo(int index, ref SessionIdentifier session, ref double offset)
        {
            uint count = 0;
            if (!UInt32.TryParse(Params[0], out count)) return false;

            if (index < 0 || index >= count) return false;
            if (Data.GetLength(1) != 4) return false;
            if (Data[index, 2] == 0) return false;

            session = SessionIdentifier.FromDateTime(new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Data[index, 2]));
            offset = Data[index, 3];
            return true;
        }

        public override void InitParamsAndData()
        {
            Params.Clear();
            Params.Add("0");
            Data = new double[100, 4];
        }

        public override void MergeWith(GraphData data)
        {
            uint srcCount = 0, dstCount = 0;
            if (!UInt32.TryParse(Params[0], out srcCount) ||
                !UInt32.TryParse(data.Params[0], out dstCount))
            {
                return;
            }

            int srcColumns = Data.GetLength(1);
            int dstColumns = data.Data.GetLength(1);
            int columns = Math.Max(srcColumns, dstColumns);

            if (srcCount + dstCount > Data.GetLength(0) || columns > srcColumns)
            {
                var newData = new double[srcCount + dstCount, columns];
                for (int i = 0; i < srcCount; i++)
                {
                    for (int j = 0; j < srcColumns; j++)
                    {
                        newData[i, j] = Data[i, j];
                    }
                }
                Data = newData;
            }

            for (int i = 0; i < dstCount; i++)
            {
                for (int j = 0; j < dstColumns; j++)
                {
                    Data[srcCount + i, j] = data.Data[i, j];
                }
            }
            Params[0] = (srcCount + dstCount).ToString();
        }

        public override bool HasData()
        {
            if (Params.Count == 0) return false;
            double count = 0;
            if (Double.TryParse(Params[0], out count)) return count > 0;
            return false;
        }
    }
}
