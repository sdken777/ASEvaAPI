using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASEva.Samples;
using ASEva.Utility;

namespace ASEva
{
    class AgencyLocalDefault : AgencyLocalHandler
    {
        public bool ClientSide => false;

        public AddBusProtocolResult AddBusProtocolFile(string filePath, out BusProtocolFileID[] fileIDs)
        {
            fileIDs = null;
            return AddBusProtocolResult.Invalid;
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
            return null;
        }

        public void CallWebApi(string request, WebApiContext context)
        {
        }

        public void CallWebApiPost(string request, byte[] body, WebPostContentType contentType, WebApiContext context)
        {
        }

        public Task ConfigDataEncryption()
        {
            return Task.CompletedTask;
        }

        public Task ConfigOfflineMapPath()
        {
            return Task.CompletedTask;
        }

        public FloatPoint ConvertOfflineMapLocToPix(LocPoint origin, int zoom, LocPoint point)
        {
            return new FloatPoint();
        }

        public LocPoint ConvertOfflineMapPixToLoc(LocPoint origin, int zoom, FloatPoint pixel)
        {
            return new LocPoint();
        }

        public CreatePanelResult CreateConfigPanel(object caller, string dialogClassID, string transformID, out object panel, out DialogClassInfo info)
        {
            panel = null;
            info = null;
            return CreatePanelResult.CreateFailed;
        }

        public GraphPanel CreateGraphPanelForID(int graphID, string styleName)
        {
            return null;
        }

        public GraphPanel CreateGraphPanelForType(GraphType graphType, string styleName)
        {
            return null;
        }

        public CreatePanelResult CreateWindowPanel(object caller, string windowClassID, string transformID, out object panel, out WindowClassInfo info)
        {
            panel = null;
            info = null;
            return CreatePanelResult.CreateFailed;
        }

        public CommonImage DecodeImage(byte[] imageData)
        {
            return null;
        }

        public bool DeleteToRecycleBin(string path)
        {
            return false;
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
            return null;
        }

        public string GetAppFilesRoot()
        {
            return null;
        }

        public ApplicationGUI GetAppGUI()
        {
            return ApplicationGUI.NoGUI;
        }

        public string GetAppID()
        {
            return null;
        }

        public Language GetAppLanguage()
        {
            return Language.Invalid;
        }

        public string GetBusProtocolFilePath(BusProtocolFileID fileID)
        {
            return null;
        }

        public BusFileInfo[] GetBusProtocolFilesInfo()
        {
            return null;
        }

        public string GetConfigFilesRoot()
        {
            return null;
        }

        public string GetCurrentDataLayerPath()
        {
            return null;
        }

        public string GetCurrentProject()
        {
            return null;
        }

        public string GetDataPath()
        {
            return null;
        }

        public DialogClassInfo GetDialogClassInfo(string dialogClassID, string transformID)
        {
            return null;
        }

        public Dictionary<string, DialogClassInfo> GetDialogClassTable()
        {
            return null;
        }

        public Dictionary<string, string> GetFrameworkThirdPartyNotices()
        {
            return null;
        }

        public string GetGenerationPath(SessionIdentifier session, string generation)
        {
            return null;
        }

        public string GetGlobalPath(string key)
        {
            return null;
        }

        public string[] GetGlobalPathKeys()
        {
            return null;
        }

        public string GetGlobalPublicDataPath()
        {
            return null;
        }

        public string GetGlobalVariable(string key, string defaultValue)
        {
            return null;
        }

        public string[] GetGlobalVariableKeys()
        {
            return null;
        }

        public string[] GetGraphPanelStylesForID(int graphID)
        {
            return null;
        }

        public string[] GetGraphPanelStylesForType(GraphType graphType)
        {
            return null;
        }

        public DateTime? GetInternetNTPTime()
        {
            return null;
        }

        public LogMessage[] GetLogMessages()
        {
            return null;
        }

        public CommonImage GetOfflineMapCommonImage(IntSize imageSize, LocPoint centerLocation, int zoom)
        {
            return null;
        }

        public string GetOfflineMapCopyrightInfo()
        {
            return null;
        }

        public string[] GetPluginPackIDList()
        {
            return null;
        }

