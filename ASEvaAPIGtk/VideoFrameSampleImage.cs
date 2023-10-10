using System;
using Gdk;
using Cairo;
using ASEva.Samples;

namespace ASEva.UIGtk
{
    /// <summary>
    /// (api:gtk=2.0.0) ASEva.Samples.VideoFrameSample 中的图像对象
    /// </summary>
    public class VideoFrameSampleImage
    {
        /// <summary>
        /// 按指定宽高创建无alpha通道的图像
        /// </summary>
        public VideoFrameSampleImage(int width, int height)
        {
            w = Math.Max(1, width);
            h = Math.Max(1, height);
            rowBytes = (w * 3 + 3) & 0xfffc;
            data = new byte[h * rowBytes];
            withAlpha = false;
            bgrInverted = false;
        }

        /// <summary>
        /// (api:gtk=2.1.0) 绑定通用图像，共享数据
        /// </summary>
        public static VideoFrameSampleImage FromCommonImage(CommonImage image)
        {
            if (image == null) return null;

            var output = new VideoFrameSampleImage();
            output.w = image.Width;
            output.h = image.Height;
            output.rowBytes = image.RowBytes;
            output.data = image.Data;
            output.withAlpha = image.WithAlpha;
            output.bgrInverted = image.BgrInverted;
            return output;
        }

        /// <summary>
        /// 图像宽度
        /// </summary>
        public int Width
        {
            get { return w; }
        }

        /// <summary>
        /// 图像高度
        /// </summary>
        public int Height
        {
            get { return h; }
        }

        /// <summary>
        /// 一行数据的字节数
        /// </summary>
        public int RowBytes
        {
            get { return rowBytes; }
        }

        /// <summary>
        /// 图像数据
        /// </summary>
        public byte[] Data
        {
            get { return data; }
        }

        /// <summary>
        /// (api:gtk=2.1.0) 是否含alpha通道
        /// </summary>
        public bool WithAlpha
        {
            get { return withAlpha; }
        }

        /// <summary>
        /// (api:gtk=2.5.6) BGR是否逆序
        /// </summary>
        public bool BgrInverted
        {
            get { return bgrInverted; }
        }

        /// <summary>
        /// 转换为Gdk.Pixbuf对象
        /// </summary>
        public Pixbuf ToPixbuf()
        {
            if (withAlpha)
            {
                var pixbuf = new Pixbuf(Colorspace.Rgb, true, 8, w, h);
                unsafe
                {
                    byte* dstData = (byte*)pixbuf.Pixels;
                    fixed (byte* srcData = &(data[0]))
                    {
                        if (bgrInverted)
                        {
                            for (int v = 0; v < h; v++)
                            {
                                uint* srcRow = (uint*)&srcData[v * rowBytes];
                                uint* dstRow = (uint*)&dstData[v * pixbuf.Rowstride];
                                for (int u = 0; u < w; u++)
                                {
                                    dstRow[u] = srcRow[u];
                                }
                            }
                        }
                        else
                        {
                            for (int v = 0; v < h; v++)
                            {
                                byte* srcRow = &srcData[v * rowBytes];
                                byte* dstRow = &dstData[v * pixbuf.Rowstride];
                                for (int u = 0; u < w; u++)
                                {
                                    dstRow[4 * u] = srcRow[4 * u + 2];
                                    dstRow[4 * u + 1] = srcRow[4 * u + 1];
                                    dstRow[4 * u + 2] = srcRow[4 * u];
                                    dstRow[4 * u + 3] = srcRow[4 * u + 3];
                                }
                            }
                        }

                    }
                }
                return pixbuf;
            }
            else
            {
                var pixbuf = new Pixbuf(Colorspace.Rgb, false, 8, w, h);
                unsafe
                {
                    byte* dstData = (byte*)pixbuf.Pixels;
                    fixed (byte* srcData = &(data[0]))
                    {
                        if (bgrInverted)
                        {
                            for (int v = 0; v < h; v++)
                            {
                                byte* srcRow = &srcData[v * rowBytes];
                                byte* dstRow = &dstData[v * pixbuf.Rowstride];
                                for (int u = 0; u < 3 * w; u++)
                                {
                                    dstRow[u] = srcRow[u];
                                }
                            }
                        }
                        else
                        {
                            for (int v = 0; v < h; v++)
                            {
                                byte* srcRow = &srcData[v * rowBytes];
                                byte* dstRow = &dstData[v * pixbuf.Rowstride];
                                for (int u = 0; u < w; u++)
                                {
                                    dstRow[3 * u] = srcRow[3 * u + 2];
                                    dstRow[3 * u + 1] = srcRow[3 * u + 1];
                                    dstRow[3 * u + 2] = srcRow[3 * u];
                                }
                            }
                        }
                    }
                }
                return pixbuf;
            }
        }

