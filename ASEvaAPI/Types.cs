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
        /// 在线模式，采集数据与在线处理
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
    /// (api:app=2.0.0) 用于发送的总线报文数据，可调用 ASEva.Agency.SetBusMessage 进行报文发送
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
        InternetNotConnected,
        /// <summary>
        /// 超时，定义为超过3秒未收到回复，或超过5秒仍未实际发出请求
        /// </summary>
        Timeout,
        /// <summary>
        /// 成功得到响应
        /// </summary>
        Responsed,
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
        /// 文件ID
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
        /// 秒
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Session
        /// </summary>
        public DateTime Session { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 总线协议文件ID
    /// </summary>
    public class BusProtocolFileID
    {
        public String FileName { get; set; }
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
        None = 0,
        Can = 1,
        CanFD = 2,
        Lin = 3,
        Flexray = 4,
        Ethernet = 5,
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
    /// (api:app=2.0.0) 视频编码格式
    /// </summary>
    public enum VideoInputCodec
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
        /// YUV411：无损编码，帧间独立
        /// </summary>
        YUV411 = 3,

        /// <summary>
        /// YUV420：无损编码，帧间独立
        /// </summary>
        YUV420 = 4,

        /// <summary>
        /// H.265：有损编码，帧间依赖
        /// </summary>
        H265 = 5,
    };

    /// <summary>
    /// (api:app=2.0.0) 视频输入模式
    /// </summary>
    public class VideoInputMode
    {
        public VideoInputCodec Codec { get; set; }
        public IntSize Size { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && obj is VideoInputMode && (obj as VideoInputMode).Codec == Codec && (obj as VideoInputMode).Size.Width == Size.Width && (obj as VideoInputMode).Size.Height == Size.Height;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Size.Width + "x" + Size.Height + " " + Codec.ToString();
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 视频设备信息
    /// </summary>
    public class VideoDeviceInfo
    {
        public String HardwareInfo { get; set; }
        public VideoInputMode[] InputModes { get; set; }
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
        /// <param name="timeline">获取视频帧的目标时间，单位秒</param>
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
        /// CPU使用率的乘数
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
    /// (api:app=2.3.0) C++库类别
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
    /// (api:app=2.3.0) C++组件信息
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
        /// C++组件名称
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 对应的类型ID
        /// </summary>
        public String NativeType { get; set; }

        /// <summary>
        /// 绑定的各C++库版本
        /// </summary>
        public Dictionary<NativeLibraryType, Version> LibraryVersions { get; set; }
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
    }
}
