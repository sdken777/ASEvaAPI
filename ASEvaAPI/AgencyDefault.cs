using System;
using System.Collections.Generic;
using ASEva.Samples;
using ASEva.Utility;

namespace ASEva
{
    class AgencyDefault : AgencyHandler
    {
        public string GetConfigFilesRoot()
        {
            return null;
        }

        public string GetAppFilesRoot()
        {
            return null;
        }

        public string GetTempFilesRoot()
        {
            return null;
        }

        public string GetGlobalPath(string key)
        {
            return null;
        }

        public void Print(string text)
        {
        }

        public void Log(string text, LogLevel level)
        {
        }

        public ApplicationStatus GetAppStatus()
        {
            return ApplicationStatus.Idle;
        }

        public ApplicationMode GetAppMode()
        {
            return ApplicationMode.Replay;
        }

        public bool IsFileOutputEnabled()
        {
            return false;
        }

        public string GetAppLanguage()
        {
            return null;
        }

        public GraphData GetGraphData(DateTime session, int id)
        {
            return null;
        }

        public void AddSceneData(SceneData scene)
        {
        }

        public List<string> GetSampleTitle(string protocol)
        {
            return null;
        }

        public DateTime? GetStartTimeLocal(DateTime session)
        {
            return null;
        }

        public DateTime? GetStartTimeUTC(DateTime session)
        {
            return null;
        }

        public DateTime? GetTimestampLocal(DateTime session, double timeOffset)
        {
            return null;
        }

        public DateTime? GetTimestampUTC(DateTime session, double timeOffset)
        {
            return null;
        }

        public double GetTimeRatioToLocal(DateTime session)
        {
            return 0;
        }

        public double GetTimeRatioToUTC(DateTime session)
        {
            return 0;
        }

        public bool DeleteToRecycleBin(string path)
        {
            return false;
        }

        public bool IsMessageValid(string messageID, bool optional)
        {
            return false;
        }

        public bool IsSignalValid(string signalID, bool optional)
        {
            return false;
        }

        public BusMessageInfo GetBusMessageInfo(string busMessageID)
        {
            return null;
        }

        public BusSignalInfo GetBusSignalInfo(string busSignalID)
        {
            return null;
        }

        public BusProtocolFileState GetBusProtocolFileState(BusProtocolFileID fileID)
        {
            return BusProtocolFileState.OK;
        }

        public string GetChannelAliasName(string key)
        {
            return null;
        }

        public void SetGlobalVariable(String key, String value)
        {

        }

        public String GetGlobalVariable(String key, String defaultValue)
        {
            return null;
        }

        public void SetGlobalParameter(String key, String value)
        {

        }

        public String GetGlobalParameter(String key, String defaultValue)
        {
            return null;
        }

        public void AddSignalReference(String signalID)
        {
        }

        public void RemoveSignalReference(String signalID)
        {
        }

        public bool IsInputChannelAvailable(String protocol)
        {
            return false;
        }

        public BusSignalValue[] ParseBusMessage(BusMessageSample busMessage)
        {
            return null;
        }

        public void SendBusMessage(BusMessage msg)
        {
        }

        public void SendBusMessage(string messageID, uint? interval)
        {
        }

        public void SendRawData(string protocol, double[] values, byte[] binary)
        {
        }

        public void SendManualTrigger(int channel)
        {
        }

        public string GetDataPath()
        {
            return null;
        }

        public string[] GetGenerationList()
        {
            return null;
        }

        public string GetGenerationPath(DateTime session, string generation)
        {
            return null;
        }

        public DateTime[] GetFinishedSessions(string generation)
        {
            return null;
        }

        public void RefreshGenerations()
        {
        }

        public double GetInterestTime()
        {
            return 0;
        }

        public DateTime? GetInterestTimestamp()
        {
            return null;
        }

        public void SetInterestTime(double time)
        {
        }

