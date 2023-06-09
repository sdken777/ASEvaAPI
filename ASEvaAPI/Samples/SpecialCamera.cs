using System;

namespace ASEva.Samples
{
    /// <summary>
    /// (api:app=2.0.0) 特殊摄像头类型
    /// </summary>
    public enum SpecialCameraType
    {
        /// <summary>
        /// 无位置摄像头（标准模型），使用 DefaultCameraMeta
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 前向摄像头（标准模型），使用 GenericCameraMeta
        /// </summary>
        FrontCamera = 1,

        /// <summary>
        /// 垂直面向左车道线摄像头（标准模型），使用 LaneLineCameraMeta
        /// </summary>
        LeftLaneCamera = 2,

        /// <summary>
        /// 垂直面向右车道线摄像头（标准模型），使用 LaneLineCameraMeta
        /// </summary>
        RightLaneCamera = 3,

        /// <summary>
        /// 面向左盲区摄像头（标准模型），使用 BlindSpotCameraMeta
        /// </summary>
        LeftBlindSpotCamera = 4,

        /// <summary>
        /// 面向右盲区摄像头（标准模型），使用 BlindSpotCameraMeta
        /// </summary>
        RightBlindSpotCamera = 5,

        /// <summary>
        /// 任意位置摄像头（标准模型），使用 GenericCameraMeta
        /// </summary>
        AnyCamera = 6,

        /// <summary>
        /// (api:app=2.1.2) 无位置摄像头（鱼眼模型），使用 FisheyeCameraMeta
        /// </summary>
        NormalFisheyeCamera = 7,

        /// <summary>
        /// (api:app=2.1.2) 前向摄像头（鱼眼模型），使用 FisheyeCameraMeta
        /// </summary>
        FrontFisheyeCamera = 8,

        /// <summary>
        /// (api:app=2.1.2) 任意位置摄像头（鱼眼模型），使用 FisheyeCameraMeta
        /// </summary>
        AnyFisheyeCamera = 9,
    }

    /// <summary>
    /// (api:app=2.0.0) 摄像头信息共通部分（标准模型）
    /// </summary>
    public class CommonCameraMeta
    {
        /// <summary>
        /// 是否作了翻转
        /// </summary>
        public bool Inverted { get; set; }

        /// <summary>
        /// 是否作了镜像
        /// </summary>
        public bool VFlipped { get; set; }

        /// <summary>
        /// 是否作了去畸变
        /// </summary>
        public bool Undistorted { get; set; }

        /// <summary>
        /// (api:app=2.12.2) 去畸变后的理想针孔模型图像的横向视场角 [deg]
        /// </summary>
        public double UndistortedHFOV { get; set; }

        /// <summary>
        /// 像素尺寸横纵比
        /// </summary>
        public double AR { get; set; }

        /// <summary>
        /// 归一化视轴u坐标
        /// </summary>
        public double CU { get; set; }

        /// <summary>
        /// 归一化视轴v坐标
        /// </summary>
        public double CV { get; set; }

        /// <summary>
        /// 畸变参数K1
        /// </summary>
        public double K1 { get; set; }

        /// <summary>
        /// 畸变参数K2
        /// </summary>
        public double K2 { get; set; }

        /// <summary>
        /// 畸变参数P1
        /// </summary>
        public double P1 { get; set; }

        /// <summary>
        /// 畸变参数P2
        /// </summary>
        public double P2 { get; set; }

        /// <summary>
        /// (api:app=2.2.4) K3
        /// </summary>
        public double K3 { get; set; }

        /// <summary>
        /// (api:app=2.2.4) K4
        /// </summary>
        public double K4 { get; set; }

        /// <summary>
        /// (api:app=2.2.4) K5
        /// </summary>
        public double K5 { get; set; }

