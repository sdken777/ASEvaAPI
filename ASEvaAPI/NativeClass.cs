using System;
using System.Collections.Generic;

namespace ASEva
{
    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Base class for native component definition
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 原生组件定义的基类
    /// </summary>
    public class NativeClass
    {
        /// \~English
        /// <summary>
        /// [Required] Called while getting component's name
        /// </summary>
        /// <returns>Dictionary. The key 'en' is English, 'ch' is Chinese</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取原生组件的名称时被调用
        /// </summary>
        /// <returns>原生组件名称表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetNativeName() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting component's class ID
        /// </summary>
        /// <returns>Native class ID</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取原生组件的类别ID时被调用
        /// </summary>
        /// <returns>原生组件类别ID</returns>
        public virtual String GetNativeClassID() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting the related native plugin's type ID
        /// </summary>
        /// <returns>Native plugin's type ID, the same as the "type" field in info.txt</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取原生插件的类型ID时被调用
        /// </summary>
        /// <returns>原生插件类型ID，需要与插件info.txt中的type字段一致</returns>
        public virtual String GetNativePluginType() { return null; }

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
        /// [Optional] Called while checking whether the related plugins contain device connection
        /// </summary>
        /// <returns>Whether the related plugins contain device connection, default is false</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 查询是否包含硬件设备连接功能时被调用
        /// </summary>
        /// <returns>是否包含硬件设备连接功能，默认为false</returns>
        public virtual bool ContainsDeviceConnection() { return false; }

        /// \~English
        /// <summary>
        /// (api:app=2.2.0) [Optional] Called while getting the names of file readers and writers
        /// </summary>
        /// <returns>The names of file readers and writers, set to null if file functions unsupported</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.2.0) [可选实现] 获取文件读写相关名称的集合时被调用
        /// </summary>
        /// <returns>文件读写相关名称的集合，默认为null，即不提供文件读写功能</returns>
        public virtual FileIONames GetFileIONames() { return null; }
    }
}
