using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 场景片段描述
    /// </summary>
    public class SceneData
    {
        /// <summary>
        /// 场景ID
        /// </summary>
        public String SceneID { get; set; }
        /// <summary>
        /// 场景所属session ID
        /// </summary>
        public DateTime BeginBase { get; set; }
        /// <summary>
        /// 场景开始时间（在该session中的时间偏置）
        /// </summary>
        public double BeginOffset { get; set; }
        /// <summary>
        /// 场景时间长度
        /// </summary>
        public double TimeLength { get; set; }
        /// <summary>
        /// 场景属性列表
        /// </summary>
        public String[] PropertyValues { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SceneData()
        {
            PropertyValues = new string[0];
        }

        /// <summary>
        /// 从另一个对象复制
        /// </summary>
        /// <param name="data">复制来源</param>
        public void Clone(SceneData data)
        {
            SceneID = data.SceneID;
            BeginBase = data.BeginBase;
            BeginOffset = data.BeginOffset;
            TimeLength = data.TimeLength;
            PropertyValues = (string[])data.PropertyValues.Clone();
        }
    }
}
