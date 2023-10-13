using System;
using System.Collections.Generic;

namespace ASEva
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 对话框组件定义的基类
    /// </summary>
    public class DialogClass
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取对话框组件的名称时被调用
        /// </summary>
        /// <returns>对话框组件名称表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetDialogName() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取对话框组件的类别ID时被调用
        /// </summary>
        /// <returns>对话框组件类别ID</returns>
        public virtual String GetDialogClassID() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取对话框组件的图标图像时被调用
        /// </summary>
        /// <returns>图标图像（大小一般为16x16）</returns>
        public virtual object GetDialogImage() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现]  获取对话框组件相关的组件ID
        /// </summary>
        /// <returns>组件ID列表</returns>
        public virtual String[] GetRelatedModules() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取对话框组件相关的配置状态时被调用
        /// </summary>
        /// <returns>配置状态</returns>
        public virtual ConfigStatus GetRelatedConfigStatus() { return ConfigStatus.Disabled; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 查询对话框组件相关的各子功能配置状态时被调用
        /// </summary>
        /// <returns>各子功能的配置状态</returns>
        public virtual ConfigStatus[] GetRelatedChildConfigStatus() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [必须实现] 创建对话框控件时被调用
        /// </summary>
        /// <returns>配置界面控件，该控件将填满对话框</returns>
        public virtual object CreateDialogPanel() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 根据对话框配置返回分化的对话框组件定义
        /// </summary>
        /// <param name="config">对话框配置</param>
        /// <returns>分化的对话框组件定义</returns>
        public virtual DialogClass Transform(String config) { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [分化的对话框组件必须实现] 获取分化标识ID时被调用
        /// </summary>
        /// <returns>分化标识ID</returns>
        public virtual String GetTransformID() { return null; }
    }
}
