using System;

namespace ASEva.Samples
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 单个总线报文样本
    /// </summary>
    public class BusMessageSample : Sample
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 接收来源通道或发送目标通道（1~16）
        /// </summary>
        public byte Channel { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 总线报文的实际长度
        /// </summary>
        public ushort Length { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 报文ID
        /// </summary>
        public uint ID { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 报文数据，长度限定在64以下，可能小于报文的实际长度
        /// </summary>
        public byte[] Data { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.10.2) 总线通道类型
        /// </summary>
		public BusChannelType Type { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.10.2) 总线数据的(发送)状态
        /// </summary>
		public BusRawDataState State { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.10.2) 预约发送的时间偏置，0表示无效
        /// </summary>
		public double ScheduleTimeOffset { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.10.2) 预约发送的授时服务器时间，单位纳秒，0表示无效
        /// </summary>
		public ulong SchedulePosixTime { get; set; }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 总线报文样本集合
    /// </summary>
    public class BusDataPack
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 总线报文样本列表
        /// </summary>
        public BusMessageSample[] Samples { get; set; }
    }
}
