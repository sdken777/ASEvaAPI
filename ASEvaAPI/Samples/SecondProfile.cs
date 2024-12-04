using System;
using System.Collections.Generic;

namespace ASEva
{
    /// \~English
    /// <summary>
    /// (api:app=3.7.0) Channel data's second profile sample
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.0) 通道数据的秒侧写样本
    /// </summary>
    public class SecondProfileSample : Sample
    {
        /// \~English
        /// <summary>
        /// Data channel ID, with format as "protocol@channel". Channel is starting from 0. The bus channel's protocol is "bus". The video channel's protocol is "video". The audio channel's ID is "audio"
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 数据通道ID，格式为"协议名@通道序号"，通道序号从0开始。总线协议名为bus，视频协议名为video，音频协议名为audio
        /// </summary>
        public String ChannelID { get; set; }

        /// \~English
        /// <summary>
        /// The second of the profile, corresponding to data in range of [N, N+1), and the profile sample's offset is N+0.5
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 侧写所在秒，对应[N, N+1)内的数据，侧写样本的时间偏置为N+0.5
        /// </summary>
        public int Second { get; set; }

        /// \~English
        /// <summary>
        /// Frame count at data source
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 数据源处的帧数
        /// </summary>
        public int SourceCount { get; set; }

        /// \~English
        /// <summary>
        /// Available frame count at current side
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 当前端可用帧数
        /// </summary>
        public int AvailableCount { get; set; }

        /// \~English
        /// <summary>
        /// Channel data's time offsets in the time range, can be null
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道数据在该时间范围内的所有时间偏置，可为null
        /// </summary>
        public double[] Offsets { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.7.0) All profile samples of one second
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.0) 一秒内的所有侧写样本集合
    /// </summary>
    public class SecondProfilePack
    {
        /// \~English
        /// <summary>
        /// The second of the profiles, corresponding to data in range of [N, N+1)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 侧写所在秒，对应[N, N+1)内的数据
        /// </summary>
        public int Second { get; set; }

        /// \~English
        /// <summary>
        /// Sample table, key is data channel ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 样本表，键为数据通道ID
        /// </summary>
        public Dictionary<String, SecondProfileSample> Profiles { get; set; }
    }
}