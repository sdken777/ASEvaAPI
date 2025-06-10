using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASEva
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Component configuration's status
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 组件配置的状态
    /// </summary>
    public enum ConfigStatus
    {
        /// \~English
        /// <summary>
        /// Enabled and normal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 启用且正常
        /// </summary>
        Enabled,
        
        /// \~English
        /// <summary>
        /// Enabled but with errors
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 启用但存在错误
        /// </summary>
        EnabledWithError,

        /// \~English
        /// <summary>
        /// Enabled but with warnings
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 启用但部分存在错误
        /// </summary>
        EnabledWithWarning,

        /// \~English
        /// <summary>
        /// Disabled
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Title of general sample's fields
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 通用样本的标题部分
    /// </summary>
    public class GeneralSampleTitle
    {
        /// \~English
        /// <summary>
        /// Title of fields
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 标题文字列表
        /// </summary>
        public List<String> Titles { get; set; }

        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GeneralSampleTitle()
        {
            Titles = new List<string>();
        }

        /// \~English
        /// <summary>
        /// Constructor with input of comma-separated title string
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输入逗号分隔的标题字符串的构造函数
        /// </summary>
        public GeneralSampleTitle(String titleString)
        {
            Titles = new List<string>();
            if (!String.IsNullOrWhiteSpace(titleString)) Titles.AddRange(titleString.Split(','));
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Title of scenario's properties
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 场景属性标题
    /// </summary>
    public class SceneTitle
    {
        /// \~English
        /// <summary>
        /// Titles of properties
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 标题文字列表
        /// </summary>
        public List<String> Titles { get; set; }

        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SceneTitle()
        {
            Titles = new List<string>();
        }

        /// \~English
        /// <summary>
        /// Constructor with input of comma-separated title string
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输入逗号分隔的标题字符串的构造函数
        /// </summary>
        public SceneTitle(String titleString)
        {
            Titles = new List<string>();
            if (!String.IsNullOrWhiteSpace(titleString)) Titles.AddRange(titleString.Split(','));
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Sample alias
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 样本通道别名
    /// </summary>
    public class ChannelAlias
    {
        /// \~English
        /// <summary>
        /// Sample channel ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 样本通道ID
        /// </summary>
        public String ChannelID { get; set; }

        /// \~English
        /// <summary>
        /// Alias
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 别名
        /// </summary>
        public String AliasName { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Using video channel in processing
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 数据处理使用的视频通道
    /// </summary>
    public enum UsingVideoChannel
    {
        /// \~English
        /// <summary>
        /// Channel A
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道A
        /// </summary>
        ChannelA = 0,

        /// \~English
        /// <summary>
        /// Channel B
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道B
        /// </summary>
        ChannelB = 1,

        /// \~English
        /// <summary>
        /// Channel C
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道C
        /// </summary>
        ChannelC = 2,

        /// \~English
        /// <summary>
        /// Channel D
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道D
        /// </summary>
        ChannelD = 3,

        /// \~English
        /// <summary>
        /// Channel E
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道E
        /// </summary>
        ChannelE = 4,

        /// \~English
        /// <summary>
        /// Channel F
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道F
        /// </summary>
        ChannelF = 5,

        /// \~English
        /// <summary>
        /// Channel G
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道G
        /// </summary>
        ChannelG = 6,

        /// \~English
        /// <summary>
        /// Channel H
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道H
        /// </summary>
        ChannelH = 7,

        /// \~English
        /// <summary>
        /// Channel I
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道I
        /// </summary>
        ChannelI = 8,

        /// \~English
        /// <summary>
        /// Channel J
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道J
        /// </summary>
        ChannelJ = 9,

        /// \~English
        /// <summary>
        /// Channel K
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道K
        /// </summary>
        ChannelK = 10,

        /// \~English
        /// <summary>
        /// Channel L
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道L
        /// </summary>
        ChannelL = 11,

        /// \~English
        /// <summary>
        /// Channel M
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道M
        /// </summary>
        ChannelM = 12,

        /// \~English
        /// <summary>
        /// Channel N
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道N
        /// </summary>
        ChannelN = 13,

        /// \~English
        /// <summary>
        /// Channel O
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道O
        /// </summary>
        ChannelO = 14,

        /// \~English
        /// <summary>
        /// Channel P
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道P
        /// </summary>
        ChannelP = 15,

        /// \~English
        /// <summary>
        /// Channel Q
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道Q
        /// </summary>
        ChannelQ = 16,

        /// \~English
        /// <summary>
        /// Channel R
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道R
        /// </summary>
        ChannelR = 17,

        /// \~English
        /// <summary>
        /// Channel S
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道S
        /// </summary>
        ChannelS = 18,

        /// \~English
        /// <summary>
        /// Channel T
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道T
        /// </summary>
        ChannelT = 19,

        /// \~English
        /// <summary>
        /// Channel U
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道U
        /// </summary>
        ChannelU = 20,

        /// \~English
        /// <summary>
        /// Channel V
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道V
        /// </summary>
        ChannelV = 21,

        /// \~English
        /// <summary>
        /// Channel W
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道W
        /// </summary>
        ChannelW = 22,

        /// \~English
        /// <summary>
        /// Channel X
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通道X
        /// </summary>
        ChannelX = 23,

        /// \~English
        /// <summary>
        /// Special camera: Front (Pinhole model)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头：前向（标准模型）
        /// </summary>
        SpecialFront = 100,

        /// \~English
        /// <summary>
        /// Special camera: For left lane line
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头：左侧车道线
        /// </summary>
        SpecialLeftLine = 101,

        /// \~English
        /// <summary>
        /// Special camera: For right lane line
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头：右侧车道线
        /// </summary>
        SpecialRightLine = 102,

        /// \~English
        /// <summary>
        /// Special camera: For left blind spot
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头：左侧盲区
        /// </summary>
        SpecialLeftBS = 103,

        /// \~English
        /// <summary>
        /// Special camera: For right blind spot
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头：右侧盲区
        /// </summary>
        SpecialRightBS = 104,

        /// \~English
        /// <summary>
        /// Special camera: Front (Fisheye model)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 特殊摄像头：前向（鱼眼模型）
        /// </summary>
        SpecialFisheyeFront = 105,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.2.12) Interpolation mode for a signal in packing
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.2.12) 信号打包中单个信号的插值模式
    /// </summary>
    public enum SignalPackMode
    {
        /// \~English
        /// <summary>
        /// Normal interpolation
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 正常插值
        /// </summary>
        Default = 0,

        /// \~English
        /// <summary>
        /// Use value of the nearest frame
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 使用前后帧中时间较近的值
        /// </summary>
        Nearest = 1,

        /// \~English
        /// <summary>
        /// Use the buffered value (Unnecessary to wait for later frame)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 使用缓存下来的最后数值（无需等待后一帧）
        /// </summary>
        Latest = 2,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Configuration for a signal in packing
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 信号打包中单个信号的配置
    /// </summary>
    public class SignalPackConfigElem
    {
        /// \~English
        /// <summary>
        /// Value signal ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 数值信号ID
        /// </summary>
        public String ValueID { get; set; }

        /// \~English
        /// <summary>
        /// Sign bit signal ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 符号位信号ID
        /// </summary>
        public String SignID { get; set; }

        /// \~English
        /// <summary>
        /// Multiplier
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 乘数
        /// </summary>
        public double Scale { get; set; }

        /// \~English
        /// <summary>
        /// Output null if no data for a while, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 超过一段时间无数据则输出空，单位毫秒
        /// </summary>
        public double Timeout { get; set; }

        /// \~English
        /// <summary>
        /// (api:app=3.2.12) Interpolation mode for one signal, only available while IsInterpolationMode is true
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.12) 单个信号的插值模式，仅当打包配置中IsInterpolationMode为true时有效
        /// </summary>
        public SignalPackMode Mode { get; set; }

        /// \~English
        /// <summary>
        /// Deprecated. Please use Mode
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应使用Mode
        /// </summary>
        public bool IsNearestMode
        {
            get { return Mode == SignalPackMode.Nearest; }
            set { Mode = value ? SignalPackMode.Nearest : SignalPackMode.Default; }
        }

        public SignalPackConfigElem()
        {
            Scale = 1;
            Timeout = 1200;
            IsNearestMode = false;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Signal packing configuration
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 信号打包配置
    /// </summary>
    public class SignalPackConfig
    {
        /// \~English
        /// <summary>
        /// The output sample protocol for signal packing
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 信号打包输出的样本协议名
        /// </summary>
        public String Protocol { get; set; }

        /// \~English
        /// <summary>
        /// Whether in interpolation mode, otherwise bus message packing mode
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为插值模式，否则为总线报文打包模式
        /// </summary>
        public bool IsInterpolationMode { get; set; }

        /// \~English
        /// <summary>
        /// Final message, only available for bus message packing mode
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 帧尾报文，进在总线报文打包模式下有效
        /// </summary>
        public String FinalMessageID { get; set; }

        /// \~English
        /// <summary>
        /// Sampling rate, only available for interpolation mode
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 采样率，进在插值模式下有效
        /// </summary>
        public int SamplingRate { get; set; }

        /// \~English
        /// <summary>
        /// Configuration for each signal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 每个信号的配置
        /// </summary>
        public List<SignalPackConfigElem> SignalConfigs { get; set; }

        public SignalPackConfig()
        {
            IsInterpolationMode = false;
            SamplingRate = 100;
            SignalConfigs = new List<SignalPackConfigElem>();
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Base class of component configuration
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 组件配置的基类
    /// </summary>
    public class ModuleConfig
    {
        /// \~English
        /// <summary>
        /// [Required] Called while getting configuration string of the component
        /// </summary>
        /// <returns>Configuration string, recommended to use XML or JSON</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取配置的字符串描述时被调用
        /// </summary>
        /// <returns>字符串描述，建议使用XML或JSON</returns>
        public virtual String GetConfig() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while updating configuration string of the component
        /// </summary>
        /// <param name="config">Configuration string, the format should be the same as the one queried from ASEva.ModuleConfig.GetConfig </param>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 通过字符串描述更新配置时被调用
        /// </summary>
        /// <param name="config">字符串描述，需与 ASEva.ModuleConfig.GetConfig 获取的字符串格式一致</param>
        public virtual void SetConfig(String config) { }

        /// \~English
        /// <summary>
        /// (api:app=3.1.4) [Required] Called while getting configuration's status
        /// </summary>
        /// <returns>1) Configuration's status. 2) Error hint, should be available while the status is EnabledWithError or EnabledWithWarning</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.4) [必须实现] 查询配置状态时被调用
        /// </summary>
        /// <returns>1. 配置状态; 2. 错误提示，当配置状态为EnabledWithError或EnabledWithWarning时应有效</returns>
        public virtual Task<(ConfigStatus, String)> GetConfigStatus() { return Task.FromResult((ConfigStatus.Disabled, (String)null)); }

        /// \~English
        /// <summary>
        /// (api:app=3.1.4) [Optional] Called while getting configuration's child status
        /// </summary>
        /// <returns>Configuration's child status</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.4) [可选实现] 查询各子功能配置状态时被调用
        /// </summary>
        /// <returns>各子功能的配置状态</returns>
        public virtual Task<ConfigStatus[]> GetChildConfigStatus() { return Task.FromResult<ConfigStatus[]>(null); }

        /// \~English
        /// <summary>
        /// [Optional. Only for ProcessorClass and NativeClass] Called while getting titles of output general samples
        /// </summary>
        /// <returns>Dictionary. The key is sample channel ID</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现；仅限ProcessorClass和NativeClass] 获取输出的样本标题集合时被调用
        /// </summary>
        /// <returns>样本标题集合，key为样本ID，value为标题描述</returns>
        public virtual Dictionary<String, GeneralSampleTitle> GetProcessorOutputSampleTitles() { return null; }

        /// \~English
        /// <summary>
        /// [Optional. Only for ProcessorClass] Called while getting titles of output scenarios
        /// </summary>
        /// <returns>Dictionary. The key is scenario ID</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现；仅限ProcessorClass] 获取输出的场景标题集合时被调用
        /// </summary>
        /// <returns>场景标题集合，key为场景ID，value为标题描述</returns>
        public virtual Dictionary<String, SceneTitle> GetProcessorOutputSceneTitles() { return null; }

        /// \~English
        /// <summary>
        /// [Optional. Only for ProcessorClass and NativeClass] Called while getting names of output signals
        /// </summary>
        /// <returns>Names of output signals</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现；仅限ProcessorClass和NativeClass] 获得该组件默认类别下的所有输出信号的名称时被调用
        /// </summary>
        /// <returns>信号名称列表</returns>
        public virtual String[] GetProcessorOutputSignalNames() { return null; }

        /// \~English
        /// <summary>
        /// [Optional. Only for ProcessorClass] Called while getting names of output signals under multiple types other than the default type
        /// </summary>
        /// <returns>Dictionary. The key is signal type name</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现；仅限ProcessorClass] 获得该组件非默认类别下的所有输出信号的名称时被调用
        /// </summary>
        /// <returns>信号名称列表，key为类别名称</returns>
        public virtual Dictionary<String, String[]> GetProcessorOutputSignalNameTable() { return null; }

        /// \~English
        /// <summary>
        /// [Optional. Only for ProcessorClass] Called while getting definitions of output graphs
        /// </summary>
        /// <returns>Definitions of graphs</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现；仅限ProcessorClass] 获得该组件所有输出图表的定义时被调用
        /// </summary>
        /// <returns>图表定义列表</returns>
        public virtual GraphDefinition[] GetProcessorOutputGraphDefinitions() { return null; }

        /// \~English
        /// <summary>
        /// [Optional. Only for ProcessorClass and NativeClass] Called while getting signal packing configurations
        /// </summary>
        /// <returns>Signal packing configurations</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现；仅限ProcessorClass和NativeClass] 获取数据处理需要用到的所有信号打包配置时被调用
        /// </summary>
        /// <returns>信号打包配置列表</returns>
        public virtual List<SignalPackConfig> GetProcessorRelatedSignalPackings() { return null; }

        /// \~English
        /// <summary>
        /// [Optional. Only for ProcessorClass and NativeClass] Called while getting alias names of output samples
        /// </summary>
        /// <returns>Alias names of output samples</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现；仅限ProcessorClass和NativeClass] 获取所有输出样本通道的别名列表时被调用
        /// </summary>
        /// <returns>通道别名列表</returns>
        public virtual List<ChannelAlias> GetChannelAliasList() { return null; }

        /// \~English
        /// <summary>
        /// [Optional. Only for ProcessorClass and NativeClass] Called while getting used video channels
        /// </summary>
        /// <returns>Used video channels</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现；仅限ProcessorClass和NativeClass] 获取所有使用的视频通道时被调用
        /// </summary>
        /// <returns>所有使用的视频通道</returns>
        public virtual List<UsingVideoChannel> GetUsingVideoChannels() { return null; }

        /// \~English
        /// <summary>
        /// [Optional. Only for NativeClass] Called while getting required data types for file recording
        /// </summary>
        /// <returns>Required data types for file recording</returns>
        /// \~Chinese
        /// <summary>
        /// [Optional；仅限NativeClass] 获取文件写入需要的所有数据类型
        /// </summary>
        /// <returns>文件写入需要的所有数据类型</returns>
        public virtual List<RecordDataType> GetRecordDataTypes() { return null; }

        /// \~English
        /// <summary>
        /// (api:app=3.8.0) [Optional. Only for NativeClass and DeviceClass] Called while getting configs of auto converting raw data to general sample (for raw data output by this component)
        /// </summary>
        /// <returns>Configs of auto converting raw data to general sample, the key is raw data protocol, the value is config</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.8.0) [可选实现；仅限NativeClass和DeviceClass] 获取原始数据自动转通用样本的配置（针对本组件输出的原始数据）
        /// </summary>
        /// <returns>原始数据自动转通用样本的配置，键为原始数据协议，值为配置</returns>
        public virtual Dictionary<String, RawToSampleConfig> GetRawToSampleConfigs() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Disable all component functions by user
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 用户一键禁用功能时被调用
        /// </summary>
        public virtual void DisableAll() { }

        /// \~English
        /// <summary>
        /// (api:app=3.1.4) [Optional] Disable error parts of the component functions. No need to implement if ASEva.ModuleConfig.GetConfigStatus won't return ASEva.ConfigStatus.EnabledWithWarning
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.4) [可选实现] 禁用存在错误的部分功能时被调用。若 ASEva.ModuleConfig.GetConfigStatus 不会返回 ASEva.ConfigStatus.EnabledWithWarning 则不需要实现
        /// </summary>
        public virtual Task DisableErrorPart() { return Task.CompletedTask; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.9.6) Component configuration with only one enable field
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.9.6) 只含一个是否启用字段的组件配置
    /// </summary>
    public class SimpleModuleConfig : ModuleConfig
    {
        /// \~English
        /// <summary>
        /// Whether to enable the component
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; } = false;

        public override string GetConfig()
        {
            return Enable ? "enable" : "disable";
        }

        public override void SetConfig(string config)
        {
            Enable = config == "enable";
        }

        public override Task<(ConfigStatus, string)> GetConfigStatus()
        {
            return Task.FromResult<(ConfigStatus, string)>((Enable ? ConfigStatus.Enabled : ConfigStatus.Disabled, null));
        }

        public override void DisableAll()
        {
            Enable = false;
        }
    }
}
