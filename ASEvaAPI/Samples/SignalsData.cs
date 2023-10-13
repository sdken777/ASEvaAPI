using System;

namespace ASEva.Samples
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 单个信号数据
    /// </summary>
    public struct SignalData
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 时间偏置，单位秒
        /// </summary>
        public double Offset { get; set; }
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 信号值
        /// </summary>
        public double Value { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="offset">时间偏置，单位秒</param>
        /// <param name="value">信号值</param>
        public SignalData(double offset, double value)
        {
            Offset = offset;
            Value = value;
        }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 输入的信号数据列表（单个信号类别）
    /// </summary>
    public class SignalsData
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 所属session ID
        /// </summary>
        public DateTime Base { get; set; }
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 信号的全局唯一ID
        /// </summary>
        public String SignalID { get; set; }
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 信号列表
        /// </summary>
        public SignalData[] Signals { get; set; }
    }
}
