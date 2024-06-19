using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Base class for console component definition
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 控制台组件定义的基类
    /// </summary>
    public class ConsoleClass
    {
        /// \~English
        /// <summary>
        /// [Required] Called while getting component's name
        /// </summary>
        /// <returns>Dictionary. The key 'en' is English, 'ch' is Chinese</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取控制台组件的名称时被调用
        /// </summary>
        /// <returns>控制台组件名称表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetConsoleName() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting component's class ID
        /// </summary>
        /// <returns>Console class ID</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取控制台组件的类别ID时被调用
        /// </summary>
        /// <returns>控制台组件类别ID</returns>
        public virtual String GetConsoleClassID() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting class IDs of related components
        /// </summary>
        /// <returns>Class IDs of related components</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取控制台组件相关的组件ID
        /// </summary>
        /// <returns>组件ID列表</returns>
        public virtual String[] GetRelatedModules() { return null; }

        /// \~English
        /// <summary>
        /// (api:app=3.1.4) [Required] Called while getting configuration status of related components
        /// </summary>
        /// <returns>Configuration status</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.4) [必须实现] 获取控制台组件相关的配置状态时被调用
        /// </summary>
        /// <returns>配置状态</returns>
        public virtual Task<ConfigStatus> GetRelatedConfigStatus() { return Task.FromResult(ConfigStatus.Disabled); }

        /// \~English
        /// <summary>
        /// (api:app=3.1.4) [Optional] Called while getting child configuration status of related components
        /// </summary>
        /// <returns>Child configuration status</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.4) [可选实现] 查询控制台组件相关的各子功能配置状态时被调用
        /// </summary>
        /// <returns>各子功能的配置状态</returns>
        public virtual Task<ConfigStatus[]> GetRelatedChildConfigStatus() { return Task.FromResult<ConfigStatus[]>(null); }

        /// \~English
        /// <summary>
        /// (api:app=3.1.0) [Required] Called while running console procedure
        /// </summary>
        /// <param name="io">Console interaction interface. The procedure should end immediately if any method of this interface that returns false (interrupted)</param>
        /// <param name="machineText">Whether the text of messages and options given to the interaction interface should be ID or JSON string that is easy to parse by the machine. Otherwise it should be text that is easy to read by humans</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.0) [必须实现] 运行控制台过程时被调用
        /// </summary>
        /// <param name="io">控制台交互接口，在运行过程中次此接口的任何方法返回false(中断)都应该立即结束</param>
        /// <param name="machineText">输入至交互接口的消息、选项等文本是否应该为便于机器解析的ID或JSON字符串等，否则为便于人阅读的文本</param>
        public virtual Task RunConsole(ConsoleIO io, bool machineText) { return Task.CompletedTask; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Result of saving or loading file data through console
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 通过控制台读写文件的结果
    /// </summary>
    public class ConsoleFileResult
    {
        /// \~English
        /// <summary>
        /// Whether successfully saved or loaded file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否成功读写文件
        /// </summary>
        public bool OK { get; set; }

        /// \~English
        /// <summary>
        /// The target file's path
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 目标文件路径
        /// </summary>
        public String FilePath { get; set; }

        /// \~English
        /// <summary>
        /// Whether it's a client-side file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否为客户端文件
        /// </summary>
        public bool IsClientFile { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.1.0) Console interaction interface
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.1.0) 控制台交互接口
    /// </summary>
    public interface ConsoleIO
    {
        /// \~English
        /// <summary>
        /// Print message
        /// </summary>
        /// <param name="message">The message</param>
        /// \~Chinese
        /// <summary>
        /// 打印消息
        /// </summary>
        /// <param name="message">消息</param>
        void Print(String message);

        /// \~English
        /// <summary>
        /// Get text input
        /// </summary>
        /// <param name="message">Title message</param>
        /// <param name="defaultString">Default string</param>
        /// <returns>1. False if interrupted. 2. The text input by user (return the default string if no input)</returns>
        /// \~Chinese
        /// <summary>
        /// 获取字符串输入
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="defaultString">默认字符串</param>
        /// <returns>1. 若中断则返回false; 2. 用户输入的字符串(若不输入则为其默认值)</returns>
        Task<Tuple<bool, String>> InputString(String message, String defaultString);

        /// \~English
        /// <summary>
        /// Get number input
        /// </summary>
        /// <param name="message">Title message</param>
        /// <param name="defaultNumber">Default value</param>
        /// <returns>1. False if interrupted. 2. The number input by user (return the default value if no input or the input is invalid)</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数值输入
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="defaultNumber">默认值</param>
        /// <returns>1. 若中断则返回false; 2. 用户输入的数值(若不输入或输入无效则为其默认值)</returns>
        Task<Tuple<bool, double>> InputNumber(String message, double defaultNumber);

        /// \~English
        /// <summary>
        /// Request user to confirm
        /// </summary>
        /// <param name="message">Title message</param>
        /// <returns>1. False if interrupted. 2. Whether user confirmed</returns>
        /// \~Chinese
        /// <summary>
        /// 请求用户确认
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <returns>1. 若中断则返回false; 2. 用户是否确认</returns>
        Task<Tuple<bool, bool>> Confirm(String message);

        /// \~English
        /// <summary>
        /// Single selection
        /// </summary>
        /// <param name="message">Title message</param>
        /// <param name="options">All options</param>
        /// <param name="defaultIndex">Index of initial selected option</param>
        /// <returns>1. False if interrupted. 2. The index of option selected by user (-1 only when options is empty)</returns>
        /// \~Chinese
        /// <summary>
        /// 单项选择
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="options">所有选项</param>
        /// <param name="defaultIndex">开始时即选中的选项序号</param>
        /// <returns>1. 若中断则返回false; 2. 用户勾选的选项序号(仅当options为空时返回-1)</returns>
        Task<Tuple<bool, int>> SingleSelect(String message, String[] options, int defaultIndex);

        /// \~English
        /// <summary>
        /// Multiple selection
        /// </summary>
        /// <param name="message">Title message</param>
        /// <param name="options">All options</param>
        /// <param name="defaultIndices">Index of all options selected by the user</param>
        /// <returns>1. False if interrupted. 2. The index of all options selected by the user</returns>
        /// \~Chinese
        /// <summary>
        /// 多项选择
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="options">所有选项</param>
        /// <param name="defaultIndices">开始时即选中的所有选项序号</param>
        /// <returns>1. 若中断则返回false; 2. 用户勾选的所有选项序号</returns>
        Task<Tuple<bool, int[]>> MultiSelect(String message, String[] options, int[] defaultIndices);

        /// \~English
        /// <summary>
        /// Select file for opening
        /// </summary>
        /// <param name="message">Title message</param>
        /// <param name="extensions">Suffix filtering, starts with '.', or set to null not specified</param>
        /// <returns>1. False if interrupted. 2. Selected file's path, null if user cancelled</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件用于打开
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="extensions">后缀名筛选，以'.'开头，若不限后缀则设为null</param>
        /// <returns>1. 若中断则返回false; 2. 用户选中文件的路径，若取消则为null</returns>
        Task<Tuple<bool, String>> SelectOpenFile(String message, String[] extensions);

        /// \~English
        /// <summary>
        /// Select file for saving
        /// </summary>
        /// <param name="message">Title message</param>
        /// <param name="extension">Suffix of the file to save, starts with '.', or set to null not specified</param>
        /// <returns>1. False if interrupted. 2. Selected file's path, null if user cancelled</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件用于保存
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="extension">保存文件的后缀名，以'.'开头，若不考虑后缀则设为null</param>
        /// <returns>1. 若中断则返回false; 2. 用户选中文件的路径，若取消则为null</returns>
        Task<Tuple<bool, String>> SelectSaveFile(String message, String extension);

        /// \~English
        /// <summary>
        /// Select folder
        /// </summary>
        /// <param name="message">Title message</param>
        /// <returns>1. False if interrupted. 2. Selected folder's path, null if user cancelled</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <returns>1. 若中断则返回false; 2. 用户选中文件夹的路径，若取消则为null</returns>
        Task<Tuple<bool, String>> SelectFolder(String message);

        /// \~English
        /// <summary>
        /// Select a file and load the data, only for small files
        /// </summary>
        /// <param name="message">Title message</param>
        /// <param name="extensions">Suffix filtering, starts with '.', or set to null not specified</param>
        /// <returns>1. False if interrupted. 2. Binary data of the file selected by user, null if user cancelled. 3. Result of loading file</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件并读取内容，仅适用于小文件
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="extensions">后缀名筛选，以'.'开头，若不限后缀则设为null</param>
        /// <returns>1. 若中断则返回false; 2. 用户选中文件的二进制数据，若取消则输出null; 3. 读文件结果</returns>
        Tuple<bool, byte[], ConsoleFileResult> LoadFileData(String message, String[] extensions);

        /// \~English
        /// <summary>
        /// Select a file and save the data, only for small files
        /// </summary>
        /// <param name="message">Title message</param>
        /// <param name="extension">Suffix of the file to save, starts with '.', or set to null not specified</param>
        /// <param name="data">Binary data to save to the file</param>
        /// <returns>1. False if interrupted. 2. Result of saving file</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件并保存内容，仅适用于小文件
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="extension">保存文件的后缀名，以'.'开头，若不考虑后缀则设为null</param>
        /// <param name="data">需要保存的二进制数据</param>
        /// <returns>1. 若中断则返回false; 2. 写文件结果</returns>
        Tuple<bool, ConsoleFileResult> SaveFileData(String message, String extension, byte[] data);
    }
}