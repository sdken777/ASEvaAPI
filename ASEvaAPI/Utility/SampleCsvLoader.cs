using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASEva.Utility
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Sample CSV file loader
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 读取样本csv文件
    /// </summary>
    public class SampleCsvLoader : IDisposable
    {
        /// \~English
        /// <summary>
        /// Create sample CSV file loader from the specified path
        /// </summary>
        /// <param name="file">File path</param>
        /// <returns>Sample CSV file loader object, null if the file doesn't exist or failed to load</returns>
        /// \~Chinese
        /// <summary>
        /// 根据指定路径创建样本csv读取器
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>csv读取器，若文件不存在或创建失败则返回空</returns>
        public static SampleCsvLoader? CreateLoader(String file)
        {
            StreamReader? reader = null;
            try
            {
                if (!File.Exists(file)) return null;

                var fileNameComps = Path.GetFileNameWithoutExtension(file).Split('@');
                var protocol = fileNameComps[0];

                int? channel = null;
                if (fileNameComps.Length >= 2)
                {
                    int outChannel;
                    if (!Int32.TryParse(fileNameComps[1], out outChannel)) return null;
                    channel = outChannel;
                }

                String[]? title = null;
                int timeColumnIndex = 0;
                int  syncStateIndex = 0;
                int cpuTickIndex = 0;
                int hostPosixIndex = 0;
                int guestPosixIndex = 0;
                int gnssPosixIndex = 0;

                reader = new StreamReader(file);
                var firstLine = reader.ReadLine();
                if (firstLine != null && firstLine.StartsWith("Sample Table,v2")) // v2
                {
                    var titleComps = reader.ReadLine()?.Split(',');
                    if (titleComps?.Length > 2)
                    {
                        var titleElemCount = titleComps.Length - 2;
                        title = new string[titleElemCount];
                        for (int i = 0; i < titleElemCount; i++) title[i] = titleComps[i + 2];
                    }
                    timeColumnIndex = 1;
                }
                else if (firstLine != null && firstLine.StartsWith("Session")) // v3
                {
                    var titleComps = firstLine.Split(',');
                    for (int i = 1; i < titleComps.Length; i++)
                    {
                        if (titleComps[i] == "Time")
                        {
                            timeColumnIndex = i;
                            break;
                        }
                    }
                    for (int i = 1; i < titleComps.Length; i++)
                    {
                        if (syncStateIndex == 0 && titleComps[i] == "Sync State")
                        {
                            syncStateIndex = i;
                        }
                        if (cpuTickIndex == 0 && titleComps[i] == "CPU Tick")
                        {
                            cpuTickIndex = i;
                        }
                        if (hostPosixIndex == 0 && titleComps[i] == "Host Posix")
                        {
                            hostPosixIndex = i;
                        }
                        if (guestPosixIndex == 0 && titleComps[i] == "Guest Posix")
                        {
                            guestPosixIndex = i;
                        }
                        if (gnssPosixIndex == 0 && titleComps[i] == "Gnss Posix")
                        {
                            gnssPosixIndex = i;
                        }
                    }
                    if (timeColumnIndex > 0)
                    {
                        if (titleComps.Length > timeColumnIndex + 1)
                        {
                            var titleElemCount = titleComps.Length - (timeColumnIndex + 1);
                            title = new string[titleElemCount];
                            for (int i = 0; i < titleElemCount; i++) title[i] = titleComps[i + timeColumnIndex + 1];
                        }
                    }
                }
                
                if (timeColumnIndex <= 0)
                {
                    reader.Close();
                    return null;
                }
                else
                {
                    var output = new SampleCsvLoader(reader, protocol);
                    output.channel = channel;
                    output.title = title;
                    output.timeColumnIndex = timeColumnIndex;
                    output.syncStateIndex = syncStateIndex;
                    output.cpuTickIndex = cpuTickIndex;
                    output.hostPosixIndex = hostPosixIndex;
                    output.guestPosixIndex = guestPosixIndex;
                    output.gnssPosixIndex = gnssPosixIndex;
                    return output;
                }
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                if (reader != null) reader.Close();
                return null;
            }
        }

        /// \~English
        /// <summary>
        /// Get title of sample's fields 
        /// </summary>
        /// <returns>Title of sample's fields, null means no title</returns>
        /// \~Chinese
        /// <summary>
        /// 获取样本标题 
        /// </summary>
        /// <returns>样本标题，null表示无标题</returns>
        public List<String>? GetSampleTitle()
        {
            if (title == null) return null;
            else return title.ToList();
        }

        /// \~English
        /// <summary>
        /// Read next sample
        /// </summary>
        /// <returns>General sample object</returns>
        /// \~Chinese
        /// <summary>
        /// 读取下一个样本
        /// </summary>
        /// <returns>样本对象</returns>
        public GeneralSample? ReadNext()
        {
            if (readerClosed) return null;

            while (true)
            {
                var lineText = reader.ReadLine();
                if (lineText == null || lineText.Length == 0)
                {
                    reader.Close();
                    readerClosed = true;
                    return null;
                }

                var comps = lineText.Split(',');
                if (comps.Length < timeColumnIndex + 2) continue;

                var sessionDateTime = DateTime.ParseExact(comps[0], "yyyyMMdd-HH-mm-ss", null);

                double offset;
                if (!Double.TryParse(comps[timeColumnIndex], out offset)) return null;

                TimeOffsetSync syncState = TimeOffsetSync.HostArrival;
                ulong cpuTick = 0, hostPosix = 0, guestPosix = 0, gnssPosix = 0;
                if (syncStateIndex > 0)
                {
                    if (comps[syncStateIndex] == "SERV") syncState = TimeOffsetSync.Server;
                    else if (comps[syncStateIndex] == "GNSS") syncState = TimeOffsetSync.Gnss;
                    else if (comps[syncStateIndex] == "RECV") syncState = TimeOffsetSync.BusReceiverArrival;
                    else if (comps[syncStateIndex] == "INTR") syncState = TimeOffsetSync.Interpolated;
                }
                if (cpuTickIndex > 0) UInt64.TryParse(comps[cpuTickIndex], out cpuTick);
                if (hostPosixIndex > 0) UInt64.TryParse(comps[hostPosixIndex], out hostPosix);
                if (guestPosixIndex > 0) UInt64.TryParse(comps[guestPosixIndex], out guestPosix);
                if (gnssPosixIndex > 0) UInt64.TryParse(comps[gnssPosixIndex], out gnssPosix);

                var sample = new GeneralSample(protocol);
                sample.SetTime(SessionIdentifier.FromDateTime(sessionDateTime), offset, syncState, new IndependentTimeInfo(cpuTick, hostPosix, guestPosix, gnssPosix), offset);
                sample.Channel = channel;
                sample.NumberOfSignificants = comps.Length - (timeColumnIndex + 1);
                sample.Values = [];

                for (int i = timeColumnIndex + 1; i < comps.Length; i++)
                {
                    var elemText = comps[i];
                    double val = 0;
                    if (Double.TryParse(elemText, out val)) sample.Values.Add(new GeneralSampleValue(val));
                    else sample.Values.Add(new GeneralSampleValue(elemText));
                }

                return sample;
            }
        }

        /// \~English
        /// <summary>
        /// Close sample CSV loader
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 关闭样本csv读取器
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!readerClosed)
            {
                reader.Close();
                readerClosed = true;
            }
        }

        private StreamReader reader;
        private bool readerClosed = false;
        private String protocol;
        private int? channel = null;
        private String[]? title = null;
        private int timeColumnIndex = 0; // 0表示无效
        private int  syncStateIndex = 0;
        private int cpuTickIndex = 0;
        private int hostPosixIndex = 0;
        private int guestPosixIndex = 0;
        private int gnssPosixIndex = 0;

        private SampleCsvLoader(StreamReader reader, String protocol)
        {
            this.reader = reader;
            this.protocol = protocol;
        }

        /// \~English
        /// <summary>
        /// Load GeneralSample array from csv file (This function is only suitable for small files)
        /// </summary>
        /// <param name="file">File path</param>
        /// <returns>Array of general samples, null if the file doesn't exist or failed to load</returns>
        /// \~Chinese
        /// <summary>
        /// 从csv读取样本，输出GeneralSample数组（仅适合较小文件的情况）
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>返回的样本数组，若文件不存在或协议不正确或异常则返回null</returns>
        public static GeneralSample[]? Load(String file)
        {
            StreamReader? reader = null;
            try
            {
                if (!File.Exists(file)) return null;

                var fileNameComps = Path.GetFileNameWithoutExtension(file).Split('@');
                var protocol = fileNameComps[0];
                int? channel = null;
                if (fileNameComps.Length >= 2)
                {
                    int ch;
                    if (!Int32.TryParse(fileNameComps[1], out ch)) return null;
                    channel = ch;
                }

                var timeColumnIndex = 0;
                var syncStateIndex = 0;
                var cpuTickIndex = 0;
                var hostPosixIndex = 0;
                var guestPosixIndex = 0;
                var gnssPosixIndex = 0;

                reader = new StreamReader(file);
                var firstLine = reader.ReadLine();
                if (firstLine != null && firstLine.StartsWith("Sample Table,v2")) // v2
                {
                    reader.ReadLine(); // 读标题
                    timeColumnIndex = 1;
                }
                else if (firstLine != null && firstLine.StartsWith("Session")) // v3
                {
                    var titleComps = firstLine.Split(',');
                    for (int i = 1; i < titleComps.Length; i++)
                    {
                        if (titleComps[i] == "Time")
                        {
                            timeColumnIndex = i;
                            break;
                        }
                    }
                    for (int i = 1; i < titleComps.Length; i++)
                    {
                        if (syncStateIndex == 0 && titleComps[i] == "Sync State")
                        {
                            syncStateIndex = i;
                        }
                        if (cpuTickIndex == 0 && titleComps[i] == "CPU Tick")
                        {
                            cpuTickIndex = i;
                        }
                        if (hostPosixIndex == 0 && titleComps[i] == "Host Posix")
                        {
                            hostPosixIndex = i;
                        }
                        if (guestPosixIndex == 0 && titleComps[i] == "Guest Posix")
                        {
                            guestPosixIndex = i;
                        }
                        if (gnssPosixIndex == 0 && titleComps[i] == "Gnss Posix")
                        {
                            gnssPosixIndex = i;
                        }
                    }
                }
                
                if (timeColumnIndex <= 0)
                {
                    reader.Close();
                    return null;
                }

                var list = new List<GeneralSample>();
                while (true)
                {
                    var lineText = reader.ReadLine();
                    if (lineText == null || lineText.Length == 0) break;

                    var comps = lineText.Split(',');
                    if (comps.Length < timeColumnIndex + 2) continue;

                    var sessionDateTime = DateTime.ParseExact(comps[0], "yyyyMMdd-HH-mm-ss", null);

                    double offset;
                    if (!Double.TryParse(comps[timeColumnIndex], out offset)) continue;

                    TimeOffsetSync syncState = TimeOffsetSync.HostArrival;
                    ulong cpuTick = 0, hostPosix = 0, guestPosix = 0, gnssPosix = 0;
                    if (syncStateIndex > 0)
                    {
                        if (comps[syncStateIndex] == "SERV") syncState = TimeOffsetSync.Server;
                        else if (comps[syncStateIndex] == "GNSS") syncState = TimeOffsetSync.Gnss;
                        else if (comps[syncStateIndex] == "RECV") syncState = TimeOffsetSync.BusReceiverArrival;
                        else if (comps[syncStateIndex] == "INTR") syncState = TimeOffsetSync.Interpolated;
                    }
                    if (cpuTickIndex > 0) UInt64.TryParse(comps[cpuTickIndex], out cpuTick);
                    if (hostPosixIndex > 0) UInt64.TryParse(comps[hostPosixIndex], out hostPosix);
                    if (guestPosixIndex > 0) UInt64.TryParse(comps[guestPosixIndex], out guestPosix);
                    if (gnssPosixIndex > 0) UInt64.TryParse(comps[gnssPosixIndex], out gnssPosix);

                    var sample = new GeneralSample(protocol);
                    sample.SetTime(SessionIdentifier.FromDateTime(sessionDateTime), offset, syncState, new IndependentTimeInfo(cpuTick, hostPosix, guestPosix, gnssPosix), offset);
                    sample.Channel = channel;
                    sample.NumberOfSignificants = comps.Length - (timeColumnIndex + 1);
                    sample.Values = [];

                    for (int i = timeColumnIndex + 1; i < comps.Length; i++)
                    {
                        var elemText = comps[i];
                        double val = 0;
                        if (Double.TryParse(elemText, out val)) sample.Values.Add(new GeneralSampleValue(val));
                        else sample.Values.Add(new GeneralSampleValue(elemText));
                    }

                    list.Add(sample);
                }

                reader.Close();
                return list.ToArray();
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                if (reader != null) reader.Close();
                return null;
            }
        }
    }
}
