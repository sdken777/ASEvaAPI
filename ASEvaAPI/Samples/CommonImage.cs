using System;
using System.IO;
using System.Reflection;

namespace ASEva.Samples
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=2.3.0) Common image data
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.3.0) 通用图像数据
    /// </summary>
    public class CommonImage
    {
        /// \~English
        /// <summary>
        /// Image width
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 图像宽度
        /// </summary>
        public int Width { get { return width; } }

        /// \~English
        /// <summary>
        /// Image height
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 图像高度
        /// </summary>
        public int Height { get { return height; } }

        /// \~English
        /// <summary>
        /// Whether alpha channel included
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否含有Alpha通道
        /// </summary>
        public bool WithAlpha { get { return withAlpha; } }

        /// \~English
        /// <summary>
        /// (api:app=2.8.5) Whether BGR inverted
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.8.5) BGR是否逆序
        /// </summary>
        public bool BgrInverted { get { return bgrInverted; } }

        /// \~English
        /// <summary>
        /// Number of bytes for each row
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 每行数据字节数
        /// </summary>
        public int RowBytes { get { return rowBytes; } }

        /// \~English
        /// <summary>
        /// Image data, in order of BGR or BGRA (If inverted, it's  RGB or RGBA)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 图像数据，每个像素的存放顺序为BGR或BGRA（若BGR逆序则为RGB或RGBA）
        /// </summary>
        public byte[] Data { get { return data; } }

        /// \~English
        /// <summary>
        /// Create common image
        /// </summary>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="withAlpha">Whether to include alpha channel</param>
        /// <returns>Common image object</returns>
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
        /// <summary>
        /// Create common image
        /// </summary>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="withAlpha">Whether to include alpha channel</param>
        /// <param name="bgrInverted">Whether BGR inverted</param>
        /// <returns>Common image object</returns>
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
        /// <summary>
        /// [Depend on Agency] Load from file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>Common image object</returns>
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
        /// <summary>
        /// [Depend on Agency] Load from resource
        /// </summary>
        /// <param name="resourceName">Resource name</param>
        /// <returns>Common image object</returns>
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
        /// <summary>
        /// (api:app=2.4.1) [Depend on Agency] Create from image binary data
        /// </summary>
        /// <param name="binary">Image binary data, like jpeg, png, etc.</param>
        /// <returns>Common image object</returns>
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
        /// <summary>
        /// [Depend on Agency] Save to file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>Whether successful</returns>
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
        /// <summary>
        /// (api:app=2.4.1) [Depend on Agency] Convert to image binary data
        /// </summary>
        /// <param name="format">Target format, only "jpg" and "png" supported</param>
        /// <returns>Image binary data</returns>
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
        /// <summary>
        /// (api:app=2.8.6) Convert to/from BGR inverted image
        /// </summary>
        /// <param name="targetInverted">Whether BGR inverted</param>
        /// <returns>Converted image</returns>
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

        /// \~English
        /// <summary>
        /// (api:app=2.14.4) Resize image (keep the ratio of width to height)
        /// </summary>
        /// <param name="targetWidth">Width of resized image, in pixels, at least 8 (height will be calculated automatically)</param>
        /// <returns>Resized image, return null if the source image's width is less than 8</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.14.4) 缩放图像（保持宽高比）
        /// </summary>
        /// <param name="targetWidth">缩放后的图像宽度，高度自动计算，至少为8</param>
        /// <returns>缩放后的图像，原图像尺寸小于8像素则返回null</returns>
        public CommonImage Resize(int targetWidth)
        {
            if (Width < 8 || Height < 8 || targetWidth < 8) return null;

            if (targetWidth == Width)
            {
                var output = new CommonImage();
                output.width = this.width;
                output.height = this.height;
                output.rowBytes = this.rowBytes;
                output.bgrInverted = this.bgrInverted;
                output.withAlpha = this.withAlpha;
                output.data = new byte[this.data.Length];
                Array.Copy(this.data, output.data, this.data.Length);
                return output;
            }

            var scale = (float)targetWidth / Width;
            var targetHeight = (int)Math.Ceiling(scale * Height);

            var rawClipRect = new IntRect(0, 0, targetWidth, targetHeight);
            if (scale < 0.25)
            {
                var halfClipRect = new IntRect(0, 0, (int)Math.Ceiling(0.5f * targetWidth), (int)Math.Ceiling(0.5f * targetHeight));
                var halfImage = processImageSub(this, 0.5f, halfClipRect);
                var quarterClipRect = new IntRect(0, 0, (int)Math.Ceiling(0.25f * targetWidth), (int)Math.Ceiling(0.25f * targetHeight));
                var quarterImage = processImageSub(halfImage, 0.5f, quarterClipRect);
                return processImageSub(quarterImage, scale * 4, rawClipRect);
            }
            else if (scale < 0.5)
            {
                var halfClipRect = new IntRect(0, 0, (int)Math.Ceiling(0.5f * targetWidth), (int)Math.Ceiling(0.5f * targetHeight));
                var halfImage = processImageSub(this, 0.5f, halfClipRect);
                return processImageSub(halfImage, scale * 2, rawClipRect);
            }
            else
            {
                return processImageSub(this, scale, rawClipRect);
            }
        }

        private CommonImage()
        {}

        private CommonImage processImageSub(CommonImage srcImage, float scale, IntRect clipRect)
        {
            var newImage = CommonImage.Create(clipRect.Width, clipRect.Height, srcImage.WithAlpha);
            unsafe
            {
                int clipWidth = clipRect.Width, clipHeight = clipRect.Height;
                int offsetU = clipRect.X, offsetV = clipRect.Y;
                int bpp = srcImage.WithAlpha ? 4 : 3;
                int srcStep = srcImage.RowBytes;
                int dstStep = newImage.RowBytes;
                float srcWidthLimit = (float)srcImage.Width - 1.1f;
                float srcHeightLimit = (float)srcImage.Height - 1.1f;
                fixed (byte *srcData = &srcImage.Data[0], dstData = &newImage.Data[0])
                {
                    for (int v = 0; v < clipHeight; v++)
                    {
                        float srcV = Math.Max(0.0f, Math.Min(srcHeightLimit, ((float)offsetV + v + 0.5f) / scale) - 0.5f);
                        int srcVI = (int)Math.Floor(srcV);
                        float q = srcV - srcVI;
                        byte *srcRowT = srcData + srcVI * srcStep;
                        byte *srcRowB = srcData + (srcVI + 1) * srcStep;
                        byte *dstRow = dstData + v * dstStep;
                        for (int u = 0; u < clipWidth; u++)
                        {
                            float srcU = Math.Max(0.0f, Math.Min(srcWidthLimit, ((float)offsetU + u + 0.5f) / scale) - 0.5f);
                            int srcUI = (int)Math.Floor(srcU);
                            float p = srcU - srcUI;
                            byte *srcCellTL = srcRowT + srcUI * bpp;
                            byte *srcCellTR = srcRowT + (srcUI + 1) * bpp;
                            byte *srcCellBL = srcRowB + srcUI * bpp;
                            byte *srcCellBR = srcRowB + (srcUI + 1) * bpp;
                            byte *dstCell = dstRow + u * bpp;
                            for (int n = 0; n < bpp; n++)
                            {
                                dstCell[n] = (byte)((1-p)*(1-q)*srcCellTL[n] + p*(1-q) * srcCellTR[n] + (1-p)*q*srcCellBL[n] + p*q*srcCellBR[n]);
                            }
                        }
                    }
                }
            }

            return newImage;
        }

        private int width, height;
        private byte[] data;
        private bool withAlpha;
        private bool bgrInverted;
        private int rowBytes;
    }
}
