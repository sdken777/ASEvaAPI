using System;

namespace ASEva.Samples
{
    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Special camera's type
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 特殊摄像头类型
    /// </summary>
    public enum SpecialCameraType
    {
        /// \~English
        /// <summary>
        /// Camera with no position info (Pinhole model), use DefaultCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无位置摄像头（标准模型），使用 DefaultCameraMeta
        /// </summary>
        Normal = 0,

        /// \~English
        /// <summary>
        /// Front camera (Pinhole model), use GenericCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 前向摄像头（标准模型），使用 GenericCameraMeta
        /// </summary>
        FrontCamera = 1,

        /// \~English
        /// <summary>
        /// Camera for left lane line (Pinhole model), use LaneLineCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 垂直面向左车道线摄像头（标准模型），使用 LaneLineCameraMeta
        /// </summary>
        LeftLaneCamera = 2,

        /// \~English
        /// <summary>
        /// Camera for right lane line (Pinhole model), use LaneLineCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 垂直面向右车道线摄像头（标准模型），使用 LaneLineCameraMeta
        /// </summary>
        RightLaneCamera = 3,

        /// \~English
        /// <summary>
        /// Camera for left blind spot (Pinhole model), use LaneLineCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 面向左盲区摄像头（标准模型），使用 BlindSpotCameraMeta
        /// </summary>
        LeftBlindSpotCamera = 4,

        /// \~English
        /// <summary>
        /// Camera for right blind spot (Pinhole model), use LaneLineCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 面向右盲区摄像头（标准模型），使用 BlindSpotCameraMeta
        /// </summary>
        RightBlindSpotCamera = 5,

        /// \~English
        /// <summary>
        /// Camera at any position (Pinhole model), use GenericCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 任意位置摄像头（标准模型），使用 GenericCameraMeta
        /// </summary>
        AnyCamera = 6,

        /// \~English
        /// <summary>
        /// (api:app=2.1.2) Camera with no position info (Fisheye model), use FisheyeCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.2) 无位置摄像头（鱼眼模型），使用 FisheyeCameraMeta
        /// </summary>
        NormalFisheyeCamera = 7,

        /// \~English
        /// <summary>
        /// (api:app=2.1.2) Front camera (Fisheye model), use FisheyeCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.2) 前向摄像头（鱼眼模型），使用 FisheyeCameraMeta
        /// </summary>
        FrontFisheyeCamera = 8,