        public void SetInterestTimestamp(DateTime timestamp)
        {
        }

        public BufferRange GetBufferRange()
        {
            return new BufferRange();
        }

        public GeneralDeviceStatus GetDeviceStatus(string type)
        {
            return GeneralDeviceStatus.None;
        }

        public GeneralDeviceStatus[] GetChildDeviceStatus(string type)
        {
            return null;
        }

        public bool IsInternetConnected()
        {
            return false;
        }

        public void CallWebApi(string request, WebApiContext context)
        {
        }

        public string[] GetManualTriggerNames()
        {
            return null;
        }

        public void SetManualTriggerName(int index, string name)
        {
        }

        public DateTime[] GetFilteredSessionList()
        {
            return null;
        }

        public string GetCurrentDataGeneration()
        {
            return null;
        }

        public string[] GetSignalNamesOfBusMessage(string messageID)
        {
            return null;
        }

        public void SetGlobalPath(string key, string path)
        {
        }

        public BusFileInfo[] GetBusProtocolFilesInfo()
        {
            return null;
        }

        public string[] GetBusFloat32Signals()
        {
            return null;
        }

        public float GetBusMessageFPS(int channel, uint localID)
        {
            return 0;
        }

        public bool IsBusMessageBound(string busMessageID)
        {
            return false;
        }

        public object[] GetEventHandles()
        {
            return null;
        }

        public EventInfo GetEventInfo(object eventHandle)
        {
            return null;
        }

        public void RemoveEvent(object eventHandle)
        {
        }

        public void SetEventComment(object eventHandle, string comment)
        {
        }

        public double? GetSessionTimeline(DateTime session)
        {
            return null;
        }

        public double? GetSessionLength(DateTime session)
        {
            return null;
        }

        public int[] GetGraphIDList()
        {
            return null;
        }

        public string GetGraphTitle(int id)
        {
            return null;
        }

        public string GetSignalName(string signalID, bool fullName)
        {
            return null;
        }

        public bool IsVideoDataAvailable(int channel, uint? tolerance)
        {
            return false;
        }

        public BusMessageInfo GetBusMessageInfo(int channel, uint localID)
        {
            return null;
        }

        public DateTime[] GetSessionList()
        {
            return null;
        }

        public double GetFilteredSessionListTotalLength()
        {
            return 0;
        }

        public string[] GetSceneIDList()
        {
            return null;
        }

        public Dictionary<string, SceneTitle> GetSceneTitleTable()
        {
            return null;
        }

        public string GetCurrentSessionGUID()
        {
            return null;
        }

        public void CallWebApiPost(String request, byte[] body, WebApiContext context)
        {
        }

        public void CallWebApiPost(string request, byte[] body, WebPostContentType type, WebApiContext context)
        {
        }

        public Dictionary<string, bool> GetChannelStatusTable(uint? tolerance)
        {
            return null;
        }

        public bool SetControlFlag(string controllerID, bool enabled)
        {
            return false;
        }

        public string GetModuleConfig(object caller, string moduleClassID)
        {
            return null;
        }

        public void SetModuleConfig(object caller, string moduleClassID, string config)
        {
        }

        public ConfigStatus GetModuleConfigStatus(object caller, string moduleClassID)
        {
            return ConfigStatus.Disabled;
        }

        public ConfigStatus[] GetModuleChildConfigStatus(object caller, string moduleClassID)
        {
            return null;
        }

        public Samples.SpecialCameraType GetVideoSpecialType(int channel)
        {
            return Samples.SpecialCameraType.Normal;
        }

        public ulong GetMemoryCapacity()
        {
            return 0;
        }

        public Dictionary<BusDeviceID, BusDeviceInfo> GetBusDevices()
        {
            return null;
        }

        public Dictionary<VideoDeviceID, VideoDeviceInfo> GetVideoDevices()
        {
            return null;
        }

