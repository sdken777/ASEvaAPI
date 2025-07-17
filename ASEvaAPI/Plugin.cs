using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Base class for plugin entry. In the C\# class library project, you can implement a class that's derived from Plugin as a dll entry. As long as the dll file name, the namespace where the class is located, and the class name are consistent, the dll can be loaded by the framework as a plugin
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 插件入口基类。在C\#类库项目中可实现一个继承Plugin的类作为dll入口，只要保证dll文件名、类所在命名空间、类名三者一致，即可被框架软件作为插件加载
    /// </summary>
    public class Plugin
    {
        /// \~English
        /// <summary>
        /// [Required] Called while getting plugin's version
        /// </summary>
        /// <returns>Plugin's version</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取插件版本时被调用
        /// </summary>
        /// <returns>版本号</returns>
        public virtual Version GetVersion() { return new Version(1, 0, 0, 0); }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting plugin's release date
        /// </summary>
        /// <returns>Plugin's release date</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取当前版本发布日期时被调用
        /// </summary>
        /// <returns>当前版本发布日期</returns>
        public virtual DateTime? GetVersionReleaseDate() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting all definitions of window components
        /// </summary>
        /// <returns>All definitions of window components</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件包含的所有窗口组件的定义对象时被调用
        /// </summary>
        /// <returns>窗口组件定义对象的列表</returns>
        public virtual WindowClass[] GetWindowClasses() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting all definitions of dialog components
        /// </summary>
        /// <returns>All definitions of dialog components</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件包含的所有对话框组件的定义对象时被调用
        /// </summary>
        /// <returns>对话框组件定义对象的列表</returns>
        public virtual DialogClass[] GetDialogClasses() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting all definitions of console components
        /// </summary>
        /// <returns>All definitions of console components</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件包含的所有控制台组件的定义对象时被调用
        /// </summary>
        /// <returns>控制台组件定义对象的列表</returns>
        public virtual ConsoleClass[] GetConsoleClasses() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting all definitions of processor components
        /// </summary>
        /// <returns>All definitions of processor components</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件包含的所有数据处理组件的定义对象时被调用
        /// </summary>
        /// <returns>数据处理组件定义对象的列表</returns>
        public virtual ProcessorClass[] GetProcessorClasses() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting all definitions of native components
        /// </summary>
        /// <returns>All definitions of native components</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件包含的所有原生组件的定义对象时被调用
        /// </summary>
        /// <returns>原生组件定义对象的列表</returns>
        public virtual NativeClass[] GetNativeClasses() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting all definitions of task components
        /// </summary>
        /// <returns>All definitions of task components</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件包含的所有独立任务组件的定义对象时被调用
        /// </summary>
        /// <returns>独立任务组件定义对象的列表</returns>
        public virtual TaskClass[] GetTaskClasses() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting all definitions of device components
        /// </summary>
        /// <returns>All definitions of device components</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件包含的所有设备组件的定义对象时被调用
        /// </summary>
        /// <returns>设备组件定义对象的列表</returns>
        public virtual DeviceClass[] GetDeviceClasses() { return null; }

        /// \~English
        /// <summary>
        /// (api:app=3.1.0) Deprecated. Use GetWorkflow(clientSide) instead.
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.0) 弃用。请使用GetWorkflow(clientSide)替代。
        /// </summary>
        public virtual CommonWorkflow GetWorkflow() { return null; }

        /// \~English
        /// <summary>
        /// (api:app=3.9.1) [Optional] Called while getting workflow object
        /// </summary>  
        /// <param name="clientSide">Whether the workflow is client side</param>
        /// <returns>The workflow object</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.9.1) [可选实现] 获取插件包含的流程对象时被调用
        /// </summary>
        /// <param name="clientSide">是否为客户端流程</param>
        /// <returns>流程对象</returns>
        public virtual CommonWorkflow GetWorkflow(bool clientSide) { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting all keys of global paths needed by the plugin
        /// </summary>
        /// <returns>All keys of global paths needed by the plugin</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件包需要使用的所有全局路径的键时被调用
        /// </summary>
        /// <returns>全局路径键的列表</returns>
        public virtual String[] GetGlobalPathKeys() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting all related native class IDs (No need to include the ones supplied by self plugin)
        /// </summary>
        /// <returns>Related native class IDs</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件相关的原生组件ID时被调用（无需考虑本插件内的原生组件）
        /// </summary>
        /// <returns>原生组件ID列表</returns>
        public virtual String[] GetRelatedNativeModules() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting titles of guest synchronization IDs
        /// </summary>
        /// <returns>Dictionary. The key is guest synchronization ID</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件相关的客机同步通道标题表时被调用
        /// </summary>
        /// <returns>客机同步通道标题表，键为客机同步ID，值为其标题</returns>
        public virtual Dictionary<String, String> GetGuestSyncTitleTable() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting the library files that should be preloaded
        /// </summary>
        /// <returns>List of library file names (not absolute path)</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取需要提前加载的库文件列表时被调用
        /// </summary>
        /// <returns>库文件名的列表，不需要绝对路径</returns>
        public virtual String[] GetPreloadLibFiles() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting license notices of third party software that used by the plugin
        /// </summary>
        /// <returns>Dictionary. The key is third party software's title</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取插件使用的第三方软件版权声明时被调用
        /// </summary>
        /// <returns>键为标题，值为版权声明</returns>
        public virtual Dictionary<String, String> GetThirdPartyNotices() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting the executable paths needed to be run with auto root privilege
        /// </summary>
        /// <returns>The executable paths</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取Linux系统下需要自动root权限的可执行程序路径时被调用
        /// </summary>
        /// <returns>可执行程序路径列表</returns>
        public virtual String[] GetAutoRootTargets() { return null; }

        /// \~English
        /// <summary>
        /// (api:app=3.4.5) [Optional] Called while getting the list of preference variable ID
        /// </summary>
        /// <returns>The list of preference variable ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.4.5) [可选实现] 获取用户偏好变量ID列表时被调用
        /// </summary>
        /// <returns>用户偏好变量ID列表</returns>
        public virtual String[] GetPreferenceVariables() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while setting logger for debug info
        /// </summary>
        /// <param name="logger">Debug info logger. You can print messages with source using the logger</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 设置用于记录调试信息的接口时被调用
        /// </summary>
        /// <param name="logger">记录调试信息的接口，基于此接口记录可带上来源信息</param>
        public virtual void UseLogger(Logger logger) { }

        /// \~English
        /// <summary>
        /// [Optional] Called while the plugin is loaded successfully (Background threads can be started here)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在插件成功加载后被调用（用于开启背景线程等）
        /// </summary>
        public virtual void OnApplicationStarted() { }

        /// \~English
        /// <summary>
        /// (api:app=3.1.0) [Optional] Called while the application is about to exit
        /// </summary>
        /// <param name="force">Whether forced to exit. If true, background threads can be notified to stop here</param>
        /// <returns>Whether it's OK to exit (no use while forced to exit)</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.0) [可选实现] 在应用程序准备关闭时被调用
        /// </summary>
        /// <param name="force">是否为强制关闭，若为true可发送停止线程指令等</param>
        /// <returns>返回是否可关闭，强制关闭时将不起作用</returns>
        public virtual Task<bool> OnApplicationPrepareStopping(bool force) { return Task.FromResult(true); }

        /// \~English
        /// <summary>
        /// [Optional] Called while the application is exiting (Background threads should be stopped here)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在应用程序正在关闭时被调用（用于停止并确保线程已停止等）
        /// </summary>
        public virtual void OnApplicationStopping() { }

        /// \~English
        /// <summary>
        /// [Optional] Called while another project loaded (include new project)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在项目切换后（包括新建项目）被调用
        /// </summary>
        public virtual void OnProjectSwitched() { }

        /// \~English
        /// <summary>
        /// [Optional] Called while data path changed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在数据根目录切换后被调用
        /// </summary>
        public virtual void OnDataPathSwitched() { }

        /// \~English
        /// <summary>
        /// [Optional] Called while data layer changed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在数据层级切换后被调用
        /// </summary>
        public virtual void OnDataLayerSwitched() { }

        /// \~English
        /// <summary>
        /// [Optional] Called before session starting
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在Session开始前被调用
        /// </summary>
        public virtual void OnSessionStarting() { }

        /// \~English
        /// <summary>
        /// [Optional] Called after session stopped
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在Session采集或处理完毕后被调用
        /// </summary>
        public virtual void OnSessionStopped() { }

        /// \~English
        /// <summary>
        /// [Optional] Called in the UI main loop (For functions that can only be run in UI main thread)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在UI线程循环时被调用（用于执行仅限UI线程的功能）
        /// </summary>
        public virtual void OnUIThreadLooping() { }

        /// \~English
        /// <summary>
        /// [Optional] Called while checking whether the plugin is busy with some time-consuming operation
        /// </summary>
        /// <returns>Description of the time-consuming operation, null if none</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在检查插件是否忙于长耗时操作时被调用
        /// </summary>
        /// <returns>长耗时操作的描述，若无则返回null</returns>
        public virtual String OnCheckBusy() { return null; }

        /// \~English
        /// <summary>
        /// (api:app=3.7.11) [Optional] Called while getting new sound wave data
        /// </summary>
        /// <param name="soundWave">The new sound wave data, 44100Hz, 16bit, mono</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.7.11) [可选实现] 在获取到新的声波数据时被调用
        /// </summary>
        /// <param name="soundWave">新的声波数据，44100Hz，16bit，单通道</param>
        public virtual void OnSoundWave(short[] soundWave) { }
        
    }
}