        public PluginPackInfo GetPluginPackInfo(string packID)
        {
            return null;
        }

        public Dictionary<string, Dictionary<string, string>> GetPluginThirdPartyNotices()
        {
            return null;
        }

        public string[] GetRecentProjectPaths()
        {
            return null;
        }

        public string GetSessionPath(SessionIdentifier session)
        {
            return null;
        }

        public string GetSessionPublicDataPath(SessionIdentifier session)
        {
            return null;
        }

        public string[] GetSubDataPaths()
        {
            return null;
        }

        public string GetTempFilesRoot()
        {
            return null;
        }

        public Dictionary<string, Version> GetVersionTable()
        {
            return null;
        }

        public WindowClassInfo GetWindowClassInfo(string windowClassID, string transformID)
        {
            return null;
        }

        public Dictionary<string, WindowClassInfo> GetWindowClassTable()
        {
            return null;
        }

        public Task<bool> InstallPlugin(string dirPath)
        {
            return Task.FromResult(false);
        }

        public bool IsInternetConnected()
        {
            return false;
        }

        public bool IsPRCWebPreferred()
        {
            return false;
        }

        public void Log(string text, LogLevel level)
        {
        }

        public Task<bool> NewProject(bool force)
        {
            return Task.FromResult(false);
        }

        public Task OpenDialog(object caller, string dialogClassID, string config)
        {
            return Task.CompletedTask;
        }

        public Task<bool> OpenProject(string projectFile, bool force)
        {
            return Task.FromResult(false);
        }

        public void PlayMp3(byte[] mp3FileData)
        {
        }

        public Task<bool> PopupConfirm(string msg)
        {
            return Task.FromResult(false);
        }

        public Task PopupError(string msg)
        {
            return Task.CompletedTask;
        }

        public Task PopupNotice(string msg)
        {
            return Task.CompletedTask;
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
            return null;
        }

        public DialogClassInfo RegisterTransformDialogClassDirectly(string dialogClassID, DialogClass transformDialogClass, string defaultConfig)
        {
            return null;
        }

        public WindowClassInfo RegisterTransformWindowClass(string windowClassID, string config)
        {
            return null;
        }

        public WindowClassInfo RegisterTransformWindowClassDirectly(string windowClassID, WindowClass transformWindowClass, string defaultConfig)
        {
            return null;
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
            return false;
        }

        public Task<string> SelectBusMessage(string originMessageID)
        {
            return Task.FromResult<string>(null);
        }

        public Task SelectBusMessages(SelectBusMessageHandler handler, List<string> existBusMessageIDList)
        {
            return Task.CompletedTask;
        }

        public Task<BusProtocolFileID[]> SelectBusProtocolFiles(BusProtocolFileID[] selected)
        {
            return Task.FromResult<BusProtocolFileID[]>(null);
        }

        public Task<SignalConfig> SelectSignal(SignalConfig origin, bool withScale, bool withSignBit, string unit)
        {
            return Task.FromResult<SignalConfig>(null);
        }

        public Task SelectSignals(SelectSignalHandler handler, List<string> existSignalIDList)
        {
            return Task.CompletedTask;
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
            return false;
        }

        public DataSubscriber SubscribeData(string dataID, int bufferLength, int timeout)
        {
            return null;
        }

        public Task<bool> TerminateApp(bool force, bool autosave)
        {
            return Task.FromResult(false);
        }

        public bool UninstallPlugin(string packID)
        {
            return false;
        }

        public void UnregisterPanel(object panel)
        {
        }

        public bool UpdateBusProtocolFilePath(BusProtocolFileID fileID, string filePath)
        {
            return false;
        }
    }

    class AgencyAsyncDefault : AgencyAsyncHandler
    {
        public bool SyncMode => false;

        public Task AddDataLayer(string layer)
        {
            return Task.CompletedTask;
        }

        public Task AddSignalReference(string signalID)
        {
            return Task.CompletedTask;
        }

        public Task<TimeWithSession> ConvertTimeIntoSession(double timeline)
        {
            return Task.FromResult<TimeWithSession>(null);
        }

        public Task DeleteDataLayer(string layer)
        {
            return Task.CompletedTask;
        }

        public Task<byte[][]> DequeueDataFromNative(object caller, string nativeClassID, string dataID)
        {
            return Task.FromResult<byte[][]>(null);
        }