        /// <summary>
        /// (api:app=2.2.4) K6
        /// </summary>
        public double K6 { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 默认摄像头关联信息（标准模型）
    /// </summary>
    public class DefaultCameraMeta
    {
        /// <summary>
        /// 共同部分
        /// </summary>
        public CommonCameraMeta Common { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 泛用参数摄像头关联信息（标准模型）
    /// </summary>
    public class GenericCameraMeta
    {
        /// <summary>
        /// 共通部分
        /// </summary>
        public CommonCameraMeta Common { get; set; }

        /// <summary>
        /// 位置坐标X轴分量 [m]
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// 位置坐标Y轴分量 [m]
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// 位置坐标Z轴分量 [m]
        /// </summary>
        public double PositionZ { get; set; }

        /// <summary>
        /// 是否作了横摆对正
        /// </summary>
        public bool YawRectified { get; set; }

        /// <summary>
        /// 原横摆角 [°]
        /// </summary>
        public double OriginYaw { get; set; }

        /// <summary>
        /// 对正后的横摆角 [°]
        /// </summary>
        public double RectifiedYaw { get; set; }

        /// <summary>
        /// 是否作了俯仰对正
        /// </summary>
        public bool PitchRectified { get; set; }

        /// <summary>
        /// 原俯仰角 [°]
        /// </summary>
        public double OriginPitch { get; set; }

        /// <summary>
        /// 是否作了横滚对正
        /// </summary>
        public bool RollRectified { get; set; }

        /// <summary>
        /// 原横滚角 [°]
        /// </summary>
        public double OriginRoll { get; set; }

        /// <summary>
        /// 抖动偏置横摆角（仅部分摄像头有效）
        /// </summary>
        public double ShakeYaw { get; set; }

        /// <summary>
        /// 抖动偏置俯仰角（仅部分摄像头有效）
        /// </summary>
        public double ShakePitch { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 车道线摄像头关联信息（标准模型）
    /// </summary>
    public class LaneLineCameraMeta
    {
        /// <summary>
        /// 是否在右侧
        /// </summary>
        public bool IsRightSide { get; set; }

        /// <summary>
        /// 共通部分
        /// </summary>
        public CommonCameraMeta Common { get; set; }

        /// <summary>
        /// 单位点长度 [m]
        /// </summary>
        public float MeterScale { get; set; }

        /// <summary>
        /// 零点位置
        /// </summary>
        public FloatPoint ZeroPosition { get; set; }

        /// <summary>
        /// 单位点位置
        /// </summary>
        public FloatPoint MeterPosition { get; set; }

        /// <summary>
        /// 半长占比
        /// </summary>
        public float FiftyRatio { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 盲区摄像头关联信息（标准模型）
    /// </summary>
    public class BlindSpotCameraMeta
    {
        /// <summary>
        /// 是否在右侧
        /// </summary>
        public bool IsRightSide { get; set; }

        /// <summary>
        /// 共通部分
        /// </summary>
        public CommonCameraMeta Common { get; set; }

        /// <summary>
        /// P00位置
        /// </summary>
        public FloatPoint P00Position { get; set; }

        /// <summary>
        /// P04位置
        /// </summary>
        public FloatPoint P04Position { get; set; }

        /// <summary>
        /// P30位置
        /// </summary>
        public FloatPoint P30Position { get; set; }

        /// <summary>
        /// P34位置
        /// </summary>
        public FloatPoint P34Position { get; set; }

        /// <summary>
        /// P0X半长占比
        /// </summary>
        public float P0HRatio { get; set; }

        /// <summary>
        /// P3X半长占比
        /// </summary>
        public float P3HRatio { get; set; }

        /// <summary>
        /// PX0半长占比
        /// </summary>
        public float PH0Ratio { get; set; }

        /// <summary>
        /// PX4半长占比
        /// </summary>
        public float PH4Ratio { get; set; }
    }

    /// <summary>
    /// (api:app=2.1.2) 鱼眼摄像头关联信息（鱼眼模型）
    /// </summary>
    public class FisheyeCameraMeta
    {
        /// <summary>
        /// 是否作了翻转
        /// </summary>
        public bool Inverted { get; set; }

        /// <summary>
        /// 是否作了镜像
        /// </summary>
        public bool VFlipped { get; set; }

        /// <summary>
        /// 是否作了去畸变
        /// </summary>
        public bool Undistorted { get; set; }

        /// <summary>
        /// (api:app=2.12.2) 去畸变后的理想针孔模型图像的横向视场角 [deg]
        /// </summary>
        public double UndistortedHFOV { get; set; }

        /// <summary>
        /// 理想鱼眼模型横向视场角 [deg]
        /// </summary>
        public double HFOVFisheye { get; set; }

        /// <summary>
        /// 像素尺寸横纵比
        /// </summary>
        public double AR { get; set; }

        /// <summary>
        /// 归一化视轴u坐标
        /// </summary>
        public double CU { get; set; }

        /// <summary>
        /// 归一化视轴v坐标
        /// </summary>
        public double CV { get; set; }

        /// <summary>
        /// 畸变参数K1
        /// </summary>
        public double K1 { get; set; }

        /// <summary>
        /// 畸变参数K2
        /// </summary>
        public double K2 { get; set; }

        /// <summary>
        /// 畸变参数K3
        /// </summary>
        public double K3 { get; set; }

        /// <summary>
        /// 畸变参数K4
        /// </summary>
        public double K4 { get; set; }

        /// <summary>
        /// 是否含有外参信息
        /// </summary>
        public bool HasExtrinsicsInfo { get; set; }

        /// <summary>
        /// 位置坐标X轴分量 [m]
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// 位置坐标Y轴分量 [m]
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// 位置坐标Z轴分量 [m]
        /// </summary>
        public double PositionZ { get; set; }

        /// <summary>
        /// 是否作了横摆对正
        /// </summary>
        public bool YawRectified { get; set; }

        /// <summary>
        /// 原横摆角 [°]
        /// </summary>
        public double OriginYaw { get; set; }

        /// <summary>
        /// 对正后的横摆角 [°]
        /// </summary>
        public double RectifiedYaw { get; set; }

        /// <summary>
        /// 是否作了俯仰对正
        /// </summary>
        public bool PitchRectified { get; set; }

        /// <summary>
        /// 原俯仰角 [°]
        /// </summary>
        public double OriginPitch { get; set; }

        /// <summary>
        /// 是否作了横滚对正
        /// </summary>
        public bool RollRectified { get; set; }

        /// <summary>
        /// 原横滚角 [°]
        /// </summary>
        public double OriginRoll { get; set; }

        /// <summary>
        /// 抖动偏置横摆角（仅部分摄像头有效）
        /// </summary>
        public double ShakeYaw { get; set; }

        /// <summary>
        /// 抖动偏置俯仰角（仅部分摄像头有效）
        /// </summary>
        public double ShakePitch { get; set; }
    }

    /// <summary>
    /// (api:app=2.9.0) 摄像头信息
    /// </summary>
    public class CameraInfo
    {
        /// <summary>
        /// 特殊摄像头类型
        /// </summary>
        public SpecialCameraType SpecialCameraType { get; set; }

        /// <summary>
        /// 特殊摄像头信息，可以为 ASEva.Samples.DefaultCameraMeta , ASEva.Samples.GenericCameraMeta , ASEva.Samples.LaneLineCameraMeta , ASEva.Samples.BlindSpotCameraMeta , ASEva.Samples.FisheyeCameraMeta
        /// </summary>
        public object SpecialCameraInfo { get; set; }

        /// <summary>
        /// 标准针孔模型下的横向视场角（未去畸变时为空）
        /// </summary>
        public double? HorizontalFov { get; set; }
    }
}
