using System;
using System.Collections.Generic;
using System.Xml;
using ASEva.Samples;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.3.0) 主流程基类
    /// </summary>
    public class MainWorkflow
    {
        /// <summary>
        /// [可选实现][可含模态框] 初始化主流程
        /// </summary>
        /// <param name="appID">应用程序ID</param>
        /// <param name="parameters">初始化参数</param>
        /// <param name="uiCode">输出UI框架代号，若不使用UI则输出null</param>
        /// <returns>初始化是否成功</returns>
        public virtual bool OnInit(String appID, Dictionary<String, String> parameters, out String uiCode) { uiCode = null; return true; }

        /// <summary>
        /// [必须实现][可含模态框] 运行主流程，需要在其中主循环中确保执行了 ASEva.CoreCallback.OnLoop
        /// </summary>
        /// <param name="callback">框架软件回调接口</param>
        public virtual void OnRun(MainWorkflowLoopCallback callback) {}

        /// <summary>
        /// [可选实现] 通知应用程序的基本信息
        /// </summary>
        /// <param name="info">应用程序的基本信息</param>
        public virtual void OnAppBasicInfo(AppBasicInfo info) {}

        /// <summary>
        /// [可选实现][可含模态框] 许可证验证失败后发起新的请求
        /// </summary>
        /// <param name="reason">验证失败原因</param>
        /// <param name="mac">机器码</param>
        /// <param name="callback">框架软件的回调接口</param>
        /// <returns>是否接受请求，如接受需稍后调用 ASEva.CoreCallback.OnRevalidateLicense </returns>
        public virtual bool OnLicenseRequest(LicenseRequestReason reason, String mac, MainWorkflowLicenseCallback callback) { return false; }

        /// <summary>
        /// [可选实现][可含模态框] 通知框架软件的初始化结果
        /// </summary>
        /// <param name="result">框架软件的初始化结果</param>
        /// <param name="revisionUpdated">是否更新了发行号</param>
        public virtual void OnCoreInitResult(CoreInitResult result, bool revisionUpdated) {}

        /// <summary>
        /// [可选实现][可含模态框] 输出错误消息
        /// </summary>
        /// <param name="message">消息</param>
        public virtual void OnError(String message) {}

        /// <summary>
        /// [可选实现][可含模态框] 输出一般消息
        /// </summary>
        /// <param name="message">消息</param>
        public virtual void OnNotice(String message) {}

        /// <summary>
        /// [可选实现][可含模态框] 输出确认消息，并返回是否确认
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>是否确认</returns>
        public virtual bool OnConfirm(String message) { return true; }

        /// <summary>
        /// [可选实现] 输出调试用消息
        /// </summary>
        /// <param name="message">消息</param>
        public virtual void OnDebugMessage(String message) {}

        /// <summary>
        /// [可选实现] 获取语言
        /// </summary>
        /// <returns>语言代号，null或"en"表示英文，"ch"表示中文</returns>
        public virtual String OnGetAppLanguage() { return null; }

        /// <summary>
        /// [可选实现] 返回是否允许多个实例同时运行
        /// </summary>
        /// <returns>是否允许多个实例同时运行</returns>
        public virtual bool OnCheckMultiInstance() { return false; }

        /// <summary>
        /// [可选实现][可含模态框] 选择总线协议文件
        /// </summary>
        /// <param name="selected">已选择的总线协议文件</param>
        /// <returns>新选择的总线协议文件</returns>
        public virtual BusProtocolFileID[] OnSelectBusProtocolFiles(BusProtocolFileID[] selected) { return null; }

        /// <summary>
        /// [可选实现][可含模态框] 选择总线报文
        /// </summary>
        /// <param name="originMessageID">原报文ID</param>
        /// <returns>新报文ID</returns>
        public virtual String OnSelectBusMessage(String originMessageID) { return originMessageID; }

        /// <summary>
        /// [可选实现][可含模态框] 选择信号
        /// </summary>
        /// <param name="origin">原信号配置</param>
        /// <param name="withScale">是否需要配置scale</param>
        /// <param name="withSignBit">是否需要配置符号位信号</param>
        /// <param name="unit">单位</param>
        /// <returns>新信号配置</returns>
        public virtual SignalConfig OnSelectSignal(SignalConfig origin, bool withScale, bool withSignBit, String unit) { return origin; }

        /// <summary>
        /// [可选实现][可含模态框] 一次性选择多个信号
        /// </summary>
        /// <param name="handler">新选择信号后的回调函数</param>
        /// <param name="existSignalIDList">已选择的信号ID列表</param>
        public virtual void OnSelectSignals(SelectSignalHandler handler, List<String> existSignalIDList) {}

        /// <summary>
        /// [可选实现][可含模态框] 显示正在进行的独立任务
        /// </summary>
        /// <param name="title">独立任务标题</param>
        /// <param name="callback">框架软件的回调接口</param>
        public virtual void OnRunStandaloneTask(String title, MainWorkflowTaskCallback callback) {}

        /// <summary>
        /// [可选实现][可含模态框] 打开对话框
        /// </summary>
        /// <param name="dialog">对话框对象，继承ConfigPanel</param>
        /// <param name="info">对话框组件信息</param>
        /// <param name="config">对话框初始配置</param>
        public virtual void OnOpenDialog(object dialog, DialogClassInfo info, String config) {}

        /// <summary>
        /// [可选实现] 添加窗口至工作空间
        /// </summary>
        /// <param name="window">窗口对象，继承WindowPanel</param>
        /// <param name="info">窗口组件信息</param>
        /// <param name="config">窗口初始配置</param>
        /// <param name="newWorkspaceIfNeeded">工作空间无足够空间时，是否在新工作空间添加（若不支持工作空间，则无视此选项）</param>
        /// <returns>是否成功添加，若成功添加，在释放窗口时需要调用 ASEva.Agency.UnregisterPanel </returns>
        public virtual bool OnAddWindow(object window, WindowClassInfo info, String config, bool newWorkspaceIfNeeded) { return false; }

        /// <summary>
        /// [可选实现] 设置当前对话框的标题和图标
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="icon">图标，分辨率为16x16</param>
        public virtual void OnSetCurrentDialogTitle(String title, CommonImage icon) {}

        /// <summary>
        /// [可选实现] 设置窗口的标题和图标
        /// </summary>
        /// <param name="window">窗口对象，继承WindowPanel</param>
        /// <param name="title">标题</param>
        /// <param name="icon">图标，分辨率为16x16</param>
        public virtual void OnSetWindowTitle(object window, String title, CommonImage icon) {}

        /// <summary>
        /// [可选实现] 通知重置数据
        /// </summary>
        public virtual void OnResetData() {}

        /// <summary>
        /// [可选实现] 输入新数据
        /// </summary>
        /// <param name="data">新数据</param>
        public virtual void OnInputData(object data) { }

        /// <summary>
        /// [可选实现] 通知正在开始session
        /// </summary>
        public virtual void OnStartSession() { }

        /// <summary>
        /// [可选实现] 通知正在结束session
        /// </summary>
        public virtual void OnStopSession() { }

        /// <summary>
        /// [可选实现][可含模态框] 编辑新采集的session信息
        /// </summary>
        /// <param name="recordSessionID">新采集的session ID</param>
        public virtual void OnEditRecordedSession(DateTime recordSessionID) { }

        /// <summary>
        /// [可选实现][可含模态框] 编辑离线地图文件路径
        /// </summary>
        /// <param name="originPath">原始路径，若未设置则为null</param>
        /// <returns>新路径，若未设置则为null</returns>
        public virtual String OnEditOfflineMapPath(String originPath) { return originPath; }

        /// <summary>
        /// [可选实现][可含模态框] 编辑数据加密选项
        /// </summary>
        /// <param name="callback">框架软件的回调接口</param>
        public virtual void OnEditDataEncryption(MainWorkflowEncryptionCallback callback) {}

        /// <summary>
        /// [可选实现] 通知正在新建工程项目
        /// </summary>
        public virtual void OnNewProject() {}

        /// <summary>
        /// [可选实现] 通知新加载的工程项目
        /// </summary>
        /// <param name="xml">工程项目xml</param>
        public virtual void OnLoadProject(XmlDocument xml) {}

        /// <summary>
        /// [可选实现] 通知正在保存工程项目
        /// </summary>
        /// <param name="xml">工程项目xml</param>
        public virtual void OnSaveProject(XmlDocument xml) {}

        /// <summary>
        /// [可选实现] 返回是否允许进行保存工程项目和开始session等操作
        /// </summary>
        /// <returns>是否允许进行保存工程项目和开始session等操作</returns>
        public virtual bool OnCheckReady() { return false; }

        /// <summary>
        /// [可选实现][可含模态框] 通知正在退出应用程序
        /// </summary>
        /// <param name="force">是否为强制结束</param>
        /// <returns>返回是否可退出，强制结束时将不起作用</returns>
        public virtual bool OnExiting(bool force) { return true; }

        /// <summary>
        /// [必须实现] 退出主流程
        /// </summary>
        public virtual void OnExit() {}
    }

    /// <summary>
    /// (api:app=2.3.0) 应用程序的基本信息
    /// </summary>
    public class AppBasicInfo
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public String AppName { get; set; }

        /// <summary>
        /// 应用程序的图标，为PNG文件二进制数据
        /// </summary>
        public byte[] AppIconPNG { get; set; }

        /// <summary>
        /// 应用程序版本
        /// </summary>
        public Version AppVersion { get; set; }

        /// <summary>
        /// 应用程序发行号，可为空（相同应用程序版本下的不同插件配置）
        /// </summary>
        public String AppRevision { get; set; }

        /// <summary>
        /// 框架软件的版本
        /// </summary>
        public Version CoreVersion { get; set; }
    }

    /// <summary>
    /// (api:app=2.3.0) 许可证请求原因
    /// </summary>
    public enum LicenseRequestReason
    {
        /// <summary>
        /// 未找到有效许可证
        /// </summary>
        InvalidLicense = 1,

        /// <summary>
        /// 当前许可证机器码错误
        /// </summary>
        WrongMAC = 2,

        /// <summary>
        /// 当前许可证版本不支持
        /// </summary>
        WrongVersion = 3,

        /// <summary>
        /// 当前许可证已过期
        /// </summary>
        Expired = 4,
    }

    /// <summary>
    /// (api:app=2.3.0) 框架软件的初始化结果
    /// </summary>
    public enum CoreInitResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        OK = 0,

        /// <summary>
        /// 加载库失败
        /// </summary>
        LoadLibraryFailed = 1,

        /// <summary>
        /// 含非法或失效组件
        /// </summary>
        InvalidComponent = 2,

        /// <summary>
        /// 许可证验证失败
        /// </summary>
        LicenseFailed = 3,

        /// <summary>
        /// 安装组件失败
        /// </summary>
        InstallComponentFailed = 4,
    }

    /// <summary>
    /// (api:app=2.3.0) 在主流程中使用的主循环回调接口
    /// </summary>
    public interface MainWorkflowLoopCallback
    {
        /// <summary>
        /// 执行框架软件的主循环函数
        /// </summary>
        void OnLoop();
    }

    /// <summary>
    /// (api:app=2.3.0) 在主流程中使用的许可证验证回调接口
    /// </summary>
    public interface MainWorkflowLicenseCallback
    {
        /// <summary>
        /// 重新验证许可证
        /// </summary>
        /// <param name="license">许可证的Base64字符串，设为null表示放弃验证</param>
        /// <param name="pluginCode">插件码的Base64字符串</param>
        void OnRevalidateLicense(String license, String pluginCode);
    }

    /// <summary>
    /// (api:app=2.3.0) 在主流程中使用的获取独立任务状态回调接口
    /// </summary>
    public interface MainWorkflowTaskCallback
    {
        /// <summary>
        /// 获取独立任务状态
        /// </summary>
        TaskState GetTaskState();

        /// <summary>
        /// 获取当前状态描述
        /// </summary>
        String GetTaskStateDescription();

        /// <summary>
        /// 获取任务进度，单位百分比
        /// </summary>
        double GetTaskProgress();

        /// <summary>
        /// 取消任务
        /// </summary>
        void CanelTask();
    }

    /// <summary>
    /// (api:app=2.3.0) 在主流程中使用的配置数据加密选项的回调接口
    /// </summary>
    public interface MainWorkflowEncryptionCallback
    {
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="key">旧密码，空表示使用默认密码</param>
        /// <returns>是否成功解锁</returns>
        bool UnlockDataKey(String key);

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="key">新密码，空表示使用默认密码</param>
        void UpdateDataKey(String key);
    }
}