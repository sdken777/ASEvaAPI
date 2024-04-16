using System;
using System.Collections.Generic;

namespace ASEva
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Base class for task component definition
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 独立任务组件定义的基类
    /// </summary>
    public class TaskClass
    {
        /// \~English
        /// <summary>
        /// [Required] Called while getting component's class ID
        /// </summary>
        /// <returns>Task class ID</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取独立任务组件的类别ID时被调用
        /// </summary>
        /// <returns>独立任务组件的类别ID</returns>
        public virtual String GetTaskClassID() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting component's name
        /// </summary>
        /// <returns>Dictionary. The key 'en' is English, 'ch' is Chinese</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取独立任务的标题时被调用
        /// </summary>
        /// <returns>独立任务的标题表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetTaskTitle() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting whether the task is based on native plugins
        /// </summary>
        /// <returns>Whether the task is based on native plugins</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 查询是否为原生端独立处理任务时被调用
        /// </summary>
        /// <returns>是否为原生端独立处理任务</returns>
        public virtual bool IsNativeTask() { return false; }

        /// \~English
        /// <summary>
        /// [Required for app-layer task] Create standalone task object
        /// </summary>
        /// <returns>Standalone task object</returns>
        /// \~Chinese
        /// <summary>
        /// [非原生模式下必须实现] 创建独立任务对象
        /// </summary>
        /// <returns>独立任务对象</returns>
        public virtual StandaloneTask CreateTask() { return null; }

        /// \~English
        /// <summary>
        /// [Required for native-layer task] Called while getting native plugin's type ID
        /// </summary>
        /// <returns>Native plugin's typeID, the same as the "type" field in info.txt</returns>
        /// \~Chinese
        /// <summary>
        /// [原生模式下必须实现] 获取原生插件的类型ID时被调用
        /// </summary>
        /// <returns>类型ID，需要与插件info.txt中的type字段一致</returns>
        public virtual String GetNativePluginType() { return null; }

        /// \~English
        /// <summary>
        /// [Required for native-layer task] Called while getting the task name in native plugin
        /// </summary>
        /// <returns>Task name</returns>
        /// \~Chinese
        /// <summary>
        /// [原生模式下必须实现] 获取原生独立处理任务名称时被调用
        /// </summary>
        /// <returns>任务名称</returns>
        public virtual String GetNativeTaskName() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting the task's default configuration string
        /// </summary>
        /// <returns>Default configuration string, set to null if unsupported</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取独立任务的默认配置时被调用
        /// </summary>
        /// <returns>独立任务的默认配置字符串，若不支持则为null</returns>
        public virtual String GetDefaultConfig() { return null;}
    }
}
