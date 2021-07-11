using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.0.0) 写入样本csv文件
    /// </summary>
    public class SampleCsvWriter
    {
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
                if (fileNameComps.Length >= 2) output.channel = Convert.ToInt32(fileNameComps[1]);

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

        /// <summary>
        /// 写入一个通用样本
        /// </summary>
        /// <param name="sample">通用样本</param>
        /// <returns>若该通用样本协议或session不一致则返回false</returns>
        public bool Write(GeneralSample sample)
        {
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
            if (startTimeLocal == null) list.Add("na");
            else list.Add((startTimeLocal.Value + (long)(1000.0 * sample.Offset * timeRatioToLocal)).ToString());
            if (startTimeUTC == null) list.Add("na");
            else list.Add((startTimeUTC.Value + (long)(1000.0 * sample.Offset * timeRatioToUTC)).ToString());
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

        /// <summary>
        /// 设置Session开始时的UTC时间，默认为空
        /// </summary>
        public DateTime? StartTimeUTC
        {
            set
            {
                if (value == null) startTimeUTC = null;
                else startTimeUTC = (long)(value.Value - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            }
        }

        /// <summary>
        /// 设置Session开始时的本地时间，默认为空
        /// </summary>
        public DateTime? StartTimeLocal
        {
            set
            {
                if (value == null) startTimeLocal = null;
                else startTimeLocal = (long)(TimeZoneInfo.ConvertTimeToUtc(value.Value, TimeZoneInfo.Local) - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            }
        }

        /// <summary>
        /// 设置相对时间转为UTC时间的时间比例，默认为1
        /// </summary>
        public double TimeRatioToUTC
        {
            set
            {
                timeRatioToUTC = value;
            }
        }

        /// <summary>
        /// 设置相对时间转为本地时间的时间比例，默认为1
        /// </summary>
        public double TimeRatioToLocal
        {
            set
            {
                timeRatioToLocal = value;
            }
        }

        private StreamWriter writer = null;
        private String protocol = null;
        private int? channel = null;
        private DateTime? session = null;
        private long? startTimeUTC = null;
        private long? startTimeLocal = null;
        private double timeRatioToUTC = 1;
        private double timeRatioToLocal = 1;

        private SampleCsvWriter()
        { }
    }
}
