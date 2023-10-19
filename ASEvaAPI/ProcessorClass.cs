using System;
using System.Collections.Generic;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Base class for processor component definition
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 数据处理组件定义的基类
    /// </summary>
    public class ProcessorClass
    {
        /// \~English
        /// <summary>
        /// [Required] Called while getting component's name
        /// </summary>
        /// <returns>Dictionary. The key 'en' is English, 'ch' is Chinese</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取数据处理组件的名称时被调用
        /// </summary>
        /// <returns>数据处理组件名称表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetProcessorName() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting component's class ID
        /// </summary>
        /// <returns>Processor class ID</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取数据处理组件的类别ID时被调用
        /// </summary>
        /// <returns>数据处理组件类别ID</returns>
        public virtual String GetProcessorClassID() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called to create configuration object. It will be empty configuration if not implemented, that is always enabled
        /// </summary>
        /// <returns>Configuration object</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 创建配置对象时被调用。若不实现则为空配置，常时启用状态
        /// </summary>
        /// <returns>配置对象</returns>
        public virtual ModuleConfig CreateConfig() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called to create processor object
        /// </summary>
        /// <param name="config">Configuration string</param>
        /// <returns>Processor object</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 创建数据处理对象时被调用
        /// </summary>
        /// <param name="config">配置对象，定义数据处理行为</param>
        /// <returns>数据处理对象</returns>
        public virtual Processor CreateProcessor(ModuleConfig config) { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting the IDs of general raw data channel that the data can be output as general sample directly
        /// </summary>
        /// <returns>Dictionary. The key is general raw data channel ID, the value is general sample channel ID</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取可直接作为样本输出的原始数据协议列表及对应的样本别名
        /// </summary>
        /// <returns>原始数据协议（键）列表及对应的样本别名（值）</returns>
        public virtual Dictionary<String, String> GetRawToSampleProtocols() { return null; }
    }
}
