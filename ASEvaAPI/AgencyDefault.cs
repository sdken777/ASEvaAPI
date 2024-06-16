using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASEva.Samples;
using ASEva.Utility;

namespace ASEva
{
    class AgencyLocalDefault : AgencyLocalHandler
    {
        public AddBusProtocolResult AddBusProtocolFile(string filePath, out BusProtocolFileID[] fileIDs)
        {
        }

        public void AddMainThreadCheckpoint(string location)
        {
        }

        public void AddProcessorVideoReference(int videoChannel)
        {
        }

        public void AddSceneData(SceneData scene)
        {
        }

        public void AddWindow(object caller, string windowClassID, string config, bool newWorkspaceIfNeeded)
        {
        }

        public byte[] CallNativeFunction(object caller, string nativeClassID, string funcID, byte[] input)
        {
        }

        public void CallWebApi(string request, WebApiContext context)
        {
        }

        public void CallWebApiPost(string request, byte[] body, WebPostContentType contentType, WebApiContext context)
        {
        }

        public void ConfigDataEncryption()
        {
        }

        public void ConfigOfflineMapPath()
        {
        }

        public FloatPoint ConvertOfflineMapLocToPix(LocPoint origin, int zoom, LocPoint point)
        {
        }

        public LocPoint ConvertOfflineMapPixToLoc(LocPoint origin, int zoom, FloatPoint pixel)
        {
        }

        public CreatePanelResult CreateConfigPanel(object caller, string dialogClassID, string transformID, out object panel, out DialogClassInfo info)
        {
        }

        public GraphPanel CreateGraphPanelForID(int graphID, string styleName)
        {
        }

        public GraphPanel CreateGraphPanelForType(GraphType graphType, string styleName)
        {
        }

        public CreatePanelResult CreateWindowPanel(object caller, string windowClassID, string transformID, out object panel, out WindowClassInfo info)
        {
        }

        public CommonImage DecodeImage(byte[] imageData)
        {
        }

        public bool DeleteToRecycleBin(string path)
        {
        }

        public void DisableAllPlugins()
        {
        }

        public void DisablePlugin(string packID)
        {
        }

        public void EnablePlugin(string packID)
        {
        }

        public byte[] EncodeImage(CommonImage image, string format)
        {
        }

        public string[] GetAllChannelMonitoringKeys()
        {
        }

        public string[] GetAllChannelServerSyncMonitoringKeys()
        {
        }

        public string GetAppFilesRoot()
        {
        }

        public ApplicationGUI GetAppGUI()
        {
        }

        public string GetAppID()
        {
        }

        public Language GetAppLanguage()
        {
        }

        public string GetBusProtocolFilePath(BusProtocolFileID fileID)
        {
        }

        public BusFileInfo[] GetBusProtocolFilesInfo()
        {
        }

        public bool GetChannelMonitoringFlag(string id)
        {
        }

        public bool GetChannelServerSyncMonitoringFlag(string id)
        {
        }

        public string GetConfigFilesRoot()
        {
        }

        public string GetCurrentDataLayerPath()
        {
        }

        public string GetCurrentProject()
        {
        }

        public string GetDataPath()
        {
        }

        public DialogClassInfo GetDialogClassInfo(string dialogClassID, string transformID)
        {
        }

        public Dictionary<string, DialogClassInfo> GetDialogClassTable()
        {
        }

        public Dictionary<string, string> GetFrameworkThirdPartyNotices()
        {
        }

        public string GetGenerationPath(DateTime session, string generation)
        {
        }

        public string GetGlobalPath(string key)
        {
        }

        public string[] GetGlobalPathKeys()
        {
        }

        public string GetGlobalPublicDataPath()
        {
        }

        public string GetGlobalVariable(string key, string defaultValue)
        {
        }

        public string[] GetGlobalVariableKeys()
        {
        }

        public string[] GetGraphPanelStylesForID(int graphID)
        {
        }

        public string[] GetGraphPanelStylesForType(GraphType graphType)
        {
        }

        public DateTime? GetInternetNTPTime()
        {
        }

        public LogMessage[] GetLogMessages()
        {
        }

        public CommonImage GetOfflineMapCommonImage(IntSize imageSize, LocPoint centerLocation, int zoom)
        {
        }

        public string GetOfflineMapCopyrightInfo()
        {
        }

        public string[] GetPluginPackIDList()
        {
        }