        /// \~English
        /// <summary>
        /// (api:app=2.1.2) Camera at any position (Fisheye model), use FisheyeCameraMeta
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.2) 任意位置摄像头（鱼眼模型），使用 FisheyeCameraMeta
        /// </summary>
        AnyFisheyeCamera = 9,
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 摄像头信息共通部分（标准模型）
    /// </summary>
    public class CommonCameraMeta
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了翻转
        /// </summary>
        public bool Inverted { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了镜像
        /// </summary>
        public bool VFlipped { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了去畸变
        /// </summary>
        public bool Undistorted { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.12.2) 去畸变后的理想针孔模型图像的横向视场角 [deg]
        /// </summary>
        public double UndistortedHFOV { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 像素尺寸横纵比
        /// </summary>
        public double AR { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 归一化视轴u坐标
        /// </summary>
        public double CU { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 归一化视轴v坐标
        /// </summary>
        public double CV { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 畸变参数K1
        /// </summary>
        public double K1 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 畸变参数K2
        /// </summary>
        public double K2 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 畸变参数P1
        /// </summary>
        public double P1 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 畸变参数P2
        /// </summary>
        public double P2 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.2.4) K3
        /// </summary>
        public double K3 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.2.4) K4
        /// </summary>
        public double K4 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.2.4) K5
        /// </summary>
        public double K5 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.2.4) K6
        /// </summary>
        public double K6 { get; set; }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 默认摄像头关联信息（标准模型）
    /// </summary>
    public class DefaultCameraMeta
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 共同部分
        /// </summary>
        public CommonCameraMeta Common { get; set; }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 泛用参数摄像头关联信息（标准模型）
    /// </summary>
    public class GenericCameraMeta
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 共通部分
        /// </summary>
        public CommonCameraMeta Common { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 位置坐标X轴分量 [m]
        /// </summary>
        public double PositionX { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 位置坐标Y轴分量 [m]
        /// </summary>
        public double PositionY { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 位置坐标Z轴分量 [m]
        /// </summary>
        public double PositionZ { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了横摆对正
        /// </summary>
        public bool YawRectified { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 原横摆角 [°]
        /// </summary>
        public double OriginYaw { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 对正后的横摆角 [°]
        /// </summary>
        public double RectifiedYaw { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了俯仰对正
        /// </summary>
        public bool PitchRectified { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 原俯仰角 [°]
        /// </summary>
        public double OriginPitch { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了横滚对正
        /// </summary>
        public bool RollRectified { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 原横滚角 [°]
        /// </summary>
        public double OriginRoll { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 抖动偏置横摆角（仅部分摄像头有效）
        /// </summary>
        public double ShakeYaw { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 抖动偏置俯仰角（仅部分摄像头有效）
        /// </summary>
        public double ShakePitch { get; set; }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 车道线摄像头关联信息（标准模型）
    /// </summary>
    public class LaneLineCameraMeta
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否在右侧
        /// </summary>
        public bool IsRightSide { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 共通部分
        /// </summary>
        public CommonCameraMeta Common { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 单位点长度 [m]
        /// </summary>
        public float MeterScale { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 零点位置
        /// </summary>
        public FloatPoint ZeroPosition { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 单位点位置
        /// </summary>
        public FloatPoint MeterPosition { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 半长占比
        /// </summary>
        public float FiftyRatio { get; set; }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 盲区摄像头关联信息（标准模型）
    /// </summary>
    public class BlindSpotCameraMeta
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否在右侧
        /// </summary>
        public bool IsRightSide { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 共通部分
        /// </summary>
        public CommonCameraMeta Common { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// P00位置
        /// </summary>
        public FloatPoint P00Position { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// P04位置
        /// </summary>
        public FloatPoint P04Position { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// P30位置
        /// </summary>
        public FloatPoint P30Position { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// P34位置
        /// </summary>
        public FloatPoint P34Position { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// P0X半长占比
        /// </summary>
        public float P0HRatio { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// P3X半长占比
        /// </summary>
        public float P3HRatio { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// PX0半长占比
        /// </summary>
        public float PH0Ratio { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// PX4半长占比
        /// </summary>
        public float PH4Ratio { get; set; }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.1.2) 鱼眼摄像头关联信息（鱼眼模型）
    /// </summary>
    public class FisheyeCameraMeta
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了翻转
        /// </summary>
        public bool Inverted { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了镜像
        /// </summary>
        public bool VFlipped { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了去畸变
        /// </summary>
        public bool Undistorted { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.12.2) 去畸变后的理想针孔模型图像的横向视场角 [deg]
        /// </summary>
        public double UndistortedHFOV { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 理想鱼眼模型横向视场角 [deg]
        /// </summary>
        public double HFOVFisheye { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 像素尺寸横纵比
        /// </summary>
        public double AR { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 归一化视轴u坐标
        /// </summary>
        public double CU { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 归一化视轴v坐标
        /// </summary>
        public double CV { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 畸变参数K1
        /// </summary>
        public double K1 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 畸变参数K2
        /// </summary>
        public double K2 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 畸变参数K3
        /// </summary>
        public double K3 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 畸变参数K4
        /// </summary>
        public double K4 { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否含有外参信息
        /// </summary>
        public bool HasExtrinsicsInfo { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 位置坐标X轴分量 [m]
        /// </summary>
        public double PositionX { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 位置坐标Y轴分量 [m]
        /// </summary>
        public double PositionY { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 位置坐标Z轴分量 [m]
        /// </summary>
        public double PositionZ { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了横摆对正
        /// </summary>
        public bool YawRectified { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 原横摆角 [°]
        /// </summary>
        public double OriginYaw { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 对正后的横摆角 [°]
        /// </summary>
        public double RectifiedYaw { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了俯仰对正
        /// </summary>
        public bool PitchRectified { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 原俯仰角 [°]
        /// </summary>
        public double OriginPitch { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否作了横滚对正
        /// </summary>
        public bool RollRectified { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 原横滚角 [°]
        /// </summary>
        public double OriginRoll { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 抖动偏置横摆角（仅部分摄像头有效）
        /// </summary>
        public double ShakeYaw { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 抖动偏置俯仰角（仅部分摄像头有效）
        /// </summary>
        public double ShakePitch { get; set; }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.9.0) 摄像头信息
    /// </summary>
    public class CameraInfo
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头类型
        /// </summary>
        public SpecialCameraType SpecialCameraType { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头信息，可以为 ASEva.Samples.DefaultCameraMeta , ASEva.Samples.GenericCameraMeta , ASEva.Samples.LaneLineCameraMeta , ASEva.Samples.BlindSpotCameraMeta , ASEva.Samples.FisheyeCameraMeta
        /// </summary>
        public object SpecialCameraInfo { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 标准针孔模型下的横向视场角（未去畸变时为空）
        /// </summary>
        public double? HorizontalFov { get; set; }
    }
}
