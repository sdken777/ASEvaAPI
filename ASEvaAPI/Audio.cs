using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASEva
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 在音频采集到数据后调用此接口
    /// </summary>
    public interface WaveReceiver
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 数据回调函数
        /// </summary>
        /// <param name="samples">音频样本数据</param>
        /// <param name="cpuSyncTime">样本数据的到达时CPU时间，单位秒，可通过 ASEva.Agency.GetCPUTime 获得</param>
        void OnData(short[] samples, double cpuSyncTime);
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 在音频回放需要数据时调用此接口
    /// </summary>
    public interface WaveProvider
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 数据回调函数
        /// </summary>
        /// <param name="sampleCount">需要的样本数</param>
        /// <returns>返回音频数据样本</returns>
        short[] GetData(int sampleCount);
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 音频设备信息
    /// </summary>
    public class AudioDeviceInfo
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 设备ID
        /// </summary>
        public String DeviceID { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 设备名称
        /// </summary>
        public String DeviceName { get; set; }
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 音频采集设备接口
    /// </summary>
    public interface AudioRecorder
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns>设备列表</returns>
        AudioDeviceInfo[] GetRecordDevices();

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 启动音频采集设备
        /// </summary>
        /// <param name="deviceID">设备ID</param>
        /// <param name="receiver">回调接口</param>
        /// <returns>设备对象，若启动失败返回null</returns>
        object StartRecordDevice(String deviceID, WaveReceiver receiver);

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 停止音频采集设备
        /// </summary>
        /// <param name="device">设备对象</param>
        void StopRecordDevice(object device);
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 音频回放设备接口
    /// </summary>
    public interface AudioReplayer
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns>设备列表</returns>
        AudioDeviceInfo[] GetReplayDevices();

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 启动音频回放设备
        /// </summary>
        /// <param name="deviceID">设备ID</param>
        /// <param name="provider">回调接口</param>
        /// <returns>设备对象，若启动失败返回null</returns>
        object StartReplayDevice(String deviceID, WaveProvider provider);

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 停止音频回放设备
        /// </summary>
        /// <param name="device">设备对象</param>
        void StopReplayDevice(object device);
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 音频驱动信息
    /// </summary>
    public class AudioDriverInfo
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 设备ID
        /// </summary>
        public String DriverID { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 设备名称
        /// </summary>
        public String DriverName { get; set; }

    }
}
