using System;
using System.IO;
using System.Reflection;
using ASEva.Utility;

namespace ASEva.Samples
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Video frame sample (Old style)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 视频帧样本（旧样式）
    /// </summary>
    public class VideoFrameSample : Sample
    {
        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Common image data (in order of BGR)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 通用图像数据(BGR不逆序)
        /// </summary>
        public CommonImage CommonImage { get; set; }

        /// \~English
        /// <summary>
        /// [Depend on Agency] Get platform image
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [依赖Agency] 获取视频帧的位图图像（平台特化对象）
        /// </summary>
        public object Image
        {
            get
            {
                if (platformImage == null && CommonImage != null) platformImage = Agency.ConvertImageToPlatform(CommonImage, false);
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
                    else platformImage = Agency.ConvertImageToPlatform(CommonImage, false);
                }
            }
        }

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
        /// Special camera type
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头类型
        /// </summary>
        public SpecialCameraType SpecialCameraType { get; set; }

        /// \~English
        /// <summary>
        /// Information of special camera, could be ASEva.Samples.DefaultCameraMeta , ASEva.Samples.GenericCameraMeta , ASEva.Samples.LaneLineCameraMeta , ASEva.Samples.BlindSpotCameraMeta , ASEva.Samples.FisheyeCameraMeta , ASEva.Samples.CustomizedCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头信息，可以为 ASEva.Samples.DefaultCameraMeta , ASEva.Samples.GenericCameraMeta , ASEva.Samples.LaneLineCameraMeta , ASEva.Samples.BlindSpotCameraMeta , ASEva.Samples.FisheyeCameraMeta , ASEva.Samples.CustomizedCameraMeta
        /// </summary>
        public object SpecialCameraInfo { get; set; }

        /// \~English
        /// <summary>
        /// Horizontal FOV of ideal pinhole model (null if not undistorted)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 标准针孔模型下的横向视场角（未去畸变时为空）
        /// </summary>
        public double? HorizontalFov { get; set; }

        /// \~English
        /// <summary>
        /// Whether it's scaled, false means fitting to the size
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否对图像进行了放大，false表示按可贴合的最小尺寸缩放
        /// </summary>
        public bool Scaled { get; set; }

        /// \~English
        /// <summary>
        /// Raw image's resolution
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 原始图像的原分辨率
        /// </summary>
        public IntSize RawResolution { get; set; }

        /// \~English
        /// <summary>
        /// The image center in the raw image's coordinate system
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 在原始图像坐标系下的实际截取框中心
        /// </summary>
        public FloatPoint RawCenter { get; set; }

        /// \~English
        /// <summary>
        /// Scaling ratio to the raw image's size
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 相对与原始图像大小的实际缩放比率
        /// </summary>
        public float RawScale { get; set; }

        /// \~English
        /// <summary>
        /// Medium image's resolution
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 实际图像的原分辨率
        /// </summary>
        public IntSize ActualResolution { get; set; }

        /// \~English
        /// <summary>
        /// The image center in the medium image's coordinate system
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 在实际图像坐标系下的实际截取框中心
        /// </summary>
        public FloatPoint ActualCenter { get; set; }

        /// \~English
        /// <summary>
        /// Scaling ratio to the medium image's size
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 相对与实际图像大小的实际缩放比率
        /// </summary>
        public float ActualScale { get; set; }

        /// \~English
        /// <summary>
        /// [Depend on Agency] Default constructor
        /// </summary>
        /// <param name="image">The platform image</param>
        /// <param name="rawSize">Raw resolution</param>
        /// <param name="channelIndex">Video channel</param>
        /// <param name="session">Session ID</param>
        /// <param name="offset">Time offset, in secodns</param>
        /// <param name="timeline">Timeline point</param>
        /// \~Chinese
        /// <summary>
        /// [依赖Agency] 默认构造函数
        /// </summary>
        /// <param name="image">平台特化图像</param>
        /// <param name="rawSize">图像的原始大小</param>
        /// <param name="channelIndex">通道</param>
        /// <param name="session">Session ID</param>
        /// <param name="offset">时间偏置，单位秒</param>
        /// <param name="timeline">时间线上的目标时间点</param>
        public VideoFrameSample(object image, IntSize rawSize, int channelIndex, DateTime session, double offset, double timeline) : base(session, offset, timeline)
        {
            if (image != null) CommonImage = Agency.ConvertImageToCommon(image);
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

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Default constructor
        /// </summary>
        /// <param name="image">Common image object</param>
        /// <param name="rawSize">Raw resolution</param>
        /// <param name="channelIndex">Video channel</param>
        /// <param name="session">Session ID</param>
        /// <param name="offset">Time offset, in secodns</param>
        /// <param name="timeline">Timeline point</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 默认构造函数
        /// </summary>
        /// <param name="image">通用图像对象</param>
        /// <param name="rawSize">图像的原始大小</param>
        /// <param name="channelIndex">通道</param>
        /// <param name="session">Session ID</param>
        /// <param name="offset">时间偏置，单位秒</param>
        /// <param name="timeline">时间线上的目标时间点</param>
        public VideoFrameSample(CommonImage image, IntSize rawSize, int channelIndex, DateTime session, double offset, double timeline) : base(session, offset, timeline)
        {
            if (image != null && !image.BgrInverted) CommonImage = image;
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