        public PluginPackInfo GetPluginPackInfo(string packID)
        {
        }

        public Dictionary<string, Dictionary<string, string>> GetPluginThirdPartyNotices()
        {
        }

        public string[] GetRecentProjectPaths()
        {
        }

        public string GetSessionPath(DateTime session)
        {
        }

        public string GetSessionPublicDataPath(DateTime session)
        {
        }

        public string[] GetSubDataPaths()
        {
        }

        public string GetTempFilesRoot()
        {
        }

        public Dictionary<string, Version> GetVersionTable()
        {
        }

        public WindowClassInfo GetWindowClassInfo(string windowClassID, string transformID)
        {
        }

        public Dictionary<string, WindowClassInfo> GetWindowClassTable()
        {
        }

        public bool InstallPlugin(string dirPath)
        {
        }

        public bool IsInternetConnected()
        {
        }

        public void Log(string text, LogLevel level)
        {
        }

        public bool NewProject(bool force)
        {
        }

        public void OpenDialog(object caller, string dialogClassID, string config)
        {
        }

        public bool OpenProject(string projectFile, bool force)
        {
        }

        public void PlayMp3(byte[] mp3FileData)
        {
        }

        public bool PopupConfirm(string msg)
        {
        }

        public void PopupError(string msg)
        {
        }

        public void PopupNotice(string msg)
        {
        }

        public void Print(string text)
        {
        }

        public void PublishData(string dataID, byte[] data)
        {
        }

        public void RegisterAudioDriver(AudioDriverInfo driver, AudioRecorder recorder, AudioReplayer replayer)
        {
        }

        public void RegisterGraphPanelForID(int graphID, string styleName, Type panelType)
        {
        }

        public void RegisterGraphPanelForType(GraphType graphType, string styleName, Type panelType)
        {
        }

        public DialogClassInfo RegisterTransformDialogClass(string dialogClassID, string config)
        {
        }

        public DialogClassInfo RegisterTransformDialogClassDirectly(string dialogClassID, DialogClass transformDialogClass, string defaultConfig)
        {
        }

        public WindowClassInfo RegisterTransformWindowClass(string windowClassID, string config)
        {
        }

        public WindowClassInfo RegisterTransformWindowClassDirectly(string windowClassID, WindowClass transformWindowClass, string defaultConfig)
        {
        }

        public void RemoveBusProtocolFile(BusProtocolFileID fileID)
        {
        }

        public void RemoveProcessorVideoReference(int videoChannel)
        {
        }

        public void ResetAppFunctionHandler(object caller, string nativeClassID, string funcID)
        {
        }

        public bool SaveCurrentProject(string projectFile)
        {
        }

        public void SendRawDataWithCPUTick(ulong cpuTick, string channelID, double[] values, byte[] binary)
        {
        }

        public void SetAppFunctionHandler(object caller, string nativeClassID, string funcID, AppFunctionHandler handler)
        {
        }

        public void SetAudioVolume(double volume)
        {
        }

        public void SetChannelMonitoringFlag(string id, bool monitoring)
        {
        }

        public void SetChannelServerSyncMonitoringFlag(string id, bool monitoring)
        {
        }

        public void SetCurrentDialogTitle(string title, object icon)
        {
        }

        public void SetDataPath(string path)
        {
        }

        public void SetGlobalPath(string key, string path)
        {
        }

        public void SetGlobalVariable(string key, string value)
        {
        }

        public void SetSubDataPath(int subIndex, string path)
        {
        }

        public void SetWindowTitle(object window, string title, object icon)
        {
        }

        public bool StartProcess(string target)
        {
        }

        public DataSubscriber SubscribeData(string dataID, int bufferLength, int timeout)
        {
        }

        public bool TerminateApp(bool force, bool autosave)
        {
        }

        public bool UninstallPlugin(string packID)
        {
        }

        public void UnregisterPanel(object panel)
        {
        }

        public bool UpdateBusProtocolFilePath(BusProtocolFileID fileID, string filePath)
        {
        }
    }

    class AgencyAsyncDefault : AgencyAsyncHandler
    {
        public bool SyncMode => false;

        public Task AddDataLayer(string layer)
        {
        }

        public Task AddSignalReference(string signalID)
        {
        }

        public Task<TimeWithSession> ConvertTimeIntoSession(double timeline)
        {
        }

        public Task DeleteDataLayer(string layer)
        {
        }

