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
        public SceneTitle Title { get; set; } = new SceneTitle();
        public List<SceneData> Segments { get; set; } = [];

        public static SceneCsv? Load(String file)
        {
            StreamReader? reader = null;
            try
            {
                if (!File.Exists(file)) return null;

                reader = new StreamReader(file);
                var firstLine = reader.ReadLine();
                if (firstLine == null || !firstLine.StartsWith("Scene Table,v2"))
                {
                    reader.Close();
                    return null;
                }

                var output = new SceneCsv();
                output.Title = new SceneTitle();

                var titleComps = reader.ReadLine()?.Split(',');
                if (titleComps != null && titleComps.Length > 4)
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

                    var scene = new SceneData(sceneID);
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

        public void Save(String file)
        {
            StreamWriter? writer = null;
            try
            {
                writer = new StreamWriter(file, false, Encoding.UTF8);

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
