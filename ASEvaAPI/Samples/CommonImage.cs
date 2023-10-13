using System;
using System.IO;
using System.Reflection;

namespace ASEva.Samples
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 通用图像数据
    /// </summary>
    public class CommonImage
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 图像宽度
        /// </summary>
        public int Width { get { return width; } }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 图像高度
        /// </summary>
        public int Height { get { return height; } }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 是否含有Alpha通道
        /// </summary>
        public bool WithAlpha { get { return withAlpha; } }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.8.5) BGR是否逆序
        /// </summary>
        public bool BgrInverted { get { return bgrInverted; } }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 每行数据字节数
        /// </summary>
        public int RowBytes { get { return rowBytes; } }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 图像数据，每个像素的存放顺序为BGR或BGRA（若BGR逆序则为RGB或RGBA）
        /// </summary>
        public byte[] Data { get { return data; } }

        /// \~English
        /// 
        /// \~Chinese
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
            image.bgrInverted = false;
            image.rowBytes = rowBytes;
            image.data = new byte[rowBytes * height];
            return image;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 创建通用图像数据
        /// </summary>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <param name="withAlpha">是否带Alpha通道</param>
        /// <param name="bgrInverted">BGR是否逆序</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage Create(int width, int height, bool withAlpha, bool bgrInverted)
        {
            if (width <= 0 || height <= 0 || width > 65536 || height > 65536) return null;

            var rowBytesValid = width * (withAlpha ? 4 : 3);
            var rowBytes = (((rowBytesValid - 1) >> 2) + 1) << 2;

            var image = new CommonImage();
            image.width = width;
            image.height = height;
            image.withAlpha = withAlpha;
            image.bgrInverted = bgrInverted;
            image.rowBytes = rowBytes;
            image.data = new byte[rowBytes * height];
            return image;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [依赖Agency] 从文件读取图像
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

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [依赖Agency] 从资源读取图像
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

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.1) [依赖Agency] 从图像二进制数据转换
        /// </summary>
        /// <param name="binary">图像二进制数据，如jpeg、png等</param>
        /// <returns>通用图像数据</returns>
        public static CommonImage FromBinary(byte[] binary)
        {
            if (binary == null || binary.Length == 0) return null;
            return Agency.DecodeImage(binary);
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [依赖Agency] 保存至文件
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

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.4.1) [依赖Agency] 转为图像二进制数据
        /// </summary>
        /// <param name="format">编码格式，目前支持"jpg", "png"</param>
        /// <returns>图像二进制数据</returns>
        public byte[] ToBinary(String format)
        {
            if (format == null) return null;
            if (format == "png" || format == "jpg") return Agency.EncodeImage(this, format);
            else return null;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.8.6) 转换为BGR逆序或不逆序的图像对象
        /// </summary>
        /// <param name="targetInverted">BGR是否逆序</param>
        /// <returns>转换后的图像对象</returns>
        public CommonImage ConvertBgrInverted(bool targetInverted)
        {
            if (targetInverted == bgrInverted) return this;

            var output = CommonImage.Create(width, height, withAlpha, targetInverted);
            unsafe
            {
                fixed (byte* srcData = &data[0], dstData = &output.data[0])
                {
                    for (int i = 0; i < height; i++)
                    {
                        byte* srcRow = &srcData[i * rowBytes];
                        byte* dstRow = &dstData[i * output.rowBytes];
                        if (withAlpha)
                        {
                            for (int n = 0; n < width; n++)
                            {
                                dstRow[4 * n] = srcRow[4 * n + 2];
                                dstRow[4 * n + 1] = srcRow[4 * n + 1];
                                dstRow[4 * n + 2] = srcRow[4 * n];
                                dstRow[4 * n + 3] = srcRow[4 * n + 3];
                            }
                        }
                        else
                        {
                            for (int n = 0; n < width; n++)
                            {
                                dstRow[3 * n] = srcRow[3 * n + 2];
                                dstRow[3 * n + 1] = srcRow[3 * n + 1];
                                dstRow[3 * n + 2] = srcRow[3 * n];
                            }
                        }
                    }
                }
            }
            return output;
        }

        private CommonImage()
        {}

        private int width, height;
        private byte[] data;
        private bool withAlpha;
        private bool bgrInverted;
        private int rowBytes;
    }
}
