using System;
using System.Collections.Generic;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 窗口组件定义的基类
    /// </summary>
    public class WindowClass
    {
        /// <summary>
        /// [必须实现] 获取窗口组件的名称时被调用
        /// </summary>
        /// <returns>窗口组件名称表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetWindowName() { return null; }

        /// <summary>
        /// [必须实现] 获取窗口组件的类别ID时被调用
        /// </summary>
        /// <returns>窗口组件的类别ID</returns>
        public virtual String GetWindowClassID() { return null; }

        /// <summary>
        /// [可选实现] 获取窗口组件的图标图像时被调用
        /// </summary>
        /// <returns>图标图像（大小一般为16x16）</returns>
        public virtual object GetWindowImage() { return null; }

        /// <summary>
        /// [可选实现] 获取窗口组件相关的组件ID
        /// </summary>
        /// <returns>组件ID列表</returns>
        public virtual String[] GetRelatedModules() { return null; }

        /// <summary>
        /// [必须实现] 创建窗口控件时被调用
        /// </summary>
        /// <returns>窗口控件，该控件将填满显示窗口</returns>
        public virtual object CreateWindow() { return null; }

        /// <summary>
        /// [可选实现] 查询是否支持显示多个窗口时被调用
        /// </summary>
        /// <returns>是否支持显示多个窗口，默认为false</returns>
        public virtual bool IsMultipleWindowSupported() { return false; }

        /// <summary>
        /// [可选实现] 获取可直接作为样本输出的原始数据协议列表及对应的样本别名
        /// </summary>
        /// <returns>原始数据协议（键）列表及对应的样本别名（值）</returns>
        public virtual Dictionary<String, String> GetRawToSampleProtocols() { return null; }

        /// <summary>
        /// [可选实现] 根据窗口配置返回分化的窗口组件定义
        /// </summary>
        /// <param name="config">窗口配置</param>
        /// <returns>分化的窗口组件定义</returns>
        public virtual WindowClass Transform(String config) { return null; }

        /// <summary>
        /// [分化的窗口组件必须实现] 获取分化标识ID时被调用
        /// </summary>
        /// <returns>分化标识ID</returns>
        public virtual String GetTransformID() { return null; }
    }
}
