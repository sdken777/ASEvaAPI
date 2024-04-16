using System;

namespace ASEva.Samples
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Single bus message sample
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 单个总线报文样本
    /// </summary>
    public class BusMessageSample : Sample
    {
        /// \~English
        /// <summary>
        /// Bus channel, ranges 1~16
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 接收来源通道或发送目标通道（1~16）
        /// </summary>
        public byte Channel { get; set; }

        /// \~English
        /// <summary>
        /// Actual length of the bus message
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线报文的实际长度
        /// </summary>
        public ushort Length { get; set; }

        /// \~English
        /// <summary>
        /// Local ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 报文ID
        /// </summary>
        public uint ID { get; set; }

        /// \~English
        /// <summary>
        /// Bus message data, the length is limited to 64 bytes, so it may be smaller than actual length
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 报文数据，长度限定在64以下，可能小于报文的实际长度
        /// </summary>
        public byte[] Data { get; set; }

        /// \~English
        /// <summary>
        /// Bus channel type
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线通道类型
        /// </summary>
		public BusChannelType Type { get; set; }

        /// \~English
        /// <summary>
        /// Data state (mainly for transmitting)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线数据的(发送)状态
        /// </summary>
		public BusRawDataState State { get; set; }

        /// \~English
        /// <summary>
        /// Time offset of scheduled transmitting, 0 means invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 预约发送的时间偏置，0表示无效
        /// </summary>
		public double ScheduleTimeOffset { get; set; }

        /// \~English
        /// <summary>
        /// Server posix time of scheduled transmitting, 0 means invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 预约发送的授时服务器时间，单位纳秒，0表示无效
        /// </summary>
		public ulong SchedulePosixTime { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Pack of bus message data
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线报文样本集合
    /// </summary>
    public class BusDataPack
    {
        /// \~English
        /// <summary>
        /// Samples of bus message data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线报文样本列表
        /// </summary>
        public BusMessageSample[] Samples { get; set; }
    }
}