        public Task DisableAllConfigs()
        {
            return Task.CompletedTask;
        }

        public Task DisableAllPlugins()
        {
            return Task.CompletedTask;
        }

        public Task DisableModule(object caller, string classID)
        {
            return Task.CompletedTask;
        }

        public Task DisablePlugin(string packID)
        {
            return Task.CompletedTask;
        }

        public Task EnablePlugin(string packID)
        {
            return Task.CompletedTask;
        }

        public Task EnqueueDataToNative(object caller, string nativeClassID, string dataID, byte[] data)
        {
            return Task.CompletedTask;
        }

        public Task<string[]> GetAllChannelGuestSyncKeys()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<string[]> GetAllChannelMonitoringKeys()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<string[]> GetAllChannelServerSyncMonitoringKeys()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<Dictionary<string, DeviceStatusDetail>> GetAllDeviceStatus()
        {
            return Task.FromResult<Dictionary<string, DeviceStatusDetail>>(null);
        }

        public Task<Dictionary<string, double>> GetAllRawChannelDelayConfigs()
        {
            return Task.FromResult<Dictionary<string, double>>(null);
        }

        public Task<ApplicationGUI> GetAppGUI()
        {
            return Task.FromResult<ApplicationGUI>(ApplicationGUI.NoGUI);
        }

        public Task<string> GetAppID()
        {
            return Task.FromResult<string>(null);
        }

        public Task<Language> GetAppLanguage()
        {
            return Task.FromResult<Language>(Language.Invalid);
        }

        public Task<ApplicationMode> GetAppMode()
        {
            return Task.FromResult<ApplicationMode>(ApplicationMode.Replay);
        }

        public Task<ApplicationStatus> GetAppStatus()
        {
            return Task.FromResult<ApplicationStatus>(ApplicationStatus.Idle);
        }

        public Task<double> GetAudioChannelDelayConfig()
        {
            return Task.FromResult<double>(0);
        }

        public Task<(bool, double[], double[])> GetAudioChannelStatus(uint? toleranceMillisecond)
        {
            return Task.FromResult<(bool, double[], double[])>((false, null, null));
        }

        public Task<AudioDriverInfo[]> GetAudioDrivers()
        {
            return Task.FromResult<AudioDriverInfo[]>(null);
        }

        public Task<AudioDeviceInfo[]> GetAudioRecordDevices(string driverID)
        {
            return Task.FromResult<AudioDeviceInfo[]>(null);
        }

        public Task<AudioDeviceInfo[]> GetAudioReplayDevices(string driverID)
        {
            return Task.FromResult<AudioDeviceInfo[]>(null);
        }

        public Task<int[]> GetAvailableBusChannels()
        {
            return Task.FromResult<int[]>(null);
        }

        public Task<string[]> GetAvailableRawChannels()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<string[]> GetAvailableSampleChannels()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<int[]> GetAvailableVideoChannels()
        {
            return Task.FromResult<int[]>(null);
        }

        public Task<BufferRange> GetBufferRange()
        {
            return Task.FromResult<BufferRange>(new BufferRange());
        }

        public Task<double> GetBusChannelDelayConfig(int channel)
        {
            return Task.FromResult<double>(0);
        }

        public Task<BusChannelInfo[]> GetBusChannelsInfo(SessionIdentifier session)
        {
            return Task.FromResult<BusChannelInfo[]>(null);
        }

        public Task<bool> GetBusChannelStatus(int channel, uint? toleranceMillisecond)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<Dictionary<BusDeviceID, BusDeviceInfo>> GetBusDevices()
        {
            return Task.FromResult<Dictionary<BusDeviceID, BusDeviceInfo>>(null);
        }

        public Task<float> GetBusMessageFPS(int channel, uint localID)
        {
            return Task.FromResult<float>(0);
        }

        public Task<BusMessageInfo> GetBusMessageInfo(string busMessageID)
        {
            return Task.FromResult<BusMessageInfo>(null);
        }

        public Task<BusMessageInfo> GetBusMessageInfoByLocalID(int channel, uint localID)
        {
            return Task.FromResult<BusMessageInfo>(null);
        }

