using System;
using System.Collections.Generic;

namespace ASEva
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 原生组件定义的基类
    /// </summary>
    public class NativeClass
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取原生组件的名称时被调用
        /// </summary>
        /// <returns>原生组件名称表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetNativeName() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取原生组件的类别ID时被调用
        /// </summary>
        /// <returns>原生组件类别ID</returns>
        public virtual String GetNativeClassID() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取原生模块的类型ID时被调用
        /// </summary>
        /// <returns>原生模块类型ID，需要与插件info.txt中的type字段一致</returns>
        public virtual String GetNativePluginType() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 创建配置对象时被调用。若不实现则为空配置，常时启用状态
        /// </summary>
        /// <returns>配置对象</returns>
        public virtual ModuleConfig CreateConfig() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 查询是否包含硬件设备连接功能时被调用
        /// </summary>
        /// <returns>是否包含硬件设备连接功能，默认为false</returns>
        public virtual bool ContainsDeviceConnection() { return false; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.2.0) [可选实现] 获取文件读写相关名称的集合
        /// </summary>
        /// <returns>文件读写相关名称的集合，默认为null，即不提供文件读写功能</returns>
        public virtual FileIONames GetFileIONames() { return null; }
    }
}
