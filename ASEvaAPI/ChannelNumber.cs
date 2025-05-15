using System;

namespace ASEva
{
    /// \~English
    /// <summary>
    /// (api:app=3.9.0) Channel number
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.9.0) 通道数量
    /// </summary>
    public class ChannelNumber
    {
        /// \~English
        /// <summary>
        /// Bus data channel number
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线数据通道数
        /// </summary>
        public static int Bus => 48;

        /// \~English
        /// <summary>
        /// Video data channel number
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频通道数
        /// </summary>
        public static int Video => 24;

        /// \~English
        /// <summary>
        /// Built-in manual trigger channel number
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 内建手动触发器通道数
        /// </summary>
        public static int ManualTrigger => 16;

        /// \~English
        /// <summary>
        /// Point cloud channel number
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 点云通道数
        /// </summary>
        public static int PointCloud => 12;
    }
}