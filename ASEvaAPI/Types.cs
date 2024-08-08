using System;
using System.Collections.Generic;
using ASEva.Samples;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Size (Integer)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 尺寸大小（整型）
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Size (Floating)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 尺寸大小（浮点型）
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Point (Integer)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 点坐标（整型）
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Point (Floating)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 点坐标（浮点型）
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Rectangle (Integer)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 矩形（整型）
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Rectangle (Floating)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 矩形（浮点型）
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Current status of application, call ASEva.AgencyAsync.GetAppStatus to get
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 应用当前的运行状态，调用 ASEva.AgencyAsync.GetAppStatus 获取
    /// </summary>
    public enum ApplicationStatus
    {
        /// \~English
        /// <summary>
        /// Idle
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 空闲
        /// </summary>
        Idle,

        /// \~English
        /// <summary>
        /// Running session
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 运行session中
        /// </summary>
        Running,

        /// \~English
        /// <summary>
        /// Starting session
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 启动session中
        /// </summary>
        Starting,

        /// \~English
        /// <summary>
        /// Stopping session
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 停止session中
        /// </summary>
        Stopping,

        /// \~English
        /// <summary>
        /// Running standalone task
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 运行独立任务中
        /// </summary>
        Tasking,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Current mode of application, call ASEva.AgencyAsync.GetAppMode to get
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 应用当前的运行模式，调用 ASEva.AgencyAsync.GetAppMode 获取
    /// </summary>
    public enum ApplicationMode
    {
        /// \~English
        /// <summary>
        /// Online acquisition mode
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 在线采集模式
        /// </summary>
        Online,

        /// \~English
        /// <summary>
        /// Offline processing mode
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 离线处理模式
        /// </summary>
        Offline,

        /// \~English
        /// <summary>
        /// Offline replay mode
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 离线回放模式
        /// </summary>
        Replay,

        /// \~English
        /// <summary>
        /// Remove acquisition mode
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 远程采集模式
        /// </summary>
        Remote,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.7) GUI framework that the application based on, call ASEva.AgencyLocal.GetAppGUI to get
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.7) 应用基于的图形界面框架，调用 ASEva.AgencyLocal.GetAppGUI 获取
    /// </summary>
    public enum ApplicationGUI
    {
        /// \~English
        /// <summary>
        /// Not using GUI, the implementation of workflow should be all synchronous (Not asynchronous)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无图形界面，流程的实现应确保是同步的（非异步）
        /// </summary>
        NoGUI,

        /// \~English
        /// <summary>
        /// Windows Forms, Windows only
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Windows Forms，仅限Windows
        /// </summary>
        WindowsForms,

        /// \~English
        /// <summary>
        /// Windows Presentation Foundation, Windows only
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Windows Presentation Foundation，仅限Windows
        /// </summary>
        WPF,

        /// \~English
        /// <summary>
        /// Eto.Forms, for desktop application. Windows, Linux, MacOS supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Eto.Forms，面向桌面应用，支持Windows、Linux、MacOS
        /// </summary>
        Eto,

        /// \~English
        /// <summary>
        /// Avalonia, for desktop application. Windows, Linux, MacOS supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Avalonia，面向桌面应用，支持Windows、Linux、MacOS
        /// </summary>
        Avalonia,

        /// \~English
        /// <summary>
        /// (api:app=3.3.0) Multi-platform App UI, for mobile application. iOS, Android supported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.3.0) Multi-platform App UI，面向移动应用，支持iOS、安卓
        /// </summary>
        MAUI,

        /// \~English
        /// <summary>
        /// (api:app=3.3.0) Blazor WebAssembly, for browser application
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.3.0) Blazor WebAssembly，面向浏览器应用
        /// </summary>
        Blazor,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Log level, as the input argument of ASEva.AgencyLocal.Log
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 清单信息等级，为 ASEva.AgencyLocal.Log 函数的输入参数
    /// </summary>
    public enum LogLevel
    {
        /// \~English
        /// <summary>
        /// Info, generally shown in green
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信息，绿色显示
        /// </summary>
        Info,

        /// \~English
        /// <summary>
        /// Warning, generally shown in orange
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 警告，橙色显示
        /// </summary>
        Warning,

        /// \~English
        /// <summary>
        /// Error, generally shown in red
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 错误，红色显示
        /// </summary>
        Error,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Global buffer range on the timeline, use ASEva.AgencyAsync.GetBufferRange to get
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 当前全局缓存数据在时间线上的范围，调用 ASEva.AgencyAsync.GetBufferRange 获取
    /// </summary>
    public struct BufferRange
    {
        /// \~English
        /// <summary>
        /// Lower bound (earliest), in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 缓存数据时间范围的下沿（最早），单位秒
        /// </summary>
        public double begin;

        /// \~English
        /// <summary>
        /// Upper bound (latest), in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 缓存数据时间范围的下沿（最迟），单位秒
        /// </summary>
        public double end;
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus message data for transmitting, use ASEva.AgencyAsync.SendBusMessage to transmit
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 用于发送的总线报文数据，可调用 ASEva.AgencyAsync.SendBusMessage 进行报文发送
    /// </summary>
    public class BusMessage
    {
        /// \~English
        /// <summary>
        /// Bus channel (1~16), the bus message will be transmitted through the mapped device channel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线设备通道序号（1~16），报文将在对应硬件上发送
        /// </summary>
        public uint Channel { get; set; }

        /// \~English
        /// <summary>
        /// Local ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 报文ID
        /// </summary>
        public uint ID { get; set; }

        /// \~English
        /// <summary>
        /// Message data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 报文数据
        /// </summary>
        public byte[] Data { get; set; }
        
        /// \~English
        /// <summary>
        /// Transmitting period, in milliseconds, at least 10, set to null to transmit once only
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 报文发送周期，单位毫秒（至少为10），若设为null则只发送一次
        /// </summary>
        public uint? MillisecondInterval { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus message configuration
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 报文配置
    /// </summary>
    public class MessageConfig
    {
        /// \~English
        /// <summary>
        /// Bus message ID, the format is "xxx.yyy:zzz", while "xxx.yyy" is the bus protocol name (like vehicle.dbc), and "zzz" is local ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 报文的全局唯一ID，为xxx.yyy:zzz格式。其中xxx.yyy为协议名称，如vehicle.dbc，zzz为报文ID
        /// </summary>
        public String MessageID { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Signal configuration
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 信号配置
    /// </summary>
    public class SignalConfig
    {
        /// \~English
        /// <summary>
        /// Signal ID, the format is "xxx:yyy:zzz", while "xxx" is signal category or bus protocol name, "yyy" is signal type or bus message local ID, and "zzz" is signal name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 主信号的全局唯一ID，为xxx:yyy:zzz格式。其中xxx为协议名称或信号分类，yyy为报文ID或信号子分类，zzz为信号名称
        /// </summary>
        public String SignalID { get; set; }

        /// \~English
        /// <summary>
        /// Sign bit signal ID, for the case that absolute value and sign bit is provided separately
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 符号位信号的全局唯一ID，格式与主信号一致，仅在主信号与符号位信号分别解析的情况使用
        /// </summary>
        public String SignBitSignalID { get; set; }

        /// \~English
        /// <summary>
        /// Multiplier of signal value, generally for unit conversion
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号值的乘数，一般用于单位转换
        /// </summary>
        public double? Scale { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Signal value configuration
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 信号值配置
    /// </summary>
    public class ValueConfig
    {
        /// \~English
        /// <summary>
        /// Signal values
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号值列表
        /// </summary>
        public double[] Values { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Web API POST's content type
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) Web API POST的内容类型
    /// </summary>
    public enum WebPostContentType
    {
        /// \~English
        /// <summary>
        /// Default type, as application/x-www-form-urlencoded
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认类型，对应application/x-www-form-urlencoded
        /// </summary>
        WWWFormUrlEncoded = 0,

        /// \~English
        /// <summary>
        /// As application/octet-stream
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对应application/octet-stream
        /// </summary>
        OctetStream = 1,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Result of Web API request
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) Web API调用的结果
    /// </summary>
    public enum WebApiResult
    {
        /// \~English
        /// <summary>
        /// Unknown, the request should be in this status for a while after sending
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未知，在添加请求后的一段时间内为此状态
        /// </summary>
        Unknown = 0,

        /// \~English
        /// <summary>
        /// Failed because the Internet is not connected
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 互联网未连接导致失败
        /// </summary>
        InternetNotConnected = 1,

        /// \~English
        /// <summary>
        /// Timeout, for 5 seconds not sending the request, or 3 seconds not receiving response
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 超时，定义为超过3秒未收到回复，或超过5秒仍未实际发出请求
        /// </summary>
        Timeout = 2,

        /// \~English
        /// <summary>
        /// The response received
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 成功得到响应
        /// </summary>
        Responded = 3,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Context for Web API calling, as an argument to ASEva.AgencyLocal.CallWebApi
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) Web API调用的上下文，作为 ASEva.AgencyLocal.CallWebApi 参数传入
    /// </summary>
    public class WebApiContext
    {
        /// \~English
        /// <summary>
        /// Result
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 调用结果
        /// </summary>
        public WebApiResult Result { get; set; }

        /// \~English
        /// <summary>
        /// Response string
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 响应的字符串
        /// </summary>
        public String Response { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) General device's status, call ASEva.AgencyAsync.GetDeviceStatus to get
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 设备状态，调用 ASEva.AgencyAsync.GetDeviceStatus 获取
    /// </summary>
    public enum GeneralDeviceStatus
    {
        /// \~English
        /// <summary>
        /// Connection not requested
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未启用连接
        /// </summary>
        None = 0,

        /// \~English
        /// <summary>
        /// Connected
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 连接成功
        /// </summary>
        OK = 1,

        /// \~English
        /// <summary>
        /// Failed to connect
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未连接或连接状态不正常
        /// </summary>
        Error = 2,

        /// \~English
        /// <summary>
        /// Failed to connect part of child devices, or the connection status is not fully normal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 部分设备未连接或连接状态不正常
        /// </summary>
        Warning = 3,
    };

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Multiplex type of bus signal
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线信号的多路复用类型
    /// </summary>
    public enum BusSignalMultiplexType
    {
        /// \~English
        /// <summary>
        /// Normal signal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 一般信号
        /// </summary>
        None = 0,

        /// \~English
        /// <summary>
        /// Multiplexor signal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 多路复用通道值信号
        /// </summary>
        Multiplexor = 1,

        /// \~English
        /// <summary>
        /// Multiplexed signal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 多路复用信号
        /// </summary>
        Multiplexed = 2,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus signal info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线信号信息
    /// </summary>
    public class BusSignalInfo
    {
        /// \~English
        /// <summary>
        /// Signal ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号ID
        /// </summary>
        public String SignalID { get; set; }

        /// \~English
        /// <summary>
        /// Signal name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号名称
        /// </summary>
        public String SignalName { get; set; }

        /// \~English
        /// <summary>
        /// Start bit
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 起始bit
        /// </summary>
        public int StartBit { get; set; }

        /// \~English
        /// <summary>
        /// Bit length
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// bit长度
        /// </summary>
        public int BitLength { get; set; }

        /// \~English
        /// <summary>
        /// Whether it's big endian
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否大字序
        /// </summary>
        public bool BigEndian { get; set; }

        /// \~English
        /// <summary>
        /// Whether it's signed value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否有符号
        /// </summary>
        public bool Signed { get; set; }

        /// \~English
        /// <summary>
        /// Factor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 乘数
        /// </summary>
        public double Factor { get; set; }

        /// \~English
        /// <summary>
        /// Offset
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 加数
        /// </summary>
        public double Offset { get; set; }

        /// \~English
        /// <summary>
        /// Minimum value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最小值
        /// </summary>
        public double Minimum { get; set; }

        /// \~English
        /// <summary>
        /// Maximum value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最大值
        /// </summary>
        public double Maximum { get; set; }

        /// \~English
        /// <summary>
        /// Unit
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 单位
        /// </summary>
        public String Unit { get; set; }

        /// \~English
        /// <summary>
        /// Multiplex type
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号的多路复用类型
        /// </summary>
        public BusSignalMultiplexType MultiplexType { get; set; }

        /// \~English
        /// <summary>
        /// Multiplex channel, only valid if MultiplexType is Multiplexed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号对应的多路复用通道，仅当多路复用类型为Multiplexed时有效
        /// </summary>
        public int MultiplexChannel { get; set; }

        /// \~English
        /// <summary>
        /// Enumeration values
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 枚举值
        /// </summary>
        public Dictionary<long, String> Enums { get; set; }

        /// \~English
        /// <summary>
        /// Information of the bus message that signal belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属报文
        /// </summary>
        public BusMessageInfo OwnerMessage { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus message info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线报文信息
    /// </summary>
    public class BusMessageInfo
    {
        /// \~English
        /// <summary>
        /// Bus message ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 报文ID
        /// </summary>
        public String MessageID { get; set; }

        /// \~English
        /// <summary>
        /// Bus message name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 报文名称
        /// </summary>
        public String MessageName { get; set; }

        /// \~English
        /// <summary>
        /// Local ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道内ID
        /// </summary>
        public uint LocalID { get; set; }

        /// \~English
        /// <summary>
        /// Number of bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 字节数
        /// </summary>
        public int ByteLength { get; set; }

        /// \~English
        /// <summary>
        /// Information of signals
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号信息列表
        /// </summary>
        public BusSignalInfo[] Signals { get; set; }

        /// \~English
        /// <summary>
        /// Information of the bus protocol that bus message belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属总线协议
        /// </summary>
        public BusFileInfo OwnerFile { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus protocol info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线协议信息
    /// </summary>
    public class BusFileInfo
    {
        /// \~English
        /// <summary>
        /// Protocol name (Channel name included for multi-channel protocol file)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 协议名称（多通道的情况下包括通道名）
        /// </summary>
        public String FileID { get; set; }

        /// \~English
        /// <summary>
        /// File path
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 文件路径
        /// </summary>
        public String FilePath { get; set; }

        /// \~English
        /// <summary>
        /// Information of bus messages
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 报文信息列表
        /// </summary>
        public BusMessageInfo[] Messages { get; set; }

        override public String ToString()
        {
            return FileID;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Callback interface for ASEva.AgencyAsync.SelectSignals
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 用于 ASEva.AgencyAsync.SelectSignals 的回调接口
    /// </summary>
    public interface SelectSignalHandler
    {
        /// \~English
        /// <summary>
        /// Called while select a new signal
        /// </summary>
        /// <param name="signalID">Signal ID</param>
        /// <returns>Whether it's available to add more signals</returns>
        /// \~Chinese
        /// <summary>
        /// 添加选中信号时被调用
        /// </summary>
        /// <param name="signalID">选中信号的ID</param>
        /// <returns>返回是否仍可添加信号</returns>
        bool SelectSignal(String signalID);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.6) Callback interface for ASEva.AgencyAsync.SelectBusMessages
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.6) 用于 ASEva.AgencyAsync.SelectBusMessages 的回调接口
    /// </summary>
    public interface SelectBusMessageHandler
    {
        /// \~English
        /// <summary>
        /// Called while select a new bus message
        /// </summary>
        /// <param name="busMessageID">Bus message ID</param>
        /// <returns>Whether it's available to add more bus messages</returns>
        /// \~Chinese
        /// <summary>
        /// 添加选中总线报文时被调用
        /// </summary>
        /// <param name="busMessageID">选中总线报文的ID</param>
        /// <returns>返回是否仍可添加总线报文</returns>
        bool SelectBusMessage(String busMessageID);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.2.0) Time in a session
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.2.0) 在session中的时间
    /// </summary>
    public class TimeWithSession
    {
        /// \~English
        /// <summary>
        /// Time offset, in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 时间偏置，单位秒
        /// </summary>
        public double Time { get; set; }

        /// \~English
        /// <summary>
        /// The session that it belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所在Session
        /// </summary>
        public SessionIdentifier Session { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus protocol file ID
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线协议文件ID
    /// </summary>
    public class BusProtocolFileID
    {
        /// \~English
        /// <summary>
        /// Protocol name (Channel name included for multi-channel protocol file)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 协议名称（多通道的情况下包括通道名）
        /// </summary>
        public String FileName { get; set; }

        /// \~English
        /// <summary>
        /// File's MD5
        /// </summary>
        /// \~Chinese
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus protocol file's status
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线协议文件状态
    /// </summary>
    public enum BusProtocolFileState
    {
        /// \~English
        /// <summary>
        /// OK
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 正常
        /// </summary>
        OK,

        /// \~English
        /// <summary>
        /// Not in the protocol library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未在协议库中找到
        /// </summary>
        NotFoundInLibrary,

        /// \~English
        /// <summary>
        /// Protocol file doesn't exist
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 协议文件不存在
        /// </summary>
        FileNotExist,

        /// \~English
        /// <summary>
        /// Protocol file content doesn't match
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 协议文件内容不匹配
        /// </summary>
        MD5NotCorrect,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus device ID
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线设备ID
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Type of bus device channel
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线设备通道类型
    /// </summary>
    public enum BusChannelType
    {
        /// \~English
        /// <summary>
        /// Invalid type
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无效类型
        /// </summary>
        None = 0,

        /// \~English
        /// <summary>
        /// CAN bus, message data is payload, 1~8 bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// CAN总线，报文数据即payload，1～8字节
        /// </summary>
        Can = 1,

        /// \~English
        /// <summary>
        /// CAN-FD bus, message data is payload, 1~64 bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// CAN-FD总线，报文数据即payload，1～64字节
        /// </summary>
        CanFD = 2,

        /// \~English
        /// <summary>
        /// LIN bus, message data is payload, 1~8 bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// LIN总线，报文数据即payload，1～8字节
        /// </summary>
        Lin = 3,

        /// \~English
        /// <summary>
        /// Flexray bus, message data is composed of flag byte (startup, sync, null from low to high), cycle byte and payload, totally 2~256 bytes, and message ID is Slot ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Flexray总线，报文数据由标志位字节(从低至高为startup,sync,null)、cycle字节和payload构成(共2～256字节)，报文ID即Slot ID
        /// </summary>
        Flexray = 4,

        /// \~English
        /// <summary>
        /// Ethernet bus, the message data is the complete Ethernet frame data including the link layer and other protocols, and the message ID is defined as the last four bytes (little endian) of the source MAC
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 以太网总线，报文数据为包含链路层等等协议的完整以太网帧数据，报文ID定义为源MAC的后四字节(小字序)
        /// </summary>
        Ethernet = 5,

        /// \~English
        /// <summary>
        /// SOME/IP automobile Ethernet bus, the message data is the complete Ethernet frame data including the link layer and other protocols, and the message ID is the "SOME/IP Message ID" (composed of Service ID and Method ID)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// SOME/IP车载以太网总线，报文数据为包含链路层等等协议的完整以太网帧数据，报文ID即Message ID(由Service ID和Method ID组成)
        /// </summary>
        SomeIP = 6,
    };

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Information of bus device
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线设备信息
    /// </summary>
    public class BusDeviceInfo
    {
        public BusChannelType[] SupportedTypes { get; set; }
        public String Description { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Video device ID
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 视频设备ID
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Video stream codec type
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 视频编码格式
    /// </summary>
    public enum VideoDataCodec
    {
        /// \~English
        /// <summary>
        /// Invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无效
        /// </summary>
        Invalid = 0,

        /// \~English
        /// <summary>
        /// MJPEG: Lossy encoding, independent frames
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// MJPEG：有损编码，帧间独立
        /// </summary>
        MJPEG = 1,

        /// \~English
        /// <summary>
        /// H.264: Lossy encoding, inter-frame dependence
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// H.264：有损编码，帧间依赖
        /// </summary>
        H264 = 2,

        /// \~English
        /// <summary>
        /// YUV411: Lossless encoding, independent frames, the format is (U0 Y0 V0 Y1 U4 Y2 V4 Y3 Y4 Y5 Y6 Y7) per 8 pixels, 8 bits per value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// YUV411：无损编码，帧间独立，格式为每8像素(U0 Y0 V0 Y1 U4 Y2 V4 Y3 Y4 Y5 Y6 Y7)，每数值8bit
        /// </summary>
        YUV411 = 3,

        /// \~English
        /// <summary>
        /// YUV420: Lossless encoding, independent frames, the format is (U V Y00 Y01 Y10 Y11) per 2x2 pixels, 8 bits per value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// YUV420：无损编码，帧间独立，格式为每2x2像素(U V Y00 Y01 Y10 Y11)，每数值8bit
        /// </summary>
        YUV420 = 4,

        /// \~English
        /// <summary>
        /// H.265: Lossy encoding, inter-frame dependence
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// H.265：有损编码，帧间依赖
        /// </summary>
        H265 = 5,

        /// \~English
        /// <summary>
        /// YUV422: Lossless encoding, independent frames, the format is (Y0 U Y1 V) per 2 pixels , 8 bits per value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// YUV422：无损编码，帧间独立，格式为每2像素(Y0 U Y1 V)，每数值8bit
        /// </summary>
        YUV422 = 6,

        /// \~English
        /// <summary>
        /// RAW: Lossless encoding, independent frames, the format is BG on 024... rows, GR on 135... rows, 8 bits per value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// RAW：无损编码，帧间独立，格式为024...行BG，135...行GR，每数值8bit
        /// </summary>
        RAW = 7,

        /// \~English
        /// <summary>
        /// RAW12: Lossless encoding, independent frames, the format is BG on 024... rows, GR on 135... rows, 12 bits per value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// RAW12：无损编码，帧间独立，格式为024...行BG，135...行GR，每数值12bit按小字序依次存储
        /// </summary>
        RAW12 = 8,

        /// \~English
        /// <summary>
        /// RAW14: Lossless encoding, independent frames, the format is BG on 024... rows, GR on 135... rows, 14 bits per value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// RAW14：无损编码，帧间独立，格式为024...行BG，135...行GR，每数值14bit按小字序依次存储
        /// </summary>
        RAW14 = 9,

        /// \~English
        /// <summary>
        /// RAW16: Lossless encoding, independent frames, the format is BG on 024... rows, GR on 135... rows, 16 bits per value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// RAW16：无损编码，帧间独立，格式为024...行BG，135...行GR，每数值16bit按大字序依次存储
        /// </summary>
        RAW16 = 10,

        /// \~English
        /// <summary>
        /// Y16: Lossless encoding, independent frames, 16 bits (big endian) per value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Y16：无损编码，帧间独立，每数值16bit按大字序依次存储
        /// </summary>
        Y16 = 11,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Video input mode
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 视频输入模式
    /// </summary>
    public class VideoInputMode
    {
        /// \~English
        /// <summary>
        /// Video input codec
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频输入编码格式
        /// </summary>
        public VideoDataCodec InputCodec { get; set; }

        /// \~English
        /// <summary>
        /// Video size (resolution)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频尺寸 (分辨率)
        /// </summary>
        public IntSize Size { get; set; }

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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Video output mode
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 视频输出模式
    /// </summary>
    public class VideoOutputMode
    {
        /// \~English
        /// <summary>
        /// Video output codec
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频输出编码格式
        /// </summary>
        public VideoDataCodec OutputCodec { get; set; }

        /// \~English
        /// <summary>
        /// Video size (resolution)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频尺寸 (分辨率)
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

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Information of video device
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 视频设备信息
    /// </summary>
    public class VideoDeviceInfo
    {
        /// \~English
        /// <summary>
        /// Device's description
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 设备信息描述
        /// </summary>
        public String HardwareInfo { get; set; }

        /// \~English
        /// <summary>
        /// Supported input modes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 支持的视频输入格式列表
        /// </summary>
        public VideoInputMode[] InputModes { get; set; }

        /// \~English
        /// <summary>
        /// Supported output modes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 支持的视频输出格式列表
        /// </summary>
        public VideoOutputMode[] OutputModes { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Scaling configuration for getting video frame
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 获取视频帧的缩放设置
    /// </summary>
    public class VideoFrameGetScale
    {
        /// \~English
        /// <summary>
        /// Coordinates of the image center in the original image coordinate system
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输出图像中心在原始图像坐标系下的像素坐标
        /// </summary>
        public FloatPoint Center { get; set; }

        /// \~English
        /// <summary>
        /// The scaling ratio relative to the size of the original image, ranges 0.1~4x
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 相对于原始图像大小的缩放比率，0.1~4x
        /// </summary>
        public float Scale { get; set; }

        /// \~English
        /// <summary>
        /// Whether to draw a thumbnail above the image (if it is actually enlarged)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否在图像上方绘制缩略图（若实际进行了放大）
        /// </summary>
        public bool WithPreview { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Related names of file readers and recorders
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 文件读写相关名称的集合
    /// </summary>
    public class FileIONames
    {
        /// \~English
        /// <summary>
        /// Names of readers
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 文件读取器的名称列表
        /// </summary>
        public String[] ReaderNames { get; set; }

        /// \~English
        /// <summary>
        /// Names of remote data readers
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 远程文件读取器的名称列表
        /// </summary>
        public String[] RemoteReaderNames { get; set; }

        /// \~English
        /// <summary>
        /// Names of writers
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 文件写入器的名称列表
        /// </summary>
        public String[] WriterNames { get; set; }

        /// \~English
        /// <summary>
        /// Name of data pickers
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 文件数据截取器的名称列表
        /// </summary>
        public String[] PickerNames { get; set; }

        /// \~English
        /// <summary>
        /// List of data pickers formed by a combination of file readers and writers, with the key as the reader name and the value as the writer name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通过文件读取器和写入器组合的方式形成的数据截取器列表，键为读取器名称，值为写入器名称
        /// </summary>
        public Dictionary<String, String> ComboPickers { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) The data type needed for file writing
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 文件写入需要的数据类型
    /// </summary>
    public enum RecordDataType
    {
        /// \~English
        /// <summary>
        /// Invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无效值
        /// </summary>
        Invalid = 0,

        /// \~English
        /// <summary>
        /// Bus raw data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线原始数据
        /// </summary>
        BusRawData = 1,

        /// \~English
        /// <summary>
        /// Bus message data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线协议报文数据
        /// </summary>
        BusMessages = 2,

        /// \~English
        /// <summary>
        /// Video raw data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频原始数据
        /// </summary>
        VideoRawData = 3,

        /// \~English
        /// <summary>
        /// Video data for processing
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 用于数据处理的视频数据
        /// </summary>
        VideoProcData = 4,

        /// \~English
        /// <summary>
        /// Signal data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号数据
        /// </summary>
        Signals = 5,

        /// \~English
        /// <summary>
        /// Sample data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 样本数据
        /// </summary>
        Samples = 6,

        /// \~English
        /// <summary>
        /// Matrix data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 矩阵数据
        /// </summary>
        Matrices = 7,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Parsed bus signal value and related info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 解析总线报文得到的信号值及相关信息
    /// </summary>
    public struct BusSignalValue
    {
        /// \~English
        /// <summary>
        /// Signal name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号名
        /// </summary>
        public String signalName;

        /// \~English
        /// <summary>
        /// Signal value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号值
        /// </summary>
        public double? value;

        /// \~English
        /// <summary>
        /// Unit
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 单位
        /// </summary>
        public String unit;

        /// \~English
        /// <summary>
        /// Enumeration value (if exists)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对应的枚举值（若存在枚举信息）
        /// </summary>
        public String enumValue;
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) System status 
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 系统状态 
    /// </summary>
    public enum SystemStatus
    {
        /// \~English
        /// <summary>
        /// Actual replay speed (times)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 实际回放速度（倍速）
        /// </summary>
        ActualReplaySpeed = 1,

        /// \~English
        /// <summary>
        /// Target replay speed (times)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 目标回放速度（倍速）
        /// </summary>
        TargetReplaySpeed = 2,

        /// \~English
        /// <summary>
        /// The latest log message
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最新清单消息
        /// </summary>
        CurrentLoggerMessage = 3,

        /// \~English
        /// <summary>
        /// Display lag, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 显示延迟，单位毫秒
        /// </summary>
        DisplayLag = 4,

        /// \~English
        /// <summary>
        /// Queue length for writing continuous data, in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 连续数据或缓存数据写入队列长度，单位秒
        /// </summary>
        ContinuousFileWriteQueue = 5,

        /// \~English
        /// <summary>
        /// Queue length for writing event session data, in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 事件数据写入队列长度，单位秒
        /// </summary>
        EventFileWriteQueue = 6,

        /// \~English
        /// <summary>
        /// Video process queue's capacity
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频处理队列长度限制
        /// </summary>
        VideoProcessQueueCapacity = 7,

        /// \~English
        /// <summary>
        /// Video process queue's length
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频处理队列长度
        /// </summary>
        VideoProcessQueue = 8,

        /// \~English
        /// <summary>
        /// Audio volume (times)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 音量（倍数）
        /// </summary>
        AudioVolume = 9,

        /// \~English
        /// <summary>
        /// CPU usage, in percentages
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// CPU使用率，单位百分比
        /// </summary>
        CPUUsage = 10,

        /// \~English
        /// <summary>
        /// Multiplier of CPU usage, fixed to 1
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// CPU使用率的乘数，固定为1
        /// </summary>
        CPUUsageRatio = 11,

        /// \~English
        /// <summary>
        /// Memory capacity, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 内存总容量，单位字节
        /// </summary>
        MemoryCapacity = 12,

        /// \~English
        /// <summary>
        /// Memory free space, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 内存可用容量，单位字节
        /// </summary>
        MemoryFree = 13,

        /// \~English
        /// <summary>
        /// Warning threshold of memory free space, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 内存可用容量的警告阈值，单位字节
        /// </summary>
        MemoryWarningThreshold = 14,

        /// \~English
        /// <summary>
        /// Lower limit of memory free space, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 内存可用容量的最小阈值，单位字节
        /// </summary>
        MemoryErrorThreshold = 15,

        /// \~English
        /// <summary>
        /// Capacity of the storage that owns current data path, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 当前数据目录所在磁盘的总容量，单位字节
        /// </summary>
        StorageCapacity = 16,

        /// \~English
        /// Free space of the storage that owns current data path, in bytes
        /// \~Chinese
        /// <summary>
        /// 当前数据目录所在磁盘可用容量，单位字节
        /// </summary>
        StorageFree = 17,

        /// \~English
        /// <summary>
        /// Estimated recordable time of the storage that owns current data path, in hours
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 根据当前数据目录所在磁盘可用容量预估的时长，单位小时
        /// </summary>
        StorageFreeHours = 18,

        /// \~English
        /// <summary>
        /// Warning threshold of free space of the storage that owns current data path, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 当前数据目录所在磁盘可用容量的警告阈值，单位字节
        /// </summary>
        StorageWarningThreshold = 19,

        /// \~English
        /// <summary>
        /// Lower limit of free space of the storage that owns current data path, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 当前数据目录所在磁盘可用容量的最小阈值，单位字节
        /// </summary>
        StorageErrorThreshold = 20,

        /// \~English
        /// <summary>
        /// The latest heart beat time of work thread, in format "yyyyMMddHHmmss.fff"
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最近一次基础线程心跳时间，格式为yyyyMMddHHmmss.fff
        /// </summary>
        WorkthreadHeartBeatTime = 21,
        
        /// \~English
        /// <summary>
        /// Current operation of work thread
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 基础线程当前运行位置
        /// </summary>
        WorkthreadCurrentLocation = 22,

        /// \~English
        /// <summary>
        /// Average loop time of work thread, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 基础线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        WorkthreadLoopTime = 23,

        /// \~English
        /// <summary>
        /// The latest heart beat time of process thread, in format "yyyyMMddHHmmss.fff"
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 最近一次处理线程心跳时间，格式为yyyyMMddHHmmss.fff
        /// </summary>
        ProcthreadHeartBeatTime = 24,

        /// \~English
        /// <summary>
        /// Current operation of process thread
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 处理线程当前运行位置
        /// </summary>
        ProcthreadCurrentLocation = 25,

        /// \~English
        /// <summary>
        /// Average loop time of process thread, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 处理线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        ProcthreadLoopTime = 26,

        /// \~English
        /// <summary>
        /// Average loop time of main thread, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 主线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        MainthreadLoopTime = 27,

        /// \~English
        /// <summary>
        /// Bus data flow, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线数据流量，单位字节
        /// </summary>
        BusDataFlow = 28,

        /// \~English
        /// <summary>
        /// Max cost time for bus device to receive a frame, in microseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线设备接收一帧数据的最大耗时，单位微秒
        /// </summary>
        BusDeviceReadTime = 29,

        /// \~English
        /// <summary>
        /// Video data flow, in pixels
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频数据流量，单位像素数
        /// </summary>
        VideoDataFlow = 30,

        /// \~English
        /// <summary>
        /// Max cost time for video device to receive a frame, in microseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频设备接收一帧数据的最大耗时，单位微秒
        /// </summary>
        VideoDeviceReadTime = 31,

        /// \~English
        /// <summary>
        /// Cost time of starting session, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 开始session耗时，单位毫秒
        /// </summary>
        StartSessionTime = 32,

        /// \~English
        /// <summary>
        /// Cost time of stopping session, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 结束session耗时，单位毫秒
        /// </summary>
        StopSessionTime = 33,

        /// \~English
        /// <summary>
        /// Bottleneck of replay speed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 回放速度瓶颈
        /// </summary>
        ReplayNeck = 34,

        /// \~English
        /// <summary>
        /// Average loop time of file reading thread, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 文件读取线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        FileReadThreadLoopTime = 35,

        /// \~English
        /// <summary>
        /// Average loop time of file writing thread, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 文件写入线程循环平均运行时间（最近），单位毫秒
        /// </summary>
        FileWriteThreadLoopTime = 36,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Window component info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 窗口组件信息
    /// </summary>
    public class WindowClassInfo
    {
        /// \~English
        /// <summary>
        /// ID of the plugin pack that component belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// \~English
        /// <summary>
        /// Component's class ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// \~English
        /// <summary>
        /// Transform ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 分化ID
        /// </summary>
        public String TransformID { get; set; }

        /// \~English
        /// <summary>
        /// Configuration string to register transformed window component
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 注册分化窗口组件的配置字符串
        /// </summary>
        public String TransformConfig { get; set; }

        /// \~English
        /// <summary>
        /// Window title
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 窗口标题
        /// </summary>
        public String Title { get; set; }

        /// \~English
        /// <summary>
        /// Window's icon, whose resolution is 16x16
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 窗口图标，分辨率为16x16
        /// </summary>
        public CommonImage Icon { get; set; }

        /// \~English
        /// <summary>
        /// Whether to support multiple instances
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否支持同时打开多个窗口
        /// </summary>
        public bool MultipleSupported { get; set; }

        /// \~English
        /// <summary>
        /// Information of transformed window components
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 分化的窗口组件信息
        /// </summary>
        public Dictionary<String, WindowClassInfo> TransformClasses { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Dialog component info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 对话框组件信息
    /// </summary>
    public class DialogClassInfo
    {
        /// \~English
        /// <summary>
        /// ID of the plugin pack that component belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// \~English
        /// <summary>
        /// Component's class ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// \~English
        /// <summary>
        /// Transform ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 分化ID
        /// </summary>
        public String TransformID { get; set; }

        /// \~English
        /// <summary>
        /// Configuration string to register transformed dialog component
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 注册分化对话框组件的配置字符串
        /// </summary>
        public String TransformConfig { get; set; }

        /// \~English
        /// <summary>
        /// Dialog title
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对话框标题
        /// </summary>
        public String Title { get; set; }

        /// \~English
        /// <summary>
        /// Dialog's icon, whose resolution is 16x16
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对话框图标，分辨率为16x16
        /// </summary>
        public CommonImage Icon { get; set; }

        /// \~English
        /// <summary>
        /// Information of transformed dialog components
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 分化的对话框组件信息
        /// </summary>
        public Dictionary<String, DialogClassInfo> TransformClasses { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Processor component info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 数据处理组件信息
    /// </summary>
    public class ProcessorClassInfo
    {
        /// \~English
        /// <summary>
        /// ID of the plugin pack that component belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// \~English
        /// <summary>
        /// Component's class ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// \~English
        /// <summary>
        /// Processor's title
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 数据处理组件名称
        /// </summary>
        public String Title { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Native plugin's type
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 原生插件类别
    /// </summary>
    public enum NativeLibraryType
    {
        /// \~English
        /// <summary>
        /// General native library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 一般原生库
        /// </summary>
        Native = 1,

        /// \~English
        /// <summary>
        /// Bus device library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线设备库
        /// </summary>
        Bus = 2,

        /// \~English
        /// <summary>
        /// Video device library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频设备库
        /// </summary>
        Video = 3,

        /// \~English
        /// <summary>
        /// Data processing library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 数据处理库
        /// </summary>
        Processor = 4,

        /// \~English
        /// <summary>
        /// General device library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 一般设备库
        /// </summary>
        Device = 5,

        /// \~English
        /// <summary>
        /// File R/W library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 文件读写库
        /// </summary>
        FileIO = 6,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Native component info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 原生组件信息
    /// </summary>
    public class NativeClassInfo
    {
        /// \~English
        /// <summary>
        /// ID of the plugin pack that component belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// \~English
        /// <summary>
        /// Component's class ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// \~English
        /// <summary>
        /// Native component's title
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 原生组件名称
        /// </summary>
        public String Title { get; set; }

        /// \~English
        /// <summary>
        /// Corresponding native plugin's type ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对应的原生插件类型ID
        /// </summary>
        public String NativeType { get; set; }

        /// \~English
        /// <summary>
        /// Corresponding native plugin's version
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 绑定的各原生库版本
        /// </summary>
        public Dictionary<NativeLibraryType, Version> LibraryVersions { get; set; }

        /// \~English
        /// <summary>
        /// (api:app=3.3.0) Corresponding native plugin's debug ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.3.0) 绑定的各原生库的调试编号
        /// </summary>
        public Dictionary<NativeLibraryType, uint> LibraryDebugIDs { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Device component info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 设备组件信息
    /// </summary>
    public class DeviceClassInfo
    {
        /// \~English
        /// <summary>
        /// ID of the plugin pack that component belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// \~English
        /// <summary>
        /// Component's class ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// \~English
        /// <summary>
        /// Device component's title
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 设备组件名称
        /// </summary>
        public String Title { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Task component info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 独立任务组件信息
    /// </summary>
    public class TaskClassInfo
    {
        /// \~English
        /// <summary>
        /// ID of the plugin pack that component belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// \~English
        /// <summary>
        /// Component's class ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// \~English
        /// <summary>
        /// Task component's title
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 独立任务组件名称
        /// </summary>
        public String Title { get; set; }

        /// \~English
        /// <summary>
        /// Default configuration string, null if unsupported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认配置字符串，若不支持则为null
        /// </summary>
        public String DefaultConfig { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Session filter flags
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) Session筛选标志位
    /// </summary>
    public class SessionFilterFlags
    {
        /// \~English
        /// <summary>
        /// Match the search key
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 符合搜索条件
        /// </summary>
        public bool SearchTrue { get; set; }

        /// \~English
        /// <summary>
        /// Checked
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已选中
        /// </summary>
        public bool CheckTrue { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Detailed information of general device's status
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 详细的设备状态信息
    /// </summary>
    public class DeviceStatusDetail
    {
        /// \~English
        /// <summary>
        /// Device status
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 设备状态
        /// </summary>
        public GeneralDeviceStatus Status { get; set; }

        /// \~English
        /// <summary>
        /// Detailed information
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 状态详细描述
        /// </summary>
        public String Description { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Result of creating window panel of configuration panel
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 创建窗口对象或对话框对象的结果
    /// </summary>
    public enum CreatePanelResult
    {
        /// \~English
        /// <summary>
        /// Success
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 创建成功
        /// </summary>
        OK = 0,

        /// \~English
        /// <summary>
        /// Invalid caller
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无效的调用者
        /// </summary>
        InvalidCaller = 1,

        /// \~English
        /// <summary>
        /// Can't find the target class by ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未找到类型
        /// </summary>
        ClassNotFound = 2,

        /// \~English
        /// <summary>
        /// Failed to create the panel object
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 创建失败
        /// </summary>
        CreateFailed = 3,

        /// \~English
        /// <summary>
        /// System is busy
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 系统繁忙
        /// </summary>
        SystemBusy = 4,

        /// \~English
        /// <summary>
        /// The window already exists (for the window classes which don't support multiple windows)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 窗口已存在（不支持多窗口）
        /// </summary>
        AlreadyExist = 5,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Type of a node in signal tree
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 信号树节点类别
    /// </summary>
    public enum SignalTreeNodeType
    {
        /// \~English
        /// <summary>
        /// Layer I: signal category
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// I级：信号大类
        /// </summary>
        Category = 11,

        /// \~English
        /// <summary>
        /// Layer I: bus protocol
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// I级：总线协议
        /// </summary>
        BusProtocol = 12,

        /// \~English
        /// <summary>
        /// Layer II: signal type
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// II级：信号小类
        /// </summary>
        Type = 21,

        /// \~English
        /// <summary>
        /// Layer II: bus message
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// II级：总线报文
        /// </summary>
        BusMessage = 22,

        /// \~English
        /// <summary>
        /// Layer III: general signal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// III级：一般信号
        /// </summary>
        GeneralSignal = 31,

        /// \~English
        /// <summary>
        /// Layer III: system signal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// III级：系统信号
        /// </summary>
        SystemSignal = 32,

        /// \~English
        /// <summary>
        /// Layer III: normal bus signal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// III级：一般总线信号
        /// </summary>
        NormalBusSignal = 33,

        /// \~English
        /// <summary>
        /// Layer III: multiplexed bus signal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// III级：复用的总线信号
        /// </summary>
        MultiplexedBusSignal = 34,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Node in signal tree
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 信号树节点
    /// </summary>
    public class SignalTreeNode
    {
        /// \~English
        /// <summary>
        /// Node type
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 节点类别
        /// </summary>
        public SignalTreeNodeType Type { get; set; }

        /// \~English
        /// <summary>
        /// Node ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 节点ID
        /// </summary>
        public String ID { get; set; }

        /// \~English
        /// <summary>
        /// Node name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 节点名称
        /// </summary>
        public String Name { get; set; }

        /// \~English
        /// <summary>
        /// Child nodes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 子节点
        /// </summary>
        public SignalTreeNode[] Children { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Plugin pack's status
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 插件包状态
    /// </summary>
    public enum PluginPackStatus
    {
        /// \~English
        /// <summary>
        /// Disabled
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已禁用
        /// </summary>
        Disabled,

        /// \~English
        /// <summary>
        /// Enabled
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已启用
        /// </summary>
        Enabled,

        /// \~English
        /// <summary>
        /// Enabled but only activated after restarting application
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 应用程序重启后启用
        /// </summary>
        ToBeEnabled,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.7) Error info of plugin pack
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.7) 插件包错误信息
    /// </summary>
    public enum PluginPackError
    {
        /// \~English
        /// <summary>
        /// Normal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 正常
        /// </summary>
        OK = 0,
        
        /// \~English
        /// <summary>
        /// Disabled
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 禁用中
        /// </summary>
        Disabled = 1,

        /// \~English
        /// <summary>
        /// Failed to load
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 加载失败
        /// </summary>
        LoadFailed = 2,

        /// \~English
        /// <summary>
        /// Not licensed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未许可
        /// </summary>
        Unlicensed = 3,

        /// \~English
        /// <summary>
        /// Not supported for current GUI framework, but non-GUI functions can still be used
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 当前图形界面框架不支持，但非界面功能仍可使用
        /// </summary>
        GUIUnsupported = 4,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Information of plugin pack
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 插件包信息
    /// </summary>
    public class PluginPackInfo
    {
        /// \~English
        /// <summary>
        /// Plugin ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件包ID
        /// </summary>
        public String ID { get; set; }

        /// \~English
        /// <summary>
        /// Name of plugin pack
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件包名称
        /// </summary>
        public String Name { get; set; }

        /// \~English
        /// <summary>
        /// Version of plugin pack
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件包版本
        /// </summary>
        public Version Version { get; set; }

        /// \~English
        /// <summary>
        /// Brief
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件包简介
        /// </summary>
        public String Brief { get; set; }

        /// \~English
        /// <summary>
        /// Status of plugin pack
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件包状态
        /// </summary>
        public PluginPackStatus Status { get; set; }

        /// \~English
        /// <summary>
        /// Error info of the plugin pack
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件包错误信息
        /// </summary>
        public PluginPackError Error { get; set; }

        /// \~English
        /// <summary>
        /// App-layer function's details
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 应用层详情
        /// </summary>
        public String AppLayerDetails { get; set; }

        /// \~English
        /// <summary>
        /// Native-layer function's details
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 原生层详情
        /// </summary>
        public String NativeLayerDetails { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.7) Installation status of the plugin related library
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.7) 插件关联的库是否可安装的状态
    /// </summary>
    public enum InstallPluginLibraryStatus
    {
        /// \~English
        /// <summary>
        /// Installable
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 可安装
        /// </summary>
        OK,

        /// \~English
        /// <summary>
        /// Not supported by current GUI framework
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 当前图形界面框架不支持
        /// </summary>
        GUIUnsupported,

        /// \~English
        /// <summary>
        /// The plugin is too new
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件太新
        /// </summary>
        TooNew,

        /// \~English
        /// <summary>
        /// The plugin is too old
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件太旧
        /// </summary>
        TooOld,

        /// \~English
        /// <summary>
        /// (api:app=3.2.5) Asynchronous call unsupported but required by current application
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.5) 不支持异步调用但当前应用需要
        /// </summary>
        AsyncUnsupported,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.6) Status of installed plugin
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.6) 已安装插件的状态
    /// </summary>
    public enum PluginInstalledStatus
    {
        /// \~English
        /// <summary>
        /// Not installed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未安装
        /// </summary>
        NotInstalled = 0,

        /// \~English
        /// <summary>
        /// Installed and enabled
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已安装且启用中
        /// </summary>
        InstalledAndEnabled = 1,

        /// \~English
        /// <summary>
        /// Installed but disabled
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已安装但被禁用
        /// </summary>
        InstalledButDisabled = 2,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Information of plugin related library
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 插件关联库的信息
    /// </summary>
    public class InstallPluginLibraryInfo
    {
        /// \~English
        /// <summary>
        /// Library ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 库ID
        /// </summary>
        public String LibraryID { get; set; }

        /// \~English
        /// <summary>
        /// Name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }

        /// \~English
        /// <summary>
        /// Whether installable
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否可安装的状态
        /// </summary>
        public InstallPluginLibraryStatus Status { get; set; }

        /// \~English
        /// <summary>
        /// Plugin version
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件版本
        /// </summary>
        public Version Version { get; set; }

        /// \~English
        /// <summary>
        /// Version of the installed one, null if not installed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已安装插件的版本，若未安装则为null
        /// </summary>
        public Version InstalledVersion { get; set; }

        /// \~English
        /// <summary>
        /// (api:app=3.0.6) Status of installed plugin
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.0.6) 已安装插件的状态
        /// </summary>
        public PluginInstalledStatus InstalledStatus { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Information of plugin related driver and environment pack
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 插件关联驱动和环境的信息
    /// </summary>
    public class InstallPluginDriverInfo
    {
        /// \~English
        /// <summary>
        /// Driver ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 驱动ID
        /// </summary>
        public String DriverID { get; set; }

        /// \~English
        /// <summary>
        /// Name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }

        /// \~English
        /// <summary>
        /// Whether the driver installer or environment pack is attached in the plugin pack
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否随付在安装包中
        /// </summary>
        public bool Attached { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Log message
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 清单信息
    /// </summary>
    public class LogMessage
    {
        /// \~English
        /// <summary>
        /// Log level
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 清单信息级别
        /// </summary>
        public LogLevel Type { get; set; }

        /// \~English
        /// <summary>
        /// Message
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 清单信息文本
        /// </summary>
        public String Text { get; set; }

        /// \~English
        /// <summary>
        /// Time of logging
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 清单信息时间
        /// </summary>
        public DateTime Time { get; set; }

        /// \~English
        /// <summary>
        /// Number of times the same message is repeated
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 清单信息重复次数
        /// </summary>
        public int RepeatedCount { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Handler for the calling requests from native plugins
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 处理来自原生插件的函数调用请求
    /// </summary>
    public interface AppFunctionHandler
    {
        /// \~English
        /// <summary>
        /// Function to handle (You should guarantee it only costs milliseconds)
        /// </summary>
        /// <param name="nativeClassID">Corresponding native component's class ID</param>
        /// <param name="funcID">Function ID</param>
        /// <param name="input">Input binary data</param>
        /// <returns>Output binary data</returns>
        /// \~Chinese
        /// <summary>
        /// 处理函数（应确保毫秒级别的运行时间）
        /// </summary>
        /// <param name="nativeClassID">原生插件对应的原生组件ID</param>
        /// <param name="funcID">被调用的函数ID</param>
        /// <param name="input">输入数据</param>
        /// <returns>输出数据</returns>
        byte[] OnCrossCall(String nativeClassID, String funcID, byte[] input);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Result of adding bus protocol
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 添加总线协议文件结果
    /// </summary>
    public enum AddBusProtocolResult
    {
        /// \~English
        /// <summary>
        /// Invalid parameter of unimplemented
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无效参数或未实现
        /// </summary>
        Invalid,

        /// \~English
        /// <summary>
        /// Success
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 成功添加
        /// </summary>
        OK,

        /// \~English
        /// <summary>
        /// Already added
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已添加过
        /// </summary>
        AlreadyAdded,

        /// \~English
        /// <summary>
        /// Failed to calculate MD5
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无法计算文件MD5
        /// </summary>
        CalculateMD5Failed,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Data subscriber object, call ASEva.AgencyLocal.SubscribeData to get
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 数据订阅对象，调用 ASEva.AgencyLocal.SubscribeData 获取
    /// </summary>
    public class DataSubscriber
    {
        /// \~English
        /// <summary>
        /// Dequeue all data from the buffer (Should be called frequently, since it will close automatically if not called for "timeout")
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 从缓存取出所有新数据（需要确保经常调用，超时后将自动关闭订阅）
        /// </summary>
        public virtual byte[][] Dequeue()
        {
            return null;
        }

        /// \~English
        /// <summary>
        /// Close immediately
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 立即关闭订阅
        /// </summary>
        public virtual void Close()
        {}

        /// \~English
        /// <summary>
        /// Whether it's closed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否已关闭订阅
        /// </summary>
        public virtual bool IsClosed()
        {
            return false;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) CPU time model
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) CPU时间模型
    /// </summary>
    public class CPUTimeModel
    {
        /// \~English
        /// <summary>
        /// CPU tick when the session starts
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Session开始时的CPU计数
        /// </summary>
        public ulong StartCPUTick { get; set; }

        /// \~English
        /// <summary>
        /// CPU ticks per second
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 每秒增加的CPU计数
        /// </summary>
        public ulong CPUTicksPerSecond { get; set; }

        /// \~English
        /// <summary>
        /// Constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CPUTimeModel()
        {
            StartCPUTick = 0;
            CPUTicksPerSecond = 1000000000;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Posix time model
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) Posix时间模型
    /// </summary>
    public class PosixTimeModel
    {
        /// \~English
        /// <summary>
        /// Posix time when the session starts, in milliseconds, 0 means invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Session开始时的Posix时间，单位毫秒，0表示无效
        /// </summary>
        public ulong StartPosix { get; set; }

        /// \~English
        /// <summary>
        /// Ratio of posix time to CPU time. It should be about 1.0
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// CPU时间转为Posix时间的时间比例，应为1.0左右
        /// </summary>
        public double TimeRatio { get; set; }

        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PosixTimeModel()
        {
            StartPosix = 0;
            TimeRatio = 1;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) The mode to query video frame
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 获取视频帧的模式
    /// </summary>
    public enum VideoFrameGetMode
    {
        /// \~English
        /// <summary>
        /// Fixed to output image of VGA size (640xN)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 固定输出VGA尺寸图像数据
        /// </summary>
        Preview = 0,

        /// \~English
        /// <summary>
        /// Output image of raw size (Fallback to Preview if unavailable)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输出原始尺寸图像数据(若条件不满足则退化为Preview)
        /// </summary>
        RawFull = 1,

        /// \~English
        /// <summary>
        /// Output image of half raw size (Fallback to Preview if unavailable)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输出按原始尺寸1/2缩小后的图像数据(若条件不满足则退化为Preview)
        /// </summary>
        RawHalf = 2,

        /// \~English
        /// <summary>
        /// Output image of quarter raw size (Fallback to Preview if unavailable)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输出按原始尺寸1/4缩小后的图像数据(若条件不满足则退化为Preview)
        /// </summary>
        RawQuarter = 3,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus raw data's status (mainly for transmitting)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线数据的(发送)状态
    /// </summary>
    public enum BusRawDataState
	{
        /// \~English
        /// <summary>
        /// Received message (Other enumeration values are for transmitting)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 收到的报文，其他状态都为发送报文
        /// </summary>
		Received = 0,

        /// \~English
        /// <summary>
        /// Not running a session
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未运行
        /// </summary>
		NotRunning = 1,

        /// \~English
        /// <summary>
        /// Invalid channel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无效通道
        /// </summary>
		InvalidChannel = 2,

        /// \~English
        /// <summary>
        /// Can't find corresponding plugin for the channel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 插件未找到
        /// </summary>
		PluginNotFound = 3,

        /// \~English
        /// <summary>
        /// Not synchronized
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未同步
        /// </summary>
		NotSync = 4,

        /// \~English
        /// <summary>
        /// Scheduled transmitting unsupported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 不支持预约
        /// </summary>
		ScheduleUnsupported = 5,

        /// \~English
        /// <summary>
        /// Disorder of transmitting time
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 时间乱序
        /// </summary>
		TimeDisorder = 6,

        /// \~English
        /// <summary>
        /// Transmitting OK
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 发送成功
        /// </summary>
		TransmitOK = 7,

        /// \~English
        /// <summary>
        /// Transmitting failed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 发送失败
        /// </summary>
		TransmitFailed = 8,

        /// \~English
        /// <summary>
        /// Scheduled
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 预约成功
        /// </summary>
		ScheduleOK = 9,

        /// \~English
        /// <summary>
        /// Failed to schedule transmitting
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 预约失败
        /// </summary>
		ScheduleFailed = 10,
	};

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Logger for debug info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 记录调试信息接口
    /// </summary>
    public interface Logger
    {
        /// \~English
        /// <summary>
        /// Print for debugging, use ASEva.AgencyLocal.Print when you don't need to specify the source
        /// </summary>
        /// <param name="text">想要打印的文本</param>
        /// \~Chinese
        /// <summary>
        /// 打印信息用于调试，不需要指定来源时可使用 ASEva.AgencyLocal.Print
        /// </summary>
        /// <param name="text">想要打印的文本</param>
        void Print(String text);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Vendor of dedicated graphic card
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 独立显卡厂商
    /// </summary>
    public enum GraphicCardVendor
    {
        /// \~English
        /// <summary>
        /// Unknown
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// \~English
        /// <summary>
        /// nVidia
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 英伟达
        /// </summary>
        NVidia = 1,

        /// \~English
        /// <summary>
        /// AMD
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// AMD
        /// </summary>
        AMD = 2,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Information of dedicated graphics card
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 独立显卡信息
    /// </summary>
    public class GraphicCardInfo
    {
        /// \~English
        /// <summary>
        /// Vendor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 独立显卡厂商
        /// </summary>
        public GraphicCardVendor Vendor { get; set; }

        /// \~English
        /// <summary>
        /// The index under the same vendor, starting from 0 (for multiple graphic card cases)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 该厂商下的显卡序号，从0起算 (针对多显卡场合)
        /// </summary>
        public int CardIndex { get; set; }

        /// \~English
        /// <summary>
        /// Capacity of dedicated memory, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 显存总容量，单位字节
        /// </summary>
        public ulong MemoryCapacity { get; set; }

        /// \~English
        /// <summary>
        /// Free space of dedicated memory, in bytes
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 显存可用容量，单位字节
        /// </summary>
        public ulong MemoryFree { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Console component info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 控制台组件信息
    /// </summary>
    public class ConsoleClassInfo
    {
        /// \~English
        /// <summary>
        /// ID of the plugin pack that component belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属插件ID
        /// </summary>
        public String OwnerPluginID { get; set; }

        /// \~English
        /// <summary>
        /// Component's class ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 组件ID
        /// </summary>
        public String ID { get; set; }

        /// \~English
        /// <summary>
        /// Console component's title
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 控制台组件标题
        /// </summary>
        public String Title { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Bus channel info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 总线通道信息
    /// </summary>
    public class BusChannelInfo
    {
        /// \~English
        /// <summary>
        /// Channel's type
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道类型
        /// </summary>
        public BusChannelType Type { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Video channel info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 视频通道信息
    /// </summary>
    public class VideoChannelInfo
    {
        /// \~English
        /// <summary>
        /// Input mode
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输入模式
        /// </summary>
        public VideoInputMode InputMode { get; set; }

        /// \~English
        /// <summary>
        /// Frame rate in recording file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 采集文件帧率
        /// </summary>
        public int RecordFPS { get; set; }

        /// \~English
        /// <summary>
        /// Whether to align as frame rate in recording file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否按采集文件帧率对齐
        /// </summary>
        public bool RecordFPSAlign { get; set; }

        /// \~English
        /// <summary>
        /// Constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public VideoChannelInfo()
        {
            InputMode = new VideoInputMode();
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Type of component related UI
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 组件相关用户界面的类别
    /// </summary>
    public enum ModuleRelatedUIType
    {
        /// \~English
        /// <summary>
        /// Invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无效
        /// </summary>
        Invalid = 0,

        /// \~English
        /// <summary>
        /// Dialog component
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对话框组件
        /// </summary>
        Dialog = 1,

        /// \~English
        /// <summary>
        /// Window component
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 窗口组件
        /// </summary>
        Window = 2,

        /// \~English
        /// <summary>
        /// Console component
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 控制台组件
        /// </summary>
        Console = 3
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Entry arguments of component related UI
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 组件相关用户界面的入口参数
    /// </summary>
    public class ModuleRelatedUIEntry
    {
        /// \~English
        /// <summary>
        /// UI type
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// UI类别
        /// </summary>
        public ModuleRelatedUIType Type { get; set; }

        /// \~English
        /// <summary>
        /// UI component's class ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// UI组件的类别ID
        /// </summary>
        public String ClassID { get; set; }

        /// \~English
        /// <summary>
        /// UI component's transform ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// UI组件的分化标识ID
        /// </summary>
        public String TransformID { get; set; }

        /// \~English
        /// <summary>
        /// Component's name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 组件名称
        /// </summary>
        public String Name { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Component details
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 组件详情
    /// </summary>
    public class ModuleDetails
    {
        /// \~English
        /// <summary>
        /// Channel ID and alias name of output samples
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输出样本的通道ID和别名
        /// </summary>
        public Dictionary<String, String> OutputSamples { get; set; }

        /// \~English
        /// <summary>
        /// ID of output scenarios
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输出场景的ID
        /// </summary>
        public String[] OutputScenes { get; set; }

        /// \~English
        /// <summary>
        /// Type and name of output signals, use empty string for default type
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输出信号类别和名称，默认类别使用空字符串
        /// </summary>
        public Dictionary<String, String[]> OutputSignals { get; set; }

        /// \~English
        /// <summary>
        /// Title of output graphs
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输出图表的标题
        /// </summary>
        public String[] OutputGraphs { get; set; }

        /// \~English
        /// <summary>
        /// ID of the required signal packing data channels
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所需信号打包数据通道ID
        /// </summary>
        public String[] RequiredSignalPackings { get; set; }

        /// \~English
        /// <summary>
        /// ID of the required video channels
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所需视频通道
        /// </summary>
        public UsingVideoChannel[] RequiredVideoChannels { get; set; }

        /// \~English
        /// <summary>
        /// Data types required for writing data files
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 写入数据文件所需的数据类型
        /// </summary>
        public RecordDataType[] RequiredRecordDataTypes { get; set; }

        /// \~English
        /// <summary>
        /// Entry arguments of related UIs
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 相关用户界面的入口参数
        /// </summary>
        public ModuleRelatedUIEntry[] RelatedUIEntries { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Language
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 语言
    /// </summary>
    public enum Language
    {
        /// \~English
        /// <summary>
        /// Invalid value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无效值
        /// </summary>
        Invalid = 0,

        /// \~English
        /// <summary>
        /// English, language code is "en"
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 英文，语言代号为"en"
        /// </summary>
        English = 1,

        /// \~English
        /// <summary>
        /// Chinese, language code is "zh", default is simplified chinese
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 中文，语言代号为"zh"，默认为简体中文
        /// </summary>
        Chinese = 2,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.4.0) Data stream types of transfer to client side
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.4.0) 往客户端传输的数据流类型
    /// </summary>
    public enum TransferStreamType
    {
        /// \~English
        /// <summary>
        /// Manual trigger data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 触发器数据
        /// </summary>
        ManualTrigger = 1,

        /// \~English
        /// <summary>
        /// Bus message data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总线报文数据
        /// </summary>
        BusMessage = 2,

        /// \~English
        /// <summary>
        /// Video frame data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 视频帧数据
        /// </summary>
        VideoFrame = 3,

        /// \~English
        /// <summary>
        /// Audio frame data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 音频帧数据
        /// </summary>
        AudioFrame = 4,

        /// \~English
        /// <summary>
        /// Signal data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号数据
        /// </summary>
        Signal = 5,

        /// \~English
        /// <summary>
        /// General sample data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通用样本数据
        /// </summary>
        GeneralSample = 6,

        /// \~English
        /// <summary>
        /// Point cloud data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 点云数据
        /// </summary>
        PointCloud = 7,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.4.0) Data stream transfer statistics
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.4.0) 数据流传输统计信息
    /// </summary>
    public class TransferStatistics
    {
        /// \~English
        /// <summary>
        /// Successfully transferred data bytes per second
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 每秒传输成功字节数
        /// </summary>
        public ulong SuccessBytesPerSecond { get; set; }

        /// \~English
        /// <summary>
        /// Transfer failing data bytes per second
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 每秒传输失败字节数
        /// </summary>
        public ulong FailBytesPerSecond { get; set; }
    }
}