        public Task<byte[][]> DequeueDataFromNative(object caller, string nativeClassID, string dataID)
        {
        }

        public Task DisableAllConfigs()
        {
        }

        public Task DisableAllPlugins()
        {
        }

        public Task DisableModule(object caller, string classID)
        {
        }

        public Task DisablePlugin(string packID)
        {
        }

        public Task EnablePlugin(string packID)
        {
        }

        public Task EnqueueDataToNative(object caller, string nativeClassID, string dataID, byte[] data)
        {
        }

        public Task<string[]> GetAllChannelGuestSyncKeys()
        {
        }

        public Task<Dictionary<string, DeviceStatusDetail>> GetAllDeviceStatus()
        {
        }

        public Task<Dictionary<string, double>> GetAllRawChannelDelayConfigs()
        {
        }

        public Task<ApplicationGUI> GetAppGUI()
        {
        }

        public Task<string> GetAppID()
        {
        }

        public Task<Language> GetAppLanguage()
        {
        }

        public Task<ApplicationMode> GetAppMode()
        {
        }

        public Task<ApplicationStatus> GetAppStatus()
        {
        }

        public Task<double> GetAudioChannelDelayConfig()
        {
        }

        public Task<Tuple<bool, double[], double[]>> GetAudioChannelStatus(uint? toleranceMillisecond)
        {
        }

        public Task<AudioDriverInfo[]> GetAudioDrivers()
        {
        }

        public Task<AudioDeviceInfo[]> GetAudioRecordDevices(string driverID)
        {
        }

        public Task<AudioDeviceInfo[]> GetAudioReplayDevices(string driverID)
        {
        }

        public Task<int[]> GetAvailableBusChannels()
        {
        }

        public Task<string[]> GetAvailableRawChannels()
        {
        }

        public Task<string[]> GetAvailableSampleChannels()
        {
        }

        public Task<int[]> GetAvailableVideoChannels()
        {
        }

        public Task<BufferRange> GetBufferRange()
        {
        }

        public Task<double> GetBusChannelDelayConfig(int channel)
        {
        }

        public Task<BusChannelInfo[]> GetBusChannelsInfo(DateTime session)
        {
        }

        public Task<bool> GetBusChannelStatus(int channel, uint? toleranceMillisecond)
        {
        }

        public Task<Dictionary<BusDeviceID, BusDeviceInfo>> GetBusDevices()
        {
        }

        public Task<float> GetBusMessageFPS(int channel, uint localID)
        {
        }

        public Task<BusMessageInfo> GetBusMessageInfo(string busMessageID)
        {
        }

        public Task<BusMessageInfo> GetBusMessageInfoByLocalID(int channel, uint localID)
        {
        }

        public Task<double?> GetBusPayloadPercentage(int channel)
        {
        }

        public Task<int?> GetBusProtocolFileChannel(string protocolName)
        {
        }

        public Task<BusProtocolFileID[]> GetBusProtocolFileIDList()
        {
        }

        public Task<BusProtocolFileState> GetBusProtocolFileState(BusProtocolFileID fileID)
        {
        }

        public Task<BusSignalInfo> GetBusSignalInfo(string busSignalID)
        {
        }

        public Task<string> GetChannelAliasName(string channelID)
        {
        }

        public Task<Dictionary<string, string>> GetChannelAliasTable()
        {
        }

        public Task<bool> GetChannelGuestSyncFlag(string id)
        {
        }

        public Task<Timestamp[]> GetChannelLatestTimestamps(string channelID)
        {
        }

        public Task<Dictionary<string, bool>> GetChannelStatusTable(uint? tolerance)
        {
        }

        public Task<Dictionary<string, TimeOffsetSync>> GetChannelSyncTable()
        {
        }

        public Task<GeneralDeviceStatus[]> GetChildDeviceStatus(string id)
        {
        }

        public Task<ConsoleClassInfo> GetConsoleClassInfo(string consoleClassID)
        {
        }

        public Task<Dictionary<string, ConsoleClassInfo>> GetConsoleClassTable()
        {
        }

        public Task<Tuple<ConfigStatus, ConfigStatus[]>> GetConsoleRelatedModulesConfigStatus(string consoleClassID)
        {
        }

        public Task<ulong> GetCPUTick()
        {
        }

        public Task<ulong> GetCPUTicksPerSecond()
        {
        }

        public Task<double> GetCPUTime()
        {
        }

