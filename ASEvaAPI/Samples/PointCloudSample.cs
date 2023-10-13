using System;
using System.Collections.Generic;

namespace ASEva.Samples
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 点云样本
    /// </summary>
    public class PointCloudSample : Sample
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 样本通道（0~11）
        /// </summary>
        public int Channel { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 点个数
        /// </summary>
        public int PointCount { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.11.4) 数据是否已被释放（出于内存用量控制等目的）
        /// </summary>
        public bool DataReleased { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 点坐标列表，数组尺寸为Nx3，N为个数，3分别为x(前),y(左),z(上)轴坐标，单位为厘米
        /// </summary>
        public short[,] Points { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 点强度列表，数组长度个数N，数值范围为0(最小)~10000(最大)
        /// </summary>
        public ushort[] Intensities { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 点颜色列表，数组尺寸为Nx3，N为个数，3分别为R,G,B颜色值
        /// </summary>
        public byte[,] Colors { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 数组长度N，值为激光线ID，如32线雷达从下往上ID分别为0~31
        /// </summary>
        public ushort[] LaserIDs { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 数组长度N，值为相对于样本时间的时间偏置，单位为10微秒（若一帧数据时间跨度超过655ms则不应有此字段数据）
        /// </summary>
        public ushort[] TimeOffsets { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 数组长度N，值为横摆角，单位为0.01°，数值范围为-18000~+18000，0点为车辆坐标系x轴，逆时针为正
        /// </summary>
        public short[] YawAngles { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 数组长度N，值为镜面ID
        /// </summary>
        public byte[] MirrorIDs { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.0.9) byte字段数组的表，键为字段ID（具体请参考点云数据格式文档），值为长度为N的数组，存放该字段ID对应byte数值
        /// </summary>
        public Dictionary<int, byte[]> ByteValues { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.0.9) short字段数组的表，键为字段ID（具体请参考点云数据格式文档），值为长度为N的数组，存放该字段ID对应short数值
        /// </summary>
        public Dictionary<int, short[]> ShortValues { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.0.9) float字段数组的表，键为字段ID（具体请参考点云数据格式文档），值为长度为N的数组，存放该字段ID对应float数值
        /// </summary>
        public Dictionary<int, float[]> FloatValues { get; set; }

        public PointCloudSample()
        {
            ByteValues = new Dictionary<int, byte[]>();
            ShortValues = new Dictionary<int, short[]>();
            FloatValues = new Dictionary<int, float[]>();
        }
    }
}
