using System;
using System.Collections.Generic;

namespace ASEva
{
    /// \~English
    /// (api:app=2.0.0) Component configuration's status
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 组件配置的状态
    /// </summary>
    public enum ConfigStatus
    {
        /// \~English
        /// Enabled and normal
        /// \~Chinese
        /// <summary>
        /// 启用且正常
        /// </summary>
        Enabled,
        
        /// \~English
        /// Enabled but with errors
        /// \~Chinese
        /// <summary>
        /// 启用但存在错误
        /// </summary>
        EnabledWithError,

        /// \~English
        /// Enabled but with warnings
        /// \~Chinese
        /// <summary>
        /// 启用但部分存在错误
        /// </summary>
        EnabledWithWarning,

        /// \~English
        /// Disabled
        /// \~Chinese
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled,
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 通用样本的标题部分
    /// </summary>
    public class GeneralSampleTitle
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 标题文字列表
        /// </summary>
        public List<String> Titles { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GeneralSampleTitle()
        {
            Titles = new List<string>();
        }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 场景属性标题
    /// </summary>
    public class SceneTitle
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 标题文字列表
        /// </summary>
        public List<String> Titles { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SceneTitle()
        {
            Titles = new List<string>();
        }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 样本通道别名
    /// </summary>
    public class ChannelAlias
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 样本通道ID
        /// </summary>
        public String ChannelID { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 别名
        /// </summary>
        public String AliasName { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Using video channel in processing
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 数据处理使用的视频通道
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
        /// (api:app=2.1.3) Channel M
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道M
        /// </summary>
        ChannelM = 12,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel N
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道N
        /// </summary>
        ChannelN = 13,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel O
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道O
        /// </summary>
        ChannelO = 14,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel P
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道P
        /// </summary>
        ChannelP = 15,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel Q
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道Q
        /// </summary>
        ChannelQ = 16,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel R
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道R
        /// </summary>
        ChannelR = 17,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel S
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道S
        /// </summary>
        ChannelS = 18,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel T
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道T
        /// </summary>
        ChannelT = 19,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel U
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道U
        /// </summary>
        ChannelU = 20,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel V
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道V
        /// </summary>
        ChannelV = 21,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel W
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道W
        /// </summary>
        ChannelW = 22,

        /// \~English
        /// <summary>
        /// (api:app=2.1.3) Channel X
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.3) 通道X
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
        /// (api:app=2.1.2) Special camera: Front (Fisheye model)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.1.2) 特殊摄像头：前向（鱼眼模型）
        /// </summary>
        SpecialFisheyeFront = 105,
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 信号打包中单个信号的配置
    /// </summary>
    public class SignalPackConfigElem
    {
        public String ValueID { get; set; }
        public String SignID { get; set; }
        public double Scale { get; set; }
        public double Timeout { get; set; }
        public bool IsNearestMode { get; set; }
        public SignalPackConfigElem()
        {
            Scale = 1;
            Timeout = 1200;
            IsNearestMode = false;
        }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 信号打包配置
    /// </summary>
    public class SignalPackConfig
    {
        public String Protocol { get; set; }
        public bool IsInterpolationMode { get; set; }
        public String FinalMessageID { get; set; }
        public int SamplingRate { get; set; }
        public List<SignalPackConfigElem> SignalConfigs { get; set; }
        public SignalPackConfig()
        {
            IsInterpolationMode = false;
            SamplingRate = 100;
            SignalConfigs = new List<SignalPackConfigElem>();
        }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 模块配置的基类
    /// </summary>
    public class ModuleConfig
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取配置的字符串描述时被调用
        /// </summary>
        /// <returns>字符串描述，建议使用XML或JSON</returns>
        public virtual String GetConfig() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 通过字符串描述更新配置时被调用
        /// </summary>
        /// <param name="config">字符串描述，需与 ASEva.ModuleConfig.GetConfig 获取的字符串格式一致</param>
        public virtual void SetConfig(String config) { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 查询配置状态时被调用
        /// </summary>
        /// <returns>配置状态</returns>
        public virtual ConfigStatus GetConfigStatus() { return ConfigStatus.Disabled; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 查询各子功能配置状态时被调用
        /// </summary>
        /// <returns>各子功能的配置状态</returns>
        public virtual ConfigStatus[] GetChildConfigStatus() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [ProcessorClass/NativeClass可选实现] 获取输出的样本标题集合时被调用
        /// </summary>
        /// <returns>样本标题集合，key为样本ID，value为标题描述</returns>
        public virtual Dictionary<String, GeneralSampleTitle> GetProcessorOutputSampleTitles() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [ProcessorClass可选实现] 获取输出的场景标题集合时被调用
        /// </summary>
        /// <returns>场景标题集合，key为场景ID，value为标题描述</returns>
        public virtual Dictionary<String, SceneTitle> GetProcessorOutputSceneTitles() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [ProcessorClass/NativeClass可选实现] 获得该组件默认类别下的所有输出信号的名称时被调用
        /// </summary>
        /// <returns>信号名称列表</returns>
        public virtual String[] GetProcessorOutputSignalNames() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [ProcessorClass可选实现] 获得该组件非默认类别下的所有输出信号的名称时被调用
        /// </summary>
        /// <returns>信号名称列表，key为类别名称</returns>
        public virtual Dictionary<String, String[]> GetProcessorOutputSignalNameTable() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [ProcessorClass可选实现] 获得该组件所有输出图表的定义时被调用
        /// </summary>
        /// <returns>图表定义列表</returns>
        public virtual GraphDefinition[] GetProcessorOutputGraphDefinitions() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [ProcessorClass/NativeClass可选实现] 获取数据处理需要用到的所有信号打包配置时被调用
        /// </summary>
        /// <returns>信号打包配置列表</returns>
        public virtual List<SignalPackConfig> GetProcessorRelatedSignalPackings() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [ProcessorClass/NativeClass可选实现] 获取所有输出样本通道的别名列表时被调用
        /// </summary>
        /// <returns>通道别名列表</returns>
        public virtual List<ChannelAlias> GetChannelAliasList() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [ProcessorClass/NativeClass可选实现] 获取所有使用的视频通道时被调用
        /// </summary>
        /// <returns>所有使用的视频通道</returns>
        public virtual List<UsingVideoChannel> GetUsingVideoChannels() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.2.1) [NativeClass可选实现] 获取文件写入需要的所有数据类型
        /// </summary>
        /// <returns>文件写入需要的所有数据类型</returns>
        public virtual List<RecordDataType> GetRecordDataTypes() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.2) 已弃用，远程采集应使用 ASEva.FileIONames.RemoteReaderNames
        /// </summary>
        public virtual bool MayCauseLowSpeed() { return false; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 用户一键禁用功能时被调用
        /// </summary>
        public virtual void DisableAll() { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 禁用存在错误的部分功能时被调用。若 ASEva.ProcessorConfig.GetConfigStatus 不会返回 ASEva.ProcessorConfigStatus.EnabledWithWarning 则不需要实现
        /// </summary>
        public virtual void DisableErrorPart() { }
    }
}