        public Dictionary<string, string> GetNativePluginVersions(string prefix)
        {
            return null;
        }

        public int? GetBusProtocolFileChannel(string fileID)
        {
            return null;
        }

        public void AddWindow(object caller, string windowClassID, string config, bool newWorkspaceIfNeeded)
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
            return new FloatPoint();
        }

        public LocPoint ConvertOfflineMapPixToLoc(LocPoint origin, int zoom, FloatPoint pixel)
        {
            return new LocPoint();
        }

        public VideoFrameGetter CreateVideoFrameGetter()
        {
            return null;
        }

        public object GetOfflineMapImage(IntSize imageSize, LocPoint centerLocation, int zoom)
        {
            return null;
        }

        public void OpenDialog(object caller, string dialogClassID, string config)
        {
        }

        public List<string> SelectBusFloat32Signal(List<string> existSignalIDList)
        {
            return null;
        }

        public string SelectBusMessage(string originMessageID)
        {
            return null;
        }

        public BusProtocolFileID[] SelectBusProtocolFiles(BusProtocolFileID[] selected)
        {
            return null;
        }

        public SignalConfig SelectSignal(SignalConfig origin, bool withScale, bool withSignBit, string unit)
        {
            return null;
        }

        public void SelectSignals(SelectSignalHandler handler, List<string> existSignalIDList)
        {
        }

        public void SetCurrentDialogTitle(string title, object icon)
        {
        }

        public void SetWindowTitle(object window, string title, object icon)
        {
        }

        public void RegisterAudioDriver(AudioDriverInfo driver, AudioRecorder recorder, AudioReplayer replayer)
        {
        }

        public AudioDriverInfo[] GetAudioDrivers()
        {
            return null;
        }

        public AudioDeviceInfo[] GetAudioRecordDevices(string driverID)
        {
            return null;
        }

        public AudioDeviceInfo[] GetAudioReplayDevices(string driverID)
        {
            return null;
        }

        public double GetCPUTime()
        {
            return 0;
        }

        public String GetSessionPath(DateTime session)
        {
            return null;
        }

        public String GetSessionPublicDataPath(DateTime session)
        {
            return null;
        }

        public String GetGlobalPublicDataPath()
        {
            return null;
        }

        public String GetManualTriggerName(int index)
        {
            return null;
        }

        public TaskResult RunStandaloneTask(object caller, String taskClassID, String config, out String returnValue)
        {
            returnValue = null;
            return TaskResult.TaskInitFailed;
        }

        public void StartReplay(double startTimeline, double? interestTarget)
        {
        }

        public bool StartOnline(String controllerName, bool previewOnly)
        {
            return false;
        }

        public void StopRunning()
        {
        }

        public bool StopRunning(String controllerID)
        {
            return false;
        }

        public int? GetGraphIDWithTitle(String title)
        {
            return null;
        }

        public TimeWithSession ConvertTimeIntoSession(double timeline)
        {
            return null;
        }

        public String[] GetEventTypeNames()
        {
            return null;
        }

        public string[] GetRecentProjectPathes()
        {
            return null;
        }

        public bool TerminateApp(bool force, bool autosave)
        {
            return false;
        }

        public void PopupError(string msg)
        {
        }

        public void PopupNotice(string msg)
        {
        }

        public bool PopupConfirm(string msg)
        {
            return false;
        }

        public void AddMainThreadCheckpoint(string location)
        {
        }

        public bool NewProject(bool force)
        {
            return false;
        }

        public bool OpenProject(string projectFile)
        {
            return false;
        }

        public bool SaveCurrentProject(string projectFile)
        {
            return false;
        }

        public string GetCurrentProject()
        {
            return null;
        }

        public void PlayMp3(byte[] mp3FileData)
        {
        }

        public bool StartProcess(string target)
        {
            return false;
        }

        public Dictionary<string, Version> GetVersionTable()
        {
            return null;
        }

