using System;
using System.IO;
using System.Reflection;
using ASEva.Utility;

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
        public int Width { get { return width; } }

        /// <summary>
        /// 图像高度
        /// </summary>
        public int Height { get { return height; } }

        /// <summary>
        /// 是否含有Alpha通道
        /// </summary>
        public bool WithAlpha { get { return withAlpha; } }

        /// <summary>
        /// 每行数据字节数
        /// </summary>
        public int RowBytes { get { return rowBytes; } }

        /// <summary>
        /// 图像数据，每个像素的存放顺序为BGR或BGRA
        /// </summary>
        public byte[] Data { get { return data; } }

        /// <summary>
        /// 创建通用图像数据
        /// </summary>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <param name="withAlpha">是否带Alpha通道</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage Create(int width, int height, bool withAlpha)
        {
            if (width <= 0 || height <= 0 || width > 65536 || height > 65536) return null;

            var rowBytesValid = width * (withAlpha ? 4 : 3);
            var rowBytes = (((rowBytesValid - 1) >> 2) + 1) << 2;

            var image = new CommonImage();
            image.width = width;
            image.height = height;
            image.withAlpha = withAlpha;
            image.rowBytes = rowBytes;
            image.data = new byte[rowBytes * height];
            return image;
        }

        /// <summary>
        /// [依赖Agency] 从文件读取图像
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage LoadFile(String filePath)
        {
            if (!File.Exists(filePath)) return null;
            
            byte[] data = null;
            FileStream file = null;
            try
            {
                file = File.Open(filePath, FileMode.Open, FileAccess.Read);
                data = new byte[file.Length];
                int length = file.Read(data, 0, (int)file.Length);
                if (length != file.Length) data = null;
            }
            catch (Exception) {}
            if (file != null) file.Close();
            if (data == null) return null;

            return Agency.DecodeImage(data);
        }

        /// <summary>
        /// [依赖Agency] 从资源读取图像
        /// </summary>
        /// <param name="resourceName">资源名称</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage LoadResource(String resourceName)
        {
            var instream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName);
            if (instream == null || instream.Length == 0) return null;

            var data = new byte[instream.Length];
            instream.Read(data, 0, data.Length);
            instream.Close();

            return Agency.DecodeImage(data);
        }

        /// <summary>
        /// [依赖Agency] 保存至文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否成功保存</returns>
        public bool Save(String filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            String format = null;
            if (extension == ".jpeg" || extension == ".jpg") format = "jpg";
            else if (extension == ".png") format = "png";
            else return false;

            var data = Agency.EncodeImage(this, format);
            if (data == null) return false;

            bool ok = false;
            FileStream file = null;
            try
            {
                file = File.Open(filePath, FileMode.Create, FileAccess.Write);
                file.Write(data);
                ok = true;
            }
            catch (Exception) {}
            if (file != null) file.Close();

            return ok;
        }

        private CommonImage()
        {}

        private int width, height;
        private byte[] data;
        private bool withAlpha;
        private int rowBytes;
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
        /// [依赖Agency] 默认构造函数
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
