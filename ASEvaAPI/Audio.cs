using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Use this interface to output audio data while collecting
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 在音频采集到数据后调用此接口
    /// </summary>
    public interface WaveReceiver
    {
        /// \~English
        /// <summary>
        /// Data callback function
        /// </summary>
        /// <param name="samples">Audio samples</param>
        /// <param name="cpuSyncTime">CPU time when the samples arrived. Use ASEva.Agency.GetCPUTime to get the CPU time</param>
        /// \~Chinese
        /// <summary>
        /// 数据回调函数
        /// </summary>
        /// <param name="samples">音频样本数据</param>
        /// <param name="cpuSyncTime">样本数据的到达时CPU时间，单位秒，可通过 ASEva.Agency.GetCPUTime 获得</param>
        void OnData(short[] samples, double cpuSyncTime);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Use this interface to input audio data while replaying
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 在音频回放需要数据时调用此接口
    /// </summary>
    public interface WaveProvider
    {
        /// \~English
        /// <summary>
        /// Data callback function
        /// </summary>
        /// <param name="sampleCount">Number of samples needed</param>
        /// <returns>Audio samples</returns>
        /// \~Chinese
        /// <summary>
        /// 数据回调函数
        /// </summary>
        /// <param name="sampleCount">需要的样本数</param>
        /// <returns>返回音频数据样本</returns>
        short[] GetData(int sampleCount);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.7.0) Audio device information
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.0) 音频设备信息
    /// </summary>
    public class AudioDeviceInfo(String id, String name)
    {
        /// \~English
        /// <summary>
        /// Device ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 设备ID
        /// </summary>
        public String DeviceID { get; set; } = id;

        /// \~English
        /// <summary>
        /// Device name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 设备名称
        /// </summary>
        public String DeviceName { get; set; } = name;
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Audio recorder interface
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 音频采集设备接口
    /// </summary>
    public interface AudioRecorder
    {
        /// \~English
        /// <summary>
        /// Get information of recorders
        /// </summary>
        /// <returns>Information of recorders</returns>
        /// \~Chinese
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns>设备列表</returns>
        AudioDeviceInfo[] GetRecordDevices();

        /// \~English
        /// <summary>
        /// Start recording
        /// </summary>
        /// <param name="deviceID">Device ID</param>
        /// <param name="receiver">Callback interface</param>
        /// <returns>Device object, null if failed to start</returns>
        /// \~Chinese
        /// <summary>
        /// 启动音频采集设备
        /// </summary>
        /// <param name="deviceID">设备ID</param>
        /// <param name="receiver">回调接口</param>
        /// <returns>设备对象，若启动失败返回null</returns>
        object? StartRecordDevice(String deviceID, WaveReceiver receiver);

        /// \~English
        /// <summary>
        /// Stop recording
        /// </summary>
        /// <param name="device">Device object</param>
        /// \~Chinese
        /// <summary>
        /// 停止音频采集设备
        /// </summary>
        /// <param name="device">设备对象</param>
        void StopRecordDevice(object device);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Audio player interface
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 音频回放设备接口
    /// </summary>
    public interface AudioReplayer
    {
        /// \~English
        /// <summary>
        /// Get information of players
        /// </summary>
        /// <returns>Information of players</returns>
        /// \~Chinese
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns>设备列表</returns>
        AudioDeviceInfo[] GetReplayDevices();

        /// \~English
        /// <summary>
        /// Start replaying
        /// </summary>
        /// <param name="deviceID">Device ID</param>
        /// <param name="provider">Callback interface</param>
        /// <returns>Device object, null if failed to start</returns>
        /// \~Chinese
        /// <summary>
        /// 启动音频回放设备
        /// </summary>
        /// <param name="deviceID">设备ID</param>
        /// <param name="provider">回调接口</param>
        /// <returns>设备对象，若启动失败返回null</returns>
        object? StartReplayDevice(String deviceID, WaveProvider provider);

        /// \~English
        /// <summary>
        /// Stop replaying
        /// </summary>
        /// <param name="device">Device object</param>
        /// \~Chinese
        /// <summary>
        /// 停止音频回放设备
        /// </summary>
        /// <param name="device">设备对象</param>
        void StopReplayDevice(object device);
    }

    /// \~English
    /// <summary>
    /// (api:app=3.7.0) Audio driver information
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.0) 音频驱动信息
    /// </summary>
    public class AudioDriverInfo(String id, String name)
    {
        /// \~English
        /// <summary>
        /// Driver ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 驱动ID
        /// </summary>
        public String DriverID { get; set; } = id;

        /// \~English
        /// <summary>
        /// Driver name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 驱动名称
        /// </summary>
        public String DriverName { get; set; } = name;
    }
}