        public Task<double?> GetBusPayloadPercentage(int channel)
        {
            return Task.FromResult<double?>(null);
        }

        public Task<int?> GetBusProtocolFileChannel(string protocolName)
        {
            return Task.FromResult<int?>(null);
        }

        public Task<BusProtocolFileID[]> GetBusProtocolFileIDList()
        {
            return Task.FromResult<BusProtocolFileID[]>(null);
        }

        public Task<BusProtocolFileState> GetBusProtocolFileState(BusProtocolFileID fileID)
        {
            return Task.FromResult<BusProtocolFileState>(BusProtocolFileState.OK);
        }

        public Task<BusSignalInfo> GetBusSignalInfo(string busSignalID)
        {
            return Task.FromResult<BusSignalInfo>(null);
        }

        public Task<string> GetChannelAliasName(string channelID)
        {
            return Task.FromResult<string>(null);
        }

        public Task<Dictionary<string, string>> GetChannelAliasTable()
        {
            return Task.FromResult<Dictionary<string, string>>(null);
        }

        public Task<bool> GetChannelGuestSyncFlag(string id)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<Timestamp[]> GetChannelLatestTimestamps(string channelID)
        {
            return Task.FromResult<Timestamp[]>(null);
        }

        public Task<bool> GetChannelMonitoringFlag(string id)
        {
            return Task.FromResult(false);
        }

        public Task<bool> GetChannelServerSyncMonitoringFlag(string id)
        {
            return Task.FromResult(false);
        }

        public Task<Dictionary<string, bool>> GetChannelStatusTable(uint? tolerance)
        {
            return Task.FromResult<Dictionary<string, bool>>(null);
        }

        public Task<Dictionary<string, TimeOffsetSync>> GetChannelSyncTable()
        {
            return Task.FromResult<Dictionary<string, TimeOffsetSync>>(null);
        }

        public Task<GeneralDeviceStatus[]> GetChildDeviceStatus(string id)
        {
            return Task.FromResult<GeneralDeviceStatus[]>(null);
        }

        public Task<ConsoleClassInfo> GetConsoleClassInfo(string consoleClassID)
        {
            return Task.FromResult<ConsoleClassInfo>(null);
        }

        public Task<Dictionary<string, ConsoleClassInfo>> GetConsoleClassTable()
        {
            return Task.FromResult<Dictionary<string, ConsoleClassInfo>>(null);
        }

        public Task<(ConfigStatus, ConfigStatus[])> GetConsoleRelatedModulesConfigStatus(string consoleClassID)
        {
            return Task.FromResult<(ConfigStatus, ConfigStatus[])>((ConfigStatus.Disabled, null));
        }

        public Task<ulong> GetCPUTick()
        {
            return Task.FromResult<ulong>(0);
        }

        public Task<ulong> GetCPUTicksPerSecond()
        {
            return Task.FromResult<ulong>(0);
        }

        public Task<double> GetCPUTime()
        {
            return Task.FromResult<double>(0);
        }

        public Task<CPUTimeModel> GetCPUTimeModel(SessionIdentifier session)
        {
            return Task.FromResult<CPUTimeModel>(null);
        }

        public Task<string> GetCurrentDataGeneration()
        {
            return Task.FromResult<string>(null);
        }

        public Task<string> GetCurrentDataLayer()
        {
            return Task.FromResult<string>(null);
        }

        public Task<SessionIdentifier?> GetCurrentOnlineSession()
        {
            return Task.FromResult<SessionIdentifier?>(null);
        }

        public Task<string> GetCurrentSessionGUID()
        {
            return Task.FromResult<string>(null);
        }

        public Task<string[]> GetDataLayers()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<Dictionary<string, DeviceClassInfo>> GetDeviceClassTable()
        {
            return Task.FromResult<Dictionary<string, DeviceClassInfo>>(null);
        }

        public Task<GeneralDeviceStatus> GetDeviceStatus(string id)
        {
            return Task.FromResult<GeneralDeviceStatus>(GeneralDeviceStatus.None);
        }

        public Task<(ConfigStatus, ConfigStatus[])> GetDialogRelatedModulesConfigStatus(string dialogClassID, string transformID)
        {
            return Task.FromResult<(ConfigStatus, ConfigStatus[])>((ConfigStatus.Disabled, null));
        }

