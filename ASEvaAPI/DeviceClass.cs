using System;
using System.Collections.Generic;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.8.0) 设备组件定义的基类
    /// </summary>
    public class DeviceClass
    {
        /// <summary>
        /// [必须实现] 获取设备组件的名称时被调用
        /// </summary>
        /// <returns>设备组件名称表，键'en'表示英文，'ch'表示中文</returns>
        public virtual Dictionary<String, String> GetDeviceName() { return null; }

        /// <summary>
        /// [必须实现] 获取设备组件的类别ID时被调用
        /// </summary>
        /// <returns>设备组件类别ID</returns>
        public virtual String GetDeviceClassID() { return null; }

        /// <summary>
        /// [必须实现] 创建配置对象时被调用。若不实现则为空配置，常时启用状态
        /// </summary>
        /// <returns>配置对象</returns>
        public virtual ModuleConfig CreateConfig() { return null; }

        /// <summary>
        /// [必须实现] 配置设备，在函数内部根据配置实现连接、断开、或重连（仅在非Session时段被调用）
        /// </summary>
        /// <param name="config">配置对象，应包含是否连接设备的配置</param>
        public virtual void SetDeviceConnection(ModuleConfig config) {}

        /// <summary>
        /// [必须实现] 断开连接（在程序关闭前被调用）
        /// </summary>
        public virtual void DisconnectDevice() {}

        /// <summary>
        /// [必须实现] 获取设备连接状态
        /// </summary>
        /// <param name="info">输出设备连接信息</param>
        /// <returns>返回设备状态信息</returns>
		public virtual GeneralDeviceStatus GetDeviceStatus(out String info) { info = null; return GeneralDeviceStatus.None; }

        /// <summary>
        /// [可选实现] 获取各子设备的连接状态
        /// </summary>
        /// <returns>返回各子设备的连接状态，数组长度即子设备数量</returns>
		public virtual GeneralDeviceStatus[] GetChildDeviceStatus() { return null; }
    }
}
