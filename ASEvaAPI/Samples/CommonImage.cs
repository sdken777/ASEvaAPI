using System;
using System.IO;
using System.Reflection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace ASEva.Samples
{
    /// <summary>
    /// (api:app=2.3.0) 通用图像数据
    /// </summary>
    public class CommonImage
    {
        /// <summary>
        /// 图像宽度
        /// </summary>
        public int Width { get { return width; } }

        /// <summary>
        /// 图像高度
        /// </summary>
        public int Height { get { return height; } }

        /// <summary>
        /// 是否含有Alpha通道
        /// </summary>
        public bool WithAlpha { get { return withAlpha; } }

        /// <summary>
        /// 每行数据字节数
        /// </summary>
        public int RowBytes { get { return rowBytes; } }

        /// <summary>
        /// 图像数据，每个像素的存放顺序为BGR或BGRA
        /// </summary>
        public byte[] Data { get { return data; } }

        /// <summary>
        /// 创建通用图像数据
        /// </summary>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <param name="withAlpha">是否带Alpha通道</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage Create(int width, int height, bool withAlpha)
        {
            if (width <= 0 || height <= 0 || width > 65536 || height > 65536) return null;

            var rowBytesValid = width * (withAlpha ? 4 : 3);
            var rowBytes = (((rowBytesValid - 1) >> 2) + 1) << 2;

            var image = new CommonImage();
            image.width = width;
            image.height = height;
            image.withAlpha = withAlpha;
            image.rowBytes = rowBytes;
            image.data = new byte[rowBytes * height];
            return image;
        }

        /// <summary>
        /// 从文件读取图像
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage LoadFile(String filePath)
        {
            if (!File.Exists(filePath)) return null;
            
            byte[] data = null;
            FileStream file = null;
            try
            {
                file = File.Open(filePath, FileMode.Open, FileAccess.Read);
                data = new byte[file.Length];
                int length = file.Read(data, 0, (int)file.Length);
                if (length != file.Length) data = null;
            }
            catch (Exception) {}
            if (file != null) file.Close();
            if (data == null) return null;

            return CommonImage.FromBinary(data);
        }

        /// <summary>
        /// 从资源读取图像
        /// </summary>
        /// <param name="resourceName">资源名称</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage LoadResource(String resourceName)
        {
            var instream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName);
            if (instream == null || instream.Length == 0) return null;

            var data = new byte[instream.Length];
            instream.Read(data, 0, data.Length);
            instream.Close();

            return CommonImage.FromBinary(data);
        }

        /// <summary>
        /// (api:app=2.4.1) 从图像二进制数据转换
        /// </summary>
        /// <param name="binary">图像二进制数据，如jpeg、png等</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage FromBinary(byte[] binary)
        {
            if (binary == null || binary.Length == 0) return null;

            Image<Rgba32> image = null;
            try { image = Image.Load(binary); }
            catch (Exception) { return null; }

            return CommonImage.FromRgba32(image);
        }

        /// <summary>
        /// (api:app=2.4.1) 从ImageSharp的Rgba32图像数据转换
        /// </summary>
        /// <param name="image">ImageSharp的Rgba32图像对象</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage FromRgba32(Image<Rgba32> image)
        {
            if (image == null) return null;

            var withAlpha = image.PixelType.BitsPerPixel == 32;
            var bytesPerPixel = withAlpha ? 4 : 3;

            var output = CommonImage.Create(image.Width, image.Height, withAlpha);
            for (int i = 0; i < image.Height; i++)
            {
                var srcRow = image.GetPixelRowSpan(i);
                var dstRowIndex = i * output.RowBytes;
                for (int j = 0; j < srcRow.Length; j++)
                {
                    bool alphaZero = false;
                    if (withAlpha)
                    {
                        var alpha = srcRow[j].A;
                        output.Data[dstRowIndex + j * bytesPerPixel + 3] = alpha;
                        alphaZero = alpha == 0;
                    }
                    if (alphaZero)
                    {
                        output.Data[dstRowIndex + j * bytesPerPixel] = 0;
                        output.Data[dstRowIndex + j * bytesPerPixel + 1] = 0;
                        output.Data[dstRowIndex + j * bytesPerPixel + 2] = 0;
                    }
                    else
                    {
                        output.Data[dstRowIndex + j * bytesPerPixel] = srcRow[j].B;
                        output.Data[dstRowIndex + j * bytesPerPixel + 1] = srcRow[j].G;
                        output.Data[dstRowIndex + j * bytesPerPixel + 2] = srcRow[j].R;
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// 保存至文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否成功保存</returns>
        public bool Save(String filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            String format = null;
            if (extension == ".jpeg" || extension == ".jpg") format = "jpg";
            else if (extension == ".png") format = "png";
            else return false;

            var data = ToBinary(format);
            if (data == null) return false;

            bool ok = false;
            FileStream file = null;
            try
            {
                file = File.Open(filePath, FileMode.Create, FileAccess.Write);
                file.Write(data);
                ok = true;
            }
            catch (Exception) {}
            if (file != null) file.Close();

            return ok;
        }

        /// <summary>
        /// (api:app=2.4.1) 转为图像二进制数据
        /// </summary>
        /// <param name="format">编码格式，目前支持"jpg", "png"</param>
        /// <returns>图像二进制数据</returns>
        public byte[] ToBinary(String format)
        {
            IImageEncoder encoder = null;
            if (format == "png") encoder = new PngEncoder();
            else if (format == "jpg") encoder = new JpegEncoder();
            else return null;

            var image = ToRgba32();

            byte[] output = null;
            var stream = new MemoryStream();
            try
            {
                image.Save(stream, encoder);
                stream.Position = 0;

                output = new byte[stream.Length];
                stream.Read(output, 0, output.Length);
            }
            catch (Exception)
            {
                output = null;
            }
            stream.Close();

            return output;
        }

        /// <summary>
        /// (api:app=2.4.1) 转为ImageSharp的Rgba32图像
        /// </summary>
        /// <returns>ImageSharp的Rgba32图像对象</returns>
        public Image<Rgba32> ToRgba32()
        {
            Image<Rgba32> image = null;
            if (WithAlpha)
            {
                image = new Image<Rgba32>(Width, Height, new Rgba32(0, 0, 0, 0));
                for (int i = 0; i < image.Height; i++)
                {
                    var srcRowIndex = i * RowBytes;
                    var dstRow = image.GetPixelRowSpan(i);
                    for (int j = 0; j < dstRow.Length; j++)
                    {
                        dstRow[j].B = Data[srcRowIndex + j * 4];
                        dstRow[j].G = Data[srcRowIndex + j * 4 + 1];
                        dstRow[j].R = Data[srcRowIndex + j * 4 + 2];
                        dstRow[j].A = Data[srcRowIndex + j * 4 + 3];
                    }
                }
            }
            else
            {
                image = new Image<Rgba32>(Width, Height, new Rgba32(0, 0, 0, 255));
                for (int i = 0; i < image.Height; i++)
                {
                    var srcRowIndex = i * RowBytes;
                    var dstRow = image.GetPixelRowSpan(i);
                    for (int j = 0; j < dstRow.Length; j++)
                    {
                        dstRow[j].B = Data[srcRowIndex + j * 3];
                        dstRow[j].G = Data[srcRowIndex + j * 3 + 1];
                        dstRow[j].R = Data[srcRowIndex + j * 3 + 2];
                    }
                }
            }
            return image;
        }

        private CommonImage()
        {}

        private int width, height;
        private byte[] data;
        private bool withAlpha;
        private int rowBytes;
    }
}
