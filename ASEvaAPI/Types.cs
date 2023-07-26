using System;
using System.Collections.Generic;
using ASEva.Samples;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 尺寸大小（整型）
    /// </summary>
    public struct IntSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public IntSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 尺寸大小（浮点型）
    /// </summary>
    public struct FloatSize
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public FloatSize(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 点坐标（整型）
    /// </summary>
    public struct IntPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 点坐标（浮点型）
    /// </summary>
    public struct FloatPoint
    {
        public float X { get; set; }
        public float Y { get; set; }

        public FloatPoint(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 矩形（整型）
    /// </summary>
    public struct IntRect
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public IntRect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int Left
        {
            get { return X; }
        }

        public int Right
        {
            get { return X + Width; }
        }

        public int Top
        {
            get { return Y; }
        }

        public int Bottom
        {
            get { return Y + Height; }
        }

        public IntPoint Location
        {
            get { return new IntPoint(X, Y); }
        }

        public IntSize Size
        {
            get { return new IntSize(Width, Height); }
        }

        public bool Contains(double x, double y)
        {
            return x >= Left && x < Right && y >= Top && y < Bottom;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 矩形（浮点型）
    /// </summary>
    public struct FloatRect
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public FloatRect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float Left
        {
            get { return X; }
        }

        public float Right
        {
            get { return X + Width; }
        }

        public float Top
        {
            get { return Y; }
        }

        public float Bottom
        {
            get { return Y + Height; }
        }

        public FloatPoint Location
        {
            get { return new FloatPoint(X, Y); }
        }

        public FloatPoint Size
        {
            get { return new FloatPoint(Width, Height); }
        }

        public bool Contains(double x, double y)
        {
            return x >= Left && x < Right && y >= Top && y < Bottom;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) ASEva当前的运行状态，调用 ASEva.Agency.GetAppStatus 获取
    /// </summary>
    public enum ApplicationStatus
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Idle,
        /// <summary>
        /// 运行session中
        /// </summary>
        Running,
        /// <summary>
        /// 启动session中
        /// </summary>
        Starting,
        /// <summary>
        /// 停止session中
        /// </summary>
        Stopping,
        /// <summary>
        /// 运行独立任务中
        /// </summary>
        Tasking,
    }

    /// <summary>
    /// (api:app=2.0.0) ASEva当前的运行模式，调用 ASEva.Agency.GetAppMode 获取
    /// </summary>
    public enum ApplicationMode
    {
        /// <summary>
        /// 在线采集模式
        /// </summary>
        Online,
        /// <summary>
        /// 离线处理模式
        /// </summary>
        Offline,
        /// <summary>
        /// 离线回放模式
        /// </summary>
        Replay,
        /// <summary>
        /// (api:app=2.10.0) 远程采集模式
        /// </summary>
        Remote,
    }

    /// <summary>
    /// (api:app=2.0.0) 清单信息等级，为 ASEva.Agency.Log 函数的输入参数
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 信息，绿色显示
        /// </summary>
        Info,
        /// <summary>
        /// 警告，橙色显示
        /// </summary>
        Warning,
        /// <summary>
        /// 错误，红色显示
        /// </summary>
        Error,
    }

    /// <summary>
    /// (api:app=2.0.0) 当前缓存的数据在时间线上的范围，调用 ASEva.Agency.GetBufferRange 获取
    /// </summary>
    public struct BufferRange
    {
        /// <summary>
        /// 缓存数据时间范围的下沿（最早），单位秒
        /// </summary>
        public double begin;
        /// <summary>
        /// 缓存数据时间范围的下沿（最迟），单位秒
        /// </summary>
        public double end;
    }

    /// <summary>
    /// (api:app=2.0.0) 用于发送的总线报文数据，可调用 ASEva.Agency.SendBusMessage 进行报文发送
    /// </summary>
    public class BusMessage
    {
        /// <summary>
        /// 总线设备通道序号（1~16），报文将在对应硬件上发送
        /// </summary>
        public uint Channel { get; set; }
        /// <summary>
        /// 报文ID
        /// </summary>
        public uint ID { get; set; }
        /// <summary>
        /// 报文数据
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// 报文发送周期，单位毫秒（至少为10），若设为null则只发送一次
        /// </summary>
        public uint? MillisecondInterval { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 报文配置，作为 ASEva.Agency.BindMessageSelector 参数传入
    /// </summary>
    public class MessageConfig
    {
        /// <summary>
        /// 报文的全局唯一ID，为xxx.yyy:zzz格式。其中xxx.yyy为协议文件名，如vehicle.dbc，zzz为报文ID
        /// </summary>
        public String MessageID { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 信号配置，作为 ASEva.Agency.BindSignalSelector 参数传入
    /// </summary>
    public class SignalConfig
    {
        /// <summary>
        /// 主信号的全局唯一ID，为xxx:yyy:zzz格式。其中xxx为协议文件名或信号分类，yyy为报文ID或信号子分类，zzz为信号名称
        /// </summary>
        public String SignalID { get; set; }
        /// <summary>
        /// 符号位信号的全局唯一ID，格式与主信号一致，仅在主信号与符号位信号分别解析的情况使用
        /// </summary>
        public String SignBitSignalID { get; set; }
        /// <summary>
        /// 信号值的乘数，一般用于单位转换
        /// </summary>
        public double? Scale { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 信号值配置，作为 ASEva.Agency.BindValueInput 参数传入
    /// </summary>
    public class ValueConfig
    {
        /// <summary>
        /// 信号值列表
        /// </summary>
        public double[] Values { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.4) Web API POST的内容类型
    /// </summary>
    public enum WebPostContentType
    {
        /// <summary>
        /// 默认类型，对应application/x-www-form-urlencoded
        /// </summary>
        WWWFormUrlEncoded = 0,

        /// <summary>
        /// 对应application/octet-stream
        /// </summary>
        OctetStream = 1,
    }

    /// <summary>
    /// (api:app=2.0.0) Web API调用的结果
    /// </summary>
    public enum WebApiResult
    {
        /// <summary>
        /// 未知，在添加请求后的一段时间内为此状态
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 互联网未连接导致失败
        /// </summary>
        InternetNotConnected = 1,
        /// <summary>
        /// 超时，定义为超过3秒未收到回复，或超过5秒仍未实际发出请求
        /// </summary>
        Timeout = 2,
        /// <summary>
        /// 已弃用，应使用 ASEva.WebApiResult.Responded
        /// </summary>
        Responsed = 3,
        /// <summary>
        /// (api:app=2.3.0) 成功得到响应
        /// </summary>
        Responded = 3,
    }

    /// <summary>
    /// (api:app=2.0.0) Web API调用的上下文，作为 ASEva.Agency.CallWebApi 参数传入
    /// </summary>
    public class WebApiContext
    {
        /// <summary>
        /// 调用结果
        /// </summary>
        public WebApiResult Result { get; set; }
        /// <summary>
        /// 响应的字符串
        /// </summary>
        public String Response { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 设备状态，调用 ASEva.Agency.GetDeviceStatus 获取
    /// </summary>
    public enum GeneralDeviceStatus
    {
        /// <summary>
        /// 未启用连接
        /// </summary>
        None = 0,
        /// <summary>
        /// 连接成功
        /// </summary>
        OK = 1,
        /// <summary>
        /// 未连接或连接状态不正常
        /// </summary>
        Error = 2,
        /// <summary>
        /// 部分设备未连接或连接状态不正常
        /// </summary>
        Warning = 3,
    };

    /// <summary>
    /// (api:app=2.0.0) 总线信号的多路复用类型
    /// </summary>
    public enum BusSignalMultiplexType
    {
        /// <summary>
        /// 一般信号
        /// </summary>
        None = 0,
        /// <summary>
        /// 多路复用通道值信号
        /// </summary>
        Multiplexor = 1,
        /// <summary>
        /// 多路复用信号
        /// </summary>
        Multiplexed = 2,
    }

    /// <summary>
    /// (api:app=2.0.0) 总线信号信息
    /// </summary>
    public class BusSignalInfo
    {
        /// <summary>
        /// 信号ID
        /// </summary>
        public String SignalID { get; set; }

        /// <summary>
        /// 信号名称
        /// </summary>
        public String SignalName { get; set; }

        /// <summary>
        /// 起始bit
        /// </summary>
        public int StartBit { get; set; }

        /// <summary>
        /// bit长度
        /// </summary>
        public int BitLength { get; set; }

        /// <summary>
        /// 是否大字序
        /// </summary>
        public bool BigEndian { get; set; }

        /// <summary>
        /// 是否有符号
        /// </summary>
        public bool Signed { get; set; }

        /// <summary>
        /// 乘数
        /// </summary>
        public double Factor { get; set; }

        /// <summary>
        /// 加数
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public double Minimum { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public double Maximum { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public String Unit { get; set; }

        /// <summary>
        /// 信号的多路复用类型
        /// </summary>
        public BusSignalMultiplexType MultiplexType { get; set; }

        /// <summary>
        /// 信号对应的多路复用通道，仅当多路复用类型为Multiplexed时有效
        /// </summary>
        public int MultiplexChannel { get; set; }

        /// <summary>
        /// 枚举值
        /// </summary>
        public Dictionary<long, String> Enums { get; set; }

        /// <summary>
        /// 所属报文
        /// </summary>
        public BusMessageInfo OwnerMessage { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 总线报文信息
    /// </summary>
    public class BusMessageInfo
    {
        /// <summary>
        /// 报文ID
        /// </summary>
        public String MessageID { get; set; }

        /// <summary>
        /// 报文名称
        /// </summary>
        public String MessageName { get; set; }

        /// <summary>
        /// 通道内ID
        /// </summary>
        public uint LocalID { get; set; }

        /// <summary>
        /// 字节数
        /// </summary>
        public int ByteLength { get; set; }

        /// <summary>
        /// 信号信息列表
        /// </summary>
        public BusSignalInfo[] Signals { get; set; }

        /// <summary>
        /// 所属协议文件
        /// </summary>
        public BusFileInfo OwnerFile { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 总线协议文件信息
    /// </summary>
    public class BusFileInfo
    {
        /// <summary>
        /// 文件ID（多通道的情况下包括通道名）
        /// </summary>
        public String FileID { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public String FilePath { get; set; }

        /// <summary>
        /// 报文信息列表
        /// </summary>
        public BusMessageInfo[] Messages { get; set; }

        override public String ToString()
        {
            return FileID;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 用于 ASEva.Agency.SelectSignals 的回调接口
    /// </summary>
    public interface SelectSignalHandler
    {
        /// <summary>
        /// 添加选中信号时被调用
        /// </summary>
        /// <param name="signalID">选中信号的ID</param>
        /// <returns>返回是否仍可添加信号</returns>
        bool SelectSignal(String signalID);
    }

    /// <summary>
    /// (api:app=2.0.0) 在session中的时间
    /// </summary>
    public class TimeWithSession
    {
        /// <summary>
        /// 时间偏置，单位秒
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// 所在Session
        /// </summary>
        public DateTime Session { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 总线协议ID
    /// </summary>
    public class BusProtocolFileID
    {
        /// <summary>
        /// 文件ID（多通道的情况下包括通道名）
        /// </summary>
        public String FileName { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public String MD5 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is BusProtocolFileID)
            {
                var obj2 = obj as BusProtocolFileID;
                var res = FileName == obj2.FileName && MD5 == obj2.MD5;
                return res;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return (FileName + ":" + MD5).GetHashCode();
        }

        public override string ToString()
        {
            return FileName;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 总线协议文件状态
    /// </summary>
    public enum BusProtocolFileState
    {
        OK,
        NotFoundInLibrary,
        FileNotExist,
        MD5NotCorrect,
    }

    /// <summary>
    /// (api:app=2.0.0) 总线设备ID
    /// </summary>
    public class BusDeviceID
    {
        public String Type { get; set; }
        public long Serial { get; set; }
        public int Index { get; set; }

        public BusDeviceID()
        {
            Type = "";
            Serial = 0;
            Index = 0;
        }

        public override string ToString()
        {
            var text = "";
            text += Type;

            if (Serial != 0) text += " #" + Serial;

            text += "-" + (char)(Index + 'A');

            return text;
        }

        public override bool Equals(object obj)
        {
            if (obj is BusDeviceID)
            {
                var target = obj as BusDeviceID;
                return Type == target.Type && Index == target.Index && Serial == target.Serial;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var text = Type + ":" + Index + ":" + Serial;
            return text.GetHashCode();
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 总线设备通道类型
    /// </summary>
    public enum BusChannelType
    {
        /// <summary>
        /// 无效类型
        /// </summary>
        None = 0,

        /// <summary>
        /// CAN总线，报文数据即payload，1～8字节
        /// </summary>
        Can = 1,

        /// <summary>
        /// CAN-FD总线，报文数据即payload，1～64字节
        /// </summary>
        CanFD = 2,

        /// <summary>
        /// LIN总线，报文数据即payload，1～8字节
        /// </summary>
        Lin = 3,

        /// <summary>
        /// Flexray总线，报文数据由标志位字节(从低至高为startup,sync,null)、cycle字节和payload构成(共2～256字节)，报文ID即Slot ID
        /// </summary>
        Flexray = 4,

        /// <summary>
        /// 以太网总线，报文数据为包含链路层等等协议的完整以太网帧数据，报文ID定义为源MAC的后四字节(小字序)
        /// </summary>
        Ethernet = 5,

        /// <summary>
        /// (api:app=2.11.3) SOME/IP车载以太网总线，报文数据为包含链路层等等协议的完整以太网帧数据，报文ID即Message ID(由Service ID和Method ID组成)
        /// </summary>
        SomeIP = 6,
    };

    /// <summary>
    /// (api:app=2.0.0) 总线设备信息
    /// </summary>
    public class BusDeviceInfo
    {
        public BusChannelType[] SupportedTypes { get; set; }
        public String Description { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 视频设备ID
    /// </summary>
    public class VideoDeviceID
    {
        public String Type { get; set; }
        public String LocalID { get; set; }

        public VideoDeviceID()
        {
            Type = "";
        }

        public override string ToString()
        {
            var text = "";
            text += Type;

            if (LocalID != null && LocalID.Length > 0) text += " " + LocalID;

            return text;
        }

        public override bool Equals(object obj)
        {
            if (obj is VideoDeviceID)
            {
                var target = obj as VideoDeviceID;
                return Type == target.Type && LocalID == target.LocalID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var text = Type + ":" + LocalID;
            return text.GetHashCode();
        }

        public static bool operator ==(VideoDeviceID obj1, VideoDeviceID obj2)
        {
            return Object.Equals(obj1, obj2);
        }

        public static bool operator !=(VideoDeviceID obj1, VideoDeviceID obj2)
        {
            return !Object.Equals(obj1, obj2);
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 已弃用，应使用 ASEva.VideoDataCodec
    /// </summary>
    public enum VideoInputCodec
    {
        Invalid = 0,
        MJPEG = 1,
        H264 = 2,
        YUV411 = 3,
        YUV420 = 4,
        H265 = 5,
    };

    /// <summary>
    /// (api:app=2.7.1) 视频编码格式
    /// </summary>
    public enum VideoDataCodec
    {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// MJPEG：有损编码，帧间独立
        /// </summary>
        MJPEG = 1,

        /// <summary>
        /// H.264：有损编码，帧间依赖
        /// </summary>
        H264 = 2,

        /// <summary>
        /// YUV411：无损编码，帧间独立，格式为每8像素(U0 Y0 V0 Y1 U4 Y2 V4 Y3 Y4 Y5 Y6 Y7)，每数值8bit
        /// </summary>
        YUV411 = 3,

        /// <summary>
        /// YUV420：无损编码，帧间独立，格式为每2x2像素(U V Y00 Y01 Y10 Y11)，每数值8bit
        /// </summary>
        YUV420 = 4,

        /// <summary>
        /// H.265：有损编码，帧间依赖
        /// </summary>
        H265 = 5,

        /// <summary>
        /// YUV422：无损编码，帧间独立，格式为每2像素(Y0 U Y1 V)，每数值8bit
        /// </summary>
        YUV422 = 6,

        /// <summary>
        /// RAW：无损编码，帧间独立，格式为024...行BG，135...行GR，每数值8bit
        /// </summary>
        RAW = 7,

        /// <summary>
        /// (api:app=2.7.5) RAW12：无损编码，帧间独立，格式为024...行BG，135...行GR，每数值12bit按小字序依次存储
        /// </summary>
        RAW12 = 8,

        /// <summary>
        /// (api:app=2.7.5) RAW14：无损编码，帧间独立，格式为024...行BG，135...行GR，每数值14bit按小字序依次存储
        /// </summary>
        RAW14 = 9,

        /// <summary>
        /// (api:app=2.9.1) RAW16：无损编码，帧间独立，格式为024...行BG，135...行GR，每数值16bit按大字序依次存储
        /// </summary>
        RAW16 = 10,

        /// <summary>
        /// (api:app=2.9.1) Y16：无损编码，帧间独立，每数值16bit按大字序依次存储
        /// </summary>
        Y16 = 11,
    }

    /// <summary>
    /// (api:app=2.0.0) 视频输入模式
    /// </summary>
    public class VideoInputMode
    {
        /// <summary>
        /// (api:app=2.7.1) 视频输入编码格式
        /// </summary>
        public VideoDataCodec InputCodec { get; set; }

        /// <summary>
        /// 视频尺寸
        /// </summary>
        public IntSize Size { get; set; }

        /// <summary>
        /// 已弃用，应使用 ASEva.VideoInputMode.InputCodec
        /// </summary>
        public VideoInputCodec Codec
        {
            get { return (VideoInputCodec)InputCodec; }
            set { InputCodec = (VideoDataCodec)value; }
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is VideoInputMode && (obj as VideoInputMode).InputCodec == InputCodec && (obj as VideoInputMode).Size.Width == Size.Width && (obj as VideoInputMode).Size.Height == Size.Height;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Size.Width + "x" + Size.Height + " " + InputCodec.ToString();
        }
    }

    /// <summary>
    /// (api:app=2.7.1) 视频输出模式
    /// </summary>
    public class VideoOutputMode
    {
        /// <summary>
        /// 视频输出编码格式
        /// </summary>
        public VideoDataCodec OutputCodec { get; set; }

        /// <summary>
        /// 视频尺寸
        /// </summary>
        public IntSize Size { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && obj is VideoOutputMode && (obj as VideoOutputMode).OutputCodec == OutputCodec && (obj as VideoOutputMode).Size.Width == Size.Width && (obj as VideoOutputMode).Size.Height == Size.Height;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Size.Width + "x" + Size.Height + " " + OutputCodec.ToString();
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 视频设备信息
    /// </summary>
    public class VideoDeviceInfo
    {
        /// <summary>
        /// 硬件信息描述
        /// </summary>
        public String HardwareInfo { get; set; }

        /// <summary>
        /// 支持的视频输入格式列表
        /// </summary>
        public VideoInputMode[] InputModes { get; set; }

        /// <summary>
        /// (api:app=2.7.1) 支持的视频输出格式列表
        /// </summary>
        public VideoOutputMode[] OutputModes { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 获取视频帧的缩放设置
    /// </summary>
    public class VideoFrameGetScale
    {
        /// <summary>
        /// 输出图像中心在原始图像坐标系下的像素坐标
        /// </summary>
        public FloatPoint Center { get; set; }

        /// <summary>
        /// 相对于原始图像大小的缩放比率，0.1~4x
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// 是否在图像上方绘制缩略图（若实际进行了放大）
        /// </summary>
        public bool WithPreview { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 获取视频帧的接口
    /// </summary>
    public interface VideoFrameGetter
    {
        /// <summary>
        /// 按指定参数获取时间轴上最近的视频帧
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="timeline">获取视频帧的目标时间线，单位秒</param>
        /// <param name="maxGap">容许的最大间隔，单位秒</param>
        /// <param name="targetSize">输出图像的尺寸</param>
        /// <param name="hires">是否优先输出高清图像，否则固定输出不大于VGA分辨率的图像</param>
        /// <param name="scale">缩放设置，若按整幅输出则设为null</param>
        /// <returns>图像帧</returns>
        VideoFrameSample GetVideoFrame(int channel, double timeline, double maxGap, IntSize targetSize, bool hires, VideoFrameGetScale scale);
    }

    /// <summary>
    /// (api:app=2.2.0) 文件读写相关名称的集合
    /// </summary>
    public class FileIONames
    {
        /// <summary>
        /// 文件读取器的名称列表
        /// </summary>
        public String[] ReaderNames { get; set; }

        /// <summary>
        /// (api:app=2.10.0) 远程文件读取器的名称列表
        /// </summary>
        public String[] RemoteReaderNames { get; set; }

        /// <summary>
        /// 文件写入器的名称列表
        /// </summary>
        public String[] WriterNames { get; set; }

        /// <summary>
        /// 文件数据截取器的名称列表
        /// </summary>
        public String[] PickerNames { get; set; }

        /// <summary>
        /// 通过文件读取器和写入器组合的方式形成的数据截取器列表，键为读取器名称，值为写入器名称
        /// </summary>
        public Dictionary<String, String> ComboPickers { get; set; }
    }

    /// <summary>
    /// (api:app=2.2.1) 文件写入需要的数据类型
    /// </summary>
    public enum RecordDataType
    {
        /// <summary>
        /// 无效值
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// 总线原始数据
        /// </summary>
        BusRawData = 1,
        /// <summary>
        /// 总线协议报文数据
        /// </summary>
        BusMessages = 2,
        /// <summary>
        /// 视频原始数据
        /// </summary>
        VideoRawData = 3,
        /// <summary>
        /// 用于数据处理的视频数据
        /// </summary>
        VideoProcData = 4,
        /// <summary>
        /// 信号数据
        /// </summary>
        Signals = 5,
        /// <summary>
        /// 样本数据
        /// </summary>
        Samples = 6,
        /// <summary>
        /// 矩阵数据
        /// </summary>
        Matrices = 7,
    }

    /// <summary>
    /// (api:app=2.2.3) 解析总线报文得到的信号值及相关信息
    /// </summary>
    public struct BusSignalValue
    {
        /// <summary>
        /// 信号名
        /// </summary>
        public String signalName;

        /// <summary>
        /// 信号值
        /// </summary>
        public double? value;

        /// <summary>
        /// 单位
        /// </summary>
        public String unit;

        /// <summary>
        /// 对应的枚举值（若存在枚举信息）
        /// </summary>
        public String enumValue;
    }

    /// <summary>
    /// (api:app=2.3.0) 系统状态 
    /// </summary>
    public enum SystemStatus
    {
        /// <summary>
        /// 实际回放速度（倍速）
        /// </summary>
        ActualReplaySpeed = 1,

        /// <summary>
        /// 目标回放速度（倍速）
        /// </summary>
        TargetReplaySpeed = 2,

        /// <summary>
        /// 最新清单消息
        /// </summary>
        CurrentLoggerMessage = 3,

        /// <summary>
        /// 显示延迟，单位毫秒
        /// </summary>
        DisplayLag = 4,

        /// <summary>
        /// 连续数据或缓存数据写入队列长度，单位秒
        /// </summary>
        ContinuousFileWriteQueue = 5,

        /// <summary>
        /// 事件数据写入队列长度，单位秒
        /// </summary>
        EventFileWriteQueue = 6,

        /// <summary>
        /// 视频处理队列长度限制
        /// </summary>
        VideoProcessQueueCapacity = 7,

        /// <summary>
        /// 视频处理队列长度
        /// </summary>
        VideoProcessQueue = 8,

        /// <summary>
        /// 音量（倍数）
        /// </summary>
        AudioVolume = 9,

        /// <summary>
        /// CPU使用率，单位百分比
        /// </summary>
        CPUUsage = 10,

        /// <summary>
        /// CPU使用率的乘数，固定为1
        /// </summary>
        CPUUsageRatio = 11,

        /// <summary>
        /// 内存总容量，单位字节
        /// </summary>
        MemoryCapacity = 12,

        /// <summary>
        /// 内存可用容量，单位字节
        /// </summary>
        MemoryFree = 13,

        /// <summary>
        /// 内存可用容量的警告阈值，单位字节
        /// </summary>
        MemoryWarningThreshold = 14,

        /// <summary>
        /// 内存可用容量的最小阈值，单位字节
        /// </summary>
        MemoryErrorThreshold = 15,

        /// <summary>
        /// 当前数据目录所在磁盘的总容量，单位字节
        /// </summary>
        StorageCapacity = 16,

        /// <summary>
        /// 当前数据目录所在磁盘可用容量，单位字节
        /// </summary>
        StorageFree = 17,

        /// <summary>
        /// 根据当前数据目录所在磁盘可用容量预估的时长，单位小时
        /// </summary>
        StorageFreeHours = 18,

        /// <summary>
        /// 当前数据目录所在磁盘可用容量的警告阈值，单位字节
        /// </summary>
        StorageWarningThreshold = 19,

        /// <summary>
        /// 当前数据目录所在磁盘可用容量的最小阈值，单位字节
        /// </summary>
        StorageErrorThreshold = 20,

        /// <summary>
        /// 最近一次基础线程心跳时间，格式为yyyyMMddHHmmss.fff
        /// </summary>
        WorkthreadHeartBeatTime = 21,
        
        /// <summary>
        /// 基础线程当前运行位置
        /// </summary>
        WorkthreadCurrentLocation = 22,

        /// <summary>
        /// 基础线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        WorkthreadLoopTime = 23,

        /// <summary>
        /// 最近一次处理线程心跳时间，格式为yyyyMMddHHmmss.fff
        /// </summary>
        ProcthreadHeartBeatTime = 24,

        /// <summary>
        /// 处理线程当前运行位置
        /// </summary>
        ProcthreadCurrentLocation = 25,

        /// <summary>
        /// 处理线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        ProcthreadLoopTime = 26,

        /// <summary>
        /// 主线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        MainthreadLoopTime = 27,

        /// <summary>
        /// 总线数据流量，单位字节
        /// </summary>
        BusDataFlow = 28,

        /// <summary>
        /// 总线设备接收一帧数据的最大耗时，单位微秒
        /// </summary>
        BusDeviceReadTime = 29,

        /// <summary>
        /// 视频数据流量，单位像素数
        /// </summary>
        VideoDataFlow = 30,

        /// <summary>
        /// 视频设备接收一帧数据的最大耗时，单位微秒
        /// </summary>
        VideoDeviceReadTime = 31,

        /// <summary>
        /// 开始session耗时，单位毫秒
        /// </summary>
        StartSessionTime = 32,

        /// <summary>
        /// 结束session耗时，单位毫秒
        /// </summary>
        StopSessionTime = 33,

        /// <summary>
        /// 回放速度瓶颈
        /// </summary>
        ReplayNeck = 34,

        /// <summary>
        /// (api:app=2.6.17) 文件读取线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        FileReadThreadLoopTime = 35,

        /// <summary>
        /// (api:app=2.6.17) 文件写入线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        FileWriteThreadLoopTime = 36,
    }

    /// <summary>
    /// (api:app=2.3.0) 窗口组件信息
    /// </summary>
    public class WindowClassInfo
    {
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// 分化ID
        /// </summary>
        public String TransformID { get; set; }

        /// <summary>
        /// (api:app=2.5.0) 注册分化窗口组件的配置字符串
        /// </summary>
        public String TransformConfig { get; set; }

        /// <summary>
        /// 窗口标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 窗口图标，分辨率为16x16
        /// </summary>
        public CommonImage Icon { get; set; }

        /// <summary>
        /// 是否支持同时打开多个窗口
        /// </summary>
        public bool MultipleSupported { get; set; }

        /// <summary>
        /// 分化的窗口组件信息
        /// </summary>
        public Dictionary<String, WindowClassInfo> TransformClasses { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 对话框组件信息
    /// </summary>
    public class DialogClassInfo
    {
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// 分化ID
        /// </summary>
        public String TransformID { get; set; }

        /// <summary>
        /// (api:app=2.5.0) 注册分化对话框组件的配置字符串
        /// </summary>
        public String TransformConfig { get; set; }

        /// <summary>
        /// 对话框标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 对话框图标，分辨率为16x16
        /// </summary>
        public CommonImage Icon { get; set; }

        /// <summary>
        /// 分化的对话框组件信息
        /// </summary>
        public Dictionary<String, DialogClassInfo> TransformClasses { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 数据处理组件信息
    /// </summary>
    public class ProcessorClassInfo
    {
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// 数据处理组件名称
        /// </summary>
        public String Title { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 原生库类别
    /// </summary>
    public enum NativeLibraryType
    {
        /// <summary>
        /// 一般原生库
        /// </summary>
        Native = 1,

        /// <summary>
        /// 总线设备库
        /// </summary>
        Bus = 2,

        /// <summary>
        /// 视频设备库
        /// </summary>
        Video = 3,

        /// <summary>
        /// 数据处理库
        /// </summary>
        Processor = 4,

        /// <summary>
        /// 一般设备库
        /// </summary>
        Device = 5,

        /// <summary>
        /// 文件读写库
        /// </summary>
        FileIO = 6,
    }

    /// <summary>
    /// (api:app=2.3.0) 原生组件信息
    /// </summary>
    public class NativeClassInfo
    {
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// 原生组件名称
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 对应的类型ID
        /// </summary>
        public String NativeType { get; set; }

        /// <summary>
        /// 绑定的各原生库版本
        /// </summary>
        public Dictionary<NativeLibraryType, Version> LibraryVersions { get; set; }
    }

    /// <summary>
    /// (api:app=2.8.0) 设备组件信息
    /// </summary>
    public class DeviceClassInfo
    {
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// 设备组件名称
        /// </summary>
        public String Title { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 独立任务组件信息
    /// </summary>
    public class TaskClassInfo
    {
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// 独立任务组件名称
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// (api:app=2.5.1) 默认配置字符串，若不支持则为null 
        /// </summary>
        public String DefaultConfig { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) Session筛选标志位
    /// </summary>
    public class SessionFilterFlags
    {
        /// <summary>
        /// 符合搜索条件
        /// </summary>
        public bool SearchTrue { get; set; }

        /// <summary>
        /// 已选中
        /// </summary>
        public bool CheckTrue { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 详细的设备状态信息
    /// </summary>
    public class DeviceStatusDetail
    {
        /// <summary>
        /// 设备状态
        /// </summary>
        public GeneralDeviceStatus Status { get; set; }

        /// <summary>
        /// 状态详细描述
        /// </summary>
        public String Description { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 创建窗口对象或对话框对象的结果
    /// </summary>
    public enum CreatePanelResult
    {
        /// <summary>
        /// 创建成功
        /// </summary>
        OK = 0,

        /// <summary>
        /// 无效的调用者
        /// </summary>
        InvalidCaller = 1,

        /// <summary>
        /// 未找到类型
        /// </summary>
        ClassNotFound = 2,

        /// <summary>
        /// 创建失败
        /// </summary>
        CreateFailed = 3,

        /// <summary>
        /// 系统繁忙
        /// </summary>
        SystemBusy = 4,

        /// <summary>
        /// 窗口已存在（不支持多窗口）
        /// </summary>
        AlreadyExist = 5,
    }

    /// <summary>
    /// (api:app=2.3.0) 信号树节点类别
    /// </summary>
    public enum SignalTreeNodeType
    {
        /// <summary>
        /// I级：信号大类
        /// </summary>
        Category = 11,

        /// <summary>
        /// I级：总线协议
        /// </summary>
        BusProtocol = 12,

        /// <summary>
        /// II级：信号小类
        /// </summary>
        Type = 21,

        /// <summary>
        /// II级：总线报文
        /// </summary>
        BusMessage = 22,

        /// <summary>
        /// III级：一般信号
        /// </summary>
        GeneralSignal = 31,

        /// <summary>
        /// III级：系统信号
        /// </summary>
        SystemSignal = 32,

        /// <summary>
        /// III级：一般总线信号
        /// </summary>
        NormalBusSignal = 33,

        /// <summary>
        /// III级：复用的总线信号
        /// </summary>
        MultiplexedBusSignal = 34,
    }

    /// <summary>
    /// (api:app=2.3.0) 信号树节点
    /// </summary>
    public class SignalTreeNode
    {
        /// <summary>
        /// 节点类别
        /// </summary>
        public SignalTreeNodeType Type { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public SignalTreeNode[] Children { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.4) 插件包状态
    /// </summary>
    public enum PluginPackStatus
    {
        /// <summary>
        /// 已禁用
        /// </summary>
        Disabled,

        /// <summary>
        /// 已启用
        /// </summary>
        Enabled,

        /// <summary>
        /// 应用程序重启后启用
        /// </summary>
        ToBeEnabled,
    }

    /// <summary>
    /// (api:app=2.6.16) 插件包错误信息
    /// </summary>
    public enum PluginPackError
    {
        /// <summary>
        /// 正常
        /// </summary>
        OK = 0,
        
        /// <summary>
        /// 禁用中
        /// </summary>
        Disabled = 1,

        /// <summary>
        /// 加载失败
        /// </summary>
        LoadFailed = 2,

        /// <summary>
        /// 未许可
        /// </summary>
        Unlicensed = 3,

        /// <summary>
        /// 平台不支持（UI）
        /// </summary>
        PlatformUnsupported = 4,
    }

    /// <summary>
    /// (api:app=2.3.0) 插件包信息
    /// </summary>
    public class PluginPackInfo
    {
        /// <summary>
        /// 插件包ID
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// 插件包名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 插件包版本
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// 插件包简介
        /// </summary>
        public String Brief { get; set; }

        /// <summary>
        /// (api:app=2.3.4) 插件包状态
        /// </summary>
        public PluginPackStatus Status { get; set; }

        /// <summary>
        /// (api:app=2.6.16) 插件包错误信息
        /// </summary>
        public PluginPackError Error { get; set; }

        /// <summary>
        /// 应用层详情
        /// </summary>
        public String AppLayerDetails { get; set; }

        /// <summary>
        /// 原生层详情
        /// </summary>
        public String NativeLayerDetails { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 插件关联的库是否可安装的状态
    /// </summary>
    public enum InstallPluginLibraryStatus
    {
        /// <summary>
        /// 可安装
        /// </summary>
        OK,

        /// <summary>
        /// 平台不支持（UI）
        /// </summary>
        PlatformUnsupported,

        /// <summary>
        /// 插件太新
        /// </summary>
        TooNew,

        /// <summary>
        /// 插件太旧
        /// </summary>
        TooOld,
    }

    /// <summary>
    /// (api:app=2.3.0) 插件关联库的信息
    /// </summary>
    public class InstallPluginLibraryInfo
    {
        /// <summary>
        /// 库ID
        /// </summary>
        public String LibraryID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 是否可安装的状态
        /// </summary>
        public InstallPluginLibraryStatus Status { get; set; }

        /// <summary>
        /// 插件版本
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// 已安装插件的版本，若未安装则为null
        /// </summary>
        public Version InstalledVersion { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 插件关联驱动和环境的信息
    /// </summary>
    public class InstallPluginDriverInfo
    {
        /// <summary>
        /// 驱动ID
        /// </summary>
        public String DriverID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 是否随付在安装包中
        /// </summary>
        public bool Attached { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 清单信息
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// 清单信息级别
        /// </summary>
        public LogLevel Type { get; set; }

        /// <summary>
        /// 清单信息文本
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// 清单信息时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 清单信息重复次数
        /// </summary>
        public int RepeatedCount { get; set; }
    }

    /// <summary>
    /// (api:app=2.6.0) 处理来自原生层的函数调用请求
    /// </summary>
    public interface AppFunctionHandler
    {
        /// <summary>
        /// 处理函数（应确保毫秒级别的运行时间）
        /// </summary>
        /// <param name="nativeClassID">原生层模块对应的原生组件ID</param>
        /// <param name="funcID">被调用的函数ID</param>
        /// <param name="input">输入数据</param>
        /// <returns>输出数据</returns>
        byte[] OnCrossCall(String nativeClassID, String funcID, byte[] input);
    }

    /// <summary>
    /// (api:app=2.6.3) 添加总线协议文件结果
    /// </summary>
    public enum AddBusProtocolResult
    {
        /// <summary>
        /// 无效参数或未实现
        /// </summary>
        Invalid,

        /// <summary>
        /// 成功添加
        /// </summary>
        OK,

        /// <summary>
        /// 已添加过
        /// </summary>
        AlreadyAdded,

        /// <summary>
        /// 无法计算文件MD5
        /// </summary>
        CalculateMD5Failed,
    }

    /// <summary>
    /// (api:app=2.6.19) 数据订阅对象，调用 ASEva.Agency.SubscribeData 获取
    /// </summary>
    public class DataSubscriber
    {
        /// <summary>
        /// 从缓存取出所有新数据（需要确保经常调用，超时后将自动关闭订阅）
        /// </summary>
        public virtual byte[][] Dequeue()
        {
            return null;
        }

        /// <summary>
        /// 立即关闭订阅
        /// </summary>
        public virtual void Close()
        {}

        /// <summary>
        /// 是否已关闭订阅
        /// </summary>
        public virtual bool IsClosed()
        {
            return false;
        }
    }

    /// <summary>
    /// (api:app=2.7.0) CPU时间模型
    /// </summary>
    public class CPUTimeModel
    {
        /// <summary>
        /// Session开始时的CPU计数
        /// </summary>
        public ulong StartCPUTick { get; set; }

        /// <summary>
        /// 每秒增加的CPU计数
        /// </summary>
        public ulong CPUTicksPerSecond { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CPUTimeModel()
        {
            StartCPUTick = 0;
            CPUTicksPerSecond = 1000000000;
        }
    }

    /// <summary>
    /// (api:app=2.7.0) Posix时间模型
    /// </summary>
    public class PosixTimeModel
    {
        /// <summary>
        /// Session开始时的Posix时间，单位毫秒，0表示无效
        /// </summary>
        public ulong StartPosix { get; set; }

        /// <summary>
        /// CPU时间转为Posix时间的时间比例，应为1左右
        /// </summary>
        public double TimeRatio { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PosixTimeModel()
        {
            StartPosix = 0;
            TimeRatio = 1;
        }
    }

    /// <summary>
    /// (api:app=2.9.0) 获取视频帧的模式
    /// </summary>
    public enum VideoFrameGetMode
    {
        /// <summary>
        /// 固定输出VGA尺寸图像数据
        /// </summary>
        Preview = 0,

        /// <summary>
        /// 输出原始尺寸图像数据(若条件不满足则退化为Preview)
        /// </summary>
        RawFull = 1,

        /// <summary>
        /// 输出按原始尺寸1/2缩小后的图像数据(若条件不满足则退化为Preview)
        /// </summary>
        RawHalf = 2,

        /// <summary>
        /// 输出按原始尺寸1/4缩小后的图像数据(若条件不满足则退化为Preview)
        /// </summary>
        RawQuarter = 3,
    }

    /// <summary>
    /// (api:app=2.9.0) 获取视频帧的接口（扩展版）
    /// </summary>
    public interface VideoFrameGetterX
    {
        /// <summary>
        /// 获取指定通道在指定时间上的视频帧的原始尺寸
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="timeline">获取视频帧的目标时间线，单位秒</param>
        /// <returns>原始尺寸，若无数据则返回null</returns>
        IntSize? GetVideoRawSize(int channel, double timeline);

        /// <summary>
        /// 获取距离指定时间最近的视频帧数据
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="timeline">获取视频帧的目标时间线，单位秒</param>
        /// <param name="maxGap">容许的最大间隔，单位秒</param>
        /// <param name="mode">视频数据输出模式</param>
        /// <param name="clip">在输出模式基础上进一步裁剪，为原始尺寸坐标系，至少为16x16，null表示完整输出</param>
        /// <param name="withAlpha">是否输出带Alpha通道的图像(固定赋值255)</param>
        /// <param name="timestamp">输出图像的时间戳，获取失败则为null</param>
        /// <param name="cameraInfo">摄像头信息，获取失败则为null</param>
        /// <returns>视频帧数据，图像实际大小由mode和clip决定，获取失败则返回null</returns>
        CommonImage GetVideoFrameImage(int channel, double timeline, double maxGap, VideoFrameGetMode mode, IntRect? clip, bool withAlpha, out Timestamp? timestamp, out CameraInfo cameraInfo);

        /// <summary>
        /// 获取距离指定时间最近的缩略图数据
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="timeline">获取视频帧的目标时间线，单位秒</param>
        /// <param name="maxGap">容许的最大间隔，单位秒</param>
        /// <param name="withAlpha">是否输出带Alpha通道的图像(固定赋值255)</param>
        /// <returns>缩略图数据，图像宽度固定为80，获取失败则返回null</returns>
        CommonImage GetVideoFrameThumbnail(int channel, double timeline, double maxGap, bool withAlpha);
    }

    /// <summary>
    /// (api:app=2.10.2) 总线数据的(发送)状态
    /// </summary>
    public enum BusRawDataState
	{
        /// <summary>
        /// 收到的报文，其他状态都为发送报文
        /// </summary>
		Received = 0,

        /// <summary>
        /// 未运行
        /// </summary>
		NotRunning = 1,

        /// <summary>
        /// 无效通道
        /// </summary>
		InvalidChannel = 2,

        /// <summary>
        /// 插件未找到
        /// </summary>
		PluginNotFound = 3,

        /// <summary>
        /// 未同步
        /// </summary>
		NotSync = 4,

        /// <summary>
        /// 不支持预约
        /// </summary>
		ScheduleUnsupported = 5,

        /// <summary>
        /// 时间乱序
        /// </summary>
		TimeDisorder = 6,

        /// <summary>
        /// 发送成功
        /// </summary>
		TransmitOK = 7,

        /// <summary>
        /// 发送失败
        /// </summary>
		TransmitFailed = 8,

        /// <summary>
        /// 预约成功
        /// </summary>
		ScheduleOK = 9,

        /// <summary>
        /// 预约失败
        /// </summary>
		ScheduleFailed = 10,
	};

    /// <summary>
    /// (api:app=2.11.0) 记录调试信息接口
    /// </summary>
    public interface Logger
    {
        /// <summary>
        /// 打印信息至Debugger，不需要指定来源时可使用 ASEva.Agency.Print
        /// </summary>
        /// <param name="text">想要打印的文本</param>
        void Print(String text);
    }

    /// <summary>
    /// (api:app=2.13.2) 独立显卡厂商
    /// </summary>
    public enum GraphicCardVendor
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 英伟达
        /// </summary>
        NVidia = 1,

        /// <summary>
        /// AMD
        /// </summary>
        AMD = 2,
    }

    /// <summary>
    /// (api:app=2.13.2) 独立显卡信息
    /// </summary>
    public class GraphicCardInfo
    {
        /// <summary>
        /// 独立显卡厂商
        /// </summary>
        public GraphicCardVendor Vendor { get; set; }

        /// <summary>
        /// 该厂商下的显卡序号，从0起算 (针对多显卡场合)
        /// </summary>
        public int CardIndex { get; set; }

        /// <summary>
        /// 显存总容量，单位字节
        /// </summary>
        public ulong MemoryCapacity { get; set; }

        /// <summary>
        /// 显存可用容量，单位字节
        /// </summary>
        public ulong MemoryFree { get; set; }
    }
}
