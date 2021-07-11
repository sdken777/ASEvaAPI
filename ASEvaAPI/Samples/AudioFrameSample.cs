using System;

namespace ASEva.Samples
{
    /// <summary>
    /// (api:app=2.0.0) 音频帧样本，44100Hz，16bit，单通道
    /// </summary>
    public class AudioFrameSample : Sample
    {
        /// <summary>
        /// 音频帧数据
        /// </summary>
        public short[] Data { get; set; }
    }
}