        public Task<object[]> GetEventHandles()
        {
            return Task.FromResult<object[]>(null);
        }

        public Task<EventInfo> GetEventInfo(object eventHandle)
        {
            return Task.FromResult<EventInfo>(null);
        }

        public Task<string[]> GetEventTypeNames()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<SessionIdentifier[]> GetFilteredSessionList()
        {
            return Task.FromResult<SessionIdentifier[]>(null);
        }

        public Task<double> GetFilteredSessionListTotalLength()
        {
            return Task.FromResult<double>(0);
        }

        public Task<SessionIdentifier[]> GetFinishedSessions(string generation)
        {
            return Task.FromResult<SessionIdentifier[]>(null);
        }

        public Task<Dictionary<string, string>> GetFrameworkThirdPartyNotices()
        {
            return Task.FromResult<Dictionary<string, string>>(null);
        }

        public Task<string[]> GetGenerationList()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<GenerationProcessStatus?> GetGenerationProcessStatus(SessionIdentifier session, string generation)
        {
            return Task.FromResult<GenerationProcessStatus?>(null);
        }

        public Task<SessionIdentifier[]> GetGenerationSessions(string generationID)
        {
            return Task.FromResult<SessionIdentifier[]>(null);
        }

        public Task<string> GetGlobalParameter(string key, string defaultValue)
        {
            return Task.FromResult<string>(null);
        }

        public Task<string[]> GetGlobalParameterKeys()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<string> GetGlobalVariable(string key, string defaultValue)
        {
            return Task.FromResult<string>(null);
        }

        public Task<string[]> GetGlobalVariableKeys()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<PosixTimeModel> GetGNSSPosixTimeModel(SessionIdentifier session)
        {
            return Task.FromResult<PosixTimeModel>(null);
        }

        public Task<GPUDecoderTestResults> GetGPUDecoderTestResults()
        {
            return Task.FromResult((GPUDecoderTestResults)null);
        }

        public Task<GraphData> GetGraphData(SessionIdentifier session, int graphID)
        {
            return Task.FromResult<GraphData>(null);
        }

        public Task<GraphicCardInfo[]> GetGraphicCardInfos()
        {
            return Task.FromResult<GraphicCardInfo[]>(null);
        }

        public Task<int[]> GetGraphIDList()
        {
            return Task.FromResult<int[]>(null);
        }

        public Task<int?> GetGraphIDWithTitle(string title)
        {
            return Task.FromResult<int?>(null);
        }

        public Task<string> GetGraphTitle(int graphID)
        {
            return Task.FromResult<string>(null);
        }

        public Task<PosixTimeModel> GetHostPosixTimeModel(SessionIdentifier session)
        {
            return Task.FromResult<PosixTimeModel>(null);
        }

        public Task<double> GetInterestTarget()
        {
            return Task.FromResult<double>(0);
        }

        public Task<double> GetInterestTime()
        {
            return Task.FromResult<double>(0);
        }

        public Task<DateTime?> GetInterestTimestamp()
        {
            return Task.FromResult<DateTime?>(null);
        }

        public Task<DateTime?> GetInternetNTPTime()
        {
            return Task.FromResult<DateTime?>(null);
        }

        public Task<int[]> GetLicensedFunctionIndices()
        {
            return Task.FromResult<int[]>(null);
        }

        public Task<string> GetLicenseInfo()
        {
            return Task.FromResult<string>(null);
        }

        public Task<DateTime?> GetLocalDateTime(SessionIdentifier session, double timeOffset, bool useGNSS)
        {
            return Task.FromResult<DateTime?>(null);
        }

        public Task<LogMessage[]> GetLogMessages()
        {
            return Task.FromResult<LogMessage[]>(null);
        }

        public Task<string> GetManualTriggerName(int index)
        {
            return Task.FromResult<string>(null);
        }

        public Task<string[]> GetManualTriggerNames()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<ConfigStatus[]> GetModuleChildConfigStatus(object caller, string classID)
        {
            return Task.FromResult<ConfigStatus[]>(null);
        }

        public Task<string> GetModuleConfig(object caller, string classID)
        {
            return Task.FromResult<string>(null);
        }

