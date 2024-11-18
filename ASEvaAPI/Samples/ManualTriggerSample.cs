using System;

namespace ASEva.Samples
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Manual trigger sample
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 手动触发器样本
    /// </summary>
    public class ManualTriggerSample : Sample
    {
        /// \~English
        /// <summary>
        /// State of each channel, length is 16
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 手动触发器状态列表，长度16
        /// </summary>
        public bool[] States { get; set; } = new bool[16];
    }
}
