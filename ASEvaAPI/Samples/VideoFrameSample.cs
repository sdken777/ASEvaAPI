using System;

namespace ASEva.Samples
{
    /// <summary>
    /// (api:app=2.0.0) 视频帧样本
    /// </summary>
    public class VideoFrameSample : Sample
    {
        /// <summary>
        /// 获取视频帧的位图图像
        /// </summary>
        public object Image { get; set; }

        /// <summary>
        /// 视频帧所属通道（0~11）
        /// </summary>
        public int ChannelIndex { get; set; }

        /// <summary>
        /// 特殊摄像头类型
        /// </summary>
        public SpecialCameraType SpecialCameraType { get; set; }

        /// <summary>
        /// 特殊摄像头信息，可以为 ASEva.Samples.DefaultCameraMeta , ASEva.Samples.GenericCameraMeta , ASEva.Samples.LaneLineCameraMeta , ASEva.Samples.BlindSpotCameraMeta
        /// </summary>
        public object SpecialCameraInfo { get; set; }

        /// <summary>
        /// 横向视场角
        /// </summary>
        public double? HorizontalFov { get; set; }

        /// <summary>
        /// 是否对图像进行了放大，false表示按可贴合的最小尺寸缩放
        /// </summary>
        public bool Scaled { get; set; }

        /// <summary>
        /// 原始图像的原分辨率
        /// </summary>
        public IntSize RawResolution { get; set; }

        /// <summary>
        /// 在原始图像坐标系下的实际截取框中心
        /// </summary>
        public FloatPoint RawCenter { get; set; }

        /// <summary>
        /// 相对与原始图像大小的实际缩放比率
        /// </summary>
        public float RawScale { get; set; }

        /// <summary>
        /// 实际图像的原分辨率
        /// </summary>
        public IntSize ActualResolution { get; set; }

        /// <summary>
        /// 在实际图像坐标系下的实际截取框中心
        /// </summary>
        public FloatPoint ActualCenter { get; set; }

        /// <summary>
        /// 相对与实际图像大小的实际缩放比率
        /// </summary>
        public float ActualScale { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="rawSize">图像的原始大小</param>
        /// <param name="channelIndex">通道</param>
        /// <param name="bas">Session ID</param>
        /// <param name="offset">相对时间戳</param>
        /// <param name="timeline">时间线</param>
        public VideoFrameSample(object image, IntSize rawSize, int channelIndex, DateTime bas, double offset, double timeline)
            : base(bas, offset, timeline)
        {
            Image = image;
            ChannelIndex = channelIndex;
            SpecialCameraType = SpecialCameraType.Normal;
            SpecialCameraInfo = null;
            HorizontalFov = null;
            Scaled = false;
            RawResolution = rawSize;
            RawCenter = new FloatPoint((float)(rawSize.Width - 1) * 0.5f, (float)(rawSize.Height - 1) * 0.5f);
            RawScale = 1;
            ActualResolution = RawResolution;
            ActualCenter = RawCenter;
            ActualScale = 1;
        }
    }
}