        public Task<(ConfigStatus, string)> GetModuleConfigStatus(object caller, string classID)
        {
            return Task.FromResult<(ConfigStatus, string)>((ConfigStatus.Disabled, null));
        }

        public Task<ModuleDetails> GetModuleDetails(string classID)
        {
            return Task.FromResult<ModuleDetails>(null);
        }

        public Task<Dictionary<string, NativeClassInfo>> GetNativeClassTable()
        {
            return Task.FromResult<Dictionary<string, NativeClassInfo>>(null);
        }

        public Task<Dictionary<string, Version>> GetNativePluginVersions(NativeLibraryType type)
        {
            return Task.FromResult<Dictionary<string, Version>>(null);
        }

        public Task<Dictionary<string, string>> GetPluginGuestSyncTable()
        {
            return Task.FromResult<Dictionary<string, string>>(null);
        }

        public Task<string[]> GetPluginPackIDList()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<PluginPackInfo> GetPluginPackInfo(string packID)
        {
            return Task.FromResult<PluginPackInfo>(null);
        }

        public Task<Dictionary<string, Dictionary<string, string>>> GetPluginThirdPartyNotices()
        {
            return Task.FromResult<Dictionary<string, Dictionary<string, string>>>(null);
        }

        public Task<(byte[], Timestamp?, CameraInfo)> GetPreviewJpeg(int channel, double timeline, double maxGap)
        {
            return Task.FromResult<(byte[], Timestamp?, CameraInfo)>((null, null, null));
        }

        public Task<Dictionary<string, ProcessorClassInfo>> GetProcessorClassTable()
        {
            return Task.FromResult<Dictionary<string, ProcessorClassInfo>>(null);
        }

        public Task<double> GetRawChannelDelayConfig(string id)
        {
            return Task.FromResult<double>(0);
        }

        public Task<bool> GetRawChannelStatus(string channelID, uint? toleranceMillisecond)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<(bool, double[], double[])> GetSampleChannelStatus(string channelID, uint? toleranceMillisecond)
        {
            return Task.FromResult<(bool, double[], double[])>((false, null, null));
        }

        public Task<List<string>> GetSampleTitle(string channelID)
        {
            return Task.FromResult<List<string>>(null);
        }

        public Task<string[]> GetSceneIDList()
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<Dictionary<string, SceneTitle>> GetSceneTitleTable()
        {
            return Task.FromResult<Dictionary<string, SceneTitle>>(null);
        }

        public Task<string> GetSessionComment(SessionIdentifier session)
        {
            return Task.FromResult<string>(null);
        }

        public Task<Dictionary<SessionIdentifier, SessionFilterFlags>> GetSessionFilterTable()
        {
            return Task.FromResult<Dictionary<SessionIdentifier, SessionFilterFlags>>(null);
        }

        public Task<string> GetSessionFolderName(SessionIdentifier session)
        {
            return Task.FromResult<string>(null);
        }

        public Task<string[]> GetSessionGenerations(SessionIdentifier session)
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<bool> GetSessionHostSync(SessionIdentifier session)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<string> GetSessionLayer(SessionIdentifier session)
        {
            return Task.FromResult<string>(null);
        }

        public Task<double?> GetSessionLength(SessionIdentifier session)
        {
            return Task.FromResult<double?>(null);
        }

        public Task<SessionIdentifier[]> GetSessionList()
        {
            return Task.FromResult<SessionIdentifier[]>(null);
        }

        public Task<double> GetSessionListTotalLength()
        {
            return Task.FromResult<double>(0);
        }

        public Task<Dictionary<string, string>> GetSessionProperties(SessionIdentifier session)
        {
            return Task.FromResult<Dictionary<string, string>>(null);
        }

        public Task<string> GetSessionSearchKey()
        {
            return Task.FromResult<string>(null);
        }

        public Task<double?> GetSessionTimeline(SessionIdentifier session)
        {
            return Task.FromResult<double?>(null);
        }

        public Task<string> GetSignalName(string signalID, bool fullName)
        {
            return Task.FromResult<string>(null);
        }

        public Task<string[]> GetSignalNamesOfBusMessage(string messageID)
        {
            return Task.FromResult<string[]>(null);
        }

