using System;
using System.Collections.Generic;
using ASEva.Samples;
using ASEva.Utility;

namespace ASEva
{
    public interface AgencyHandler
    {
        String GetConfigFilesRoot();
        String GetAppFilesRoot();
        String GetTempFilesRoot();
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
        GraphData GetGraphData(DateTime session, int id);
        void AddSceneData(SceneData scene);
        List<String> GetSampleTitle(String protocol);
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
        void RemoveBusProtocolFile(BusProtocolFileID fileID);
        String GetChannelAliasName(String key);
        bool IsInputChannelAvailable(String protocol);
        BusSignalValue[] ParseBusMessage(BusMessageSample busMessage);
        void SendBusMessage(BusMessage msg);
        void SendBusMessage(String messageID, uint? interval);
        void SendRawData(String protocol, double[] values, byte[] binary);
        void SendManualTrigger(int channel);
        String GetDataPath();
        String[] GetSubDataPathes();
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
        void SetCurrentDataLayer(String layer);
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
        int? GetBusProtocolFileChannel(String fileID);
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
        void StopRunning();
        bool StopRunning(String controllerID);
        bool StopRunning(bool force, bool editRecordedSession);
        double? GetSessionTimeline(DateTime session);
        double? GetSessionLength(DateTime session);
        double GetFilteredSessionListTotalLength();
        int[] GetGraphIDList();
        int? GetGraphIDWithTitle(String title);
        String GetGraphTitle(int id);
        String GetSignalName(String signalID, bool fullName);
        bool IsVideoDataAvailable(int channel, uint? tolerance);
        Samples.SpecialCameraType GetVideoSpecialType(int channel);
        TimeWithSession ConvertTimeIntoSession(double timeline);
        String[] GetSceneIDList();
        Dictionary<String, SceneTitle> GetSceneTitleTable();
        Dictionary<String, bool> GetChannelStatusTable(uint? tolerance);
        bool SetControlFlag(String controllerID, bool enabled);
        String GetModuleConfig(object caller, String moduleClassID);
        void SetModuleConfig(object caller, String moduleClassID, String config);
        ConfigStatus GetModuleConfigStatus(object caller, String moduleClassID);
        ConfigStatus[] GetModuleChildConfigStatus(object caller, String moduleClassID);
        Dictionary<BusDeviceID, BusDeviceInfo> GetBusDevices();
        Dictionary<VideoDeviceID, VideoDeviceInfo> GetVideoDevices();
        Dictionary<String, String> GetNativePluginVersions(String prefix);
        Dictionary<String, Version> GetNativePluginVersions(NativeLibraryType type);
        VideoFrameGetter CreateVideoFrameGetter();
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
        List<String> SelectBusFloat32Signal(List<String> existSignalIDList);
        void ConfigDataEncryption();
        void ConfigOfflineMapPath();
        void OpenDialog(object caller, String dialogClassID, String config);
        void AddWindow(object caller, String windowClassID, String config, bool newWorkspaceIfNeeded);
        void RegisterAudioDriver(AudioDriverInfo driver, AudioRecorder recorder, AudioReplayer replayer);
        AudioDriverInfo[] GetAudioDrivers();
        AudioDeviceInfo[] GetAudioRecordDevices(String driverID);
        AudioDeviceInfo[] GetAudioReplayDevices(String driverID);
        double GetCPUTime();
        String[] GetEventTypeNames();
        String[] GetRecentProjectPathes();
        bool TerminateApp(bool force, bool autosave);
        void PopupError(String msg);
        void PopupNotice(String msg);
        bool PopupConfirm(String msg);
        void AddMainThreadCheckpoint(String location);
        bool NewProject(bool force);
        bool OpenProject(String projectFile);
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
        Dictionary<String, TaskClassInfo> GetTaskClassTable();
        WindowClassInfo RegisterTransformWindowClass(String windowClassID, String config);
        DialogClassInfo RegisterTransformDialogClass(String dialogClassID, String config);
        ConfigStatus GetDialogRelatedModulesConfigStatus(String dialogClassID, String transformID, out ConfigStatus[] childrenStatus);
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
        bool GetChannelMonitoringFlag(String key);
        void SetChannelMonitoringFlag(String key, bool monitoring);
        String[] GetAllChannelMonitoringKeys();
        bool GetBusChannelStatus(int channel, uint? toleranceMillisecond);
        bool GetVideoChannelStatus(int channel, uint? toleranceMillisecond, out List<double> interval, out List<double> delay);
        bool GetAudioChannelStatus(uint? toleranceMillisecond, out List<double> interval, out List<double> delay);
        bool GetRawChannelStatus(String protocol, uint? toleranceMillisecond);
        bool GetSampleChannelStatus(String protocol, uint? toleranceMillisecond, out List<double> interval, out List<double> delay);
        int[] GetAvailableBusChannels();
        int[] GetAvailableVideoChannels();
        String[] GetAvailableRawChannels();
        String[] GetAvailableSampleChannels();
        double? GetBusPayloadPercentage(int channel);
        bool IsSampleChannelConflict(String protocol);
        CreatePanelResult CreateWindowPanel(object caller, String windowClassID, String transformID, out object panel, out WindowClassInfo info);
        CreatePanelResult CreateConfigPanel(object caller, String dialogClassID, String transformID, out object panel, out DialogClassInfo info);
        void UnregisterPanel(object panel);
        CommonImage ConvertImageToCommon(object image);
        object ConvertImageToPlatform(CommonImage image, bool eto);
        WindowClassInfo GetWindowClassInfo(String windowClassID);
        DialogClassInfo GetDialogClassInfo(String dialogClassID);
        CommonImage DecodeImage(byte[] imageData);
        byte[] EncodeImage(CommonImage image, String format);
        SignalTreeNode[] GetSignalTree();
        String[] GetPluginPackIDList();
        PluginPackInfo GetPluginPackInfo(String packID);
        bool InstallPlugin(String dirPath);
        bool UninstallPlugin(String packID);
        LogMessage[] GetLogMessages();
        bool IsReady();
        void DisablePlugin(String packID);
        void EnablePlugin(String packID);
    }

    /// <summary>
    /// (api:app=2.0.0) 集合了ASEva所有主要API
    /// </summary>
    public partial class Agency
    {
        private static AgencyHandler hander = null;
        public static AgencyHandler Handler
        {
            private get
            {
                if (hander == null) hander = new AgencyDefault();
                return hander;
            }
            set
            {
                if (value != null) hander = value;
            }
        }

        /// <summary>
        /// 获取ASEva当前的运行状态
        /// </summary>
        /// <returns>ASEva运行状态</returns>
        public static ApplicationStatus GetAppStatus()
        {
            return Handler.GetAppStatus();
        }

        /// <summary>
        /// 获取ASEva当前的运行模式
        /// </summary>
        /// <returns>ASEva运行模式</returns>
        public static ApplicationMode GetAppMode()
        {
            return Handler.GetAppMode();
        }

        /// <summary>
        /// 检查是否为在线采集或离线处理生成模式
        /// </summary>
        /// <returns>是否为在线采集或离线处理生成模式</returns>
        public static bool IsFileOutputEnabled()
        {
            return Handler.IsFileOutputEnabled();
        }

        /// <summary>
        /// 获取ASEva当前的显示语言
        /// </summary>
        /// <returns>语言ID，如en表示英文，ch表示中文等</returns>
        public static String GetAppLanguage()
        {
            return Handler.GetAppLanguage();
        }

        /// <summary>
        /// 获取ASEva当前的兴趣时间点
        /// </summary>
        /// <returns>在时间线上的兴趣点，单位秒</returns>
        public static double GetInterestTime()
        {
            return Handler.GetInterestTime();
        }

