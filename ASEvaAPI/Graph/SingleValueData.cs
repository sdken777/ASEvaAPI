using System;

namespace ASEva.Graph
{
    /// <summary>
    /// (api:app=2.0.0) 单值数据
    /// </summary>
    public class SingleValueData : GraphData
    {
        /// <summary>
        /// 创建图表定义（不带数据验证）
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition CreateDefinition(String title)
        {
            return CreateDefinitionWithValidation(title, null);
        }

        /// <summary>
        /// 创建带数据验证的图表定义
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="validation">数据验证方式，null表示不验证。支持ValueAbove, ValueBelow, ValueInRange</param>
        /// <returns>图表定义对象</returns>
        public static GraphDefinition CreateDefinitionWithValidation(String title, GraphValidation validation)
        {
            var def = new GraphDefinition();
            def.Type = GraphType.SingleValue;
            def.MainTitle = title;
            def.ColumnTitles.Add("Value");

            if (validation != null)
            {
                var vdType = validation.GetValidationType();
                if (vdType == GraphValidationType.ValueAbove ||
                    vdType == GraphValidationType.ValueBelow ||
                    vdType == GraphValidationType.ValueInRange) def.Validation = validation;
            }

            return def;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="val">设置值</param>
        public void SetValue(double val)
        {
            Data[0, 0] = val;
        }

        /// <summary>
        /// 添加值
        /// </summary>
        /// <param name="val">添加值</param>
        public void AddValue(double val)
        {
            if (Double.IsNaN(Data[0, 0])) Data[0, 0] = val;
            else Data[0, 0] += val;
        }

        /// <summary>
        /// 取得值
        /// </summary>
        /// <returns>取得值</returns>
        public double GetValue()
        {
            return Data[0, 0];
        }

        public override void InitParamsAndData()
        {
            Data = new double[1, 1];
            Data[0, 0] = Double.NaN;
        }

        public override void MergeWith(GraphData data)
        {
            var D = data as SingleValueData;

            var srcVal = Data[0, 0];
            var targetVal = D.GetValue();

            if (!Double.IsNaN(targetVal))
            {
                if (Double.IsNaN(srcVal)) Data[0, 0] = targetVal;
                else Data[0, 0] += targetVal;
            }
        }

        public override bool HasData()
        {
            return Data.Length != 0 && !Double.IsNaN(Data[0, 0]);
        }
    }
}
