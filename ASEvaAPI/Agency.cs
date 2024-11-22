using System;
using System.Collections.Generic;
using System.Linq;
using ASEva.Samples;
using ASEva.Utility;

namespace ASEva
{
    /// \~English
    /// <summary>
    /// (api:app=3.7.3) Wrap ASEva.AgencyLocal and ASEva.AgencyAsync, you can use this class if neither considering cross-end nor using Avalonia
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.3) 打包了 ASEva.AgencyLocal 和 ASEva.AgencyAsync ，不考虑跨端且不使用Avalonia时可直接使用此类
    /// </summary>
    public class Agency
    {
        public static AddBusProtocolResult AddBusProtocolFile(String filePath, out BusProtocolFileID[] fileIDs)
        {
            return AgencyLocal.AddBusProtocolFile(filePath, out fileIDs);
        }

        public static void AddDataLayer(String layer)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.AddDataLayer(layer).Wait();
        }

        public static void AddMainThreadCheckpoint(String location)
        {
            AgencyLocal.AddMainThreadCheckpoint(location);
        }

        public static void AddProcessorVideoReference(int videoChannel)
        {
            AgencyLocal.AddProcessorVideoReference(videoChannel);
        }

        public static void AddSceneData(SceneData scene)
        {
            AgencyLocal.AddSceneData(scene);
        }

        public static void AddSignalReference(String signalID)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.AddSignalReference(signalID).Wait();
        }

        public static void AddWindow(object caller, String windowClassID, String config, bool newWorkspaceIfNeeded)
        {
            AgencyLocal.AddWindow(caller, windowClassID, config, newWorkspaceIfNeeded);
        }

        public static byte[]? CallNativeFunction(object caller, String nativeClassID, String funcID, byte[]? input)
        {
            return AgencyLocal.CallNativeFunction(caller, nativeClassID, funcID, input);
        }

        public static void CallWebApi(String request, WebApiContext context)
        {
            AgencyLocal.CallWebApi(request, context);
        }

        public static void CallWebApiPost(String request, byte[] body, WebPostContentType contentType, WebApiContext context)
        {
            AgencyLocal.CallWebApiPost(request, body, contentType, context);
        }

        public static void ConfigDataEncryption()
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyLocal.ConfigDataEncryption().Wait();
        }

        public static void ConfigOfflineMapPath()
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyLocal.ConfigOfflineMapPath().Wait();
        }

        public static FloatPoint ConvertOfflineMapLocToPix(LocPoint origin, int zoom, LocPoint point)
        {
            return AgencyLocal.ConvertOfflineMapLocToPix(origin, zoom, point);
        }

        public static LocPoint ConvertOfflineMapPixToLoc(LocPoint origin, int zoom, FloatPoint pixel)
        {
            return AgencyLocal.ConvertOfflineMapPixToLoc(origin, zoom, pixel);
        }

        public static TimeWithSession? ConvertTimeIntoSession(double timeline)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.ConvertTimeIntoSession(timeline).Result;
        }

        public static CreatePanelResult CreateConfigPanel(object caller, String dialogClassID, String? transformID, out object? panel, out DialogClassInfo? info)
        {
            return AgencyLocal.CreateConfigPanel(caller, dialogClassID, transformID, out panel, out info);
        }

        public static GraphPanel? CreateGraphPanelForType(GraphType graphType, String? styleName)
        {
            return AgencyLocal.CreateGraphPanelForType(graphType, styleName);
        }

        public static GraphPanel? CreateGraphPanelForID(int graphID, String? styleName)
        {
            return AgencyLocal.CreateGraphPanelForID(graphID, styleName);
        }

        public static CreatePanelResult CreateWindowPanel(object caller, String windowClassID, String? transformID, out object? panel, out WindowClassInfo? info)
        {
            return AgencyLocal.CreateWindowPanel(caller, windowClassID, transformID, out panel, out info);
        }

        public static CommonImage? DecodeImage(byte[] imageData)
        {
            return AgencyLocal.DecodeImage(imageData);
        }

        public static void DeleteDataLayer(String layer)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.DeleteDataLayer(layer).Wait();
        }

        public static bool DeleteToRecycleBin(String path)
        {
            return AgencyLocal.DeleteToRecycleBin(path);
        }

        public static byte[][] DequeueDataFromNative(object caller, String nativeClassID, String dataID)
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.DequeueDataFromNative(caller, nativeClassID, dataID).Result;
        }

        public static void DisableAllConfigs()
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.DisableAllConfigs().Wait();
        }

        public static void DisableAllPlugins()
        {
            AgencyLocal.DisableAllPlugins();
        }

        public static void DisableModule(object caller, String classID)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.DisableModule(caller, classID).Wait();
        }

        public static void DisablePlugin(String packID)
        {
            AgencyLocal.DisablePlugin(packID);
        }

        public static void EnablePlugin(String packID)
        {
            AgencyLocal.EnablePlugin(packID);
        }

        public static byte[]? EncodeImage(CommonImage image, String format)
        {
            return AgencyLocal.EncodeImage(image, format);
        }

        public static void EnqueueDataToNative(object caller, String nativeClassID, String dataID, byte[] data)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.EnqueueDataToNative(caller, nativeClassID, dataID, data).Wait();
        }

        public static string[] GetAllChannelGuestSyncKeys()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAllChannelGuestSyncKeys().Result ?? [];
        }

        public static string[] GetAllChannelMonitoringKeys()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAllChannelMonitoringKeys().Result ?? [];
        }

        public static String[] GetAllChannelServerSyncMonitoringKeys()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAllChannelServerSyncMonitoringKeys().Result ?? [];
        }

        public static Dictionary<String, DeviceStatusDetail> GetAllDeviceStatus()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAllDeviceStatus().Result ?? [];
        }

        public static Dictionary<string, double> GetAllRawChannelDelayConfigs()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAllRawChannelDelayConfigs().Result ?? [];
        }

        public static String GetAppFilesRoot()
        {
            return AgencyLocal.GetAppFilesRoot() ?? "";
        }

        public static ApplicationGUI GetAppGUI()
        {
            return AgencyLocal.GetAppGUI();
        }

        public static String GetAppID()
        {
            return AgencyLocal.GetAppID();
        }

        public static Language GetAppLanguage()
        {
            return AgencyLocal.GetAppLanguage();
        }

        public static ApplicationMode GetAppMode()
        {
            if (!AgencyAsync.SyncMode) return ApplicationMode.Unknown;
            return AgencyAsync.GetAppMode().Result;
        }

        public static ApplicationStatus GetAppStatus()
        {
            if (!AgencyAsync.SyncMode) return ApplicationStatus.Unknown;
            return AgencyAsync.GetAppStatus().Result;
        }

        public static double GetAudioChannelDelayConfig()
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetAudioChannelDelayConfig().Result;
        }

        public static bool GetAudioChannelStatus(uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            if (!AgencyAsync.SyncMode)
            {
                interval = [];
                delay = [];
                return false;
            }
            var result = AgencyAsync.GetAudioChannelStatus(toleranceMillisecond).Result;
            interval = result.Item2.ToList();
            delay = result.Item3.ToList();
            return result.Item1;
        }

        public static AudioDriverInfo[] GetAudioDrivers()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAudioDrivers().Result ?? [];
        }

        public static AudioDeviceInfo[]? GetAudioRecordDevices(String driverID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetAudioRecordDevices(driverID).Result;
        }

        public static AudioDeviceInfo[]? GetAudioReplayDevices(String driverID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetAudioReplayDevices(driverID).Result;
        }

        public static int[] GetAvailableBusChannels()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAvailableBusChannels().Result ?? [];
        }

        public static String[] GetAvailableRawChannels()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAvailableRawChannels().Result ?? [];
        }

        public static String[] GetAvailableSampleChannels()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAvailableSampleChannels().Result ?? [];
        }

        public static int[] GetAvailableVideoChannels()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetAvailableVideoChannels().Result ?? [];   
        }

        public static BufferRange GetBufferRange()
        {
            return AgencyLocal.GetBufferRange();
        }

        public static double GetBusChannelDelayConfig(int busChannel)
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetBusChannelDelayConfig(busChannel).Result;
        }

        public static BusChannelInfo[]? GetBusChannelsInfo(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetBusChannelsInfo(session).Result;
        }

        public static bool GetBusChannelStatus(int busChannel, uint? toleranceMillisecond)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.GetBusChannelStatus(busChannel, toleranceMillisecond).Result;
        }

        public static Dictionary<BusDeviceID, BusDeviceInfo> GetBusDevices()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetBusDevices().Result ?? [];
        }

        public static float GetBusMessageFPS(int busChannel, uint localID)
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetBusMessageFPS(busChannel, localID).Result;
        }

        public static BusMessageInfo? GetBusMessageInfoByLocalID(int busChannel, uint localID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetBusMessageInfoByLocalID(busChannel, localID).Result;
        }

        public static BusMessageInfo? GetBusMessageInfo(String busMessageID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetBusMessageInfo(busMessageID).Result;
        }

        public static double? GetBusPayloadPercentage(int busChannel)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetBusPayloadPercentage(busChannel).Result;
        }

        public static int? GetBusProtocolFileChannel(String protocolName)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetBusProtocolFileChannel(protocolName).Result;
        }

        public static BusProtocolFileID[] GetBusProtocolFileIDList()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetBusProtocolFileIDList().Result ?? [];
        }

        public static String? GetBusProtocolFilePath(BusProtocolFileID fileID)
        {
            return AgencyLocal.GetBusProtocolFilePath(fileID);
        }

        public static BusFileInfo[] GetBusProtocolFilesInfo()
        {
            return AgencyLocal.GetBusProtocolFilesInfo();
        }

        public static BusProtocolFileState GetBusProtocolFileState(BusProtocolFileID fileID)
        {
            if (!AgencyAsync.SyncMode) return BusProtocolFileState.Unknown;
            return AgencyAsync.GetBusProtocolFileState(fileID).Result;
        }

        public static BusSignalInfo? GetBusSignalInfo(String busSignalID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetBusSignalInfo(busSignalID).Result;
        }

        public static String? GetChannelAliasName(String channelID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetChannelAliasName(channelID).Result;
        }

        public static Dictionary<String, String> GetChannelAliasTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetChannelAliasTable().Result ?? [];
        }

        public static bool GetChannelGuestSyncFlag(String id)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.GetChannelGuestSyncFlag(id).Result;
        }

        public static Timestamp[]? GetChannelLatestTimestamps(String channelID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetChannelLatestTimestamps(channelID).Result;
        }

        public static bool GetChannelMonitoringFlag(String id)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.GetChannelMonitoringFlag(id).Result;
        }

        public static bool GetChannelServerSyncMonitoringFlag(String id)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.GetChannelServerSyncMonitoringFlag(id).Result;
        }

        public static Dictionary<String, bool> GetChannelStatusTable(uint? tolerance)
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetChannelStatusTable(tolerance).Result ?? [];
        }

        public static Dictionary<String, TimeOffsetSync> GetChannelSyncTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetChannelSyncTable().Result ?? [];
        }

        public static GeneralDeviceStatus[]? GetChildDeviceStatus(String id)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetChildDeviceStatus(id).Result;
        }

        public static String GetConfigFilesRoot()
        {
            return AgencyLocal.GetConfigFilesRoot() ?? "";
        }

        public static ConsoleClassInfo? GetConsoleClassInfo(String consoleClassID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetConsoleClassInfo(consoleClassID).Result;
        }

        public static Dictionary<string, ConsoleClassInfo> GetConsoleClassTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetConsoleClassTable().Result ?? [];
        }

        public static ConfigStatus GetConsoleRelatedModulesConfigStatus(String consoleClassID, out ConfigStatus[] childrenStatus)
        {
            if (!AgencyAsync.SyncMode)
            {
                childrenStatus = [];
                return ConfigStatus.Disabled;
            }
            var result = AgencyAsync.GetConsoleRelatedModulesConfigStatus(consoleClassID).Result;
            childrenStatus = result.Item2;
            return result.Item1;
        }

        public static ulong GetCPUTick()
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetCPUTick().Result;
        }

        public static ulong GetCPUTicksPerSecond()
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetCPUTicksPerSecond().Result;
        }

        public static double GetCPUTime()
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetCPUTime().Result;
        }

        public static CPUTimeModel? GetCPUTimeModel(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetCPUTimeModel(session).Result;
        }

        public static String? GetCurrentDataGeneration()
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetCurrentDataGeneration().Result;
        }

        public static String? GetCurrentDataLayer()
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetCurrentDataLayer().Result;
        }

        public static String? GetCurrentDataLayerPath()
        {
            return AgencyLocal.GetCurrentDataLayerPath();
        }

        public static SessionIdentifier? GetCurrentOnlineSession()
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetCurrentOnlineSession().Result;
        }

        public static String? GetCurrentProject()
        {
            return AgencyLocal.GetCurrentProject();
        }

        public static String? GetCurrentSessionGUID()
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetCurrentSessionGUID().Result;
        }

        public static String[] GetDataLayers()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetDataLayers().Result ?? [];
        }

        public static String? GetDataPath()
        {
            return AgencyLocal.GetDataPath();
        }

        public static Dictionary<String, DeviceClassInfo> GetDeviceClassTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetDeviceClassTable().Result ?? [];
        }

        public static GeneralDeviceStatus GetDeviceStatus(String id)
        {
            if (!AgencyAsync.SyncMode) return GeneralDeviceStatus.None;
            return AgencyAsync.GetDeviceStatus(id).Result;
        }

        public static DialogClassInfo? GetDialogClassInfo(String dialogClassID, String? transformID)
        {
            return AgencyLocal.GetDialogClassInfo(dialogClassID, transformID);
        }

        public static Dictionary<String, DialogClassInfo> GetDialogClassTable()
        {
            return AgencyLocal.GetDialogClassTable();
        }

        public static ConfigStatus GetDialogRelatedModulesConfigStatus(String dialogClassID, String transformID, out ConfigStatus[] childrenStatus)
        {
            if (!AgencyAsync.SyncMode)
            {
                childrenStatus = [];
                return ConfigStatus.Disabled;
            }
            var result = AgencyAsync.GetDialogRelatedModulesConfigStatus(dialogClassID, transformID).Result;
            childrenStatus = result.Item2;
            return result.Item1;
        }

        public static object[] GetEventHandles()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetEventHandles().Result ?? [];
        }

        public static EventInfo? GetEventInfo(object eventHandle)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetEventInfo(eventHandle).Result;
        }

        public static String[] GetEventTypeNames()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetEventTypeNames().Result ?? [];
        }

        public static SessionIdentifier[] GetFilteredSessionList()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetFilteredSessionList().Result ?? [];
        }

        public static double GetFilteredSessionListTotalLength()
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetFilteredSessionListTotalLength().Result;
        }

        public static SessionIdentifier[] GetFinishedSessions(String generation)
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetFinishedSessions(generation).Result ?? [];
        }

        public static Dictionary<String, String> GetFrameworkThirdPartyNotices()
        {
            return AgencyLocal.GetFrameworkThirdPartyNotices();
        }

        public static String[] GetGenerationList()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetGenerationList().Result ?? [];
        }

        public static String? GetGenerationPath(SessionIdentifier session, String generation)
        {
            return AgencyLocal.GetGenerationPath(session, generation);
        }

        public static GenerationProcessStatus? GetGenerationProcessStatus(SessionIdentifier session, String generation)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetGenerationProcessStatus(session, generation).Result;
        }

        public static SessionIdentifier[] GetGenerationSessions(String generationID)
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetGenerationSessions(generationID).Result ?? [];
        }

        public static String? GetGlobalParameter(String key, String? defaultValue)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetGlobalParameter(key, defaultValue).Result;
        }

        public static String[] GetGlobalParameterKeys()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetGlobalParameterKeys().Result ?? [];
        }

        public static String? GetGlobalPath(String key)
        {
            return AgencyLocal.GetGlobalPath(key);
        }

        public static String[] GetGlobalPathKeys()
        {
            return AgencyLocal.GetGlobalPathKeys();
        }

        public static String? GetGlobalPublicDataPath()
        {
            return AgencyLocal.GetGlobalPublicDataPath();
        }

        public static String? GetGlobalVariable(String key, String? defaultValue)
        {
            return AgencyLocal.GetGlobalVariable(key, defaultValue);
        }

        public static String[] GetGlobalVariableKeys()
        {
            return AgencyLocal.GetGlobalVariableKeys();
        }

        public static PosixTimeModel? GetGNSSPosixTimeModel(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetGNSSPosixTimeModel(session).Result;
        }

        public static GPUDecoderTestResults GetGPUDecoderTestResults()
        {
            if (!AgencyAsync.SyncMode) return new GPUDecoderTestResults();
            return AgencyAsync.GetGPUDecoderTestResults().Result ?? new GPUDecoderTestResults();
        }

        public static GraphData? GetGraphData(SessionIdentifier session, int graphID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetGraphData(session, graphID).Result;
        }

        public static GraphicCardInfo[] GetGraphicCardInfos()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetGraphicCardInfos().Result ?? [];
        }

        public static int[] GetGraphIDList()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetGraphIDList().Result ?? [];
        }

        public static int? GetGraphIDWithTitle(String title)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetGraphIDWithTitle(title).Result;
        }

        public static String[] GetGraphPanelStylesForID(int graphID)
        {
            return AgencyLocal.GetGraphPanelStylesForID(graphID);
        }

        public static String[] GetGraphPanelStylesForType(GraphType graphType)
        {
            return AgencyLocal.GetGraphPanelStylesForType(graphType);
        }

        public static String? GetGraphTitle(int graphID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetGraphTitle(graphID).Result;
        }

        public static PosixTimeModel? GetHostPosixTimeModel(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetHostPosixTimeModel(session).Result;
        }

        public static double GetInterestTarget()
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetInterestTarget().Result;
        }

        public static double GetInterestTime()
        {
            return AgencyLocal.GetInterestTime();
        }

        public static DateTime? GetInterestTimestamp()
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetInterestTimestamp().Result;
        }

        public static DateTime? GetInternetNTPTime()
        {
            return AgencyLocal.GetInternetNTPTime();
        }

        public static int[] GetLicensedFunctionIndices()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetLicensedFunctionIndices().Result ?? [];
        }

        public static String GetLicenseInfo()
        {
            if (!AgencyAsync.SyncMode) return "";
            return AgencyAsync.GetLicenseInfo().Result ?? "";
        }

        public static DateTime? GetLocalDateTime(SessionIdentifier session, double timeOffset, bool useGNSS)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetLocalDateTime(session, timeOffset, useGNSS).Result;
        }

        public static LogMessage[] GetLogMessages()
        {
            return AgencyLocal.GetLogMessages();
        }

        public static String[] GetManualTriggerNames()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetManualTriggerNames().Result ?? [];
        }

        public static String? GetManualTriggerName(int index)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetManualTriggerName(index).Result;
        }

        public static ConfigStatus[]? GetModuleChildConfigStatus(object caller, String classID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetModuleChildConfigStatus(caller, classID).Result;
        }

        public static String? GetModuleConfig(object caller, String classID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetModuleConfig(caller, classID).Result;
        }

        public static ConfigStatus GetModuleConfigStatus(object caller, String classID, out String? errorHint)
        {
            if (!AgencyAsync.SyncMode)
            {
                errorHint = null;
                return ConfigStatus.Disabled;
            }
            var result = AgencyAsync.GetModuleConfigStatus(caller, classID).Result;
            errorHint = result.Item2;
            return result.Item1;
        }

        public static ModuleDetails? GetModuleDetails(String classID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetModuleDetails(classID).Result;
        }

        public static Dictionary<String, NativeClassInfo> GetNativeClassTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetNativeClassTable().Result ?? [];
        }

        public static Dictionary<String, Version> GetNativePluginVersions(NativeLibraryType type)
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetNativePluginVersions(type).Result ?? [];
        }

        public static CommonImage? GetOfflineMapCommonImage(IntSize imageSize, LocPoint centerLocation, int zoom)
        {
            return AgencyLocal.GetOfflineMapCommonImage(imageSize, centerLocation, zoom);
        }

        public static String GetOfflineMapCopyrightInfo()
        {
            return AgencyLocal.GetOfflineMapCopyrightInfo() ?? "";
        }

        public static Dictionary<String, String> GetPluginGuestSyncTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetPluginGuestSyncTable().Result ?? [];
        }

        public static String[] GetPluginPackIDList()
        {
            return AgencyLocal.GetPluginPackIDList();
        }

        public static PluginPackInfo? GetPluginPackInfo(String packID)
        {
            return AgencyLocal.GetPluginPackInfo(packID);
        }

        public static Dictionary<String, Dictionary<String, String> > GetPluginThirdPartyNotices()
        {
            return AgencyLocal.GetPluginThirdPartyNotices();
        }

        public static byte[]? GetPreviewJpeg(int channel, double timeline, double maxGap, out Timestamp? timestamp, out CameraInfo? cameraInfo)
        {
            var result = AgencyLocal.GetPreviewJpeg(channel, timeline, maxGap);
            timestamp = result.Item2;
            cameraInfo = result.Item3;
            return result.Item1;
        }

        public static Dictionary<String, ProcessorClassInfo> GetProcessorClassTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetProcessorClassTable().Result ?? [];
        }

        public static double GetRawChannelDelayConfig(String id)
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetRawChannelDelayConfig(id).Result;
        }

        public static bool GetRawChannelStatus(String channelID, uint? toleranceMillisecond)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.GetRawChannelStatus(channelID, toleranceMillisecond).Result;
        }

        public static String[] GetRecentProjectPaths()
        {
            return AgencyLocal.GetRecentProjectPaths();
        }

        public static bool GetSampleChannelStatus(String channelID, uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            if (!AgencyAsync.SyncMode)
            {
                interval = [];
                delay = [];
                return false;
            }
            var result = AgencyAsync.GetSampleChannelStatus(channelID, toleranceMillisecond).Result;
            interval = result.Item2.ToList();
            delay = result.Item3.ToList();
            return result.Item1;
        }

        public static List<String>? GetSampleTitle(String channelID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSampleTitle(channelID).Result;
        }

        public static String[] GetSceneIDList()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetSceneIDList().Result ?? [];
        }

        public static Dictionary<String, SceneTitle> GetSceneTitleTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetSceneTitleTable().Result ?? [];
        }

        public static String? GetSessionComment(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSessionComment(session).Result;
        }

        public static Dictionary<SessionIdentifier, SessionFilterFlags> GetSessionFilterTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetSessionFilterTable().Result ?? [];
        }

        public static String? GetSessionFolderName(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSessionFolderName(session).Result;
        }

        public static String[]? GetSessionGenerations(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSessionGenerations(session).Result;
        }

        public static bool GetSessionHostSync(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.GetSessionHostSync(session).Result;
        }

        public static String? GetSessionLayer(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSessionLayer(session).Result;
        }

        public static double? GetSessionLength(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSessionLength(session).Result;
        }

        public static SessionIdentifier[] GetSessionList()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetSessionList().Result ?? [];
        }

        public static double GetSessionListTotalLength()
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetSessionListTotalLength().Result;
        }

        public static String? GetSessionPath(SessionIdentifier session)
        {
            return AgencyLocal.GetSessionPath(session);
        }

        public static Dictionary<String, String>? GetSessionProperties(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSessionProperties(session).Result;
        }

        public static String? GetSessionPublicDataPath(SessionIdentifier session)
        {
            return AgencyLocal.GetSessionPublicDataPath(session);
        }

         public static String GetSessionSearchKey()
        {
            if (!AgencyAsync.SyncMode) return "";
            return AgencyAsync.GetSessionSearchKey().Result ?? "";
        }

        public static double? GetSessionTimeline(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSessionTimeline(session).Result;
        }

        public static String? GetSignalName(String signalID, bool fullName)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSignalName(signalID, fullName).Result;
        }

        public static String[]? GetSignalNamesOfBusMessage(String messageID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetSignalNamesOfBusMessage(messageID).Result;
        }

        public static SignalTreeNode[] GetSignalTree()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetSignalTree().Result ?? [];
        }

        public static String?[] GetSubDataPaths()
        {
            return AgencyLocal.GetSubDataPaths();
        }

        public static String GetSystemStatus(SystemStatus status)
        {
            if (!AgencyAsync.SyncMode) return "";
            return AgencyAsync.GetSystemStatus(status).Result ?? "";
        }

        public static String GetSystemStatusDetails(SystemStatus status)
        {
            if (!AgencyAsync.SyncMode) return "";
            return AgencyAsync.GetSystemStatusDetails(status).Result ?? "";
        }

        public static TaskClassInfo? GetTaskClassInfo(String taskClassID)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetTaskClassInfo(taskClassID).Result;
        }

        public static Dictionary<String, TaskClassInfo> GetTaskClassTable()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetTaskClassTable().Result ?? [];
        }

        public static String GetTempFilesRoot()
        {
            return AgencyLocal.GetTempFilesRoot() ?? "";
        }

        public static DateTime? GetUTCDateTime(SessionIdentifier session, double timeOffset, bool useGNSS)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetUTCDateTime(session, timeOffset, useGNSS).Result;
        }

        public static Dictionary<String, Version> GetVersionTable()
        {
            return AgencyLocal.GetVersionTable();
        }

        public static double GetVideoChannelDelayConfig(int channel)
        {
            if (!AgencyAsync.SyncMode) return 0;
            return AgencyAsync.GetVideoChannelDelayConfig(channel).Result;
        }

        public static VideoChannelInfo[]? GetVideoChannelsInfo(SessionIdentifier session)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetVideoChannelsInfo(session).Result;
        }

        public static bool GetVideoChannelStatus(int channel, uint? toleranceMillisecond, out List<double> interval, out List<double> delay)
        {
            if (!AgencyAsync.SyncMode)
            {
                interval = [];
                delay = [];
                return false;
            }
            var result = AgencyAsync.GetVideoChannelStatus(channel, toleranceMillisecond).Result;
            interval = result.Item2.ToList();
            delay = result.Item3.ToList();
            return result.Item1;
        }

        public static Dictionary<VideoDeviceID, VideoDeviceInfo> GetVideoDevices()
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.GetVideoDevices().Result ?? [];
        }

        public static CommonImage? GetVideoFrameImage(int channel, double timeline, double maxGap, VideoFrameGetMode mode, IntRect? clip, bool withAlpha, out Timestamp? timestamp, out CameraInfo? cameraInfo)
        {
            if (!AgencyAsync.SyncMode)
            {
                timestamp = null;
                cameraInfo = null;
                return null;
            }
            var result = AgencyAsync.GetVideoFrameImage(channel, timeline, maxGap, mode, clip, withAlpha).Result;
            timestamp = result.Item2;
            cameraInfo = result.Item3;
            return result.Item1;
        }

        public static CommonImage? GetVideoFrameThumbnail(int channel, double timeline, double maxGap, bool withAlpha)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetVideoFrameThumbnail(channel, timeline, maxGap, withAlpha).Result;
        }

        public static IntSize? GetVideoRawSize(int channel, double timeline)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.GetVideoRawSize(channel, timeline).Result;
        }

        public static SpecialCameraType GetVideoSpecialType(int channel)
        {
            if (!AgencyAsync.SyncMode) return SpecialCameraType.Unknown;
            return AgencyAsync.GetVideoSpecialType(channel).Result;
        }

        public static WindowClassInfo? GetWindowClassInfo(String windowClassID, String? transformID)
        {
            return AgencyLocal.GetWindowClassInfo(windowClassID, transformID);
        }

        public static Dictionary<String, WindowClassInfo> GetWindowClassTable()
        {
            return AgencyLocal.GetWindowClassTable();
        }

        public static bool InstallPlugin(String dirPath)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyLocal.InstallPlugin(dirPath).Result;
        }

        public static bool IsBusMessageBound(string busMessageID)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.IsBusMessageBound(busMessageID).Result;
        }

        public static bool IsFileOutputEnabled()
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.IsFileOutputEnabled().Result;
        }

        public static bool IsInputChannelAvailable(String channelID)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.IsInputChannelAvailable(channelID).Result;
        }

        public static bool IsInternetConnected()
        {
            return AgencyLocal.IsInternetConnected();
        }

        public static bool IsMainThreadFunction(String funcName)
        {
            return AgencyLocal.IsMainThreadFunction(funcName);
        }

        public static bool IsMessageValid(String messageID, bool optional)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.IsMessageValid(messageID, optional).Result;
        }

        public static bool IsPRCWebPreferred()
        {
            return AgencyLocal.IsPRCWebPreferred();
        }

        public static bool IsReady(out String? busyReason)
        {
            if (!AgencyAsync.SyncMode)
            {
                busyReason = null;
                return false;
            }
            var result = AgencyAsync.IsReady().Result;
            busyReason = result.Item2;
            return result.Item1;
        }

        public static bool IsSampleChannelConflict(string channelID)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.IsSampleChannelConflict(channelID).Result;
        }

        public static bool IsSignalValid(String signalID, bool optional)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.IsSignalValid(signalID, optional).Result;
        }

        public static bool IsVideoDataAvailable(int channel, uint? tolerance)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.IsVideoDataAvailable(channel, tolerance).Result;
        }

        public static void Log(String text, LogLevel level)
        {
            AgencyLocal.Log(text, level);
        }

        public static bool NewProject(bool force)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyLocal.NewProject(force).Result;
        }

        public static void OpenDialog(object caller, String dialogClassID, String config)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyLocal.OpenDialog(caller, dialogClassID, config).Wait();
        }

        public static bool OpenProject(String? projectFile, bool force)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyLocal.OpenProject(projectFile, force).Result;
        }

        public static BusSignalValue[] ParseBusMessage(BusMessageSample busMessage)
        {
            if (!AgencyAsync.SyncMode) return [];
            return AgencyAsync.ParseBusMessage(busMessage).Result ?? [];
        }

        public static void PlayMp3(byte[] mp3FileData)
        {
            AgencyLocal.PlayMp3(mp3FileData);
        }

        public static bool PopupConfirm(String msg)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyLocal.PopupConfirm(msg).Result;
        }

        public static void PopupError(String msg)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyLocal.PopupError(msg).Wait();
        }

        public static void PopupNotice(String msg)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyLocal.PopupNotice(msg).Wait();
        }

        public static void Print(String text)
        {
            AgencyLocal.Print(text);
        }

        public static void PublishData(String dataID, byte[] data)
        {
            AgencyLocal.PublishData(dataID, data);
        }

        public static void RefreshGenerations()
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.RefreshGenerations().Wait();
        }

        public static void RefreshSessions()
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.RefreshSessions().Wait();
        }

        public static void RegisterAudioDriver(AudioDriverInfo driver, AudioRecorder? recorder, AudioReplayer? replayer)
        {
            AgencyLocal.RegisterAudioDriver(driver, recorder, replayer);
        }

        public static void RegisterAudioReplayers(AudioDriverInfo driver, AudioReplayer replayer)
        {
            AgencyLocal.RegisterAudioReplayers(driver, replayer);
        }

        public static void RegisterGraphPanelForType(GraphType graphType, String styleName, Type panelType)
        {
            AgencyLocal.RegisterGraphPanelForType(graphType, styleName, panelType);
        }

        public static void RegisterGraphPanelForID(int graphID, String styleName, Type panelType)
        {
            AgencyLocal.RegisterGraphPanelForID(graphID, styleName, panelType);
        }

        public static DialogClassInfo? RegisterTransformDialogClass(String dialogClassID, String config)
        {
            return AgencyLocal.RegisterTransformDialogClass(dialogClassID, config);
        }

        public static DialogClassInfo? RegisterTransformDialogClassDirectly(String dialogClassID, DialogClass transformDialogClass, String defaultConfig)
        {
            return AgencyLocal.RegisterTransformDialogClassDirectly(dialogClassID, transformDialogClass, defaultConfig);
        }

        public static WindowClassInfo? RegisterTransformWindowClass(String windowClassID, String config)
        {
            return AgencyLocal.RegisterTransformWindowClass(windowClassID, config);
        }

        public static WindowClassInfo? RegisterTransformWindowClassDirectly(String windowClassID, WindowClass transformWindowClass, String defaultConfig)
        {
            return AgencyLocal.RegisterTransformWindowClassDirectly(windowClassID, transformWindowClass, defaultConfig);
        }

        public static void RemoveBusProtocolFile(BusProtocolFileID fileID)
        {
            AgencyLocal.RemoveBusProtocolFile(fileID);
        }

        public static void RemoveEvent(object eventHandle)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.RemoveEvent(eventHandle).Wait();
        }

        public static void RemoveGeneration(SessionIdentifier session, String genID)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.RemoveGeneration(session, genID).Wait();
        }

        public static void RemoveProcessorVideoReference(int videoChannel)
        {
            AgencyLocal.RemoveProcessorVideoReference(videoChannel);
        }

        public static bool RemoveSession(SessionIdentifier session, bool force)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.RemoveSession(session, force).Result;
        }

        public static void RemoveSignalReference(String signalID)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.RemoveSignalReference(signalID).Wait();
        }

        public static void ResetAppFunctionHandler(object caller, String nativeClassID, String funcID)
        {
            AgencyLocal.ResetAppFunctionHandler(caller, nativeClassID, funcID);
        }

        public static void ResetGPUDecoderTestResults()
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.ResetGPUDecoderTestResults().Wait();
        }

        public static void RunConsole(object caller, string consoleClassID)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.RunConsole(caller, consoleClassID).Wait();
        }

        public static TaskResult RunStandaloneTask(object caller, String taskClassID, String config, out String? returnValue)
        {
            if (!AgencyAsync.SyncMode)
            {
                returnValue = null;
                return TaskResult.Unknown;
            }
            var result = AgencyAsync.RunStandaloneTask(caller, taskClassID, config).Result;
            returnValue = result.Item2;
            return result.Item1;
        }

        public static bool SaveCurrentProject(String? projectFile)
        {
            return AgencyLocal.SaveCurrentProject(projectFile);
        }

        public static String? SelectBusMessage(String? originMessageID)
        {
            return AgencyLocal.SelectBusMessage(originMessageID).Result;
        }

        public static void SelectBusMessages(SelectBusMessageHandler handler, List<String> existBusMessageIDList)
        {
            AgencyLocal.SelectBusMessages(handler, existBusMessageIDList);
        }

        public static BusProtocolFileID[] SelectBusProtocolFiles(BusProtocolFileID[] selected)
        {
            return AgencyLocal.SelectBusProtocolFiles(selected).Result;
        }

        public static SignalConfig? SelectSignal(SignalConfig? origin, bool withScale, bool withSignBit, String unit)
        {
            return AgencyLocal.SelectSignal(origin, withScale, withSignBit, unit).Result;
        }

        public static void SelectSignals(SelectSignalHandler handler, List<String> existSignalIDList)
        {
            AgencyLocal.SelectSignals(handler, existSignalIDList);
        }

        public static void SendBusMessage(BusMessage message)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SendBusMessage(message).Wait();
        }

        public static byte[]? SendBusMessageBound(String messageID, uint? interval)
        {
            if (!AgencyAsync.SyncMode) return null;
            return AgencyAsync.SendBusMessageBound(messageID, interval).Result;
        }

        public static void SendManualTrigger(int channel)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SendManualTrigger(channel).Wait();
        }

        public static void SendRawData(String channelID, double[] values, byte[] binary)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SendRawData(channelID, values, binary).Wait();
        }

        public static void SendRawDataWithCPUTick(ulong cpuTick, String channelID, double[] values, byte[] binary)
        {
            AgencyLocal.SendRawDataWithCPUTick(cpuTick, channelID, values, binary);
        }

        public static void SetAppFunctionHandler(object caller, String nativeClassID, String funcID, AppFunctionHandler handler)
        {
            AgencyLocal.SetAppFunctionHandler(caller, nativeClassID, funcID, handler);
        }

        public static void SetAudioChannelDelayConfig(double delay)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetAudioChannelDelayConfig(delay).Wait();
        }

        public static void SetAudioVolume(double volume)
        {
            AgencyLocal.SetAudioVolume(volume);
        }

        public static void SetBusChannelDelayConfig(int busChannel, double delay)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetBusChannelDelayConfig(busChannel, delay).Wait();
        }

        public static void SetChannelGuestSyncFlag(String id, bool guestSync)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetChannelGuestSyncFlag(id, guestSync).Wait();
        }

        public static void SetChannelMonitoringFlag(String id, bool monitoring)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetChannelMonitoringFlag(id, monitoring).Wait();
        }

        public static void SetChannelServerSyncMonitoringFlag(String id, bool monitoring)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetChannelServerSyncMonitoringFlag(id, monitoring).Wait();
        }

        public static bool SetControlFlag(String controllerName, bool enabled)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.SetControlFlag(controllerName, enabled).Result;
        }

        public static void SetCurrentDataLayer(String? layer)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetCurrentDataLayer(layer).Wait();
        }

        public static void SetCurrentDialogTitle(String? title, object? icon)
        {
            AgencyLocal.SetCurrentDialogTitle(title, icon);
        }

        public static void SetDataPath(String? path)
        {
            AgencyLocal.SetDataPath(path);
        }

        public static void SetEventComment(object eventHandle, String comment)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetEventComment(eventHandle, comment).Wait();
        }

        public static void SetGlobalParameter(String key, String value)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetGlobalParameter(key, value).Wait();
        }

        public static void SetGlobalPath(String key, String path)
        {
            AgencyLocal.SetGlobalPath(key, path);
        }

        public static void SetGlobalVariable(String key, String value)
        {
            AgencyLocal.SetGlobalVariable(key, value);
        }

        public static void SetInterestTime(double targetTimeline)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetInterestTime(targetTimeline).Wait();
        }

        public static void SetInterestTimestamp(DateTime targetTimestamp)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetInterestTimestamp(targetTimestamp).Wait();
        }

        public static void SetManualTriggerName(int index, String name)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetManualTriggerName(index, name).Wait();
        }

        public static void SetModuleConfig(object caller, String classID, String config)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetModuleConfig(caller, classID, config).Wait();
        }

        public static void SetRawChannelDelayConfig(String id, double delay)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetRawChannelDelayConfig(id, delay).Wait();
        }

        public static void SetSessionChecker(SessionIdentifier session, bool check)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetSessionChecker(session, check).Wait();
        }

        public static void SetSessionComment(SessionIdentifier session, String comment)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetSessionComment(session, comment).Wait();
        }

        public static void SetSessionHostSync(SessionIdentifier session, bool hostSync)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetSessionHostSync(session, hostSync).Wait();
        }

        public static void SetSessionProperties(SessionIdentifier session, Dictionary<String, String> properties)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetSessionProperties(session, properties).Wait();
        }

        public static void SetSessionSearchKeyword(String keyword)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetSessionSearchKeyword(keyword).Wait();
        }

        public static void SetSubDataPath(int subIndex, String? path)
        {
            AgencyLocal.SetSubDataPath(subIndex, path);
        }

        public static void SetTargetReplaySpeed(double speed)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetTargetReplaySpeed(speed).Wait();
        }

        public static void SetVideoChannelDelayConfig(int channel, double delay)
        {
            if (!AgencyAsync.SyncMode) return;
            AgencyAsync.SetVideoChannelDelayConfig(channel, delay).Wait();
        }

        public static void SetWindowTitle(object window, String? title, object? icon)
        {
            AgencyLocal.SetWindowTitle(window, title, icon);
        }

        public static bool StartOffline(bool force, bool previewOnly, String? genDirName)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.StartOffline(force, previewOnly, genDirName).Result;
        }

        public static bool StartOnlineWithController(String controllerName, bool previewOnly)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.StartOnlineWithController(controllerName, previewOnly).Result;
        }

        public static bool StartOnline(bool force, bool previewOnly, String? sessionDirName)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.StartOnline(force, previewOnly, sessionDirName).Result;
        }

        public static bool StartProcess(String target)
        {
            return AgencyLocal.StartProcess(target);
        }

        public static bool StartRemote(bool force, bool previewOnly, String? sessionDirName, ulong startPosixTime)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.StartRemote(force, previewOnly, sessionDirName, startPosixTime).Result;
        }

        public static bool StartRemoteWithController(String controllerName, bool previewOnly, ulong startPosixTime)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.StartRemoteWithController(controllerName, previewOnly, startPosixTime).Result;
        }

        public static bool StartReplay(bool force, double startTimeline, double? interestTarget)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.StartReplay(force, startTimeline, interestTarget).Result;
        }

        public static bool StopRunningWithController(String controllerName)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.StopRunningWithController(controllerName).Result;
        }

        public static bool StopRunning(bool force, bool editRecordedSession)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.StopRunning(force, editRecordedSession).Result;
        }

        public static DataSubscriber? SubscribeData(String dataID, int bufferLength, int timeout)
        {
            return AgencyLocal.SubscribeData(dataID, bufferLength, timeout);
        }

        public static bool SwitchAppMode(String? controllerName, ApplicationMode mode, int waitSecond)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyAsync.SwitchAppMode(controllerName, mode, waitSecond).Result;
        }

        public static bool TerminateApp(bool force, bool autosave)
        {
            if (!AgencyAsync.SyncMode) return false;
            return AgencyLocal.TerminateApp(force, autosave).Result;
        }

        public static bool UninstallPlugin(String packID)
        {
            return AgencyLocal.UninstallPlugin(packID);
        }

        public static void UnregisterPanel(object panel)
        {
            AgencyLocal.UnregisterPanel(panel);
        }

        public static bool UpdateBusProtocolFilePath(BusProtocolFileID fileID, String filePath)
        {
            return AgencyLocal.UpdateBusProtocolFilePath(fileID, filePath);
        }
    }
}