        public Task<CPUTimeModel> GetCPUTimeModel(DateTime session)
        {
        }

        public Task<string> GetCurrentDataGeneration()
        {
        }

        public Task<string> GetCurrentDataLayer()
        {
        }

        public Task<DateTime?> GetCurrentOnlineSession()
        {
        }

        public Task<string> GetCurrentSessionGUID()
        {
        }

        public Task<string[]> GetDataLayers()
        {
        }

        public Task<Dictionary<string, DeviceClassInfo>> GetDeviceClassTable()
        {
        }

        public Task<GeneralDeviceStatus> GetDeviceStatus(string id)
        {
        }

        public Task<Tuple<ConfigStatus, ConfigStatus[]>> GetDialogRelatedModulesConfigStatus(string dialogClassID, string transformID)
        {
        }

        public Task<object[]> GetEventHandles()
        {
        }

        public Task<EventInfo> GetEventInfo(object eventHandle)
        {
        }

        public Task<string[]> GetEventTypeNames()
        {
        }

        public Task<DateTime[]> GetFilteredSessionList()
        {
        }

        public Task<double> GetFilteredSessionListTotalLength()
        {
        }

        public Task<DateTime[]> GetFinishedSessions(string generation)
        {
        }

        public Task<Dictionary<string, string>> GetFrameworkThirdPartyNotices()
        {
        }

        public Task<string[]> GetGenerationList()
        {
        }

        public Task<GenerationProcessStatus?> GetGenerationProcessStatus(DateTime session, string generation)
        {
        }

        public Task<DateTime[]> GetGenerationSessions(string generationID)
        {
        }

        public Task<string> GetGlobalParameter(string key, string defaultValue)
        {
        }

        public Task<string[]> GetGlobalParameterKeys()
        {
        }

        public Task<string> GetGlobalVariable(string key, string defaultValue)
        {
        }

        public Task<string[]> GetGlobalVariableKeys()
        {
        }

        public Task<PosixTimeModel> GetGNSSPosixTimeModel(DateTime session)
        {
        }

        public Task<GraphData> GetGraphData(DateTime session, int graphID)
        {
        }

        public Task<GraphicCardInfo[]> GetGraphicCardInfos()
        {
        }

        public Task<int[]> GetGraphIDList()
        {
        }

        public Task<int?> GetGraphIDWithTitle(string title)
        {
        }

        public Task<string> GetGraphTitle(int graphID)
        {
        }

        public Task<PosixTimeModel> GetHostPosixTimeModel(DateTime session)
        {
        }

        public Task<double> GetInterestTarget()
        {
        }

        public Task<double> GetInterestTime()
        {
        }

        public Task<DateTime?> GetInterestTimestamp()
        {
        }

        public Task<DateTime?> GetInternetNTPTime()
        {
        }

        public Task<int[]> GetLicensedFunctionIndices()
        {
        }

        public Task<string> GetLicenseInfo()
        {
        }

        public Task<DateTime?> GetLocalDateTime(DateTime session, double timeOffset, bool useGNSS)
        {
        }

        public Task<LogMessage[]> GetLogMessages()
        {
        }

        public Task<string> GetManualTriggerName(int index)
        {
        }

        public Task<string[]> GetManualTriggerNames()
        {
        }

        public Task<ulong> GetMemoryCapacity()
        {
        }

        public Task<ConfigStatus[]> GetModuleChildConfigStatus(object caller, string classID)
        {
        }

        public Task<string> GetModuleConfig(object caller, string classID)
        {
        }

        public Task<Tuple<ConfigStatus, string>> GetModuleConfigStatus(object caller, string classID)
        {
        }

        public Task<ModuleDetails> GetModuleDetails(string classID)
        {
        }

        public Task<Dictionary<string, NativeClassInfo>> GetNativeClassTable()
        {
        }

        public Task<Dictionary<string, Version>> GetNativePluginVersions(NativeLibraryType type)
        {
        }

        public Task<Dictionary<string, string>> GetPluginGuestSyncTable()
        {
        }

        public Task<string[]> GetPluginPackIDList()
        {
        }

        public Task<PluginPackInfo> GetPluginPackInfo(string packID)
        {
        }

        public Task<Dictionary<string, Dictionary<string, string>>> GetPluginThirdPartyNotices()
        {
        }

