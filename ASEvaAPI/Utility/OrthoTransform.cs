using System;

namespace ASEva.Utility
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Coordinates of longitude and latitude
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 经纬度坐标
    /// </summary>
    public struct LocPoint
    {
        /// \~English
        /// <summary>
        /// Longitude
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }

        /// \~English
        /// <summary>
        /// Latitude
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Parameters of orthogonal coordinate system
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 正交坐标系参数
    /// </summary>
    public class OrthoParameters
    {
        /// \~English
        /// <summary>
        /// Longitude of the orthogonal coordinate system's origin
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 原点经度
        /// </summary>
        public double OrthoCenterLng { get; set; }

        /// \~English
        /// <summary>
        /// Latitude of the orthogonal coordinate system's origin
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 原点纬度
        /// </summary>
        public double OrthoCenterLat { get; set; }

        /// \~English
        /// <summary>
        /// Sphere's radius
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 球半径
        /// </summary>
        public double OrthoRadius { get; set; }

        /// \~English
        /// <summary>
        /// Default constructor, default origin is (0, 0) and use the earth's radius
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 构造函数，默认原点为(0, 0)，默认球半径为地球半径
        /// </summary>
        public OrthoParameters()
        {
            OrthoCenterLng = 0;
            OrthoCenterLat = 0;
            OrthoRadius = 6371393;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Conversion of orthogonal coordinates (Use with caution, as there may be an error of up to 0.2%)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 正交坐标系变换（谨慎使用，可能存在高达0.2%的误差）
    /// </summary>
    public class OrthoTransform
    {
        /// \~English
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param">Parameters of orthogonal coordinate system</param>
        /// \~Chinese
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="param">正交坐标系参数</param>
        public OrthoTransform(OrthoParameters param)
        {
            this.param = param;
            rad2deg = 180.0 / Math.PI;
            deg2rad = Math.PI / 180;
            radius = param.OrthoRadius;
            lat0Cos = Math.Cos(param.OrthoCenterLat * deg2rad);
            lat0Sin = Math.Sin(param.OrthoCenterLat * deg2rad);
            lng0Rad = param.OrthoCenterLng * deg2rad;
        }

        /// \~English
        /// <summary>
        /// Convert global coordinates to orthogonal coordinates
        /// </summary>
        /// <param name="src">Global coordinates</param>
        /// <returns>Orthogonal coordinates</returns>
        /// \~Chinese
        /// <summary>
        /// 全球坐标系转正交坐标系
        /// </summary>
        /// <param name="src">经纬度坐标</param>
        /// <returns>正交坐标系坐标</returns>
        public FloatPoint GlobalToOrtho(LocPoint src)
        {
            double latRad = src.Lat * deg2rad;
            double lngRad = src.Lng * deg2rad;
            double latCos = Math.Cos(latRad);
            double x = radius * latCos * Math.Sin(lngRad - lng0Rad);
            double y = radius * (lat0Cos * Math.Sin(latRad) - lat0Sin * latCos * Math.Cos(lngRad - lng0Rad));
            return new FloatPoint((float)x, (float)y);
        }

        /// \~English
        /// <summary>
        /// Convert orthogonal coordinates to global coordinates
        /// </summary>
        /// <param name="src">Orthogonal coordinates</param>
        /// <returns>Global coordinates</returns>
        /// \~Chinese
        /// <summary>
        /// 正交坐标系转全球坐标系
        /// </summary>
        /// <param name="src">正交坐标系坐标</param>
        /// <returns>经纬度坐标</returns>
        public LocPoint OrthoToGlobal(FloatPoint src)
        {
            if (src.X == 0 && src.Y == 0) return new LocPoint() { Lat = param.OrthoCenterLat, Lng = param.OrthoCenterLng };
            double rou = Math.Sqrt((double)(src.X * src.X + src.Y * src.Y));
            double cSin = rou / radius;
            double cCos = Math.Cos(Math.Asin(cSin));
            double lat = Math.Asin(cCos * lat0Sin + src.Y * cSin * lat0Cos / rou);
            double lng = lng0Rad + Math.Atan2(src.X * cSin, rou * cCos * lat0Cos - src.Y * cSin * lat0Sin);
            return new LocPoint() { Lat = lat * rad2deg, Lng = lng * rad2deg };
        }

        private OrthoParameters param;
        private double radius;
        private double lat0Sin;
        private double lat0Cos;
        private double lng0Rad;
        private double rad2deg;
        private double deg2rad;
    }
}