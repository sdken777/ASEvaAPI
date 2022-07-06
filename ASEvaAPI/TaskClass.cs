using System;
using System.Collections.Generic;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 独立任务组件定义的基类
    /// </summary>
    public class TaskClass
    {
        /// <summary>
        /// [必须实现] 获取独立任务组件的类别ID时被调用
        /// </summary>
        /// <returns>独立任务组件的类别ID</returns>
        public virtual String GetTaskClassID() { return null; }

        /// <summary>
        /// [必须实现] 获取独立任务的标题时被调用
        /// </summary>
        /// <returns>独立任务的标题表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetTaskTitle() { return null; }

        /// <summary>
        /// [必须实现] 查询是否为原生端独立处理任务时被调用
        /// </summary>
        /// <returns>是否为原生端独立处理任务</returns>
        public virtual bool IsNativeTask() { return false; }

        /// <summary>
        /// [非原生模式下必须实现] 创建独立任务对象
        /// </summary>
        /// <returns>独立任务对象</returns>
        public virtual Task CreateTask() { return null; }

        /// <summary>
        /// [原生模式下必须实现] 获取原生模块的类型ID时被调用
        /// </summary>
        /// <returns>类型ID，需要与插件info.txt中的type字段一致</returns>
        public virtual String GetNativePluginType() { return null; }

        /// <summary>
        /// [原生模式下必须实现] 获取原生独立处理任务名称时被调用
        /// </summary>
        /// <returns>任务名称</returns>
        public virtual String GetNativeTaskName() { return null; }

        /// <summary>
        /// (api:app=2.5.1) [可选实现] 获取独立任务的默认配置时被调用
        /// </summary>
        /// <returns>独立任务的默认配置字符串，若不支持则为null</returns>
        public virtual String GetDefaultConfig() { return null;}
    }
}
