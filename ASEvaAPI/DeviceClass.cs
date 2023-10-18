using System;
using System.Collections.Generic;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=2.8.0) Base class for device component definition
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.8.0) 设备组件定义的基类
    /// </summary>
    public class DeviceClass
    {
        /// \~English
        /// <summary>
        /// [Required] Called while getting component's name
        /// </summary>
        /// <returns>Dictionary. The key 'en' is English, 'ch' is Chinese</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取设备组件的名称时被调用
        /// </summary>
        /// <returns>设备组件名称表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetDeviceName() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while getting component's class ID
        /// </summary>
        /// <returns>Device class ID</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取设备组件的类别ID时被调用
        /// </summary>
        /// <returns>设备组件类别ID</returns>
        public virtual String GetDeviceClassID() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Called while creating module configuration object. It will be empty configuration if not implemented, that is always enabled
        /// </summary>
        /// <returns>Configuration object</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 创建配置对象时被调用。若不实现则为空配置，常时启用状态
        /// </summary>
        /// <returns>配置对象</returns>
        public virtual ModuleConfig CreateConfig() { return null; }

        /// \~English
        /// <summary>
        /// [Required] Update the configuration (called only when idle). The function should implement connect, disconnect, reconnect based on the configuration
        /// </summary>
        /// <param name="config">Configuration object, which should include whether to connect the device</param>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 配置设备，在函数内部根据配置实现连接、断开、或重连（仅在非Session时段被调用）
        /// </summary>
        /// <param name="config">配置对象，应包含是否连接设备的配置</param>
        public virtual void SetDeviceConnection(ModuleConfig config) {}

        /// \~English
        /// <summary>
        /// [Required] Disconnect device (called while exiting application)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 断开连接（在程序关闭前被调用）
        /// </summary>
        public virtual void DisconnectDevice() {}

        /// \~English
        /// <summary>
        /// [Required] Called while getting device connection status
        /// </summary>
        /// <param name="info">Output information about the connection</param>
        /// <returns>Device connection status</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 获取设备连接状态
        /// </summary>
        /// <param name="info">输出设备连接信息</param>
        /// <returns>返回设备状态信息</returns>
		public virtual GeneralDeviceStatus GetDeviceStatus(out String info) { info = null; return GeneralDeviceStatus.None; }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting sub devices' connection status
        /// </summary>
        /// <returns>Sub devices' connection status, while the length of array is the number of sub devices</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取各子设备的连接状态
        /// </summary>
        /// <returns>返回各子设备的连接状态，数组长度即子设备数量</returns>
		public virtual GeneralDeviceStatus[] GetChildDeviceStatus() { return null; }
    }
}
