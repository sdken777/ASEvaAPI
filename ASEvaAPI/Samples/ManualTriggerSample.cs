using System;

namespace ASEva.Samples
{
    /// <summary>
    /// (api:app=2.0.0) 手动触发器样本
    /// </summary>
    public class ManualTriggerSample : Sample
    {
        /// <summary>
        /// 手动触发器状态列表，长度16
        /// </summary>
        public bool[] States { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ManualTriggerSample()
        {
            States = new bool[16];
        }
    }
}
