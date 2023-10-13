using System;
using System.Collections.Generic;
using ASEva;
using ASEva.Samples;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.11.3) 方便生成图标的扩展
    /// </summary>
    public static class IconExtensions
    {
        public static Icon ToIcon(this byte[] imageData)
        {
            try
            {
                var bitmap = new Bitmap(imageData);
                if (bitmap == null) return null;
                else return ToIcon(bitmap);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Icon ToIcon(this Bitmap bitmap)
        {
            var image = ImageConverter.ConvertFromBitmap(bitmap);
            if (image == null) return null;
            else return ToIcon(image);
        }

        public static Icon ToIcon(this CommonImage image)
        {
            var fineIcon = makeFine(image);
            if (fineIcon == null) return new Icon(1, image.ToEtoBitmap());
            else return fineIcon;
        }

        private static Icon makeFine(CommonImage image)
        {
            if (!image.WithAlpha) return null;

            int resizeTime = 0;
            if (image.Width == 256 || image.Height == 256) resizeTime = 3;
            else if (image.Width == 128 || image.Height == 128) resizeTime = 2;
            else if (image.Width == 64 || image.Height == 64) resizeTime = 1;
            else return null;

            if (Pixel.Scale > 1.5f) resizeTime -= 1;

            var iconFrames = new List<IconFrame>();
            iconFrames.Add(new IconFrame(1, image.ToEtoBitmap()));
            for (int i = 0; i < resizeTime; i++)
            {
                image = resizeHalf(image);
                iconFrames.Add(new IconFrame(1, image.ToEtoBitmap()));
            }

            if (FinalFrameOnly) return new Icon(iconFrames[iconFrames.Count - 1]);
            else return new Icon(iconFrames.ToArray());
        }

        private static CommonImage resizeHalf(CommonImage input)
        {
            var output = CommonImage.Create(input.Width / 2, input.Height / 2, true);
            var inputData = input.Data;
            var inputStep = input.RowBytes;
            var outputData = output.Data;
            var outputStep = output.RowBytes;
            for (int v = 0; v < output.Height; v++)
            {
                int outputRowIndex = v * outputStep;
                int inputRowIndex1 = 2 * v * inputStep;
                int inputRowIndex2 = (2 * v + 1) * inputStep;
                for (int u = 0; u < output.Width; u++)
                {
                    int outputCellIndex = outputRowIndex + 4 * u;
                    int inputCellIndex1 = inputRowIndex1 + 8 * u;
                    int inputCellIndex2 = inputRowIndex2 + 8 * u;
                    for (int n = 0; n < 4; n++)
                    {
                        outputData[outputCellIndex + n] = (byte)(((uint)inputData[inputCellIndex1 + n] + (uint)inputData[inputCellIndex1 + 4 + n] + (uint)inputData[inputCellIndex2 + n] + (uint)inputData[inputCellIndex2 + 4 + n]) >> 2);
                    }
                }
            }
            return output;
        }

        public static bool FinalFrameOnly { private get; set; }
    }
}