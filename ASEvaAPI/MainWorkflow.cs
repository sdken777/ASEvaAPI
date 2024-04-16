using System;
using System.Collections.Generic;
using System.Xml;
using ASEva.Samples;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) Base class of main workflow
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 主流程基类
    /// </summary>
    public class MainWorkflow
    {
        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Initialize main workflow
        /// </summary>
        /// <param name="appID">Application ID</param>
        /// <param name="parameters">Initial parameters</param>
        /// <param name="uiCode">Output UI framework code, set to null if not using GUI</param>
        /// <returns>Whether initialization is successful</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 初始化主流程
        /// </summary>
        /// <param name="appID">应用程序ID</param>
        /// <param name="parameters">初始化参数</param>
        /// <param name="uiCode">输出UI框架代号，若不使用UI则输出null</param>
        /// <returns>初始化是否成功</returns>
        public virtual bool OnInit(String appID, Dictionary<String, String> parameters, out String uiCode) { uiCode = null; return true; }

        /// \~English
        /// <summary>
        /// Deprecated, implement OnRun(loopCallback, modalCallback, startupProject)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应实现OnRun(loopCallback, modalCallback, startupProject)
        /// </summary>

        public virtual void OnRun(MainWorkflowLoopCallback callback, String startupProject) {}

        /// \~English
        /// <summary>
        /// (api:app=2.10.1) [Required][OK for modal] Run the main workflow, you should guarantee ASEva.MainWorkflowLoopCallback.OnLoop and ASEva.MainWorkflowModalCallback.OnHandleModal is called in the main loop
        /// </summary>
        /// <param name="loopCallback">Loop callback interface</param>
        /// <param name="modalCallback">Modal callback interface</param>
        /// <param name="startupProject">Startup project file path</param>
        /// <returns>Always return true, or else the legacy OnRun will be called</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.10.1) [必须实现][可含模态] 运行主流程，需要在其主循环中确保执行了 ASEva.MainWorkflowLoopCallback.OnLoop 和 ASEva.MainWorkflowModalCallback.OnHandleModal
        /// </summary>
        /// <param name="loopCallback">主循环回调接口</param>
        /// <param name="modalCallback">模态对话回调接口</param>
        /// <param name="startupProject">初始项目文件路径</param>
        /// <returns>应固定返回true，否则将继续调用旧版OnRun</returns>
        public virtual bool OnRun(MainWorkflowLoopCallback loopCallback, MainWorkflowModalCallback modalCallback, String startupProject) { return false; }

        /// \~English
        /// <summary>
        /// [Optional] Notify the application's basic info
        /// </summary>
        /// <param name="info">Application's basic info</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 通知应用程序的基本信息
        /// </summary>
        /// <param name="info">应用程序的基本信息</param>
        public virtual void OnAppBasicInfo(AppBasicInfo info) {}

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Called after license validation failed
        /// </summary>
        /// <param name="reason">Reason why validation failed</param>
        /// <param name="mac">Machine code</param>
        /// <param name="callback">Framework callback interface</param>
        /// <returns>Whether a new request is accepted, if yes you should guarantee ASEva.MainWorkflowLicenseCallback.OnRevalidateLicense is called </returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 许可证验证失败后发起新的请求
        /// </summary>
        /// <param name="reason">验证失败原因</param>
        /// <param name="mac">机器码</param>
        /// <param name="callback">框架软件的回调接口</param>
        /// <returns>是否接受请求，如接受需确保已调用 ASEva.MainWorkflowLicenseCallback.OnRevalidateLicense </returns>
        public virtual bool OnLicenseRequest(LicenseRequestReason reason, String mac, MainWorkflowLicenseCallback callback) { return false; }

        /// \~English
        /// <summary>
        /// (api:app=2.6.11) [Optional] Notify the initialization phase of framework
        /// </summary>
        /// <param name="phaseDescription">Initialization phase of framework</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.11) [可选实现] 通知框架软件的初始化阶段
        /// </summary>
        /// <param name="phaseDescription">当前初始化阶段的描述</param>
        public virtual void OnCoreInitPhase(String phaseDescription) {}

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Notify the result of framework's initialization
        /// </summary>
        /// <param name="result">Result of framework's initialization</param>
        /// <param name="revisionUpdated">Whether release revision is updated</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 通知框架软件的初始化结果
        /// </summary>
        /// <param name="result">框架软件的初始化结果</param>
        /// <param name="revisionUpdated">是否更新了发行号</param>
        public virtual void OnCoreInitResult(CoreInitResult result, bool revisionUpdated) {}

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Handle error message
        /// </summary>
        /// <param name="message">The message</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 输出错误消息
        /// </summary>
        /// <param name="message">消息</param>
        public virtual void OnError(String message) {}

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Handle general message
        /// </summary>
        /// <param name="message">The message</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 输出一般消息
        /// </summary>
        /// <param name="message">消息</param>
        public virtual void OnNotice(String message) {}

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Request user to confirm
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>Whether it's confirmed</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 输出确认消息，并返回是否确认
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>是否确认</returns>
        public virtual bool OnConfirm(String message) { return true; }

        /// \~English
        /// <summary>
        /// (api:app=2.3.3) [Optional] Output log message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="level">Log level</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.3) [可选实现] 输出清单消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="level">消息级别</param>
        public virtual void OnLog(String message, LogLevel level) {}

        /// \~English
        /// <summary>
        /// Deprecated, implement OnDebugMessage(message, source)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应实现OnDebugMessage(message, source)
        /// </summary>
        public virtual void OnDebugMessage(String message) {}

        /// \~English
        /// <summary>
        /// (api:app=2.11.0) [Optional] Output message for debugging
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="source">Source</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.11.0) [可选实现] 输出调试用消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="source">来源</param>
        public virtual void OnDebugMessage(String message, String source) {}

        /// \~English
        /// <summary>
        /// [Optional] Get application's language
        /// </summary>
        /// <returns>Language code, null or "en" is English, "ch" is Chinese</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取语言
        /// </summary>
        /// <returns>语言代号，null或"en"表示英文，"ch"表示中文</returns>
        public virtual String OnGetAppLanguage() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Get whether to allow multiple application instances running
        /// </summary>
        /// <returns>Whether to allow multiple application instances running</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 返回是否允许多个实例同时运行
        /// </summary>
        /// <returns>是否允许多个实例同时运行</returns>
        public virtual bool OnCheckMultiInstance() { return false; }

        /// \~English
        /// <summary>
        /// (api:app=2.6.10) [Optional] Get whether to disable GPU rendering
        /// </summary>
        /// <returns>Whether to disable GPU rendering</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.10) [可选实现] 返回是否禁用GPU渲染
        /// </summary>
        /// <returns>是否禁用GPU渲染</returns>
        public virtual bool OnCheckDisableGPURendering() { return false; }

        /// \~English
        /// <summary>
        /// (api:app=2.6.13) [Optional] Get whether to enable GPU onscreen rendering
        /// </summary>
        /// <returns>Whether to enable GPU onscreen rendering (always false if GPU rendering is disabled)</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.13) [可选实现] 返回是否启用GPU在屏渲染
        /// </summary>
        /// <returns>是否启用GPU在屏渲染（若已禁用GPU渲染则无效）</returns>
        public virtual bool OnCheckEnableOnscreenGPURendering() { return false; }

        /// \~English
        /// <summary>
        /// (api:app=2.13.0) [Optional] Get whether to disable GPU decoding
        /// </summary>
        /// <returns>Whether to disable GPU decoding</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.13.0) [可选实现] 返回是否禁用GPU解码
        /// </summary>
        /// <returns>是否禁用GPU解码</returns>
        public virtual bool OnCheckDisableGPUDecoding() { return false; }

        /// \~English
        /// <summary>
        /// (api:app=2.13.0) [Optional] Get whether to enable GPU specialized decoding
        /// </summary>
        /// <returns>Whether to enable GPU specialized decoding (always false if GPU decoding is disabled)</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.13.0) [可选实现] 返回是否启用GPU专用解码
        /// </summary>
        /// <returns>是否启用GPU专用解码（若已禁用GPU解码则无效）</returns>
        public virtual bool OnCheckEnableSpecializedGPUDecoding() { return false; }

        /// \~English
        /// <summary>
        /// (api:app=2.14.0) [Optional] Get whether to use PRC web service
        /// </summary>
        /// <returns>Whether to use PRC web service</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.14.0) [可选实现] 返回是否使用境内网络服务
        /// </summary>
        /// <returns>是否使用境内网络服务</returns>
        public virtual bool OnCheckPreferPRCWeb() { return true; }

        /// \~English
        /// <summary>
        /// (api:app=2.14.1) [Optional] Get whether to request auto root privilege (Only for Linux)
        /// </summary>
        /// <returns>Whether to request auto root privilege</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.14.1) [可选实现] 返回是否请求自动root权限 (仅针对Linux操作系统)
        /// </summary>
        /// <returns>是否请求自动root权限</returns>
        public virtual bool OnCheckRequestAutoRoot() { return false; }

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Select (multiple) bus protocols
        /// </summary>
        /// <param name="selected">Bus protocols already selected</param>
        /// <returns>Newly selected bus protocols</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 选择总线协议文件（可多个）
        /// </summary>
        /// <param name="selected">已选择的总线协议文件</param>
        /// <returns>新选择的总线协议文件</returns>
        public virtual BusProtocolFileID[] OnSelectBusProtocolFiles(BusProtocolFileID[] selected) { return null; }

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Select bus message
        /// </summary>
        /// <param name="originMessageID">Initial bus message configuration</param>
        /// <returns>Bus message configuration result, null if requested to delete</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 选择总线报文
        /// </summary>
        /// <param name="originMessageID">初始总线报文配置</param>
        /// <returns>返回总线报文配置，若删除则返回null</returns>
        public virtual String OnSelectBusMessage(String originMessageID) { return originMessageID; }

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Select signal
        /// </summary>
        /// <param name="origin">Initial signal configuration</param>
        /// <param name="withScale">Whether to enable the configuration of value scale</param>
        /// <param name="withSignBit">Whether to enable the configuration of sign bit signal</param>
        /// <param name="unit">The unit to display, only available while the configuration of value scale is enabled</param>
        /// <returns>Signal configuration result, null if requested to delete</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 选择信号
        /// </summary>
        /// <param name="origin">初始信号配置</param>
        /// <param name="withScale">是否包含乘数的配置</param>
        /// <param name="withSignBit">是否包含符号位信号的配置</param>
        /// <param name="unit">该信号的单位显示，仅当包含乘数配置时有效</param>
        /// <returns>返回信号配置，若删除则返回null</returns>
        public virtual SignalConfig OnSelectSignal(SignalConfig origin, bool withScale, bool withSignBit, String unit) { return origin; }

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Select multiple signals at once
        /// </summary>
        /// <param name="handler">Callback to handle signal selection</param>
        /// <param name="existSignalIDList">List of all signal IDs that already exist</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 一次性选择多个信号
        /// </summary>
        /// <param name="handler">选中信号时调用的回调接口</param>
        /// <param name="existSignalIDList">既存的选中信号ID列表</param>
        public virtual void OnSelectSignals(SelectSignalHandler handler, List<String> existSignalIDList) {}

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Deprecated, implement OnRunStandaloneTask(title, taskClassID, basicCallback, detailsCallback)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 已弃用，应实现OnRunStandaloneTask(title, taskClassID, basicCallback, detailsCallback)
        /// </summary>
        public virtual void OnRunStandaloneTask(String title, MainWorkflowTaskCallback callback) {}

        /// \~English
        /// <summary>
        /// (api:app=2.15.9) [Optional][OK for modal] Deprecated, implement OnRunStandaloneTask(title, taskClassID, basicCallback, detailsCallback)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.9) [可选实现][可含模态] 已弃用，应实现OnRunStandaloneTask(title, taskClassID, basicCallback, detailsCallback)
        /// </summary>
        public virtual bool OnRunStandaloneTask(String title, MainWorkflowTaskCallback basicCallback, MainWorkflowTaskIODetailsCallback detailsCallback) { return false; }

        /// \~English
        /// <summary>
        /// (api:app=2.16.2) [Optional][OK for modal] Show running standalone task
        /// </summary>
        /// <param name="title">Title of standalone task</param>
        /// <param name="taskClassID">The standalone task's class ID</param>
        /// <param name="basicCallback">Basic callback interface</param>
        /// <param name="detailsCallback">I/O details getter callback interface</param>
        /// <returns>Always return true, or else the legacy OnRunStandaloneTask will be called</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.16.2) [可选实现][可含模态] 显示正在进行的独立任务
        /// </summary>
        /// <param name="title">独立任务标题</param>
        /// <param name="taskClassID">独立任务类别ID</param>
        /// <param name="basicCallback">基础回调接口</param>
        /// <param name="detailsCallback">获取I/O详情的回调接口</param>
        /// <returns>应固定返回true，否则将继续调用旧版OnRunStandaloneTask</returns>
        public virtual bool OnRunStandaloneTask(String title, String taskClassID, MainWorkflowTaskCallback basicCallback, MainWorkflowTaskIODetailsCallback detailsCallback) { return false; }


        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Open dialog
        /// </summary>
        /// <param name="dialog">Configuration panel object, derived from ConfigPanel</param>
        /// <param name="info">Information of dialog component</param>
        /// <param name="config">Initial configuration string</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 打开对话框
        /// </summary>
        /// <param name="dialog">配置面板对象，继承ConfigPanel</param>
        /// <param name="info">对话框组件信息</param>
        /// <param name="config">对话框初始配置</param>
        public virtual void OnOpenDialog(object dialog, DialogClassInfo info, String config) {}

        /// \~English
        /// <summary>
        /// [Optional] Add window to workspace
        /// </summary>
        /// <param name="window">Window panel object, derived from WindowPanel</param>
        /// <param name="info">Information of window component</param>
        /// <param name="config">Initial configuration string</param>
        /// <param name="newWorkspaceIfNeeded">Whether to add to a new workspace (if available) if there's no space in the current workspace</param>
        /// <returns>Whether successful. After you're done using the panel, call ASEva.Agency.UnregisterPanel</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 添加窗口至工作空间
        /// </summary>
        /// <param name="window">窗口面板对象，继承WindowPanel</param>
        /// <param name="info">窗口组件信息</param>
        /// <param name="config">窗口初始配置</param>
        /// <param name="newWorkspaceIfNeeded">工作空间无足够空间时，是否在新工作空间添加（若不支持工作空间，则无视此选项）</param>
        /// <returns>是否成功添加，若成功添加，在释放窗口时需要调用 ASEva.Agency.UnregisterPanel </returns>
        public virtual bool OnAddWindow(object window, WindowClassInfo info, String config, bool newWorkspaceIfNeeded) { return false; }

        /// \~English
        /// <summary>
        /// (api:app=2.15.0) [Optional][OK for modal] Run console procedure
        /// </summary>
        /// <param name="consoleClass">Console object</param>
        /// <param name="info">Information of console component</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.0) [可选实现][可含模态] 运行控制台过程
        /// </summary>
        /// <param name="consoleClass">控制台对象</param>
        /// <param name="info">控制台组件信息</param>
        public virtual void OnRunConsole(ConsoleClass consoleClass, ConsoleClassInfo info) {}

        /// \~English
        /// <summary>
        /// [Optional] Set current dialog's title and icon
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="icon">Icon (size should be 16x16)</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 设置当前对话框的标题和图标
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="icon">图标，分辨率为16x16</param>
        public virtual void OnSetCurrentDialogTitle(String title, CommonImage icon) {}

        /// \~English
        /// <summary>
        /// [Optional] Set window's title and icon
        /// </summary>
        /// <param name="window">Window panel object, derived from WindowPanel</param>
        /// <param name="title">Title</param>
        /// <param name="icon">Icon (size should be 16x16)</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 设置窗口的标题和图标
        /// </summary>
        /// <param name="window">窗口面板对象，继承WindowPanel</param>
        /// <param name="title">标题</param>
        /// <param name="icon">图标，分辨率为16x16</param>
        public virtual void OnSetWindowTitle(object window, String title, CommonImage icon) {}

        /// \~English
        /// <summary>
        /// [Optional] Notify to reset data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 通知重置数据
        /// </summary>
        public virtual void OnResetData() {}

        /// \~English
        /// <summary>
        /// [Optional] Input new data
        /// </summary>
        /// <param name="data">New data</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 输入新数据
        /// </summary>
        /// <param name="data">新数据</param>
        public virtual void OnInputData(object data) { }

        /// \~English
        /// <summary>
        /// [Optional] Notify that the session is starting
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 通知正在开始session
        /// </summary>
        public virtual void OnStartSession() { }

        /// \~English
        /// <summary>
        /// [Optional] Notify that the session is stopping
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 通知正在结束session
        /// </summary>
        public virtual void OnStopSession() { }

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Edit the information of lately recorded session
        /// </summary>
        /// <param name="recordSessionID">Lately recorded session's ID</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 编辑新采集的session信息
        /// </summary>
        /// <param name="recordSessionID">新采集的session ID</param>
        public virtual void OnEditRecordedSession(DateTime recordSessionID) { }

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Configure path of offline map file
        /// </summary>
        /// <param name="originPath">The original path, null if not configured</param>
        /// <returns>New path, set to null if not configured</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 编辑离线地图文件路径
        /// </summary>
        /// <param name="originPath">原始路径，若未设置则为null</param>
        /// <returns>新路径，若未设置则为null</returns>
        public virtual String OnEditOfflineMapPath(String originPath) { return originPath; }

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Configure data encryption
        /// </summary>
        /// <param name="callback">Framework callback interface</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 编辑数据加密选项
        /// </summary>
        /// <param name="callback">框架软件的回调接口</param>
        public virtual void OnEditDataEncryption(MainWorkflowEncryptionCallback callback) {}

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Install plugins
        /// </summary>
        /// <param name="libs">Information of related library files</param>
        /// <param name="drivers">Information of related driver or environment pack</param>
        /// <param name="callback">Framework callback interface</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 安装插件
        /// </summary>
        /// <param name="libs">插件关联的库信息列表</param>
        /// <param name="drivers">插件关联的驱动和环境信息列表</param>
        /// <param name="callback">框架软件的回调接口</param>
        public virtual void OnInstallPlugin(InstallPluginLibraryInfo[] libs, InstallPluginDriverInfo[] drivers, MainWorkflowInstallCallback callback) {}

        /// \~English
        /// <summary>
        /// [Optional] Notify that a new project is being loading
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 通知正在新建工程项目
        /// </summary>
        public virtual void OnNewProject() {}

        /// \~English
        /// <summary>
        /// [Optional] Notify that a project is loaded
        /// </summary>
        /// <param name="xml">XML of the loaded project</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 通知新加载的工程项目
        /// </summary>
        /// <param name="xml">工程项目xml</param>
        public virtual void OnLoadProject(XmlDocument xml) {}

        /// \~English
        /// <summary>
        /// [Optional] Notify that the current project is being saved
        /// </summary>
        /// <param name="xml">XML of the current project</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 通知正在保存工程项目
        /// </summary>
        /// <param name="xml">工程项目xml</param>
        public virtual void OnSaveProject(XmlDocument xml) {}

        /// \~English
        /// <summary>
        /// [Optional] Get whether it's allowed by main workflow to save project, start session, etc.
        /// </summary>
        /// <returns>Whether it's allowed by main workflow to save project, start session, etc.</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 返回主流程端是否允许进行保存工程项目和开始session等操作
        /// </summary>
        /// <returns>主流程端是否允许进行保存工程项目和开始session等操作</returns>
        public virtual bool OnCheckReady() { return true; }

        /// \~English
        /// <summary>
        /// [Optional][OK for modal] Notify that the application is exiting
        /// </summary>
        /// <param name="force">Whether forced to exist</param>
        /// <returns>Whether it's OK to exist (no use while forced to exist)</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现][可含模态] 通知正在退出应用程序
        /// </summary>
        /// <param name="force">是否为强制结束</param>
        /// <returns>返回是否可退出，强制结束时将不起作用</returns>
        public virtual bool OnExiting(bool force) { return true; }

        /// \~English
        /// <summary>
        /// [Required] Exit main workflow
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 退出主流程
        /// </summary>
        public virtual void OnExit() {}
    }

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) Application's basic info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 应用程序的基本信息
    /// </summary>
    public class AppBasicInfo
    {
        /// \~English
        /// <summary>
        /// Application name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public String AppName { get; set; }

        /// \~English
        /// <summary>
        /// Application's icon, which is PNG binary data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 应用程序的图标，为PNG文件二进制数据
        /// </summary>
        public byte[] AppIconPNG { get; set; }

        /// \~English
        /// <summary>
        /// Application's version
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 应用程序版本
        /// </summary>
        public Version AppVersion { get; set; }

        /// \~English
        /// <summary>
        /// Application's release revision, which can be null (Applications with the same version can be distributed in different configurations)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 应用程序发行号，可为空（相同应用程序版本下可按不同配置发行）
        /// </summary>
        public String AppRevision { get; set; }

        /// \~English
        /// <summary>
        /// (api:app=2.6.20) Application's release revision in short, which can be null (Applications with the same version can be distributed in different configurations)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.20) 缩略的应用程序发行号，可为空（相同应用程序版本下可按不同配置发行）
        /// </summary>
        public String AppRevisionShort { get; set; }

        /// \~English
        /// <summary>
        /// Framework's version
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 框架软件的版本
        /// </summary>
        public Version CoreVersion { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) The reason for requesting license
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 许可证请求原因
    /// </summary>
    public enum LicenseRequestReason
    {
        /// \~English
        /// <summary>
        /// Can't find valid license
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未找到有效许可证
        /// </summary>
        InvalidLicense = 1,

        /// \~English
        /// <summary>
        /// Machine code is wrong
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 当前许可证机器码错误
        /// </summary>
        WrongMAC = 2,

        /// \~English
        /// <summary>
        /// Framework version unsupported
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 当前许可证版本不支持
        /// </summary>
        WrongVersion = 3,

        /// \~English
        /// <summary>
        /// Current license is expired
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 当前许可证已过期
        /// </summary>
        Expired = 4,
    }

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) Initialization result of framework software
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 框架软件的初始化结果
    /// </summary>
    public enum CoreInitResult
    {
        /// \~English
        /// <summary>
        /// Success
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 成功
        /// </summary>
        OK = 0,

        /// \~English
        /// <summary>
        /// Failed to load library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 加载库失败
        /// </summary>
        LoadLibraryFailed = 1,

        /// \~English
        /// <summary>
        /// Invalid component included
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 含非法或失效组件
        /// </summary>
        InvalidComponent = 2,

        /// \~English
        /// <summary>
        /// Failed to validate license
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 许可证验证失败
        /// </summary>
        LicenseFailed = 3,

        /// \~English
        /// <summary>
        /// Failed to install component
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 安装组件失败
        /// </summary>
        InstallComponentFailed = 4,
    }

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) Loop callback interface used by main workflow
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 在主流程中使用的主循环回调接口
    /// </summary>
    public interface MainWorkflowLoopCallback
    {
        /// \~English
        /// <summary>
        /// Run framework's main loop function
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 执行框架软件中的主循环程序
        /// </summary>
        void OnLoop();
    }

    /// \~English
    /// <summary>
    /// (api:app=2.10.1) Modal callback interface used by main workflow
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.10.1) 在主流程中使用的模态对话回调接口
    /// </summary>
    public interface MainWorkflowModalCallback
    {
        /// \~English
        /// <summary>
        /// Run framework's functions that may be modal
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 执行框架软件中可能产生模态对话的程序
        /// </summary>
        void OnHandleModal();
    }

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) License validation callback used by main workflow
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 在主流程中使用的许可证验证回调接口
    /// </summary>
    public interface MainWorkflowLicenseCallback
    {
        /// \~English
        /// <summary>
        /// Revalidate a new license
        /// </summary>
        /// <param name="license">Base64 string of license, set to null to abort</param>
        /// <param name="pluginCode">Base64 string of plugin code</param>
        /// \~Chinese
        /// <summary>
        /// 重新验证许可证
        /// </summary>
        /// <param name="license">许可证的Base64字符串，设为null表示放弃验证</param>
        /// <param name="pluginCode">插件码的Base64字符串</param>
        void OnRevalidateLicense(String license, String pluginCode);
    }

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) Standalone task callback interface used my main workflow
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 在主流程中使用的获取独立任务状态回调接口
    /// </summary>
    public interface MainWorkflowTaskCallback
    {
        /// \~English
        /// <summary>
        /// Get task's state
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取独立任务状态
        /// </summary>
        TaskState GetTaskState();

        /// \~English
        /// <summary>
        /// Get description of task's current state
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取当前状态描述
        /// </summary>
        String GetTaskStateDescription();

        /// \~English
        /// <summary>
        /// Get progress, in percentages
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取任务进度，单位百分比
        /// </summary>
        double GetTaskProgress();

        /// \~English
        /// <summary>
        /// Cancel the task
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 取消任务
        /// </summary>
        void CancelTask();
    }

    /// \~English
    /// <summary>
    /// (api:app=2.15.9) Task I/O details getter callback interface used my main workflow
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.15.9) 在主流程中使用的获取独立任务I/O详情的回调接口
    /// </summary>
    public interface MainWorkflowTaskIODetailsCallback
    {
        /// \~English
        /// <summary>
        /// Get task config
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取任务配置
        /// </summary>
        String GetTaskConfig();

        /// \~English
        /// <summary>
        /// Get task return value
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取任务返回值
        /// </summary>
        String GetTaskReturnValue();
    }

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) Data encryption callback used by main workflow
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 在主流程中使用的配置数据加密选项的回调接口
    /// </summary>
    public interface MainWorkflowEncryptionCallback
    {
        /// \~English
        /// <summary>
        /// Try to unlock
        /// </summary>
        /// <param name="key">The old password, set to null to use default password</param>
        /// <returns>Whether successful to unlock</returns>
        /// \~Chinese
        /// <summary>
        /// 尝试解锁
        /// </summary>
        /// <param name="key">旧密码，空表示使用默认密码</param>
        /// <returns>是否成功解锁</returns>
        bool UnlockDataKey(String key);

        /// \~English
        /// <summary>
        /// Update password
        /// </summary>
        /// <param name="key">New password, set to null to use default password</param>
        /// \~Chinese
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="key">新密码，空表示使用默认密码</param>
        void UpdateDataKey(String key);
    }

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) Plugin installation callback used by main workflow
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 在主流程中使用的安装插件的回调接口
    /// </summary>
    public interface MainWorkflowInstallCallback
    {
        /// \~English
        /// <summary>
        /// Install library files
        /// </summary>
        /// <param name="libraryID">Related library ID</param>
        /// \~Chinese
        /// <summary>
        /// 安装库文件
        /// </summary>
        /// <param name="libraryID">插件关联的库ID</param>
        void InstallLibrary(String libraryID);

        /// \~English
        /// <summary>
        /// Prepare driver or environment pack, and return the executable path for installation
        /// </summary>
        /// <param name="driverID">Related driver ID</param>
        /// <returns>Executable path for installation</returns>
        /// \~Chinese
        /// <summary>
        /// 准备驱动和环境等的文件，并返回安装文件的运行路径
        /// </summary>
        /// <param name="driverID">插件关联的驱动ID</param>
        /// <returns>安装文件的运行路径</returns>
        String PrepareDriver(String driverID);
    }
}