using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Scenario CSV reader and writer
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 场景csv文件读写
    /// </summary>
    public class SceneCsv
    {
        /// \~English
        /// <summary>
        /// Title of scene properties
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 场景属性标题
        /// </summary>
        public SceneTitle Title { get; set; }

        /// \~English
        /// <summary>
        /// Scene segment list
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 场景片段列表
        /// </summary>
        public List<SceneData> Segments { get; set; }

        /// \~English
        /// <summary>
        /// Create scene CSV object
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 创建场景csv对象
        /// </summary>
        public SceneCsv()
        {
            Title = new SceneTitle();
            Segments = new List<SceneData>();
        }

        /// \~English
        /// <summary>
        /// Load scene CSV object from file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 从文件加载场景csv对象
        /// </summary>
        public static SceneCsv Load(String file)
        {
            if (!File.Exists(file)) return null;

            Stream stream = null;
            try { stream = File.OpenRead(file); }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                return null;
            }

            return Load(stream);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.10.3) Load scene CSV object from stream
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.10.3) 从数据流加载场景csv对象
        /// </summary>
        public static SceneCsv Load(Stream stream)
        {
            if (stream == null) return null;

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(stream);
                var firstLine = reader.ReadLine();
                if (!firstLine.StartsWith("Scene Table,v2"))
                {
                    reader.Close();
                    return null;
                }

                var output = new SceneCsv();
                output.Title = new SceneTitle();

                var titleComps = reader.ReadLine().Split(',');
                if (titleComps.Length > 4)
                {
                    var titleElemCount = titleComps.Length - 4;
                    for (int i = 0; i < titleElemCount; i++) output.Title.Titles.Add(titleComps[i + 4]);
                }

                var list = new List<GeneralSample>();
                var sceneBuf = new List<SceneData>();
                while (true)
                {
                    var lineText = reader.ReadLine();
                    if (lineText == null || lineText.Length == 0) break;

                    var comps = lineText.Split(',');
                    if (comps.Length < 4) continue;

                    var sceneID = comps[3];
                    if (sceneID.Length == 0) continue;

                    var sessionDateTime = DateTime.ParseExact(comps[0], "yyyyMMdd-HH-mm-ss", null);

                    double startTime, length;
                    if (!Double.TryParse(comps[1], out startTime) || !Double.TryParse(comps[2], out length)) continue;

                    var scene = new SceneData();
                    scene.SceneID = sceneID;
                    scene.Session = SessionIdentifier.FromDateTime(sessionDateTime);
                    scene.BeginOffset = startTime;
                    scene.TimeLength = length;
                    scene.PropertyValues = new string[comps.Length - 4];
                    for (int i = 0; i < scene.PropertyValues.Length; i++) scene.PropertyValues[i] = comps[i + 4];

                    sceneBuf.Add(scene);
                }

                reader.Close();

                output.Segments = sceneBuf;
                return output;
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
        /// Save scene CSV object to file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 保存场景csv对象到文件
        /// </summary>
        public void Save(String file)
        {
            if (file == null) return;

            try
            {
                var root = Path.GetDirectoryName(file);
                if (!Directory.Exists(root)) Directory.CreateDirectory(root);
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                return;
            }

            Stream stream = null;
            try { stream = File.OpenWrite(file); }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                return;
            }

            Save(stream);
        }

        /// \~English
        /// <summary>
        /// (api:app=3.10.3) Save scene CSV object to stream
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.10.3) 保存场景csv对象到数据流
        /// </summary>
        public void Save(Stream stream)
        {
            if (stream == null) return;

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(stream, Encoding.UTF8);

                writer.WriteLine("Scene Table,v2");

                var titleLine = "Session,Start time,Time length,Scene type";
                if (Title != null) titleLine += "," + String.Join(",", Title.Titles);
                writer.WriteLine(titleLine);

                if (Segments != null)
                {
                    foreach (var scene in Segments)
                    {
                        var list = new List<String>();
                        list.Add(scene.Session.ToDateTime().ToString("yyyyMMdd-HH-mm-ss"));
                        list.Add(scene.BeginOffset.ToString());
                        list.Add(scene.TimeLength.ToString());
                        list.Add(scene.SceneID);
                        if (scene.PropertyValues != null && scene.PropertyValues.Length > 0) list.AddRange(scene.PropertyValues);

                        writer.WriteLine(String.Join(",", list));
                    }
                }

                writer.Close();
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                if (writer != null) writer.Close();
            }
        }
    }
}
