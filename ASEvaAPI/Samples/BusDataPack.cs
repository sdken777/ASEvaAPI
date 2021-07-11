using System;

namespace ASEva.Samples
{
    /// <summary>
    /// (api:app=2.0.0) 单个总线报文样本
    /// </summary>
    public class BusMessageSample : Sample
    {
        /// <summary>
        /// 来源通道（1~16）
        /// </summary>
        public byte Channel { get; set; }

        /// <summary>
        /// 总线报文的实际长度
        /// </summary>
        public ushort Length { get; set; }

        /// <summary>
        /// 报文ID
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// 报文数据，长度限定在64以下，可能小于报文的实际长度
        /// </summary>
        public byte[] Data { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 总线报文样本集合
    /// </summary>
    public class BusDataPack
    {
        /// <summary>
        /// 总线报文样本列表
        /// </summary>
        public BusMessageSample[] Samples { get; set; }
    }
}
