using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.0.0) 读取样本csv文件
    /// </summary>
    public class SampleCsvLoader
    {
        /// <summary>
        /// 根据指定路径创建样本csv读取器
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>csv读取器，若文件不存在或创建失败则返回空</returns>
        public static SampleCsvLoader CreateLoader(String file)
        {
            StreamReader reader = null;
            try
            {
                if (!File.Exists(file)) return null;

                var loader = new SampleCsvLoader();

                var fileNameComps = Path.GetFileNameWithoutExtension(file).Split('@');
                loader.protocol = fileNameComps[0];
                if (fileNameComps.Length >= 2) loader.channel = Convert.ToInt32(fileNameComps[1]);

                reader = new StreamReader(file);
                var firstLine = reader.ReadLine();
                if (firstLine.StartsWith("Sample Table,v2")) // v2
                {
                    var titleComps = reader.ReadLine().Split(',');
                    if (titleComps.Length > 2)
                    {
                        var titleElemCount = titleComps.Length - 2;
                        loader.title = new string[titleElemCount];
                        for (int i = 0; i < titleElemCount; i++) loader.title[i] = titleComps[i + 2];
                    }
                    loader.timeColumnIndex = 1;
                }
                else if (firstLine.StartsWith("Session")) // v3
                {
                    var titleComps = firstLine.Split(',');
                    for (int i = 1; i < titleComps.Length; i++)
                    {
                        if (titleComps[i] == "Time")
                        {
                            loader.timeColumnIndex = i;
                            break;
                        }
                    }
                    if (loader.timeColumnIndex > 0)
                    {
                        if (titleComps.Length > loader.timeColumnIndex + 1)
                        {
                            var titleElemCount = titleComps.Length - (loader.timeColumnIndex + 1);
                            loader.title = new string[titleElemCount];
                            for (int i = 0; i < titleElemCount; i++) loader.title[i] = titleComps[i + loader.timeColumnIndex + 1];
                        }
                    }
                }
                
                if (loader.timeColumnIndex <= 0)
                {
                    reader.Close();
                    return null;
                }
                else
                {
                    loader.reader = reader;
                    return loader;
                }
            }
            catch (Exception)
            {
                if (reader != null) reader.Close();
                return null;
            }
        }

        /// <summary>
        /// 获取样本标题 
        /// </summary>
        /// <returns>样本标题，null表示无标题</returns>
        public List<String> GetSampleTitle()
        {
            if (title == null) return null;
            else return title.ToList();
        }

        /// <summary>
        /// 读取下一个样本
        /// </summary>
        /// <returns>样本对象</returns>
        public GeneralSample ReadNext()
        {
            if (reader == null) return null;

            while (true)
            {
                var lineText = reader.ReadLine();
                if (lineText == null || lineText.Length == 0)
                {
                    reader.Close();
                    reader = null;
                    return null;
                }

                var comps = lineText.Split(',');
                if (comps.Length < timeColumnIndex + 2) continue;

                var session = DateTime.ParseExact(comps[0], "yyyyMMdd-HH-mm-ss", null);
                var offset = Convert.ToDouble(comps[timeColumnIndex]);

                var sample = new GeneralSample();
                sample.Base = session;
                sample.Channel = channel;
                sample.NumberOfSignificants = comps.Length - (timeColumnIndex + 1);
                sample.Offset = offset;
                sample.Protocol = protocol;
                sample.Timeline = offset;
                sample.Values = new List<GeneralSampleValue>();

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

        /// <summary>
        /// 关闭样本csv读取器
        /// </summary>
        public void Close()
        {
            if (reader != null)
            {
                reader.Close();
                reader = null;
            }
        }

        private StreamReader reader = null;
        private String protocol = null;
        private int? channel = null;
        private String[] title = null;
        private int timeColumnIndex = 0; // 0表示无效

        private SampleCsvLoader()
        { }

        /// <summary>
        /// 从csv读取样本，输出GeneralSample数组（仅适合较小文件的情况）
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>返回的样本数组，若文件不存在或协议不正确或异常则返回null</returns>
        public static GeneralSample[] Load(String file)
        {
            StreamReader reader = null;
            try
            {
                if (!File.Exists(file)) return null;

                var fileNameComps = Path.GetFileNameWithoutExtension(file).Split('@');
                var protocol = fileNameComps[0];
                int? channel = null;
                if (fileNameComps.Length >= 2) channel = Convert.ToInt32(fileNameComps[1]);

                var timeColumnIndex = 0;

                reader = new StreamReader(file);
                var firstLine = reader.ReadLine();
                if (firstLine.StartsWith("Sample Table,v2")) // v2
                {
                    reader.ReadLine(); // 读标题
                    timeColumnIndex = 1;
                }
                else if (firstLine.StartsWith("Session")) // v3
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

                    var session = DateTime.ParseExact(comps[0], "yyyyMMdd-HH-mm-ss", null);
                    var offset = Convert.ToDouble(comps[timeColumnIndex]);

                    var sample = new GeneralSample();
                    sample.Base = session;
                    sample.Channel = channel;
                    sample.NumberOfSignificants = comps.Length - (timeColumnIndex + 1);
                    sample.Offset = offset;
                    sample.Protocol = protocol;
                    sample.Timeline = offset;
                    sample.Values = new List<GeneralSampleValue>();

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
            catch (Exception)
            {
                if (reader != null) reader.Close();
                return null;
            }
        }
    }
}
