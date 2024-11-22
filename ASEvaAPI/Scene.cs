using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASEva
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Scenario segment
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 场景片段描述
    /// </summary>
    public class SceneData
    {
        /// \~English
        /// <summary>
        /// Scenario ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 场景ID
        /// </summary>
        public String SceneID { get; set; }

        /// \~English
        /// <summary>
        /// (api:app=3.2.0) The session that scenario belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.0) 场景所属session ID
        /// </summary>
        public SessionIdentifier Session { get; set; }

        /// \~English
        /// <summary>
        /// Start time (time offset in the session)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 场景开始时间（在该session中的时间偏置）
        /// </summary>
        public double BeginOffset { get; set; }

        /// \~English
        /// <summary>
        /// Duration of the scenario segment
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 场景时间长度
        /// </summary>
        public double TimeLength { get; set; }

        /// \~English
        /// <summary>
        /// Scenario properties
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 场景属性列表
        /// </summary>
        public String[] PropertyValues { get; set; }

        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SceneData()
        {
            PropertyValues = new string[0];
        }

        /// \~English
        /// <summary>
        /// Clone from another segment
        /// </summary>
        /// <param name="data">Another segment</param>
        /// \~Chinese
        /// <summary>
        /// 从另一个对象复制
        /// </summary>
        /// <param name="data">复制来源</param>
        public void Clone(SceneData data)
        {
            SceneID = data.SceneID;
            Session = data.Session;
            BeginOffset = data.BeginOffset;
            TimeLength = data.TimeLength;
            PropertyValues = (string[])data.PropertyValues.Clone();
        }
    }
}
