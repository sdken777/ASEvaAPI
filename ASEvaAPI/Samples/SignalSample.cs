using System;

namespace ASEva.Samples
{
    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Signal sample output by processor
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 数据处理组件输出的信号样本
    /// </summary>
    public class SignalSample : Sample
    {
        /// \~English
        /// <summary>
        /// Prefix of signal ID, set to null for output (will be set by framework later)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号ID的前缀，输出时无需赋值（由系统赋值）
        /// </summary>
        public String Prefix { get; set; }

        /// \~English
        /// <summary>
        /// Signal name, should be in the list returned by ASEva.ProcessorClass.GetProcessorOutputSignalNames
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号的名称，应为 ASEva.ProcessorClass.GetProcessorOutputSignalNames 返回列表中的名称
        /// </summary>
        public String Name { get; set; }

        /// \~English
        /// <summary>
        /// Signal value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号值
        /// </summary>
        public double Value { get; set; }

        /// \~English
        /// <summary>
        /// Signal ID (get only)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号的全局唯一ID（仅get）
        /// </summary>
        public String SignalID
        {
            get
            {
                if (Prefix == null) return null;
                else if (Name.IndexOf(':') >= 0) return Prefix.Split(':')[0] + ":" + Name;
                else return Prefix + ":" + Name;
            }
        }
    }
}