        public Task<SignalTreeNode[]> GetSignalTree()
        {
            return Task.FromResult<SignalTreeNode[]>(null);
        }

        public Task<string> GetSystemStatus(SystemStatus status)
        {
            return Task.FromResult<string>(null);
        }

        public Task<string> GetSystemStatusDetails(SystemStatus status)
        {
            return Task.FromResult<string>(null);
        }

        public Task<TaskClassInfo> GetTaskClassInfo(string taskClassID)
        {
            return Task.FromResult<TaskClassInfo>(null);
        }

        public Task<Dictionary<string, TaskClassInfo>> GetTaskClassTable()
        {
            return Task.FromResult<Dictionary<string, TaskClassInfo>>(null);
        }

        public Task<DateTime?> GetUTCDateTime(SessionIdentifier session, double timeOffset, bool useGNSS)
        {
            return Task.FromResult<DateTime?>(null);
        }

        public Task<Dictionary<string, Version>> GetVersionTable()
        {
            return Task.FromResult<Dictionary<string, Version>>(null);
        }

        public Task<double> GetVideoChannelDelayConfig(int channel)
        {
            return Task.FromResult<double>(0);
        }

        public Task<VideoChannelInfo[]> GetVideoChannelsInfo(SessionIdentifier session)
        {
            return Task.FromResult<VideoChannelInfo[]>(null);
        }

        public Task<(bool, double[], double[])> GetVideoChannelStatus(int channel, uint? toleranceMillisecond)
        {
            return Task.FromResult<(bool, double[], double[])>((false, null, null));
        }

        public Task<Dictionary<VideoDeviceID, VideoDeviceInfo>> GetVideoDevices()
        {
            return Task.FromResult<Dictionary<VideoDeviceID, VideoDeviceInfo>>(null);
        }

        public Task<(CommonImage, Timestamp?, CameraInfo)> GetVideoFrameImage(int channel, double timeline, double maxGap, VideoFrameGetMode mode, IntRect? clip, bool withAlpha)
        {
            return Task.FromResult<(CommonImage, Timestamp?, CameraInfo)>((null, null, null));
        }

        public Task<CommonImage> GetVideoFrameThumbnail(int channel, double timeline, double maxGap, bool withAlpha)
        {
            return Task.FromResult<CommonImage>(null);
        }

        public Task<IntSize?> GetVideoRawSize(int channel, double timeline)
        {
            return Task.FromResult<IntSize?>(null);
        }

        public Task<SpecialCameraType> GetVideoSpecialType(int channel)
        {
            return Task.FromResult<SpecialCameraType>(SpecialCameraType.Normal);
        }

        public Task<bool> IsBusMessageBound(string busMessageID)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> IsFileOutputEnabled()
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> IsInputChannelAvailable(string channelID)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> IsInternetConnected()
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> IsMessageValid(string messageID, bool optional)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> IsPRCWebPreferred()
        {
            return Task.FromResult<bool>(false);
        }

        public Task<(bool, string)> IsReady()
        {
            return Task.FromResult<(bool, string)>((false, null));
        }

        public Task<bool> IsSampleChannelConflict(string channelID)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> IsSignalValid(string signalID, bool optional)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> IsVideoDataAvailable(int channel, uint? tolerance)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<BusSignalValue[]> ParseBusMessage(BusMessageSample busMessage)
        {
            return Task.FromResult<BusSignalValue[]>(null);
        }

        public Task PublishData(string dataID, byte[] data)
        {
            return Task.CompletedTask;
        }

        public Task RefreshGenerations()
        {
            return Task.CompletedTask;
        }

        public Task RefreshSessions()
        {
            return Task.CompletedTask;
        }

        public Task RemoveEvent(object eventHandle)
        {
            return Task.CompletedTask;
        }

        public Task RemoveGeneration(SessionIdentifier session, string genID)
        {
            return Task.CompletedTask;
        }

        public Task<bool> RemoveSession(SessionIdentifier session, bool force)
        {
            return Task.FromResult<bool>(false);
        }

        public Task RemoveSignalReference(string signalID)
        {
            return Task.CompletedTask;
        }

        public Task ResetGPUDecoderTestResults()
        {
            return Task.CompletedTask;
        }

