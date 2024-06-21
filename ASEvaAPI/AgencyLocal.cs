using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASEva.Samples;
using ASEva.Utility;

namespace ASEva
{
    #pragma warning disable CS1571

    public interface AgencyLocalHandler
    {
        AddBusProtocolResult AddBusProtocolFile(String filePath, out BusProtocolFileID[] fileIDs);
        void AddMainThreadCheckpoint(String location);
        void AddProcessorVideoReference(int videoChannel);
        void AddSceneData(SceneData scene);
        void AddWindow(object caller, String windowClassID, String config, bool newWorkspaceIfNeeded);
        byte[] CallNativeFunction(object caller, String nativeClassID, String funcID, byte[] input);
        void CallWebApi(String request, WebApiContext context);
        void CallWebApiPost(String request, byte[] body, WebPostContentType contentType, WebApiContext context);
        Task ConfigDataEncryption();
        Task ConfigOfflineMapPath();
        FloatPoint ConvertOfflineMapLocToPix(LocPoint origin, int zoom, LocPoint point);
        LocPoint ConvertOfflineMapPixToLoc(LocPoint origin, int zoom, FloatPoint pixel);
        CreatePanelResult CreateConfigPanel(object caller, String dialogClassID, String transformID, out object panel, out DialogClassInfo info);
        GraphPanel CreateGraphPanelForType(GraphType graphType, String styleName);
        GraphPanel CreateGraphPanelForID(int graphID, String styleName);
        CreatePanelResult CreateWindowPanel(object caller, String windowClassID, String transformID, out object panel, out WindowClassInfo info);
        CommonImage DecodeImage(byte[] imageData);
        bool DeleteToRecycleBin(String path);
        void DisableAllPlugins();
        void DisablePlugin(String packID);
        void EnablePlugin(String packID);
        byte[] EncodeImage(CommonImage image, String format);
        string[] GetAllChannelMonitoringKeys();
        String[] GetAllChannelServerSyncMonitoringKeys();
        String GetAppFilesRoot();
        ApplicationGUI GetAppGUI();
        String GetAppID();
        Language GetAppLanguage();
        String GetBusProtocolFilePath(BusProtocolFileID fileID);
        BusFileInfo[] GetBusProtocolFilesInfo();
        bool GetChannelMonitoringFlag(String id);
        bool GetChannelServerSyncMonitoringFlag(String id);
        String GetConfigFilesRoot();
        String GetCurrentDataLayerPath();
        String GetCurrentProject();
        String GetDataPath();
        DialogClassInfo GetDialogClassInfo(String dialogClassID, String transformID);
        Dictionary<String, DialogClassInfo> GetDialogClassTable();
        Dictionary<String, String> GetFrameworkThirdPartyNotices();
        String GetGenerationPath(SessionIdentifier session, String generation);
        String GetGlobalPath(String key);
        String[] GetGlobalPathKeys();
        String GetGlobalPublicDataPath();
        String GetGlobalVariable(String key, String defaultValue);
        String[] GetGlobalVariableKeys();
        String[] GetGraphPanelStylesForID(int graphID);
        String[] GetGraphPanelStylesForType(GraphType graphType);
        DateTime? GetInternetNTPTime();
        LogMessage[] GetLogMessages();
        CommonImage GetOfflineMapCommonImage(IntSize imageSize, LocPoint centerLocation, int zoom);
        String GetOfflineMapCopyrightInfo();
        String[] GetPluginPackIDList();
        PluginPackInfo GetPluginPackInfo(String packID);
        Dictionary<String, Dictionary<String, String> > GetPluginThirdPartyNotices();
        String[] GetRecentProjectPaths();
        String GetSessionPath(SessionIdentifier session);
        String GetSessionPublicDataPath(SessionIdentifier session);
        String[] GetSubDataPaths();
        String GetTempFilesRoot();
        Dictionary<String, Version> GetVersionTable();
        WindowClassInfo GetWindowClassInfo(String windowClassID, String transformID);
        Dictionary<String, WindowClassInfo> GetWindowClassTable();
        Task<bool> InstallPlugin(String dirPath);
        bool IsInternetConnected();
        void Log(String text, LogLevel level);
        Task<bool> NewProject(bool force);
        Task OpenDialog(object caller, String dialogClassID, String config);
        Task<bool> OpenProject(String projectFile, bool force);
        void PlayMp3(byte[] mp3FileData);
        Task<bool> PopupConfirm(String msg);
        Task PopupError(String msg);
        Task PopupNotice(String msg);
        void Print(String text);
        void PublishData(String dataID, byte[] data);
        void RegisterAudioDriver(AudioDriverInfo driver, AudioRecorder recorder, AudioReplayer replayer);
        void RegisterGraphPanelForType(GraphType graphType, String styleName, Type panelType);
        void RegisterGraphPanelForID(int graphID, String styleName, Type panelType);
        DialogClassInfo RegisterTransformDialogClass(String dialogClassID, String config);
        DialogClassInfo RegisterTransformDialogClassDirectly(String dialogClassID, DialogClass transformDialogClass, String defaultConfig);
        WindowClassInfo RegisterTransformWindowClass(String windowClassID, String config);
        WindowClassInfo RegisterTransformWindowClassDirectly(String windowClassID, WindowClass transformWindowClass, String defaultConfig);
        void RemoveBusProtocolFile(BusProtocolFileID fileID);
        void RemoveProcessorVideoReference(int videoChannel);
        void ResetAppFunctionHandler(object caller, String nativeClassID, String funcID);
        bool SaveCurrentProject(String projectFile);
        void SendRawDataWithCPUTick(ulong cpuTick, String channelID, double[] values, byte[] binary);
        void SetAppFunctionHandler(object caller, String nativeClassID, String funcID, AppFunctionHandler handler);
        void SetAudioVolume(double volume);
        void SetChannelMonitoringFlag(String id, bool monitoring);
        void SetChannelServerSyncMonitoringFlag(String id, bool monitoring);
        void SetCurrentDialogTitle(String title, object icon);
        void SetDataPath(String path);
        void SetGlobalPath(String key, String path);
        void SetGlobalVariable(String key, String value);
        void SetSubDataPath(int subIndex, String path);
        void SetWindowTitle(object window, String title, object icon);
        bool StartProcess(String target);
        DataSubscriber SubscribeData(String dataID, int bufferLength, int timeout);
        Task<bool> TerminateApp(bool force, bool autosave);
        bool UninstallPlugin(String packID);
        void UnregisterPanel(object panel);
        bool UpdateBusProtocolFilePath(BusProtocolFileID fileID, String filePath);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.1.0) Include all main local APIs
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.1.0) 集合了所有主要本地API
    /// </summary>
    public class AgencyLocal
    {
        private static AgencyLocalHandler handler = null;
        public static AgencyLocalHandler Handler
        {
            private get
            {
                if (handler == null) handler = new AgencyLocalDefault();
                return handler;
            }
            set
            {
                if (value != null) handler = value;
            }
        }

        /// \~English
        /// <summary>
        /// Add bus protocol file
        /// </summary>
        /// <param name="filePath">File path of bus protocol file</param>
        /// <param name="fileIDs">Output bus protocol file IDs, null if the file doesn't exist</param>
        /// <returns>The result</returns>
        /// \~Chinese
        /// <summary>
        /// 添加新的总线协议文件
        /// </summary>
        /// <param name="filePath">总线协议文件路径</param>
        /// <param name="fileIDs">若文件存在则输出关联的总线协议文件ID列表，否则输出null</param>
        /// <returns>添加结果</returns>
        public static AddBusProtocolResult AddBusProtocolFile(String filePath, out BusProtocolFileID[] fileIDs)
        {
            return Handler.AddBusProtocolFile(filePath, out fileIDs);
        }

        /// \~English
        /// <summary>
        /// Add checkpoint for main thread
        /// </summary>
        /// <param name="location">Checkpoint location</param>
        /// \~Chinese
        /// <summary>
        /// 添加主线程检查点
        /// </summary>
        /// <param name="location">主线程检查点位置</param>
        public static void AddMainThreadCheckpoint(String location)
        {
            Handler.AddMainThreadCheckpoint(location);
        }

        /// \~English
        /// <summary>
        /// Add video reference (only video with references will be sent to app layer's processors)
        /// </summary>
        /// <param name="videoChannel">Video channel, 0~23 corresponding to A~X</param>
        /// \~Chinese
        /// <summary>
        /// 添加视频引用，在应用层的数据处理对象才可获得该通道的数据
        /// </summary>
        /// <param name="videoChannel">视频通道，0~23对应A~X</param>
        public static void AddProcessorVideoReference(int videoChannel)
        {
            Handler.AddProcessorVideoReference(videoChannel);
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
        /// Add a window to workspace
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="windowClassID">Window class ID</param>
        /// <param name="config">Configuration string</param>
        /// <param name="newWorkspaceIfNeeded">Whether to add to a new workspace (if available) if there's no space in the current workspace</param>
        /// \~Chinese
        /// <summary>
        /// 添加窗口至工作空间
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <param name="config">初始化配置</param>
        /// <param name="newWorkspaceIfNeeded">如果当前工作空间位置不足，是否添加至新工作空间（如果支持）</param>
        public static void AddWindow(object caller, String windowClassID, String config, bool newWorkspaceIfNeeded)
        {
            Handler.AddWindow(caller, windowClassID, config, newWorkspaceIfNeeded);
        }

        /// \~English
        /// <summary>
        /// Call native plugin's function
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="funcID">Function ID</param>
        /// <param name="input">Input data for the function</param>
        /// <returns>Output data from the function, null if the function is not found</returns>
        /// \~Chinese
        /// <summary>
        /// 调用原生插件中的函数
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
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
        /// Call Web API (POST) asynchronously with the specified content type
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="body">Binary data body</param>
        /// <param name="contentType">Content type</param>
        /// <param name="context">The context of calling. Since this function is non-blocking, the context needs to be used to get the status and response at a future time after it ends</param>
        /// \~Chinese
        /// <summary>
        /// 非阻塞调用Web API (POST)，指定内容类型
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
        /// (api:app=3.2.3) Show modal dialog to configure data encryption
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.3) 打开对话框配置文件加密选项
        /// </summary>
        public static Task ConfigDataEncryption()
        {
            return Handler.ConfigDataEncryption();
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.3) Show a modal dialog to configure offline map's path
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.3) 弹出对话框配置离线地图路径
        /// </summary>
        public static Task ConfigOfflineMapPath()
        {
            return Handler.ConfigOfflineMapPath();
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
        /// Create config panel
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="dialogClassID">Dialog class ID</param>
        /// <param name="transformID">Transform ID, set to null if not to transform</param>
        /// <param name="panel">The created config panel, null if failed to create</param>
        /// <param name="info">Information of the created config panel, null if failed to create</param>
        /// <returns>Result of creating (After you're done using the panel, call ASEva.AgencyLocal.UnregisterPanel )</returns>
        /// \~Chinese
        /// <summary>
        /// 创建配置面板对象
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="transformID">分化ID，null表示不分化</param>
        /// <param name="panel">新建的配置面板对象，创建失败则为null</param>
        /// <param name="info">新建配置面板的组件信息，创建失败则为null</param>
        /// <returns>创建结果，若成功则在释放窗口时需要调用 ASEva.AgencyLocal.UnregisterPanel </returns>
        public static CreatePanelResult CreateConfigPanel(object caller, String dialogClassID, String transformID, out object panel, out DialogClassInfo info)
        {
            return Handler.CreateConfigPanel(caller, dialogClassID, transformID, out panel, out info);
        }

        /// \~English
        /// <summary>
        /// Create graph panel
        /// </summary>
        /// <param name="graphType">Graph type</param>
        /// <param name="styleName">Style name, set to null to use the first style</param>
        /// <returns>Created graph panel, null if failed to create</returns>
        /// \~Chinese
        /// <summary>
        /// 创建图表可视化面板
        /// </summary>
        /// <param name="graphType">图表类型</param>
        /// <param name="styleName">可视化面板样式名，若输入空则使用首个注册样式</param>
        /// <returns>可视化面板对象，若创建失败则返回null</returns>
        public static GraphPanel CreateGraphPanelForType(GraphType graphType, String styleName)
        {
            return Handler.CreateGraphPanelForType(graphType, styleName);
        }

        /// \~English
        /// <summary>
        /// Create graph panel
        /// </summary>
        /// <param name="graphID">Graph ID</param>
        /// <param name="styleName">Style name, set to null to use the first style</param>
        /// <returns>Created graph panel, null if failed to create</returns>
        /// \~Chinese
        /// <summary>
        /// 创建图表可视化面板
        /// </summary>
        /// <param name="graphID">图表报告ID</param>
        /// <param name="styleName">可视化面板样式名，若输入空则使用首个注册样式</param>
        /// <returns>可视化面板对象，若创建失败则返回null</returns>
        public static GraphPanel CreateGraphPanelForID(int graphID, String styleName)
        {
            return Handler.CreateGraphPanelForID(graphID, styleName);
        }

        /// \~English
        /// <summary>
        /// Create window panel
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="windowClassID">Window class ID</param>
        /// <param name="transformID">Transform ID, set to null if not to transform</param>
        /// <param name="panel">The created window panel, null if failed to create</param>
        /// <param name="info">Information of the created window panel, null if failed to create</param>
        /// <returns>Result of creating (After you're done using the panel, call ASEva.AgencyLocal.UnregisterPanel )</returns>
        /// \~Chinese
        /// <summary>
        /// 创建窗口面板对象
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <param name="transformID">分化ID，null表示不分化</param>
        /// <param name="panel">新建的窗口面板对象，创建失败则为null</param>
        /// <param name="info">新建窗口面板的组件信息，创建失败则为null</param>
        /// <returns>创建结果，若成功则在释放窗口时需要调用 ASEva.AgencyLocal.UnregisterPanel </returns>
        public static CreatePanelResult CreateWindowPanel(object caller, String windowClassID, String transformID, out object panel, out WindowClassInfo info)
        {
            return Handler.CreateWindowPanel(caller, windowClassID, transformID, out panel, out info);
        }

        /// \~English
        /// <summary>
        /// Decode image
        /// </summary>
        /// <param name="imageData">JPG or PNG binary data</param>
        /// <returns>Decoded common image, null if failed to decode</returns>
        /// \~Chinese
        /// <summary>
        /// 解码图像数据
        /// </summary>
        /// <param name="imageData">JPG或PNG二进制数据</param>
        /// <returns>解码后的通用图像数据(BGR不逆序)，若失败则返回null</returns>
        public static CommonImage DecodeImage(byte[] imageData)
        {
            return Handler.DecodeImage(imageData);
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
        /// Disable all plugins (except for the workflow plugin)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 禁用所有插件（除当前流程插件外）
        /// </summary>
        public static void DisableAllPlugins()
        {
            Handler.DisableAllPlugins();
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
        public static void DisablePlugin(String packID)
        {
            Handler.DisablePlugin(packID);
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
        public static void EnablePlugin(String packID)
        {
            Handler.EnablePlugin(packID);
        }

        /// \~English
        /// <summary>
        /// Encode image
        /// </summary>
        /// <param name="image">Common image</param>
        /// <param name="format">Target format, only "jpg" and "png" supported</param>
        /// <returns>Encoded data, null, if failed to encode</returns>
        /// \~Chinese
        /// <summary>
        /// 编码图像数据
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
        /// Get monitor IDs of all the channels being monitored that there's data in the channel
        /// </summary>
        /// <returns>Monitor IDs of all the channels being monitored</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有正在监控有无数据的通道ID
        /// </summary>
        /// <returns>正在监控有无数据的通道ID列表</returns>
        public static string[] GetAllChannelMonitoringKeys()
        {
            return Handler.GetAllChannelMonitoringKeys();
        }

        /// \~English
        /// <summary>
        /// Get monitor IDs of all the channels being monitored that the channel's data is synchronized with time server
        /// </summary>
        /// <returns>Monitor IDs of all the channels being monitored</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有正在监控数据与授时服务器同步的监控ID
        /// </summary>
        /// <returns>正在监控数据与授时服务器同步的通道ID列表</returns>
        public static String[] GetAllChannelServerSyncMonitoringKeys()
        {
            return Handler.GetAllChannelServerSyncMonitoringKeys();
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
        /// Get GUI framework that the application based on
        /// </summary>
        /// <returns>GUI framework</returns>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序基于的图形界面框架
        /// </summary>
        /// <returns>图形界面框架</returns>
        public static ApplicationGUI GetAppGUI()
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
        public static String GetAppID()
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
        public static Language GetAppLanguage()
        {
            return Handler.GetAppLanguage();
        }

        /// \~English
        /// <summary>
        /// Get the path of bus protocol file
        /// </summary>
        /// <param name="fileID">Bus protocol file ID</param>
        /// <returns>The path of bus protocol file, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取总线协议对应文件的路径
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        /// <returns>总线协议文件路径，若未找到返回null</returns>
        public static String GetBusProtocolFilePath(BusProtocolFileID fileID)
        {
            return Handler.GetBusProtocolFilePath(fileID);
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
        /// Get whether to monitor that there's data in the specified channel
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0, etc.</param>
        /// <returns>Whether to monitor</returns>
        /// \~Chinese
        /// <summary>
        /// 获取是否监控指定通道有无数据
        /// </summary>
        /// <param name="id">监控ID，如：bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0等</param>
        /// <returns>是否监控有无数据</returns>
        public static bool GetChannelMonitoringFlag(String id)
        {
            return Handler.GetChannelMonitoringFlag(id);
        }

        /// \~English
        /// <summary>
        /// Get whether to monitor that the specified channel's data is synchronized with time server
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, sample@xxx-v2@0, etc.</param>
        /// <returns>Whether to monitor</returns>
        /// \~Chinese
        /// <summary>
        /// 获取是否监控指定通道数据与授时服务器同步
        /// </summary>
        /// <param name="id">监控ID，如bus@1, video@0, sample@xxx-v2@0等</param>
        /// <returns>是否监控指定通道数据与授时服务器同步</returns>
        public static bool GetChannelServerSyncMonitoringFlag(String id)
        {
            return Handler.GetChannelServerSyncMonitoringFlag(id);
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
        /// Get the path of current data layer
        /// </summary>
        /// <returns>The path of current data layer, returns null if the data path is not configured or the current data layer is '..', return data path if the current data layer is null (all layers)</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前数据层级的路径
        /// </summary>
        /// <returns>当前数据层级的路径，若数据目录未设置或数据层级为'..'则返回null，若当前数据层级为null(所有层级)则返回数据目录根路径</returns>
        public static String GetCurrentDataLayerPath()
        {
            return Handler.GetCurrentDataLayerPath();
        }

        /// \~English
        /// <summary>
        /// Get current project file's path
        /// </summary>
        /// <returns>Current project file's path, null if it's a new project or loaded from autosaved</returns>
        /// \~Chinese
        /// <summary>
        /// 获取当前项目文件
        /// </summary>
        /// <returns>当前项目文件，新项目或从autosave读取的项目都为null</returns>
        public static String GetCurrentProject()
        {
            return Handler.GetCurrentProject();
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
        /// Get information of dialog class
        /// </summary>
        /// <param name="dialogClassID">Dialog class ID</param>
        /// <param name="transformID">Transform ID, can be null</param>
        /// <returns>Information of dialog class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取对话框组件信息
        /// </summary>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="transformID">分化ID，可为null</param>
        /// <returns>对话框组件信息，若未找到返回null</returns>
        public static DialogClassInfo GetDialogClassInfo(String dialogClassID, String transformID)
        {
            return Handler.GetDialogClassInfo(dialogClassID, transformID);
        }

        /// \~English
        /// <summary>
        /// Get information of all dialog classes
        /// </summary>
        /// <returns>Dictionary. The key is dialog class ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取对话框组件信息表
        /// </summary>
        /// <returns>对话框组件信息表，键为组件ID</returns>
        public static Dictionary<String, DialogClassInfo> GetDialogClassTable()
        {
            return Handler.GetDialogClassTable();
        }

        /// \~English
        /// <summary>
        /// Get third part license notices of software used by framework
        /// </summary>
        /// <returns>Dictionary. The key is title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取框架软件使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为标题，值为版权声明</returns>
        public static Dictionary<String, String> GetFrameworkThirdPartyNotices()
        {
            return Handler.GetFrameworkThirdPartyNotices();
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
        public static String GetGenerationPath(SessionIdentifier session, String generation)
        {
            return Handler.GetGenerationPath(session, generation);
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
        /// Get all keys of global path
        /// </summary>
        /// <returns>All keys of global path</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有全局路径的键
        /// </summary>
        /// <returns>所有全局路径的键</returns>
        public static String[] GetGlobalPathKeys()
        {
            return Handler.GetGlobalPathKeys();
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
        /// Get all keys of global variable
        /// </summary>
        /// <returns>All keys of global variable</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有全局变量的键
        /// </summary>
        /// <returns>所有全局变量的键</returns>
        public static String[] GetGlobalVariableKeys()
        {
            return Handler.GetGlobalVariableKeys();
        }

        /// \~English
        /// <summary>
        /// Get all available style names for the specified graph ID
        /// </summary>
        /// <param name="graphID">Graph ID</param>
        /// <returns>All available style names</returns>
        /// \~Chinese
        /// <summary>
        /// 获取符合图表报告的所有可视化面板样式名
        /// </summary>
        /// <param name="graphID">图表报告ID</param>
        /// <returns>可视化面板样式名列表</returns>
        public static String[] GetGraphPanelStylesForID(int graphID)
        {
            return Handler.GetGraphPanelStylesForID(graphID);
        }

        /// \~English
        /// <summary>
        /// Get all available style names for the specified graph type
        /// </summary>
        /// <param name="graphType">Graph type</param>
        /// <returns>All available style names</returns>
        /// \~Chinese
        /// <summary>
        /// 获取符合图表报告的所有可视化面板样式名
        /// </summary>
        /// <param name="graphType">图表类型</param>
        /// <returns>可视化面板样式名列表</returns>
        public static String[] GetGraphPanelStylesForType(GraphType graphType)
        {
            return Handler.GetGraphPanelStylesForType(graphType);
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
        public static DateTime? GetInternetNTPTime()
        {
            return Handler.GetInternetNTPTime();
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
        public static LogMessage[] GetLogMessages()
        {
            return Handler.GetLogMessages();
        }

        /// \~English
        /// <summary>
        /// Query offline map's image
        /// </summary>
        /// <param name="imageSize">Image size</param>
        /// <param name="centerLocation">Location of the image's center</param>
        /// <param name="zoom">Scale, ranges 0~24</param>
        /// <returns>Common image of offline map, null if failed to query</returns>
        /// \~Chinese
        /// <summary>
        /// 获取离线地图图像
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
        /// Get copyright information of offline map
        /// </summary>
        /// <returns>Copyright information of offline map</returns>
        /// \~Chinese
        /// <summary>
        /// 获取离线地图的版权信息
        /// </summary>
        /// <returns>离线地图的版权信息</returns>
        public static String GetOfflineMapCopyrightInfo()
        {
            return Handler.GetOfflineMapCopyrightInfo();
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
        public static String[] GetPluginPackIDList()
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
        public static PluginPackInfo GetPluginPackInfo(String packID)
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
        public static Dictionary<String, Dictionary<String, String> > GetPluginThirdPartyNotices()
        {
            return Handler.GetPluginThirdPartyNotices();
        }

        /// \~English
        /// <summary>
        /// Get paths of recent project files
        /// </summary>
        /// <returns>Paths of recent project files</returns>
        /// \~Chinese
        /// <summary>
        /// 获取最近项目文件路径列表
        /// </summary>
        /// <returns>最近项目文件路径列表</returns>
        public static String[] GetRecentProjectPaths()
        {
            return Handler.GetRecentProjectPaths();
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
        public static String GetSessionPath(SessionIdentifier session)
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
        public static String GetSessionPublicDataPath(SessionIdentifier session)
        {
            return Handler.GetSessionPublicDataPath(session);
        }

        /// \~English
        /// <summary>
        /// Get all sub data paths
        /// </summary>
        /// <returns>Sub data paths, null if they don't exist</returns>
        /// \~Chinese
        /// <summary>
        /// 获取所有子数据目录的路径
        /// </summary>
        /// <returns>所有子数据目录的路径，目录不存在则为null</returns>
        public static String[] GetSubDataPaths()
        {
            return Handler.GetSubDataPaths();
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
        /// Get application's version info
        /// </summary>
        /// <returns>Dictionary. The key is version title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取软件版本信息总表
        /// </summary>
        /// <returns>软件版本信息总表</returns>
        public static Dictionary<String, Version> GetVersionTable()
        {
            return Handler.GetVersionTable();
        }

        /// \~English
        /// <summary>
        /// Get information of window class
        /// </summary>
        /// <param name="windowClassID">Window class ID</param>
        /// <param name="transformID">Transform ID, can be null</param>
        /// <returns>Information of window class, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 获取窗口组件信息
        /// </summary>
        /// <param name="windowClassID">窗口组件ID</param>
        /// <param name="transformID">分化ID，可为null</param>
        /// <returns>窗口组件信息，若未找到返回null</returns>
        public static WindowClassInfo GetWindowClassInfo(String windowClassID, String transformID)
        {
            return Handler.GetWindowClassInfo(windowClassID, transformID);
        }

        /// \~English
        /// <summary>
        /// Get information of all window classes
        /// </summary>
        /// <returns>Dictionary. The key is window class ID</returns>
        /// \~Chinese
        /// <summary>
        /// 获取窗口组件信息表
        /// </summary>
        /// <returns>窗口组件信息表，键为组件ID</returns>
        public static Dictionary<String, WindowClassInfo> GetWindowClassTable()
        {
            return Handler.GetWindowClassTable();
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.1) Install plugin (After installation, restart is still needed to activated it)
        /// </summary>
        /// <param name="dirPath">The directory containing plugin files</param>
        /// <returns>Whether any installation is performed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.1) 安装插件（安装完毕后也需要重启才生效）
        /// </summary>
        /// <param name="dirPath">插件包文件夹，或包含若干插件包的文件夹</param>
        /// <returns>是否安装了插件</returns>
        public static Task<bool> InstallPlugin(String dirPath)
        {
            return Handler.InstallPlugin(dirPath);
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
        /// (api:app=3.2.1) Create blank project
        /// </summary>
        /// <param name="force">Whether forced to create new project</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.1) 新建空白项目
        /// </summary>
        /// <param name="force">是否强制新建空白项目</param>
        /// <returns>是否成功新建项目</returns>
        public static Task<bool> NewProject(bool force)
        {
            return Handler.NewProject(force);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.3) Open a dialog
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel, etc.</param>
        /// <param name="dialogClassID">Dialog class ID</param>
        /// <param name="config">Configuration string</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.3) 打开对话框
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.WindowClass , WindowPanel等</param>
        /// <param name="dialogClassID">对话框组件ID</param>
        /// <param name="config">初始化配置</param>
        public static Task OpenDialog(object caller, String dialogClassID, String config)
        {
            return Handler.OpenDialog(caller, dialogClassID, config);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.1) Open project
        /// </summary>
        /// <param name="projectFile">Path of project file, set to null to load autosaved in the folder of application's configuration files</param>
        /// <param name="force">Whether forced to open the project</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.1) 打开项目
        /// </summary>
        /// <param name="projectFile">项目文件路径，若设为null则从autosave读取</param>
        /// <param name="force">是否强制打开项目</param>
        /// <returns>是否成功打开项目</returns>
        public static Task<bool> OpenProject(String projectFile, bool force)
        {
            return Handler.OpenProject(projectFile, force);
        }

        /// \~English
        /// <summary>
        /// Play mp3 audio
        /// </summary>
        /// <param name="mp3FileData">MP3 audio data</param>
        /// \~Chinese
        /// <summary>
        /// 播放MP3音频
        /// </summary>
        /// <param name="mp3FileData">MP3音频文件数据</param>
        public static void PlayMp3(byte[] mp3FileData)
        {
            Handler.PlayMp3(mp3FileData);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.1) Show a modal dialog for user to confirm
        /// </summary>
        /// <param name="msg">Message for user to confirm</param>
        /// <returns>Whether confirmed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.1) 弹出模态框显示确认信息
        /// </summary>
        /// <param name="msg">确认信息</param>
        /// <returns>是否得到确认</returns>
        public static Task<bool> PopupConfirm(String msg)
        {
            return Handler.PopupConfirm(msg);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.3) Show a modal dialog to display error message
        /// </summary>
        /// <param name="msg">Error message</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.3) 弹出模态框显示错误信息
        /// </summary>
        /// <param name="msg">错误信息</param>
        public static Task PopupError(String msg)
        {
            return Handler.PopupError(msg);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.3) Show a modal dialog to display notice
        /// </summary>
        /// <param name="msg">Notice</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.3) 弹出模态框显示提示信息
        /// </summary>
        /// <param name="msg">提示信息</param>
        public static Task PopupNotice(String msg)
        {
            return Handler.PopupNotice(msg);
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
        public static void PublishData(String dataID, byte[] data)
        {
            Handler.PublishData(dataID, data);
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
        /// Register graph panel type for the specified graph type
        /// </summary>
        /// <param name="graphType">Graph type</param>
        /// <param name="styleName">Style name to register</param>
        /// <param name="panelType">Graph panel type, which should be derived from UI framework's control base class, and implement ASEva.GraphPanel </param>
        /// \~Chinese
        /// <summary>
        /// 注册针对指定图表类型的可视化面板
        /// </summary>
        /// <param name="graphType">指定图表类型</param>
        /// <param name="styleName">面板样式名</param>
        /// <param name="panelType">面板类型，需要继承UI框架的控件基类，并实现 ASEva.GraphPanel </param>
        public static void RegisterGraphPanelForType(GraphType graphType, String styleName, Type panelType)
        {
            Handler.RegisterGraphPanelForType(graphType, styleName, panelType);
        }

        /// \~English
        /// <summary>
        /// Register graph panel type for the specified graph ID (higher priority than graph type)
        /// </summary>
        /// <param name="graphID">Graph ID</param>
        /// <param name="styleName">Style name to register</param>
        /// <param name="panelType">Graph panel type, which should be derived from UI framework's control base class, and implement ASEva.GraphPanel </param>
        /// \~Chinese
        /// <summary>
        /// 注册针对指定图表ID的可视化面板（比按图表类型注册的优先级更高）
        /// </summary>
        /// <param name="graphID">图表报告ID</param>
        /// <param name="styleName">面板样式名</param>
        /// <param name="panelType">面板类型，需要继承UI框架的控件基类，并实现 ASEva.GraphPanel </param>
        public static void RegisterGraphPanelForID(int graphID, String styleName, Type panelType)
        {
            Handler.RegisterGraphPanelForID(graphID, styleName, panelType);
        }

        /// \~English
        /// <summary>
        /// Register transformed dialog class through configuration string
        /// </summary>
        /// <param name="dialogClassID">Original dialog class's ID</param>
        /// <param name="config">Configuration string</param>
        /// <returns>Information of transformed dialog class</returns>
        /// \~Chinese
        /// <summary>
        /// 通过配置注册分化对话框组件
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
        /// Register transformed dialog class directly
        /// </summary>
        /// <param name="dialogClassID">Original dialog class's ID</param>
        /// <param name="transformDialogClass">Transformed dialog class object</param>
        /// <param name="defaultConfig">Default configuration string</param>
        /// <returns>Information of transformed dialog class</returns>
        /// \~Chinese
        /// <summary>
        /// 直接注册分化对话框组件
        /// </summary>
        /// <param name="dialogClassID">原对话框组件ID</param>
        /// <param name="transformDialogClass">分化对话框组件类</param>
        /// <param name="defaultConfig">默认的可用于分化的配置字符串</param>
        /// <returns>分化后的对话框组件信息</returns>
        public static DialogClassInfo RegisterTransformDialogClassDirectly(String dialogClassID, DialogClass transformDialogClass, String defaultConfig)
        {
            return Handler.RegisterTransformDialogClassDirectly(dialogClassID, transformDialogClass, defaultConfig);
        }

        /// \~English
        /// <summary>
        /// Register transformed window class through configuration string
        /// </summary>
        /// <param name="windowClassID">Original window class's ID</param>
        /// <param name="config">Configuration string</param>
        /// <returns>Information of transformed window class</returns>
        /// \~Chinese
        /// <summary>
        /// 通过配置注册分化窗口组件
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
        /// Register transformed window class directly
        /// </summary>
        /// <param name="windowClassID">Original window class's ID</param>
        /// <param name="transformWindowClass">Transformed window class object</param>
        /// <param name="defaultConfig">Default configuration string</param>
        /// <returns>Information of transformed window class</returns>
        /// \~Chinese
        /// <summary>
        /// 直接注册分化窗口组件
        /// </summary>
        /// <param name="windowClassID">原窗口组件ID</param>
        /// <param name="transformWindowClass">分化窗口组件类</param>
        /// <param name="defaultConfig">默认的可用于分化的配置字符串</param>
        /// <returns>分化后的窗口组件信息</returns>
        public static WindowClassInfo RegisterTransformWindowClassDirectly(String windowClassID, WindowClass transformWindowClass, String defaultConfig)
        {
            return Handler.RegisterTransformWindowClassDirectly(windowClassID, transformWindowClass, defaultConfig);
        }

        /// \~English
        /// <summary>
        /// Remove bus protocol
        /// </summary>
        /// <param name="fileID">Bus protocol file ID</param>
        /// \~Chinese
        /// <summary>
        /// 移除总线协议
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        public static void RemoveBusProtocolFile(BusProtocolFileID fileID)
        {
            Handler.RemoveBusProtocolFile(fileID);
        }

        /// \~English
        /// <summary>
        /// Remove video reference
        /// </summary>
        /// <param name="videoChannel">Video channel, 0~23 corresponding to A~X</param>
        /// \~Chinese
        /// <summary>
        /// 移除视频引用
        /// </summary>
        /// <param name="videoChannel">视频通道，0~23对应A~X</param>
        public static void RemoveProcessorVideoReference(int videoChannel)
        {
            Handler.RemoveProcessorVideoReference(videoChannel);
        }

        /// \~English
        /// <summary>
        /// Reset the handler for function calling from native plugins
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="funcID">Function ID</param>
        /// \~Chinese
        /// <summary>
        /// 移除供原生插件调用的应用层函数
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="nativeClassID">原生组件ID</param>
        /// <param name="funcID">函数ID</param>
        public static void ResetAppFunctionHandler(object caller, String nativeClassID, String funcID)
        {
            Handler.ResetAppFunctionHandler(caller, nativeClassID, funcID);
        }

        /// \~English
        /// <summary>
        /// Save project
        /// </summary>
        /// <param name="projectFile">Path of project file, set to null to write to autosave in the folder of application's configuration files</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 保存当前项目
        /// </summary>
        /// <param name="projectFile">项目文件路径，null表示保存至当前项目文件</param>
        /// <returns>是否成功保存项目</returns>
        public static bool SaveCurrentProject(String projectFile)
        {
            return Handler.SaveCurrentProject(projectFile);
        }

        /// \~English
        /// <summary>
        /// Transmitted received general raw data (only available in online mode)
        /// </summary>
        /// <param name="cpuTick">CPU tick while data arriving</param>
        /// <param name="channelID">General raw data's channel ID, corresponding to the first column of input/raw/raw.csv</param>
        /// <param name="values">Numeric data</param>
        /// <param name="binary">Binary data</param>
        /// \~Chinese
        /// <summary>
        /// 发送已获取的原始数据信息（仅在线模式可用）
        /// </summary>
        /// <param name="cpuTick">数据的到达时CPU计数</param>
        /// <param name="channelID">原始数据协议名称，对应input/raw/raw.csv首列文字</param>
        /// <param name="values">数值数据</param>
        /// <param name="binary">二进制数据</param>
        public static void SendRawDataWithCPUTick(ulong cpuTick, String channelID, double[] values, byte[] binary)
        {
            Handler.SendRawDataWithCPUTick(cpuTick, channelID, values, binary);
        }

        /// \~English
        /// <summary>
        /// Set handler for function calling from native plugins
        /// </summary>
        /// <param name="caller">The caller who calls this API, can be object of ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel , etc.</param>
        /// <param name="nativeClassID">Native class ID</param>
        /// <param name="funcID">Function ID</param>
        /// <param name="handler">Handler for function calling</param>
        /// \~Chinese
        /// <summary>
        /// 设置供原生插件调用的应用层函数
        /// </summary>
        /// <param name="caller">调用此API的对象，可为以下类型： ASEva.CommonWorkflow , ASEva.Plugin , ASEva.WindowClass , ASEva.DialogClass , ASEva.ConsoleClass , WindowPanel, ConfigPanel等</param>
        /// <param name="nativeClassID">原生组件ID</param>
        /// <param name="funcID">函数ID</param>
        /// <param name="handler">函数接口</param>
        public static void SetAppFunctionHandler(object caller, String nativeClassID, String funcID, AppFunctionHandler handler)
        {
            Handler.SetAppFunctionHandler(caller, nativeClassID, funcID, handler);
        }

        /// \~English
        /// <summary>
        /// Set audio volume
        /// </summary>
        /// <param name="volume">Audio volume (times)</param>
        /// \~Chinese
        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="volume">音量（倍数）</param>
        public static void SetAudioVolume(double volume)
        {
            Handler.SetAudioVolume(volume);
        }

        /// \~English
        /// <summary>
        /// Set whether to monitor that there's data in the specified channel
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0, etc.</param>
        /// <param name="monitoring">Whether to monitor (The function should be implemented by plugins, like audio alarm, UI flashing, etc.)</param>
        /// \~Chinese
        /// <summary>
        /// 设置是否监控指定通道有无数据
        /// </summary>
        /// <param name="id">监控ID，如：bus@1, video@0, audio, raw@xxx-v1, sample@xxx-v2@0等</param>
        /// <param name="monitoring">是否监控有无数据，通道监控的具体实现应由插件给出，如发出报警音、指示灯闪烁等</param>
        public static void SetChannelMonitoringFlag(String id, bool monitoring)
        {
            Handler.SetChannelMonitoringFlag(id, monitoring);
        }

        /// \~English
        /// <summary>
        /// Set whether to monitor that the specified channel's data is synchronized with time server
        /// </summary>
        /// <param name="id">Monitor ID, like bus@1, video@0, sample@xxx-v2@0, etc.</param>
        /// <param name="monitoring">Whether to monitor (The function should be implemented by plugins, like audio alarm, UI flashing, etc.)</param>
        /// \~Chinese
        /// <summary>
        /// 设置是否监控指定通道数据与授时服务器同步
        /// </summary>
        /// <param name="id">监控ID，如bus@1, video@0, sample@xxx-v2@0等</param>
        /// <param name="monitoring">是否监控数据与授时服务器同步，通道监控的具体实现应由插件给出，如发出报警音、指示灯闪烁等</param>
        public static void SetChannelServerSyncMonitoringFlag(String id, bool monitoring)
        {
            Handler.SetChannelServerSyncMonitoringFlag(id, monitoring);
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
        /// Set data path
        /// </summary>
        /// <param name="path">The data path</param>
        /// \~Chinese
        /// <summary>
        /// 设置数据目录的路径
        /// </summary>
        /// <param name="path">数据目录的路径</param>
        public static void SetDataPath(String path)
        {
            Handler.SetDataPath(path);
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
        /// Set sub data path
        /// </summary>
        /// <param name="subIndex">Index, ranges 0~3</param>
        /// <param name="path">Sub data path</param>
        /// \~Chinese
        /// <summary>
        /// 设置子数据目录的路径
        /// </summary>
        /// <param name="subIndex">子数据目录序号，0~3</param>
        /// <param name="path">子数据目录的路径</param>
        public static void SetSubDataPath(int subIndex, String path)
        {
            Handler.SetSubDataPath(subIndex, path);
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
        /// Start a process to open file, folder or web site
        /// </summary>
        /// <param name="target">Path of file, folder or web site</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// 启动进程以默认方式打开文件、文件夹或网址
        /// </summary>
        /// <param name="target">目标文件、文件夹或网址</param>
        /// <returns>是否成功打开</returns>
        public static bool StartProcess(String target)
        {
            return Handler.StartProcess(target);
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
        public static DataSubscriber SubscribeData(String dataID, int bufferLength, int timeout)
        {
            return Handler.SubscribeData(dataID, bufferLength, timeout);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.2.1) Try to terminate application
        /// </summary>
        /// <param name="force">Whether forced to terminate</param>
        /// <param name="autosave">Whether to save current project to autosave in the folder of application's configuration files</param>
        /// <returns>Whether successful</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.2.1) 尝试终止应用程序
        /// </summary>
        /// <param name="force">是否强制终止</param>
        /// <param name="autosave">是否保存当前工程至autosave至应用程序的配置文件目录</param>
        /// <returns>是否成功终止</returns>
        public static Task<bool> TerminateApp(bool force, bool autosave)
        {
            return Handler.TerminateApp(force, autosave);
        }

        /// \~English
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        /// <param name="packID">Plugin pack ID</param>
        /// <returns>Whether uninstalled</returns>
        /// \~Chinese
        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="packID">插件包ID</param>
        /// <returns>是否卸载了插件</returns>
        public static bool UninstallPlugin(String packID)
        {
            return Handler.UninstallPlugin(packID);
        }

        /// \~English
        /// <summary>
        /// Unregister window panel or config panel
        /// </summary>
        /// <param name="panel">Window panel or config panel object</param>
        /// \~Chinese
        /// <summary>
        /// 注销窗口面板或配置面板对象
        /// </summary>
        /// <param name="panel">窗口面板或配置面板对象</param>
        public static void UnregisterPanel(object panel)
        {
            Handler.UnregisterPanel(panel);
        }

        /// \~English
        /// <summary>
        /// Update the path of bus protocol file (Only single channel protocol supported)
        /// </summary>
        /// <param name="fileID">Bus protocol file ID</param>
        /// <param name="filePath">New path</param>
        /// <returns>Whether successfull, false if the file is not found or MD5 doesn't match</returns>
        /// \~Chinese
        /// <summary>
        /// 更新总线协议文件路径(仅支持单通道的情况)
        /// </summary>
        /// <param name="fileID">总线协议文件ID</param>
        /// <param name="filePath">新路径</param>
        /// <returns>是否成功更新，false表示未找到文件或MD5不匹配</returns>
        public static bool UpdateBusProtocolFilePath(BusProtocolFileID fileID, String filePath)
        {
            return Handler.UpdateBusProtocolFilePath(fileID, filePath);
        }
    }
}