        public Task<Tuple<byte[], Timestamp?, CameraInfo>> GetPreviewJpeg(int channel, double timeline, double maxGap)
        {
        }

        public Task<Dictionary<string, ProcessorClassInfo>> GetProcessorClassTable()
        {
        }

        public Task<double> GetRawChannelDelayConfig(string id)
        {
        }

        public Task<bool> GetRawChannelStatus(string channelID, uint? toleranceMillisecond)
        {
        }

        public Task<Tuple<bool, double[], double[]>> GetSampleChannelStatus(string channelID, uint? toleranceMillisecond)
        {
        }

        public Task<List<string>> GetSampleTitle(string channelID)
        {
        }

        public Task<string[]> GetSceneIDList()
        {
        }

        public Task<Dictionary<string, SceneTitle>> GetSceneTitleTable()
        {
        }

        public Task<string> GetSessionComment(DateTime session)
        {
        }

        public Task<Dictionary<DateTime, SessionFilterFlags>> GetSessionFilterTable()
        {
        }

        public Task<string> GetSessionFolderName(DateTime session)
        {
        }

        public Task<string[]> GetSessionGenerations(DateTime sessionID)
        {
        }

        public Task<bool> GetSessionHostSync(DateTime session)
        {
        }

        public Task<string> GetSessionLayer(DateTime session)
        {
        }

        public Task<double?> GetSessionLength(DateTime session)
        {
        }

        public Task<DateTime[]> GetSessionList()
        {
        }

        public Task<double> GetSessionListTotalLength()
        {
        }

        public Task<Dictionary<string, string>> GetSessionProperties(DateTime session)
        {
        }

        public Task<string> GetSessionSearchKey()
        {
        }

        public Task<double?> GetSessionTimeline(DateTime session)
        {
        }

        public Task<string> GetSignalName(string signalID, bool fullName)
        {
        }

        public Task<string[]> GetSignalNamesOfBusMessage(string messageID)
        {
        }

        public Task<SignalTreeNode[]> GetSignalTree()
        {
        }

        public Task<string> GetSystemStatus(SystemStatus status)
        {
        }

        public Task<string> GetSystemStatusDetails(SystemStatus status)
        {
        }

        public Task<TaskClassInfo> GetTaskClassInfo(string taskClassID)
        {
        }

        public Task<Dictionary<string, TaskClassInfo>> GetTaskClassTable()
        {
        }

        public Task<DateTime?> GetUTCDateTime(DateTime session, double timeOffset, bool useGNSS)
        {
        }

        public Task<Dictionary<string, Version>> GetVersionTable()
        {
        }

        public Task<double> GetVideoChannelDelayConfig(int channel)
        {
        }

        public Task<VideoChannelInfo[]> GetVideoChannelsInfo(DateTime session)
        {
        }

        public Task<Tuple<bool, double[], double[]>> GetVideoChannelStatus(int channel, uint? toleranceMillisecond)
        {
        }

        public Task<Dictionary<VideoDeviceID, VideoDeviceInfo>> GetVideoDevices()
        {
        }

        public Task<Tuple<CommonImage, Timestamp?, CameraInfo>> GetVideoFrameImage(int channel, double timeline, double maxGap, VideoFrameGetMode mode, IntRect? clip, bool withAlpha)
        {
        }

        public Task<CommonImage> GetVideoFrameThumbnail(int channel, double timeline, double maxGap, bool withAlpha)
        {
        }

        public Task<IntSize?> GetVideoRawSize(int channel, double timeline)
        {
        }

        public Task<SpecialCameraType> GetVideoSpecialType(int channel)
        {
        }

        public Task<bool> IsBusMessageBound(string busMessageID)
        {
        }

        public Task<bool> IsFileOutputEnabled()
        {
        }

        public Task<bool> IsInputChannelAvailable(string channelID)
        {
        }

        public Task<bool> IsInternetConnected()
        {
        }

        public Task<bool> IsMessageValid(string messageID, bool optional)
        {
        }

        public Task<bool> IsPRCWebPreferred()
        {
        }

        public Task<Tuple<bool, string>> IsReady()
        {
        }

        public Task<bool> IsSampleChannelConflict(string channelID)
        {
        }

        public Task<bool> IsSignalValid(string signalID, bool optional)
        {
        }

        public Task<bool> IsVideoDataAvailable(int channel, uint? tolerance)
        {
        }

        public Task<BusSignalValue[]> ParseBusMessage(BusMessageSample busMessage)
        {
        }

