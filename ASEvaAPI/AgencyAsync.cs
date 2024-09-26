using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASEva.Samples;
using ASEva.Utility;

namespace ASEva
{
    #pragma warning disable CS1571

    public interface AgencyAsyncHandler
    {
        bool SyncMode { get; }
        Task AddDataLayer(String layer);
        Task AddSignalReference(String signalID);
        Task<TimeWithSession> ConvertTimeIntoSession(double timeline);
        Task DeleteDataLayer(String layer);
        Task<byte[][]> DequeueDataFromNative(object caller, String nativeClassID, String dataID);
        Task DisableAllConfigs();
        Task DisableAllPlugins();
        Task DisableModule(object caller, String classID);
        Task DisablePlugin(String packID);
        Task EnablePlugin(String packID);
        Task EnqueueDataToNative(object caller, String nativeClassID, String dataID, byte[] data);
        Task<string[]> GetAllChannelGuestSyncKeys();
        Task<string[]> GetAllChannelMonitoringKeys();
        Task<string[]> GetAllChannelServerSyncMonitoringKeys();
        Task<Dictionary<String, DeviceStatusDetail>> GetAllDeviceStatus();
        Task<Dictionary<string, double>> GetAllRawChannelDelayConfigs();
        Task<ApplicationGUI> GetAppGUI();
        Task<String> GetAppID();
        Task<Language> GetAppLanguage();
        Task<ApplicationMode> GetAppMode();
        Task<ApplicationStatus> GetAppStatus();
        Task<double> GetAudioChannelDelayConfig();
        Task<(bool, double[], double[])> GetAudioChannelStatus(uint? toleranceMillisecond);
        Task<AudioDriverInfo[]> GetAudioDrivers();
        Task<AudioDeviceInfo[]> GetAudioRecordDevices(String driverID);
        Task<AudioDeviceInfo[]> GetAudioReplayDevices(String driverID);
        Task<int[]> GetAvailableBusChannels();
        Task<String[]> GetAvailableRawChannels();
        Task<String[]> GetAvailableSampleChannels();
        Task<int[]> GetAvailableVideoChannels();
        Task<BufferRange> GetBufferRange();
        Task<double> GetBusChannelDelayConfig(int channel);
        Task<BusChannelInfo[]> GetBusChannelsInfo(SessionIdentifier session);
        Task<bool> GetBusChannelStatus(int channel/* 1~16 */, uint? toleranceMillisecond);
        Task<Dictionary<BusDeviceID, BusDeviceInfo>> GetBusDevices();
        Task<float> GetBusMessageFPS(int channel, uint localID);
        Task<BusMessageInfo> GetBusMessageInfoByLocalID(int channel, uint localID);
        Task<BusMessageInfo> GetBusMessageInfo(String busMessageID);
        Task<double?> GetBusPayloadPercentage(int channel);
        Task<int?> GetBusProtocolFileChannel(String protocolName);
        Task<BusProtocolFileID[]> GetBusProtocolFileIDList();
        Task<BusProtocolFileState> GetBusProtocolFileState(BusProtocolFileID fileID);
        Task<BusSignalInfo> GetBusSignalInfo(String busSignalID);
        Task<String> GetChannelAliasName(String channelID);
        Task<Dictionary<String, String>> GetChannelAliasTable();
        Task<bool> GetChannelGuestSyncFlag(String id);
        Task<Timestamp[]> GetChannelLatestTimestamps(String channelID);
        Task<bool> GetChannelMonitoringFlag(String id);
        Task<bool> GetChannelServerSyncMonitoringFlag(String id);
        Task<Dictionary<String, bool>> GetChannelStatusTable(uint? tolerance);
        Task<Dictionary<String, TimeOffsetSync>> GetChannelSyncTable();
        Task<GeneralDeviceStatus[]> GetChildDeviceStatus(String id);
        Task<ConsoleClassInfo> GetConsoleClassInfo(String consoleClassID);
        Task<Dictionary<string, ConsoleClassInfo>> GetConsoleClassTable();
        Task<(ConfigStatus, ConfigStatus[])> GetConsoleRelatedModulesConfigStatus(String consoleClassID);
        Task<ulong> GetCPUTick();
        Task<ulong> GetCPUTicksPerSecond();
        Task<double> GetCPUTime();
        Task<CPUTimeModel> GetCPUTimeModel(SessionIdentifier session);
        Task<String> GetCurrentDataGeneration();
        Task<String> GetCurrentDataLayer();
        Task<SessionIdentifier?> GetCurrentOnlineSession();
        Task<String> GetCurrentSessionGUID();
        Task<String[]> GetDataLayers();
        Task<Dictionary<String, DeviceClassInfo>> GetDeviceClassTable();
        Task<GeneralDeviceStatus> GetDeviceStatus(String id);
        Task<(ConfigStatus, ConfigStatus[])> GetDialogRelatedModulesConfigStatus(String dialogClassID, String transformID);
        Task<object[]> GetEventHandles();
        Task<EventInfo> GetEventInfo(object eventHandle);
        Task<String[]> GetEventTypeNames();
        Task<SessionIdentifier[]> GetFilteredSessionList();
        Task<double> GetFilteredSessionListTotalLength();
        Task<SessionIdentifier[]> GetFinishedSessions(String generation);
        Task<Dictionary<String, String>> GetFrameworkThirdPartyNotices();
        Task<String[]> GetGenerationList();
        Task<GenerationProcessStatus?> GetGenerationProcessStatus(SessionIdentifier session, String generation);
        Task<SessionIdentifier[]> GetGenerationSessions(String generationID);
        Task<String> GetGlobalParameter(String key, String defaultValue);
        Task<String[]> GetGlobalParameterKeys();
        Task<String> GetGlobalVariable(String key, String defaultValue);
        Task<String[]> GetGlobalVariableKeys();
        Task<PosixTimeModel> GetGNSSPosixTimeModel(SessionIdentifier session);
        Task<GPUDecoderTestResults> GetGPUDecoderTestResults();
        Task<GraphData> GetGraphData(SessionIdentifier session, int graphID);
        Task<GraphicCardInfo[]> GetGraphicCardInfos();
        Task<int[]> GetGraphIDList();
        Task<int?> GetGraphIDWithTitle(String title);
        Task<String> GetGraphTitle(int graphID);
        Task<PosixTimeModel> GetHostPosixTimeModel(SessionIdentifier session);
        Task<double> GetInterestTarget();
        Task<double> GetInterestTime();
        Task<DateTime?> GetInterestTimestamp();
        Task<DateTime?> GetInternetNTPTime();
        Task<int[]> GetLicensedFunctionIndices();
        Task<String> GetLicenseInfo();
        Task<DateTime?> GetLocalDateTime(SessionIdentifier session, double timeOffset, bool useGNSS);
        Task<LogMessage[]> GetLogMessages();
        Task<String[]> GetManualTriggerNames();
        Task<String> GetManualTriggerName(int index);
        Task<ConfigStatus[]> GetModuleChildConfigStatus(object caller, String classID);
        Task<String> GetModuleConfig(object caller, String classID);
        Task<(ConfigStatus, String)> GetModuleConfigStatus(object caller, String classID);
        Task<ModuleDetails> GetModuleDetails(String classID);
        Task<Dictionary<String, NativeClassInfo>> GetNativeClassTable();
        Task<Dictionary<String, Version>> GetNativePluginVersions(NativeLibraryType type);
        Task<Dictionary<String, String>> GetPluginGuestSyncTable();
        Task<String[]> GetPluginPackIDList();
        Task<PluginPackInfo> GetPluginPackInfo(String packID);
        Task<Dictionary<String, Dictionary<String, String>>> GetPluginThirdPartyNotices();
        Task<(byte[], Timestamp?, CameraInfo)> GetPreviewJpeg(int channel, double timeline, double maxGap);
        Task<Dictionary<String, ProcessorClassInfo>> GetProcessorClassTable();
        Task<double> GetRawChannelDelayConfig(String id);
        Task<bool> GetRawChannelStatus(String channelID, uint? toleranceMillisecond);
        Task<(bool, double[], double[])> GetSampleChannelStatus(String channelID, uint? toleranceMillisecond);
        Task<List<String>> GetSampleTitle(String channelID);
        Task<String[]> GetSceneIDList();
        Task<Dictionary<String, SceneTitle>> GetSceneTitleTable();
        Task<String> GetSessionComment(SessionIdentifier session);
        Task<Dictionary<SessionIdentifier, SessionFilterFlags>> GetSessionFilterTable();
        Task<String> GetSessionFolderName(SessionIdentifier session);
        Task<String[]> GetSessionGenerations(SessionIdentifier session);
        Task<bool> GetSessionHostSync(SessionIdentifier session);
        Task<String> GetSessionLayer(SessionIdentifier session);
        Task<double?> GetSessionLength(SessionIdentifier session);
        Task<SessionIdentifier[]> GetSessionList();
        Task<double> GetSessionListTotalLength();
        Task<Dictionary<String, String>> GetSessionProperties(SessionIdentifier session);
        Task<String> GetSessionSearchKey();
        Task<double?> GetSessionTimeline(SessionIdentifier session);
        Task<String> GetSignalName(String signalID, bool fullName);
        Task<String[]> GetSignalNamesOfBusMessage(String messageID);
        Task<SignalTreeNode[]> GetSignalTree();
        Task<String> GetSystemStatus(SystemStatus status);
        Task<String> GetSystemStatusDetails(SystemStatus status);
        Task<TaskClassInfo> GetTaskClassInfo(String taskClassID);
        Task<Dictionary<String, TaskClassInfo>> GetTaskClassTable();
        Task<DateTime?> GetUTCDateTime(SessionIdentifier session, double timeOffset, bool useGNSS);
        Task<Dictionary<String, Version>> GetVersionTable();
        Task<double> GetVideoChannelDelayConfig(int channel);
        Task<VideoChannelInfo[]> GetVideoChannelsInfo(SessionIdentifier session);
        Task<(bool, double[], double[])> GetVideoChannelStatus(int channel, uint? toleranceMillisecond);
        Task<Dictionary<VideoDeviceID, VideoDeviceInfo>> GetVideoDevices();
        Task<(CommonImage, Timestamp?, CameraInfo)> GetVideoFrameImage(int channel, double timeline, double maxGap, VideoFrameGetMode mode, IntRect? clip, bool withAlpha);
        Task<CommonImage> GetVideoFrameThumbnail(int channel, double timeline, double maxGap, bool withAlpha);
        Task<IntSize?> GetVideoRawSize(int channel, double timeline);
        Task<Samples.SpecialCameraType> GetVideoSpecialType(int channel);
        Task<bool> IsBusMessageBound(string busMessageID);
        Task<bool> IsFileOutputEnabled();
        Task<bool> IsInputChannelAvailable(String channelID);
        Task<bool> IsInternetConnected();
        Task<bool> IsMessageValid(String messageID, bool optional);
        Task<bool> IsPRCWebPreferred();
        Task<(bool, String)> IsReady();
        Task<bool> IsSampleChannelConflict(string channelID);
        Task<bool> IsSignalValid(String signalID, bool optional);
        Task<bool> IsVideoDataAvailable(int channel, uint? tolerance);
        Task<BusSignalValue[]> ParseBusMessage(BusMessageSample busMessage);
        Task PublishData(String dataID, byte[] data);
        Task RefreshGenerations();
        Task RefreshSessions();
        Task RemoveEvent(object eventHandle);
        Task RemoveGeneration(SessionIdentifier session, String genID);
        Task<bool> RemoveSession(SessionIdentifier session, bool force);
        Task RemoveSignalReference(String signalID);
        Task ResetGPUDecoderTestResults();
        Task RunConsole(object caller, string consoleClassID);
        Task<(TaskResult, String)> RunStandaloneTask(object caller, String taskClassID, String config);
        Task SendBusMessage(BusMessage message);
        Task<byte[]> SendBusMessageBound(String messageID, uint? interval);
        Task SendManualTrigger(int channel);
        Task SendRawData(String channelID, double[] values, byte[] binary);
        Task SetAudioChannelDelayConfig(double delay);
        Task SetBusChannelDelayConfig(int channel, double delay);
        Task SetChannelGuestSyncFlag(String id, bool guestSync);
        Task SetChannelMonitoringFlag(String id, bool monitoring);
        Task SetChannelServerSyncMonitoringFlag(String id, bool monitoring);
        Task<bool> SetControlFlag(String controllerName, bool enabled);
        Task SetCurrentDataLayer(String layer);
        Task SetEventComment(object eventHandle, String comment);
        Task SetGlobalParameter(String key, String value);
        Task SetGlobalVariable(String key, String value);
        Task SetInterestTime(double targetTimeline);
        Task SetInterestTimestamp(DateTime targetTimestamp);
        Task SetManualTriggerName(int index, String name);
        Task SetModuleConfig(object caller, String classID, String config);
        Task SetRawChannelDelayConfig(String id, double delay);
        Task SetSessionChecker(SessionIdentifier session, bool check);
        Task SetSessionComment(SessionIdentifier session, String comment);
        Task SetSessionHostSync(SessionIdentifier session, bool hostSync);
        Task SetSessionProperties(SessionIdentifier session, Dictionary<String, String> properties);
        Task SetSessionSearchKeyword(String keyword);
        Task SetTargetReplaySpeed(double speed);
        Task SetVideoChannelDelayConfig(int channel, double delay);
        Task<bool> StartOffline(bool force, bool previewOnly, String genDirName);
        Task<bool> StartOnlineWithController(String controllerName, bool previewOnly);
        Task<bool> StartOnline(bool force, bool previewOnly, String sessionDirName);
        Task<bool> StartRemote(bool force, bool previewOnly, String sessionDirName, ulong startPosixTime);
        Task<bool> StartRemoteWithController(String controllerName, bool previewOnly, ulong startPosixTime);
        Task<bool> StartReplay(bool force, double startTimeline, double? interestTarget);
        Task<bool> StopRunningWithController(String controllerName);
        Task<bool> StopRunning(bool force, bool editRecordedSession);
        Task<DataSubscriber> SubscribeData(String dataID, int bufferLength, int timeout);
        Task<bool> SwitchAppMode(String controllerName, ApplicationMode mode, int waitSecond);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.2.0) Include all main asynchronous APIs 
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.2.0) 集合了所有主要异步API
    /// </summary>
    public class AgencyAsync
    {
        private static AgencyAsyncHandler handler = null;
        public static AgencyAsyncHandler Handler
        {
            private get
            {
                if (handler == null) handler = new AgencyAsyncDefault();
                return handler;
            }
            set
            {
                if (value != null) handler = value;
            }
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.0) Whether the implementations are all synchronous call
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.0) 是否底层实现都为同步调用
        /// </summary>
        public static bool SyncMode
        {
            get { return Handler.SyncMode; }
        }

        /// \~English
        /// <summary>
        /// Add a new data layer
        /// </summary>
        /// <param name="layer">Data layer</param>
        /// \~Chinese
        /// <summary>
        /// 添加新的数据层级
        /// </summary>
        /// <param name="layer">数据层级</param>
        public static Task AddDataLayer(String layer)
        {
            return Handler.AddDataLayer(layer);
        }

        /// \~English
        /// <summary>
        /// Add signal reference (only signals with references will be sent to app layer)
        /// </summary>
        /// <param name="signalID">Signal ID</param>
        /// \~Chinese
        /// <summary>
        /// 添加信号引用，在应用层才可获得该信号的数据
        /// </summary>
        /// <param name="signalID">信号ID</param>
        public static Task AddSignalReference(String signalID)
        {
            return Handler.AddSignalReference(signalID);
        }

        /// \~English
        /// <summary>
        /// Convert timeline point to time offset in session
        /// </summary>
        /// <param name="timeline">Timeline point</param>
        /// <returns>Time offset in session, null if over the bound</returns>
        /// \~Chinese
        /// <summary>
        /// 将时间线上的时间转换为在session中的时间
        /// </summary>
        /// <param name="timeline">时间线上的时间点</param>
        /// <returns>在session中的时间，若超出范围则返回null</returns>
        public static Task<TimeWithSession> ConvertTimeIntoSession(double timeline)
        {
            return Handler.ConvertTimeIntoSession(timeline);
        }

        /// \~English
        /// <summary>
        /// Remove data layer, and delete files to recycle bin
        /// </summary>
        /// <param name="layer">Data layer</param>
        /// \~Chinese
        /// <summary>
        /// 移除数据层级，并删除所有文件至回收站
        /// </summary>
        /// <param name="layer">数据层级</param>
        public static Task DeleteDataLayer(String layer)
        {
            return Handler.DeleteDataLayer(layer);
        }

        /// \~English
        /// <summary>
        /// Receive data from native plugins
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="dataID">Data ID, should not be null</param>
        /// <returns>All received binary data</returns>
        /// \~Chinese
        /// <summary>
        /// 接收所有从原生插件发来的新数据
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="nativeClassID">原生组件ID</param>
        /// <param name="dataID">数据ID，不可为null</param>
        /// <returns>所有新数据</returns>
        public static Task<byte[][]> DequeueDataFromNative(object caller, String nativeClassID, String dataID)
        {
            return Handler.DequeueDataFromNative(caller, nativeClassID, dataID);
        }

        /// \~English
        /// <summary>
        /// Disable all components (Some may not be able to disabled)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 禁用所有组件配置
        /// </summary>
        public static Task DisableAllConfigs()
        {
            return Handler.DisableAllConfigs();
        }

        /// \~English
        /// <summary>
        /// Disable all plugins (except for the workflow plugin)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 禁用所有插件（除当前流程插件外）
        /// </summary>
        public static Task DisableAllPlugins()
        {
            return Handler.DisableAllPlugins();
        }

        /// \~English
        /// <summary>
        /// Disable processor/native/device component component
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// \~Chinese
        /// <summary>
        /// 禁用数据处理/原生/设备组件
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        public static Task DisableModule(object caller, String classID)
        {
            return Handler.DisableModule(caller, classID);
        }

        /// \~English
        /// <summary>
        /// Disable plugin
        /// </summary>
        /// <param name="packID">Plugin pack ID</param>
        /// \~Chinese
        /// <summary>
        /// 禁用插件
        /// </summary>
        /// <param name="packID">插件包ID</param>
        public static Task DisablePlugin(String packID)
        {
            return Handler.DisablePlugin(packID);
        }

        /// \~English
        /// <summary>
        /// Enable plugin (restart is still needed to activate it)
        /// </summary>
        /// <param name="packID">Plugin pack ID</param>
        /// \~Chinese
        /// <summary>
        /// 启用插件（需要重启应用程序后生效）
        /// </summary>
        /// <param name="packID">插件包ID</param>
        public static Task EnablePlugin(String packID)
        {
            return Handler.EnablePlugin(packID);
        }

        /// \~English
        /// <summary>
        /// Send data to native plugins
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="dataID">Data ID, should not be null</param>
        /// <param name="data">Binary data, should not be null</param>
        /// \~Chinese
        /// <summary>
        /// 发送数据至原生插件
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="nativeClassID">原生组件ID</param>
        /// <param name="dataID">数据ID，不可为null</param>
        /// <param name="data">数据，不可为null</param>
        public static Task EnqueueDataToNative(object caller, String nativeClassID, String dataID, byte[] data)
        {
            return Handler.EnqueueDataToNative(caller, nativeClassID, dataID, data);
        }

        /// \~English
        /// <summary>
        /// Get guest synchronization IDs of all channels being configured as guest synchronized
        /// </summary>
        /// <returns>Guest synchronization IDs of all channels being configured as guest synchronized</returns>
        /// \~Chinese
        /// <summary>
        /// 获取已配置为客机同步的所有ID
        /// </summary>
        /// <returns>已配置为客机同步的所有ID列表</returns>
        public static Task<string[]> GetAllChannelGuestSyncKeys()
        {
            return Handler.GetAllChannelGuestSyncKeys();
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.14) Get monitor IDs of all the channels being monitored that there's data in the channel
        /// </summary>
        /// <returns>Monitor IDs of all the channels being monitored</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.14) 获取所有正在监控有无数据的通道ID
        /// </summary>
        /// <returns>正在监控有无数据的通道ID列表</returns>
        public static Task<string[]> GetAllChannelMonitoringKeys()
        {
            return Handler.GetAllChannelMonitoringKeys();
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.14) Get monitor IDs of all the channels being monitored that the channel's data is synchronized with time server
        /// </summary>
        /// <returns>Monitor IDs of all the channels being monitored</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.14) 获取所有正在监控数据与授时服务器同步的监控ID
        /// </summary>
        /// <returns>正在监控数据与授时服务器同步的通道ID列表</returns>
        public static Task<string[]> GetAllChannelServerSyncMonitoringKeys()
        {
            return Handler.GetAllChannelServerSyncMonitoringKeys();
        }

        /// \~English
        /// <summary>
        /// Get all general devices' status
        /// </summary>
        /// <returns>The Dictionary. The key is native type ID or class ID of the general device</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有设备状态
        /// </summary>
        /// <returns>设备状态表，键为设备的原生类型ID或组件ID</returns>
        public static Task<Dictionary<String, DeviceStatusDetail>> GetAllDeviceStatus()
        {
            return Handler.GetAllDeviceStatus();
        }

        /// \~English
        /// <summary>
        /// Get time shift configuration for all raw data channels
        /// </summary>
        /// <returns>Dictionary. The key is channel ID and the value is time shift (in milliseconds)</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有原始数据通道延迟配置
        /// </summary>
        /// <returns>所有原始数据通道的延迟配置，单位毫秒</returns>
        public static Task<Dictionary<string, double>> GetAllRawChannelDelayConfigs()
        {
            return Handler.GetAllRawChannelDelayConfigs();
        }

        /// \~English
        /// <summary>
        /// Get GUI framework that the application based on
        /// </summary>
        /// <returns>GUI framework</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序基于的图形界面框架
        /// </summary>
        /// <returns>图形界面框架</returns>
        public static Task<ApplicationGUI> GetAppGUI()
        {
            return handler.GetAppGUI();
        }

        /// \~English
        /// <summary>
        /// Get application's ID
        /// </summary>
        /// <returns>Application's ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序ID
        /// </summary>
        /// <returns>应用程序ID</returns>
        public static Task<String> GetAppID()
        {
            return handler.GetAppID();
        }

        /// \~English
        /// <summary>
        /// Get application's language
        /// </summary>
        /// <returns>Application's language</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序的显示语言
        /// </summary>
        /// <returns>应用程序的显示语言</returns>
        public static Task<Language> GetAppLanguage()
        {
            return Handler.GetAppLanguage();
        }

        /// \~English
        /// <summary>
        /// Get application's current mode
        /// </summary>
        /// <returns>Application's current mode</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序当前的运行模式
        /// </summary>
        /// <returns>应用程序运行模式</returns>
        public static Task<ApplicationMode> GetAppMode()
        {
            return Handler.GetAppMode();
        }

        /// \~English
        /// <summary>
        /// Get application's current status
        /// </summary>
        /// <returns>Application's current status</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序当前的运行状态
        /// </summary>
        /// <returns>应用程序运行状态</returns>
        public static Task<ApplicationStatus> GetAppStatus()
        {
            return Handler.GetAppStatus();
        }

        /// \~English
        /// <summary>
        /// Get time shift configuration for audio data channel
        /// </summary>
        /// <returns>Time shift, in milliseconds</returns>
        /// \~Chinese
        /// <summary>
        /// 获取音频数据通道延迟配置
        /// </summary>
        /// <returns>延迟配置，单位毫秒</returns>
        public static Task<double> GetAudioChannelDelayConfig()
        {
            return Handler.GetAudioChannelDelayConfig();
        }

        /// \~English
        /// <summary>
        /// Get whether there's data in audio channel, and the interval and delay information
        /// </summary>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <returns>1. Whether there's data. 2. The interval (time between frames) curve, in seconds. 3. The delay curve, in seconds</returns>
        /// \~Chinese
        /// <summary>
        /// 获取音频数据通道状态
        /// </summary>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <returns>1. 是否有数据; 2. 每帧数据之间的时间间隔曲线，单位秒; 3. 每帧数据的延迟曲线，单位秒</returns>
        public static Task<(bool, double[], double[])> GetAudioChannelStatus(uint? toleranceMillisecond)
        {
            return Handler.GetAudioChannelStatus(toleranceMillisecond);
        }

        /// \~English
        /// <summary>
        /// Get information of all registered audio drivers
        /// </summary>
        /// <returns>Information of all registered audio drivers, null if none registered</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有已注册的音频驱动信息
        /// </summary>
        /// <returns>已注册的音频驱动信息列表，若未注册任何有效驱动则返回null</returns>
        public static Task<AudioDriverInfo[]> GetAudioDrivers()
        {
            return Handler.GetAudioDrivers();
        }

        /// \~English
        /// <summary>
        /// Get information of all audio recorders related to the specified driver
        /// </summary>
        /// <param name="driverID">Driver ID</param>
        /// <returns>Information of all audio recorders, null if the driver is not found or there's no recorders related</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定驱动下的音频采集设备信息列表
        /// </summary>
        /// <param name="driverID">音频驱动ID</param>
        /// <returns>音频采集设备信息列表，若无该驱动或驱动下无采集设备则返回null</returns>
        public static Task<AudioDeviceInfo[]> GetAudioRecordDevices(String driverID)
        {
            return Handler.GetAudioRecordDevices(driverID);
        }

        /// \~English
        /// <summary>
        /// Get information of all audio players related to the specified driver
        /// </summary>
        /// <param name="driverID">Driver ID</param>
        /// <returns>Information of all audio players, null if the driver is not found or there's no players related</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定驱动下的音频回放设备信息列表
        /// </summary>
        /// <param name="driverID">音频驱动ID</param>
        /// <returns>音频回放设备信息列表，若无该驱动或驱动下无回放设备则返回null</returns>
        public static Task<AudioDeviceInfo[]> GetAudioReplayDevices(String driverID)
        {
            return Handler.GetAudioReplayDevices(driverID);
        }

        /// \~English
        /// <summary>
        /// Get all bus channels with data
        /// </summary>
        /// <returns>Bus channels with data, value ranges 1~16</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有有效的总线通道
        /// </summary>
        /// <returns>有效的总线通道列表，值为1~16</returns>
        public static Task<int[]> GetAvailableBusChannels()
        {
            return Handler.GetAvailableBusChannels();
        }

        /// \~English
        /// <summary>
        /// Get all general raw data channels with data
        /// </summary>
        /// <returns>Channels IDs of all general raw data channels with data</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有有效的原始数据通道
        /// </summary>
        /// <returns>有效的原始数据通道列表，值为通道ID</returns>
        public static Task<String[]> GetAvailableRawChannels()
        {
            return Handler.GetAvailableRawChannels();
        }

        /// \~English
        /// <summary>
        /// Get all sample channels with data
        /// </summary>
        /// <returns>Channel IDs of all sample channels with data</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有有效的样本数据通道
        /// </summary>
        /// <returns>有效的样本数据通道列表，值为通道ID</returns>
        public static Task<String[]> GetAvailableSampleChannels()
        {
            return Handler.GetAvailableSampleChannels();
        }

        /// \~English
        /// <summary>
        /// Get all video channels with data
        /// </summary>
        /// <returns>Video channels with data, value ranges 0~23</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有有效的视频通道
        /// </summary>
        /// <returns>有效的视频通道列表，值为0~23</returns>
        public static Task<int[]> GetAvailableVideoChannels()
        {
            return Handler.GetAvailableVideoChannels();   
        }

        /// \~English
        /// <summary>
        /// Get the buffer range
        /// </summary>
        /// <returns>The buffer range</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序当前的数据缓存范围
        /// </summary>
        /// <returns>数据缓存范围</returns>
        public static Task<BufferRange> GetBufferRange()
        {
            return Handler.GetBufferRange();
        }

        /// \~English
        /// <summary>
        /// Get time shift configuration for bus raw data channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <returns>Time shift, in milliseconds</returns>
        /// \~Chinese
        /// <summary>
        /// 获取总线数据通道延迟配置
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <returns>延迟配置，单位毫秒</returns>
        public static Task<double> GetBusChannelDelayConfig(int channel)
        {
            return Handler.GetBusChannelDelayConfig(channel);
        }

        /// \~English
        /// <summary>
        /// Gets information about all bus channels of a specified session
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Information about all bus channels of this session. Return null if none exist</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定session的所有总线通道的信息
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>该session的所有总线通道的信息，若不存在则返回null</returns>
        public static Task<BusChannelInfo[]> GetBusChannelsInfo(SessionIdentifier session)
        {
            return Handler.GetBusChannelsInfo(session);
        }

        /// \~English
        /// <summary>
        /// Get whether there's data in a bus channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <returns>Whether there's data</returns>
        /// \~Chinese
        /// <summary>
        /// 获取总线数据通道状态
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <returns>是否有数据</returns>
        public static Task<bool> GetBusChannelStatus(int channel/* 1~16 */, uint? toleranceMillisecond)
        {
            return Handler.GetBusChannelStatus(channel, toleranceMillisecond);
        }

        /// \~English
        /// <summary>
        /// Get information of all bus devices
        /// </summary>
        /// <returns>Dictionary. The key is bus device ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取总线设备列表
        /// </summary>
        /// <returns>总线设备列表，键为设备ID，值为对应的设备信息</returns>
        public static Task<Dictionary<BusDeviceID, BusDeviceInfo>> GetBusDevices()
        {
            return Handler.GetBusDevices();
        }

        /// \~English
        /// <summary>
        /// Get frame rate of messages with the same local ID at the same channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <param name="localID">Local ID of bus message</param>
        /// <returns>Frame rate, 0 means invalid</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定通道收到的指定ID报文的帧率
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <param name="localID">通道内的报文ID</param>
        /// <returns>每秒帧率，0表示无效</returns>
        public static Task<float> GetBusMessageFPS(int channel, uint localID)
        {
            return Handler.GetBusMessageFPS(channel, localID);
        }

        /// \~English
        /// <summary>
        /// Get information of message with the specified local ID at the specified channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <param name="localID">Local ID of bus message</param>
        /// <returns>Message information, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定通道上指定ID报文信息
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <param name="localID">通道内的报文ID</param>
        /// <returns>总线报文信息，无信息则返回null</returns>
        public static Task<BusMessageInfo> GetBusMessageInfoByLocalID(int channel, uint localID)
        {
            return Handler.GetBusMessageInfoByLocalID(channel, localID);
        }

        /// \~English
        /// <summary>
        /// Get information of message with the specified ID
        /// </summary>
        /// <param name="busMessageID">Bus message ID</param>
        /// <returns>Message information, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定ID的总线报文的信息
        /// </summary>
        /// <param name="busMessageID">总线报文ID</param>
        /// <returns>总线报文的信息，报文不存在则返回null</returns>
        public static Task<BusMessageInfo> GetBusMessageInfo(String busMessageID)
        {
            return Handler.GetBusMessageInfo(busMessageID);
        }

        /// \~English
        /// <summary>
        /// Get payload of bus channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <returns>Payload in percentages, null if unavailable</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定总线通道的负载百分比
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <returns>总线通道的负载百分比，若无效则返回null</returns>
        public static Task<double?> GetBusPayloadPercentage(int channel)
        {
            return Handler.GetBusPayloadPercentage(channel);
        }

        /// \~English
        /// <summary>
        /// Get the bound channel of bus protocol
        /// </summary>
        /// <param name="protocolName">Bus protocol name (In the case of multiple channels, the channel name is included)</param>
        /// <returns>Bus channel (1~16), null if not bound</returns>
        /// \~Chinese
        /// <summary>
        /// 获取总线协议文件当前配置于哪个通道
        /// </summary>
        /// <param name="protocolName">总线协议名称（多通道的情况下包括通道名）</param>
        /// <returns>总线通道（1~16），若未配置则返回null</returns>
        public static Task<int?> GetBusProtocolFileChannel(String protocolName)
        {
            return Handler.GetBusProtocolFileChannel(protocolName);
        }

        /// \~English
        /// <summary>
        /// Get all bus protocol file IDs
        /// </summary>
        /// <returns>All bus protocol file IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取总线协议文件ID列表
        /// </summary>
        /// <returns>总线协议文件ID列表</returns>
        public static Task<BusProtocolFileID[]> GetBusProtocolFileIDList()
        {
            return Handler.GetBusProtocolFileIDList();
        }

        /// \~English
        /// <summary>
        /// Get the status of bus protocol file
        /// </summary>
        /// <param name="fileID">Bus protocol file ID</param>
        /// <returns>Status of bus protocol file</returns>
        /// \~Chinese
        /// <summary>
        /// 获取总线协议对应文件的状态
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        /// <returns>文件状态</returns>
        public static Task<BusProtocolFileState> GetBusProtocolFileState(BusProtocolFileID fileID)
        {
            return Handler.GetBusProtocolFileState(fileID);
        }

        /// \~English
        /// <summary>
        /// Get information of bus signal
        /// </summary>
        /// <param name="busSignalID">Bus signal ID</param>
        /// <returns>Information of bus signal, null if the signal doesn't exist or it's not a bus signal</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定ID的总线信号的信息
        /// </summary>
        /// <param name="busSignalID">总线信号ID</param>
        /// <returns>总线信号的信息，信号不存在或信号非总线信号则返回null</returns>
        public static Task<BusSignalInfo> GetBusSignalInfo(String busSignalID)
        {
            return Handler.GetBusSignalInfo(busSignalID);
        }


        /// \~English
        /// <summary>
        /// Get alias name of data channel
        /// </summary>
        /// <param name="channelID">Data channel ID, with format as "protocol@channel". Channel is starting from 0. Protocol with "v" version number is with backward compatibility. The video channel's protocol is "video"</param>
        /// <returns>Data channel ID, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据通道别名
        /// </summary>
        /// <param name="channelID">数据通道关键字，格式为"协议名@通道序号"，通道序号从0开始，协议名中带"v"字版本号的可向下兼容。视频协议名为video</param>
        /// <returns>数据通道别名，若未找到返回null</returns>
        public static Task<String> GetChannelAliasName(String channelID)
        {
            return Handler.GetChannelAliasName(channelID);
        }

        /// \~English
        /// <summary>
        /// Get all data channels' alias name
        /// </summary>
        /// <returns>The Dictionary. The key is data channel ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据通道别名
        /// </summary>
        /// <returns>数据所有通道的别名表</returns>
        public static Task<Dictionary<String, String>> GetChannelAliasTable()
        {
            return Handler.GetChannelAliasTable();
        }

        /// \~English
        /// <summary>
        /// Get whether the channel is configured as guest synchronized
        /// </summary>
        /// <param name="id">Guest synchronization ID, like bus.1, video.0, xxx.yyy(xxx is native plugin's type ID，yyy is channel name)</param>
        /// <returns>Whether the channel is configured as guest synchronized</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定通道是否已配置为客机同步
        /// </summary>
        /// <param name="id">客机同步ID，如bus.1, video.0, xxx.yyy(xxx为原生插件类型ID，yyy为通道名称)等</param>
        /// <returns>是否已配置为客机同步</returns>
        public static Task<bool> GetChannelGuestSyncFlag(String id)
        {
            return Handler.GetChannelGuestSyncFlag(id);
        }

        /// \~English
        /// <summary>
        /// Get the latest several timestamps of the specified channel
        /// </summary>
        /// <param name="channelID">Data channel ID, with format as "protocol@channel". Channel is starting from 0. The video channel's protocol is "video". The audio channel's ID is "audio"</param>
        /// <returns>the latest several timestamps, null if the channel is not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据通道上最近的若干帧时间戳
        /// </summary>
        /// <param name="channelID">数据通道关键字，格式为"协议名@通道序号"，通道序号从0开始。视频协议名为video，音频协议名为audio</param>
        /// <returns>指定数据通道上最近的若干帧时间戳，若该通道未找到或最近无数据则返回null</returns>
        public static Task<Timestamp[]> GetChannelLatestTimestamps(String channelID)
        {
            return Handler.GetChannelLatestTimestamps(channelID);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.14) Get whether to monitor that there's data in the specified channel
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0, etc.</param>
        /// <returns>Whether to monitor</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.14) 获取是否监控指定通道有无数据
        /// </summary>
        /// <param name="id">监控ID，如：bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0等</param>
        /// <returns>是否监控有无数据</returns>
        public static Task<bool> GetChannelMonitoringFlag(String id)
        {
            return Handler.GetChannelMonitoringFlag(id);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.14) Get whether to monitor that the specified channel's data is synchronized with time server
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, sample@xxx-v2@0, etc.</param>
        /// <returns>Whether to monitor</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.14) 获取是否监控指定通道数据与授时服务器同步
        /// </summary>
        /// <param name="id">监控ID，如bus@1, video@0, sample@xxx-v2@0等</param>
        /// <returns>是否监控指定通道数据与授时服务器同步</returns>
        public static Task<bool> GetChannelServerSyncMonitoringFlag(String id)
        {
            return Handler.GetChannelServerSyncMonitoringFlag(id);
        }

        /// \~English
        /// <summary>
        /// Get whether there's data of all channels
        /// </summary>
        /// <param name="tolerance">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <returns>Dictionary. The key is channel ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有通道的数据状态
        /// </summary>
        /// <param name="tolerance">可容忍最近多少毫秒（现实时间）无数据</param>
        /// <returns>返回各通道的数据状态，key为通道ID</returns>
        public static Task<Dictionary<String, bool>> GetChannelStatusTable(uint? tolerance)
        {
            return Handler.GetChannelStatusTable(tolerance);
        }

        /// \~English
        /// <summary>
        /// Get time synchronization status of all channels
        /// </summary>
        /// <returns>Dictionary. The key is channel ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有通道的时间同步状态
        /// </summary>
        /// <returns>返回各通道的时间同步状态，key为通道ID</returns>
        public static Task<Dictionary<String, TimeOffsetSync>> GetChannelSyncTable()
        {
            return Handler.GetChannelSyncTable();
        }

        /// \~English
        /// <summary>
        /// Get status of a general device's sub devices
        /// </summary>
        /// <param name="id">Device native type ID, or device class ID</param>
        /// <returns>Status of a general device's sub devices</returns>
        /// \~Chinese
        /// <summary>
        /// 获取各子设备的设备状态
        /// </summary>
        /// <param name="id">设备原生类型ID，或设备组件ID</param>
        /// <returns>各子设备的设备状态</returns>
        public static Task<GeneralDeviceStatus[]> GetChildDeviceStatus(String id)
        {
            return Handler.GetChildDeviceStatus(id);
        }

        /// \~English
        /// <summary>
        /// Get information of console class
        /// </summary>
        /// <param name="consoleClassID">Console class ID</param>
        /// <returns>Information of console class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取控制台组件信息
        /// </summary>
        /// <param name="consoleClassID">控制台组件ID</param>
        /// <returns>控制台组件信息，若未找到返回null</returns>
        public static Task<ConsoleClassInfo> GetConsoleClassInfo(String consoleClassID)
        {
            return Handler.GetConsoleClassInfo(consoleClassID);
        }

        /// \~English
        /// <summary>
        /// Get information of all console classes
        /// </summary>
        /// <returns>Dictionary. The key is console class ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取控制台组件信息表
        /// </summary>
        /// <returns>控制台组件信息表，键为组件ID</returns>
        public static Task<Dictionary<string, ConsoleClassInfo>> GetConsoleClassTable()
        {
            return Handler.GetConsoleClassTable();
        }

        /// \~English
        /// <summary>
        /// Get status of components related to the console
        /// </summary>
        /// <param name="consoleClassID">Console component's class ID</param>
        /// <returns>1. The main status. 2. Children status</returns>
        /// \~Chinese
        /// <summary>
        /// 获取控制台相关组件配置状态
        /// </summary>
        /// <param name="consoleClassID">控制台组件ID</param>
        /// <returns>1. 配置状态; 2. 子配置状态</returns>
        public static Task<(ConfigStatus, ConfigStatus[])> GetConsoleRelatedModulesConfigStatus(String consoleClassID)
        {
            return Handler.GetConsoleRelatedModulesConfigStatus(consoleClassID);
        }

        /// \~English
        /// <summary>
        /// Get current CPU tick on host machine
        /// </summary>
        /// <returns>Current CPU tick on host machine</returns>
        /// \~Chinese
        /// <summary>
        /// 获取主机当前的CPU计数
        /// </summary>
        /// <returns>当前的CPU计数</returns>
        public static Task<ulong> GetCPUTick()
        {
            return Handler.GetCPUTick();
        }

        /// \~English
        /// <summary>
        /// Get CPU ticks per second on host machine
        /// </summary>
        /// <returns>CPU ticks per second on host machine</returns>
        /// \~Chinese
        /// <summary>
        /// 获取主机上每秒增加的CPU计数
        /// </summary>
        /// <returns>每秒增加的CPU计数</returns>
        public static Task<ulong> GetCPUTicksPerSecond()
        {
            return Handler.GetCPUTicksPerSecond();
        }

        /// \~English
        /// <summary>
        /// Get CPU time (starting from host machine powered on)
        /// </summary>
        /// <returns>CPU time, in seconds, 0 means invalid</returns>
        /// \~Chinese
        /// <summary>
        /// 获取CPU时间（从开机起算的时间）
        /// </summary>
        /// <returns>CPU时间，单位秒，返回0表示无效</returns>
        public static Task<double> GetCPUTime()
        {
            return Handler.GetCPUTime();
        }

        /// \~English
        /// <summary>
        /// Get session's CPU time model
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>CPU time model</returns>
        /// \~Chinese
        /// <summary>
        /// 获取session的CPU时间模型
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>CPU时间模型</returns>
        public static Task<CPUTimeModel> GetCPUTimeModel(SessionIdentifier session)
        {
            return Handler.GetCPUTimeModel(session);
        }

        /// \~English
        /// <summary>
        /// Get current generation ID of input data
        /// </summary>
        /// <returns>Current generation ID of input data, null means the input source is raw data (not any generation)</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前输入数据的generation ID
        /// </summary>
        /// <returns>当前输入数据的generation ID，空表示输入数据为原始数据</returns>
        public static Task<String> GetCurrentDataGeneration()
        {
            return Handler.GetCurrentDataGeneration();
        }

        /// \~English
        /// <summary>
        /// Get current data layer
        /// </summary>
        /// <returns>The current data layer, while null means all layers, '.' means the layer of sessions under data path, '..' means the layer of data path which is session</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前的数据层级
        /// </summary>
        /// <returns>数据层级，其中null表示所有层级，'.'表示根路径下的session，'..'表示根路径即session</returns>
        public static Task<String> GetCurrentDataLayer()
        {
            return Handler.GetCurrentDataLayer();
        }

        /// \~English
        /// <summary>
        /// Get ID of the session currently recording or previewing in online mode or remote mode
        /// </summary>
        /// <returns>Session ID, null if neither in online mode nor in remote mode</returns>
        /// \~Chinese
        /// <summary>
        /// 获取采集模式(在线/远程)下正在预览或采集的session
        /// </summary>
        /// <returns>采集模式(在线/远程)下正在预览或采集的session，若非采集模式则返回null</returns>
        public static Task<SessionIdentifier?> GetCurrentOnlineSession()
        {
            return Handler.GetCurrentOnlineSession();
        }

        /// \~English
        /// <summary>
        /// Get current session's GUID
        /// </summary>
        /// <returns>Current session's GUID, null if application is not in running status</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前session的GUID
        /// </summary>
        /// <returns>当前session的GUID，若未运行则返回null</returns>
        public static Task<String> GetCurrentSessionGUID()
        {
            return Handler.GetCurrentSessionGUID();
        }

        /// \~English
        /// <summary>
        /// Get all data layers
        /// </summary>
        /// <returns>Data layers, while '.' means the layer of sessions under data path, '..' means the layer of data path which is session</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有有效的数据层级
        /// </summary>
        /// <returns>数据层级列表，其中'.'表示根路径下的session，'..'表示根路径即session</returns>
        public static Task<String[]> GetDataLayers()
        {
            return Handler.GetDataLayers();
        }

        /// \~English
        /// <summary>
        /// Get information of all app-layer device classes
        /// </summary>
        /// <returns>Dictionary. The key is device class ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取设备组件信息表
        /// </summary>
        /// <returns>设备组件信息表，键为组件ID</returns>
        public static Task<Dictionary<String, DeviceClassInfo>> GetDeviceClassTable()
        {
            return Handler.GetDeviceClassTable();
        }

        /// \~English
        /// <summary>
        /// Get status of a general device
        /// </summary>
        /// <param name="id">Device native type ID, or device class ID</param>
        /// <returns>Status of the general device</returns>
        /// \~Chinese
        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="id">设备原生类型ID，或设备组件ID</param>
        /// <returns>返回设备状态</returns>
        public static Task<GeneralDeviceStatus> GetDeviceStatus(String id)
        {
            return Handler.GetDeviceStatus(id);
        }

        /// \~English
        /// <summary>
        /// Get status of components related to the dialog
        /// </summary>
        /// <param name="dialogClassID">Dialog component's class ID</param>
        /// <param name="transformID">Transform ID</param>
        /// <returns>1. The main status. 2. Children status</returns>
        /// \~Chinese
        /// <summary>
        /// 获取对话框相关组件配置状态
        /// </summary>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="transformID">分化ID</param>
        /// <returns>1. 配置状态; 2. 子配置状态</returns>
        public static Task<(ConfigStatus, ConfigStatus[])> GetDialogRelatedModulesConfigStatus(String dialogClassID, String transformID)
        {
            return Handler.GetDialogRelatedModulesConfigStatus(dialogClassID, transformID);
        }

        /// \~English
        /// <summary>
        /// Get list of event handle
        /// </summary>
        /// <returns>List of event handle</returns>
        /// \~Chinese
        /// <summary>
        /// 返回事件对象列表
        /// </summary>
        /// <returns>事件对象列表</returns>
        public static Task<object[]> GetEventHandles()
        {
            return Handler.GetEventHandles();
        }

        /// \~English
        /// <summary>
        /// Get full information of event
        /// </summary>
        /// <param name="eventHandle">Event handle</param>
        /// <returns>Full information of event, null if the handle is invalid or the info is not complete</returns>
        /// \~Chinese
        /// <summary>
        /// 返回指定事件对象的完整信息
        /// </summary>
        /// <param name="eventHandle">事件对象</param>
        /// <returns>事件完整信息，null表示事件对象无效或信息不完整</returns>
        public static Task<EventInfo> GetEventInfo(object eventHandle)
        {
            return Handler.GetEventInfo(eventHandle);
        }

        /// \~English
        /// <summary>
        /// Get list of all event types (include not enabled)
        /// </summary>
        /// <returns>List of all event types</returns>
        /// \~Chinese
        /// <summary>
        /// 获取事件类型名称列表（包括未启用的）
        /// </summary>
        /// <returns>事件类型名称列表</returns>
        public static Task<String[]> GetEventTypeNames()
        {
            return Handler.GetEventTypeNames();
        }

        /// \~English
        /// <summary>
        /// Get all filtered session IDs under current data layer
        /// </summary>
        /// <returns>Session IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前数据层级下筛选后的所有session
        /// </summary>
        /// <returns>Session ID列表</returns>
        public static Task<SessionIdentifier[]> GetFilteredSessionList()
        {
            return Handler.GetFilteredSessionList();
        }

        /// \~English
        /// <summary>
        /// Get total length of filtered sessions under current data layer
        /// </summary>
        /// <returns>Total length of filtered sessions under current data layer</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前层级下筛选后的所有session的时长总长
        /// </summary>
        /// <returns>筛选后的所有session的时长总长</returns>
        public static Task<double> GetFilteredSessionListTotalLength()
        {
            return Handler.GetFilteredSessionListTotalLength();
        }

        /// \~English
        /// <summary>
        /// Get all sessions of which the generations are finished processing
        /// </summary>
        /// <param name="generation">Generation ID</param>
        /// <returns>Session IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前层级下某个generation下所有处理完毕的session
        /// </summary>
        /// <param name="generation">目标generation</param>
        /// <returns>处理完毕的session ID列表</returns>
        public static Task<SessionIdentifier[]> GetFinishedSessions(String generation)
        {
            return Handler.GetFinishedSessions(generation);
        }

        /// \~English
        /// <summary>
        /// Get thirt party license notices of software used by framework
        /// </summary>
        /// <returns>Dictionary. The key is title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取框架软件使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为标题，值为版权声明</returns>
        public static Task<Dictionary<String, String>> GetFrameworkThirdPartyNotices()
        {
            return Handler.GetFrameworkThirdPartyNotices();
        }

        /// \~English
        /// <summary>
        /// Get all generations under current data path
        /// </summary>
        /// <returns>Generation IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前数据目录下的所有generation
        /// </summary>
        /// <returns>Generation ID列表</returns>
        public static Task<String[]> GetGenerationList()
        {
            return Handler.GetGenerationList();
        }

        /// \~English
        /// <summary>
        /// Get the status of generation with the specified generation ID and under the specified session
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="generation">Generation ID</param>
        /// <returns>Generation's status, null if it doesn't exist</returns>
        /// \~Chinese
        /// <summary>
        /// 获取某个session中的某个generation的处理状态
        /// </summary>
        /// <param name="session">希望获取generation所属的session ID</param>
        /// <param name="generation">希望获取的generation ID</param>
        /// <returns>Generation的处理状态，若generation不存在则返回null</returns>
        public static Task<GenerationProcessStatus?> GetGenerationProcessStatus(SessionIdentifier session, String generation)
        {
            return Handler.GetGenerationProcessStatus(session, generation);
        }

        /// \~English
        /// <summary>
        /// Get all sessions that contain generation with the specified ID under current data layer
        /// </summary>
        /// <param name="generationID">Generation ID</param>
        /// <returns>Session IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前层级下含有指定generation ID的所有session
        /// </summary>
        /// <param name="generationID">Generation ID</param>
        /// <returns>Session ID列表</returns>
        public static Task<SessionIdentifier[]> GetGenerationSessions(String generationID)
        {
            return Handler.GetGenerationSessions(generationID);
        }

        /// \~English
        /// <summary>
        /// Get value of global parameter
        /// </summary>
        /// <param name="key">Key of global parameter</param>
        /// <param name="defaultValue">Default value, which will be returned while key is null, empty, or doesn't exist</param>
        /// <returns>Value of global parameter</returns>
        /// \~Chinese
        /// <summary>
        /// 获取全局参数
        /// </summary>
        /// <param name="key">全局参数key</param>
        /// <param name="defaultValue">默认值，即当key为null、""、或不存在时返回的值</param>
        /// <returns>全局参数value</returns>
        public static Task<String> GetGlobalParameter(String key, String defaultValue)
        {
            return Handler.GetGlobalParameter(key, defaultValue);
        }

        /// \~English
        /// <summary>
        /// Get all keys of global parameter
        /// </summary>
        /// <returns>All keys of global parameter</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有全局参数的键
        /// </summary>
        /// <returns>所有全局参数的键</returns>
        public static Task<String[]> GetGlobalParameterKeys()
        {
            return Handler.GetGlobalParameterKeys();
        }

        /// \~English
        /// <summary>
        /// Get value of global variable
        /// </summary>
        /// <param name="key">Key of global variable</param>
        /// <param name="defaultValue">Default value, which will be returned while key is null, empty, or doesn't exist</param>
        /// <returns>Value of global variable</returns>
        /// \~Chinese
        /// <summary>
        /// 获取全局变量
        /// </summary>
        /// <param name="key">全局变量key</param>
        /// <param name="defaultValue">默认值，即当key为null、""、或不存在时返回的值</param>
        /// <returns>全局变量value</returns>
        public static Task<String> GetGlobalVariable(String key, String defaultValue)
        {
            return Handler.GetGlobalVariable(key, defaultValue);
        }

        /// \~English
        /// <summary>
        /// Get all keys of global variable
        /// </summary>
        /// <returns>All keys of global variable</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有全局变量的键
        /// </summary>
        /// <returns>所有全局变量的键</returns>
        public static Task<String[]> GetGlobalVariableKeys()
        {
            return Handler.GetGlobalVariableKeys();
        }

        /// \~English
        /// <summary>
        /// Get session's satellite posix time model
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Satellite posix time model</returns>
        /// \~Chinese
        /// <summary>
        /// 获取session的卫星Posix时间模型
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>卫星Posix时间模型</returns>
        public static Task<PosixTimeModel> GetGNSSPosixTimeModel(SessionIdentifier session)
        {
            return Handler.GetGNSSPosixTimeModel(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.13) Get GPU decoder test results
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.13) 获取GPU解码测试结果
        /// </summary>
        public static Task<GPUDecoderTestResults> GetGPUDecoderTestResults()
        {
            return Handler.GetGPUDecoderTestResults();
        }

        /// \~English
        /// <summary>
        /// Get graph's data
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="graphID">Graph's ID, queried by ASEva.GraphDefinition.GetID </param>
        /// <returns>Graph's data, null if it doesn't exist or not belongs to current data layer</returns>
        /// \~Chinese
        /// <summary>
        /// 获取图表对象
        /// </summary>
        /// <param name="session">想要获取图表的session ID</param>
        /// <param name="graphID">图表报告ID，通过 ASEva.GraphDefinition.GetID 获取</param>
        /// <returns>图表对象，若不存在或不属于当前层级则返回null</returns>
        public static Task<GraphData> GetGraphData(SessionIdentifier session, int graphID)
        {
            return Handler.GetGraphData(session, graphID);
        }

        /// \~English
        /// <summary>
        /// Get information of all dedicated graphic card
        /// </summary>
        /// <returns>Information of all dedicated graphic card</returns>
        /// \~Chinese
        /// <summary>
        /// 获取主机上所有独立显卡信息
        /// </summary>
        /// <returns>独立显卡信息列表</returns>
        public static Task<GraphicCardInfo[]> GetGraphicCardInfos()
        {
            return Handler.GetGraphicCardInfos();
        }

        /// \~English
        /// <summary>
        /// Get all graph IDs
        /// </summary>
        /// <returns>All graph IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取图表报告ID列表
        /// </summary>
        /// <returns>图表报告ID列表</returns>
        public static Task<int[]> GetGraphIDList()
        {
            return Handler.GetGraphIDList();
        }

        /// \~English
        /// <summary>
        /// Get graph ID with the specified title
        /// </summary>
        /// <param name="title">Graph's title</param>
        /// <returns>Graph ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定图表报告标题的报告ID
        /// </summary>
        /// <param name="title">图表报告标题</param>
        /// <returns>图表报告ID</returns>
        public static Task<int?> GetGraphIDWithTitle(String title)
        {
            return Handler.GetGraphIDWithTitle(title);
        }

        /// \~English
        /// <summary>
        /// Get title of graph with the specified ID
        /// </summary>
        /// <param name="graphID">Graph ID</param>
        /// <returns>Graph's title, null if the graph doesn't exist</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定ID图表的标题
        /// </summary>
        /// <param name="graphID">图表报告ID</param>
        /// <returns>图表报告标题，若不存在则返回null</returns>
        public static Task<String> GetGraphTitle(int graphID)
        {
            return Handler.GetGraphTitle(graphID);
        }

        /// \~English
        /// <summary>
        /// Get session's host machine posix time model
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Host machine posix time model</returns>
        /// \~Chinese
        /// <summary>
        /// 获取session的主机Posix时间模型
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>主机Posix时间模型</returns>
        public static Task<PosixTimeModel> GetHostPosixTimeModel(SessionIdentifier session)
        {
            return Handler.GetHostPosixTimeModel(session);
        }

        /// \~English
        /// <summary>
        /// Get the interest target
        /// </summary>
        /// <returns>The interest target (in seconds), which could be out of the buffer range</returns>
        /// \~Chinese
        /// <summary>
        /// 获取兴趣时间点目标
        /// </summary>
        /// <returns>兴趣时间点目标(秒单位)，可能超出数据缓存范围</returns>
        public static Task<double> GetInterestTarget()
        {
            return Handler.GetInterestTarget();
        }

        /// \~English
        /// <summary>
        /// Get the timeline point of current interest
        /// </summary>
        /// <returns>Timeline point of current interest, in seconds</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序当前兴趣点在时间线上的位置
        /// </summary>
        /// <returns>在时间线上的兴趣点，单位秒</returns>
        public static Task<double> GetInterestTime()
        {
            return Handler.GetInterestTime();
        }

        /// \~English
        /// <summary>
        /// Get the host machine's date and time of current interest
        /// </summary>
        /// <returns>Host machine's date and time of current interest, null if unavailable</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序当前兴趣点对应的本地时间
        /// </summary>
        /// <returns>兴趣点的本地时间，若无数据则返回null</returns>
        public static Task<DateTime?> GetInterestTimestamp()
        {
            return Handler.GetInterestTimestamp();
        }

        /// \~English
        /// <summary>
        /// Get the UTC date and time queried from Internet NTP server
        /// </summary>
        /// <returns>The UTC date and time queried from Internet NTP server, null if Internet is not connected or querying failed</returns>
        /// \~Chinese
        /// <summary>
        /// 获取从互联网获取的当前时间
        /// </summary>
        /// <returns>从互联网获取的当前时间(UTC时间)，若未联网或获取失败则返回空</returns>
        public static Task<DateTime?> GetInternetNTPTime()
        {
            return Handler.GetInternetNTPTime();
        }

        /// \~English
        /// <summary>
        /// Get all licensed function indices
        /// </summary>
        /// <returns>All licensed function indices</returns>
        /// \~Chinese
        /// <summary>
        /// 获取被许可的功能序号列表
        /// </summary>
        /// <returns>被许可的功能序号列表</returns>
        public static Task<int[]> GetLicensedFunctionIndices()
        {
            return Handler.GetLicensedFunctionIndices();
        }

        /// \~English
        /// <summary>
        /// Get license information
        /// </summary>
        /// <returns>License information</returns>
        /// \~Chinese
        /// <summary>
        /// 获取许可证的详细信息
        /// </summary>
        /// <returns>许可证的详细信息</returns>
        public static Task<String> GetLicenseInfo()
        {
            return Handler.GetLicenseInfo();
        }

        /// \~English
        /// <summary>
        /// Convert time offset in session to local date and time
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">Time offset, in seconds</param>
        /// <param name="useGNSS">Whether to use satellite posix time model, or else use host machine posix time model</param>
        /// <returns>Local date and time</returns>
        /// \~Chinese
        /// <summary>
        /// 计算session中某个时间偏置对应的本地时间
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">时间偏置，单位秒</param>
        /// <param name="useGNSS">是否使用卫星Posix时间模型计算，否则使用主机Posix时间模型</param>
        /// <returns>对应的本地时间</returns>
        public static Task<DateTime?> GetLocalDateTime(SessionIdentifier session, double timeOffset, bool useGNSS)
        {
            return Handler.GetLocalDateTime(session, timeOffset, useGNSS);
        }

        /// \~English
        /// <summary>
        /// Get all log messages
        /// </summary>
        /// <returns>All log messages</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有清单信息
        /// </summary>
        /// <returns>清单信息列表</returns>
        public static Task<LogMessage[]> GetLogMessages()
        {
            return Handler.GetLogMessages();
        }

        /// \~English
        /// <summary>
        /// Get all manual trigger names
        /// </summary>
        /// <returns>Manual trigger names</returns>
        /// \~Chinese
        /// <summary>
        /// 获得手动触发器所有通道的名称
        /// </summary>
        /// <returns>名称列表</returns>
        public static Task<String[]> GetManualTriggerNames()
        {
            return Handler.GetManualTriggerNames();
        }

        /// \~English
        /// <summary>
        /// Get name of manual trigger at the channel
        /// </summary>
        /// <param name="index">Channel index, ranges 0~15</param>
        /// <returns>Name of manual trigger, null if the index is over the bound</returns>
        /// \~Chinese
        /// <summary>
        /// 获得手动触发器通道的名称
        /// </summary>
        /// <param name="index">通道序号，0~15</param>
        /// <returns>手动触发器通道的名称，若序号超出范围则返回null</returns>
        public static Task<String> GetManualTriggerName(int index)
        {
            return Handler.GetManualTriggerName(index);
        }

        /// \~English
        /// <summary>
        /// Get child status of processor/native/device component's configuration
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// <returns>Child status of component's configuration, null if not found or there's no child functions</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据处理/原生/设备组件各子功能配置的状态
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        /// <returns>各子功能配置的状态，若找不到类别ID对应的组件或无子功能配置则返回null</returns>
        public static Task<ConfigStatus[]> GetModuleChildConfigStatus(object caller, String classID)
        {
            return Handler.GetModuleChildConfigStatus(caller, classID);
        }

        /// \~English
        /// <summary>
        /// Get processor/native/device component's configuration string
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// <returns>Configuration string, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据处理/原生/设备组件配置的字符串描述
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        /// <returns>配置的字符串描述，null表示找不到类别ID对应的组件</returns>
        public static Task<String> GetModuleConfig(object caller, String classID)
        {
            return Handler.GetModuleConfig(caller, classID);
        }

        /// \~English
        /// <summary>
        /// Get status of processor/native/device component's configuration
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// <returns>1. Status of component's configuration, returns ASEva.ConfigStatus.Disabled if not found. 2. Error hint, available while the status is EnabledWithError or EnabledWithWarning</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据处理/原生/设备组件配置的状态
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        /// <returns>1. 组件配置的状态，若找不到类别ID对应的组件则返回 ASEva.ConfigStatus.Disabled ; 2. 错误提示，当配置状态为EnabledWithError或EnabledWithWarning时有效</returns>
        public static Task<(ConfigStatus, String)> GetModuleConfigStatus(object caller, String classID)
        {
            return Handler.GetModuleConfigStatus(caller, classID);
        }

        /// \~English
        /// <summary>
        /// Gets details about the processor/native/device component
        /// </summary>
        /// <returns>Component details. Return null if none exist</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据处理/原生/设备组件的详情
        /// </summary>
        /// <returns>组件详情，若不存在则返回null</returns>
        public static Task<ModuleDetails> GetModuleDetails(String classID)
        {
            return Handler.GetModuleDetails(classID);
        }

        /// \~English
        /// <summary>
        /// Get information of all native-layer component classes
        /// </summary>
        /// <returns>Dictionary. The key is native-layer component's class ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取原生组件信息表
        /// </summary>
        /// <returns>原生组件信息表，键为组件ID</returns>
        public static Task<Dictionary<String, NativeClassInfo>> GetNativeClassTable()
        {
            return Handler.GetNativeClassTable();
        }

        /// \~English
        /// <summary>
        /// Get version of all native plugins
        /// </summary>
        /// <param name="type">Type of native plugin</param>
        /// <returns>Dictionary. The key is plugin's type ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取原生插件插件版本列表
        /// </summary>
        /// <param name="type">原生库类别</param>
        /// <returns>版本列表，键为原生插件的类型ID</returns>
        public static Task<Dictionary<String, Version>> GetNativePluginVersions(NativeLibraryType type)
        {
            return Handler.GetNativePluginVersions(type);
        }

        /// \~English
        /// <summary>
        /// Get all guest synchronization IDs and title of each ID
        /// </summary>
        /// <returns>Dictionary. The key is guest synchronization ID and the value is the title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有插件的客机同步ID以及对应的标题
        /// </summary>
        /// <returns>客机同步标题表，键为客机同步ID，值为对应的标题</returns>
        public static Task<Dictionary<String, String>> GetPluginGuestSyncTable()
        {
            return Handler.GetPluginGuestSyncTable();
        }

        /// \~English
        /// <summary>
        /// Get all plugin pack IDs
        /// </summary>
        /// <returns>All plugin pack IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取插件包ID列表
        /// </summary>
        /// <returns>插件包ID列表</returns>
        public static Task<String[]> GetPluginPackIDList()
        {
            return Handler.GetPluginPackIDList();
        }

        /// \~English
        /// <summary>
        /// Get information of plugin pack
        /// </summary>
        /// <param name="packID">Plugin pack ID</param>
        /// <returns>Information of plugin pack, null if the plugin pack is not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取插件包信息
        /// </summary>
        /// <param name="packID">插件包ID</param>
        /// <returns>插件包信息，若无对应插件包则返回null</returns>
        public static Task<PluginPackInfo> GetPluginPackInfo(String packID)
        {
            return Handler.GetPluginPackInfo(packID);
        }

        /// \~English
        /// <summary>
        /// Get all third party license notices of software used by plugins
        /// </summary>
        /// <returns>Dictionary. The key is plugin pack ID and the value is the information corresponding to the plugin (which is a dictionary with key as third party software's title)</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有插件使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为插件ID，值为该插件使用的第三方软件版权声明(其中键为标题，值为版权声明)</returns>
        public static Task<Dictionary<String, Dictionary<String, String>>> GetPluginThirdPartyNotices()
        {
            return Handler.GetPluginThirdPartyNotices();
        }

        /// \~English
        /// <summary>
        /// Get the nearest video frame's preview JPEG data from the specified time
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="timeline">Target timeline point, in seconds</param>
        /// <param name="maxGap">Max time gap, in seconds</param>
        /// <returns>1. Video frame's preview JPEG data, image width is 640 pix, null if failed to query. 2. Timestamp of output image, null if failed to query. 3. Camera information, null if failed to query</returns>
        /// \~Chinese
        /// <summary>
        /// 获取距离指定时间最近的视频帧的预览JPEG图像数据
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="timeline">获取视频帧的目标时间线，单位秒</param>
        /// <param name="maxGap">容许的最大间隔，单位秒</param>
        /// <returns>1. 视频帧的预览JPEG数据，图像宽度为640像素，获取失败则返回null; 2. 图像的时间戳，获取失败则为null; 3. 摄像头信息，获取失败则为null</returns>
        public static Task<(byte[], Timestamp?, CameraInfo)> GetPreviewJpeg(int channel, double timeline, double maxGap)
        {
            return Handler.GetPreviewJpeg(channel, timeline, maxGap);
        }

        /// \~English
        /// <summary>
        /// Get information of all app-layer processor classes
        /// </summary>
        /// <returns>Dictionary. The key is processor class ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据处理组件信息表
        /// </summary>
        /// <returns>数据处理组件信息表，键为组件ID</returns>
        public static Task<Dictionary<String, ProcessorClassInfo>> GetProcessorClassTable()
        {
            return Handler.GetProcessorClassTable();
        }

        /// \~English
        /// <summary>
        /// Get time shift configuration for general raw data channel
        /// </summary>
        /// <param name="id">General raw data's channel ID</param>
        /// <returns>Time shift, in milliseconds</returns>
        /// \~Chinese
        /// <summary>
        /// 获取原始数据通道延迟配置
        /// </summary>
        /// <param name="id">原始数据通道ID</param>
        /// <returns>延迟配置，单位毫秒</returns>
        public static Task<double> GetRawChannelDelayConfig(String id)
        {
            return Handler.GetRawChannelDelayConfig(id);
        }

        /// \~English
        /// <summary>
        /// Get whether there's data in general raw data channel
        /// </summary>
        /// <param name="channelID">General raw data's channel ID</param>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <returns>Whether there's data</returns>
        /// \~Chinese
        /// <summary>
        /// 获取原始数据通道状态
        /// </summary>
        /// <param name="channelID">原始数据通道ID</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <returns>是否有数据</returns>
        public static Task<bool> GetRawChannelStatus(String channelID, uint? toleranceMillisecond)
        {
            return Handler.GetRawChannelStatus(channelID, toleranceMillisecond);
        }

        /// \~English
        /// <summary>
        /// Get whether there's data in sample channel, and the interval and delay information
        /// </summary>
        /// <param name="channelID">Sample's channel ID</param>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <returns>1. Whether there's data. 2. The interval (time between frames) curve, in seconds. 3. The delay curve, in seconds</returns>
        /// \~Chinese
        /// <summary>
        /// 获取样本数据通道状态
        /// </summary>
        /// <param name="channelID">样本数据通道ID</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <returns>1. 是否有数据; 2. 每帧数据之间的时间间隔曲线，单位秒; 3. 每帧数据的延迟曲线，单位秒</returns>
        public static Task<(bool, double[], double[])> GetSampleChannelStatus(String channelID, uint? toleranceMillisecond)
        {
            return Handler.GetSampleChannelStatus(channelID, toleranceMillisecond);
        }

        /// \~English
        /// <summary>
        /// Get field titles of a general sample
        /// </summary>
        /// <param name="channelID">Sample channel ID, with format as "protocol@channel". Channel is starting from 0. Protocol with "v" version number is with backward compatibility</param>
        /// <returns>Field title, null if the channel doesn't exist or there's no titles</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定样本通道对应的标题
        /// </summary>
        /// <param name="channelID">样本通道关键字，格式为"协议名@通道序号"，通道序号从0开始</param>
        /// <returns>样本标题，null表示通道不存在或该样本通道无标题</returns>
        public static Task<List<String>> GetSampleTitle(String channelID)
        {
            return Handler.GetSampleTitle(channelID);
        }

        /// \~English
        /// <summary>
        /// Get all scenario IDs
        /// </summary>
        /// <returns>All scenario IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有场景ID列表
        /// </summary>
        /// <returns>场景ID列表</returns>
        public static Task<String[]> GetSceneIDList()
        {
            return Handler.GetSceneIDList();
        }

        /// \~English
        /// <summary>
        /// Get titles of all scenarios
        /// </summary>
        /// <returns>Dictionary. The key is scenario ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取场景标题表
        /// </summary>
        /// <returns>场景标题表</returns>
        public static Task<Dictionary<String, SceneTitle>> GetSceneTitleTable()
        {
            return Handler.GetSceneTitleTable();
        }

        /// \~English
        /// <summary>
        /// Get session's comment
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session's comment</returns>
        /// \~Chinese
        /// <summary>
        /// 获取session的注释说明
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session的注释说明</returns>
        public static Task<String> GetSessionComment(SessionIdentifier session)
        {
            return Handler.GetSessionComment(session);
        }

        /// \~English
        /// <summary>
        /// Get filter flags of all sessions
        /// </summary>
        /// <returns>Filter flags of all sessions</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有session的筛选标志位
        /// </summary>
        /// <returns>session的筛选标志位表</returns>
        public static Task<Dictionary<SessionIdentifier, SessionFilterFlags>> GetSessionFilterTable()
        {
            return Handler.GetSessionFilterTable();
        }

        /// \~English
        /// <summary>
        /// Get the folder name of a session
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Folder name</returns>
        /// \~Chinese
        /// <summary>
        /// 获取某个session的文件夹名
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>文件夹名</returns>
        public static Task<String> GetSessionFolderName(SessionIdentifier session)
        {
            return Handler.GetSessionFolderName(session);
        }

        /// \~English
        /// <summary>
        /// Get all generations of the session under current data layer
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Generation IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前层级下指定session下的所有generation ID
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Generation ID列表</returns>
        public static Task<String[]> GetSessionGenerations(SessionIdentifier session)
        {
            return Handler.GetSessionGenerations(session);
        }

        /// \~English
        /// <summary>
        /// Get whether the host machine is synchronized with time server while the session is being recorded
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Whether the host machine is synchronized with time server</returns>
        /// \~Chinese
        /// <summary>
        /// 获取session采集时主机是否与授时服务器同步
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>主机是否与授时服务器同步</returns>
        public static Task<bool> GetSessionHostSync(SessionIdentifier session)
        {
            return Handler.GetSessionHostSync(session);
        }

        /// \~English
        /// <summary>
        /// Get the data layer to which a session belongs
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>The data layer, while '.' means the layer of sessions under data path, '..' means the layer of data path which is a session</returns>
        /// \~Chinese
        /// <summary>
        /// 获取某个session所属数据层级
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>数据层级，其中'.'表示根路径下的session，'..'表示根路径即session</returns>
        public static Task<String> GetSessionLayer(SessionIdentifier session)
        {
            return Handler.GetSessionLayer(session);
        }

        /// \~English
        /// <summary>
        /// Get session's duration
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session's duration in seconds, null if the session doesn't exist or not belongs to current data layer</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定session的长度
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session长度，单位秒，session不存在或不属于当前层级则返回null</returns>
        public static Task<double?> GetSessionLength(SessionIdentifier session)
        {
            return Handler.GetSessionLength(session);
        }

        /// \~English
        /// <summary>
        /// Get all session IDs under current data layer
        /// </summary>
        /// <returns>Session IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前数据层级下的所有session
        /// </summary>
        /// <returns>Session ID列表</returns>
        public static Task<SessionIdentifier[]> GetSessionList()
        {
            return Handler.GetSessionList();
        }

        /// \~English
        /// <summary>
        /// Get total length of all sessions (not filtered)
        /// </summary>
        /// <returns>Total length of all sessions</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前层级下所有session的总时长（未筛选）
        /// </summary>
        /// <returns>所有session的总时长</returns>
        public static Task<double> GetSessionListTotalLength()
        {
            return Handler.GetSessionListTotalLength();
        }

        /// \~English
        /// <summary>
        /// Get session's properties
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Dictionary. The key is session's property title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取session的属性表
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session的属性表</returns>
        public static Task<Dictionary<String, String>> GetSessionProperties(SessionIdentifier session)
        {
            return Handler.GetSessionProperties(session);
        }

        /// \~English
        /// <summary>
        /// Get current session search key
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取当前的session搜索关键字
        /// </summary>
         public static Task<String> GetSessionSearchKey()
        {
            return Handler.GetSessionSearchKey();
        }

        /// \~English
        /// <summary>
        /// Get session's start time as a timeline point
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session's start time as a timeline point, null if the session doesn't exist or not belongs to current data layer</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定session在时间线上的开始时间点
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>在时间线上的开始时间点，session不存在或不属于当前层级则返回null</returns>
        public static Task<double?> GetSessionTimeline(SessionIdentifier session)
        {
            return Handler.GetSessionTimeline(session);
        }

        /// \~English
        /// <summary>
        /// Get signal's name
        /// </summary>
        /// <param name="signalID">Signal ID</param>
        /// <param name="fullName">Whether to return the full name</param>
        /// <returns>Signal's name, null if the signal doesn't exist</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定ID信号的名称
        /// </summary>
        /// <param name="signalID">信号ID</param>
        /// <param name="fullName">是否返回完整名称</param>
        /// <returns>信号名称，若无该ID信号则返回null</returns>
        public static Task<String> GetSignalName(String signalID, bool fullName)
        {
            return Handler.GetSignalName(signalID, fullName);
        }

        /// \~English
        /// <summary>
        /// Get all signal names of the bus message
        /// </summary>
        /// <param name="messageID">Bus message ID</param>
        /// <returns>Signal names, null if the bus message ID is invalid</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定报文ID下的所有信号名称
        /// </summary>
        /// <param name="messageID">报文的全局唯一ID</param>
        /// <returns>信号列表，若该报文不存在则返回null</returns>
        public static Task<String[]> GetSignalNamesOfBusMessage(String messageID)
        {
            return Handler.GetSignalNamesOfBusMessage(messageID);
        }

        /// \~English
        /// <summary>
        /// Get signal tree
        /// </summary>
        /// <returns>All child nodes under the root of tree</returns>
        /// \~Chinese
        /// <summary>
        /// 获取信号树
        /// </summary>
        /// <returns>信号树根节点下的所有子节点</returns>
        public static Task<SignalTreeNode[]> GetSignalTree()
        {
            return Handler.GetSignalTree();
        }

        /// \~English
        /// <summary>
        /// Get system status
        /// </summary>
        /// <param name="status">Type of system status</param>
        /// <returns>Information of system status, null if no available info</returns>
        /// \~Chinese
        /// <summary>
        /// 获取系统状态信息
        /// </summary>
        /// <param name="status">系统状态类别</param>
        /// <returns>系统状态信息，若无有效信息则返回null</returns>
        public static Task<String> GetSystemStatus(SystemStatus status)
        {
            return Handler.GetSystemStatus(status);
        }

        /// \~English
        /// <summary>
        /// Get details of system status
        /// </summary>
        /// <param name="status">Type of system status</param>
        /// <returns>Details of system status, null if no available info</returns>
        /// \~Chinese
        /// <summary>
        /// 获取系统状态详情
        /// </summary>
        /// <param name="status">系统状态类别</param>
        /// <returns>系统状态详情，若无有效信息则返回null</returns>
        public static Task<String> GetSystemStatusDetails(SystemStatus status)
        {
            return Handler.GetSystemStatusDetails(status);
        }

        /// \~English
        /// <summary>
        /// Get information of standalone task class
        /// </summary>
        /// <param name="taskClassID">Task class ID</param>
        /// <returns>Information of standalone task class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取独立任务组件信息
        /// </summary>
        /// <param name="taskClassID">独立任务组件ID</param>
        /// <returns>独立任务组件信息，若未找到返回null</returns>
        public static Task<TaskClassInfo> GetTaskClassInfo(String taskClassID)
        {
            return Handler.GetTaskClassInfo(taskClassID);
        }

        /// \~English
        /// <summary>
        /// Get information of all standalone task classes
        /// </summary>
        /// <returns>Dictionary. The key is task class ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取独立任务组件信息表
        /// </summary>
        /// <returns>独立任务组件信息表，键为组件ID</returns>
        public static Task<Dictionary<String, TaskClassInfo>> GetTaskClassTable()
        {
            return Handler.GetTaskClassTable();
        }

        /// \~English
        /// <summary>
        /// Convert time offset in session to UTC date and time
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">Time offset, in seconds</param>
        /// <param name="useGNSS">Whether to use satellite posix time model, or else use host machine posix time model</param>
        /// <returns>UTC date and time</returns>
        /// \~Chinese
        /// <summary>
        /// 计算session中某个时间偏置对应的UTC时间
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">时间偏置，单位秒</param>
        /// <param name="useGNSS">是否使用卫星Posix时间模型计算，否则使用主机Posix时间模型</param>
        /// <returns>对应的UTC时间</returns>
        public static Task<DateTime?> GetUTCDateTime(SessionIdentifier session, double timeOffset, bool useGNSS)
        {
            return Handler.GetUTCDateTime(session, timeOffset, useGNSS);
        }

        /// \~English
        /// <summary>
        /// Get application's version info
        /// </summary>
        /// <returns>Dictionary. The key is version title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取软件版本信息总表
        /// </summary>
        /// <returns>软件版本信息总表</returns>
        public static Task<Dictionary<String, Version>> GetVersionTable()
        {
            return Handler.GetVersionTable();
        }

        /// \~English
        /// <summary>
        /// Get time shift configuration for video raw data channel
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <returns>Time shift, in milliseconds</returns>
        /// \~Chinese
        /// <summary>
        /// 获取视频数据通道延迟配置
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <returns>延迟配置，单位毫秒</returns>
        public static Task<double> GetVideoChannelDelayConfig(int channel)
        {
            return Handler.GetVideoChannelDelayConfig(channel);
        }

        /// \~English
        /// <summary>
        /// Gets information about all video channels of a specified session
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Information about all video channels of this session. Return null if none exist</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定session的所有视频通道的信息
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>该session的所有视频通道的信息，若不存在则返回null</returns>
        public static Task<VideoChannelInfo[]> GetVideoChannelsInfo(SessionIdentifier session)
        {
            return Handler.GetVideoChannelsInfo(session);
        }

        /// \~English
        /// <summary>
        /// Get whether there's data in a video channel, and the interval and delay information
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <returns>1. Whether there's data. 2. The interval (time between frames) curve, in seconds. 3. The delay curve, in seconds</returns>
        /// \~Chinese
        /// <summary>
        /// 获取视频数据通道状态
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <returns>1. 是否有数据; 2. 每帧数据之间的时间间隔曲线，单位秒; 3. 每帧数据的延迟曲线，单位秒</returns>
        public static Task<(bool, double[], double[])> GetVideoChannelStatus(int channel, uint? toleranceMillisecond)
        {
            return Handler.GetVideoChannelStatus(channel, toleranceMillisecond);
        }

        /// \~English
        /// <summary>
        /// Get information of all video devices
        /// </summary>
        /// <returns>Dictionary. The key is video device ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取视频设备列表
        /// </summary>
        /// <returns>视频设备列表，键为设备ID，值为对应的设备信息</returns>
        public static Task<Dictionary<VideoDeviceID, VideoDeviceInfo>> GetVideoDevices()
        {
            return Handler.GetVideoDevices();
        }

        /// \~English
        /// <summary>
        /// Get the nearest video frame data from the specified time
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="timeline">Target timeline point, in seconds</param>
        /// <param name="maxGap">Max time gap, in seconds</param>
        /// <param name="mode">The mode to query video frame</param>
        /// <param name="clip">Based on the mode, image is further cropped to the raw coordinate system, at least 16x16, null means the full size</param>
        /// <param name="withAlpha">Whether to output image with alpha channel (Value fixed to 255)</param>
        /// <returns>1. Video frame data, size is determined by "mode" and "clip", null if failed to query. 2. Timestamp of output image, null if failed to query. 3. Camera information, null if failed to query</returns>
        /// \~Chinese
        /// <summary>
        /// 获取距离指定时间最近的视频帧数据
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="timeline">获取视频帧的目标时间线，单位秒</param>
        /// <param name="maxGap">容许的最大间隔，单位秒</param>
        /// <param name="mode">获取视频帧的模式</param>
        /// <param name="clip">在输出模式基础上进一步裁剪，为原始尺寸坐标系，至少为16x16，null表示完整输出</param>
        /// <param name="withAlpha">是否输出带Alpha通道的图像(固定赋值255)</param>
        /// <returns>1. 视频帧数据，图像实际大小由mode和clip决定，获取失败则返回null; 2. 图像的时间戳，获取失败则为null; 3. 摄像头信息，获取失败则为null</returns>
        public static Task<(CommonImage, Timestamp?, CameraInfo)> GetVideoFrameImage(int channel, double timeline, double maxGap, VideoFrameGetMode mode, IntRect? clip, bool withAlpha)
        {
            return Handler.GetVideoFrameImage(channel, timeline, maxGap, mode, clip, withAlpha);
        }

        /// \~English
        /// <summary>
        /// Get the nearest thumbnail data from the specified time
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="timeline">Target timeline point, in seconds</param>
        /// <param name="maxGap">Max time gap, in seconds</param>
        /// <param name="withAlpha">Whether to output image with alpha channel (Value fixed to 255)</param>
        /// <returns>Thumbnail data, null if failed to query. The width is fixed to 80</returns>
        /// \~Chinese
        /// <summary>
        /// 获取距离指定时间最近的缩略图数据
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="timeline">获取视频帧的目标时间线，单位秒</param>
        /// <param name="maxGap">容许的最大间隔，单位秒</param>
        /// <param name="withAlpha">是否输出带Alpha通道的图像(固定赋值255)</param>
        /// <returns>缩略图数据，图像宽度固定为80，获取失败则返回null</returns>
        public static Task<CommonImage> GetVideoFrameThumbnail(int channel, double timeline, double maxGap, bool withAlpha)
        {
            return Handler.GetVideoFrameThumbnail(channel, timeline, maxGap, withAlpha);
        }

        /// \~English
        /// <summary>
        /// Get the video raw size at the specified time at the specified channel
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="timeline">Target timeline point, in seconds</param>
        /// <returns>Raw size, null if no data</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定通道在指定时间上的视频帧的原始尺寸
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="timeline">获取视频帧的目标时间线，单位秒</param>
        /// <returns>原始尺寸，若无数据则返回null</returns>
        public static Task<IntSize?> GetVideoRawSize(int channel, double timeline)
        {
            return Handler.GetVideoRawSize(channel, timeline);
        }

        /// \~English
        /// <summary>
        /// Get the type of special camera (if any)
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <returns>Type of special camera</returns>
        /// \~Chinese
        /// <summary>
        /// 获取视频通道的特殊摄像头类型
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <returns>特殊摄像头类型</returns>
        public static Task<Samples.SpecialCameraType> GetVideoSpecialType(int channel)
        {
            return Handler.GetVideoSpecialType(channel);
        }

        /// \~English
        /// <summary>
        /// Get whether the bus message is bound
        /// </summary>
        /// <param name="busMessageID">Bus message ID</param>
        /// <returns>Whether the bus message is bound</returns>
        /// \~Chinese
        /// <summary>
        /// 获取总线报文是否已绑定发送
        /// </summary>
        /// <param name="busMessageID">总线报文ID</param>
        /// <returns>是否已绑定</returns>
        public static Task<bool> IsBusMessageBound(string busMessageID)
        {
            return Handler.IsBusMessageBound(busMessageID);
        }

        /// \~English
        /// <summary>
        /// Get whether file writing is enabled for online acquisition and offline processing
        /// </summary>
        /// <returns>Whether file writing is enabled for online acquisition and offline processing</returns>
        /// \~Chinese
        /// <summary>
        /// 检查是否为在线采集或离线处理生成模式
        /// </summary>
        /// <returns>是否为在线采集或离线处理生成模式</returns>
        public static Task<bool> IsFileOutputEnabled()
        {
            return Handler.IsFileOutputEnabled();
        }

        /// \~English
        /// <summary>
        /// Get whether the sample channel is available (data output by other component)
        /// </summary>
        /// <param name="channelID">Sample channel ID, with format as "protocol@channel". Channel is starting from 0. Protocol with "v" version number is with backward compatibility</param>
        /// <returns>Whether the sample channel is available</returns>
        /// \~Chinese
        /// <summary>
        /// 检查指定的输入样本通道是否可用
        /// </summary>
        /// <param name="channelID">样本通道关键字，格式为"协议名@通道序号"，通道序号从0开始，协议名中带"v"字版本号的可向下兼容</param>
        /// <returns>该通道是否可用</returns>
        public static Task<bool> IsInputChannelAvailable(String channelID)
        {
            return Handler.IsInputChannelAvailable(channelID);
        }

        /// \~English
        /// <summary>
        /// Get whether Internet is connected
        /// </summary>
        /// <returns>Whether Internet is connected</returns>
        /// \~Chinese
        /// <summary>
        /// 当前互联网是否已连接
        /// </summary>
        /// <returns>是否已连接互联网</returns>
        public static Task<bool> IsInternetConnected()
        {
            return Handler.IsInternetConnected();
        }

        /// \~English
        /// <summary>
        /// Whether the bus message ID is valid, for example, it's invalid if corresponding dbc file isn't loaded correctly
        /// </summary>
        /// <param name="messageID">Bus message ID</param>
        /// <param name="optional">Whether it can be null</param>
        /// <returns>Whether the bus message ID is valid</returns>
        /// \~Chinese
        /// <summary>
        /// 查看报文配置是否有效，如dbc文件加载不正确则可能导致无效
        /// </summary>
        /// <param name="messageID">报文的全局唯一ID</param>
        /// <param name="optional">是否为可选配置</param>
        /// <returns>是否有效</returns>
        public static Task<bool> IsMessageValid(String messageID, bool optional)
        {
            return Handler.IsMessageValid(messageID, optional);
        }

        /// \~English
        /// <summary>
        /// Get whether to use PRC web service
        /// </summary>
        /// <returns>Whether to use PRC web service</returns>
        /// \~Chinese
        /// <summary>
        /// 获取是否使用境内网络服务
        /// </summary>
        /// <returns>是否使用境内网络服务</returns>
        public static Task<bool> IsPRCWebPreferred()
        {
            return Handler.IsPRCWebPreferred();
        }

        /// \~English
        /// <summary>
        /// Get whether the system is ready for saving project file, starting session, and output the reason why it's not ready
        /// </summary>
        /// <returns>1. Whether the system is ready for saving project file, starting session, etc. 2. The ready why the system is not ready, empty means unknown</returns>
        /// \~Chinese
        /// <summary>
        /// 返回是否允许进行保存工程项目和开始session等操作，若不允许则输出繁忙原因
        /// </summary>
        /// <returns>1. 是否允许进行保存工程项目和开始session等操作; 2. 系统繁忙原因，空表示原因未知</returns>
        public static Task<(bool, String)> IsReady()
        {
            return Handler.IsReady();
        }

        /// \~English
        /// <summary>
        /// Get whether there're multiple components outputting data with the same sample channel ID
        /// </summary>
        /// <param name="channelID">Sample channel ID, with format as "protocol@channel". Channel is starting from 0. Protocol with "v" version number is with backward compatibility</param>
        /// <returns>Whether conflict exists</returns>
        /// \~Chinese
        /// <summary>
        /// 检查样本数据通道是否冲突（有多个组件输出相同协议和通道的样本数据）
        /// </summary>
        /// <param name="channelID">样本通道协议，格式为"协议名@通道序号"，通道序号从0开始，协议名中带"v"字版本号的可向下兼容</param>
        /// <returns>是否冲突</returns>
        public static Task<bool> IsSampleChannelConflict(string channelID)
        {
            return Handler.IsSampleChannelConflict(channelID);
        }

        /// \~English
        /// <summary>
        /// Whether the signal ID is valid, for example, it's invalid if corresponding dbc file or plugin file isn't loaded correctly
        /// </summary>
        /// <param name="signalID">Signal ID</param>
        /// <param name="optional">Whether it can be null</param>
        /// <returns>Whether the signal ID is valid</returns>
        /// \~Chinese
        /// <summary>
        /// 查看信号配置是否有效，如dbc文件加载不正确，插件文件未加载都可能导致无效
        /// </summary>
        /// <param name="signalID">信号的全局唯一ID</param>
        /// <param name="optional">是否为可选配置</param>
        /// <returns>是否有效</returns>
        public static Task<bool> IsSignalValid(String signalID, bool optional)
        {
            return Handler.IsSignalValid(signalID, optional);
        }

        /// \~English
        /// <summary>
        /// Get whether there's data in the specified video channel
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="tolerance">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <returns>Whether there's data in the specified video channel</returns>
        /// \~Chinese
        /// <summary>
        /// 查看指定视频通道是否有数据可供显示
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="tolerance">可容忍最近多少毫秒（现实时间）无数据</param>
        /// <returns>是否有数据可供显示</returns>
        public static Task<bool> IsVideoDataAvailable(int channel, uint? tolerance)
        {
            return Handler.IsVideoDataAvailable(channel, tolerance);
        }

        /// \~English
        /// <summary>
        /// Parse bus message data, and get signal values
        /// </summary>
        /// <param name="busMessage">Bus message data</param>
        /// <returns>Signal values and other information</returns>
        /// \~Chinese
        /// <summary>
        /// 解析总线报文，获取信号值等信息
        /// </summary>
        /// <param name="busMessage">总线报文</param>
        /// <returns>所有信号值及相关信息</returns>
        public static Task<BusSignalValue[]> ParseBusMessage(BusMessageSample busMessage)
        {
            return Handler.ParseBusMessage(busMessage);
        }

        /// \~English
        /// <summary>
        /// Public data
        /// </summary>
        /// <param name="dataID">Data ID, should not be null or empty</param>
        /// <param name="data">Binary data, should not be null</param>
        /// \~Chinese
        /// <summary>
        /// 发布数据
        /// </summary>
        /// <param name="dataID">数据ID，不可为null或空字符串</param>
        /// <param name="data">数据，不可为null</param>
        public static Task PublishData(String dataID, byte[] data)
        {
            return Handler.PublishData(dataID, data);
        }

        /// \~English
        /// <summary>
        /// Refresh all the generations of the current sessions
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 刷新当前层级下所有session的generation
        /// </summary>
        public static Task RefreshGenerations()
        {
            return Handler.RefreshGenerations();
        }

        /// \~English
        /// <summary>
        /// Refresh the sessions under the current layer
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 刷新当前层级下所有session
        /// </summary>
        public static Task RefreshSessions()
        {
            return Handler.RefreshSessions();
        }

        /// \~English
        /// <summary>
        /// Remove event
        /// </summary>
        /// <param name="eventHandle">Event handle</param>
        /// \~Chinese
        /// <summary>
        /// 移除指定事件对象
        /// </summary>
        /// <param name="eventHandle">事件对象</param>
        public static Task RemoveEvent(object eventHandle)
        {
            return Handler.RemoveEvent(eventHandle);
        }

        /// \~English
        /// <summary>
        /// Remove generation and delete files to recycle bin
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="genID">Generation ID</param>
        /// \~Chinese
        /// <summary>
        /// 移除generation及相关文件至回收站
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="genID">Generation ID</param>
        public static Task RemoveGeneration(SessionIdentifier session, String genID)
        {
            return Handler.RemoveGeneration(session, genID);
        }

        /// \~English
        /// <summary>
        /// Remove session and delete files to recycle bin
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="force">Whether forced to remove</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 移除session及相关文件至回收站
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="force">是否强制移除</param>
        /// <returns>是否成功移除</returns>
        public static Task<bool> RemoveSession(SessionIdentifier session, bool force)
        {
            return Handler.RemoveSession(session, force);
        }

        /// \~English
        /// <summary>
        /// Remove signal reference
        /// </summary>
        /// <param name="signalID">Signal ID</param>
        /// \~Chinese
        /// <summary>
        /// 移除信号引用
        /// </summary>
        /// <param name="signalID">信号ID</param>
        public static Task RemoveSignalReference(String signalID)
        {
            return Handler.RemoveSignalReference(signalID);
        }

        /// \~English
        /// <summary>
        /// Reset GPU decoder test results
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 重置GPU解码测试结果
        /// </summary>
        public static Task ResetGPUDecoderTestResults()
        {
            return Handler.ResetGPUDecoderTestResults();
        }

        /// \~English
        /// <summary>
        /// Run console procedure
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="consoleClassID">Console class ID</param>
        /// \~Chinese
        /// <summary>
        /// 运行控制台过程
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="consoleClassID">控制台组件ID</param>
        public static Task RunConsole(object caller, string consoleClassID)
        {
            return Handler.RunConsole(caller, consoleClassID);
        }

        /// \~English
        /// <summary>
        /// Run a standalone task, only available while idle
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="taskClassID">Task class ID</param>
        /// <param name="config">Configuration string</param>
        /// <returns>1. Result of the task. 2. Return value string</returns>
        /// \~Chinese
        /// <summary>
        /// 运行一个独立处理任务，仅限空闲时运行
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="taskClassID">任务组件的类别ID</param>
        /// <param name="config">配置的字符串描述</param>
        /// <returns>1. 任务运行结果; 2. 任务的返回值信息</returns>
        public static Task<(TaskResult, String)> RunStandaloneTask(object caller, String taskClassID, String config)
        {
            return Handler.RunStandaloneTask(caller, taskClassID, config);
        }

        /// \~English
        /// <summary>
        /// Transmit bus message, either periodically or once (only available in online mode)
        /// </summary>
        /// <param name="message">The bus message</param>
        /// \~Chinese
        /// <summary>
        /// 发送总线报文，可周期性发送，也可单次发送（仅在线模式可用）
        /// </summary>
        /// <param name="message">想要发送的报文信息</param>
        public static Task SendBusMessage(BusMessage message)
        {
            return Handler.SendBusMessage(message);
        }

        /// \~English
        /// <summary>
        /// Transmit bound bus message, either periodically or once (only available in online mode)
        /// </summary>
        /// <param name="messageID">Message ID of bound bus message</param>
        /// <param name="interval">Transmit period, in milliseconds (at least 10). If it is set to null, it is transmitted only once</param>
        /// <returns>Generated bus message data, null if not bound</returns>
        /// \~Chinese
        /// <summary>
        /// 发送总线报文（该报文需设置绑定），可周期性发送，也可单次发送（仅在线模式可用）
        /// </summary>
        /// <param name="messageID">绑定的报文ID</param>
        /// <param name="interval">报文发送周期，单位毫秒（至少为10），若设为null则只发送一次</param>
        /// <returns>输出生成的报文数据，若未绑定则输出null</returns>
        public static Task<byte[]> SendBusMessageBound(String messageID, uint? interval)
        {
            return Handler.SendBusMessageBound(messageID, interval);
        }

        /// \~English
        /// <summary>
        /// Send manual trigger signal
        /// </summary>
        /// <param name="channel">Channel of manual trigger, ranges 0~15</param>
        /// \~Chinese
        /// <summary>
        /// 发送手动触发器信号
        /// </summary>
        /// <param name="channel">手动触发器通道，0~15</param>
        public static Task SendManualTrigger(int channel)
        {
            return Handler.SendManualTrigger(channel);
        }

        /// \~English
        /// <summary>
        /// Transmitted received general raw data (only available in online mode)
        /// </summary>
        /// <param name="channelID">General raw data's channel ID, corresponding to the first column of input/raw/raw.csv</param>
        /// <param name="values">Numeric data</param>
        /// <param name="binary">Binary data</param>
        /// \~Chinese
        /// <summary>
        /// 发送已获取的原始数据信息（仅在线模式可用）
        /// </summary>
        /// <param name="channelID">原始数据协议名称，对应input/raw/raw.csv首列文字</param>
        /// <param name="values">数值数据</param>
        /// <param name="binary">二进制数据</param>
        public static Task SendRawData(String channelID, double[] values, byte[] binary)
        {
            return Handler.SendRawData(channelID, values, binary);
        }

        /// \~English
        /// <summary>
        /// Set time shift for audio data channel
        /// </summary>
        /// <param name="delay">Time shift, in milliseconds</param>
        /// \~Chinese
        /// <summary>
        /// 设置音频数据通道延迟配置
        /// </summary>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static Task SetAudioChannelDelayConfig(double delay)
        {
            return Handler.SetAudioChannelDelayConfig(delay);
        }

        /// \~English
        /// <summary>
        /// Set time shift for bus raw data channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <param name="delay">Time shift, in milliseconds</param>
        /// \~Chinese
        /// <summary>
        /// 设置总线数据通道延迟配置
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static Task SetBusChannelDelayConfig(int channel, double delay)
        {
            return Handler.SetBusChannelDelayConfig(channel, delay);
        }

        /// \~English
        /// <summary>
        /// Set whether the channel is configured as guest synchronized
        /// </summary>
        /// <param name="id">Guest synchronization ID, like bus.1, video.0, xxx.yyy(xxx is native plugin's type ID，yyy is channel name)</param>
        /// <param name="guestSync">Whether the channel is configured as guest synchronized</param>
        /// \~Chinese
        /// <summary>
        /// 设置指定通道是否配置为客机同步
        /// </summary>
        /// <param name="id">客机同步ID，如bus.1, video.0, xxx.yyy(xxx为原生插件类型ID，yyy为通道名称)等</param>
        /// <param name="guestSync">是否配置为客机同步</param>
        public static Task SetChannelGuestSyncFlag(String id, bool guestSync)
        {
            return Handler.SetChannelGuestSyncFlag(id, guestSync);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.14) Set whether to monitor that there's data in the specified channel
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0, etc.</param>
        /// <param name="monitoring">Whether to monitor (The function should be implemented by plugins, like audio alarm, UI flashing, etc.)</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.14) 设置是否监控指定通道有无数据
        /// </summary>
        /// <param name="id">监控ID，如：bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0等</param>
        /// <param name="monitoring">是否监控有无数据，通道监控的具体实现应由插件给出，如发出报警音、指示灯闪烁等</param>
        public static Task SetChannelMonitoringFlag(String id, bool monitoring)
        {
            return Handler.SetChannelMonitoringFlag(id, monitoring);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.14) Set whether to monitor that the specified channel's data is synchronized with time server
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, sample@xxx-v2@0, etc.</param>
        /// <param name="monitoring">Whether to monitor (The function should be implemented by plugins, like audio alarm, UI flashing, etc.)</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.14) 设置是否监控指定通道数据与授时服务器同步
        /// </summary>
        /// <param name="id">监控ID，如bus@1, video@0, sample@xxx-v2@0等</param>
        /// <param name="monitoring">是否监控数据与授时服务器同步，通道监控的具体实现应由插件给出，如发出报警音、指示灯闪烁等</param>
        public static Task SetChannelServerSyncMonitoringFlag(String id, bool monitoring)
        {
            return Handler.SetChannelServerSyncMonitoringFlag(id, monitoring);
        }

        /// \~English
        /// <summary>
        /// Enable or disable exclusive control mode (While enabled, other controls will be ignored)
        /// </summary>
        /// <param name="controllerName">Controller name, as ID</param>
        /// <param name="enabled">Whether to enable exclusive control mode</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 设置独占控制模式（在独占控制模式下，其他控制者的操作将被禁用）
        /// </summary>
        /// <param name="controllerName">控制者名称，即ID</param>
        /// <param name="enabled">是否开启独占控制模式</param>
        /// <returns>开启或关闭是否成功</returns>
        public static Task<bool> SetControlFlag(String controllerName, bool enabled)
        {
            return Handler.SetControlFlag(controllerName, enabled);
        }

        /// \~English
        /// <summary>
        /// Set current data layer
        /// </summary>
        /// <param name="layer">The current data layer, while null means all layers, '.' means the layer of sessions under data path, '..' means the layer of data path which is session</param>
        /// \~Chinese
        /// <summary>
        /// 设置当前的数据层级
        /// </summary>
        /// <param name="layer">数据层级，其中null表示所有层级，'.'表示根路径下的session，'..'表示根路径即session</param>
        public static Task SetCurrentDataLayer(String layer)
        {
            return Handler.SetCurrentDataLayer(layer);
        }

        /// \~English
        /// <summary>
        /// Set event's comment
        /// </summary>
        /// <param name="eventHandle">Event handle</param>
        /// <param name="comment">Comment of event</param>
        /// \~Chinese
        /// <summary>
        /// 设置指定事件的注释
        /// </summary>
        /// <param name="eventHandle">事件对象</param>
        /// <param name="comment">事件注释</param>
        public static Task SetEventComment(object eventHandle, String comment)
        {
            return Handler.SetEventComment(eventHandle, comment);
        }

        /// \~English
        /// <summary>
        /// Set value of global parameter
        /// </summary>
        /// <param name="key">Key of global parameter, do nothing if it's null or empty</param>
        /// <param name="value">Value of global parameter, do nothing if it's null</param>
        /// \~Chinese
        /// <summary>
        /// 设置全局参数
        /// </summary>
        /// <param name="key">全局参数key，若为null或""则忽略</param>
        /// <param name="value">全局参数value，若为null则忽略</param>
        public static Task SetGlobalParameter(String key, String value)
        {
            return Handler.SetGlobalParameter(key, value);
        }

        /// \~English
        /// <summary>
        /// Set value of global variable
        /// </summary>
        /// <param name="key">Key of global variable, do nothing if it's null or empty</param>
        /// <param name="value">Value of global variable, do nothing if it's null</param>
        /// \~Chinese
        /// <summary>
        /// 设置全局变量
        /// </summary>
        /// <param name="key">全局变量key，若为null或""则忽略</param>
        /// <param name="value">全局变量value，若为null则忽略</param>
        public static Task SetGlobalVariable(String key, String value)
        {
            return Handler.SetGlobalVariable(key, value);
        }

        /// \~English
        /// <summary>
        /// Set interest with timeline point
        /// </summary>
        /// <param name="targetTimeline">Timeline point</param>
        /// \~Chinese
        /// <summary>
        /// 按时间线设置兴趣点
        /// </summary>
        /// <param name="targetTimeline">时间线</param>
        public static Task SetInterestTime(double targetTimeline)
        {
            return Handler.SetInterestTime(targetTimeline);
        }

        /// \~English
        /// <summary>
        /// Set interest with host machine's date and time
        /// </summary>
        /// <param name="targetTimestamp">Host machine's date and time</param>
        /// \~Chinese
        /// <summary>
        /// 按本地日期时间设置兴趣点
        /// </summary>
        /// <param name="targetTimestamp">本地日期时间</param>
        public static Task SetInterestTimestamp(DateTime targetTimestamp)
        {
            return Handler.SetInterestTimestamp(targetTimestamp);
        }

        /// \~English
        /// <summary>
        /// Set the name of manual trigger at a channel
        /// </summary>
        /// <param name="index">Channel index, ranges 0~15</param>
        /// <param name="name">Name of manual trigger, do nothing if it's null</param>
        /// \~Chinese
        /// <summary>
        /// 设定手动触发器通道的名称
        /// </summary>
        /// <param name="index">通道序号，0~15</param>
        /// <param name="name">手动触发器通道的名称，若值为空则忽略</param>
        public static Task SetManualTriggerName(int index, String name)
        {
            return Handler.SetManualTriggerName(index, name);
        }

        /// \~English
        /// <summary>
        /// Set processor/native/device component's configuration string
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// <param name="config">Configuration string</param>
        /// \~Chinese
        /// <summary>
        /// 设置数据处理/原生/设备组件配置的字符串描述
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        /// <param name="config">配置的字符串描述</param>
        public static Task SetModuleConfig(object caller, String classID, String config)
        {
            return Handler.SetModuleConfig(caller, classID, config);
        }

        /// \~English
        /// <summary>
        /// Set time shift for general raw data channel
        /// </summary>
        /// <param name="id">General raw data's channel ID</param>
        /// <param name="delay">Time shift, in milliseconds</param>
        /// \~Chinese
        /// <summary>
        /// 设置原始数据通道延迟配置
        /// </summary>
        /// <param name="id">原始数据通道ID</param>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static Task SetRawChannelDelayConfig(String id, double delay)
        {
            return Handler.SetRawChannelDelayConfig(id, delay);
        }

        /// \~English
        /// <summary>
        /// Set session's check filter
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="check">Whether checked</param>
        /// \~Chinese
        /// <summary>
        /// 设置session是否框选
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="check">是否框选</param>
        public static Task SetSessionChecker(SessionIdentifier session, bool check)
        {
            return Handler.SetSessionChecker(session, check);
        }

        /// \~English
        /// <summary>
        /// Set session's comment
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="comment">Session's comment</param>
        /// \~Chinese
        /// <summary>
        /// 设置session的注释说明
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="comment">Session的注释说明</param>
        public static Task SetSessionComment(SessionIdentifier session, String comment)
        {
            return Handler.SetSessionComment(session, comment);
        }

        /// \~English
        /// <summary>
        /// Set whether the host machine is synchronized with time server while the session is being recorded
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="hostSync">Whether the host machine is synchronized with time server</param>
        /// \~Chinese
        /// <summary>
        /// 设置session采集时主机是否与授时服务器同步
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="hostSync">主机是否与授时服务器同步</param>
        public static Task SetSessionHostSync(SessionIdentifier session, bool hostSync)
        {
            return Handler.SetSessionHostSync(session, hostSync);
        }

        /// \~English
        /// <summary>
        /// Set session's properties
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="properties">Dictionary. The key is session's property title</param>
        /// \~Chinese
        /// <summary>
        /// 设置session的属性表
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="properties">Session的属性表</param>
        public static Task SetSessionProperties(SessionIdentifier session, Dictionary<String, String> properties)
        {
            return Handler.SetSessionProperties(session, properties);
        }

        /// \~English
        /// <summary>
        /// Set search keyword for session filtering
        /// </summary>
        /// <param name="keyword">Search keyword for session filtering</param>
        /// \~Chinese
        /// <summary>
        /// 设置session搜索关键字
        /// </summary>
        /// <param name="keyword">session搜索关键字</param>
        public static Task SetSessionSearchKeyword(String keyword)
        {
            return Handler.SetSessionSearchKeyword(keyword);
        }

        /// \~English
        /// <summary>
        /// Set target replay speed for replaying or offline processing
        /// </summary>
        /// <param name="speed">Target replay speed</param>
        /// \~Chinese
        /// <summary>
        /// 设置目标回放速度，用于回放或离线处理
        /// </summary>
        /// <param name="speed">目标回放速度</param>
        public static Task SetTargetReplaySpeed(double speed)
        {
            return Handler.SetTargetReplaySpeed(speed);
        }

        /// \~English
        /// <summary>
        /// Set time shift for video raw data channel
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="delay">Time shift, in milliseconds</param>
        /// \~Chinese
        /// <summary>
        /// 设置视频数据通道延迟配置
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static Task SetVideoChannelDelayConfig(int channel, double delay)
        {
            return Handler.SetVideoChannelDelayConfig(channel, delay);
        }

        /// \~English
        /// <summary>
        /// Switch to offline processing mode and start
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="previewOnly">Whether previewing, otherwise generating</param>
        /// <param name="genDirName">The folder name for generation recording, can be null (If the folder already exists and "force" is true, it will use default date and time format)</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 切换至离线模式并开始预览或后处理
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <param name="genDirName">后处理输出时，写入generation数据的文件夹名，可为null（若已存在且强制开始时，则使用默认的日期格式）</param>
        /// <returns>是否成功</returns>
        public static Task<bool> StartOffline(bool force, bool previewOnly, String genDirName)
        {
            return Handler.StartOffline(force, previewOnly, genDirName);
        }

        /// \~English
        /// <summary>
        /// Switch to online mode and start
        /// </summary>
        /// <param name="controllerName">Controller name, for exclusive control</param>
        /// <param name="previewOnly">Whether previewing, otherwise recording</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 切换至在线模式并开始预览或采集
        /// </summary>
        /// <param name="controllerName">控制者名称，用于独占控制模式</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <returns>是否成功</returns>
        public static Task<bool> StartOnlineWithController(String controllerName, bool previewOnly)
        {
            return Handler.StartOnlineWithController(controllerName, previewOnly);
        }

        /// \~English
        /// <summary>
        /// Switch to online mode and start
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="previewOnly">Whether previewing, otherwise recording</param>
        /// <param name="sessionDirName">The folder name for session recording, can be null (If the folder already exists and "force" is true, it will use default date and time format)</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 切换至在线模式并开始预览或采集
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <param name="sessionDirName">采集时，写入session数据的文件夹名，可为null（若已存在且强制开始时，则使用默认的日期格式）</param>
        /// <returns>是否成功</returns>
        public static Task<bool> StartOnline(bool force, bool previewOnly, String sessionDirName)
        {
            return Handler.StartOnline(force, previewOnly, sessionDirName);
        }

        /// \~English
        /// <summary>
        /// Switch to remote mode and start
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="previewOnly">Whether previewing, otherwise recording</param>
        /// <param name="sessionDirName">The folder name for session recording, can be null (If the folder already exists and "force" is true, it will use default date and time format)</param>
        /// <param name="startPosixTime">Start time on the remote machine, in posix milliseconds</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 切换至远程模式并开始预览或采集
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <param name="sessionDirName">采集时，写入session数据的文件夹名，可为null（若已存在且强制开始时，则使用默认的日期格式，时间为本机时间，非远程主机时间）</param>
        /// <param name="startPosixTime">远程主机的开始时间，单位毫秒</param>
        /// <returns>是否成功</returns>
        public static Task<bool> StartRemote(bool force, bool previewOnly, String sessionDirName, ulong startPosixTime)
        {
            return Handler.StartRemote(force, previewOnly, sessionDirName, startPosixTime);
        }

        /// \~English
        /// <summary>
        /// Switch to remote mode and start
        /// </summary>
        /// <param name="controllerName">Controller name, for exclusive control</param>
        /// <param name="previewOnly">Whether previewing, otherwise recording</param>
        /// <param name="startPosixTime">Start time on the remote machine, in posix milliseconds</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 切换至远程模式并开始预览或采集
        /// </summary>
        /// <param name="controllerName">控制者名称，用于独占控制模式</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <param name="startPosixTime">远程主机的开始时间，单位毫秒</param>
        /// <returns>是否成功</returns>
        public static Task<bool> StartRemoteWithController(String controllerName, bool previewOnly, ulong startPosixTime)
        {
            return Handler.StartRemoteWithController(controllerName, previewOnly, startPosixTime);
        }

        /// \~English
        /// <summary>
        /// Switch to replay mode and start replay
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="startTimeline">The timeline point from which to start replay, in seconds</param>
        /// <param name="interestTarget">The timeline point of replay target, in seconds (null means replaying to end of timeline)</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 切换至回放模式并开始回放
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="startTimeline">时间线上的回放开始时间，单位秒</param>
        /// <param name="interestTarget">时间线上的目标兴趣点，单位秒（空表示不设置兴趣点）</param>
        /// <returns>是否成功</returns>
        public static Task<bool> StartReplay(bool force, double startTimeline, double? interestTarget)
        {
            return Handler.StartReplay(force, startTimeline, interestTarget);
        }

        /// \~English
        /// <summary>
        /// Stop the session
        /// </summary>
        /// <param name="controllerName">Controller name, for exclusive control</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 停止Session
        /// </summary>
        /// <param name="controllerName">控制者名称，用于独占控制模式</param>
        /// <returns>是否成功</returns>
        public static Task<bool> StopRunningWithController(String controllerName)
        {
            return Handler.StopRunningWithController(controllerName);
        }

        /// \~English
        /// <summary>
        /// Stop the session
        /// </summary>
        /// <param name="force">Whether forced to stop</param>
        /// <param name="editRecordedSession">Whether to edit the session's info right after the session's stopped successfully</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 停止Session
        /// </summary>
        /// <param name="force">是否强制结束</param>
        /// <param name="editRecordedSession">成功停止后是否立即编辑session信息</param>
        /// <returns>是否成功</returns>
        public static Task<bool> StopRunning(bool force, bool editRecordedSession)
        {
            return Handler.StopRunning(force, editRecordedSession);
        }

        /// \~English
        /// <summary>
        /// Subscribe data from other app layer components
        /// </summary>
        /// <param name="dataID">Data ID, should not be null or empty</param>
        /// <param name="bufferLength">Buffer length, ranges 1~1000</param>
        /// <param name="timeout">The subscription will be closed if no dequeue for a along time, ranges 10～600 in seconds</param>
        /// <returns>Data subscriber object, null if failed to initialize</returns>
        /// \~Chinese
        /// <summary>
        /// 订阅来自应用层其他组件的数据
        /// </summary>
        /// <param name="dataID">数据ID，不可为null或空字符串</param>
        /// <param name="bufferLength">缓存长度，范围在1~1000</param>
        /// <param name="timeout">超过该时间不取出缓存数据则自动关闭订阅，单位秒，范围在10～600</param>
        /// <returns>数据订阅对象，若初始化失败则返回null</returns>
        public static Task<DataSubscriber> SubscribeData(String dataID, int bufferLength, int timeout)
        {
            return Handler.SubscribeData(dataID, bufferLength, timeout);
        }

        /// \~English
        /// <summary>
        /// Switch application's mode
        /// </summary>
        /// <param name="controllerName">Controller name, for exclusive control</param>
        /// <param name="mode">Target mode</param>
        /// <param name="waitSecond">Timeout in seconds, only available while larger than 0</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 切换应用程序当前的运行模式
        /// </summary>
        /// <param name="controllerName">控制者名称，用于独占控制模式</param>
        /// <param name="mode">目标运行模式</param>
        /// <param name="waitSecond">超时，大于0有效，单位秒</param>
        /// <returns>是否成功切换</returns>
        public static Task<bool> SwitchAppMode(String controllerName, ApplicationMode mode, int waitSecond)
        {
            return Handler.SwitchAppMode(controllerName, mode, waitSecond);
        }
    }
}
