using System;

namespace ASEva.Samples
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.3) Single signal data
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.3) 单个信号数据
    /// </summary>
    public struct SignalData
    {
        /// \~English
        /// <summary>
        /// Time offset, in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 时间偏置，单位秒
        /// </summary>
        public double Offset { get; set; }

        /// \~English
        /// <summary>
        /// Time offset synchronization status
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 时间偏置同步状态
        /// </summary>
        public TimeOffsetSync OffsetSync { get; set; }

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
        /// Default constructor
        /// </summary>
        /// <param name="offset">Time offset, in seconds</param>
        /// <param name="offsetSync">Time offset synchronization status</param>
        /// <param name="value">Signal value</param>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="offset">时间偏置，单位秒</param>
        /// <param name="offsetSync">时间偏置同步状态</param>
        /// <param name="value">信号值</param>
        public SignalData(double offset, TimeOffsetSync offsetSync, double value)
        {
            Offset = offset;
            OffsetSync = offsetSync;
            Value = value;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.2.0) Input signal data (For one signal ID)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.2.0) 输入的信号数据列表（单个信号类别）
    /// </summary>
    public class SignalsData
    {
        /// \~English
        /// <summary>
        /// The session that data belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属session ID
        /// </summary>
        public SessionIdentifier Session { get; set; }

        /// \~English
        /// <summary>
        /// Signal ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号的全局唯一ID
        /// </summary>
        public String SignalID { get; set; }

        /// \~English
        /// <summary>
        /// Signal data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号列表
        /// </summary>
        public SignalData[] Signals { get; set; }

        public SignalsData()
        {
            SignalID = "";
            Signals = [];
        }
    }
}
