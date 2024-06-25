using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Base class for dialog component definition
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 对话框组件定义的基类
    /// </summary>
    public class DialogClass
    {
        /// \~English
        /// <summary>
        /// (api:app=3.1.6) [Required] Called while getting component's name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.1.6) [必须实现] 获取对话框组件的名称时被调用
        /// </summary>
        public virtual Dictionary<Language, String> GetDialogName() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting component's class ID
        /// </summary>
        /// <returns>Dialog class ID</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取对话框组件的类别ID时被调用
        /// </summary>
        /// <returns>对话框组件类别ID</returns>
        public virtual String GetDialogClassID() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting dialog's icon
        /// </summary>
        /// <returns>Icon image (size should be 16x16)</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取对话框组件的图标图像时被调用
        /// </summary>
        /// <returns>图标图像（大小一般为16x16）</returns>
        public virtual object GetDialogImage() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting class IDs of related components
        /// </summary>
        /// <returns>Class IDs of related components</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取对话框组件相关的组件ID
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
        /// (api:app=3.1.4) [必须实现] 获取对话框组件相关的配置状态时被调用
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
        /// (api:app=3.1.4) [可选实现] 查询对话框组件相关的各子功能配置状态时被调用
        /// </summary>
        /// <returns>各子功能的配置状态</returns>
        public virtual Task<ConfigStatus[]> GetRelatedChildConfigStatus() { return Task.FromResult<ConfigStatus[]>(null); }

        /// \~English
        /// <summary>
        /// [Required] Called to create configuration panel
        /// </summary>
        /// <returns>Configuration panel to fill the dialog's client area</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 创建对话框控件时被调用
        /// </summary>
        /// <returns>配置界面控件，该控件将填满对话框</returns>
        public virtual object CreateDialogPanel() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Return transformed dialog class according to the configuration string
        /// </summary>
        /// <param name="config">Configuration string</param>
        /// <returns>Definition of transformed dialog class</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 根据对话框配置返回分化的对话框组件定义
        /// </summary>
        /// <param name="config">对话框配置</param>
        /// <returns>分化的对话框组件定义</returns>
        public virtual DialogClass Transform(String config) { return null; }

        /// \~English
        /// <summary>
        /// [Required for transformed dialog] Called while getting transform ID
        /// </summary>
        /// <returns>Transform ID</returns>
        /// \~Chinese
        /// <summary>
        /// [分化的对话框组件必须实现] 获取分化标识ID时被调用
        /// </summary>
        /// <returns>分化标识ID</returns>
        public virtual String GetTransformID() { return null; }
    }
}
