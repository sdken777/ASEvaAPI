using System;
using System.Collections.Generic;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 数据处理组件定义的基类
    /// </summary>
    public class ProcessorClass
    {
        /// <summary>
        /// [必须实现] 获取数据处理组件的名称时被调用
        /// </summary>
        /// <returns>数据处理组件名称表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetProcessorName() { return null; }

        /// <summary>
        /// [必须实现] 获取数据处理组件的类别ID时被调用
        /// </summary>
        /// <returns>数据处理组件类别ID</returns>
        public virtual String GetProcessorClassID() { return null; }

        /// <summary>
        /// [可选实现] 创建配置对象时被调用。若不实现则仅包含一个默认的启用/禁用标志位
        /// </summary>
        /// <returns>配置对象</returns>
        public virtual ModuleConfig CreateConfig() { return null; }

        /// <summary>
        /// [必须实现] 创建数据处理对象时被调用
        /// </summary>
        /// <param name="config">配置对象，定义数据处理行为</param>
        /// <returns>数据处理对象</returns>
        public virtual Processor CreateProcessor(ModuleConfig config) { return null; }

        /// <summary>
        /// [可选实现] 获取可直接作为样本输出的原始数据协议列表及对应的样本别名
        /// </summary>
        /// <returns>原始数据协议（键）列表及对应的样本别名（值）</returns>
        public virtual Dictionary<String, String> GetRawToSampleProtocols() { return null; }
    }
}