        /// <summary>
        /// 转换为Cairo.ImageSurface对象，使用完毕后需调用Dispose方法进行释放
        /// </summary>
        public ImageSurface ToImageSurface()
        {
            var surface = new Cairo.ImageSurface(Cairo.Format.Argb32, w, h);
            unsafe
            {
                byte* dstData = (byte*)surface.DataPtr;
                if (withAlpha)
                {
                    var k = 1.0f / 255;
                    fixed (byte* srcData = &(Data[0]))
                    {
                        for (int v = 0; v < h; v++)
                        {
                            byte* srcRow = &srcData[v * rowBytes];
                            byte* dstRow = &dstData[v * w * 4];
                            if (bgrInverted)
                            {
                                for (int u = 0; u < w; u++)
                                {
                                    if (srcRow[4 * u + 3] == 0) continue;
                                    var ratio = k * srcRow[4 * u + 3];
                                    dstRow[4 * u] = (byte)(ratio * srcRow[4 * u + 2]);
                                    dstRow[4 * u + 1] = (byte)(ratio * srcRow[4 * u + 1]);
                                    dstRow[4 * u + 2] = (byte)(ratio * srcRow[4 * u]);
                                    dstRow[4 * u + 3] = srcRow[4 * u + 3];
                                }
                            }
                            else
                            {
                                for (int u = 0; u < w; u++)
                                {
                                    if (srcRow[4 * u + 3] == 0) continue;
                                    var ratio = k * srcRow[4 * u + 3];
                                    dstRow[4 * u] = (byte)(ratio * srcRow[4 * u]);
                                    dstRow[4 * u + 1] = (byte)(ratio * srcRow[4 * u + 1]);
                                    dstRow[4 * u + 2] = (byte)(ratio * srcRow[4 * u + 2]);
                                    dstRow[4 * u + 3] = srcRow[4 * u + 3];
                                }
                            }
                        }
                    }
                }
                else
                {
                    fixed (byte* srcData = &(Data[0]))
                    {
                        for (int v = 0; v < h; v++)
                        {
                            byte* srcRow = &srcData[v * rowBytes];
                            byte* dstRow = &dstData[v * w * 4];
                            if (bgrInverted)
                            {
                                for (int u = 0; u < w; u++)
                                {
                                    dstRow[4 * u] = srcRow[3 * u + 2];
                                    dstRow[4 * u + 1] = srcRow[3 * u + 1];
                                    dstRow[4 * u + 2] = srcRow[3 * u];
                                    dstRow[4 * u + 3] = 255;
                                }
                            }
                            else
                            {
                                for (int u = 0; u < w; u++)
                                {
                                    dstRow[4 * u] = srcRow[3 * u];
                                    dstRow[4 * u + 1] = srcRow[3 * u + 1];
                                    dstRow[4 * u + 2] = srcRow[3 * u + 2];
                                    dstRow[4 * u + 3] = 255;
                                }
                            }
                        }
                    }
                }
            }
            surface.MarkDirty();
            return surface;
        }

        private VideoFrameSampleImage()
        {}

        private byte[] data;
        private int w;
        private int h;
        private int rowBytes;
        private bool withAlpha;
        private bool bgrInverted;
    }
}