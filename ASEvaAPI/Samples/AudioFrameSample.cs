using System;
using ASEva.Utility;

namespace ASEva.Samples
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Audio frame sample, 44100Hz, 16bit, mono channel
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 音频帧样本，44100Hz，16bit，单通道
    /// </summary>
    public class AudioFrameSample : Sample
    {
        /// \~English
        /// <summary>
        /// Sample data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 音频帧数据
        /// </summary>
        public short[] Data
        {
            get { return data; }
            set
            {
                data = value;
                if (data.Length == 0) MD5 = null;
                else
                {
                    var buffer = new byte[data.Length * 2];
                    if (buffer.Length > 0) Buffer.BlockCopy(data, 0, buffer, 0, buffer.Length);
                    MD5 = MD5Calculator.Calculate(buffer);
                }
            }
        }

        /// \~English
        /// <summary>
        /// MD5 of the sample data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 音频帧数据的MD5
        /// </summary>
        public String? MD5 { get; private set; }

        public AudioFrameSample()
        {
            data = [];
        }

        private short[] data;
    }
}
