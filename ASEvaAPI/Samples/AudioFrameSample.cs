using System;
using ASEva.Utility;

namespace ASEva.Samples
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 音频帧样本，44100Hz，16bit，单通道
    /// </summary>
    public class AudioFrameSample : Sample
    {
        /// \~English
        /// 
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
                if (data == null) MD5 = null;
                else
                {
                    var buffer = new byte[data.Length * 2];
                    if (buffer.Length > 0) Buffer.BlockCopy(data, 0, buffer, 0, buffer.Length);
                    MD5 = MD5Calculator.Calculate(buffer);
                }
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.4) 音频帧数据的MD5
        /// </summary>
        /// <value></value>
        public String MD5 { get; private set; }

        private short[] data;
    }
}
