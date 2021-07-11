using System;
using System.Collections.Generic;
using ASEva.Utility;

namespace ASEva
{
    class AgencyDefaultGlobal : AgencyHandlerGlobal
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

        public Dictionary<string, string> GetGlobalVariableTable()
        {
            return null;
        }

        public Dictionary<string, string> GetGlobalParameterTable()
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

        public void UpdateSignalRef(string signalID, bool isRemove)
        {
        }

        public bool DeleteToRecycleBin(string path)
        {
            return false;
        }

        public bool IsMessageValid(string messageID)
        {
            return false;
        }

        public bool IsSignalValid(string signalID)
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
    }

    class AgencyDefaultLocal : AgencyHandlerLocal
    {
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

        public void UpdateSignalRef(string signalID, bool isRemove)
        {
        }

        public bool RunNativeStandaloneTask(string title, string nativeType, string taskName, string config, out string returnValue)
        {
            returnValue = null;
            return false;
        }

        public bool RunStandaloneTask(string title, Task task, string config)
        {
            return false;
        }

        public TaskClass GetTaskClass(object caller, string taskClassID)
        {
            return null;
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

        public bool IsBusMessageBinded(string busMessageID)
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

        public bool SwitchMode(String controllerID, ApplicationMode mode)
        {
            return false;
        }

        public bool StartSession(String controllerID, bool withRecord, double startTimeline, double? interestTarget)
        {
            return false;
        }

        public bool Stopsession(String controllerID)
        {
            return false;
        }

        public double? GetSessionTimeline(DateTime session)
        {
            return null;
        }

        public double? GetSessionLength(DateTime session)
        {
            return null;
        }

        public int[] GetGraphIDList(string title)
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

        public Sample GetVirtualSampleOfTimeline(double timeline)
        {
            return null;
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
    }
}
