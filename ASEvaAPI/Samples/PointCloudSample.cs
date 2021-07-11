using System;

namespace ASEva.Samples
{
    /// <summary>
    /// (api:app=2.0.0) 点云样本
    /// </summary>
    public class PointCloudSample : Sample
    {
        /// <summary>
        /// 样本通道（0~5）
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 点个数
        /// </summary>
        public int PointCount { get; set; }

        /// <summary>
        /// 点坐标列表，数组尺寸为Nx3，N为个数，3分别为x(前),y(左),z(上)轴坐标，单位为厘米
        /// </summary>
        public short[,] Points { get; set; }

        /// <summary>
        /// 点强度列表，数组长度个数N，数值范围为0(最小)~10000(最大)
        /// </summary>
        public ushort[] Intensities { get; set; }

        /// <summary>
        /// 点颜色列表，数组尺寸为Nx3，N为个数，3分别为R,G,B颜色值
        /// </summary>
        public byte[,] Colors { get; set; }

        /// <summary>
        /// 数组长度N，值为激光线ID，如32线雷达从下往上ID分别为0~31
        /// </summary>
        public ushort[] LaserIDs { get; set; }

        /// <summary>
        /// 数组长度N，值为相对于样本时间戳的时间偏置，单位为10微秒（若一帧数据时间跨度超过655ms则不应有此字段数据）
        /// </summary>
        public ushort[] TimeOffsets { get; set; }

        /// <summary>
        /// 数组长度N，值为横摆角，单位为0.01°，数值范围为-18000~+18000，0点为车辆坐标系x轴，逆时针为正
        /// </summary>
        public short[] YawAngles { get; set; }

        /// <summary>
        /// 数组长度N，值为镜面ID
        /// </summary>
        public byte[] MirrorIDs { get; set; }
    }
}
