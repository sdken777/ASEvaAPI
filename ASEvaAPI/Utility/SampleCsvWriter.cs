using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ASEva.Utility
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 写入样本csv文件
    /// </summary>
    public class SampleCsvWriter
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 根据指定路径创建样本csv写入器
        /// </summary>
        /// <param name="file">文件路径，文件名必须与样本协议ID一致，后缀为csv</param>
        /// <param name="overwrite">若文件已存在是否覆盖</param>
        /// <param name="titles"></param>
        /// <returns>csv写入器，若文件创建失败则返回空</returns>
        public static SampleCsvWriter CreateWriter(String file, bool overwrite, List<String> titles)
        {
            if (File.Exists(file) && !overwrite) return null;

            StreamWriter writer = null;
            try
            {
                var output = new SampleCsvWriter();

                var fileNameComps = Path.GetFileNameWithoutExtension(file).Split('@');
                output.protocol = fileNameComps[0];
                if (fileNameComps.Length >= 2)
                {
                    int channel;
                    if (!Int32.TryParse(fileNameComps[1], out channel)) return null;
                    output.channel = channel;
                }

                writer = new StreamWriter(file, false, Encoding.UTF8);

                var titleLine = "Session,LocalTime,UtcTime,Time";
                if (titles != null) titleLine += "," + String.Join(",", titles);
                writer.WriteLine(titleLine);

                output.writer = writer;
                return output;
            }
            catch (Exception)
            {
                if (writer != null) writer.Close();
                return null;
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 写入一个通用样本
        /// </summary>
        /// <param name="sample">通用样本</param>
        /// <returns>若该通用样本协议或session不一致则返回false</returns>
        public bool Write(GeneralSample sample)
        {
            if (sample == null || sample.Offset <= 0) return false;
            if (sample.Protocol != protocol) return false;
            if (sample.Channel == null)
            {
                if (channel != null) return false;
            }
            else
            {
                if (channel == null) return false;
                else if (sample.Channel.Value != channel.Value) return false;
            }
            if (session != null && sample.Base != session.Value) return false;

            session = sample.Base;

            var list = new List<String>();
            list.Add(sample.Base.ToString("yyyyMMdd-HH-mm-ss"));
            if (hostModel == null) list.Add("na");
            else list.Add((hostModel.StartPosix + (ulong)(1000.0 * sample.Offset * hostModel.TimeRatio)).ToString());
            if (gnssModel == null) list.Add("na");
            else list.Add((gnssModel.StartPosix + (ulong)(1000.0 * sample.Offset * gnssModel.TimeRatio)).ToString());
            list.Add(sample.Offset.ToString());

            int count = 0;
            foreach (var val in sample.Values)
            {
                if (++count > sample.NumberOfSignificants) break;
                list.Add(val.mode == GeneralSampleValueMode.Number ? val.number.ToString() : (val.mode == GeneralSampleValueMode.Invalid ? "na" : val.text));
            }
            writer.WriteLine(String.Join(",", list));

            return true;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 关闭样本csv写入器
        /// </summary>
        public void Close()
        {
            if (writer != null)
            {
                writer.Close();
                writer = null;
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 按UTC时间设置卫星Posix时间模型的session开始时间
        /// </summary>
        public DateTime? StartTimeUTC
        {
            set
            {
                if (value == null) gnssModel = null;
                else
                {
                    if (gnssModel == null) gnssModel = new PosixTimeModel();
                    gnssModel.StartPosix = (ulong)(value.Value - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
                }
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 按本地时间设置主机Posix时间模型的session开始时间
        /// </summary>
        public DateTime? StartTimeLocal
        {
            set
            {
                if (value == null) hostModel = null;
                else
                {
                    if (hostModel == null) hostModel = new PosixTimeModel();
                    hostModel.StartPosix = (ulong)(TimeZoneInfo.ConvertTimeToUtc(value.Value, TimeZoneInfo.Local) - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
                }
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 设置卫星Posix时间模型的时间比例，默认为1
        /// </summary>
        public double TimeRatioToUTC
        {
            set
            {
                if (gnssModel == null) gnssModel = new PosixTimeModel();
                gnssModel.TimeRatio = value;
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 设置主机Posix时间模型的时间比例，默认为1
        /// </summary>
        public double TimeRatioToLocal
        {
            set
            {
                if (hostModel == null) hostModel = new PosixTimeModel();
                hostModel.TimeRatio = value;
            }
        }

        private StreamWriter writer = null;
        private String protocol = null;
        private int? channel = null;
        private DateTime? session = null;
        private PosixTimeModel hostModel = null;
        private PosixTimeModel gnssModel = null;

        private SampleCsvWriter()
        { }
    }
}
