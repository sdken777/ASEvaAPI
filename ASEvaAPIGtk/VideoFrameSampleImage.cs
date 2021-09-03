using System;
using Gdk;
using Cairo;

namespace ASEva.UIGtk
{
    /// <summary>
    /// (api:gtk=2.0.0) ASEva.Samples.VideoFrameSample 中的图像对象
    /// </summary>
    public class VideoFrameSampleImage
    {
        public VideoFrameSampleImage(int width, int height)
        {
            w = Math.Max(1, width);
            h = Math.Max(1, height);
            rowBytes = (w * 3 + 3) & 0xfffc;
            data = new byte[h * rowBytes];
        }

        public int Width
        {
            get { return w; }
        }

        public int Height
        {
            get { return h; }
        }

        public int RowBytes
        {
            get { return rowBytes; }
        }

        public byte[] Data
        {
            get { return data; }
        }

        /// <summary>
        /// 转换为Gdk.Pixbuf对象
        /// </summary>
        public Pixbuf ToPixbuf()
        {
            var buffer = new byte[data.Length];
            unsafe
            {
                fixed (byte* srcData = &(data[0]), dstData = &(buffer[0]))
                {
                    for (int v = 0; v < h; v++)
                    {
                        byte* srcRow = &srcData[v * rowBytes];
                        byte* dstRow = &dstData[v * rowBytes];
                        for (int u = 0; u < w; u++)
                        {
                            dstRow[3 * u] = srcRow[3 * u + 2];
                            dstRow[3 * u + 1] = srcRow[3 * u + 1];
                            dstRow[3 * u + 2] = srcRow[3 * u];
                        }
                    }
                }
            }
            return new Pixbuf(buffer, Colorspace.Rgb, false, 8, w, h, rowBytes);
        }

        /// <summary>
        /// 转换为Cairo.ImageSurface对象，使用完毕后需调用Dispose方法进行释放
        /// </summary>
        public ImageSurface ToImageSurface()
        {
            var buffer = new byte[w * h * 4];
            unsafe
            {
                fixed (byte* srcData = &(Data[0]), dstData = &(buffer[0]))
                {
                    for (int v = 0; v < h; v++)
                    {
                        byte* srcRow = &srcData[v * rowBytes];
                        byte* dstRow = &dstData[v * w * 4];
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
            return new Cairo.ImageSurface(buffer, Cairo.Format.Argb32, w, h, w * 4);
        }

        private byte[] data;
        private int w;
        private int h;
        private int rowBytes;
    }
}