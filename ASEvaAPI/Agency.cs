using System;
using System.Collections.Generic;
using ASEva.Samples;
using ASEva.Utility;

namespace ASEva
{
    #pragma warning disable CS1571
    
    public interface AgencyHandler
    {
        String GetAppID();
        String GetConfigFilesRoot();
        String GetAppFilesRoot();
        String GetTempFilesRoot();
        String[] GetGlobalVariableKeys();
        String[] GetGlobalParameterKeys();
        String[] GetGlobalPathKeys();
        void SetGlobalVariable(String key, String value);
        String GetGlobalVariable(String key, String defaultValue);
        void SetGlobalParameter(String key, String value);
        String GetGlobalParameter(String key, String defaultValue);
        String GetGlobalPath(String key);
        void Print(String text);
        void Log(String text, LogLevel level);
        ApplicationStatus GetAppStatus();
        ApplicationMode GetAppMode();
        bool IsFileOutputEnabled();
        String GetAppLanguage();
        GraphData GetGraphData(DateTime session, int graphID);
        void AddSceneData(SceneData scene);
        List<String> GetSampleTitle(String channelID);
        DateTime? GetStartTimeLocal(DateTime session);
        DateTime? GetStartTimeUTC(DateTime session);
        DateTime? GetTimestampLocal(DateTime session, double timeOffset);
        DateTime? GetTimestampUTC(DateTime session, double timeOffset);
        double GetTimeRatioToLocal(DateTime session);
        double GetTimeRatioToUTC(DateTime session);
        void AddSignalReference(String signalID);
        void RemoveSignalReference(String signalID);
        bool DeleteToRecycleBin(String path);
        bool IsMessageValid(String messageID, bool optional);
        bool IsSignalValid(String signalID, bool optional);
        BusMessageInfo GetBusMessageInfo(String busMessageID);
        BusSignalInfo GetBusSignalInfo(String busSignalID);
        BusProtocolFileID[] GetBusProtocolFileIDList();
        String GetBusProtocolFilePath(BusProtocolFileID fileID);
        bool UpdateBusProtocolFilePath(BusProtocolFileID fileID, String filePath);
        BusProtocolFileState GetBusProtocolFileState(BusProtocolFileID fileID);
        bool AddBusProtocolFile(String filePath, out BusProtocolFileID fileID);
        AddBusProtocolResult AddBusProtocolFile(String filePath);
        void RemoveBusProtocolFile(BusProtocolFileID fileID);
        String GetChannelAliasName(String channelID);
        Dictionary<String, String> GetChannelAliasTable();
        bool IsInputChannelAvailable(String channelID);
        BusSignalValue[] ParseBusMessage(BusMessageSample busMessage);
        void SendBusMessage(BusMessage msg);
        void SendBusMessage(String messageID, uint? interval);
        void SendBusMessage(String messageID, uint? interval, out byte[] data);
        void SendRawData(String channelID, double[] values, byte[] binary);
        void SendRawData(ulong cpuTick, String channelID, double[] values, byte[] binary);
        void SendManualTrigger(int channel);
        String GetDataPath();
        String[] GetSubDataPaths();
        String[] GetDataLayers();
        String GetGlobalPublicDataPath();
        String[] GetGenerationList();
        DateTime[] GetSessionList();
        DateTime[] GetFilteredSessionList();
        String GetSessionPath(DateTime session);
        String GetSessionPublicDataPath(DateTime session);
        String GetSessionLayer(DateTime session);
        String GetSessionFolderName(DateTime session);
        String GetGenerationPath(DateTime session, String generation);
        GenerationProcessStatus? GetGenerationProcessStatus(DateTime session, String generation);
        DateTime[] GetFinishedSessions(String generation);
        void AddDataLayer(String layer);
        void DeleteDataLayer(String layer);
        String GetCurrentDataLayer();
        String GetCurrentDataLayerPath();
        void SetCurrentDataLayer(String layer);
        void RefreshSessions();
        void RefreshGenerations();
        String GetCurrentDataGeneration();
        String GetCurrentSessionGUID();
        double GetInterestTime();
        DateTime? GetInterestTimestamp();
        void SetInterestTime(double timeline);
        void SetInterestTimestamp(DateTime timestamp);
        BufferRange GetBufferRange();
        GeneralDeviceStatus GetDeviceStatus(String type);
        GeneralDeviceStatus[] GetChildDeviceStatus(String type);
        String[] GetSignalNamesOfBusMessage(String messageID);
        ulong GetMemoryCapacity();
        bool IsInternetConnected();
        void CallWebApi(String request, WebApiContext context);
        void CallWebApiPost(String request, byte[] body, WebApiContext context);
        void CallWebApiPost(String request, byte[] body, WebPostContentType type, WebApiContext context);
        String[] GetManualTriggerNames();
        String GetManualTriggerName(int index);
        void SetManualTriggerName(int index, String name);
        TaskResult RunStandaloneTask(object caller, String taskClassID, String config, out String returnValue);
        void SetGlobalPath(String key, String path);
        BusFileInfo[] GetBusProtocolFilesInfo();
        int? GetBusProtocolFileChannel(String protocolName);
        String[] GetBusFloat32Signals();
        float GetBusMessageFPS(int channel, uint localID);
        BusMessageInfo GetBusMessageInfo(int channel, uint localID);
        bool IsBusMessageBound(String busMessageID);
        object[] GetEventHandles();
        EventInfo GetEventInfo(object eventHandle);
        void RemoveEvent(object eventHandle);
        void SetEventComment(object eventHandle, String comment);
        void StartReplay(double startTimeline, double? interestTarget);
        bool StartReplay(bool force, double startTimeline, double? interestTarget);
        bool StartOnline(String controllerName, bool previewOnly);
        bool StartOnline(bool force, bool previewOnly);
        bool StartOnline(bool force, bool previewOnly, String sessionDirName);
        bool StartOffline(bool force, bool previewOnly);
        bool StartOffline(bool force, bool previewOnly, String genDirName);
        bool StartRemote(bool force, bool previewOnly, String sessionDirName, ulong startPosixTime);
        bool StartRemote(String controllerName, bool previewOnly, ulong startPosixTime);
        void StopRunning();
        bool StopRunning(String controllerID);
        bool StopRunning(bool force, bool editRecordedSession);
        double? GetSessionTimeline(DateTime session);
        double? GetSessionLength(DateTime session);
        double GetFilteredSessionListTotalLength();
        int[] GetGraphIDList();
        int? GetGraphIDWithTitle(String title);
        String GetGraphTitle(int graphID);
        String GetSignalName(String signalID, bool fullName);
        bool IsVideoDataAvailable(int channel, uint? tolerance);
        Samples.SpecialCameraType GetVideoSpecialType(int channel);
        TimeWithSession ConvertTimeIntoSession(double timeline);
        String[] GetSceneIDList();
        Dictionary<String, SceneTitle> GetSceneTitleTable();
        Dictionary<String, bool> GetChannelStatusTable(uint? tolerance);
        Dictionary<String, TimeOffsetSync> GetChannelSyncTable();
        bool SetControlFlag(String controllerID, bool enabled);
        String GetModuleConfig(object caller, String classID);
        void SetModuleConfig(object caller, String classID, String config);
        void DisableModule(object caller, String classID);
        ConfigStatus GetModuleConfigStatus(object caller, String classID);
        ConfigStatus GetModuleConfigStatus(object caller, String classID, out String errorHint);
        ConfigStatus[] GetModuleChildConfigStatus(object caller, String classID);
        Dictionary<BusDeviceID, BusDeviceInfo> GetBusDevices();
        Dictionary<VideoDeviceID, VideoDeviceInfo> GetVideoDevices();
        Dictionary<String, String> GetNativePluginVersions(String prefix);
        Dictionary<String, Version> GetNativePluginVersions(NativeLibraryType type);
        VideoFrameGetter CreateVideoFrameGetter();
        VideoFrameGetterX CreateVideoFrameGetterX();
        byte[] GetPreviewJpeg(int channel, double timeline, double maxGap, out Timestamp? timestamp, out CameraInfo cameraInfo);
        object GetOfflineMapImage(IntSize imageSize, LocPoint centerLocation, int zoom);
        CommonImage GetOfflineMapCommonImage(IntSize imageSize, LocPoint centerLocation, int zoom);
        FloatPoint ConvertOfflineMapLocToPix(LocPoint origin, int zoom, LocPoint point);
        Utility.LocPoint ConvertOfflineMapPixToLoc(LocPoint origin, int zoom, FloatPoint pixel);
        void SetWindowTitle(object window, String title, object icon);
        void SetCurrentDialogTitle(String title, object icon);
        BusProtocolFileID[] SelectBusProtocolFiles(BusProtocolFileID[] selected);
        SignalConfig SelectSignal(SignalConfig origin, bool withScale, bool withSignBit, String unit);
        String SelectBusMessage(String originMessageID);
        void SelectSignals(SelectSignalHandler handler, List<String> existSignalIDList);
        void SelectBusMessages(SelectBusMessageHandler handler, List<String> existBusMessageIDList);
        List<String> SelectBusFloat32Signal(List<String> existSignalIDList);
        void ConfigDataEncryption();
        void ConfigOfflineMapPath();
        void OpenDialog(object caller, String dialogClassID, String config);
        void AddWindow(object caller, String windowClassID, String config, bool newWorkspaceIfNeeded);
        void RunConsole(object caller, String consoleClassID);
        void RegisterAudioDriver(AudioDriverInfo driver, AudioRecorder recorder, AudioReplayer replayer);
        AudioDriverInfo[] GetAudioDrivers();
        AudioDeviceInfo[] GetAudioRecordDevices(String driverID);
        AudioDeviceInfo[] GetAudioReplayDevices(String driverID);
        double GetCPUTime();
        String[] GetEventTypeNames();
        String[] GetRecentProjectPaths();
        bool TerminateApp(bool force, bool autosave);
        void PopupError(String msg);
        void PopupNotice(String msg);
        bool PopupConfirm(String msg);
        void AddMainThreadCheckpoint(String location);
        bool NewProject(bool force);
        bool OpenProject(String projectFile);
        bool OpenProject(String projectFile, bool force);
        bool SaveCurrentProject(String projectFile);
        String GetCurrentProject();
        void PlayMp3(byte[] mp3FileData);
        bool StartProcess(String target);
        Dictionary<String, Version> GetVersionTable();
        String GetSystemStatus(SystemStatus status);
        String GetSystemStatusDetails(SystemStatus status);
        int[] GetLicensedFunctionIndices();
        bool SwitchAppMode(String controllerName, ApplicationMode mode, int waitSecond);
        void SetDataPath(String path);
        void SetSubDataPath(int subIndex, String path);
        Dictionary<String, WindowClassInfo> GetWindowClassTable();
        Dictionary<String, DialogClassInfo> GetDialogClassTable();
        Dictionary<String, ProcessorClassInfo> GetProcessorClassTable();
        Dictionary<String, NativeClassInfo> GetNativeClassTable();
        Dictionary<String, DeviceClassInfo> GetDeviceClassTable();
        Dictionary<String, TaskClassInfo> GetTaskClassTable();
        Dictionary<String, ConsoleClassInfo> GetConsoleClassTable();
        WindowClassInfo RegisterTransformWindowClass(String windowClassID, String config);
        WindowClassInfo RegisterTransformWindowClass(String windowClassID, WindowClass transformWindowClass, String defaultConfig);
        DialogClassInfo RegisterTransformDialogClass(String dialogClassID, String config);
        DialogClassInfo RegisterTransformDialogClass(String dialogClassID, DialogClass transformDialogClass, String defaultConfig);
        ConfigStatus GetDialogRelatedModulesConfigStatus(String dialogClassID, String transformID, out ConfigStatus[] childrenStatus);
        ConfigStatus GetConsoleRelatedModulesConfigStatus(String consoleClassID, out ConfigStatus[] childrenStatus);
        void DisableAllConfigs();
        String[] GetSessionGenerations(DateTime sessionID);
        DateTime[] GetGenerationSessions(String generationID);
        double GetInterestTarget();
        Dictionary<String, DeviceStatusDetail> GetAllDeviceStatus();
        void SetTargetReplaySpeed(double speed);
        void SetSessionSearchKeyword(String keyword);
        Dictionary<DateTime, SessionFilterFlags> GetSessionFilterTable();
        DateTime? GetCurrentOnlineSession();
        double GetSessionListTotalLength();
        String GetSessionSearchKey();
        bool RemoveSession(DateTime session, bool force);
        void SetSessionChecker(DateTime session, bool check);
        void RemoveGeneration(DateTime session, String genID);
        String GetSessionComment(DateTime session);
        void SetSessionComment(DateTime session, String comment);
        Dictionary<String, String> GetSessionProperties(DateTime session);
        void SetSessionProperties(DateTime session, Dictionary<String, String> properties);
        bool GetSessionHostSync(DateTime session);
        void SetSessionHostSync(DateTime session, bool hostSync);
        void SetAudioVolume(double volume);
        void SetCPURateScale(int scale);
        String GetLicenseInfo();
        double GetRawChannelDelayConfig(String id);
        double GetBusChannelDelayConfig(int channel);
        double GetVideoChannelDelayConfig(int channel);
        double GetAudioChannelDelayConfig();
        Dictionary<String, double> GetAllRawChannelDelayConfigs();
        void SetRawChannelDelayConfig(String id, double delay);
        void SetBusChannelDelayConfig(int channel, double delay);
        void SetVideoChannelDelayConfig(int channel, double delay);
        void SetAudioChannelDelayConfig(double delay);
        bool GetChannelMonitoringFlag(String id);
        void SetChannelMonitoringFlag(String id, bool monitoring);
        String[] GetAllChannelMonitoringKeys();
        bool GetChannelGuestSyncFlag(String id);
        void SetChannelGuestSyncFlag(String id, bool guestSync);
        String[] GetAllChannelGuestSyncKeys();
        bool GetChannelServerSyncMonitoringFlag(String id);
        void SetChannelServerSyncMonitoringFlag(String id, bool monitoring);
        String[] GetAllChannelServerSyncMonitoringKeys();
        bool GetBusChannelStatus(int channel, uint? toleranceMillisecond);
        bool GetVideoChannelStatus(int channel, uint? toleranceMillisecond, out List<double> interval, out List<double> delay);
        bool GetAudioChannelStatus(uint? toleranceMillisecond, out List<double> interval, out List<double> delay);
        bool GetRawChannelStatus(String channelID, uint? toleranceMillisecond);
        bool GetSampleChannelStatus(String channelID, uint? toleranceMillisecond, out List<double> interval, out List<double> delay);
        int[] GetAvailableBusChannels();
        int[] GetAvailableVideoChannels();
        String[] GetAvailableRawChannels();
        String[] GetAvailableSampleChannels();
        double? GetBusPayloadPercentage(int channel);
        bool IsSampleChannelConflict(String channelID);
        CreatePanelResult CreateWindowPanel(object caller, String windowClassID, String transformID, out object panel, out WindowClassInfo info);
        CreatePanelResult CreateConfigPanel(object caller, String dialogClassID, String transformID, out object panel, out DialogClassInfo info);
        void UnregisterPanel(object panel);
        CommonImage ConvertImageToCommon(object image);
        object ConvertImageToPlatform(CommonImage image, bool eto);
        WindowClassInfo GetWindowClassInfo(String windowClassID);
        WindowClassInfo GetWindowClassInfo(String windowClassID, String transformID);
        DialogClassInfo GetDialogClassInfo(String dialogClassID);
        DialogClassInfo GetDialogClassInfo(String dialogClassID, String transformID);
        TaskClassInfo GetTaskClassInfo(String taskClassID);
        ConsoleClassInfo GetConsoleClassInfo(String consoleClassID);
        CommonImage DecodeImage(byte[] imageData);
        byte[] EncodeImage(CommonImage image, String format);
        SignalTreeNode[] GetSignalTree();
        String[] GetPluginPackIDList();
        PluginPackInfo GetPluginPackInfo(String packID);
        bool InstallPlugin(String dirPath);
        bool UninstallPlugin(String packID);
        LogMessage[] GetLogMessages();
        bool IsReady();
        bool IsReady(out String busyReason);
        void DisablePlugin(String packID);
        void DisableAllPlugins();
        void EnablePlugin(String packID);
        void EnqueueDataToNative(object caller, String nativeClassID, String dataID, byte[] data);
        byte[][] DequeueDataFromNative(object caller, String nativeClassID, String dataID);
        byte[] CallNativeFunction(object caller, String nativeClassID, String funcID, byte[] input);
        void SetAppFunctionHandler(object caller, String nativeClassID, String funcID, AppFunctionHandler handler);
        void ResetAppFunctionHandler(object caller, String nativeClassID, String funcID);
        bool IsGPURenderingDisabled();
        bool IsOnscreenGPURenderingEnabled();
        void ResetGPUDecoderTestResults();
        DataSubscriber SubscribeData(String dataID, int bufferLength, int timeout);
        void PublishData(String dataID, byte[] data);
        CPUTimeModel GetCPUTimeModel(DateTime session);
        PosixTimeModel GetHostPosixTimeModel(DateTime session);
        PosixTimeModel GetGNSSPosixTimeModel(DateTime session);
        DateTime? GetLocalDateTime(DateTime session, double timeOffset, bool useGNSS);
        DateTime? GetUTCDateTime(DateTime session, double timeOffset, bool useGNSS);
        ulong GetCPUTick();
        ulong GetCPUTicksPerSecond();
        Timestamp[] GetChannelLatestTimestamps(String channelID);
        void RegisterGraphPanel(GraphType graphType, String styleName, Type panelType);
        void RegisterGraphPanel(int graphID, String styleName, Type panelType);
        String[] GetGraphPanelStyles(GraphType graphType);
        String[] GetGraphPanelStyles(int graphID);
        GraphPanel CreateGraphPanel(GraphType graphID, String styleName);
        GraphPanel CreateGraphPanel(int graphID, String styleName);
        DateTime? GetInternetNTPTime();
        Dictionary<String, String> GetPluginGuestSyncTable();
        GraphicCardInfo[] GetGraphicCardInfos();
        Dictionary<String, String> GetFrameworkThirdPartyNotices();
        Dictionary<String, Dictionary<String, String> > GetPluginThirdPartyNotices();
        String GetOfflineMapCopyrightInfo();
        bool IsPRCWebPreferred();
        BusChannelInfo[] GetBusChannelsInfo(DateTime session);
        VideoChannelInfo[] GetVideoChannelsInfo(DateTime session);
        ModuleDetails GetModuleDetails(String classID);
    }
    
    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Include all main APIs
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 集合了所有主要API
    /// </summary>
    public partial class Agency
    {
        private static AgencyHandler handler = null;
        public static AgencyHandler Handler
        {
            private get
            {
                if (handler == null) handler = new AgencyDefault();
                return handler;
            }
            set
            {
                if (value != null) handler = value;
            }
        }

