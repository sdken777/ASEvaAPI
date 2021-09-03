using System;
using System.Reflection;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.0) 图像资源加载
    /// </summary>
    public class ImageResourceLoader
    {
        public static Bitmap Load(String fileName)
        {
            var instream = Assembly.GetCallingAssembly().GetManifestResourceStream(fileName);
            if (instream == null) return null;

            var data = new byte[instream.Length];
            instream.Read(data, 0, data.Length);
            instream.Close();

            try
            {
                return new Bitmap(data);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}