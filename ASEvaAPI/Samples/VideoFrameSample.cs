using System;

namespace ASEva.Samples
{
    /// <summary>
    /// (api:app=2.3.0) 通用图像数据
    /// </summary>
    public class CommonImage
    {
        /// <summary>
        /// 图像宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 图像高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 图像数据，数组长度为宽度x高度x4，每个像素的存放顺序为BGRA
        /// </summary>
        public byte[] Data { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 视频帧样本
    /// </summary>
    public class VideoFrameSample : Sample
    {
        /// <summary>
        /// (api:app=2.3.0) 通用图像数据
        /// </summary>
        /// <value></value>
        public CommonImage CommonImage { get; set; }

        /// <summary>
        /// 获取视频帧的位图图像（平台特化对象）
        /// </summary>
        public object Image
        {
            get
            {
                if (platformImage == null && CommonImage != null) platformImage = Agency.ConvertImageToPlatform(CommonImage, false, false);
                return platformImage;
            }
            set
            {
                if (value == null)
                {
                    CommonImage = null;
                    platformImage = null;
                }
                else
                {
                    CommonImage = Agency.ConvertImageToCommon(value);
                    if (CommonImage == null) platformImage = null;
                    else platformImage = Agency.ConvertImageToPlatform(CommonImage, false, false);
                }
            }
        }

        /// <summary>
        /// 视频帧所属通道（0~23）
        /// </summary>
        public int ChannelIndex { get; set; }

        /// <summary>
        /// 特殊摄像头类型
        /// </summary>
        public SpecialCameraType SpecialCameraType { get; set; }

        /// <summary>
        /// 特殊摄像头信息，可以为 ASEva.Samples.DefaultCameraMeta , ASEva.Samples.GenericCameraMeta , ASEva.Samples.LaneLineCameraMeta , ASEva.Samples.BlindSpotCameraMeta , ASEva.Samples.FisheyeCameraMeta
        /// </summary>
        public object SpecialCameraInfo { get; set; }

        /// <summary>
        /// 标准针孔模型下的横向视场角（未消歪曲时为空）
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
            CommonImage = Agency.ConvertImageToCommon(image);
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

        /// <summary>
        /// (api:app=2.3.0) 默认构造函数
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="rawSize">图像的原始大小</param>
        /// <param name="channelIndex">通道</param>
        /// <param name="bas">Session ID</param>
        /// <param name="offset">相对时间戳</param>
        /// <param name="timeline">时间线</param>
        public VideoFrameSample(CommonImage image, IntSize rawSize, int channelIndex, DateTime bas, double offset, double timeline)
            : base(bas, offset, timeline)
        {
            CommonImage = image;
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

        private object platformImage = null;
    }
}