        public string GetSystemStatus(SystemStatus status)
        {
            return null;
        }

        public string GetSystemStatusDetails(SystemStatus status)
        {
            return null;
        }

        public int[] GetLicensedFunctionIndices()
        {
            return null;
        }

        public bool SwitchAppMode(string controllerName, ApplicationMode mode, int waitSecond)
        {
            return false;
        }

        public void SetDataPath(string path)
        {
        }

        public Dictionary<string, WindowClassInfo> GetWindowClassTable()
        {
            return null;
        }

        public Dictionary<string, DialogClassInfo> GetDialogClassTable()
        {
            return null;
        }

        public Dictionary<string, ProcessorClassInfo> GetProcessorClassTable()
        {
            return null;
        }

        public Dictionary<string, NativeClassInfo> GetNativeClassTable()
        {
            return null;
        }

        public Dictionary<string, TaskClassInfo> GetTaskClassTable()
        {
            return null;
        }

        public Dictionary<string, Version> GetNativePluginVersions(NativeLibraryType type)
        {
            return null;
        }

        public WindowClassInfo RegisterTransformWindowClass(string windowClassID, string config)
        {
            return null;
        }

        public DialogClassInfo RegisterTransformDialogClass(string dialogClassID, string config)
        {
            return null;
        }

        public ConfigStatus GetDialogRelatedModulesConfigStatus(string dialogClassID, string transformID, out ConfigStatus[] childrenStatus)
        {
            childrenStatus = null;
            return ConfigStatus.Disabled;
        }

        public void DisableAllConfigs()
        {
        }

        public DateTime[] GetGenerationSessions(string generationID)
        {
            return null;
        }

        public double GetInterestTarget()
        {
            return 0;
        }

        public Dictionary<string, DeviceStatusDetail> GetAllDeviceStatus()
        {
            return null;
        }

        public void SetTargetReplaySpeed(double speed)
        {
        }

        public void SetSessionSearchKeyword(string keyword)
        {
        }

        public Dictionary<DateTime, SessionFilterFlags> GetSessionFilterTable()
        {
            return null;
        }

        public DateTime? GetCurrentOnlineSession()
        {
            return null;
        }

        public double GetSessionListTotalLength()
        {
            return 0;
        }

        public string GetSessionSearchKey()
        {
            return null;
        }

        public bool RemoveSession(DateTime session, bool force)
        {
            return false;
        }

        public void SetSessionChecker(DateTime session, bool check)
        {
        }

        public void RemoveGeneration(DateTime session, string genID)
        {
        }

        public string GetSessionComment(DateTime session)
        {
            return null;
        }

        public void SetAudioVolume(double volume)
        {
        }

        public void SetCPURateScale(int scale)
        {
        }

        public string GetLicenseInfo()
        {
            return null;
        }

        public double GetRawChannelDelayConfig(string id)
        {
            return 0;
        }

        public double GetBusChannelDelayConfig(int channel)
        {
            return 0;
        }

        public double GetVideoChannelDelayConfig(int channel)
        {
            return 0;
        }

        public void SetRawChannelDelayConfig(string id, double delay)
        {
        }

        public void SetBusChannelDelayConfig(int channel, double delay)
        {
        }

        public void SetVideoChannelDelayConfig(int channel, double delay)
        {
        }

        public bool GetChannelMonitoringFlag(string key)
        {
            return false;
        }

        public void SetChannelMonitoringFlag(string key, bool monitoring)
        {
        }

        public bool GetBusChannelStatus(int channel, uint? toleranceMillisecond)
        {
            return false;
        }

        public bool GetVideoChannelStatus(int channel, uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            interval = null;
            delay = null;
            return false;
        }

        public bool GetAudioChannelStatus(uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            interval = null;
            delay = null;
            return false;
        }

        public bool GetRawChannelStatus(string protocol, uint? toleranceMillisecond)
        {
            return false;
        }