        /// \~English
        /// <summary>
        /// (api:app=2.16.3) Get application's ID
        /// </summary>
        /// <returns>Application's ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.16.3) 获取应用程序ID
        /// </summary>
        /// <returns>应用程序ID</returns>
        public static String GetAppID()
        {
            return handler.GetAppID();
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
        public static ApplicationStatus GetAppStatus()
        {
            return Handler.GetAppStatus();
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
        public static ApplicationMode GetAppMode()
        {
            return Handler.GetAppMode();
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
        public static bool IsFileOutputEnabled()
        {
            return Handler.IsFileOutputEnabled();
        }

        /// \~English
        /// <summary>
        /// Get application's language code
        /// </summary>
        /// <returns>Application's language code, "en" means English, "ch" means Chinese</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序当前的显示语言
        /// </summary>
        /// <returns>语言ID，如en表示英文，ch表示中文等</returns>
        public static String GetAppLanguage()
        {
            return Handler.GetAppLanguage();
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
        public static double GetInterestTime()
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
        public static DateTime? GetInterestTimestamp()
        {
            return Handler.GetInterestTimestamp();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get the interest target
        /// </summary>
        /// <returns>The interest target (in seconds), which could be out of the buffer range</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取兴趣时间点目标
        /// </summary>
        /// <returns>兴趣时间点目标(秒单位)，可能超出数据缓存范围</returns>
        public static double GetInterestTarget()
        {
            return Handler.GetInterestTarget();
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
        public static BufferRange GetBufferRange()
        {
            return Handler.GetBufferRange();
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
        public static DateTime[] GetSessionList()
        {
            return Handler.GetSessionList();
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
        public static DateTime[] GetFilteredSessionList()
        {
            return Handler.GetFilteredSessionList();
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
        public static String[] GetGenerationList()
        {
            return Handler.GetGenerationList();
        }

        /// \~English
        /// <summary>
        /// Get the path of a session's data
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>The path of the session's data, null if it does not exist or not belongs to the current layer</returns>
        /// \~Chinese
        /// <summary>
        /// 获取某个session数据在硬盘的根路径
        /// </summary>
        /// <param name="session">希望获取的session ID</param>
        /// <returns>Session数据的根路径，若不存在或不属于当前层级则返回null</returns>
        public static String GetSessionPath(DateTime session)
        {
            return Handler.GetSessionPath(session);
        }

        /// \~English
        /// <summary>
        /// Get the path of a session's public data
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>The path of the session's public data, null if it doesn't exist or not belongs to the current layer</returns>
        /// \~Chinese
        /// <summary>
        /// 获取某个session的公共数据在硬盘的根路径
        /// </summary>
        /// <param name="session">希望获取的session ID</param>
        /// <returns>Session公共数据的根路径，若不存在或不属于当前层级则返回null</returns>
        public static String GetSessionPublicDataPath(DateTime session)
        {
            return Handler.GetSessionPublicDataPath(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Get the data layer to which a session belongs
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>The data layer, while '.' means the layer of sessions under data path, '..' means the layer of data path which is a session</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 获取某个session所属数据层级
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>数据层级，其中'.'表示根路径下的session，'..'表示根路径即session</returns>
        public static String GetSessionLayer(DateTime session)
        {
            return Handler.GetSessionLayer(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Get the folder name of a session
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Folder name</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 获取某个session的文件夹名
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>文件夹名</returns>
        public static String GetSessionFolderName(DateTime session)
        {
            return Handler.GetSessionFolderName(session);
        }

        /// \~English
        /// <summary>
        /// Get the path of a generation with the specified generation ID and under the specified session
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="generation">Generation ID</param>
        /// <returns>The path of a generation, null if it doesn't exist or not belongs to current layer</returns>
        /// \~Chinese
        /// <summary>
        /// 获取某个session中的某个generation在硬盘的根路径
        /// </summary>
        /// <param name="session">希望获取generation所属的session ID</param>
        /// <param name="generation">希望获取的generation ID</param>
        /// <returns>Generation数据的根路径，若不存在或不属于当前层级则返回null</returns>
        public static String GetGenerationPath(DateTime session, String generation)
        {
            return Handler.GetGenerationPath(session, generation);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.2) Get the status of generation with the specified generation ID and under the specified session
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="generation">Generation ID</param>
        /// <returns>Generation's status, null if it doesn't exist</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.2) 获取某个session中的某个generation的处理状态
        /// </summary>
        /// <param name="session">希望获取generation所属的session ID</param>
        /// <param name="generation">希望获取的generation ID</param>
        /// <returns>Generation的处理状态，若generation不存在则返回null</returns>
        public static GenerationProcessStatus? GetGenerationProcessStatus(DateTime session, String generation)
        {
            return Handler.GetGenerationProcessStatus(session, generation);
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
        public static DateTime[] GetFinishedSessions(String generation)
        {
            return Handler.GetFinishedSessions(generation);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.5) Refresh the sessions under the current layer
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.5) 刷新当前层级下所有session
        /// </summary>
        public static void RefreshSessions()
        {
            Handler.RefreshSessions();
        }

        /// \~English
        /// <summary>
        /// Refresh all the generations of the current sessions
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 刷新当前层级下所有session的generation
        /// </summary>
        public static void RefreshGenerations()
        {
            Handler.RefreshGenerations();
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
        public static String GetCurrentDataGeneration()
        {
            return Handler.GetCurrentDataGeneration();
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
        public static String GetCurrentSessionGUID()
        {
            return Handler.GetCurrentSessionGUID();
        }

        /// \~English
        /// <summary>
        /// Deprecated, use ASEva.Agency.GetHostPosixTimeModel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应使用 ASEva.Agency.GetHostPosixTimeModel
        /// </summary>
        public static DateTime? GetStartTimeLocal(DateTime session)
        {
            return Handler.GetStartTimeLocal(session);
        }

        /// \~English
        /// <summary>
        /// Deprecated, use ASEva.Agency.GetGNSSPosixTimeModel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应使用 ASEva.Agency.GetGNSSPosixTimeModel
        /// </summary>
        public static DateTime? GetStartTimeUTC(DateTime session)
        {
            return Handler.GetStartTimeUTC(session);
        }

        /// \~English
        /// <summary>
        /// Deprecated, use ASEva.Agency.GetLocalDateTime
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应使用 ASEva.Agency.GetLocalDateTime
        /// </summary>
        public static DateTime? GetTimestampLocal(DateTime session, double timeOffset)
        {
            return Handler.GetTimestampLocal(session, timeOffset);
        }

        /// \~English
        /// <summary>
        /// Deprecated, use ASEva.Agency.GetUTCDateTime
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应使用 ASEva.Agency.GetUTCDateTime
        /// </summary>
        public static DateTime? GetTimestampUTC(DateTime session, double timeOffset)
        {
            return Handler.GetTimestampUTC(session, timeOffset);
        }

        /// \~English
        /// <summary>
        /// Deprecated, use ASEva.Agency.GetHostPosixTimeModel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应使用 ASEva.Agency.GetHostPosixTimeModel
        /// </summary>
        public static double GetTimeRatioToLocal(DateTime session)
        {
            return Handler.GetTimeRatioToLocal(session);
        }

        /// \~English
        /// <summary>
        /// Deprecated, use ASEva.Agency.GetGNSSPosixTimeModel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应使用 ASEva.Agency.GetGNSSPosixTimeModel
        /// </summary>
        public static double GetTimeRatioToUTC(DateTime session)
        {
            return Handler.GetTimeRatioToUTC(session);
        }

        /// \~English
        /// <summary>
        /// Get the current data path
        /// </summary>
        /// <returns>The data path, null if not configured</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前数据目录的路径
        /// </summary>
        /// <returns>当前数据目录的路径，若未设置返回null</returns>
        public static String GetDataPath()
        {
            return Handler.GetDataPath();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Deprecated, use ASEva.Agency.GetSubDataPaths
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 已弃用，应使用 ASEva.Agency.GetSubDataPaths
        /// </summary>
        public static String[] GetSubDataPathes()
        {
            return Handler.GetSubDataPaths();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.12) Get all sub data paths
        /// </summary>
        /// <returns>Sub data paths, null if they don't exist</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.12) 获取所有子数据目录的路径
        /// </summary>
        /// <returns>所有子数据目录的路径，目录不存在则为null</returns>
        public static String[] GetSubDataPaths()
        {
            return Handler.GetSubDataPaths();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Set sub data path
        /// </summary>
        /// <param name="subIndex">Index, ranges 0~3</param>
        /// <param name="path">Sub data path</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 设置子数据目录的路径
        /// </summary>
        /// <param name="subIndex">子数据目录序号，0~3</param>
        /// <param name="path">子数据目录的路径</param>
        public static void SetSubDataPath(int subIndex, String path)
        {
            Handler.SetSubDataPath(subIndex, path);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Get all data layers
        /// </summary>
        /// <returns>Data layers, while '.' means the layer of sessions under data path, '..' means the layer of data path which is session</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 获取所有有效的数据层级
        /// </summary>
        /// <returns>数据层级列表，其中'.'表示根路径下的session，'..'表示根路径即session</returns>
        public static String[] GetDataLayers()
        {
            return Handler.GetDataLayers();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Add a new data layer
        /// </summary>
        /// <param name="layer">Data layer</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 添加新的数据层级
        /// </summary>
        /// <param name="layer">数据层级</param>
        public static void AddDataLayer(String layer)
        {
            Handler.AddDataLayer(layer);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Remove data layer, and delete files to recycle bin
        /// </summary>
        /// <param name="layer">Data layer</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 移除数据层级，并删除所有文件至回收站
        /// </summary>
        /// <param name="layer">数据层级</param>
        public static void DeleteDataLayer(String layer)
        {
            Handler.DeleteDataLayer(layer);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Get current data layer
        /// </summary>
        /// <returns>The current data layer, while null means all layers, '.' means the layer of sessions under data path, '..' means the layer of data path which is session</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 获取当前的数据层级
        /// </summary>
        /// <returns>数据层级，其中null表示所有层级，'.'表示根路径下的session，'..'表示根路径即session</returns>
        public static String GetCurrentDataLayer()
        {
            return Handler.GetCurrentDataLayer();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.5) Get the path of current data layer
        /// </summary>
        /// <returns>The path of current data layer, returns null if the data path is not configured or the current data layer is '..', return data path if the current data layer is null (all layers)</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.5) 获取当前数据层级的路径
        /// </summary>
        /// <returns>当前数据层级的路径，若数据目录未设置或数据层级为'..'则返回null，若当前数据层级为null(所有层级)则返回数据目录根路径</returns>
        public static String GetCurrentDataLayerPath()
        {
            return Handler.GetCurrentDataLayerPath();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Set current data layer
        /// </summary>
        /// <param name="layer">The current data layer, while null means all layers, '.' means the layer of sessions under data path, '..' means the layer of data path which is session</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 设置当前的数据层级
        /// </summary>
        /// <param name="layer">数据层级，其中null表示所有层级，'.'表示根路径下的session，'..'表示根路径即session</param>
        public static void SetCurrentDataLayer(String layer)
        {
            Handler.SetCurrentDataLayer(layer);
        }

        /// \~English
        /// <summary>
        /// Get the path of global public data
        /// </summary>
        /// <returns>The path of global public data, null if the data path is not configured</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前全局公共数据目录的路径
        /// </summary>
        /// <returns>当前全局公共数据目录的路径，若未设置返回null</returns>
        public static String GetGlobalPublicDataPath()
        {
            return Handler.GetGlobalPublicDataPath();
        }

        /// \~English
        /// <summary>
        /// Get the path of current application's config files
        /// </summary>
        /// <returns>The path of current application's config files</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前应用程序的配置文件根目录路径
        /// </summary>
        /// <returns>配置文件根目录路径</returns>
        public static String GetConfigFilesRoot()
        {
            return Handler.GetConfigFilesRoot();
        }

        /// \~English
        /// <summary>
        /// Get the path of current application's data and document files
        /// </summary>
        /// <returns>The path of current application's data and document files</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用数据和文档文件根目录路径
        /// </summary>
        /// <returns>应用数据和文档文件根目录路径</returns>
        public static String GetAppFilesRoot()
        {
            return Handler.GetAppFilesRoot();
        }

        /// \~English
        /// <summary>
        /// Get the path of current application's temporary files
        /// </summary>
        /// <returns>The path of current application's temporary files</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前应用程序临时文件根目录路径
        /// </summary>
        /// <returns>临时文件根目录路径</returns>
        public static String GetTempFilesRoot()
        {
            return Handler.GetTempFilesRoot();
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
        public static void SendBusMessage(BusMessage message)
        {
            Handler.SendBusMessage(message);
        }

        /// \~English
        /// <summary>
        /// Transmit bound bus message, either periodically or once (only available in online mode)
        /// </summary>
        /// <param name="messageID">Message ID of bound bus message</param>
        /// <param name="interval">Transmit period, in milliseconds (at least 10). If it is set to null, it is transmitted only once</param>
        /// \~Chinese
        /// <summary>
        /// 发送总线报文（该报文需设置绑定），可周期性发送，也可单次发送（仅在线模式可用）
        /// </summary>
        /// <param name="messageID">绑定的报文ID</param>
        /// <param name="interval">报文发送周期，单位毫秒（至少为10），若设为null则只发送一次</param>
        public static void SendBusMessage(String messageID, uint? interval)
        {
            Handler.SendBusMessage(messageID, interval);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.15) Transmit bound bus message, either periodically or once (only available in online mode)
        /// </summary>
        /// <param name="messageID">Message ID of bound bus message</param>
        /// <param name="interval">Transmit period, in milliseconds (at least 10). If it is set to null, it is transmitted only once</param>
        /// <param name="data">Generated bus message data, null if not bound</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.15) 发送总线报文（该报文需设置绑定），可周期性发送，也可单次发送（仅在线模式可用）
        /// </summary>
        /// <param name="messageID">绑定的报文ID</param>
        /// <param name="interval">报文发送周期，单位毫秒（至少为10），若设为null则只发送一次</param>
        /// <param name="data">输出生成的报文数据，若未绑定则输出null</param>
        public static void SendBusMessage(String messageID, uint? interval, out byte[] data)
        {
            Handler.SendBusMessage(messageID, interval, out data);
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
        public static void SendRawData(String channelID, double[] values, byte[] binary)
        {
            Handler.SendRawData(channelID, values, binary);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.8.0) Transmitted received general raw data (only available in online mode)
        /// </summary>
        /// <param name="cpuTick">CPU tick while data arriving</param>
        /// <param name="channelID">General raw data's channel ID, corresponding to the first column of input/raw/raw.csv</param>
        /// <param name="values">Numeric data</param>
        /// <param name="binary">Binary data</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.8.0) 发送已获取的原始数据信息（仅在线模式可用）
        /// </summary>
        /// <param name="cpuTick">数据的到达时CPU计数</param>
        /// <param name="channelID">原始数据协议名称，对应input/raw/raw.csv首列文字</param>
        /// <param name="values">数值数据</param>
        /// <param name="binary">二进制数据</param>
        public static void SendRawData(ulong cpuTick, String channelID, double[] values, byte[] binary)
        {
            Handler.SendRawData(cpuTick, channelID, values, binary);
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
        public static void SendManualTrigger(int channel)
        {
            Handler.SendManualTrigger(channel);
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
        public static GraphData GetGraphData(DateTime session, int graphID)
        {
            return Handler.GetGraphData(session, graphID);
        }

        /// \~English
        /// <summary>
        /// Add a new scenario segment
        /// </summary>
        /// <param name="scene">Scenario segment</param>
        /// \~Chinese
        /// <summary>
        /// 添加一个场景片段
        /// </summary>
        /// <param name="scene">想要添加的场景片段描述</param>
        public static void AddSceneData(SceneData scene)
        {
            Handler.AddSceneData(scene);
        }

        /// \~English
        /// <summary>
        /// Get status of a general device
        /// </summary>
        /// <param name="type">Related native plugin's type ID, the same as the "type" field in info.txt</param>
        /// <returns>Status of the general device</returns>
        /// \~Chinese
        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="type">设备的类型ID，在插件的info.txt中以type字段描述</param>
        /// <returns>返回设备状态</returns>
        public static GeneralDeviceStatus GetDeviceStatus(String type)
        {
            return Handler.GetDeviceStatus(type);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all general devices' status
        /// </summary>
        /// <returns>The Dictionary. The key is type ID of general device</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取所有设备状态
        /// </summary>
        /// <returns>设备状态表，键为设备的类型ID</returns>
        public static Dictionary<String, DeviceStatusDetail> GetAllDeviceStatus()
        {
            return Handler.GetAllDeviceStatus();
        }

        /// \~English
        /// <summary>
        /// Get status of a general device's sub devices
        /// </summary>
        /// <param name="type">Related native plugin's type ID, the same as the "type" field in info.txt</param>
        /// <returns>Status of a general device's sub devices</returns>
        /// \~Chinese
        /// <summary>
        /// 获取各子设备的设备状态
        /// </summary>
        /// <param name="type">设备的类型ID，在插件的info.txt中以type字段描述</param>
        /// <returns>各子设备的设备状态</returns>
        public static GeneralDeviceStatus[] GetChildDeviceStatus(String type)
        {
            return Handler.GetChildDeviceStatus(type);
        }

        /// \~English
        /// <summary>
        /// Print for debugging, use ASEva.Logger when you need to specify the source
        /// </summary>
        /// <param name="text">The message</param>
        /// \~Chinese
        /// <summary>
        /// 打印信息用于调试，需要指定来源时应使用 ASEva.Logger
        /// </summary>
        /// <param name="text">想要打印的文本</param>
        public static void Print(String text)
        {
            Handler.Print(text);
        }

        /// \~English
        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="text">The message</param>
        /// <param name="level">Log's level</param>
        /// \~Chinese
        /// <summary>
        /// 添加清单信息
        /// </summary>
        /// <param name="text">想要显示的信息</param>
        /// <param name="level">清单信息级别</param>
        public static void Log(String text, LogLevel level)
        {
            Handler.Log(text, level);
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
        public static bool IsMessageValid(String messageID, bool optional)
        {
            return Handler.IsMessageValid(messageID, optional);
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
        public static bool IsSignalValid(String signalID, bool optional)
        {
            return Handler.IsSignalValid(signalID, optional);
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
        public static String[] GetSignalNamesOfBusMessage(String messageID)
        {
            return Handler.GetSignalNamesOfBusMessage(messageID);
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
        public static bool IsInternetConnected()
        {
            return Handler.IsInternetConnected();
        }

        /// \~English
        /// <summary>
        /// Get memory capacity
        /// </summary>
        /// <returns>Memory capacity, in bytes, zero if failed to query</returns>
        /// \~Chinese
        /// <summary>
        /// 获取内存容量
        /// </summary>
        /// <returns>内存容量，单位为字节，获取失败时返回0</returns>
        public static ulong GetMemoryCapacity()
        {
            return Handler.GetMemoryCapacity();
        }

        /// \~English
        /// <summary>
        /// Call Web API (GET) asynchronously
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="context">The context of calling. Since this function is non-blocking, the context needs to be used to get the status and response at a future time after it ends</param>
        /// \~Chinese
        /// <summary>
        /// 非阻塞调用Web API (GET)
        /// </summary>
        /// <param name="request">调用请求</param>
        /// <param name="context">调用上下文。由于本函数为非阻塞，在结束后需要通过该对象在未来时刻获取调用状态和响应字符串</param>
        public static void CallWebApi(String request, WebApiContext context)
        {
            Handler.CallWebApi(request, context);
        }

        /// \~English
        /// <summary>
        /// Call Web API (POST) asynchronously, default content type is ASEva.WebPostContentType.WWWFormUrlEncoded
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="body">Binary data body</param>
        /// <param name="context">The context of calling. Since this function is non-blocking, the context needs to be used to get the status and response at a future time after it ends</param>
        /// \~Chinese
        /// <summary>
        /// 非阻塞调用Web API (POST)，内容类型默认为 ASEva.WebPostContentType.WWWFormUrlEncoded
        /// </summary>
        /// <param name="request">调用请求</param>
        /// <param name="body">提交的二进制数据body</param>
        /// <param name="context">调用上下文。由于本函数为非阻塞，在结束后需要通过该对象在未来时刻获取调用状态和响应字符串</param>
        public static void CallWebApiPost(String request, byte[] body, WebApiContext context)
        {
            Handler.CallWebApiPost(request, body, context);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.0.4) Call Web API (POST) asynchronously with the specified content type
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="body">Binary data body</param>
        /// <param name="contentType">Content type</param>
        /// <param name="context">The context of calling. Since this function is non-blocking, the context needs to be used to get the status and response at a future time after it ends</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.0.4) 非阻塞调用Web API (POST)，指定内容类型
        /// </summary>
        /// <param name="request">调用请求</param>
        /// <param name="body">提交的二进制数据body</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="context">调用上下文。由于本函数为非阻塞，在结束后需要通过该对象在未来时刻获取调用状态和响应字符串</param>
        public static void CallWebApiPost(String request, byte[] body, WebPostContentType contentType, WebApiContext context)
        {
            Handler.CallWebApiPost(request, body, contentType, context);
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
        public static String GetChannelAliasName(String channelID)
        {
            return Handler.GetChannelAliasName(channelID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.4) Get all data channels' alias name
        /// </summary>
        /// <returns>The Dictionary. The key is data channel ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.4) 获取数据通道别名
        /// </summary>
        /// <returns>数据所有通道的别名表</returns>
        public static Dictionary<String, String> GetChannelAliasTable()
        {
            return Handler.GetChannelAliasTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get whether there're multiple components outputting data with the same sample channel ID
        /// </summary>
        /// <param name="channelID">Sample channel ID, with format as "protocol@channel". Channel is starting from 0. Protocol with "v" version number is with backward compatibility</param>
        /// <returns>Whether conflict exists</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 检查样本数据通道是否冲突（有多个组件输出相同协议和通道的样本数据）
        /// </summary>
        /// <param name="channelID">样本通道协议，格式为"协议名@通道序号"，通道序号从0开始，协议名中带"v"字版本号的可向下兼容</param>
        /// <returns>是否冲突</returns>
        public static bool IsSampleChannelConflict(string channelID)
        {
            return Handler.IsSampleChannelConflict(channelID);
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
        public static bool IsInputChannelAvailable(String channelID)
        {
            return Handler.IsInputChannelAvailable(channelID);
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
        public static List<String> GetSampleTitle(String channelID)
        {
            return Handler.GetSampleTitle(channelID);
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
        public static String[] GetManualTriggerNames()
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
        public static String GetManualTriggerName(int index)
        {
            return Handler.GetManualTriggerName(index);
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
        public static void SetManualTriggerName(int index, String name)
        {
            Handler.SetManualTriggerName(index, name);
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
        public static void AddSignalReference(String signalID)
        {
            Handler.AddSignalReference(signalID);
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
        public static void RemoveSignalReference(String signalID)
        {
            Handler.RemoveSignalReference(signalID);
        }

        /// \~English
        /// <summary>
        /// Run a standalone task, only available while idle
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="taskClassID">Task class ID</param>
        /// <param name="config">Configuration string</param>
        /// <param name="returnValue">Return value string</param>
        /// <returns>Result of the task</returns>
        /// \~Chinese
        /// <summary>
        /// 运行一个独立处理任务，仅限空闲时运行
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="taskClassID">任务组件的类别ID</param>
        /// <param name="config">配置的字符串描述</param>
        /// <param name="returnValue">任务的返回值信息</param>
        /// <returns>任务运行结果</returns>
        public static TaskResult RunStandaloneTask(object caller, String taskClassID, String config, out String returnValue)
        {
            return Handler.RunStandaloneTask(caller, taskClassID, config, out returnValue);
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
        public static void SetInterestTimestamp(DateTime targetTimestamp)
        {
            Handler.SetInterestTimestamp(targetTimestamp);
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
        public static void SetInterestTime(double targetTimeline)
        {
            Handler.SetInterestTime(targetTimeline);
        }

        /// \~English
        /// <summary>
        /// Delete file or folder to recycle bin
        /// </summary>
        /// <param name="path">Path of file or folder</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 删除指定文件或文件夹至回收站
        /// </summary>
        /// <param name="path">文件或文件夹路径</param>
        /// <returns>是否成功</returns>
        public static bool DeleteToRecycleBin(String path)
        {
            return Handler.DeleteToRecycleBin(path);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.1) Get all keys of global variable
        /// </summary>
        /// <returns>All keys of global variable</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.1) 获取所有全局变量的键
        /// </summary>
        /// <returns>所有全局变量的键</returns>
        public static String[] GetGlobalVariableKeys()
        {
            return Handler.GetGlobalVariableKeys();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.1) Get all keys of global parameter
        /// </summary>
        /// <returns>All keys of global parameter</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.1) 获取所有全局参数的键
        /// </summary>
        /// <returns>所有全局参数的键</returns>
        public static String[] GetGlobalParameterKeys()
        {
            return Handler.GetGlobalParameterKeys();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.1) Get all keys of global path
        /// </summary>
        /// <returns>All keys of global path</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.1) 获取所有全局路径的键
        /// </summary>
        /// <returns>所有全局路径的键</returns>
        public static String[] GetGlobalPathKeys()
        {
            return Handler.GetGlobalPathKeys();
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
        public static void SetGlobalVariable(String key, String value)
        {
            Handler.SetGlobalVariable(key, value);
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
        public static String GetGlobalVariable(String key, String defaultValue)
        {
            return Handler.GetGlobalVariable(key, defaultValue);
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
        public static void SetGlobalParameter(String key, String value)
        {
            Handler.SetGlobalParameter(key, value);
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
        public static String GetGlobalParameter(String key, String defaultValue)
        {
            return Handler.GetGlobalParameter(key, defaultValue);
        }

        /// \~English
        /// <summary>
        /// Get global path
        /// </summary>
        /// <param name="key">Key of global path</param>
        /// <returns>Global path(s) separated by ';' (Only the ones exist), null if key is null or empty</returns>
        /// \~Chinese
        /// <summary>
        /// 获取全局路径
        /// </summary>
        /// <param name="key">全局路径key</param>
        /// <returns>以分号分割的全局路径value（仅返回存在的部分），若key为null、""则返回null</returns>
        public static String GetGlobalPath(String key)
        {
            return Handler.GetGlobalPath(key);
        }

        /// \~English
        /// <summary>
        /// Set global path
        /// </summary>
        /// <param name="key">Key of global parameter, do nothing if it's null or empty</param>
        /// <param name="path">Global path(s) separated by ';', while the ones not exist will be ignored</param>
        /// \~Chinese
        /// <summary>
        /// 设置全局路径
        /// </summary>
        /// <param name="key">全局路径key，若为null或""则忽略</param>
        /// <param name="path">以分号分割的全局路径value，不存在的部分将被忽略</param>
        public static void SetGlobalPath(String key, String path)
        {
            Handler.SetGlobalPath(key, path);
        }

        /// \~English
        /// <summary>
        /// Get information of all bus protocols bound to any bus channel
        /// </summary>
        /// <returns>Information of all bus protocols</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有总线通道上的协议信息
        /// </summary>
        /// <returns>总线协议信息列表</returns>
        public static BusFileInfo[] GetBusProtocolFilesInfo()
        {
            return Handler.GetBusProtocolFilesInfo();
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
        public static int? GetBusProtocolFileChannel(String protocolName)
        {
            return Handler.GetBusProtocolFileChannel(protocolName);
        }

        /// \~English
        /// <summary>
        /// Get all bus signals should be parsed as a 32bit floating number
        /// </summary>
        /// <returns>Signal IDs</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有作为32位浮点解析的信号列表
        /// </summary>
        /// <returns>信号ID列表</returns>
        public static String[] GetBusFloat32Signals()
        {
            return Handler.GetBusFloat32Signals();
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
        public static float GetBusMessageFPS(int channel, uint localID)
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
        public static BusMessageInfo GetBusMessageInfo(int channel, uint localID)
        {
            return Handler.GetBusMessageInfo(channel, localID);
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
        public static BusMessageInfo GetBusMessageInfo(String busMessageID)
        {
            return Handler.GetBusMessageInfo(busMessageID);
        }

        /// \~English
        /// <summary>
        /// Deprecated, use ASEva.Agency.IsBusMessageBound
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应使用 ASEva.Agency.IsBusMessageBound
        /// </summary>
        public static bool IsBusMessageBinded(string busMessageID)
        {
            return Handler.IsBusMessageBound(busMessageID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get whether the bus message is bound
        /// </summary>
        /// <param name="busMessageID">Bus message ID</param>
        /// <returns>Whether the bus message is bound</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取总线报文是否已绑定发送
        /// </summary>
        /// <param name="busMessageID">总线报文ID</param>
        /// <returns>是否已绑定</returns>
        public static bool IsBusMessageBound(string busMessageID)
        {
            return Handler.IsBusMessageBound(busMessageID);
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
        public static object[] GetEventHandles()
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
        public static EventInfo GetEventInfo(object eventHandle)
        {
            return Handler.GetEventInfo(eventHandle);
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
        public static void RemoveEvent(object eventHandle)
        {
            Handler.RemoveEvent(eventHandle);
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
        public static void SetEventComment(object eventHandle, String comment)
        {
            Handler.SetEventComment(eventHandle, comment);
        }

        /// \~English
        /// <summary>
        /// Switch to replay mode and start replay
        /// </summary>
        /// <param name="startTimeline">The timeline point from which to start replay, in seconds</param>
        /// <param name="interestTarget">The timeline point of replay target, in seconds (null means replaying to end of timeline)</param>
        /// \~Chinese
        /// <summary>
        /// 切换至回放模式并开始回放
        /// </summary>
        /// <param name="startTimeline">时间线上的回放开始时间，单位秒</param>
        /// <param name="interestTarget">时间线上的目标兴趣点，单位秒（空表示不设置兴趣点）</param>
        public static void StartReplay(double startTimeline, double? interestTarget)
        {
            Handler.StartReplay(startTimeline, interestTarget);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Switch to replay mode and start replay
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="startTimeline">The timeline point from which to start replay, in seconds</param>
        /// <param name="interestTarget">The timeline point of replay target, in seconds (null means replaying to end of timeline)</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 切换至回放模式并开始回放
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="startTimeline">时间线上的回放开始时间，单位秒</param>
        /// <param name="interestTarget">时间线上的目标兴趣点，单位秒（空表示不设置兴趣点）</param>
        /// <returns>是否成功</returns>
        public static bool StartReplay(bool force, double startTimeline, double? interestTarget)
        {
            return Handler.StartReplay(force, startTimeline, interestTarget);
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
        public static bool StartOnline(String controllerName, bool previewOnly)
        {
            return Handler.StartOnline(controllerName, previewOnly);
        }

        /// \~English
        /// <summary>
        /// Switch to online mode and start
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="previewOnly">Whether previewing, otherwise recording</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 切换至在线模式并开始预览或采集
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <returns>是否成功</returns>
        public static bool StartOnline(bool force, bool previewOnly)
        {
            return Handler.StartOnline(force, previewOnly);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.0) Switch to online mode and start
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="previewOnly">Whether previewing, otherwise recording</param>
        /// <param name="sessionDirName">The folder name for session recording (If the folder already exists and "force" is true, it will use default date and time format)</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.0) 切换至在线模式并开始预览或采集
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <param name="sessionDirName">采集时，写入session数据的文件夹名（若已存在且强制开始时，则使用默认的日期格式）</param>
        /// <returns>是否成功</returns>
        public static bool StartOnline(bool force, bool previewOnly, String sessionDirName)
        {
            return Handler.StartOnline(force, previewOnly, sessionDirName);
        }

        /// \~English
        /// <summary>
        /// Switch to offline processing mode and start
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="previewOnly">Whether previewing, otherwise generating</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 切换至离线模式并开始预览或后处理
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <returns>是否成功</returns>
        public static bool StartOffline(bool force, bool previewOnly)
        {
            return Handler.StartOffline(force, previewOnly);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.3) Switch to offline processing mode and start
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="previewOnly">Whether previewing, otherwise generating</param>
        /// <param name="genDirName">The folder name for generation recording (If the folder already exists and "force" is true, it will use default date and time format)</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.3) 切换至离线模式并开始预览或后处理
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <param name="genDirName">后处理输出时，写入generation数据的文件夹名（若已存在且强制开始时，则使用默认的日期格式）</param>
        /// <returns>是否成功</returns>
        public static bool StartOffline(bool force, bool previewOnly, String genDirName)
        {
            return Handler.StartOffline(force, previewOnly, genDirName);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.10.0) Switch to remote mode and start
        /// </summary>
        /// <param name="force">Whether to force to start, switching mode could cost a lot of time</param>
        /// <param name="previewOnly">Whether previewing, otherwise recording</param>
        /// <param name="sessionDirName">The folder name for session recording (If the folder already exists and "force" is true, it will use default date and time format)</param>
        /// <param name="startPosixTime">Start time on the remote machine, in posix milliseconds</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.10.0) 切换至远程模式并开始预览或采集
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <param name="sessionDirName">采集时，写入session数据的文件夹名（若已存在且强制开始时，则使用默认的日期格式，时间为本机时间，非远程主机时间）</param>
        /// <param name="startPosixTime">远程主机的开始时间，单位毫秒</param>
        /// <returns>是否成功</returns>
        public static bool StartRemote(bool force, bool previewOnly, String sessionDirName, ulong startPosixTime)
        {
            return Handler.StartRemote(force, previewOnly, sessionDirName, startPosixTime);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.15.4) Switch to remote mode and start
        /// </summary>
        /// <param name="controllerName">Controller name, for exclusive control</param>
        /// <param name="previewOnly">Whether previewing, otherwise recording</param>
        /// <param name="startPosixTime">Start time on the remote machine, in posix milliseconds</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.4) 切换至远程模式并开始预览或采集
        /// </summary>
        /// <param name="controllerName">控制者名称，用于独占控制模式</param>
        /// <param name="previewOnly">是否为预览</param>
        /// <param name="startPosixTime">远程主机的开始时间，单位毫秒</param>
        /// <returns>是否成功</returns>
        public static bool StartRemote(String controllerName, bool previewOnly, ulong startPosixTime)
        {
            return Handler.StartRemote(controllerName, previewOnly, startPosixTime);
        }

        /// \~English
        /// <summary>
        /// Stop the session
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 停止Session
        /// </summary>
        public static void StopRunning()
        {
            Handler.StopRunning();
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
        public static bool StopRunning(String controllerName)
        {
            return Handler.StopRunning(controllerName);
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
        public static bool StopRunning(bool force, bool editRecordedSession)
        {
            return Handler.StopRunning(force, editRecordedSession);
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
        public static double? GetSessionTimeline(DateTime session)
        {
            return Handler.GetSessionTimeline(session);
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
        public static double? GetSessionLength(DateTime session)
        {
            return Handler.GetSessionLength(session);
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
        public static int[] GetGraphIDList()
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
        public static int? GetGraphIDWithTitle(String title)
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
        public static String GetGraphTitle(int graphID)
        {
            return Handler.GetGraphTitle(graphID);
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
        public static String GetSignalName(String signalID, bool fullName)
        {
            return Handler.GetSignalName(signalID, fullName);
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
        public static BusSignalInfo GetBusSignalInfo(String busSignalID)
        {
            return Handler.GetBusSignalInfo(busSignalID);
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
        public static bool IsVideoDataAvailable(int channel, uint? tolerance)
        {
            return Handler.IsVideoDataAvailable(channel, tolerance);
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
        public static Samples.SpecialCameraType GetVideoSpecialType(int channel)
        {
            return Handler.GetVideoSpecialType(channel);
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
        public static TimeWithSession ConvertTimeIntoSession(double timeline)
        {
            return  Handler.ConvertTimeIntoSession(timeline);
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
        public static double GetFilteredSessionListTotalLength()
        {
            return Handler.GetFilteredSessionListTotalLength();
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
        public static String[] GetSceneIDList()
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
        public static Dictionary<String, SceneTitle> GetSceneTitleTable()
        {
            return Handler.GetSceneTitleTable();
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
        public static Dictionary<String, bool> GetChannelStatusTable(uint? tolerance)
        {
            return Handler.GetChannelStatusTable(tolerance);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.4) Get time synchronization status of all channels
        /// </summary>
        /// <returns>Dictionary. The key is channel ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.4) 获取所有通道的时间同步状态
        /// </summary>
        /// <returns>返回各通道的时间同步状态，key为通道ID</returns>
        public static Dictionary<String, TimeOffsetSync> GetChannelSyncTable()
        {
            return Handler.GetChannelSyncTable();
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
        public static bool SetControlFlag(String controllerName, bool enabled)
        {
            return Handler.SetControlFlag(controllerName, enabled);
        }

        /// \~English
        /// <summary>
        /// Get processor/native/device component's configuration string
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// <returns>Configuration string, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据处理/原生/设备组件配置的字符串描述
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        /// <returns>配置的字符串描述，null表示找不到类别ID对应的组件</returns>
        public static String GetModuleConfig(object caller, String classID)
        {
            return Handler.GetModuleConfig(caller, classID);
        }

        /// \~English
        /// <summary>
        /// Set processor/native/device component's configuration string
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// <param name="config">Configuration string</param>
        /// \~Chinese
        /// <summary>
        /// 设置数据处理/原生/设备组件配置的字符串描述
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        /// <param name="config">配置的字符串描述</param>
        public static void SetModuleConfig(object caller, String classID, String config)
        {
            Handler.SetModuleConfig(caller, classID, config);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.7) Disable processor/native/device component component
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.7) 禁用数据处理/原生/设备组件
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        public static void DisableModule(object caller, String classID)
        {
            Handler.DisableModule(caller, classID);
        }

        /// \~English
        /// <summary>
        /// Deprecated, use GetModuleConfigStatus(caller, classID, errorHint)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已弃用，应使用GetModuleConfigStatus(caller, classID, errorHint)
        /// </summary>
        public static ConfigStatus GetModuleConfigStatus(object caller, String classID)
        {
            return Handler.GetModuleConfigStatus(caller, classID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.16.0) Get status of processor/native/device component's configuration
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// <param name="errorHint">Error hint, available while the status is EnabledWithError or EnabledWithWarning</param>
        /// <returns>Status of component's configuration, returns ASEva.ConfigStatus.Disabled if not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.16.0) 获取数据处理/原生/设备组件配置的状态
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        /// <param name="errorHint">错误提示，当配置状态为EnabledWithError或EnabledWithWarning时有效</param>
        /// <returns>组件配置的状态，若找不到类别ID对应的组件则返回 ASEva.ConfigStatus.Disabled </returns>
        public static ConfigStatus GetModuleConfigStatus(object caller, String classID, out String errorHint)
        {
            return Handler.GetModuleConfigStatus(caller, classID, out errorHint);
        }

        /// \~English
        /// <summary>
        /// Get child status of processor/native/device component's configuration
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , String(Controller name), etc.</param>
        /// <param name="classID">Component's class ID</param>
        /// <returns>Child status of component's configuration, null if not found or there's no child functions</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数据处理/原生/设备组件各子功能配置的状态
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel, String(控制者名称)等</param>
        /// <param name="classID">组件的类别ID</param>
        /// <returns>各子功能配置的状态，若找不到类别ID对应的组件或无子功能配置则返回null</returns>
        public static ConfigStatus[] GetModuleChildConfigStatus(object caller, String classID)
        {
            return Handler.GetModuleChildConfigStatus(caller, classID);
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
        public static Dictionary<BusDeviceID, BusDeviceInfo> GetBusDevices()
        {
            return Handler.GetBusDevices();
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
        public static Dictionary<VideoDeviceID, VideoDeviceInfo> GetVideoDevices()
        {
            return Handler.GetVideoDevices();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all bus protocol file IDs
        /// </summary>
        /// <returns>All bus protocol file IDs</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取总线协议文件ID列表
        /// </summary>
        /// <returns>总线协议文件ID列表</returns>
        public static BusProtocolFileID[] GetBusProtocolFileIDList()
        {
            return Handler.GetBusProtocolFileIDList();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get the path of bus protocol file
        /// </summary>
        /// <param name="fileID">Bus protocol file ID</param>
        /// <returns>The path of bus protocol file, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取总线协议对应文件的路径
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        /// <returns>总线协议文件路径，若未找到返回null</returns>
        public static String GetBusProtocolFilePath(BusProtocolFileID fileID)
        {
            return Handler.GetBusProtocolFilePath(fileID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Update the path of bus protocol file (Only single channel protocol supported)
        /// </summary>
        /// <param name="fileID">Bus protocol file ID</param>
        /// <param name="filePath">New path</param>
        /// <returns>Whether successfull, false if the file is not found or MD5 doesn't match</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 更新总线协议文件路径(仅支持单通道的情况)
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        /// <param name="filePath">新路径</param>
        /// <returns>是否成功更新，false表示未找到文件或MD5不匹配</returns>
        public static bool UpdateBusProtocolFilePath(BusProtocolFileID fileID, String filePath)
        {
            return Handler.UpdateBusProtocolFilePath(fileID, filePath);
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
        public static BusProtocolFileState GetBusProtocolFileState(BusProtocolFileID fileID)
        {
            return Handler.GetBusProtocolFileState(fileID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Add bus protocol file (For multiple channel protocol, only the first channel will be added)
        /// </summary>
        /// <param name="filePath">File path of bus protocol file</param>
        /// <param name="fileID">Output bus protocol file ID, null if the file doesn't exist</param>
        /// <returns>Whether it's a new protocol, or else it already exists</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 添加新的总线协议文件(多通道协议文件将按单通道处理，输出首个通道)
        /// </summary>
        /// <param name="filePath">总线协议文件路径</param>
        /// <param name="fileID">总线协议文件ID，若文件存在则输出该ID，否则输出null</param>
        /// <returns>是否为新添加的总线协议</returns>
        public static bool AddBusProtocolFile(String filePath, out BusProtocolFileID fileID)
        {
            return Handler.AddBusProtocolFile(filePath, out fileID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.3) Add bus protocol file
        /// </summary>
        /// <param name="filePath">File path of bus protocol file</param>
        /// <returns>The result</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.3) 添加新的总线协议文件
        /// </summary>
        /// <param name="filePath">总线协议文件路径</param>
        /// <returns>添加结果</returns>
        public static AddBusProtocolResult AddBusProtocolFile(String filePath)
        {
            return Handler.AddBusProtocolFile(filePath);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Remove bus protocol
        /// </summary>
        /// <param name="fileID">Bus protocol file ID</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 移除总线协议
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        public static void RemoveBusProtocolFile(BusProtocolFileID fileID)
        {
            Handler.RemoveBusProtocolFile(fileID);
        }

        /// \~English
        /// <summary>
        /// Get version of all native plugins
        /// </summary>
        /// <param name="prefix">Prefix, like "bus", "video", "proc", "dev", etc.</param>
        /// <returns>Dictionary. The key is plugin's type ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取原生插件版本列表
        /// </summary>
        /// <param name="prefix">插件前缀，如bus、video、proc、dev等</param>
        /// <returns>版本列表，键为原生插件的类型ID，值为版本字符串</returns>
        public static Dictionary<String, String> GetNativePluginVersions(String prefix)
        {
            return Handler.GetNativePluginVersions(prefix);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get version of all native plugins
        /// </summary>
        /// <param name="type">Type of native plugin</param>
        /// <returns>Dictionary. The key is plugin's type ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取原生插件插件版本列表
        /// </summary>
        /// <param name="type">原生库类别</param>
        /// <returns>版本列表，键为原生插件的类型ID</returns>
        public static  Dictionary<String, Version> GetNativePluginVersions(NativeLibraryType type)
        {
            return Handler.GetNativePluginVersions(type);
        }

        /// \~English
        /// <summary>
        /// Show a modal dialog to configure offline map's path
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 弹出对话框配置离线地图路径
        /// </summary>
        public static void ConfigOfflineMapPath()
        {
            Handler.ConfigOfflineMapPath();
        }

        /// \~English
        /// <summary>
        /// Query offline map's image
        /// </summary>
        /// <param name="imageSize">Image size</param>
        /// <param name="centerLocation">Location of the image's center</param>
        /// <param name="zoom">Scale, ranges 0~24</param>
        /// <returns>Platform image of offline map, null if failed to query</returns>
        /// \~Chinese
        /// <summary>
        /// 获取离线地图图像
        /// </summary>
        /// <param name="imageSize">指定图像大小</param>
        /// <param name="centerLocation">图像中心的经纬度</param>
        /// <param name="zoom">图像的尺度，0~24</param>
        /// <returns>离线地图图像（平台特化），空表示获取失败</returns>
        public static object GetOfflineMapImage(IntSize imageSize, LocPoint centerLocation, int zoom)
        {
            return Handler.GetOfflineMapImage(imageSize, centerLocation, zoom);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Query offline map's image
        /// </summary>
        /// <param name="imageSize">Image size</param>
        /// <param name="centerLocation">Location of the image's center</param>
        /// <param name="zoom">Scale, ranges 0~24</param>
        /// <returns>Common image of offline map, null if failed to query</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取离线地图图像
        /// </summary>
        /// <param name="imageSize">指定图像大小</param>
        /// <param name="centerLocation">图像中心的经纬度</param>
        /// <param name="zoom">图像的尺度，0~24</param>
        /// <returns>离线地图图像（通用图像数据，BGR不逆序），空表示获取失败</returns>
        public static CommonImage GetOfflineMapCommonImage(IntSize imageSize, LocPoint centerLocation, int zoom)
        {
            return Handler.GetOfflineMapCommonImage(imageSize, centerLocation, zoom);
        }

        /// \~English
        /// <summary>
        /// Convert location coordinate to offline map image's coordinate
        /// </summary>
        /// <param name="origin">Location of the image's origin</param>
        /// <param name="zoom">Scale, ranges 0~24</param>
        /// <param name="point">The location coordinate</param>
        /// <returns>Coordinate in offline map image</returns>
        /// \~Chinese
        /// <summary>
        /// 离线地图中经纬度坐标转为像素坐标
        /// </summary>
        /// <param name="origin">原点的经纬度坐标</param>
        /// <param name="zoom">图像的尺度，0~24</param>
        /// <param name="point">输入经纬度坐标</param>
        /// <returns>该经纬度对应的像素坐标</returns>
        public static FloatPoint ConvertOfflineMapLocToPix(LocPoint origin, int zoom, LocPoint point)
        {
            return Handler.ConvertOfflineMapLocToPix(origin, zoom, point);
        }

        /// \~English
        /// <summary>
        /// Convert offline map image's coordinate to location coordinate
        /// </summary>
        /// <param name="origin">Location of the image's origin</param>
        /// <param name="zoom">Scale, ranges 0~24</param>
        /// <param name="pixel">Coordinate in offline map image</param>
        /// <returns>The location coordinate</returns>
        /// \~Chinese
        /// <summary>
        /// 离线地图中像素坐标转为经纬度坐标
        /// </summary>
        /// <param name="origin">原点的经纬度坐标</param>
        /// <param name="zoom">图像的尺度，0~24</param>
        /// <param name="pixel">输入像素坐标</param>
        /// <returns>该像素坐标对应的经纬度坐标</returns>
        public static LocPoint ConvertOfflineMapPixToLoc(LocPoint origin, int zoom, FloatPoint pixel)
        {
            return Handler.ConvertOfflineMapPixToLoc(origin, zoom, pixel);
        }

        /// \~English
        /// <summary>
        /// Show a modal dialog to select signal
        /// </summary>
        /// <param name="origin">Initial signal configuration</param>
        /// <param name="withScale">Whether to enable the configuration of value scale</param>
        /// <param name="withSignBit">Whether to enable the configuration of sign bit signal</param>
        /// <param name="unit">The unit to display, only available while the configuration of value scale is enabled</param>
        /// <returns>Signal configuration result, null if requested to delete</returns>
        /// \~Chinese
        /// <summary>
        /// 打开对话框选择信号
        /// </summary>
        /// <param name="origin">初始信号配置</param>
        /// <param name="withScale">是否包含乘数的配置</param>
        /// <param name="withSignBit">是否包含符号位信号的配置</param>
        /// <param name="unit">该信号的单位显示，仅当包含乘数配置时有效</param>
        /// <returns>返回信号配置，若删除则返回null</returns>
        public static SignalConfig SelectSignal(SignalConfig origin, bool withScale, bool withSignBit, String unit)
        {
            return Handler.SelectSignal(origin, withScale, withSignBit, unit);
        }

        /// \~English
        /// <summary>
        /// Show a modal dialog to select bus message
        /// </summary>
        /// <param name="originMessageID">Initial bus message configuration</param>
        /// <returns>Bus message configuration result, null if requested to delete</returns>
        /// \~Chinese
        /// <summary>
        /// 打开对话框选择总线报文
        /// </summary>
        /// <param name="originMessageID">初始总线报文配置</param>
        /// <returns>返回总线报文配置，若删除则返回null</returns>
        public static String SelectBusMessage(String originMessageID)
        {
            return Handler.SelectBusMessage(originMessageID);
        }

        /// \~English
        /// <summary>
        /// Create a video frame getter
        /// </summary>
        /// <returns>Video frame getter</returns>
        /// \~Chinese
        /// <summary>
        /// 创建视频帧获取器
        /// </summary>
        /// <returns>视频帧获取器</returns>
        public static VideoFrameGetter CreateVideoFrameGetter()
        {
            return Handler.CreateVideoFrameGetter();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.9.0) Create a video frame getter (updated)
        /// </summary>
        /// <returns>Video frame getter (updated)</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.9.0) 创建视频帧获取器(扩展版)
        /// </summary>
        /// <returns>视频帧获取器(扩展版)</returns>
        public static VideoFrameGetterX CreateVideoFrameGetterX()
        {
            return Handler.CreateVideoFrameGetterX();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.15.4) Get the nearest video frame's preview JPEG data from the specified time
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="timeline">Target timeline point, in seconds</param>
        /// <param name="maxGap">Max time gap, in seconds</param>
        /// <param name="timestamp">Timestamp of output image, null if failed to query</param>
        /// <param name="cameraInfo">Camera information, null if failed to query</param>
        /// <returns>Video frame's preview JPEG data, image width is 640 pix, null if failed to query</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.4) 获取距离指定时间最近的视频帧的预览JPEG图像数据
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="timeline">获取视频帧的目标时间线，单位秒</param>
        /// <param name="maxGap">容许的最大间隔，单位秒</param>
        /// <param name="timestamp">输出图像的时间戳，获取失败则为null</param>
        /// <param name="cameraInfo">摄像头信息，获取失败则为null</param>
        /// <returns>视频帧的预览JPEG数据，图像宽度为640像素，获取失败则返回null</returns>
        public static byte[] GetPreviewJpeg(int channel, double timeline, double maxGap, out Timestamp? timestamp, out CameraInfo cameraInfo)
        {
            return Handler.GetPreviewJpeg(channel, timeline, maxGap, out timestamp, out cameraInfo);
        }

        /// \~English
        /// <summary>
        /// Show a modal dialog to select multiple signals at once
        /// </summary>
        /// <param name="handler">Callback to handle signal selection</param>
        /// <param name="existSignalIDList">List of all signal IDs that already exist</param>
        /// \~Chinese
        /// <summary>
        /// 打开对话框一次性选择多个信号
        /// </summary>
        /// <param name="handler">选中信号时调用的回调接口</param>
        /// <param name="existSignalIDList">既存的选中信号ID列表</param>
        public static void SelectSignals(SelectSignalHandler handler, List<String> existSignalIDList)
        {
            Handler.SelectSignals(handler, existSignalIDList);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.16.7) Show a modal dialog to select multiple bus messages at once
        /// </summary>
        /// <param name="handler">Callback to handle bus message selection</param>
        /// <param name="existBusMessageIDList">List of all bus message IDs that already exist</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.16.7) 打开对话框一次性选择多个总线报文
        /// </summary>
        /// <param name="handler">选中总线报文时调用的回调接口</param>
        /// <param name="existBusMessageIDList">既存的选中总线报文ID列表</param>
        public static void SelectBusMessages(SelectBusMessageHandler handler, List<String> existBusMessageIDList)
        {
            Handler.SelectBusMessages(handler, existBusMessageIDList);
        }

        /// \~English
        /// Deprecated, unnecessary to configure
        /// \~Chinese
        /// <summary>
        /// 已弃用，无需再配置
        /// </summary>
        public static List<String> SelectBusFloat32Signal(List<String> existSignalIDList)
        {
            return Handler.SelectBusFloat32Signal(existSignalIDList);
        }

        /// \~English
        /// <summary>
        /// Open a dialog
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="dialogClassID">Dialog class ID</param>
        /// <param name="config">Configuration string</param>
        /// \~Chinese
        /// <summary>
        /// 打开对话框
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="config">初始化配置</param>
        public static void OpenDialog(object caller, String dialogClassID, String config)
        {
            Handler.OpenDialog(caller, dialogClassID, config);
        }

        /// \~English
        /// <summary>
        /// Add a window to workspace
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="windowClassID">Window class ID</param>
        /// <param name="config">Configuration string</param>
        /// <param name="newWorkspaceIfNeeded">Whether to add to a new workspace (if available) if there's no space in the current workspace</param>
        /// \~Chinese
        /// <summary>
        /// 添加窗口至工作空间
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <param name="config">初始化配置</param>
        /// <param name="newWorkspaceIfNeeded">如果当前工作空间位置不足，是否添加至新工作空间（如果支持）</param>
        public static void AddWindow(object caller, String windowClassID, String config, bool newWorkspaceIfNeeded)
        {
            Handler.AddWindow(caller, windowClassID, config, newWorkspaceIfNeeded);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.15.0) Run console procedure
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="consoleClassID">Console class ID</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.0) 运行控制台过程
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="consoleClassID">控制台组件ID</param>
        public static void RunConsole(object caller, string consoleClassID)
        {
            Handler.RunConsole(caller, consoleClassID);
        }

        /// \~English
        /// <summary>
        /// Set window's title and icon
        /// </summary>
        /// <param name="window">Window panel object</param>
        /// <param name="title">The title, null to not to change</param>
        /// <param name="icon">The icon, null to not to change</param>
        /// \~Chinese
        /// <summary>
        /// 设置窗口标题与图标
        /// </summary>
        /// <param name="window">窗口对象</param>
        /// <param name="title">标题，若null则不更改</param>
        /// <param name="icon">图标，若null则不更改</param>
        public static void SetWindowTitle(object window, String title, object icon)
        {
            Handler.SetWindowTitle(window, title, icon);
        }

        /// \~English
        /// <summary>
        /// Set current dialog's title and icon
        /// </summary>
        /// <param name="title">The title, null to not to change</param>
        /// <param name="icon">The icon, null to not to change</param>
        /// \~Chinese
        /// <summary>
        /// 设置当前对话框的标题与图标
        /// </summary>
        /// <param name="title">标题，若null则不更改</param>
        /// <param name="icon">图标，若null则不更改</param>
        public static void SetCurrentDialogTitle(String title, object icon)
        {
            Handler.SetCurrentDialogTitle(title, icon);
        }

        /// \~English
        /// <summary>
        /// Show modal dialog to select (multiple) bus protocols
        /// </summary>
        /// <param name="selected">Bus protocols already selected</param>
        /// <returns>Newly selected bus protocols</returns>
        /// \~Chinese
        /// <summary>
        /// 打开对话框选择总线协议文件（可多个）
        /// </summary>
        /// <param name="selected">已选择的总线协议文件</param>
        /// <returns>新选择的总线协议文件</returns>
        public static BusProtocolFileID[] SelectBusProtocolFiles(BusProtocolFileID[] selected)
        {
            return Handler.SelectBusProtocolFiles(selected);
        }

        /// \~English
        /// <summary>
        /// Show modal dialog to configure data encryption
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 打开对话框配置文件加密选项
        /// </summary>
        public static void ConfigDataEncryption()
        {
            Handler.ConfigDataEncryption();
        }

        /// \~English
        /// <summary>
        /// Register audio recorders and players related to the specified driver
        /// </summary>
        /// <param name="driver">Driver</param>
        /// <param name="recorder">Recorders, set to null if there's none</param>
        /// <param name="replayer">Players, set to null if there's none</param>
        /// \~Chinese
        /// <summary>
        /// 注册音频驱动关联的采集和回放接口
        /// </summary>
        /// <param name="driver">驱动信息</param>
        /// <param name="recorder">采集接口，若无则设置null</param>
        /// <param name="replayer">回放接口，若无则设置额null</param>
        public static void RegisterAudioDriver(AudioDriverInfo driver, AudioRecorder recorder, AudioReplayer replayer)
        {
            Handler.RegisterAudioDriver(driver, recorder, replayer);
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
        public static AudioDriverInfo[] GetAudioDrivers()
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
        public static AudioDeviceInfo[] GetAudioRecordDevices(String driverID)
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
        public static AudioDeviceInfo[] GetAudioReplayDevices(String driverID)
        {
            return Handler.GetAudioReplayDevices(driverID);
        }
        
        /// \~English
        /// <summary>
        /// (api:app=2.0.6) Get CPU time (starting from host machine powered on)
        /// </summary>
        /// <returns>CPU time, in seconds, 0 means invalid</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.0.6) 获取CPU时间（从开机起算的时间）
        /// </summary>
        /// <returns>CPU时间，单位秒，返回0表示无效</returns>
        public static double GetCPUTime()
        {
            return Handler.GetCPUTime();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.2.3) Get list of all event types (include not enabled)
        /// </summary>
        /// <returns>List of all event types</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.2.3) 获取事件类型名称列表（包括未启用的）
        /// </summary>
        /// <returns>事件类型名称列表</returns>
        public static String[] GetEventTypeNames()
        {
            return Handler.GetEventTypeNames();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.2.3) Parse bus message data, and get signal values
        /// </summary>
        /// <param name="busMessage">Bus message data</param>
        /// <returns>Signal values and other information</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.2.3) 解析总线报文，获取信号值等信息
        /// </summary>
        /// <param name="busMessage">总线报文</param>
        /// <returns>所有信号值及相关信息</returns>
        public static BusSignalValue[] ParseBusMessage(BusMessageSample busMessage)
        {
            return Handler.ParseBusMessage(busMessage);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Deprecated, use ASEva.Agency.GetRecentProjectPaths
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 已弃用，应使用 ASEva.Agency.GetRecentProjectPaths
        /// </summary>
        public static String[] GetRecentProjectPathes()
        {
            return Handler.GetRecentProjectPaths();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.12) Get paths of recent project files
        /// </summary>
        /// <returns>Paths of recent project files</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.12) 获取最近项目文件路径列表
        /// </summary>
        /// <returns>最近项目文件路径列表</returns>
        public static String[] GetRecentProjectPaths()
        {
            return Handler.GetRecentProjectPaths();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Try to terminate application
        /// </summary>
        /// <param name="force">Whether forced to terminate</param>
        /// <param name="autosave">Whether to save current project to autosave in the folder of application's configuration files</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 尝试终止应用程序
        /// </summary>
        /// <param name="force">是否强制终止</param>
        /// <param name="autosave">是否保存当前工程至autosave至应用程序的配置文件目录</param>
        /// <returns>是否成功终止</returns>
        public static bool TerminateApp(bool force, bool autosave)
        {
            return Handler.TerminateApp(force, autosave);
        }
        
        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Show a modal dialog to display error message
        /// </summary>
        /// <param name="msg">Error message</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 弹出模态框显示错误信息
        /// </summary>
        /// <param name="msg">错误信息</param>
        public static void PopupError(String msg)
        {
            Handler.PopupError(msg);
        }
        
        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Show a modal dialog to display notice
        /// </summary>
        /// <param name="msg">Notice</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 弹出模态框显示提示信息
        /// </summary>
        /// <param name="msg">提示信息</param>
        public static void PopupNotice(String msg)
        {
            Handler.PopupNotice(msg);
        }
        
        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Show a modal dialog for user to confirm
        /// </summary>
        /// <param name="msg">Message for user to confirm</param>
        /// <returns>Whether confirmed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 弹出模态框显示确认信息
        /// </summary>
        /// <param name="msg">确认信息</param>
        /// <returns>是否得到确认</returns>
        public static bool PopupConfirm(String msg)
        {
            return Handler.PopupConfirm(msg);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Add checkpoint for main thread
        /// </summary>
        /// <param name="location">Checkpoint location</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 添加主线程检查点
        /// </summary>
        /// <param name="location">主线程检查点位置</param>
        public static void AddMainThreadCheckpoint(String location)
        {
            Handler.AddMainThreadCheckpoint(location);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Create blank project
        /// </summary>
        /// <param name="force">Whether forced to create new project</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 新建空白项目
        /// </summary>
        /// <param name="force">是否强制新建空白项目</param>
        /// <returns>是否成功新建项目</returns>
        public static bool NewProject(bool force)
        {
            return Handler.NewProject(force);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Open project
        /// </summary>
        /// <param name="projectFile">Path of project file, set to null to load autosaved in the folder of application's configuration files</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 打开项目
        /// </summary>
        /// <param name="projectFile">项目文件路径，若设为null则从autosave读取</param>
        /// <returns>是否成功打开项目</returns>
        public static bool OpenProject(String projectFile)
        {
            return Handler.OpenProject(projectFile);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.15.8) Open project
        /// </summary>
        /// <param name="projectFile">Path of project file, set to null to load autosaved in the folder of application's configuration files</param>
        /// <param name="force">Whether forced to open the project</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.8) 打开项目
        /// </summary>
        /// <param name="projectFile">项目文件路径，若设为null则从autosave读取</param>
        /// <param name="force">是否强制打开项目</param>
        /// <returns>是否成功打开项目</returns>
        public static bool OpenProject(String projectFile, bool force)
        {
            return Handler.OpenProject(projectFile, force);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Save project
        /// </summary>
        /// <param name="projectFile">Path of project file, set to null to write to autosave in the folder of application's configuration files</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 保存当前项目
        /// </summary>
        /// <param name="projectFile">项目文件路径，null表示保存至当前项目文件</param>
        /// <returns>是否成功保存项目</returns>
        public static bool SaveCurrentProject(String projectFile)
        {
            return Handler.SaveCurrentProject(projectFile);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get current project file's path
        /// </summary>
        /// <returns>Current project file's path, null if it's a new project or loaded from autosaved</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取当前项目文件
        /// </summary>
        /// <returns>当前项目文件，新项目或从autosave读取的项目都为null</returns>
        public static String GetCurrentProject()
        {
            return Handler.GetCurrentProject();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Play mp3 audio
        /// </summary>
        /// <param name="mp3FileData">MP3 audio data</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 播放MP3音频
        /// </summary>
        /// <param name="mp3FileData">MP3音频文件数据</param>
        public static void PlayMp3(byte[] mp3FileData)
        {
            Handler.PlayMp3(mp3FileData);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Start a process to open file, folder or web site
        /// </summary>
        /// <param name="target">Path of file, folder or web site</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 启动进程以默认方式打开文件、文件夹或网址
        /// </summary>
        /// <param name="target">目标文件、文件夹或网址</param>
        /// <returns>是否成功打开</returns>
        public static bool StartProcess(String target)
        {
            return Handler.StartProcess(target);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get application's version info
        /// </summary>
        /// <returns>Dictionary. The key is version title</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取软件版本信息总表
        /// </summary>
        /// <returns>软件版本信息总表</returns>
        public static Dictionary<String, Version> GetVersionTable()
        {
            return Handler.GetVersionTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get system status
        /// </summary>
        /// <param name="status">Type of system status</param>
        /// <returns>Information of system status, null if no available info</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取系统状态信息
        /// </summary>
        /// <param name="status">系统状态类别</param>
        /// <returns>系统状态信息，若无有效信息则返回null</returns>
        public static String GetSystemStatus(SystemStatus status)
        {
            return Handler.GetSystemStatus(status);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get details of system status
        /// </summary>
        /// <param name="status">Type of system status</param>
        /// <returns>Details of system status, null if no available info</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取系统状态详情
        /// </summary>
        /// <param name="status">系统状态类别</param>
        /// <returns>系统状态详情，若无有效信息则返回null</returns>
        public static String GetSystemStatusDetails(SystemStatus status)
        {
            return Handler.GetSystemStatusDetails(status);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all licensed function indices
        /// </summary>
        /// <returns>All licensed function indices</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取被许可的功能序号列表
        /// </summary>
        /// <returns>被许可的功能序号列表</returns>
        public static int[] GetLicensedFunctionIndices()
        {
            return Handler.GetLicensedFunctionIndices();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Switch application's mode
        /// </summary>
        /// <param name="controllerName">Controller name, for exclusive control</param>
        /// <param name="mode">Target mode</param>
        /// <param name="waitSecond">Timeout in seconds, only available while larger than 0</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 切换应用程序当前的运行模式
        /// </summary>
        /// <param name="controllerName">控制者名称，用于独占控制模式</param>
        /// <param name="mode">目标运行模式</param>
        /// <param name="waitSecond">超时，大于0有效，单位秒</param>
        /// <returns>是否成功切换</returns>
        public static bool SwitchAppMode(String controllerName, ApplicationMode mode, int waitSecond)
        {
            return Handler.SwitchAppMode(controllerName, mode, waitSecond);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set data path
        /// </summary>
        /// <param name="path">The data path</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置数据目录的路径
        /// </summary>
        /// <param name="path">数据目录的路径</param>
        public static void SetDataPath(String path)
        {
            Handler.SetDataPath(path);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get information of all window classes
        /// </summary>
        /// <returns>Dictionary. The key is window class ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取窗口组件信息表
        /// </summary>
        /// <returns>窗口组件信息表，键为组件ID</returns>
        public static Dictionary<String, WindowClassInfo> GetWindowClassTable()
        {
            return Handler.GetWindowClassTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get information of all dialog classes
        /// </summary>
        /// <returns>Dictionary. The key is dialog class ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取对话框组件信息表
        /// </summary>
        /// <returns>对话框组件信息表，键为组件ID</returns>
        public static Dictionary<String, DialogClassInfo> GetDialogClassTable()
        {
            return Handler.GetDialogClassTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get information of all app-layer processor classes
        /// </summary>
        /// <returns>Dictionary. The key is processor class ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取数据处理组件信息表
        /// </summary>
        /// <returns>数据处理组件信息表，键为组件ID</returns>
        public static Dictionary<String, ProcessorClassInfo> GetProcessorClassTable()
        {
            return Handler.GetProcessorClassTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get information of all native-layer component classes
        /// </summary>
        /// <returns>Dictionary. The key is native-layer component's class ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取原生组件信息表
        /// </summary>
        /// <returns>原生组件信息表，键为组件ID</returns>
        public static Dictionary<String, NativeClassInfo> GetNativeClassTable()
        {
            return Handler.GetNativeClassTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get information of all standalone task classes
        /// </summary>
        /// <returns>Dictionary. The key is task class ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取独立任务组件信息表
        /// </summary>
        /// <returns>独立任务组件信息表，键为组件ID</returns>
        public static Dictionary<String, TaskClassInfo> GetTaskClassTable()
        {
            return Handler.GetTaskClassTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.8.0) Get information of all app-layer device classes
        /// </summary>
        /// <returns>Dictionary. The key is device class ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.8.0) 获取设备组件信息表
        /// </summary>
        /// <returns>设备组件信息表，键为组件ID</returns>
        public static Dictionary<String, DeviceClassInfo> GetDeviceClassTable()
        {
            return Handler.GetDeviceClassTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.15.2) Get information of all console classes
        /// </summary>
        /// <returns>Dictionary. The key is console class ID</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.2) 获取控制台组件信息表
        /// </summary>
        /// <returns>控制台组件信息表，键为组件ID</returns>
        public static Dictionary<string, ConsoleClassInfo> GetConsoleClassTable()
        {
            return Handler.GetConsoleClassTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Register transformed window class through configuration string
        /// </summary>
        /// <param name="windowClassID">Original window class's ID</param>
        /// <param name="config">Configuration string</param>
        /// <returns>Information of transformed window class</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 通过配置注册分化窗口组件
        /// </summary>
        /// <param name="windowClassID">原窗口组件ID</param>
        /// <param name="config">用于分化的配置字符串</param>
        /// <returns>分化后的窗口组件信息</returns>
        public static WindowClassInfo RegisterTransformWindowClass(String windowClassID, String config)
        {
            return Handler.RegisterTransformWindowClass(windowClassID, config);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.5.0) Register transformed window class directly
        /// </summary>
        /// <param name="windowClassID">Original window class's ID</param>
        /// <param name="transformWindowClass">Transformed window class object</param>
        /// <param name="defaultConfig">Default configuration string</param>
        /// <returns>Information of transformed window class</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.5.0) 直接注册分化窗口组件
        /// </summary>
        /// <param name="windowClassID">原窗口组件ID</param>
        /// <param name="transformWindowClass">分化窗口组件类</param>
        /// <param name="defaultConfig">默认的可用于分化的配置字符串</param>
        /// <returns>分化后的窗口组件信息</returns>
        public static WindowClassInfo RegisterTransformWindowClass(String windowClassID, WindowClass transformWindowClass, String defaultConfig)
        {
            return Handler.RegisterTransformWindowClass(windowClassID, transformWindowClass, defaultConfig);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Register transformed dialog class through configuration string
        /// </summary>
        /// <param name="dialogClassID">Original dialog class's ID</param>
        /// <param name="config">Configuration string</param>
        /// <returns>Information of transformed dialog class</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 通过配置注册分化对话框组件
        /// </summary>
        /// <param name="dialogClassID">原对话框组件ID</param>
        /// <param name="config">用于分化的配置字符串</param>
        /// <returns>分化后的对话框组件信息</returns>
        public static DialogClassInfo RegisterTransformDialogClass(String dialogClassID, String config)
        {
            return Handler.RegisterTransformDialogClass(dialogClassID, config);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.5.0) Register transformed dialog class directly
        /// </summary>
        /// <param name="dialogClassID">Original dialog class's ID</param>
        /// <param name="transformDialogClass">Transformed dialog class object</param>
        /// <param name="defaultConfig">Default configuration string</param>
        /// <returns>Information of transformed dialog class</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.5.0) 直接注册分化对话框组件
        /// </summary>
        /// <param name="dialogClassID">原对话框组件ID</param>
        /// <param name="transformDialogClass">分化对话框组件类</param>
        /// <param name="defaultConfig">默认的可用于分化的配置字符串</param>
        /// <returns>分化后的对话框组件信息</returns>
        public static DialogClassInfo RegisterTransformDialogClass(String dialogClassID, DialogClass transformDialogClass, String defaultConfig)
        {
            return Handler.RegisterTransformDialogClass(dialogClassID, transformDialogClass, defaultConfig);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get status of components related to the dialog
        /// </summary>
        /// <param name="dialogClassID">Dialog component's class ID</param>
        /// <param name="transformID">Transform ID</param>
        /// <param name="childrenStatus">Output child status</param>
        /// <returns>The main status</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取对话框相关组件配置状态
        /// </summary>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="transformID">分化ID</param>
        /// <param name="childrenStatus">子配置状态</param>
        /// <returns>配置状态</returns>
        public static ConfigStatus GetDialogRelatedModulesConfigStatus(String dialogClassID, String transformID, out ConfigStatus[] childrenStatus)
        {
            return Handler.GetDialogRelatedModulesConfigStatus(dialogClassID, transformID, out childrenStatus);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.15.1) Get status of components related to the console
        /// </summary>
        /// <param name="dialogClassID">Console component's class ID</param>
        /// <param name="childrenStatus">Output child status</param>
        /// <returns>The main status</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.1) 获取控制台相关组件配置状态
        /// </summary>
        /// <param name="dialogClassID">控制台组件ID</param>
        /// <param name="childrenStatus">子配置状态</param>
        /// <returns>配置状态</returns>
        public static ConfigStatus GetConsoleRelatedModulesConfigStatus(String dialogClassID, out ConfigStatus[] childrenStatus)
        {
            return Handler.GetConsoleRelatedModulesConfigStatus(dialogClassID, out childrenStatus);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Disable all components (Some may not be able to disabled)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 禁用所有组件配置
        /// </summary>
        public static void DisableAllConfigs()
        {
            Handler.DisableAllConfigs();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.4.2) Get all generations of the session under current data layer
        /// </summary>
        /// <param name="sessionID">Session ID</param>
        /// <returns>Generation IDs</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.2) 获取当前层级下指定session下的所有generation ID
        /// </summary>
        /// <param name="sessionID">Session ID</param>
        /// <returns>Generation ID列表</returns>
        public static String[] GetSessionGenerations(DateTime sessionID)
        {
            return Handler.GetSessionGenerations(sessionID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all sessions that contain generation with the specified ID under current data layer
        /// </summary>
        /// <param name="generationID">Generation ID</param>
        /// <returns>Session IDs</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取当前层级下含有指定generation ID的所有session
        /// </summary>
        /// <param name="generationID">Generation ID</param>
        /// <returns>Session ID列表</returns>
        public static DateTime[] GetGenerationSessions(String generationID)
        {
            return Handler.GetGenerationSessions(generationID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set target replay speed for replaying or offline processing
        /// </summary>
        /// <param name="speed">Target replay speed</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置目标回放速度，用于回放或离线处理
        /// </summary>
        /// <param name="speed">目标回放速度</param>
        public static void SetTargetReplaySpeed(double speed)
        {
            Handler.SetTargetReplaySpeed(speed);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set search keyword for session filtering
        /// </summary>
        /// <param name="keyword">Search keyword for session filtering</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置session搜索关键字
        /// </summary>
        /// <param name="keyword">session搜索关键字</param>
        public static void SetSessionSearchKeyword(String keyword)
        {
            Handler.SetSessionSearchKeyword(keyword);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get filter flags of all sessions
        /// </summary>
        /// <returns>Filter flags of all sessions</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取所有session的筛选标志位
        /// </summary>
        /// <returns>session的筛选标志位表</returns>
        public static Dictionary<DateTime, SessionFilterFlags> GetSessionFilterTable()
        {
            return Handler.GetSessionFilterTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get ID of the session currently recording or previewing in online mode or remote mode
        /// </summary>
        /// <returns>Session ID, null if neither in online mode nor in remote mode</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取采集模式(在线/远程)下正在预览或采集的session
        /// </summary>
        /// <returns>采集模式(在线/远程)下正在预览或采集的session，若非采集模式则返回null</returns>
        public static DateTime? GetCurrentOnlineSession()
        {
            return Handler.GetCurrentOnlineSession();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get total length of all sessions (not filtered)
        /// </summary>
        /// <returns>Total length of all sessions</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取当前层级下所有session的总时长（未筛选）
        /// </summary>
        /// <returns>所有session的总时长</returns>
        public static double GetSessionListTotalLength()
        {
            return Handler.GetSessionListTotalLength();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get current session search key
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取当前的session搜索关键字
        /// </summary>
         public static String GetSessionSearchKey()
        {
            return Handler.GetSessionSearchKey();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Remove session and delete files to recycle bin
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="force">Whether forced to remove</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 移除session及相关文件至回收站
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="force">是否强制移除</param>
        /// <returns>是否成功移除</returns>
        public static bool RemoveSession(DateTime session, bool force)
        {
            return Handler.RemoveSession(session, force);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set session's check filter
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="check">Whether checked</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置session是否框选
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="check">是否框选</param>
        public static void SetSessionChecker(DateTime session, bool check)
        {
            Handler.SetSessionChecker(session, check);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Remove generation and delete files to recycle bin
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="genID">Generation ID</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 移除generation及相关文件至回收站
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="genID">Generation ID</param>
        public static void RemoveGeneration(DateTime session, String genID)
        {
            Handler.RemoveGeneration(session, genID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get session's comment
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session's comment</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取session的注释说明
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session的注释说明</returns>
        public static String GetSessionComment(DateTime session)
        {
            return Handler.GetSessionComment(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set session's comment
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="comment">Session's comment</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置session的注释说明
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="comment">Session的注释说明</param>
        public static void SetSessionComment(DateTime session, String comment)
        {
            Handler.SetSessionComment(session, comment);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get session's properties
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Dictionary. The key is session's property title</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取session的属性表
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session的属性表</returns>
        public static Dictionary<String, String> GetSessionProperties(DateTime session)
        {
            return Handler.GetSessionProperties(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set session's properties
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="properties">Dictionary. The key is session's property title</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置session的属性表
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="properties">Session的属性表</param>
        public static void SetSessionProperties(DateTime session, Dictionary<String, String> properties)
        {
            Handler.SetSessionProperties(session, properties);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Get whether the host machine is synchronized with time server while the session is being recorded
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Whether the host machine is synchronized with time server</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 获取session采集时主机是否与授时服务器同步
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>主机是否与授时服务器同步</returns>
        public static bool GetSessionHostSync(DateTime session)
        {
            return Handler.GetSessionHostSync(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Set whether the host machine is synchronized with time server while the session is being recorded
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="hostSync">Whether the host machine is synchronized with time server</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 设置session采集时主机是否与授时服务器同步
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="hostSync">主机是否与授时服务器同步</param>
        public static void SetSessionHostSync(DateTime session, bool hostSync)
        {
            Handler.SetSessionHostSync(session, hostSync);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set audio volume
        /// </summary>
        /// <param name="volume">Audio volume (times)</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置音量
        /// </summary>
        /// <param name="volume">音量（倍数）</param>
        public static void SetAudioVolume(double volume)
        {
            Handler.SetAudioVolume(volume);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Deprecated, no need to use
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 已弃用，无需再调用
        /// </summary>
        public static void SetCPURateScale(int scale)
        {
            Handler.SetCPURateScale(scale);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get license information
        /// </summary>
        /// <returns>License information</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取许可证的详细信息
        /// </summary>
        /// <returns>许可证的详细信息</returns>
        public static String GetLicenseInfo()
        {
            return Handler.GetLicenseInfo();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get time shift configuration for general raw data channel
        /// </summary>
        /// <param name="id">General raw data's channel ID</param>
        /// <returns>Time shift, in milliseconds</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取原始数据通道延迟配置
        /// </summary>
        /// <param name="id">原始数据通道ID</param>
        /// <returns>延迟配置，单位毫秒</returns>
        public static double GetRawChannelDelayConfig(String id)
        {
            return Handler.GetRawChannelDelayConfig(id);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get time shift configuration for bus raw data channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <returns>Time shift, in milliseconds</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取总线数据通道延迟配置
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <returns>延迟配置，单位毫秒</returns>
        public static double GetBusChannelDelayConfig(int channel)
        {
            return Handler.GetBusChannelDelayConfig(channel);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get time shift configuration for video raw data channel
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <returns>Time shift, in milliseconds</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取视频数据通道延迟配置
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <returns>延迟配置，单位毫秒</returns>
        public static double GetVideoChannelDelayConfig(int channel)
        {
            return Handler.GetVideoChannelDelayConfig(channel);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get time shift configuration for audio data channel
        /// </summary>
        /// <returns>Time shift, in milliseconds</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取音频数据通道延迟配置
        /// </summary>
        /// <returns>延迟配置，单位毫秒</returns>
        public static double GetAudioChannelDelayConfig()
        {
            return Handler.GetAudioChannelDelayConfig();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get time shift configuration for all raw data channels
        /// </summary>
        /// <returns>Dictionary. The key is channel ID and the value is time shift (in milliseconds)</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取所有原始数据通道延迟配置
        /// </summary>
        /// <returns>所有原始数据通道的延迟配置，单位毫秒</returns>
        public static Dictionary<string, double> GetAllRawChannelDelayConfigs()
        {
            return Handler.GetAllRawChannelDelayConfigs();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set time shift for general raw data channel
        /// </summary>
        /// <param name="id">General raw data's channel ID</param>
        /// <param name="delay">Time shift, in milliseconds</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置原始数据通道延迟配置
        /// </summary>
        /// <param name="id">原始数据通道ID</param>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static void SetRawChannelDelayConfig(String id, double delay)
        {
            Handler.SetRawChannelDelayConfig(id, delay);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set time shift for bus raw data channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <param name="delay">Time shift, in milliseconds</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置总线数据通道延迟配置
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static void SetBusChannelDelayConfig(int channel, double delay)
        {
            Handler.SetBusChannelDelayConfig(channel, delay);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set time shift for video raw data channel
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="delay">Time shift, in milliseconds</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置视频数据通道延迟配置
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static void SetVideoChannelDelayConfig(int channel, double delay)
        {
            Handler.SetVideoChannelDelayConfig(channel, delay);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set time shift for audio data channel
        /// </summary>
        /// <param name="delay">Time shift, in milliseconds</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置音频数据通道延迟配置
        /// </summary>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static void SetAudioChannelDelayConfig(double delay)
        {
            Handler.SetAudioChannelDelayConfig(delay);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get whether to monitor that there's data in the specified channel
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0, etc.</param>
        /// <returns>Whether to monitor</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取是否监控指定通道有无数据
        /// </summary>
        /// <param name="id">监控ID，如：bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0等</param>
        /// <returns>是否监控有无数据</returns>
        public static bool GetChannelMonitoringFlag(String id)
        {
            return Handler.GetChannelMonitoringFlag(id);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Set whether to monitor that there's data in the specified channel
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0, etc.</param>
        /// <param name="monitoring">Whether to monitor (The function should be implemented by plugins, like audio alarm, UI flashing, etc.)</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 设置是否监控指定通道有无数据
        /// </summary>
        /// <param name="id">监控ID，如：bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0等</param>
        /// <param name="monitoring">是否监控有无数据，通道监控的具体实现应由插件给出，如发出报警音、指示灯闪烁等</param>
        public static void SetChannelMonitoringFlag(String id, bool monitoring)
        {
            Handler.SetChannelMonitoringFlag(id, monitoring);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get monitor IDs of all the channels being monitored that there's data in the channel
        /// </summary>
        /// <returns>Monitor IDs of all the channels being monitored</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取所有正在监控有无数据的通道ID
        /// </summary>
        /// <returns>正在监控有无数据的通道ID列表</returns>
        public static string[] GetAllChannelMonitoringKeys()
        {
            return Handler.GetAllChannelMonitoringKeys();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.12.1) Get whether to monitor that the specified channel's data is synchronized with time server
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, sample@xxx-v2@0, etc.</param>
        /// <returns>Whether to monitor</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.12.1) 获取是否监控指定通道数据与授时服务器同步
        /// </summary>
        /// <param name="id">监控ID，如bus@1, video@0, sample@xxx-v2@0等</param>
        /// <returns>是否监控指定通道数据与授时服务器同步</returns>
        public static bool GetChannelServerSyncMonitoringFlag(String id)
        {
            return Handler.GetChannelServerSyncMonitoringFlag(id);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.12.1) Set whether to monitor that the specified channel's data is synchronized with time server
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, sample@xxx-v2@0, etc.</param>
        /// <param name="monitoring">Whether to monitor (The function should be implemented by plugins, like audio alarm, UI flashing, etc.)</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.12.1) 设置是否监控指定通道数据与授时服务器同步
        /// </summary>
        /// <param name="id">监控ID，如bus@1, video@0, sample@xxx-v2@0等</param>
        /// <param name="monitoring">是否监控数据与授时服务器同步，通道监控的具体实现应由插件给出，如发出报警音、指示灯闪烁等</param>
        public static void SetChannelServerSyncMonitoringFlag(String id, bool monitoring)
        {
            Handler.SetChannelServerSyncMonitoringFlag(id, monitoring);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.12.1) Get monitor IDs of all the channels being monitored that the channel's data is synchronized with time server
        /// </summary>
        /// <returns>Monitor IDs of all the channels being monitored</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.12.1) 获取所有正在监控数据与授时服务器同步的监控ID
        /// </summary>
        /// <returns>正在监控数据与授时服务器同步的通道ID列表</returns>
        public static String[] GetAllChannelServerSyncMonitoringKeys()
        {
            return Handler.GetAllChannelServerSyncMonitoringKeys();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.6) Get whether the channel is configured as guest synchronized
        /// </summary>
        /// <param name="id">Guest synchronization ID, like bus.1, video.0, xxx.yyy(xxx is native plugin's type ID，yyy is channel name)</param>
        /// <returns>Whether the channel is configured as guest synchronized</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.6) 获取指定通道是否已配置为客机同步
        /// </summary>
        /// <param name="id">客机同步ID，如bus.1, video.0, xxx.yyy(xxx为原生插件类型ID，yyy为通道名称)等</param>
        /// <returns>是否已配置为客机同步</returns>
        public static bool GetChannelGuestSyncFlag(String id)
        {
            return Handler.GetChannelGuestSyncFlag(id);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.6) Set whether the channel is configured as guest synchronized
        /// </summary>
        /// <param name="id">Guest synchronization ID, like bus.1, video.0, xxx.yyy(xxx is native plugin's type ID，yyy is channel name)</param>
        /// <param name="guestSync">Whether the channel is configured as guest synchronized</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.6) 设置指定通道是否配置为客机同步
        /// </summary>
        /// <param name="id">客机同步ID，如bus.1, video.0, xxx.yyy(xxx为原生插件类型ID，yyy为通道名称)等</param>
        /// <param name="guestSync">是否配置为客机同步</param>
        public static void SetChannelGuestSyncFlag(String id, bool guestSync)
        {
            Handler.SetChannelGuestSyncFlag(id, guestSync);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.6) Get guest synchronization IDs of all channels being configured as guest synchronized
        /// </summary>
        /// <returns>Guest synchronization IDs of all channels being configured as guest synchronized</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.6) 获取已配置为客机同步的所有ID
        /// </summary>
        /// <returns>已配置为客机同步的所有ID列表</returns>
        public static string[] GetAllChannelGuestSyncKeys()
        {
            return Handler.GetAllChannelGuestSyncKeys();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get whether there's data in a bus channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <returns>Whether there's data</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取总线数据通道状态
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <returns>是否有数据</returns>
        public static bool GetBusChannelStatus(int channel/* 1~16 */, uint? toleranceMillisecond)
        {
            return Handler.GetBusChannelStatus(channel, toleranceMillisecond);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get whether there's data in a video channel, and the interval and delay information
        /// </summary>
        /// <param name="channel">Video channel, ranges 0~23</param>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <param name="interval">Output the interval (time between frames) curve, in seconds</param>
        /// <param name="delay">Output the delay curve, in seconds</param>
        /// <returns>Whether there's data</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取视频数据通道状态
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <param name="interval">输出每帧数据之间的时间间隔曲线，单位秒</param>
        /// <param name="delay">输出每帧数据的延迟曲线，单位秒</param>
        /// <returns>是否有数据</returns>
        public static bool GetVideoChannelStatus(int channel, uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            return Handler.GetVideoChannelStatus(channel, toleranceMillisecond, out interval, out delay);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get whether there's data in audio channel, and the interval and delay information
        /// </summary>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <param name="interval">Output the interval (time between frames) curve, in seconds</param>
        /// <param name="delay">Output the delay curve, in seconds</param>
        /// <returns>Whether there's data</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取音频数据通道状态
        /// </summary>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <param name="interval">输出每帧数据之间的时间间隔曲线，单位秒</param>
        /// <param name="delay">输出每帧数据的延迟曲线，单位秒</param>
        /// <returns>是否有数据</returns>
        public static bool GetAudioChannelStatus(uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            return Handler.GetAudioChannelStatus(toleranceMillisecond, out interval, out delay);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get whether there's data in general raw data channel
        /// </summary>
        /// <param name="channelID">General raw data's channel ID</param>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <returns>Whether there's data</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取原始数据通道状态
        /// </summary>
        /// <param name="channelID">原始数据通道ID</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <returns>是否有数据</returns>
        public static bool GetRawChannelStatus(String channelID, uint? toleranceMillisecond)
        {
            return Handler.GetRawChannelStatus(channelID, toleranceMillisecond);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get whether there's data in sample channel, and the interval and delay information
        /// </summary>
        /// <param name="channelID">Sample's channel ID</param>
        /// <param name="toleranceMillisecond">How many milliseconds (realistic time) can be tolerated without data</param>
        /// <param name="interval">Output the interval (time between frames) curve, in seconds</param>
        /// <param name="delay">Output the delay curve, in seconds</param>
        /// <returns>Whether there's data</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取样本数据通道状态
        /// </summary>
        /// <param name="channelID">样本数据通道ID</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <param name="interval">输出每帧数据之间的时间间隔曲线，单位秒</param>
        /// <param name="delay">输出每帧数据的延迟曲线，单位秒</param>
        /// <returns>是否有数据</returns>
        public static bool GetSampleChannelStatus(String channelID, uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            return Handler.GetSampleChannelStatus(channelID, toleranceMillisecond, out interval, out delay);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all bus channels with data
        /// </summary>
        /// <returns>Bus channels with data, value ranges 1~16</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取所有有效的总线通道
        /// </summary>
        /// <returns>有效的总线通道列表，值为1~16</returns>
        public static int[] GetAvailableBusChannels()
        {
            return Handler.GetAvailableBusChannels();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all video channels with data
        /// </summary>
        /// <returns>Video channels with data, value ranges 0~23</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取所有有效的视频通道
        /// </summary>
        /// <returns>有效的视频通道列表，值为0~23</returns>
        public static int[] GetAvailableVideoChannels()
        {
            return Handler.GetAvailableVideoChannels();   
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all general raw data channels with data
        /// </summary>
        /// <returns>Channels IDs of all general raw data channels with data</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取所有有效的原始数据通道
        /// </summary>
        /// <returns>有效的原始数据通道列表，值为通道ID</returns>
        public static String[] GetAvailableRawChannels()
        {
            return Handler.GetAvailableRawChannels();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all sample channels with data
        /// </summary>
        /// <returns>Channel IDs of all sample channels with data</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取所有有效的样本数据通道
        /// </summary>
        /// <returns>有效的样本数据通道列表，值为通道ID</returns>
        public static String[] GetAvailableSampleChannels()
        {
            return Handler.GetAvailableSampleChannels();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get payload of bus channel
        /// </summary>
        /// <param name="channel">Bus channel, ranges 1~16</param>
        /// <returns>Payload in percentages, null if unavailable</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取指定总线通道的负载百分比
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <returns>总线通道的负载百分比，若无效则返回null</returns>
        public static double? GetBusPayloadPercentage(int channel)
        {
            return Handler.GetBusPayloadPercentage(channel);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Create window panel
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="windowClassID">Window class ID</param>
        /// <param name="transformID">Transform ID, set to null if not to transform</param>
        /// <param name="panel">The created window panel, null if failed to create</param>
        /// <param name="info">Information of the created window panel, null if failed to create</param>
        /// <returns>Result of creating (After you're done using the panel, call ASEva.Agency.UnregisterPanel )</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 创建窗口面板对象
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <param name="transformID">分化ID，null表示不分化</param>
        /// <param name="panel">新建的窗口面板对象，创建失败则为null</param>
        /// <param name="info">新建窗口面板的组件信息，创建失败则为null</param>
        /// <returns>创建结果，若成功则在释放窗口时需要调用 ASEva.Agency.UnregisterPanel </returns>
        public static CreatePanelResult CreateWindowPanel(object caller, String windowClassID, String transformID, out object panel, out WindowClassInfo info)
        {
            return Handler.CreateWindowPanel(caller, windowClassID, transformID, out panel, out info);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Create config panel
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="dialogClassID">Dialog class ID</param>
        /// <param name="transformID">Transform ID, set to null if not to transform</param>
        /// <param name="panel">The created config panel, null if failed to create</param>
        /// <param name="info">Information of the created config panel, null if failed to create</param>
        /// <returns>Result of creating (After you're done using the panel, call ASEva.Agency.UnregisterPanel )</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 创建配置面板对象
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="transformID">分化ID，null表示不分化</param>
        /// <param name="panel">新建的配置面板对象，创建失败则为null</param>
        /// <param name="info">新建配置面板的组件信息，创建失败则为null</param>
        /// <returns>创建结果，若成功则在释放窗口时需要调用 ASEva.Agency.UnregisterPanel </returns>
        public static CreatePanelResult CreateConfigPanel(object caller, String dialogClassID, String transformID, out object panel, out DialogClassInfo info)
        {
            return Handler.CreateConfigPanel(caller, dialogClassID, transformID, out panel, out info);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Unregister window panel or config panel
        /// </summary>
        /// <param name="panel">Window panel or config panel object</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 注销窗口面板或配置面板对象
        /// </summary>
        /// <param name="panel">窗口面板或配置面板对象</param>
        public static void UnregisterPanel(object panel)
        {
            Handler.UnregisterPanel(panel);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Convert platform image to common image
        /// </summary>
        /// <param name="image">Platform image</param>
        /// <returns>Common image, null if failed to convert</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 转换平台特化图像对象至通用图像数据
        /// </summary>
        /// <param name="image">平台特化图像</param>
        /// <returns>通用图像数据(BGR不逆序)，转换失败则返回null</returns>
        public static CommonImage ConvertImageToCommon(object image)
        {
            return Handler.ConvertImageToCommon(image);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Convert common image to platform image
        /// </summary>
        /// <param name="image">Common image</param>
        /// <param name="eto">Whether to convert to Eto bitmap image, or else platform image</param>
        /// <returns>Platform image, null if failed to convert</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 转换通用图像数据至平台特化图像
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <param name="eto">是否转换至Eto图像，否则转换为当前UI框架对应的图像对象</param>
        /// <returns>平台特化图像，转换失败则返回null</returns>
        public static object ConvertImageToPlatform(CommonImage image, bool eto)
        {
            return Handler.ConvertImageToPlatform(image, eto);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get information of window class
        /// </summary>
        /// <param name="windowClassID">Window class ID</param>
        /// <returns>Information of window class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取窗口组件信息
        /// </summary>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <returns>窗口组件信息，若未找到返回null</returns>
        public static WindowClassInfo GetWindowClassInfo(String windowClassID)
        {
            return Handler.GetWindowClassInfo(windowClassID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.5.0) Get information of transformed window class
        /// </summary>
        /// <param name="windowClassID">Window class ID</param>
        /// <param name="transformID">Transform ID</param>
        /// <returns>Information of transformed window class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.5.0) 获取分化窗口组件信息
        /// </summary>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <param name="transformID">分化ID</param>
        /// <returns>分化窗口组件信息，若未找到返回null</returns>
        public static WindowClassInfo GetWindowClassInfo(String windowClassID, String transformID)
        {
            return Handler.GetWindowClassInfo(windowClassID, transformID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get information of dialog class
        /// </summary>
        /// <param name="dialogClassID">Dialog class ID</param>
        /// <returns>Information of dialog class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取对话框组件信息
        /// </summary>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <returns>对话框组件信息，若未找到返回null</returns>
        public static DialogClassInfo GetDialogClassInfo(String dialogClassID)
        {
            return Handler.GetDialogClassInfo(dialogClassID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.5.0) Get information of transformed dialog class
        /// </summary>
        /// <param name="dialogClassID">Dialog class ID</param>
        /// <param name="transformID">Transform ID</param>
        /// <returns>Information of transformed dialog class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.5.0) 获取分化对话框组件信息
        /// </summary>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="transformID">分化ID</param>
        /// <returns>分化对话框组件信息，若未找到返回null</returns>
        public static DialogClassInfo GetDialogClassInfo(String dialogClassID, String transformID)
        {
            return Handler.GetDialogClassInfo(dialogClassID, transformID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.2) Get information of standalone task class
        /// </summary>
        /// <param name="taskClassID">Task class ID</param>
        /// <returns>Information of standalone task class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.2) 获取独立任务组件信息
        /// </summary>
        /// <param name="taskClassID">独立任务组件ID</param>
        /// <returns>独立任务组件信息，若未找到返回null</returns>
        public static TaskClassInfo GetTaskClassInfo(String taskClassID)
        {
            return Handler.GetTaskClassInfo(taskClassID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.15.0) Get information of console class
        /// </summary>
        /// <param name="consoleClassID">Console class ID</param>
        /// <returns>Information of console class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.0) 获取控制台组件信息
        /// </summary>
        /// <param name="consoleClassID">控制台组件ID</param>
        /// <returns>控制台组件信息，若未找到返回null</returns>
        public static ConsoleClassInfo GetConsoleClassInfo(String consoleClassID)
        {
            return Handler.GetConsoleClassInfo(consoleClassID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Decode image
        /// </summary>
        /// <param name="imageData">JPG or PNG binary data</param>
        /// <returns>Decoded common image, null if failed to decode</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 解码图像数据
        /// </summary>
        /// <param name="imageData">JPG或PNG二进制数据</param>
        /// <returns>解码后的通用图像数据(BGR不逆序)，若失败则返回null</returns>
        public static CommonImage DecodeImage(byte[] imageData)
        {
            return Handler.DecodeImage(imageData);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Encode image
        /// </summary>
        /// <param name="image">Common image</param>
        /// <param name="format">Target format, only "jpg" and "png" supported</param>
        /// <returns>Encoded data, null, if failed to encode</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 编码图像数据
        /// </summary>
        /// <param name="image">通用图像数据</param>
        /// <param name="format">编码格式，目前支持"jpg", "png"</param>
        /// <returns>编码后的二进制数据，若失败则返回null</returns>
        public static byte[] EncodeImage(CommonImage image, String format)
        {
            return Handler.EncodeImage(image, format);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get signal tree
        /// </summary>
        /// <returns>All child nodes under the root of tree</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取信号树
        /// </summary>
        /// <returns>信号树根节点下的所有子节点</returns>
        public static SignalTreeNode[] GetSignalTree()
        {
            return Handler.GetSignalTree();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all plugin pack IDs
        /// </summary>
        /// <returns>All plugin pack IDs</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取插件包ID列表
        /// </summary>
        /// <returns>插件包ID列表</returns>
        public static String[] GetPluginPackIDList()
        {
            return Handler.GetPluginPackIDList();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get information of plugin pack
        /// </summary>
        /// <param name="packID">Plugin pack ID</param>
        /// <returns>Information of plugin pack, null if the plugin pack is not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取插件包信息
        /// </summary>
        /// <param name="packID">插件包ID</param>
        /// <returns>插件包信息，若无对应插件包则返回null</returns>
        public static PluginPackInfo GetPluginPackInfo(String packID)
        {
            return Handler.GetPluginPackInfo(packID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Install plugin (After installation, restart is still needed to activated it)
        /// </summary>
        /// <param name="dirPath">The directory containing plugin files</param>
        /// <returns>Whether any installation is performed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 安装插件（安装完毕后也需要重启才生效）
        /// </summary>
        /// <param name="dirPath">插件包文件夹，或包含若干插件包的文件夹</param>
        /// <returns>是否安装了插件</returns>
        public static bool InstallPlugin(String dirPath)
        {
            return Handler.InstallPlugin(dirPath);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Uninstall plugin
        /// </summary>
        /// <param name="packID">Plugin pack ID</param>
        /// <returns>Whether uninstalled</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 卸载插件
        /// </summary>
        /// <param name="packID">插件包ID</param>
        /// <returns>是否卸载了插件</returns>
        public static bool UninstallPlugin(String packID)
        {
            return Handler.UninstallPlugin(packID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.0) Get all log messages
        /// </summary>
        /// <returns>All log messages</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.0) 获取所有清单信息
        /// </summary>
        /// <returns>清单信息列表</returns>
        public static LogMessage[] GetLogMessages()
        {
            return Handler.GetLogMessages();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.1) Get whether the system is ready for saving project file, starting session, etc.
        /// </summary>
        /// <returns>Whether the system is ready for saving project file, starting session, etc.</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.1) 返回是否允许进行保存工程项目和开始session等操作
        /// </summary>
        /// <returns>是否允许进行保存工程项目和开始session等操作</returns>
        public static bool IsReady()
        {
            return Handler.IsReady();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.13.1) Get whether the system is ready for saving project file, starting session, and output the reason why it's not ready
        /// </summary>
        /// <param name="busyReason">The ready why the system is not ready, empty means unknown</param>
        /// <returns>Whether the system is ready for saving project file, starting session, etc.</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.13.1) 返回是否允许进行保存工程项目和开始session等操作，若不允许则输出繁忙原因
        /// </summary>
        /// <param name="busyReason">系统繁忙原因，空表示原因未知</param>
        /// <returns>是否允许进行保存工程项目和开始session等操作</returns>
        public static bool IsReady(out String busyReason)
        {
            return Handler.IsReady(out busyReason);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.4) Disable plugin
        /// </summary>
        /// <param name="packID">Plugin pack ID</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.4) 禁用插件
        /// </summary>
        /// <param name="packID">插件包ID</param>
        public static void DisablePlugin(String packID)
        {
            Handler.DisablePlugin(packID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.18) Disable all plugins (except for the main workflow plugin)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.18) 禁用所有插件（除当前主流程插件外）
        /// </summary>
        public static void DisableAllPlugins()
        {
            Handler.DisableAllPlugins();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.3.4) Enable plugin (restart is still needed to activate it)
        /// </summary>
        /// <param name="packID">Plugin pack ID</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.3.4) 启用插件（需要重启应用程序后生效）
        /// </summary>
        /// <param name="packID">插件包ID</param>
        public static void EnablePlugin(String packID)
        {
            Handler.EnablePlugin(packID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.0) Send data to native plugins
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="dataID">Data ID, should not be null</param>
        /// <param name="data">Binary data, should not be null</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.0) 发送数据至原生插件
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="nativeClassID">原生组件ID</param>
        /// <param name="dataID">数据ID，不可为null</param>
        /// <param name="data">数据，不可为null</param>
        public static void EnqueueDataToNative(object caller, String nativeClassID, String dataID, byte[] data)
        {
            Handler.EnqueueDataToNative(caller, nativeClassID, dataID, data);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.0) Receive data from native plugins
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="dataID">Data ID, should not be null</param>
        /// <returns>All received binary data</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.0) 接收所有从原生插件发来的新数据
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="nativeClassID">原生组件ID</param>
        /// <param name="dataID">数据ID，不可为null</param>
        /// <returns>所有新数据</returns>
        public static byte[][] DequeueDataFromNative(object caller, String nativeClassID, String dataID)
        {
            return Handler.DequeueDataFromNative(caller, nativeClassID, dataID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.0) Call native plugin's function
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="funcID">Function ID</param>
        /// <param name="input">Input data for the function</param>
        /// <returns>Output data from the function, null if the function is not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.0) 调用原生插件中的函数
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="nativeClassID">原生组件ID</param>
        /// <param name="funcID">函数ID</param>
        /// <param name="input">函数输入数据</param>
        /// <returns>函数输出数据，若未找到相应插件或函数ID无响应则返回null</returns>
        public static byte[] CallNativeFunction(object caller, String nativeClassID, String funcID, byte[] input)
        {
            return Handler.CallNativeFunction(caller, nativeClassID, funcID, input);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.0) Set handler for function calling from native plugins
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="funcID">Function ID</param>
        /// <param name="handler">Handler for function calling</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.0) 设置供原生插件调用的应用层函数
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="nativeClassID">原生组件ID</param>
        /// <param name="funcID">函数ID</param>
        /// <param name="handler">函数接口</param>
        public static void SetAppFunctionHandler(object caller, String nativeClassID, String funcID, AppFunctionHandler handler)
        {
            Handler.SetAppFunctionHandler(caller, nativeClassID, funcID, handler);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.0) Reset the handler for function calling from native plugins
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="funcID">Function ID</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.0) 移除供原生插件调用的应用层函数
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="nativeClassID">原生组件ID</param>
        /// <param name="funcID">函数ID</param>
        public static void ResetAppFunctionHandler(object caller, String nativeClassID, String funcID)
        {
            Handler.ResetAppFunctionHandler(caller, nativeClassID, funcID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.8) Get whether GPU rendering is disabled
        /// </summary>
        /// <returns>Whether GPU rendering is disabled</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.8) 获取是否全局禁用GPU渲染
        /// </summary>
        /// <returns>是否全局禁用GPU渲染</returns>
        public static bool IsGPURenderingDisabled()
        {
            return Handler.IsGPURenderingDisabled();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.13) Get whether GPU onscreen rendering is enabled
        /// </summary>
        /// <returns>Whether GPU onscreen rendering is enabled (always return false while GPU rendering is disabled)</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.13) 获取是否全局启用在屏GPU渲染
        /// </summary>
        /// <returns>是否全局启用在屏GPU渲染（若已全局禁用GPU渲染则返回false）</returns>
        public static bool IsOnscreenGPURenderingEnabled()
        {
            return Handler.IsOnscreenGPURenderingEnabled();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.13.1) Reset GPU decoder test results
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.13.1) 重置GPU解码测试结果
        /// </summary>
        public static void ResetGPUDecoderTestResults()
        {
            Handler.ResetGPUDecoderTestResults();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.19) Subscribe data from other app layer components
        /// </summary>
        /// <param name="dataID">Data ID, should not be null or empty</param>
        /// <param name="bufferLength">Buffer length, ranges 1~1000</param>
        /// <param name="timeout">The subscription will be closed if no dequeue for a along time, ranges 10～600 in seconds</param>
        /// <returns>Data subscriber object, null if failed to initialize</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.19) 订阅来自应用层其他组件的数据
        /// </summary>
        /// <param name="dataID">数据ID，不可为null或空字符串</param>
        /// <param name="bufferLength">缓存长度，范围在1~1000</param>
        /// <param name="timeout">超过该时间不取出缓存数据则自动关闭订阅，单位秒，范围在10～600</param>
        /// <returns>数据订阅对象，若初始化失败则返回null</returns>
        public static DataSubscriber SubscribeData(String dataID, int bufferLength, int timeout)
        {
            return Handler.SubscribeData(dataID, bufferLength, timeout);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.19) Public data
        /// </summary>
        /// <param name="dataID">Data ID, should not be null or empty</param>
        /// <param name="data">Binary data, should not be null</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.19) 发布数据
        /// </summary>
        /// <param name="dataID">数据ID，不可为null或空字符串</param>
        /// <param name="data">数据，不可为null</param>
        public static void PublishData(String dataID, byte[] data)
        {
            Handler.PublishData(dataID, data);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Get session's CPU time model
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>CPU time model</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 获取session的CPU时间模型
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>CPU时间模型</returns>
        public static CPUTimeModel GetCPUTimeModel(DateTime session)
        {
            return Handler.GetCPUTimeModel(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Get session's host machine posix time model
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Host machine posix time model</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 获取session的主机Posix时间模型
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>主机Posix时间模型</returns>
        public static PosixTimeModel GetHostPosixTimeModel(DateTime session)
        {
            return Handler.GetHostPosixTimeModel(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Get session's satellite posix time model
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Satellite posix time model</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 获取session的卫星Posix时间模型
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>卫星Posix时间模型</returns>
        public static PosixTimeModel GetGNSSPosixTimeModel(DateTime session)
        {
            return Handler.GetGNSSPosixTimeModel(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Convert time offset in session to local date and time
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">Time offset, in seconds</param>
        /// <param name="useGNSS">Whether to use satellite posix time model, or else use host machine posix time model</param>
        /// <returns>Local date and time</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 计算session中某个时间偏置对应的本地时间
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">时间偏置，单位秒</param>
        /// <param name="useGNSS">是否使用卫星Posix时间模型计算，否则使用主机Posix时间模型</param>
        /// <returns>对应的本地时间</returns>
        public static DateTime? GetLocalDateTime(DateTime session, double timeOffset, bool useGNSS)
        {
            return Handler.GetLocalDateTime(session, timeOffset, useGNSS);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Convert time offset in session to UTC date and time
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">Time offset, in seconds</param>
        /// <param name="useGNSS">Whether to use satellite posix time model, or else use host machine posix time model</param>
        /// <returns>UTC date and time</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 计算session中某个时间偏置对应的UTC时间
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">时间偏置，单位秒</param>
        /// <param name="useGNSS">是否使用卫星Posix时间模型计算，否则使用主机Posix时间模型</param>
        /// <returns>对应的UTC时间</returns>
        public static DateTime? GetUTCDateTime(DateTime session, double timeOffset, bool useGNSS)
        {
            return Handler.GetUTCDateTime(session, timeOffset, useGNSS);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Get current CPU tick on host machine
        /// </summary>
        /// <returns>Current CPU tick on host machine</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 获取主机当前的CPU计数
        /// </summary>
        /// <returns>当前的CPU计数</returns>
        public static ulong GetCPUTick()
        {
            return Handler.GetCPUTick();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Get CPU ticks per second on host machine
        /// </summary>
        /// <returns>CPU ticks per second on host machine</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 获取主机上每秒增加的CPU计数
        /// </summary>
        /// <returns>每秒增加的CPU计数</returns>
        public static ulong GetCPUTicksPerSecond()
        {
            return Handler.GetCPUTicksPerSecond();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.4) Get the latest several timestamps of the specified channel
        /// </summary>
        /// <param name="channelID">Data channel ID, with format as "protocol@channel". Channel is starting from 0. The video channel's protocol is "video". The audio channel's ID is "audio"</param>
        /// <returns>the latest several timestamps, null if the channel is not found</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.4) 获取数据通道上最近的若干帧时间戳
        /// </summary>
        /// <param name="channelID">数据通道关键字，格式为"协议名@通道序号"，通道序号从0开始。视频协议名为video，音频协议名为audio</param>
        /// <returns>指定数据通道上最近的若干帧时间戳，若该通道未找到或最近无数据则返回null</returns>
        public static Timestamp[] GetChannelLatestTimestamps(String channelID)
        {
            return Handler.GetChannelLatestTimestamps(channelID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.8.1) Register graph panel type for the specified graph type
        /// </summary>
        /// <param name="graphType">Graph type</param>
        /// <param name="styleName">Style name to register</param>
        /// <param name="panelType">Graph panel type, which should be derived from UI framework's control base class, and implement ASEva.GraphPanel </param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.8.1) 注册针对指定图表类型的可视化面板
        /// </summary>
        /// <param name="graphType">指定图表类型</param>
        /// <param name="styleName">面板样式名</param>
        /// <param name="panelType">面板类型，需要继承UI框架的控件基类，并实现 ASEva.GraphPanel </param>
        public static void RegisterGraphPanel(GraphType graphType, String styleName, Type panelType)
        {
            Handler.RegisterGraphPanel(graphType, styleName, panelType);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.8.1) Register graph panel type for the specified graph ID (higher priority than graph type)
        /// </summary>
        /// <param name="graphID">Graph ID</param>
        /// <param name="styleName">Style name to register</param>
        /// <param name="panelType">Graph panel type, which should be derived from UI framework's control base class, and implement ASEva.GraphPanel </param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.8.1) 注册针对指定图表ID的可视化面板（比按图表类型注册的优先级更高）
        /// </summary>
        /// <param name="graphID">图表报告ID</param>
        /// <param name="styleName">面板样式名</param>
        /// <param name="panelType">面板类型，需要继承UI框架的控件基类，并实现 ASEva.GraphPanel </param>
        public static void RegisterGraphPanel(int graphID, String styleName, Type panelType)
        {
            Handler.RegisterGraphPanel(graphID, styleName, panelType);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.8.2) Get all available style names for the specified graph ID
        /// </summary>
        /// <param name="graphID">Graph ID</param>
        /// <returns>All available style names</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.8.2) 获取符合图表报告的所有可视化面板样式名
        /// </summary>
        /// <param name="graphID">图表报告ID</param>
        /// <returns>可视化面板样式名列表</returns>
        public static String[] GetGraphPanelStyles(int graphID)
        {
            return Handler.GetGraphPanelStyles(graphID);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.8.2) Create graph panel
        /// </summary>
        /// <param name="graphID">Graph ID</param>
        /// <param name="styleName">Style name, set to null to use the first style</param>
        /// <returns>Created graph panel, null if failed to create</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.8.2) 创建图表可视化面板
        /// </summary>
        /// <param name="graphID">图表报告ID</param>
        /// <param name="styleName">可视化面板样式名，若输入空则使用首个注册样式</param>
        /// <returns>可视化面板对象，若创建失败则返回null</returns>
        public static GraphPanel CreateGraphPanel(int graphID, String styleName)
        {
            return Handler.CreateGraphPanel(graphID, styleName);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.10.3) Get all available style names for the specified graph type
        /// </summary>
        /// <param name="graphType">Graph type</param>
        /// <returns>All available style names</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.10.3) 获取符合图表报告的所有可视化面板样式名
        /// </summary>
        /// <param name="graphType">图表类型</param>
        /// <returns>可视化面板样式名列表</returns>
        public static String[] GetGraphPanelStyles(GraphType graphType)
        {
            return Handler.GetGraphPanelStyles(graphType);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.10.3) Create graph panel
        /// </summary>
        /// <param name="graphType">Graph type</param>
        /// <param name="styleName">Style name, set to null to use the first style</param>
        /// <returns>Created graph panel, null if failed to create</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.10.3) 创建图表可视化面板
        /// </summary>
        /// <param name="graphType">图表类型</param>
        /// <param name="styleName">可视化面板样式名，若输入空则使用首个注册样式</param>
        /// <returns>可视化面板对象，若创建失败则返回null</returns>
        public static GraphPanel CreateGraphPanel(GraphType graphType, String styleName)
        {
            return Handler.CreateGraphPanel(graphType, styleName);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.11.4) Get the UTC date and time queried from Internet NTP server
        /// </summary>
        /// <returns>The UTC date and time queried from Internet NTP server, null if Internet is not connected or querying failed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.11.4) 获取从互联网获取的当前时间
        /// </summary>
        /// <returns>从互联网获取的当前时间(UTC时间)，若未联网或获取失败则返回空</returns>
        public static DateTime? GetInternetNTPTime()
        {
            return Handler.GetInternetNTPTime();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.12.0) Get all guest synchronization IDs and title of each ID
        /// </summary>
        /// <returns>Dictionary. The key is guest synchronization ID and the value is the title</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.12.0) 获取所有插件的客机同步ID以及对应的标题
        /// </summary>
        /// <returns>客机同步标题表，键为客机同步ID，值为对应的标题</returns>
        public static Dictionary<String, String> GetPluginGuestSyncTable()
        {
            return Handler.GetPluginGuestSyncTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.13.2) Get information of all dedicated graphic card
        /// </summary>
        /// <returns>Information of all dedicated graphic card</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.13.2) 获取主机上所有独立显卡信息
        /// </summary>
        /// <returns>独立显卡信息列表</returns>
        public static GraphicCardInfo[] GetGraphicCardInfos()
        {
            return Handler.GetGraphicCardInfos();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.14.0) Get third part license notices of software used by framework
        /// </summary>
        /// <returns>Dictionary. The key is title</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.14.0) 获取框架软件使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为标题，值为版权声明</returns>
        public static Dictionary<String, String> GetFrameworkThirdPartyNotices()
        {
            return Handler.GetFrameworkThirdPartyNotices();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.14.0) Get all third party license notices of software used by plugins
        /// </summary>
        /// <returns>Dictionary. The key is plugin pack ID and the value is the information corresponding to the plugin (which is a dictionary with key as third party software's title)</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.14.0) 获取所有插件使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为插件ID，值为该插件使用的第三方软件版权声明(其中键为标题，值为版权声明)</returns>
        public static Dictionary<String, Dictionary<String, String> > GetPluginThirdPartyNotices()
        {
            return Handler.GetPluginThirdPartyNotices();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.14.0) Get copyright information of offline map
        /// </summary>
        /// <returns>Copyright information of offline map</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.14.0) 获取离线地图的版权信息
        /// </summary>
        /// <returns>离线地图的版权信息</returns>
        public static String GetOfflineMapCopyrightInfo()
        {
            return Handler.GetOfflineMapCopyrightInfo();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.14.0) Get whether to use PRC web service
        /// </summary>
        /// <returns>Whether to use PRC web service</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.14.0) 获取是否使用境内网络服务
        /// </summary>
        /// <returns>是否使用境内网络服务</returns>
        public static bool IsPRCWebPreferred()
        {
            return Handler.IsPRCWebPreferred();
        }

        /// \~English
        /// <summary>
        /// (api:app=2.15.5) Gets information about all bus channels of a specified session
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Information about all bus channels of this session. Return null if none exist</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.5) 获取指定session的所有总线通道的信息
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>该session的所有总线通道的信息，若不存在则返回null</returns>
        public static BusChannelInfo[] GetBusChannelsInfo(DateTime session)
        {
            return Handler.GetBusChannelsInfo(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.15.5) Gets information about all video channels of a specified session
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Information about all video channels of this session. Return null if none exist</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.5) 获取指定session的所有视频通道的信息
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>该session的所有视频通道的信息，若不存在则返回null</returns>
        public static VideoChannelInfo[] GetVideoChannelsInfo(DateTime session)
        {
            return Handler.GetVideoChannelsInfo(session);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.16.0) Gets details about the processor/native/device component
        /// </summary>
        /// <returns>Component details. Return null if none exist</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.16.0) 获取数据处理/原生/设备组件的详情
        /// </summary>
        /// <returns>组件详情，若不存在则返回null</returns>
        public static ModuleDetails GetModuleDetails(String classID)
        {
            return Handler.GetModuleDetails(classID);
        }
    }
}
