using System;
using System.Collections.Generic;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 组件配置的状态
    /// </summary>
    public enum ConfigStatus
    {
        /// <summary>
        /// 启用且正常
        /// </summary>
        Enabled,
        /// <summary>
        /// 启用但存在错误
        /// </summary>
        EnabledWithError,
        /// <summary>
        /// 启用但部分存在错误
        /// </summary>
        EnabledWithWarning,
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled,
    }

    /// <summary>
    /// (api:app=2.0.0) 通用样本的标题部分
    /// </summary>
    public class GeneralSampleTitle
    {
        /// <summary>
        /// 标题文字列表
        /// </summary>
        public List<String> Titles { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GeneralSampleTitle()
        {
            Titles = new List<string>();
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 场景属性标题
    /// </summary>
    public class SceneTitle
    {
        /// <summary>
        /// 标题文字列表
        /// </summary>
        public List<String> Titles { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SceneTitle()
        {
            Titles = new List<string>();
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 样本通道别名
    /// </summary>
    public class ChannelAlias
    {
        /// <summary>
        /// 样本通道ID
        /// </summary>
        public String ChannelID { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public String AliasName { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 使用的视频通道
    /// </summary>
    public enum UsingVideoChannel
    {
        /// <summary>
        /// 通道A
        /// </summary>
        ChannelA = 0,

        /// <summary>
        /// 通道B
        /// </summary>
        ChannelB = 1,

        /// <summary>
        /// 通道C
        /// </summary>
        ChannelC = 2,

        /// <summary>
        /// 通道D
        /// </summary>
        ChannelD = 3,

        /// <summary>
        /// 通道E
        /// </summary>
        ChannelE = 4,

        /// <summary>
        /// 通道F
        /// </summary>
        ChannelF = 5,

        /// <summary>
        /// 通道G
        /// </summary>
        ChannelG = 6,

        /// <summary>
        /// 通道H
        /// </summary>
        ChannelH = 7,

        /// <summary>
        /// 通道I
        /// </summary>
        ChannelI = 8,

        /// <summary>
        /// 通道J
        /// </summary>
        ChannelJ = 9,

        /// <summary>
        /// 通道K
        /// </summary>
        ChannelK = 10,

        /// <summary>
        /// 通道L
        /// </summary>
        ChannelL = 11,

        /// <summary>
        /// (api:app=2.1.3) 通道M
        /// </summary>
        ChannelM = 12,

        /// <summary>
        /// (api:app=2.1.3) 通道N
        /// </summary>
        ChannelN = 13,

        /// <summary>
        /// (api:app=2.1.3) 通道O
        /// </summary>
        ChannelO = 14,

        /// <summary>
        /// (api:app=2.1.3) 通道P
        /// </summary>
        ChannelP = 15,

        /// <summary>
        /// (api:app=2.1.3) 通道Q
        /// </summary>
        ChannelQ = 16,

        /// <summary>
        /// (api:app=2.1.3) 通道R
        /// </summary>
        ChannelR = 17,

        /// <summary>
        /// (api:app=2.1.3) 通道S
        /// </summary>
        ChannelS = 18,

        /// <summary>
        /// (api:app=2.1.3) 通道T
        /// </summary>
        ChannelT = 19,

        /// <summary>
        /// (api:app=2.1.3) 通道U
        /// </summary>
        ChannelU = 20,

        /// <summary>
        /// (api:app=2.1.3) 通道V
        /// </summary>
        ChannelV = 21,

        /// <summary>
        /// (api:app=2.1.3) 通道W
        /// </summary>
        ChannelW = 22,

        /// <summary>
        /// (api:app=2.1.3) 通道X
        /// </summary>
        ChannelX = 23,

        /// <summary>
        /// 特殊摄像头：前向（标准模型）
        /// </summary>
        SpecialFront = 100,

        /// <summary>
        /// 特殊摄像头：左侧车道线
        /// </summary>
        SpecialLeftLine = 101,

        /// <summary>
        /// 特殊摄像头：右侧车道线
        /// </summary>
        SpecialRightLine = 102,

        /// <summary>
        /// 特殊摄像头：左侧盲区
        /// </summary>
        SpecialLeftBS = 103,

        /// <summary>
        /// 特殊摄像头：右侧盲区
        /// </summary>
        SpecialRightBS = 104,

        /// <summary>
        /// (api:app=2.1.2) 特殊摄像头：前向（鱼眼模型）
        /// </summary>
        SpecialFisheyeFront = 105,
    }

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

    /// <summary>
    /// (api:app=2.0.0) 模块配置的基类
    /// </summary>
    public class ModuleConfig
    {
        /// <summary>
        /// [必须实现] 获取配置的字符串描述时被调用
        /// </summary>
        /// <returns>字符串描述，建议使用XML或JSON</returns>
        public virtual String GetConfig() { return null; }

        /// <summary>
        /// [必须实现] 通过字符串描述更新配置时被调用
        /// </summary>
        /// <param name="config">字符串描述，需与 ASEva.ModuleConfig.GetConfig 获取的字符串格式一致</param>
        public virtual void SetConfig(String config) { }

        /// <summary>
        /// [必须实现] 查询配置状态时被调用
        /// </summary>
        /// <returns>配置状态</returns>
        public virtual ConfigStatus GetConfigStatus() { return ConfigStatus.Disabled; }

        /// <summary>
        /// [可选实现] 查询各子功能配置状态时被调用
        /// </summary>
        /// <returns>各子功能的配置状态</returns>
        public virtual ConfigStatus[] GetChildConfigStatus() { return null; }

        /// <summary>
        /// [可选实现] 获取输出的样本标题集合时被调用
        /// </summary>
        /// <returns>样本标题集合，key为样本ID，value为标题描述</returns>
        public virtual Dictionary<String, GeneralSampleTitle> GetProcessorOutputSampleTitles() { return null; }

        /// <summary>
        /// [可选实现] 获取输出的场景标题集合时被调用
        /// </summary>
        /// <returns>场景标题集合，key为场景ID，value为标题描述</returns>
        public virtual Dictionary<String, SceneTitle> GetProcessorOutputSceneTitles() { return null; }

        /// <summary>
        /// [可选实现] 获得该组件默认类别下的所有输出信号的名称时被调用
        /// </summary>
        /// <returns>信号名称列表</returns>
        public virtual String[] GetProcessorOutputSignalNames() { return null; }

        /// <summary>
        /// [可选实现] 获得该组件非默认类别下的所有输出信号的名称时被调用
        /// </summary>
        /// <returns>信号名称列表，key为类别名称</returns>
        public virtual Dictionary<String, String[]> GetProcessorOutputSignalNameTable() { return null; }

        /// <summary>
        /// [可选实现] 获得该组件所有输出图表的定义时被调用
        /// </summary>
        /// <returns>图表定义列表</returns>
        public virtual GraphDefinition[] GetProcessorOutputGraphDefinitions() { return null; }

        /// <summary>
        /// [可选实现] 获取数据处理需要用到的所有信号打包配置时被调用
        /// </summary>
        /// <returns>信号打包配置列表</returns>
        public virtual List<SignalPackConfig> GetProcessorRelatedSignalPackings() { return null; }

        /// <summary>
        /// [可选实现] 获取所有输出样本通道的别名列表时被调用
        /// </summary>
        /// <returns>通道别名列表</returns>
        public virtual List<ChannelAlias> GetChannelAliasList() { return null; }

        /// <summary>
        /// [可选实现] 获取所有使用的视频通道时被调用
        /// </summary>
        /// <returns>所有使用的视频通道</returns>
        public virtual List<UsingVideoChannel> GetUsingVideoChannels() { return null; }

        /// <summary>
        /// (api:app=2.2.1) [可选实现] 获取文件写入需要的所有数据类型
        /// </summary>
        /// <returns>文件写入需要的所有数据类型</returns>
        public virtual List<RecordDataType> GetRecordDataTypes() { return null; }

        /// <summary>
        /// [可选实现] 用户一键禁用功能时被调用
        /// </summary>
        public virtual void DisableAll() { }

        /// <summary>
        /// [可选实现] 禁用存在错误的部分功能时被调用。若 ASEva.ProcessorConfig.GetConfigStatus 不会返回 ASEva.ProcessorConfigStatus.EnabledWithWarning 则不需要实现
        /// </summary>
        public virtual void DisableErrorPart() { }
    }
}