        public bool GetSampleChannelStatus(string protocol, uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            interval = null;
            delay = null;
            return false;
        }

        public int[] GetAvailableBusChannels()
        {
            return null;
        }

        public int[] GetAvailableVideoChannels()
        {
            return null;
        }

        public string[] GetAvailableRawChannels()
        {
            return null;
        }

        public string[] GetAvailableSampleChannels()
        {
            return null;
        }

        public double? GetBusPayloadPercentage(int channel)
        {
            return null;
        }

        public double GetAudioChannelDelayConfig()
        {
            return 0;
        }

        public Dictionary<string, double> GetAllRawChannelDelayConfigs()
        {
            return null;
        }

        public void SetAudioChannelDelayConfig(double delay)
        {
        }

        public string[] GetAllChannelMonitoringKeys()
        {
            return null;
        }

        public bool IsSampleChannelConflict(string protocol)
        {
            return false;
        }

        public bool StartReplay(bool force, double startTimeline, double? interestTarget)
        {
            return false;
        }

        public bool StartOnline(bool force, bool previewOnly)
        {
            return false;
        }

        public bool StartOffline(bool force, bool previewOnly)
        {
            return false;
        }

        public bool StopRunning(bool force, bool editRecordedSession)
        {
            return false;
        }

        public CreatePanelResult CreateWindowPanel(object caller, string windowClassID, string transformID, out object panel, out WindowClassInfo info)
        {
            panel = null;
            info = null;
            return CreatePanelResult.CreateFailed;
        }

        public CreatePanelResult CreateConfigPanel(object caller, string dialogClassID, string transformID, out object panel, out DialogClassInfo info)
        {
            panel = null;
            info = null;
            return CreatePanelResult.CreateFailed;
        }

        public void UnregisterPanel(object panel)
        {
        }

        public CommonImage GetOfflineMapCommonImage(IntSize imageSize, LocPoint centerLocation, int zoom)
        {
            return null;
        }

        public CommonImage ConvertImageToCommon(object image)
        {
            return null;
        }

        public object ConvertImageToPlatform(CommonImage image, bool eto)
        {
            return null;
        }

        public WindowClassInfo GetWindowClassInfo(string windowClassID)
        {
            return null;
        }

        public DialogClassInfo GetDialogClassInfo(string dialogClassID)
        {
            return null;
        }

        public CommonImage DecodeImage(byte[] imageData)
        {
            return null;
        }

        public byte[] EncodeImage(CommonImage image, string format)
        {
            return null;
        }

        public SignalTreeNode[] GetSignalTree()
        {
            return null;
        }

        public BusProtocolFileID[] GetBusProtocolFileIDList()
        {
            return null;
        }

        public string GetBusProtocolFilePath(BusProtocolFileID fileID)
        {
            return null;
        }

        public bool UpdateBusProtocolFilePath(BusProtocolFileID fileID, string filePath)
        {
            return false;
        }

        public bool AddBusProtocolFile(String filePath, out BusProtocolFileID fileID)
        {
            fileID = null;
            return false;
        }

        public void RemoveBusProtocolFile(BusProtocolFileID fileID)
        {
        }

        public void SetSessionComment(DateTime session, string comment)
        {
        }

        public Dictionary<string, string> GetSessionProperties(DateTime session)
        {
            return null;
        }

        public void SetSessionProperties(DateTime session, Dictionary<string, string> properties)
        {
        }

        public string[] GetPluginPackIDList()
        {
            return null;
        }

        public PluginPackInfo GetPluginPackInfo(string packID)
        {
            return null;
        }

        public bool InstallPlugin(string dirPath)
        {
            return false;
        }

        public bool UninstallPlugin(string packID)
        {
            return false;
        }

        public LogMessage[] GetLogMessages()
        {
            return null;
        }

        public bool IsReady()
        {
            return false;
        }
    }
}
