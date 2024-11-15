using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ASEva.Utility
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Sample CSV file writer
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 写入样本csv文件
    /// </summary>
    public class SampleCsvWriter : IDisposable
    {
        /// \~English
        /// <summary>
        /// Create sample CSV writer writing to the specified path
        /// </summary>
        /// <param name="file">File path, whose file name must be the same as sample's channel ID, suffixed with ".csv"</param>
        /// <param name="overwrite">Whether to overwrite if it exists</param>
        /// <param name="titles">Title of sample's fields</param>
        /// <returns>Sample CSV writer, null if failed to create</returns>
        /// \~Chinese
        /// <summary>
        /// 根据指定路径创建样本csv写入器
        /// </summary>
        /// <param name="file">文件路径，文件名必须与样本协议ID一致，后缀为csv</param>
        /// <param name="overwrite">若文件已存在是否覆盖</param>
        /// <param name="titles">样本标题</param>
        /// <returns>csv写入器，若文件创建失败则返回空</returns>
        public static SampleCsvWriter? CreateWriter(String file, bool overwrite, List<String> titles)
        {
            if (File.Exists(file) && !overwrite) return null;

            StreamWriter? writer = null;
            try
            {
                var fileNameComps = Path.GetFileNameWithoutExtension(file).Split('@');
                var protocol = fileNameComps[0];
                int? channel = null;
                if (fileNameComps.Length >= 2)
                {
                    int outChannel;
                    if (!Int32.TryParse(fileNameComps[1], out outChannel)) return null;
                    channel = outChannel;
                }

                writer = new StreamWriter(file, false, Encoding.UTF8);

                var titleLine = "Session,Sync State,CPU Tick,Host Posix,Guest Posix,Gnss Posix,Time";
                if (titles != null) titleLine += "," + String.Join(",", titles);
                writer.WriteLine(titleLine);

                var output = new SampleCsvWriter(writer, protocol);
                output.channel = channel;
                return output;
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                if (writer != null) writer.Close();
                return null;
            }
        }

        /// \~English
        /// <summary>
        /// Write a general sample to file
        /// </summary>
        /// <param name="sample">General sample object</param>
        /// <returns>Whether successful, false if the channel ID or session doesn't match</returns>
        /// \~Chinese
        /// <summary>
        /// 写入一个通用样本
        /// </summary>
        /// <param name="sample">通用样本</param>
        /// <returns>若该通用样本协议或session不一致则返回false</returns>
        public bool Write(GeneralSample sample)
        {
            if (sample.Offset <= 0) return false;
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
            if (session != null && sample.Session != session.Value) return false;

            session = sample.Session;

            var list = new List<String>();
            list.Add(sample.Session.ToDateTime().ToString("yyyyMMdd-HH-mm-ss"));

            if (sample.Timestamp.TimeInfo == null)
            {
                list.Add("NONE");
                list.Add("0");
                list.Add("0");
                list.Add("0");
                list.Add("0");
            }
            else
            {
                if (sample.OffsetSync == TimeOffsetSync.Server) list.Add("SERV");
                else if (sample.OffsetSync == TimeOffsetSync.Gnss) list.Add("GNSS");
                else if (sample.OffsetSync == TimeOffsetSync.BusReceiverArrival) list.Add("RECV");
                else if (sample.OffsetSync == TimeOffsetSync.Interpolated) list.Add("INTR");
                else list.Add("NONE");

                var info = sample.Timestamp.TimeInfo;
                list.Add(info.CPUTick.ToString());
                list.Add(info.HostPosix.ToString());
                list.Add(info.GuestPosix.ToString());
                list.Add(info.GNSSPosix.ToString());
            }

            int count = 0;
            foreach (var val in sample.Values)
            {
                if (++count > sample.NumberOfSignificants) break;
                list.Add(val.mode == GeneralSampleValueMode.Number ? val.number.ToString() : (val.mode == GeneralSampleValueMode.Invalid ? "na" : (val.text ?? "")));
            }
            writer.WriteLine(String.Join(",", list));

            return true;
        }

        /// \~English
        /// <summary>
        /// Close sample CSV writer
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 关闭样本csv写入器
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!writerClosed)
            {
                writer.Close();
                writerClosed = true;
            }
        }

        private StreamWriter writer;
        private bool writerClosed = false;
        private String protocol;
        private int? channel = null;
        private SessionIdentifier? session = null;

        private SampleCsvWriter(StreamWriter writer, String protocol)
        {
            this.writer = writer;
            this.protocol = protocol;
        }
    }
}
