using System;
using System.Collections.Generic;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Base class for window component definition
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 窗口组件定义的基类
    /// </summary>
    public class WindowClass
    {
        /// \~English
        /// <summary>
        /// (api:app=3.1.6) [Required] Called while getting component's name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.6) [必须实现] 获取窗口组件的名称时被调用
        /// </summary>
        public virtual Dictionary<Language, String> GetWindowName() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting component's class ID
        /// </summary>
        /// <returns>Window class ID</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取窗口组件的类别ID时被调用
        /// </summary>
        /// <returns>窗口组件的类别ID</returns>
        public virtual String GetWindowClassID() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting window's icon
        /// </summary>
        /// <returns>Icon image (size should be 16x16)</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取窗口组件的图标图像时被调用
        /// </summary>
        /// <returns>图标图像（大小一般为16x16）</returns>
        public virtual object GetWindowImage() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting class IDs of related components
        /// </summary>
        /// <returns>Class IDs of related components</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取窗口组件相关的组件ID
        /// </summary>
        /// <returns>组件ID列表</returns>
        public virtual String[] GetRelatedModules() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called to create window panel
        /// </summary>
        /// <returns>Window panel to fill the container's client area</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 创建窗口控件时被调用
        /// </summary>
        /// <returns>窗口控件，该控件将填满显示窗口</returns>
        public virtual object CreateWindow() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting whether multiple window instances are supported
        /// </summary>
        /// <returns>Whether multiple window instances are supported, default is false</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 查询是否支持显示多个窗口时被调用
        /// </summary>
        /// <returns>是否支持显示多个窗口，默认为false</returns>
        public virtual bool IsMultipleWindowSupported() { return false; }

        /// \~English
        /// <summary>
        /// Deprecated, you should implement GetRawToSampleConfigs method
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应实现GetRawToSampleConfigs方法
        /// </summary>
        public virtual Dictionary<String, String> GetRawToSampleProtocols() { return null; }

        /// \~English
        /// <summary>
        /// (api:app=3.8.0) [Optional] Called while getting configs of auto converting raw data to general sample (for raw data output by this component)
        /// </summary>
        /// <returns>Configs of auto converting raw data to general sample, the key is raw data protocol, the value is config</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.8.0) [可选实现] 获取原始数据自动转通用样本的配置（针对本组件输出的原始数据）
        /// </summary>
        /// <returns>原始数据自动转通用样本的配置，键为原始数据协议，值为配置</returns>
        public virtual Dictionary<String, RawToSampleConfig> GetRawToSampleConfigs() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Return transformed window class according to the configuration string
        /// </summary>
        /// <param name="config">Configuration string</param>
        /// <returns>Definition of transformed window class</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 根据窗口配置返回分化的窗口组件定义
        /// </summary>
        /// <param name="config">窗口配置</param>
        /// <returns>分化的窗口组件定义</returns>
        public virtual WindowClass Transform(String config) { return null; }

        /// \~English
        /// <summary>
        /// [Required for transformed window] Called while getting transform ID
        /// </summary>
        /// <returns>Transform ID</returns>
        /// \~Chinese
        /// <summary>
        /// [分化的窗口组件必须实现] 获取分化标识ID时被调用
        /// </summary>
        /// <returns>分化标识ID</returns>
        public virtual String GetTransformID() { return null; }
    }
}
