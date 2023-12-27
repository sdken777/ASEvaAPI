using System;
using ASEva;

namespace ASEva
{
    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Color described by RGBA channels (integer ranges 0~255)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 以RGBA四通道描述的颜色（整型0~255）
    /// </summary>
    public struct ColorRGBA
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public ColorRGBA(byte red, byte green, byte blue)
        {
            R = red;
            G = green;
            B = blue;
            A = 255;
        }

        public ColorRGBA(byte red, byte green, byte blue, byte alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        public ColorRGBA(ColorRGBA baseColor, byte alpha)
        {
            R = baseColor.R;
            G = baseColor.G;
            B = baseColor.B;
            A = alpha;
        }

        public static ColorRGBA Black
        {
            get { return new ColorRGBA(0, 0, 0); }
        }

        public static ColorRGBA White
        {
            get { return new ColorRGBA(255, 255, 255); }
        }

        public static ColorRGBA WhiteSmoke
        {
            get { return new ColorRGBA(245, 245, 245); }
        }

        public static ColorRGBA Gray
        {
            get { return new ColorRGBA(128, 128, 128); }
        }

        public static ColorRGBA LightGray
        {
            get { return new ColorRGBA(211, 211, 211); }
        }

        public static ColorRGBA DimGray
        {
            get { return new ColorRGBA(105, 105, 105); }
        }

        public static ColorRGBA DarkGray
        {
            get { return new ColorRGBA(169, 169, 169); }
        }

        public static ColorRGBA Silver
        {
            get { return new ColorRGBA(192, 192, 192); }
        }

        public static ColorRGBA Red
        {
            get { return new ColorRGBA(255, 0, 0); }
        }

        public static ColorRGBA DarkOrange
        {
            get { return new ColorRGBA(255, 140, 0); }
        }

        public static ColorRGBA Green
        {
            get { return new ColorRGBA(0, 128, 0); }
        }

        public static ColorRGBA LimeGreen
        {
            get { return new ColorRGBA(50, 205, 50); }
        }

        public static ColorRGBA Tomato
        {
            get { return new ColorRGBA(255, 99, 71); }
        }

        public static ColorRGBA Orange
        {
            get { return new ColorRGBA(255, 165, 0); }
        }

        public static ColorRGBA Blue
        {
            get { return new ColorRGBA(0, 0, 255); }
        }

        public static ColorRGBA RoyalBlue
        {
            get { return new ColorRGBA(65,105,225); }
        }

        public static ColorRGBA DodgerBlue
        {
            get { return new ColorRGBA(30, 144, 255); }
        }

        public static ColorRGBA LightSteelBlue
        {
            get { return new ColorRGBA(176, 196, 222); }
        }

        public static ColorRGBA LightBlue
        {
            get { return new ColorRGBA(173, 216, 230); }
        }

        public static ColorRGBA Yellow
        {
            get { return new ColorRGBA(255, 255, 0); }
        }

        public static ColorRGBA LightYellow
        {
            get { return new ColorRGBA(255, 255, 224); }
        }

        public static ColorRGBA PaleGreen
        {
            get { return new ColorRGBA(152, 251, 152); }
        }

        public static ColorRGBA LightSalmon
        {
            get { return new ColorRGBA(255, 160, 122); }
        }

        public static ColorRGBA Salmon
        {
            get { return new ColorRGBA(250, 128, 114); }
        }

        public static ColorRGBA Lavender
        {
            get { return new ColorRGBA(230, 230, 250); }
        }

        public static ColorRGBA Gold
        {
            get { return new ColorRGBA(255, 215, 0); }
        }

        public static ColorRGBA Pink
        {
            get { return new ColorRGBA(255, 192, 203); }
        }

        public static ColorRGBA Cyan
        {
            get { return new ColorRGBA(0, 255, 255); }
        }

        public static ColorRGBA Purple
        {
            get { return new ColorRGBA(128, 0, 128); }
        }

        public static ColorRGBA MediumPurple
        {
            get { return new ColorRGBA(147, 112, 219); }
        }

        public static ColorRGBA Violet
        {
            get { return new ColorRGBA(238, 130, 238); }
        }

        public static ColorRGBA Turquoise
        {
            get { return new ColorRGBA(64, 224, 208); }
        }

        public static ColorRGBA MediumTurquoise
        {
            get { return new ColorRGBA(72, 209, 204); }
        }
    }
}