        public Task RunConsole(object caller, string consoleClassID)
        {
            return Task.CompletedTask;
        }

        public Task<(TaskResult, string)> RunStandaloneTask(object caller, string taskClassID, string config)
        {
            return Task.FromResult<(TaskResult, string)>((TaskResult.TaskInitFailed, null));
        }

        public Task SendBusMessage(BusMessage message)
        {
            return Task.CompletedTask;
        }

        public Task<byte[]> SendBusMessageBound(string messageID, uint? interval)
        {
            return Task.FromResult<byte[]>(null);
        }

        public Task SendManualTrigger(int channel)
        {
            return Task.CompletedTask;
        }

        public Task SendRawData(string channelID, double[] values, byte[] binary)
        {
            return Task.CompletedTask;
        }

        public Task SetAudioChannelDelayConfig(double delay)
        {
            return Task.CompletedTask;
        }

        public Task SetBusChannelDelayConfig(int channel, double delay)
        {
            return Task.CompletedTask;
        }

        public Task SetChannelGuestSyncFlag(string id, bool guestSync)
        {
            return Task.CompletedTask;
        }

        public Task SetChannelMonitoringFlag(string id, bool monitoring)
        {
            return Task.CompletedTask;
        }

        public Task SetChannelServerSyncMonitoringFlag(string id, bool monitoring)
        {
            return Task.CompletedTask;
        }

        public Task<bool> SetControlFlag(string controllerName, bool enabled)
        {
            return Task.FromResult<bool>(false);
        }

        public Task SetCurrentDataLayer(string layer)
        {
            return Task.CompletedTask;
        }

        public Task SetEventComment(object eventHandle, string comment)
        {
            return Task.CompletedTask;
        }

        public Task SetGlobalParameter(string key, string value)
        {
            return Task.CompletedTask;
        }

        public Task SetGlobalVariable(string key, string value)
        {
            return Task.CompletedTask;
        }

        public Task SetInterestTime(double targetTimeline)
        {
            return Task.CompletedTask;
        }

        public Task SetInterestTimestamp(DateTime targetTimestamp)
        {
            return Task.CompletedTask;
        }

        public Task SetManualTriggerName(int index, string name)
        {
            return Task.CompletedTask;
        }

        public Task SetModuleConfig(object caller, string classID, string config)
        {
            return Task.CompletedTask;
        }

        public Task SetRawChannelDelayConfig(string id, double delay)
        {
            return Task.CompletedTask;
        }

        public Task SetSessionChecker(SessionIdentifier session, bool check)
        {
            return Task.CompletedTask;
        }

        public Task SetSessionComment(SessionIdentifier session, string comment)
        {
            return Task.CompletedTask;
        }

        public Task SetSessionHostSync(SessionIdentifier session, bool hostSync)
        {
            return Task.CompletedTask;
        }

        public Task SetSessionProperties(SessionIdentifier session, Dictionary<string, string> properties)
        {
            return Task.CompletedTask;
        }

        public Task SetSessionSearchKeyword(string keyword)
        {
            return Task.CompletedTask;
        }

        public Task SetTargetReplaySpeed(double speed)
        {
            return Task.CompletedTask;
        }

        public Task SetVideoChannelDelayConfig(int channel, double delay)
        {
            return Task.CompletedTask;
        }

        public Task<bool> StartOffline(bool force, bool previewOnly, string genDirName)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> StartOnline(bool force, bool previewOnly, string sessionDirName)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> StartOnlineWithController(string controllerName, bool previewOnly)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> StartRemote(bool force, bool previewOnly, string sessionDirName, ulong startPosixTime)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> StartRemoteWithController(string controllerName, bool previewOnly, ulong startPosixTime)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> StartReplay(bool force, double startTimeline, double? interestTarget)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> StopRunning(bool force, bool editRecordedSession)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> StopRunningWithController(string controllerName)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<DataSubscriber> SubscribeData(string dataID, int bufferLength, int timeout)
        {
            return Task.FromResult<DataSubscriber>(null);
        }

        public Task<bool> SwitchAppMode(string controllerName, ApplicationMode mode, int waitSecond)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> TerminateApp(bool force, bool autosave)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> UninstallPlugin(string packID)
        {
            return Task.FromResult<bool>(false);
        }
    }
}