        /// <summary>
        /// 获取ASEva当前的兴趣时间戳
        /// </summary>
        /// <returns>兴趣点的时间戳(包括年月日时分秒，毫秒)，若无数据则返回null</returns>
        public static DateTime? GetInterestTimestamp()
        {
            return Handler.GetInterestTimestamp();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取兴趣时间点目标
        /// </summary>
        /// <returns>兴趣时间点目标，可能超出数据缓存范围</returns>
        public static double GetInterestTarget()
        {
            return Handler.GetInterestTarget();
        }

        /// <summary>
        /// 获取ASEva数据缓存范围
        /// </summary>
        /// <returns>数据缓存范围</returns>
        public static BufferRange GetBufferRange()
        {
            return Handler.GetBufferRange();
        }

        /// <summary>
        /// 获取当前数据层级下的所有session
        /// </summary>
        /// <returns>Session ID列表，ID为数据起始的系统时间</returns>
        public static DateTime[] GetSessionList()
        {
            return Handler.GetSessionList();
        }

        /// <summary>
        /// 获取当前数据层级下筛选后的所有session
        /// </summary>
        /// <returns>Session ID列表，ID为数据起始的系统时间</returns>
        public static DateTime[] GetFilteredSessionList()
        {
            return Handler.GetFilteredSessionList();
        }

        /// <summary>
        /// 获取当前数据目录下的所有generation
        /// </summary>
        /// <returns>Generation ID列表</returns>
        public static String[] GetGenerationList()
        {
            return Handler.GetGenerationList();
        }

        /// <summary>
        /// 获取某个session数据在硬盘的根路径
        /// </summary>
        /// <param name="session">希望获取的session ID</param>
        /// <returns>Session数据的根路径，若不存在或不属于当前层级则返回null</returns>
        public static String GetSessionPath(DateTime session)
        {
            return Handler.GetSessionPath(session);
        }

        /// <summary>
        /// 获取某个session的公共数据在硬盘的根路径
        /// </summary>
        /// <param name="session">希望获取的session ID</param>
        /// <returns>Session公共数据的根路径，若不存在或不属于当前层级则返回null</returns>
        public static String GetSessionPublicDataPath(DateTime session)
        {
            return Handler.GetSessionPublicDataPath(session);
        }

        /// <summary>
        /// (api:app=2.4.0) 获取某个session所属数据层级
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>数据层级，其中‘.’表示根路径下的session，'..'表示根路径即session</returns>
        public static String GetSessionLayer(DateTime session)
        {
            return Handler.GetSessionLayer(session);
        }

        /// <summary>
        /// (api:app=2.4.0) 获取某个session的文件夹名
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>文件夹名</returns>
        public static String GetSessionFolderName(DateTime session)
        {
            return Handler.GetSessionFolderName(session);
        }

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

        /// <summary>
        /// 获取当前层级下某个generation下所有处理完毕的session
        /// </summary>
        /// <param name="generation">目标generation</param>
        /// <returns>处理完毕的session ID列表</returns>
        public static DateTime[] GetFinishedSessions(String generation)
        {
            return Handler.GetFinishedSessions(generation);
        }

        /// <summary>
        /// 刷新当前层级下所有session的generation
        /// </summary>
        public static void RefreshGenerations()
        {
            Handler.RefreshGenerations();
        }

        /// <summary>
        /// 获取当前输入数据的generation ID
        /// </summary>
        /// <returns>当前输入数据的generation ID，空表示输入数据为原始数据</returns>
        public static String GetCurrentDataGeneration()
        {
            return Handler.GetCurrentDataGeneration();
        }

        /// <summary>
        /// 获取当前session的GUID
        /// </summary>
        /// <returns>当前session的GUID，若未运行则返回null</returns>
        public static String GetCurrentSessionGUID()
        {
            return Handler.GetCurrentSessionGUID();
        }

        /// <summary>
        /// 获取某个session的本地开始时间（相对时间戳=0）
        /// </summary>
        /// <param name="session">希望获取本地开始时间的session ID</param>
        /// <returns>本地开始时间，若空表示无此信息或session不在当前层级</returns>
        public static DateTime? GetStartTimeLocal(DateTime session)
        {
            return Handler.GetStartTimeLocal(session);
        }

        /// <summary>
        /// 获取某个session的UTC开始时间（相对时间戳=0）
        /// </summary>
        /// <param name="session">希望获取UTC开始时间的session ID</param>
        /// <returns>UTC开始时间，若空表示无此信息或session不在当前层级</returns>
        public static DateTime? GetStartTimeUTC(DateTime session)
        {
            return Handler.GetStartTimeUTC(session);
        }

        /// <summary>
        /// 获取某个session上指定相对时间对应的本地时间
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">相对时间 (时间偏置)</param>
        /// <returns>本地时间戳</returns>
        public static DateTime? GetTimestampLocal(DateTime session, double timeOffset)
        {
            return Handler.GetTimestampLocal(session, timeOffset);
        }

        /// <summary>
        /// 获取某个session上指定相对时间对应的UTC时间
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="timeOffset">相对时间 (时间偏置)</param>
        /// <returns>UTC时间戳</returns>
        public static DateTime? GetTimestampUTC(DateTime session, double timeOffset)
        {
            return Handler.GetTimestampUTC(session, timeOffset);
        }

        /// <summary>
        /// 获取某个session上相对时间转为本地时间的时间比例
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>相对时间转为本地时间的时间比例</returns>
        public static double GetTimeRatioToLocal(DateTime session)
        {
            return Handler.GetTimeRatioToLocal(session);
        }

        /// <summary>
        /// 获取某个session上相对时间转为UTC时间的时间比例
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>相对时间转为UTC时间的时间比例</returns>
        public static double GetTimeRatioToUTC(DateTime session)
        {
            return Handler.GetTimeRatioToUTC(session);
        }

        /// <summary>
        /// 获取当前数据目录的路径
        /// </summary>
        /// <returns>当前数据目录的路径，若未设置返回null</returns>
        public static String GetDataPath()
        {
            return Handler.GetDataPath();
        }

        /// <summary>
        /// (api:app=2.4.0) 获取所有子数据目录的路径
        /// </summary>
        /// <returns>所有子数据目录的路径，目录不存在则为null</returns>
        public static String[] GetSubDataPathes()
        {
            return Handler.GetSubDataPathes();
        }

        /// <summary>
        /// (api:app=2.4.0) 设置子数据目录的路径
        /// </summary>
        /// <param name="subIndex">子数据目录序号，0~3</param>
        /// <param name="path">子数据目录的路径</param>
        public static void SetSubDataPath(int subIndex, String path)
        {
            Handler.SetSubDataPath(subIndex, path);
        }

        /// <summary>
        /// (api:app=2.4.0) 获取所有有效的数据层级
        /// </summary>
        /// <returns>数据层级列表，其中‘.’表示根路径下的session，'..'表示根路径即session</returns>
        public static String[] GetDataLayers()
        {
            return Handler.GetDataLayers();
        }

        /// <summary>
        /// (api:app=2.4.0) 添加新的数据层级
        /// </summary>
        /// <param name="layer">数据层级</param>
        public static void AddDataLayer(String layer)
        {
            Handler.AddDataLayer(layer);
        }

        /// <summary>
        /// (api:app=2.4.0) 移除数据层级，并删除所有文件至回收站
        /// </summary>
        /// <param name="layer">数据层级</param>
        public static void DeleteDataLayer(String layer)
        {
            Handler.DeleteDataLayer(layer);
        }

        /// <summary>
        /// (api:app=2.4.0) 获取当前的数据层级
        /// </summary>
        /// <returns>数据层级，其中null表示所有层级，‘.’表示根路径下的session，'..'表示根路径即session</returns>
        public static String GetCurrentDataLayer()
        {
            return Handler.GetCurrentDataLayer();
        }

        /// <summary>
        /// (api:app=2.4.0) 设置当前的数据层级
        /// </summary>
        /// <param name="layer">数据层级，其中null表示所有层级，‘.’表示根路径下的session，'..'表示根路径即session</param>
        public static void SetCurrentDataLayer(String layer)
        {
            Handler.SetCurrentDataLayer(layer);
        }

        /// <summary>
        /// 获取当前全局公共数据目录的路径
        /// </summary>
        /// <returns>当前全局公共数据目录的路径，若未设置返回null</returns>
        public static String GetGlobalPublicDataPath()
        {
            return Handler.GetGlobalPublicDataPath();
        }

        /// <summary>
        /// 获取配置文件根目录路径
        /// </summary>
        /// <returns>配置文件根目录路径</returns>
        public static String GetConfigFilesRoot()
        {
            return Handler.GetConfigFilesRoot();
        }

        /// <summary>
        /// 获取应用数据和文档文件根目录路径
        /// </summary>
        /// <returns>应用数据和文档文件根目录路径</returns>
        public static String GetAppFilesRoot()
        {
            return Handler.GetAppFilesRoot();
        }

        /// <summary>
        /// 获取临时文件根目录路径
        /// </summary>
        /// <returns>临时文件根目录路径</returns>
        public static String GetTempFilesRoot()
        {
            return Handler.GetTempFilesRoot();
        }

        /// <summary>
        /// 设定想要发送的总线报文，可周期性发送，也可单次发送（仅在线模式可用）
        /// </summary>
        /// <param name="message"> 想要发送的报文信息</param>
        public static void SendBusMessage(BusMessage message)
        {
            Handler.SendBusMessage(message);
        }

        /// <summary>
        /// 设定想要发送的总线报文（该报文需设置绑定），可周期性发送，也可单次发送（仅在线模式可用）
        /// </summary>
        /// <param name="messageID">绑定的报文ID</param>
        /// <param name="interval">报文发送周期，单位毫秒（至少为10），若设为null则只发送一次</param>
        public static void SendBusMessage(String messageID, uint? interval)
        {
            Handler.SendBusMessage(messageID, interval);
        }

        /// <summary>
        /// 添加想要采集的原始数据信息（仅在线模式可用）
        /// </summary>
        /// <param name="protocol">原始数据协议名称，对应input/raw/raw.csv首列文字</param>
        /// <param name="values">数值数据</param>
        /// <param name="binary">二进制数据</param>
        public static void SendRawData(String protocol, double[] values, byte[] binary)
        {
            Handler.SendRawData(protocol, values, binary);
        }

        /// <summary>
        /// 发送手动触发器信号
        /// </summary>
        /// <param name="channel">手动触发器通道，0~15</param>
        public static void SendManualTrigger(int channel)
        {
            Handler.SendManualTrigger(channel);
        }

        /// <summary>
        /// 获取图表对象
        /// </summary>
        /// <param name="session">想要获取图表的session ID</param>
        /// <param name="id">图表的ID，通过 ASEva.GraphDefinition.GetHashCode 获取</param>
        /// <returns>图表对象，若不存在或不属于当前层级则返回null</returns>
        public static GraphData GetGraphData(DateTime session, int id)
        {
            return Handler.GetGraphData(session, id);
        }

        /// <summary>
        /// 添加一个场景片段
        /// </summary>
        /// <param name="scene">想要添加的场景片段描述</param>
        public static void AddSceneData(SceneData scene)
        {
            Handler.AddSceneData(scene);
        }

        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="type">设备的类型ID，在插件的info.txt中以type字段描述</param>
        /// <returns>返回设备状态</returns>
        public static GeneralDeviceStatus GetDeviceStatus(String type)
        {
            return Handler.GetDeviceStatus(type);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取所有设备状态
        /// </summary>
        /// <returns>设备状态表，键为设备的类型ID，在插件的info.txt中以type字段描述</returns>
        public static Dictionary<String, DeviceStatusDetail> GetAllDeviceStatus()
        {
            return Handler.GetAllDeviceStatus();
        }

        /// <summary>
        /// 获取各子设备的设备状态
        /// </summary>
        /// <param name="type">设备的类型ID，在插件的info.txt中以type字段描述</param>
        /// <returns>各子设备的设备状态</returns>
        public static GeneralDeviceStatus[] GetChildDeviceStatus(String type)
        {
            return Handler.GetChildDeviceStatus(type);
        }

        /// <summary>
        /// 打印信息至Debugger
        /// </summary>
        /// <param name="text">想要打印的文本</param>
        public static void Print(String text)
        {
            Handler.Print(text);
        }

        /// <summary>
        /// 添加清单信息，在Log窗口和状态栏显示信息
        /// </summary>
        /// <param name="text">想要显示的信息</param>
        /// <param name="level">清单信息级别</param>
        public static void Log(String text, LogLevel level)
        {
            Handler.Log(text, level);
        }

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

        /// <summary>
        /// 获取指定报文ID下的所有信号名称
        /// </summary>
        /// <param name="messageID">报文的全局唯一ID</param>
        /// <returns>信号列表，若该报文不存在则返回null</returns>
        public static String[] GetSignalNamesOfBusMessage(String messageID)
        {
            return Handler.GetSignalNamesOfBusMessage(messageID);
        }

        /// <summary>
        /// 当前互联网是否已连接
        /// </summary>
        /// <returns>是否已连接互联网</returns>
        public static bool IsInternetConnected()
        {
            return Handler.IsInternetConnected();
        }

        /// <summary>
        /// 获取内存容量
        /// </summary>
        /// <returns>内存容量，单位为字节，获取失败时返回0</returns>
        public static ulong GetMemoryCapacity()
        {
            return Handler.GetMemoryCapacity();
        }

        /// <summary>
        /// 非阻塞调用Web API (GET)
        /// </summary>
        /// <param name="request">调用请求</param>
        /// <param name="context">调用上下文。由于本函数为非阻塞，在结束后需要通过该对象在未来时刻获取调用状态和响应字符串</param>
        public static void CallWebApi(String request, WebApiContext context)
        {
            Handler.CallWebApi(request, context);
        }

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

        /// <summary>
        /// 获取数据通道别名
        /// </summary>
        /// <param name="key">数据通道关键字，格式为"协议名@通道序号"，通道序号从0开始，协议名中带"v"字版本号的可向下兼容。视频协议名为video</param>
        /// <returns>数据通道别名，若未找到返回null</returns>
        public static String GetChannelAliasName(String key)
        {
            return Handler.GetChannelAliasName(key);
        }

        /// <summary>
        /// (api:app=2.3.0) 检查样本数据通道是否冲突（有多个模块输出相同协议和通道的样本数据）
        /// </summary>
        /// <param name="protocol">样本通道协议，格式为"协议名@通道序号"，通道序号从0开始，协议名中带"v"字版本号的可向下兼容</param>
        /// <returns>是否冲突</returns>
        public static bool IsSampleChannelConflict(string protocol)
        {
            return Handler.IsSampleChannelConflict(protocol);
        }

        /// <summary>
        /// 检查指定的输入样本通道是否可用
        /// </summary>
        /// <param name="protocol">样本通道关键字，格式为"协议名@通道序号"，通道序号从0开始，协议名中带"v"字版本号的可向下兼容</param>
        /// <returns>该通道是否可用</returns>
        public static bool IsInputChannelAvailable(String protocol)
        {
            return Handler.IsInputChannelAvailable(protocol);
        }

        /// <summary>
        /// 获取指定样本通道对应的标题
        /// </summary>
        /// <param name="protocol">样本通道关键字，格式为"协议名@通道序号"，通道序号从0开始</param>
        /// <returns>样本标题，null表示通道不存在或该样本通道无标题</returns>
        public static List<String> GetSampleTitle(String protocol)
        {
            return Handler.GetSampleTitle(protocol);
        }

        /// <summary>
        /// 获得手动触发器所有通道的名称
        /// </summary>
        /// <returns>名称列表</returns>
        public static String[] GetManualTriggerNames()
        {
            return Handler.GetManualTriggerNames();
        }

        /// <summary>
        /// 获得手动触发器通道的名称
        /// </summary>
        /// <param name="index">通道序号，0~15</param>
        /// <returns>手动触发器通道的名称，若序号超出范围则返回null</returns>
        public static String GetManualTriggerName(int index)
        {
            return Handler.GetManualTriggerName(index);
        }

        /// <summary>
        /// 设定手动触发器通道的名称
        /// </summary>
        /// <param name="index">通道序号，0~15</param>
        /// <param name="name">手动触发器通道的名称，若值为空则忽略</param>
        public static void SetManualTriggerName(int index, String name)
        {
            Handler.SetManualTriggerName(index, name);
        }

        /// <summary>
        /// 添加信号引用，在 ASEva.WindowPanel.OnInputData 中可获得该信号的数据
        /// </summary>
        /// <param name="signalID">信号ID</param>
        public static void AddSignalReference(String signalID)
        {
            Handler.AddSignalReference(signalID);
        }

        /// <summary>
        /// 移除信号引用
        /// </summary>
        /// <param name="signalID">信号ID</param>
        public static void RemoveSignalReference(String signalID)
        {
            Handler.RemoveSignalReference(signalID);
        }

        /// <summary>
        /// 运行一个独立处理任务，运行过程中将弹出进度条并禁用其他操作
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , WindowPanel, ConfigPanel等</param>
        /// <param name="taskClassID">任务组件的类别ID</param>
        /// <param name="config">配置的字符串描述</param>
        /// <param name="returnValue">任务的返回值信息</param>
        /// <returns>任务运行结果</returns>
        public static TaskResult RunStandaloneTask(object caller, String taskClassID, String config, out String returnValue)
        {
            return Handler.RunStandaloneTask(caller, taskClassID, config, out returnValue);
        }

        /// <summary>
        /// 设置兴趣时间戳
        /// </summary>
        /// <param name="targetTimestamp">目标时间戳</param>
        public static void SetInterestTimestamp(DateTime targetTimestamp)
        {
            Handler.SetInterestTimestamp(targetTimestamp);
        }

        /// <summary>
        /// 设置兴趣时间点
        /// </summary>
        /// <param name="targetTimeline">目标时间点</param>
        public static void SetInterestTime(double targetTimeline)
        {
            Handler.SetInterestTime(targetTimeline);
        }

        /// <summary>
        /// 删除指定文件或文件夹至回收站
        /// </summary>
        /// <param name="path">文件或文件夹路径</param>
        /// <returns>是否成功</returns>
        public static bool DeleteToRecycleBin(String path)
        {
            return Handler.DeleteToRecycleBin(path);
        }

        /// <summary>
        /// 设置全局变量
        /// </summary>
        /// <param name="key">全局变量key，若为null或""则忽略</param>
        /// <param name="value">全局变量value，若为null则忽略</param>
        public static void SetGlobalVariable(String key, String value)
        {
            Handler.SetGlobalVariable(key, value);
        }

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

        /// <summary>
        /// 设置全局参数
        /// </summary>
        /// <param name="key">全局参数key，若为null或""则忽略</param>
        /// <param name="value">全局参数value，若为null则忽略</param>
        public static void SetGlobalParameter(String key, String value)
        {
            Handler.SetGlobalParameter(key, value);
        }

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

        /// <summary>
        /// 获取全局路径
        /// </summary>
        /// <param name="key">全局路径key</param>
        /// <returns>以分号分割的全局路径value（仅返回存在的部分），若key为null、""则返回null</returns>
        public static String GetGlobalPath(String key)
        {
            return Handler.GetGlobalPath(key);
        }

        /// <summary>
        /// 设置全局路径
        /// </summary>
        /// <param name="key">全局路径key，若为null或""则忽略</param>
        /// <param name="path">以分号分割的全局路径value，不存在的部分将被忽略</param>
        public static void SetGlobalPath(String key, String path)
        {
            Handler.SetGlobalPath(key, path);
        }

        /// <summary>
        /// 获取所有总线通道上的协议文件信息
        /// </summary>
        /// <returns>总线协议文件信息列表</returns>
        public static BusFileInfo[] GetBusProtocolFilesInfo()
        {
            return Handler.GetBusProtocolFilesInfo();
        }

        /// <summary>
        /// 获取总线协议文件当前配置于哪个通道
        /// </summary>
        /// <param name="fileID">文件ID</param>
        /// <returns>总线通道（1~16），若未配置则返回null</returns>
        public static int? GetBusProtocolFileChannel(String fileID)
        {
            return Handler.GetBusProtocolFileChannel(fileID);
        }

        /// <summary>
        /// 获取所有作为32位浮点解析的信号列表
        /// </summary>
        /// <returns>信号ID列表</returns>
        public static String[] GetBusFloat32Signals()
        {
            return Handler.GetBusFloat32Signals();
        }

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

        /// <summary>
        /// 获取指定ID的总线报文的信息
        /// </summary>
        /// <param name="busMessageID">总线报文ID</param>
        /// <returns>总线报文的信息，报文不存在则返回null</returns>
        public static BusMessageInfo GetBusMessageInfo(String busMessageID)
        {
            return Handler.GetBusMessageInfo(busMessageID);
        }

        /// <summary>
        /// 已弃用，应使用 ASEva.Agency.IsBusMessageBound
        /// </summary>
        public static bool IsBusMessageBinded(string busMessageID)
        {
            return Handler.IsBusMessageBound(busMessageID);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取总线报文是否已绑定发送
        /// </summary>
        /// <param name="busMessageID">总线报文ID</param>
        /// <returns>是否已绑定</returns>
        public static bool IsBusMessageBound(string busMessageID)
        {
            return Handler.IsBusMessageBound(busMessageID);
        }

        /// <summary>
        /// 返回事件对象列表
        /// </summary>
        /// <returns>事件对象列表</returns>
        public static object[] GetEventHandles()
        {
            return Handler.GetEventHandles();
        }

        /// <summary>
        /// 返回指定事件对象的完整信息
        /// </summary>
        /// <param name="eventHandle">事件对象</param>
        /// <returns>事件完整信息，null表示事件对象无效或信息不完整</returns>
        public static EventInfo GetEventInfo(object eventHandle)
        {
            return Handler.GetEventInfo(eventHandle);
        }

        /// <summary>
        /// 移除指定事件对象
        /// </summary>
        /// <param name="eventHandle">事件对象</param>
        public static void RemoveEvent(object eventHandle)
        {
            Handler.RemoveEvent(eventHandle);
        }

        /// <summary>
        /// 设置指定事件的注释
        /// </summary>
        /// <param name="eventHandle">事件对象</param>
        /// <param name="comment">事件注释</param>
        public static void SetEventComment(object eventHandle, String comment)
        {
            Handler.SetEventComment(eventHandle, comment);
        }

        /// <summary>
        /// 切换至回放模式并开始回放
        /// </summary>
        /// <param name="startTimeline">回放开始时间，单位秒</param>
        /// <param name="interestTarget">目标兴趣点，单位秒（空表示不设置兴趣点）</param>
        public static void StartReplay(double startTimeline, double? interestTarget)
        {
            Handler.StartReplay(startTimeline, interestTarget);
        }

        /// <summary>
        /// (api:app=2.3.0) 切换至回放模式并开始回放
        /// </summary>
        /// <param name="force">是否强制开始，强制切换模式可能等候相当长时间</param>
        /// <param name="startTimeline">回放开始时间，单位秒</param>
        /// <param name="interestTarget">目标兴趣点，单位秒（空表示不设置兴趣点）</param>
        /// <returns>是否成功</returns>
        public static bool StartReplay(bool force, double startTimeline, double? interestTarget)
        {
            return Handler.StartReplay(force, startTimeline, interestTarget);
        }

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

        /// <summary>
        /// 停止采集、处理、回放
        /// </summary>
        public static void StopRunning()
        {
            Handler.StopRunning();
        }

        /// <summary>
        /// 停止采集、处理、回放
        /// </summary>
        /// <param name="controllerName">控制者名称，用于独占控制模式</param>
        /// <returns>是否成功</returns>
        public static bool StopRunning(String controllerName)
        {
            return Handler.StopRunning(controllerName);
        }

        /// <summary>
        /// 停止采集、处理、回放
        /// </summary>
        /// <param name="force">是否强制结束</param>
        /// <param name="editRecordedSession">成功停止后是否立即编辑session信息</param>
        /// <returns>是否成功</returns>
        public static bool StopRunning(bool force, bool editRecordedSession)
        {
            return Handler.StopRunning(force, editRecordedSession);
        }

        /// <summary>
        /// 获取指定session开始时间对应的时间点
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>时间点，session不存在或不属于当前层级则返回null</returns>
        public static double? GetSessionTimeline(DateTime session)
        {
            return Handler.GetSessionTimeline(session);
        }

        /// <summary>
        /// 获取指定session的长度
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session长度，单位秒，session不存在或不属于当前层级则返回null</returns>
        public static double? GetSessionLength(DateTime session)
        {
            return Handler.GetSessionLength(session);
        }

        /// <summary>
        /// 获取图表报告ID列表
        /// </summary>
        /// <returns>图表报告ID列表</returns>
        public static int[] GetGraphIDList()
        {
            return Handler.GetGraphIDList();
        }

        /// <summary>
        /// 获取指定图表报告标题的报告ID
        /// </summary>
        /// <param name="title">图表报告标题</param>
        /// <returns>图表报告ID</returns>
        public static int? GetGraphIDWithTitle(String title)
        {
            return Handler.GetGraphIDWithTitle(title);
        }

        /// <summary>
        /// 获取指定ID图表的标题
        /// </summary>
        /// <param name="id">图表报告ID</param>
        /// <returns>图表报告标题，若不存在则返回null</returns>
        public static String GetGraphTitle(int id)
        {
            return Handler.GetGraphTitle(id);
        }

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

        /// <summary>
        /// 获取指定ID的总线信号的信息
        /// </summary>
        /// <param name="busSignalID">总线信号ID</param>
        /// <returns>总线信号的信息，信号不存在或信号非总线信号则返回null</returns>
        public static BusSignalInfo GetBusSignalInfo(String busSignalID)
        {
            return Handler.GetBusSignalInfo(busSignalID);
        }

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

        /// <summary>
        /// 获取视频通道的特殊摄像头类型
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <returns>特殊摄像头类型</returns>
        public static Samples.SpecialCameraType GetVideoSpecialType(int channel)
        {
            return Handler.GetVideoSpecialType(channel);
        }

        /// <summary>
        /// 将时间线上的时间转换为在session中的时间
        /// </summary>
        /// <param name="timeline">时间点</param>
        /// <returns>在session中的时间，若超出范围则返回null</returns>
        public static TimeWithSession ConvertTimeIntoSession(double timeline)
        {
            return  Handler.ConvertTimeIntoSession(timeline);
        }

        /// <summary>
        /// 获取当前层级下筛选后的所有session的时长总长
        /// </summary>
        /// <returns>筛选后的所有session的时长总长</returns>
        public static double GetFilteredSessionListTotalLength()
        {
            return Handler.GetFilteredSessionListTotalLength();
        }

        /// <summary>
        /// 获取所有场景ID列表
        /// </summary>
        /// <returns>场景ID列表</returns>
        public static String[] GetSceneIDList()
        {
            return Handler.GetSceneIDList();
        }

        /// <summary>
        /// 获取场景标题表
        /// </summary>
        /// <returns>场景标题表</returns>
        public static Dictionary<String, SceneTitle> GetSceneTitleTable()
        {
            return Handler.GetSceneTitleTable();
        }

        /// <summary>
        /// 获取所有通道的数据状态
        /// </summary>
        /// <param name="tolerance">可容忍最近多少毫秒（现实时间）无数据</param>
        /// <returns>返回各通道的数据状态，key为通道ID</returns>
        public static Dictionary<String, bool> GetChannelStatusTable(uint? tolerance)
        {
            return Handler.GetChannelStatusTable(tolerance);
        }

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

        /// <summary>
        /// 获取数据处理或C++模块组件配置的字符串描述
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , WindowPanel, ConfigPanel等</param>
        /// <param name="moduleClassID">模块类别ID</param>
        /// <returns>配置的字符串描述，null表示找不到类别ID对应的模块</returns>
        public static String GetModuleConfig(object caller, String moduleClassID)
        {
            return Handler.GetModuleConfig(caller, moduleClassID);
        }

        /// <summary>
        /// 设置数据处理或C++模块组件配置的字符串描述
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , WindowPanel, ConfigPanel等</param>
        /// <param name="moduleClassID">模块类别ID</param>
        /// <param name="config">配置的字符串描述</param>
        public static void SetModuleConfig(object caller, String moduleClassID, String config)
        {
            Handler.SetModuleConfig(caller, moduleClassID, config);
        }

        /// <summary>
        /// 获取数据处理或C++模块组件配置的状态
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , WindowPanel, ConfigPanel等</param>
        /// <param name="moduleClassID">模块类别ID</param>
        /// <returns>组件配置的状态，若找不到类别ID对应的模块则返回 ASEva.ConfigStatus.Disabled </returns>
        public static ConfigStatus GetModuleConfigStatus(object caller, String moduleClassID)
        {
            return Handler.GetModuleConfigStatus(caller, moduleClassID);
        }

        /// <summary>
        /// 获取数据处理或C++模块组件各子功能配置的状态
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , WindowPanel, ConfigPanel等</param>
        /// <param name="moduleClassID">模块类别ID</param>
        /// <returns>各子功能配置的状态，若找不到类别ID对应的模块或无子功能配置则返回null</returns>
        public static ConfigStatus[] GetModuleChildConfigStatus(object caller, String moduleClassID)
        {
            return Handler.GetModuleChildConfigStatus(caller, moduleClassID);
        }

        /// <summary>
        /// 获取总线设备列表
        /// </summary>
        /// <returns>总线设备列表，键为设备ID，值为对应的设备信息</returns>
        public static Dictionary<BusDeviceID, BusDeviceInfo> GetBusDevices()
        {
            return Handler.GetBusDevices();
        }

        /// <summary>
        /// 获取视频设备列表
        /// </summary>
        /// <returns>视频设备列表，键为设备ID，值为对应的设备信息</returns>
        public static Dictionary<VideoDeviceID, VideoDeviceInfo> GetVideoDevices()
        {
            return Handler.GetVideoDevices();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取总线协议文件ID列表
        /// </summary>
        /// <returns>总线协议文件ID列表</returns>
        public static BusProtocolFileID[] GetBusProtocolFileIDList()
        {
            return Handler.GetBusProtocolFileIDList();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取总线协议文件路径
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        /// <returns>总线协议文件路径，若未找到返回null</returns>
        public static String GetBusProtocolFilePath(BusProtocolFileID fileID)
        {
            return Handler.GetBusProtocolFilePath(fileID);
        }

        /// <summary>
        /// (api:app=2.3.0) 更新总线协议文件路径
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        /// <param name="filePath">新路径</param>
        /// <returns>是否成功更新，false表示未找到文件或MD5不匹配</returns>
        public static bool UpdateBusProtocolFilePath(BusProtocolFileID fileID, String filePath)
        {
            return Handler.UpdateBusProtocolFilePath(fileID, filePath);
        }

        /// <summary>
        /// 获取总线协议文件的状态
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        /// <returns>文件状态</returns>
        public static BusProtocolFileState GetBusProtocolFileState(BusProtocolFileID fileID)
        {
            return Handler.GetBusProtocolFileState(fileID);
        }

        /// <summary>
        /// (api:app=2.3.0) 添加新的总线协议文件
        /// </summary>
        /// <param name="filePath">总线协议文件路径</param>
        /// <param name="fileID">总线协议文件ID，若文件存在则输出该ID，否则输出null</param>
        /// <returns>是否为新添加的总线协议文件ID</returns>
        public static bool AddBusProtocolFile(String filePath, out BusProtocolFileID fileID)
        {
            return Handler.AddBusProtocolFile(filePath, out fileID);
        }

        /// <summary>
        /// (api:app=2.3.0) 移除总线协议文件
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        public static void RemoveBusProtocolFile(BusProtocolFileID fileID)
        {
            Handler.RemoveBusProtocolFile(fileID);
        }

        /// <summary>
        /// 获取C++插件模块版本列表
        /// </summary>
        /// <param name="prefix">组件前缀，如bus、video、proc、dev等</param>
        /// <returns>版本列表，键为C++模块的类型ID，值为版本字符串</returns>
        public static Dictionary<String, String> GetNativePluginVersions(String prefix)
        {
            return Handler.GetNativePluginVersions(prefix);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取C++插件模块版本列表
        /// </summary>
        /// <param name="type">C++库类别</param>
        /// <returns>版本列表，键为C++模块的类型ID</returns>
        public static  Dictionary<String, Version> GetNativePluginVersions(NativeLibraryType type)
        {
            return Handler.GetNativePluginVersions(type);
        }

        /// <summary>
        /// 弹出对话框配置离线地图路径
        /// </summary>
        public static void ConfigOfflineMapPath()
        {
            Handler.ConfigOfflineMapPath();
        }

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

        /// <summary>
        /// (api:app=2.3.0) 获取离线地图图像
        /// </summary>
        /// <param name="imageSize">指定图像大小</param>
        /// <param name="centerLocation">图像中心的经纬度</param>
        /// <param name="zoom">图像的尺度，0~24</param>
        /// <returns>离线地图图像（通用图像数据），空表示获取失败</returns>
        public static CommonImage GetOfflineMapCommonImage(IntSize imageSize, LocPoint centerLocation, int zoom)
        {
            return Handler.GetOfflineMapCommonImage(imageSize, centerLocation, zoom);
        }

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

        /// <summary>
        /// 打开对话框选择总线报文
        /// </summary>
        /// <param name="originMessageID">初始总线报文配置</param>
        /// <returns>返回总线报文配置，若删除则返回null</returns>
        public static String SelectBusMessage(String originMessageID)
        {
            return Handler.SelectBusMessage(originMessageID);
        }

        /// <summary>
        /// 创建视频帧获取器，可指定更多参数，以及获取高清视频帧等
        /// </summary>
        /// <returns>视频帧获取器</returns>
        public static VideoFrameGetter CreateVideoFrameGetter()
        {
            return Handler.CreateVideoFrameGetter();
        }

        /// <summary>
        /// 打开对话框选择多个信号
        /// </summary>
        /// <param name="handler">选中信号时调用的回调接口</param>
        /// <param name="existSignalIDList">既存的选中信号ID列表</param>
        public static void SelectSignals(SelectSignalHandler handler, List<String> existSignalIDList)
        {
            Handler.SelectSignals(handler, existSignalIDList);
        }

        /// <summary>
        /// 打开对话框选择作为32位浮点解析的总线信号
        /// </summary>
        /// <param name="existSignalIDList">既存的信号列表</param>
        /// <returns>新选择的总线信号ID</returns>
        public static List<String> SelectBusFloat32Signal(List<String> existSignalIDList)
        {
            return Handler.SelectBusFloat32Signal(existSignalIDList);
        }

        /// <summary>
        /// 打开对话框
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , WindowPanel, ConfigPanel等</param>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="config">初始化配置</param>
        public static void OpenDialog(object caller, String dialogClassID, String config)
        {
            Handler.OpenDialog(caller, dialogClassID, config);
        }

        /// <summary>
        /// 添加窗口至工作空间
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , WindowPanel, ConfigPanel等</param>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <param name="config">初始化配置</param>
        /// <param name="newWorkspaceIfNeeded">如果当前工作空间位置不足，是否添加至新工作空间</param>
        public static void AddWindow(object caller, String windowClassID, String config, bool newWorkspaceIfNeeded)
        {
            Handler.AddWindow(caller, windowClassID, config, newWorkspaceIfNeeded);
        }

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

        /// <summary>
        /// 设置当前对话框的标题与图标
        /// </summary>
        /// <param name="title">标题，若null则不更改</param>
        /// <param name="icon">图标，若null则不更改</param>
        public static void SetCurrentDialogTitle(String title, object icon)
        {
            Handler.SetCurrentDialogTitle(title, icon);
        }

        /// <summary>
        /// 打开对话框选择总线协议文件（可多个）
        /// </summary>
        /// <param name="selected">已选择的总线协议文件</param>
        /// <returns>新选择的总线协议文件</returns>
        public static BusProtocolFileID[] SelectBusProtocolFiles(BusProtocolFileID[] selected)
        {
            return Handler.SelectBusProtocolFiles(selected);
        }

        /// <summary>
        /// 打开对话框配置文件加密选项
        /// </summary>
        public static void ConfigDataEncryption()
        {
            Handler.ConfigDataEncryption();
        }

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

        /// <summary>
        /// 获取所有已注册的音频驱动信息
        /// </summary>
        /// <returns>已注册的音频驱动信息列表，若未注册任何有效驱动则返回null</returns>
        public static AudioDriverInfo[] GetAudioDrivers()
        {
            return Handler.GetAudioDrivers();
        }

        /// <summary>
        /// 获取指定驱动下的音频采集设备信息列表
        /// </summary>
        /// <param name="driverID">音频驱动ID</param>
        /// <returns>音频采集设备信息列表，若无该驱动或驱动下无采集设备则返回null</returns>
        public static AudioDeviceInfo[] GetAudioRecordDevices(String driverID)
        {
            return Handler.GetAudioRecordDevices(driverID);
        }

        /// <summary>
        /// 获取指定驱动下的音频回放设备信息列表
        /// </summary>
        /// <param name="driverID">音频驱动ID</param>
        /// <returns>音频回放设备信息列表，若无该驱动或驱动下无回放设备则返回null</returns>
        public static AudioDeviceInfo[] GetAudioReplayDevices(String driverID)
        {
            return Handler.GetAudioReplayDevices(driverID);
        }
        
        /// <summary>
        /// (api:app=2.0.6) 获取CPU时间
        /// </summary>
        /// <returns>CPU时间，单位秒，返回0表示无效</returns>
        public static double GetCPUTime()
        {
            return Handler.GetCPUTime();
        }

        /// <summary>
        /// (api:app=2.2.3) 获取事件类型名称列表（包括未启用的） 
        /// </summary>
        /// <returns>事件类型名称列表</returns>
        public static String[] GetEventTypeNames()
        {
            return Handler.GetEventTypeNames();
        }

        /// <summary>
        /// (api:app=2.2.3) 解析总线报文，获取信号值等信息
        /// </summary>
        /// <param name="busMessage">总线报文</param>
        /// <returns>所有信号值及相关信息</returns>
        public static BusSignalValue[] ParseBusMessage(BusMessageSample busMessage)
        {
            return Handler.ParseBusMessage(busMessage);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取最近项目文件路径列表
        /// </summary>
        /// <returns>最近项目文件路径列表</returns>
        public static String[] GetRecentProjectPathes()
        {
            return Handler.GetRecentProjectPathes();
        }

        /// <summary>
        /// (api:app=2.3.0) 尝试终止应用程序
        /// </summary>
        /// <param name="force">是否强制终止</param>
        /// <param name="autosave">是否保存当前工程至autosave</param>
        /// <returns>是否成功终止</returns>
        public static bool TerminateApp(bool force, bool autosave)
        {
            return Handler.TerminateApp(force, autosave);
        }
        
        /// <summary>
        /// (api:app=2.3.0) 弹出模态框显示错误信息
        /// </summary>
        /// <param name="msg">错误信息</param>
        public static void PopupError(String msg)
        {
            Handler.PopupError(msg);
        }
        
        /// <summary>
        /// (api:app=2.3.0) 弹出模态框显示提示信息
        /// </summary>
        /// <param name="msg">提示信息</param>
        public static void PopupNotice(String msg)
        {
            Handler.PopupNotice(msg);
        }
        
        /// <summary>
        /// (api:app=2.3.0) 弹出模态框显示确认信息
        /// </summary>
        /// <param name="msg">确认信息</param>
        /// <returns>是否得到确认</returns>
        public static bool PopupConfirm(String msg)
        {
            return Handler.PopupConfirm(msg);
        }

        /// <summary>
        /// (api:app=2.3.0) 添加主线程检查点
        /// </summary>
        /// <param name="location">主线程检查点位置</param>
        public static void AddMainThreadCheckpoint(String location)
        {
            Handler.AddMainThreadCheckpoint(location);
        }

        /// <summary>
        /// (api:app=2.3.0) 新建项目
        /// </summary>
        /// <param name="force">是否强制新建项目</param>
        /// <returns>是否成功新建项目</returns>
        public static bool NewProject(bool force)
        {
            return Handler.NewProject(force);
        }

        /// <summary>
        /// (api:app=2.3.0) 打开新项目
        /// </summary>
        /// <param name="projectFile">项目文件路径，若设为null则从autosave读取</param>
        /// <returns>是否成功打开项目</returns>
        public static bool OpenProject(String projectFile)
        {
            return Handler.OpenProject(projectFile);
        }

        /// <summary>
        /// (api:app=2.3.0) 保存当前项目
        /// </summary>
        /// <param name="projectFile">项目文件路径，null表示保存至当前项目文件</param>
        /// <returns>是否成功保存项目</returns>
        public static bool SaveCurrentProject(String projectFile)
        {
            return Handler.SaveCurrentProject(projectFile);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取当前项目文件
        /// </summary>
        /// <returns>当前项目文件，新项目或从autosave读取的项目都为null</returns>
        public static String GetCurrentProject()
        {
            return Handler.GetCurrentProject();
        }

        /// <summary>
        /// (api:app=2.3.0) 播放MP3音频
        /// </summary>
        /// <param name="mp3FileData">MP3音频文件数据</param>
        public static void PlayMp3(byte[] mp3FileData)
        {
            Handler.PlayMp3(mp3FileData);
        }

        /// <summary>
        /// (api:app=2.3.0) 启动进程以默认方式打开文件、文件夹或网址
        /// </summary>
        /// <param name="target">目标文件、文件夹或网址</param>
        /// <returns>是否成功打开</returns>
        public static bool StartProcess(String target)
        {
            return Handler.StartProcess(target);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取软件版本信息总表
        /// </summary>
        /// <returns>软件版本信息总表</returns>
        public static Dictionary<String, Version> GetVersionTable()
        {
            return Handler.GetVersionTable();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取系统状态信息
        /// </summary>
        /// <param name="status">系统状态类别</param>
        /// <returns>系统状态信息，若无有效信息则返回null</returns>
        public static String GetSystemStatus(SystemStatus status)
        {
            return Handler.GetSystemStatus(status);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取系统状态详情
        /// </summary>
        /// <param name="status">系统状态类别</param>
        /// <returns>系统状态详情，若无有效信息则返回null</returns>
        public static String GetSystemStatusDetails(SystemStatus status)
        {
            return Handler.GetSystemStatusDetails(status);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取被许可的功能序号列表
        /// </summary>
        /// <returns>被许可的功能序号列表</returns>
        public static int[] GetLicensedFunctionIndices()
        {
            return Handler.GetLicensedFunctionIndices();
        }

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

        /// <summary>
        /// (api:app=2.3.0) 设置数据目录的路径
        /// </summary>
        /// <param name="path">数据目录的路径</param>
        public static void SetDataPath(String path)
        {
            Handler.SetDataPath(path);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取窗口组件信息表
        /// </summary>
        /// <returns>窗口组件信息表，键为组件ID</returns>
        public static Dictionary<String, WindowClassInfo> GetWindowClassTable()
        {
            return Handler.GetWindowClassTable();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取对话框组件信息表
        /// </summary>
        /// <returns>对话框组件信息表，键为组件ID</returns>
        public static Dictionary<String, DialogClassInfo> GetDialogClassTable()
        {
            return Handler.GetDialogClassTable();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取数据处理组件信息表
        /// </summary>
        /// <returns>数据处理组件信息表，键为组件ID</returns>
        public static Dictionary<String, ProcessorClassInfo> GetProcessorClassTable()
        {
            return Handler.GetProcessorClassTable();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取C++组件信息表
        /// </summary>
        /// <returns>C++组件信息表，键为组件ID</returns>
        public static Dictionary<String, NativeClassInfo> GetNativeClassTable()
        {
            return Handler.GetNativeClassTable();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取独立任务组件信息表
        /// </summary>
        /// <returns>独立任务组件信息表，键为组件ID</returns>
        public static Dictionary<String, TaskClassInfo> GetTaskClassTable()
        {
            return Handler.GetTaskClassTable();
        }

        /// <summary>
        /// (api:app=2.3.0) 注册分化窗口组件
        /// </summary>
        /// <param name="windowClassID">原窗口组件ID</param>
        /// <param name="config">用于分化的配置字符串</param>
        /// <returns>分化后的窗口组件信息</returns>
        public static WindowClassInfo RegisterTransformWindowClass(String windowClassID, String config)
        {
            return Handler.RegisterTransformWindowClass(windowClassID, config);
        }

        /// <summary>
        /// (api:app=2.3.0) 注册分化对话框组件
        /// </summary>
        /// <param name="dialogClassID">原对话框组件ID</param>
        /// <param name="config">用于分化的配置字符串</param>
        /// <returns>分化后的对话框组件信息</returns>
        public static DialogClassInfo RegisterTransformDialogClass(String dialogClassID, String config)
        {
            return Handler.RegisterTransformDialogClass(dialogClassID, config);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取对话框相关模块配置状态
        /// </summary>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="transformID">分化ID</param>
        /// <param name="childrenStatus">子配置状态</param>
        /// <returns>配置状态</returns>
        public static ConfigStatus GetDialogRelatedModulesConfigStatus(String dialogClassID, String transformID, out ConfigStatus[] childrenStatus)
        {
            return Handler.GetDialogRelatedModulesConfigStatus(dialogClassID, transformID, out childrenStatus);
        }

        /// <summary>
        /// (api:app=2.3.0) 禁用所有模块配置
        /// </summary>
        public static void DisableAllConfigs()
        {
            Handler.DisableAllConfigs();
        }

        /// <summary>
        /// (api:app=2.4.2) 获取当前层级下指定session下的所有generation ID
        /// </summary>
        /// <param name="sessionID">Session ID</param>
        /// <returns>Generation ID列表</returns>
        public static String[] GetSessionGenerations(DateTime sessionID)
        {
            return Handler.GetSessionGenerations(sessionID);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取当前层级下含有指定generation ID的所有session
        /// </summary>
        /// <param name="generationID">Generation ID</param>
        /// <returns>Session ID列表</returns>
        public static DateTime[] GetGenerationSessions(String generationID)
        {
            return Handler.GetGenerationSessions(generationID);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置目标回放速度
        /// </summary>
        /// <param name="speed">目标回放速度</param>
        public static void SetTargetReplaySpeed(double speed)
        {
            Handler.SetTargetReplaySpeed(speed);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置session搜索关键字
        /// </summary>
        /// <param name="keyword">session搜索关键字</param>
        public static void SetSessionSearchKeyword(String keyword)
        {
            Handler.SetSessionSearchKeyword(keyword);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取所有session的筛选标志位
        /// </summary>
        /// <returns>session的筛选标志位表</returns>
        public static Dictionary<DateTime, SessionFilterFlags> GetSessionFilterTable()
        {
            return Handler.GetSessionFilterTable();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取在线模式下正在预览或采集的session
        /// </summary>
        /// <returns>在线模式下正在预览或采集的session，若非在线模式则返回null</returns>
        public static DateTime? GetCurrentOnlineSession()
        {
            return Handler.GetCurrentOnlineSession();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取当前层级下所有session的总时长
        /// </summary>
        /// <returns>所有session的总时长</returns>
        public static double GetSessionListTotalLength()
        {
            return Handler.GetSessionListTotalLength();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取当前的session搜索关键字
        /// </summary>
        /// <returns></returns>
        public static String GetSessionSearchKey()
        {
            return Handler.GetSessionSearchKey();
        }

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

        /// <summary>
        /// (api:app=2.3.0) 设置session是否框选
        /// </summary>
        /// <param name="session">session ID</param>
        /// <param name="check">是否框选</param>
        public static void SetSessionChecker(DateTime session, bool check)
        {
            Handler.SetSessionChecker(session, check);
        }

        /// <summary>
        /// (api:app=2.3.0) 移除generation及相关文件至回收站
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="genID">Generation ID</param>
        public static void RemoveGeneration(DateTime session, String genID)
        {
            Handler.RemoveGeneration(session, genID);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取session的注释说明
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session的注释说明</returns>
        public static String GetSessionComment(DateTime session)
        {
            return Handler.GetSessionComment(session);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置session的注释说明
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="comment">Session的注释说明</param>
        public static void SetSessionComment(DateTime session, String comment)
        {
            Handler.SetSessionComment(session, comment);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取session的属性表
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <returns>Session的属性表</returns>
        public static Dictionary<String, String> GetSessionProperties(DateTime session)
        {
            return Handler.GetSessionProperties(session);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置session的属性表
        /// </summary>
        /// <param name="session">Session ID</param>
        /// <param name="properties">Session的属性表</param>
        public static void SetSessionProperties(DateTime session, Dictionary<String, String> properties)
        {
            Handler.SetSessionProperties(session, properties);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置音量
        /// </summary>
        /// <param name="volume">音量（倍数）</param>
        public static void SetAudioVolume(double volume)
        {
            Handler.SetAudioVolume(volume);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置CPU使用率的乘数
        /// </summary>
        /// <param name="scale">CPU使用率的乘数</param>
        public static void SetCPURateScale(int scale)
        {
            Handler.SetCPURateScale(scale);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取许可证的详细信息
        /// </summary>
        /// <returns>许可证的详细信息</returns>
        public static String GetLicenseInfo()
        {
            return Handler.GetLicenseInfo();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取原始数据通道延迟配置
        /// </summary>
        /// <param name="id">原始数据通道ID</param>
        /// <returns>延迟配置，单位毫秒</returns>
        public static double GetRawChannelDelayConfig(String id)
        {
            return Handler.GetRawChannelDelayConfig(id);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取总线数据通道延迟配置
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <returns>延迟配置，单位毫秒</returns>
        public static double GetBusChannelDelayConfig(int channel)
        {
            return Handler.GetBusChannelDelayConfig(channel);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取视频数据通道延迟配置
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <returns>延迟配置，单位毫秒</returns>
        public static double GetVideoChannelDelayConfig(int channel)
        {
            return Handler.GetVideoChannelDelayConfig(channel);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取音频数据通道延迟配置
        /// </summary>
        /// <returns>延迟配置，单位毫秒</returns>
        public static double GetAudioChannelDelayConfig()
        {
            return Handler.GetAudioChannelDelayConfig();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取所有原始数据通道延迟配置
        /// </summary>
        /// <returns>所有原始数据通道的延迟配置，单位毫秒</returns>
        public static Dictionary<string, double> GetAllRawChannelDelayConfigs()
        {
            return Handler.GetAllRawChannelDelayConfigs();
        }

        /// <summary>
        /// (api:app=2.3.0) 设置原始数据通道延迟配置
        /// </summary>
        /// <param name="id">原始数据通道ID</param>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static void SetRawChannelDelayConfig(String id, double delay)
        {
            Handler.SetRawChannelDelayConfig(id, delay);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置总线数据通道延迟配置
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static void SetBusChannelDelayConfig(int channel, double delay)
        {
            Handler.SetBusChannelDelayConfig(channel, delay);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置视频数据通道延迟配置
        /// </summary>
        /// <param name="channel">视频通道，0~23</param>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static void SetVideoChannelDelayConfig(int channel, double delay)
        {
            Handler.SetVideoChannelDelayConfig(channel, delay);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置音频数据通道延迟配置
        /// </summary>
        /// <param name="delay">延迟配置，单位毫秒</param>
        public static void SetAudioChannelDelayConfig(double delay)
        {
            Handler.SetAudioChannelDelayConfig(delay);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取指定通道的监控配置
        /// </summary>
        /// <param name="id">通道ID</param>
        /// <returns>是否监控</returns>
        public static bool GetChannelMonitoringFlag(String id)
        {
            return Handler.GetChannelMonitoringFlag(id);
        }

        /// <summary>
        /// (api:app=2.3.0) 设置指定通道的监控配置
        /// </summary>
        /// <param name="id">通道ID</param>
        /// <param name="monitoring">是否监控，通道监控的具体实现应由插件给出，如发出报警音、指示灯闪烁等</param>
        public static void SetChannelMonitoringFlag(String id, bool monitoring)
        {
            Handler.SetChannelMonitoringFlag(id, monitoring);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取所有正在监控的通道ID
        /// </summary>
        /// <returns>正在监控的通道ID列表</returns>
        public static string[] GetAllChannelMonitoringKeys()
        {
            return Handler.GetAllChannelMonitoringKeys();
        }

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

        /// <summary>
        /// (api:app=2.3.0) 获取原始数据通道状态
        /// </summary>
        /// <param name="protocol">原始数据通道ID</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <returns>是否有数据</returns>
        public static bool GetRawChannelStatus(String protocol, uint? toleranceMillisecond)
        {
            return Handler.GetRawChannelStatus(protocol, toleranceMillisecond);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取样本数据通道状态
        /// </summary>
        /// <param name="protocol">样本数据通道ID</param>
        /// <param name="toleranceMillisecond">无数据的容忍时长</param>
        /// <param name="interval">输出每帧数据之间的时间间隔曲线，单位秒</param>
        /// <param name="delay">输出每帧数据的延迟曲线，单位秒</param>
        /// <returns>是否有数据</returns>
        public static bool GetSampleChannelStatus(String protocol, uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            return Handler.GetSampleChannelStatus(protocol, toleranceMillisecond, out interval, out delay);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取所有有效的总线通道
        /// </summary>
        /// <returns>有效的总线通道列表，值为1~16</returns>
        public static int[] GetAvailableBusChannels()
        {
            return Handler.GetAvailableBusChannels();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取所有有效的视频通道
        /// </summary>
        /// <returns>有效的视频通道列表，值为0~23</returns>
        public static int[] GetAvailableVideoChannels()
        {
            return Handler.GetAvailableVideoChannels();   
        }

        /// <summary>
        /// (api:app=2.3.0) 获取所有有效的原始数据通道
        /// </summary>
        /// <returns>有效的原始数据通道列表，值为通道ID</returns>
        public static String[] GetAvailableRawChannels()
        {
            return Handler.GetAvailableRawChannels();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取所有有效的样本数据通道
        /// </summary>
        /// <returns>有效的样本数据通道列表，值为通道ID</returns>
        public static String[] GetAvailableSampleChannels()
        {
            return Handler.GetAvailableSampleChannels();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取指定总线通道的负载百分比
        /// </summary>
        /// <param name="channel">总线通道，1~16</param>
        /// <returns>总线通道的负载百分比，若无效则返回null</returns>
        public static double? GetBusPayloadPercentage(int channel)
        {
            return Handler.GetBusPayloadPercentage(channel);
        }

        /// <summary>
        /// (api:app=2.3.0) 创建窗口对象
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , WindowPanel, ConfigPanel等</param>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <param name="transformID">分化ID，null表示不分化</param>
        /// <param name="panel">新建的窗口对象，创建失败则为null</param>
        /// <param name="info">新建窗口的组件信息，创建失败则为null</param>
        /// <returns>创建结果，若成功则在释放窗口时需要调用 ASEva.Agency.UnregisterPanel </returns>
        public static CreatePanelResult CreateWindowPanel(object caller, String windowClassID, String transformID, out object panel, out WindowClassInfo info)
        {
            return Handler.CreateWindowPanel(caller, windowClassID, transformID, out panel, out info);
        }

        /// <summary>
        /// (api:app=2.3.0) 创建对话框对象
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.MainWorkflow , ASEva.WindowClass , ASEva.DialogClass , WindowPanel, ConfigPanel等</param>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="transformID">分化ID，null表示不分化</param>
        /// <param name="panel">新建的对话框对象，创建失败则为null</param>
        /// <param name="info">新建对话框的组件信息，创建失败则为null</param>
        /// <returns>创建结果，若成功则在释放窗口时需要调用 ASEva.Agency.UnregisterPanel </returns>
        public static CreatePanelResult CreateConfigPanel(object caller, String dialogClassID, String transformID, out object panel, out DialogClassInfo info)
        {
            return Handler.CreateConfigPanel(caller, dialogClassID, transformID, out panel, out info);
        }

        /// <summary>
        /// (api:app=2.3.0) 注销窗口或对话框对象
        /// </summary>
        /// <param name="panel">窗口或对话框对象</param>
        public static void UnregisterPanel(object panel)
        {
            Handler.UnregisterPanel(panel);
        }

        /// <summary>
        /// (api:app=2.3.0) 转换平台特化图像对象至通用图像数据
        /// </summary>
        /// <param name="image">平台特化图像</param>
        /// <returns>通用图像数据，转换失败则返回null</returns>
        public static CommonImage ConvertImageToCommon(object image)
        {
            return Handler.ConvertImageToCommon(image);
        }

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

        /// <summary>
        /// (api:app=2.3.0) 获取窗口组件信息
        /// </summary>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <returns>窗口组件信息，若未找到返回null</returns>
        public static WindowClassInfo GetWindowClassInfo(String windowClassID)
        {
            return Handler.GetWindowClassInfo(windowClassID);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取对话框组件信息
        /// </summary>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <returns>对话框组件信息，若未找到返回null</returns>
        public static DialogClassInfo GetDialogClassInfo(String dialogClassID)
        {
            return Handler.GetDialogClassInfo(dialogClassID);
        }

        /// <summary>
        /// (api:app=2.3.0) 解码图像数据
        /// </summary>
        /// <param name="imageData">JPG或PNG二进制数据</param>
        /// <returns>解码后的通用图像数据，若失败则返回null</returns>
        public static CommonImage DecodeImage(byte[] imageData)
        {
            return Handler.DecodeImage(imageData);
        }

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

        /// <summary>
        /// (api:app=2.3.0) 获取信号树
        /// </summary>
        /// <returns>信号树根节点下的所有子节点</returns>
        public static SignalTreeNode[] GetSignalTree()
        {
            return Handler.GetSignalTree();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取插件包ID列表
        /// </summary>
        /// <returns>插件包ID列表</returns>
        public static String[] GetPluginPackIDList()
        {
            return Handler.GetPluginPackIDList();
        }

        /// <summary>
        /// (api:app=2.3.0) 获取插件包信息
        /// </summary>
        /// <param name="packID">插件包ID</param>
        /// <returns>插件包信息，若无对应插件包则返回null</returns>
        public static PluginPackInfo GetPluginPackInfo(String packID)
        {
            return Handler.GetPluginPackInfo(packID);
        }

        /// <summary>
        /// (api:app=2.3.0) 安装插件（安装完毕后也需要重启才生效）
        /// </summary>
        /// <param name="dirPath">插件包文件夹，或包含若干插件包的文件夹</param>
        /// <returns>是否安装了插件</returns>
        public static bool InstallPlugin(String dirPath)
        {
            return Handler.InstallPlugin(dirPath);
        }

        /// <summary>
        /// (api:app=2.3.0) 卸载插件
        /// </summary>
        /// <param name="packID">插件包ID</param>
        /// <returns>是否卸载了插件</returns>
        public static bool UninstallPlugin(String packID)
        {
            return Handler.UninstallPlugin(packID);
        }

        /// <summary>
        /// (api:app=2.3.0) 获取所有清单信息
        /// </summary>
        /// <returns>清单信息列表</returns>
        public static LogMessage[] GetLogMessages()
        {
            return Handler.GetLogMessages();
        }

        /// <summary>
        /// (api:app=2.3.1) 返回是否允许进行保存工程项目和开始session等操作
        /// </summary>
        /// <returns>是否允许进行保存工程项目和开始session等操作</returns>
        public static bool IsReady()
        {
            return Handler.IsReady();
        }

        /// <summary>
        /// (api:app=2.3.4) 禁用插件
        /// </summary>
        /// <param name="packID">插件包ID</param>
        public static void DisablePlugin(String packID)
        {
            Handler.DisablePlugin(packID);
        }

        /// <summary>
        /// (api:app=2.3.4) 启用插件（需要重启应用程序后生效）
        /// </summary>
        /// <param name="packID">插件包ID</param>
        public static void EnablePlugin(String packID)
        {
            Handler.EnablePlugin(packID);
        }
    }
}