        public Task PublishData(string dataID, byte[] data)
        {
        }

        public Task RefreshGenerations()
        {
        }

        public Task RefreshSessions()
        {
        }

        public Task RemoveEvent(object eventHandle)
        {
        }

        public Task RemoveGeneration(DateTime session, string genID)
        {
        }

        public Task<bool> RemoveSession(DateTime session, bool force)
        {
        }

        public Task RemoveSignalReference(string signalID)
        {
        }

        public Task ResetGPUDecoderTestResults()
        {
        }

        public Task RunConsole(object caller, string consoleClassID)
        {
        }

        public Task<Tuple<TaskResult, string>> RunStandaloneTask(object caller, string taskClassID, string config)
        {
        }

        public Task<string> SelectBusMessage(string originMessageID)
        {
        }

        public Task SelectBusMessages(SelectBusMessageHandler handler, List<string> existBusMessageIDList)
        {
        }

        public Task<BusProtocolFileID[]> SelectBusProtocolFiles(BusProtocolFileID[] selected)
        {
        }

        public Task<SignalConfig> SelectSignal(SignalConfig origin, bool withScale, bool withSignBit, string unit)
        {
        }

        public Task SelectSignals(SelectSignalHandler handler, List<string> existSignalIDList)
        {
        }

        public Task SendBusMessage(BusMessage message)
        {
        }

        public Task<byte[]> SendBusMessageBound(string messageID, uint? interval)
        {
        }

        public Task SendManualTrigger(int channel)
        {
        }

        public Task SendRawData(string channelID, double[] values, byte[] binary)
        {
        }

        public Task SetAudioChannelDelayConfig(double delay)
        {
        }

        public Task SetBusChannelDelayConfig(int channel, double delay)
        {
        }

        public Task SetChannelGuestSyncFlag(string id, bool guestSync)
        {
        }

        public Task<bool> SetControlFlag(string controllerName, bool enabled)
        {
        }

        public Task SetCurrentDataLayer(string layer)
        {
        }

        public Task SetEventComment(object eventHandle, string comment)
        {
        }

        public Task SetGlobalParameter(string key, string value)
        {
        }

        public Task SetGlobalVariable(string key, string value)
        {
        }

        public Task SetInterestTime(double targetTimeline)
        {
        }

        public Task SetInterestTimestamp(DateTime targetTimestamp)
        {
        }

        public Task SetManualTriggerName(int index, string name)
        {
        }

        public Task SetModuleConfig(object caller, string classID, string config)
        {
        }

        public Task SetRawChannelDelayConfig(string id, double delay)
        {
        }

        public Task SetSessionChecker(DateTime session, bool check)
        {
        }

        public Task SetSessionComment(DateTime session, string comment)
        {
        }

        public Task SetSessionHostSync(DateTime session, bool hostSync)
        {
        }

        public Task SetSessionProperties(DateTime session, Dictionary<string, string> properties)
        {
        }

        public Task SetSessionSearchKeyword(string keyword)
        {
        }

        public Task SetTargetReplaySpeed(double speed)
        {
        }

        public Task SetVideoChannelDelayConfig(int channel, double delay)
        {
        }

        public Task<bool> StartOffline(bool force, bool previewOnly, string genDirName)
        {
        }

        public Task<bool> StartOnline(bool force, bool previewOnly, string sessionDirName)
        {
        }

        public Task<bool> StartOnlineWithController(string controllerName, bool previewOnly)
        {
        }

        public Task<bool> StartRemote(bool force, bool previewOnly, string sessionDirName, ulong startPosixTime)
        {
        }

        public Task<bool> StartRemoteWithController(string controllerName, bool previewOnly, ulong startPosixTime)
        {
        }

        public Task<bool> StartReplay(bool force, double startTimeline, double? interestTarget)
        {
        }

        public Task<bool> StopRunning(bool force, bool editRecordedSession)
        {
        }

        public Task<bool> StopRunningWithController(string controllerName)
        {
        }

        public Task<DataSubscriber> SubscribeData(string dataID, int bufferLength, int timeout)
        {
        }

        public Task<bool> SwitchAppMode(string controllerName, ApplicationMode mode, int waitSecond)
        {
        }

        public Task<bool> TerminateApp(bool force, bool autosave)
        {
        }

        public Task<bool> UninstallPlugin(string packID)
        {
        }
    }
}
