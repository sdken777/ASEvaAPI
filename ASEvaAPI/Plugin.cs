using System;
using System.Collections.Generic;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 插件入口基类。在C\#类库项目中可实现一个继承Plugin的类作为dll入口，只要保证dll文件名、类所在命名空间、类名三者一致，即可被ASEva作为插件加载
    /// </summary>
    public class Plugin
    {
        /// <summary>
        /// [必须实现] 获取插件版本时被调用
        /// </summary>
        /// <returns>版本号</returns>
        public virtual Version GetVersion() { return new Version(1, 0, 0, 0); }

        /// <summary>
        /// [可选实现] 获取插件包含的所有窗口组件的定义对象时被调用
        /// </summary>
        /// <returns>窗口组件定义对象的列表</returns>
        public virtual WindowClass[] GetWindowClasses() { return null; }

        /// <summary>
        /// [可选实现] 获取插件包含的所有对话框组件的定义对象时被调用
        /// </summary>
        /// <returns>对话框组件定义对象的列表</returns>
        public virtual DialogClass[] GetDialogClasses() { return null; }

        /// <summary>
        /// [可选实现] 获取插件包含的所有数据处理组件的定义对象时被调用
        /// </summary>
        /// <returns>数据处理组件定义对象的列表</returns>
        public virtual ProcessorClass[] GetProcessorClasses() { return null; }

        /// <summary>
        /// [可选实现] 获取插件包含的所有原生模块的定义对象时被调用
        /// </summary>
        /// <returns>原生模块定义对象的列表</returns>
        public virtual NativeClass[] GetNativeClasses() { return null; }

        /// <summary>
        /// [可选实现] 获取插件包含的所有独立任务组件的定义对象时被调用
        /// </summary>
        /// <returns>独立任务组件定义对象的列表</returns>
        public virtual TaskClass[] GetTaskClasses() { return null; }

        /// <summary>
        /// (api:app=2.3.0) [可选实现] 获取插件包含的主流程时被调用
        /// </summary>
        /// <returns>主流程对象</returns>
        public virtual MainWorkflow GetMainWorkflow() { return null; }

        /// <summary>
        /// [可选实现] 获取插件包需要使用的所有全局路径的键
        /// </summary>
        /// <returns>全局路径键的列表</returns>
        public virtual String[] GetGlobalPathKeys() { return null; }

        /// <summary>
        /// (api:app=2.6.6) [可选实现] 获取插件相关的原生组件ID（无需考虑本插件内的原生组件）
        /// </summary>
        /// <returns>原生组件ID列表</returns>
        public virtual String[] GetRelatedNativeModules() { return null; }

        /// <summary>
        /// [可选实现] 在插件成功加载后被调用（用于开启背景线程等）
        /// </summary>
        public virtual void OnApplicationStarted() { }

        /// <summary>
        /// [可选实现] 在应用程序准备关闭时被调用（用于发送停止线程指令等）
        /// </summary>
        public virtual void OnApplicationPrepareStopping() { }

        /// <summary>
        /// [可选实现] 在应用程序正在关闭时被调用（用于停止并确保线程已停止等）
        /// </summary>
        public virtual void OnApplicationStopping() { }

        /// <summary>
        /// [可选实现] 在项目切换后（包括新建项目）被调用
        /// </summary>
        public virtual void OnProjectSwitched() { }

        /// <summary>
        /// (api:app=2.4.0) 在数据根目录切换后被调用
        /// </summary>
        public virtual void OnDataPathSwitched() { }

        /// <summary>
        /// (api:app=2.4.0) 在数据层级切换后被调用
        /// </summary>
        public virtual void OnDataLayerSwitched() { }

        /// <summary>
        /// [可选实现] 在Session开始前被调用
        /// </summary>
        public virtual void OnSessionStarting() { }

        /// <summary>
        /// [可选实现] 在Session采集或处理完毕后被调用
        /// </summary>
        public virtual void OnSessionStopped() { }

        /// <summary>
        /// [可选实现] 在UI线程循环时被调用（用于执行仅限UI线程的功能）
        /// </summary>
        public virtual void OnUIThreadLooping() { }
    }
}
