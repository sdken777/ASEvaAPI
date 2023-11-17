using System;
using System.Collections.Generic;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=2.15.0) Base class for console component definition
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.15.0) 控制台组件定义的基类
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
        /// (api:app=2.15.1) [Required] Called while getting configuration status of related components
        /// </summary>
        /// <returns>Configuration status</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.1) [必须实现] 获取控制台组件相关的配置状态时被调用
        /// </summary>
        /// <returns>配置状态</returns>
        public virtual ConfigStatus GetRelatedConfigStatus() { return ConfigStatus.Disabled; }

        /// \~English
        /// <summary>
        /// (api:app=2.15.1) [Optional] Called while getting child configuration status of related components
        /// </summary>
        /// <returns>Child configuration status</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.15.1) [可选实现] 查询控制台组件相关的各子功能配置状态时被调用
        /// </summary>
        /// <returns>各子功能的配置状态</returns>
        public virtual ConfigStatus[] GetRelatedChildConfigStatus() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while running console procedure
        /// </summary>
        /// <param name="io">Console interaction interface</param>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 运行控制台过程时被调用
        /// </summary>
        /// <param name="io">控制台交互接口</param>
        public virtual void RunConsole(ConsoleIO io) { }
    }

    /// \~English
    /// <summary>
    /// (api:app=2.15.0) Console interaction interface
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.15.0) 控制台交互接口
    /// </summary>
    public interface ConsoleIO
    {
        /// \~English
        /// <summary>
        /// Print message
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 打印消息
        /// </summary>
        void Print(String message);

        /// \~English
        /// <summary>
        /// Get text input
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <param name="defaultString">Default text to be returned if no input</param>
        /// <returns>Text input by user</returns>
        /// \~Chinese
        /// <summary>
        /// 获取字符串输入
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <param name="defaultString">默认字符串，若不输入则返回此字符串</param>
        /// <returns>用户输入的字符串</returns>
        String InputString(String requestID, String message, String defaultString);

        /// \~English
        /// <summary>
        /// Get number input
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <param name="defaultValue">Default value to be returned if no input or the input is invalid</param>
        /// <returns>Number input by user</returns>
        /// \~Chinese
        /// <summary>
        /// 获取数值输入
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <param name="defaultValue">默认数值，若不输入或输入无效则返回此数值</param>
        /// <returns>用户输入的数值</returns>
        double InputNumber(String requestID, String message, double defaultValue);

        /// \~English
        /// <summary>
        /// Request user to confirm
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <returns>Whether user confirmed</returns>
        /// \~Chinese
        /// <summary>
        /// 请求用户确认
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <returns>用户是否确认</returns>
        bool Confirm(String requestID, String message);

        /// \~English
        /// <summary>
        /// Single selection
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <param name="options">All options</param>
        /// <param name="initialSelected">Index of initial selected option</param>
        /// <returns>Index of option selected by user, -1 only when options is empty</returns>
        /// \~Chinese
        /// <summary>
        /// 单项选择
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <param name="options">所有选项</param>
        /// <param name="initialSelected">开始时即选中的选项序号</param>
        /// <returns>用户勾选的选项序号，仅当options为空时返回-1</returns>
        int SingleSelect(String requestID, String message, String[] options, int initialSelected);

        /// \~English
        /// <summary>
        /// Multiple selection
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <param name="options">All options</param>
        /// <param name="initialSelection">Index of all initial selected options</param>
        /// <returns>Index of all options selected by the user</returns>
        /// \~Chinese
        /// <summary>
        /// 多项选择
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <param name="options">所有选项</param>
        /// <param name="initialSelection">开始时即选中的所有选项序号</param>
        /// <returns>用户勾选的所有选项序号</returns>
        int[] MultiSelect(String requestID, String message, String[] options, int[] initialSelection);

        /// \~English
        /// <summary>
        /// Select file for opening
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <param name="extensions">Suffix filtering, starts with '.', or set to null not specified</param>
        /// <returns>Selected file's path, null if user cancelled</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件用于打开
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <param name="extensions">后缀名筛选，以'.'开头，若不限后缀则设为null</param>
        /// <returns>用户选中文件的路径，若取消则返回null</returns>
        String SelectOpenFile(String requestID, String message, String[] extensions);

        /// \~English
        /// <summary>
        /// Select file for saving
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <param name="extension">Suffix of the file to save, starts with '.', or set to null not specified</param>
        /// <returns>Selected file's path, null if user cancelled</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件用于保存
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <param name="extension">保存文件的后缀名，以'.'开头，若不考虑后缀则设为null</param>
        /// <returns>用户选中文件的路径，若取消则返回null</returns>
        String SelectSaveFile(String requestID, String message, String extension);

        /// \~English
        /// <summary>
        /// Select folder
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <returns>Selected folder's path, null if user cancelled</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <returns>用户选中文件夹的路径，若取消则返回null</returns>
        String SelectFolder(String requestID, String message);

        /// \~English
        /// <summary>
        /// Select a file and load the data, only for small files
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <param name="extensions">Suffix filtering, starts with '.', or set to null not specified</param>
        /// <returns>Binary data of the file selected by user, null if user cancelled</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件并读取内容，仅适用于小文件
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <param name="extensions">后缀名筛选，以'.'开头，若不限后缀则设为null</param>
        /// <returns>用户选中文件的二进制数据，若取消则返回null</returns>
        byte[] LoadFileData(String requestID, String message, String[] extensions);

        /// \~English
        /// <summary>
        /// Select a file and save the data, only for small files
        /// </summary>
        /// <param name="requestID">Request ID, for distinguish different request. Set to null if unnecessary</param>
        /// <param name="message">Title message</param>
        /// <param name="extension">Suffix of the file to save, starts with '.', or set to null not specified</param>
        /// <param name="data">Binary data to save to the file</param>
        /// <returns>Whether it's successfully saved</returns>
        /// \~Chinese
        /// <summary>
        /// 选择文件并保存内容，仅适用于小文件
        /// </summary>
        /// <param name="requestID">请求ID，用于区分不同请求，若不需要则设为null</param>
        /// <param name="message">提示消息</param>
        /// <param name="extension">保存文件的后缀名，以'.'开头，若不考虑后缀则设为null</param>
        /// <param name="data">需要保存的二进制数据</param>
        /// <returns>是否成功保存</returns>
        bool SaveFileData(String requestID, String message, String extension, byte[] data);
    }
}