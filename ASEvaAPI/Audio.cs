using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 在音频采集到数据后调用此接口
    /// </summary>
    public interface WaveReceiver
    {
        void OnData(short[] samples, double cpuSyncTime/* second */);
    }

    /// <summary>
    /// (api:app=2.0.0) 在音频回放需要数据时调用此接口
    /// </summary>
    public interface WaveProvider
    {
        short[] GetData(int sampleCount);
    }

    /// <summary>
    /// (api:app=2.0.0) 音频设备信息
    /// </summary>
    public class AudioDeviceInfo
    {
        public String DeviceID { get; set; }
        public String DeviceName { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 音频采集设备接口
    /// </summary>
    public interface AudioRecorder
    {
        AudioDeviceInfo[] GetRecordDevices();
        object StartRecordDevice(String deviceID, WaveReceiver receiver);
        void StopRecordDevice(object device);
    }

    /// <summary>
    /// (api:app=2.0.0) 音频回放设备接口
    /// </summary>
    public interface AudioReplayer
    {
        AudioDeviceInfo[] GetReplayDevices();
        object StartReplayDevice(String deviceID, WaveProvider provider);
        void StopReplayDevice(object device);
    }

    /// <summary>
    /// (api:app=2.0.0) 音频驱动信息
    /// </summary>
    public class AudioDriverInfo
    {
        public String DriverID { get; set; }
        public String DriverName { get; set; }

    }
}
