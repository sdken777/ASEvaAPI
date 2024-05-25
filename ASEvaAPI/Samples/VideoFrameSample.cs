using System;
using System.IO;
using System.Reflection;
using ASEva.Utility;

namespace ASEva.Samples
{
    /// \~English
    /// <summary>
    /// (api:app=2.17.0) Video frame sample
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.17.0) 视频帧样本
    /// </summary>
    public class VideoFrameSampleX : Sample
    {
        /// \~English
        /// <summary>
        /// Common image data (in order of BGR)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通用图像数据(BGR不逆序)
        /// </summary>
        public CommonImage CommonImage { get; set; }

        /// \~English
        /// <summary>
        /// Video channel (0~23)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频帧所属通道（0~23）
        /// </summary>
        public int ChannelIndex { get; set; }

        /// \~English
        /// <summary>
        /// Camera information
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 摄像头信息
        /// </summary>
        public CameraInfo CameraInfo { get; set; }
    }
}
