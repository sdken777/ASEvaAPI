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
        /// 任意位置摄像头
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
        /// 是否作了消歪曲
        /// </summary>
        public bool Undistorted { get; set; }

        public double AR { get; set; }
        public double CU { get; set; }
        public double CV { get; set; }
        public double K1 { get; set; }
        public double K2 { get; set; }
        public double P1 { get; set; }
        public double P2 { get; set; }
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

        public FloatPoint ZeroPosition { get; set; }
        public FloatPoint MeterPosition { get; set; }
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

        public FloatPoint P00Position { get; set; }
        public FloatPoint P04Position { get; set; }
        public FloatPoint P30Position { get; set; }
        public FloatPoint P34Position { get; set; }
        public float P0HRatio { get; set; }
        public float P3HRatio { get; set; }
        public float PH0Ratio { get; set; }
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
        /// 是否作了消歪曲
        /// </summary>
        public bool Undistorted { get; set; }

        public double HFOVFisheye { get; set; }
        public double AR { get; set; }
        public double CU { get; set; }
        public double CV { get; set; }
        public double K1 { get; set; }
        public double K2 { get; set; }
        public double K3 { get; set; }
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
}
