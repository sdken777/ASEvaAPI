using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 图表类型
    /// </summary>
    public enum GraphType
    {
        /// <summary>
        /// 无效图表
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// 单值
        /// </summary>
        SingleValue = 1,
        /// <summary>
        /// 散点图
        /// </summary>
        ScatterPoints = 2,
        /// <summary>
        /// 直方折线图
        /// </summary>
        HistAndLine = 3,
        /// <summary>
        /// 矩阵表
        /// </summary>
        MatrixTable = 4,
        /// <summary>
        /// 标签表
        /// </summary>
        LabelTable = 5,
    }

    /// <summary>
    /// (api:app=2.0.0) 图表数据验证类型
    /// </summary>
    public enum GraphValidationType
    {
        /// <summary>
        /// 不验证
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// 阈值下方有效
        /// </summary>
        ValueBelow = 1,

        /// <summary>
        /// 阈值上方有效
        /// </summary>
        ValueAbove = 2,

        /// <summary>
        /// 范围区间内有效
        /// </summary>
        ValueInRange = 3,

        /// <summary>
        /// 折线下方有效
        /// </summary>
        PolyBelow = 11,

        /// <summary>
        /// 折线上方有效
        /// </summary>
        PolyAbove = 12,

        /// <summary>
        /// 轮廓范围内有效
        /// </summary>
        OutlineInside = 21,

        /// <summary>
        /// 轮廓范围外有效
        /// </summary>
        OutlineOutside = 22,
    }

    /// <summary>
    /// (api:app=2.0.0) 图表数据验证接口
    /// </summary>
    public interface GraphValidation
    {
        GraphValidationType GetValidationType();
        List<String> ToStringList();
        bool FromStringList(List<String> stringList);
        bool? Validate(GraphData data, out double? percentage);
    }

    /// <summary>
    /// (api:app=2.0.0) 图表定义
    /// </summary>
    public class GraphDefinition
    {
        /// <summary>
        /// 图表类型
        /// </summary>
        public GraphType Type { get; set; }

        /// <summary>
        /// 图表定义相关参数
        /// </summary>
        public List<String> Config { get; set; }

        /// <summary>
        /// 图表标题
        /// </summary>
        public String MainTitle { get; set; }

        /// <summary>
        /// 各列数据标题
        /// </summary>
        public List<String> ColumnTitles { get; set; }

        /// <summary>
        /// 图表数据验证方式
        /// </summary>
        public GraphValidation Validation { get; set; }

        public GraphDefinition()
        {
            Type = GraphType.Invalid;
            Config = new List<string>();
            MainTitle = "";
            ColumnTitles = new List<string>();
            Validation = null;
        }

        /// <summary>
        /// 获取图表ID
        /// </summary>
        /// <returns>图表ID</returns>
        public int GetID()
        {
            return GetHashCode();
        }

        public override string ToString()
        {
            if (MainTitle == null || MainTitle.Length == 0 ||
                ColumnTitles == null || ColumnTitles.Count == 0) return "";

            var target = "";
            target += Type.ToString();

            if (Config != null && Config.Count > 0)
            {
                foreach (var config in Config)
                {
                    target += ":" + config;
                }
            }

            target += ":" + MainTitle;

            foreach (var title in ColumnTitles)
            {
                target += ":" + title;
            }

            if (Validation != null)
            {
                var validationStrings = Validation.ToStringList();
                foreach (var str in validationStrings)
                {
                    target += ":" + str;
                }
            }

            return target;
        }

        public override bool Equals(object obj)
        {
            if (obj is GraphDefinition)
            {
                return (obj as GraphDefinition).ToString() == this.ToString();
            }
            else return false;
        }

        public override int GetHashCode()
        {
            var bytes = Encoding.UTF8.GetBytes(this.ToString());

            uint hash = 0;
            foreach (byte b in bytes)
            {
                hash += (uint)b;
                hash *= 3;
            }

            hash &= 0x7fffffff;
            return (int)hash;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 图表数据
    /// </summary>
    public class GraphData
    {
        /// <summary>
        /// 图表ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 图表定义
        /// </summary>
        public GraphDefinition Definition { get; set; }

        /// <summary>
        /// 图表数据相关参数
        /// </summary>
        public List<String> Params { get; set; }

        /// <summary>
        /// 图表数据
        /// </summary>
        public double[,] Data { get; set; }

        public GraphData()
        {
            ID = 0;
            Definition = new GraphDefinition();
            Params = new List<string>();
            Data = new double[0, 0];
        }

        /// <summary>
        /// 图表数据初始化，需要派生类实现
        /// </summary>
        public virtual void InitParamsAndData()
        {

        }

        /// <summary>
        /// 与其他图表数据合并，需要派生类实现
        /// </summary>
        /// <param name="data">其他图表数据，需要ID一致</param>
        public virtual void MergeWith(GraphData data)
        {

        }

        /// <summary>
        /// 是否已添加或赋值数据 ，需要派生类实现
        /// </summary>
        /// <returns>是否已添加或赋值数据 </returns>
        public virtual bool HasData()
        {
            return false;
        }

        /// <summary>
        /// 验证图表数据
        /// </summary>
        /// <param name="percentage">输出数据OK的百分比</param>
        /// <returns>数据验证是否通过</returns>
        public bool? Validate(out double? percentage)
        {
            if (Definition.Validation == null)
            {
                percentage = null;
                return null;
            }
            else return Definition.Validation.Validate(this, out percentage);
        }

        /// <summary>
        /// 保存至文件
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>保存是否成功</returns>
        public bool Save(String file)
        {
            try
            {
                var root = Path.GetDirectoryName(file);
                if (!Directory.Exists(root)) Directory.CreateDirectory(root);
                if (!Directory.Exists(root)) return false;

                var writer = new StreamWriter(file);

                // 第一行：header，ID，标题，数据参数
                var paramsText = "";
                if (Params != null)
                {
                    foreach (var param in Params)
                    {
                        paramsText += "," + param;
                    }
                }
                writer.WriteLine("ASEva Report v2," + ID + "," + Definition.MainTitle + paramsText);

                // 第二行：类型，配置
                var configText = "";
                if (Definition.Config != null)
                {
                    foreach (var config in Definition.Config)
                    {
                        configText += "," + config;
                    }
                }
                writer.WriteLine(Definition.Type.ToString() + configText);

                // 第三行：数据验证
                if (Definition.Validation == null)
                {
                    writer.WriteLine("NoValidation");
                }
                else
                {
                    writer.WriteLine(String.Join(",", Definition.Validation.ToStringList()));
                }

                // 第四行：列标题
                var columnsText = "";
                foreach (var title in Definition.ColumnTitles)
                {
                    columnsText += (columnsText.Length == 0 ? "" : ",") + title;
                }
                writer.WriteLine(columnsText);

                // 数据
                int rows = Data.GetLength(0);
                int cols = Data.GetLength(1);

                for (int i = 0; i < rows; i++)
                {
                    var rowText = "";
                    rowText += Data[i, 0];
                    for (int j = 1; j < cols; j++)
                    {
                        rowText += "," + Data[i, j];
                    }
                    writer.WriteLine(rowText);
                }

                writer.Close();

                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// 根据图表定义创建初始化完成后的数据
        /// </summary>
        /// <param name="definition">图表定义</param>
        /// <returns>图表数据对象</returns>
        public static GraphData Create(GraphDefinition definition)
        {
            var rawData = new GraphData();
            rawData.ID = definition.GetID();
            rawData.Definition = definition;

            var output = CreateGraphDataEncapsulation(rawData);
            if (output == null) return null;

            output.InitParamsAndData();
            return output;
        }

        /// <summary>
        /// 从文件读取图表数据
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>图表数据对象</returns>
        public static GraphData Load(String file)
        {
            try
            {
                var reader = new StreamReader(file);

                // 第一行：header，ID，标题，数据参数
                var comps = reader.ReadLine().Split(',');
                if (comps.Length < 3 || (comps[0] != "ASEva Report" && comps[0] != "ASEva Report v2")) return null;

                bool isV2 = comps[0] == "ASEva Report v2";
                var id = Convert.ToInt32(comps[1]);
                var title = comps[2];

                var paramList = new List<String>();
                for (int i = 3; i < comps.Length; i++)
                {
                    paramList.Add(comps[i]);
                }

                // 第二行：类型，配置
                comps = reader.ReadLine().Split(',');

                GraphType type = GraphType.Invalid;
                if (!Enum.TryParse<GraphType>(comps[0], out type)) return null;

                var configList = new List<String>();
                for (int i = 1; i < comps.Length; i++)
                {
                    configList.Add(comps[i]);
                }

                // 第三行：数据验证
                GraphValidation validation = null;
                if (isV2)
                {
                    validation = RowStringToValidation(reader.ReadLine());
                }

                // 第四行：列标题
                var columnList = reader.ReadLine().Split(',');

                // 数据
                var rowTexts = new List<String>();
                while (true)
                {
                    var rowText = reader.ReadLine();
                    if (rowText == null || rowText.Length == 0) break;
                    rowTexts.Add(rowText);
                }
                if (rowTexts.Count == 0) return null;
                var rows = rowTexts.ToArray();

                var cols = rows[0].Split(',').Length;
                var data = new double[rows.Length, cols];
                for (int i = 0; i < rows.Length; i++)
                {
                    comps = rows[i].Split(',');
                    for (int j = 0; j < cols; j++)
                    {
                        data[i, j] = Convert.ToDouble(comps[j]);
                    }
                }

                var rawOutput = new GraphData();
                rawOutput.ID = id;
                rawOutput.Definition.Type = type;
                rawOutput.Definition.MainTitle = title;
                rawOutput.Definition.Config = configList;
                rawOutput.Definition.ColumnTitles.AddRange(columnList);
                rawOutput.Definition.Validation = validation;
                rawOutput.Params = paramList;
                rawOutput.Data = data;

                return CreateGraphDataEncapsulation(rawOutput);
            }
            catch (Exception) { return null; }
        }

        private static GraphValidation RowStringToValidation(String rowText)
        {
            var commaIndex = rowText.IndexOf(',');
            if (commaIndex <= 0) return null;

            var comps = rowText.Split(',').ToList();

            switch (rowText.Substring(0, commaIndex))
            {
                case "ValueBelow":
                    {
                        var output = new Graph.ValueBelowValidation();
                        output.FromStringList(comps);
                        return output;
                    }
                case "ValueAbove":
                    {
                        var output = new Graph.ValueAboveValidation();
                        output.FromStringList(comps);
                        return output;
                    }
                case "ValueInRange":
                    {
                        var output = new Graph.ValueInRangeValidation();
                        output.FromStringList(comps);
                        return output;
                    }
                case "PolyBelow":
                    {
                        var output = new Graph.PolyBelowValidation();
                        output.FromStringList(comps);
                        return output;
                    }
                case "PolyAbove":
                    {
                        var output = new Graph.PolyAboveValidation();
                        output.FromStringList(comps);
                        return output;
                    }
                case "OutlineInside":
                    {
                        var output = new Graph.OutlineInsideValidation();
                        output.FromStringList(comps);
                        return output;
                    }
                case "OutlineOutside":
                    {
                        var output = new Graph.OutlineOutsideValidation();
                        output.FromStringList(comps);
                        return output;
                    }
            }

            return null;
        }

        private static GraphData CreateGraphDataEncapsulation(GraphData data)
        {
            if (data == null) return null;

            GraphData output = null;
            switch (data.Definition.Type)
            {
                case GraphType.SingleValue:
                    output = new Graph.SingleValueData();
                    break;
                case GraphType.ScatterPoints:
                    output = new Graph.ScatterPointsData();
                    break;
                case GraphType.HistAndLine:
                    output = new Graph.HistAndLineData();
                    break;
                case GraphType.MatrixTable:
                    output = new Graph.MatrixTableData();
                    break;
                case GraphType.LabelTable:
                    output = new Graph.LabelTableData();
                    break;
                default:
                    return null;
            }

            output.ID = data.ID;
            output.Definition = data.Definition;
            output.Params = data.Params;
            output.Data = data.Data;
            return output;
        }
    }
}
