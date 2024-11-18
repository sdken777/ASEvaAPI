using System;

namespace ASEva.Utility
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Conversion of map coordinates
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 地图坐标系转换
    /// </summary>
    public class MapCoordination
    {
        private static double a = 6378245.0;
        private static double ee = 0.00669342162296594323;
        private static double x_pi = Math.PI * 3000.0 / 180.0;

        /// \~English
        /// <summary>
        /// Convert WGS coordinates to Baidu coordinates
        /// </summary>
        /// <param name="src">WGS coordinates</param>
        /// <returns>Baidu coordinates</returns>
        /// \~Chinese
        /// <summary>
        /// WGS坐标转百度坐标
        /// </summary>
        /// <param name="src">WGS坐标系坐标</param>
        /// <returns>百度坐标系坐标</returns>
        public static LocPoint wgs2bd(LocPoint src)
        {
            return gcj2bd(wgs2gcj(src));
        }

        /// \~English
        /// <summary>
        /// Convert GCJ coordinates to Baidu coordinates
        /// </summary>
        /// <param name="src">GCJ coordinates</param>
        /// <returns>Baidu coordinates</returns>
        /// \~Chinese
        /// <summary>
        /// GCJ坐标转百度坐标
        /// </summary>
        /// <param name="src">GCJ坐标系坐标</param>
        /// <returns>百度坐标系坐标</returns>
        public static LocPoint gcj2bd(LocPoint src)
        {
            double x = src.Lng, y = src.Lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            double bd_lon = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;
            return new LocPoint() { Lng = bd_lon, Lat = bd_lat };
        }

        /// \~English
        /// <summary>
        /// Convert WGS coordinates to GCJ coordinates
        /// </summary>
        /// <param name="src">WGS coordinates</param>
        /// <returns>GCJ coordinates</returns>
        /// \~Chinese
        /// <summary>
        /// WGS坐标转GCJ坐标
        /// </summary>
        /// <param name="src">WGS坐标系坐标</param>
        /// <returns>GCJ坐标系坐标</returns>
        public static LocPoint wgs2gcj(LocPoint src)
        {
            double dLat = transformLat(src.Lng - 105.0, src.Lat - 35.0);
            double dLon = transformLon(src.Lng - 105.0, src.Lat - 35.0);
            double radLat = src.Lat / 180.0 * Math.PI;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * Math.PI);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * Math.PI);
            double mgLat = src.Lat + dLat;
            double mgLon = src.Lng + dLon;
            return new LocPoint() { Lng = mgLon, Lat = mgLat };
        }

        private static double transformLat(double lat, double lon)
        {
            double ret = -100.0 + 2.0 * lat + 3.0 * lon + 0.2 * lon * lon + 0.1 * lat * lon + 0.2 * Math.Sqrt(Math.Abs(lat));
            ret += (20.0 * Math.Sin(6.0 * lat * Math.PI) + 20.0 * Math.Sin(2.0 * lat * Math.PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lon * Math.PI) + 40.0 * Math.Sin(lon / 3.0 * Math.PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lon / 12.0 * Math.PI) + 320 * Math.Sin(lon * Math.PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }
        private static double transformLon(double lat, double lon)
        {
            double ret = 300.0 + lat + 2.0 * lon + 0.1 * lat * lat + 0.1 * lat * lon + 0.1 * Math.Sqrt(Math.Abs(lat));
            ret += (20.0 * Math.Sin(6.0 * lat * Math.PI) + 20.0 * Math.Sin(2.0 * lat * Math.PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lat * Math.PI) + 40.0 * Math.Sin(lat / 3.0 * Math.PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lat / 12.0 * Math.PI) + 300.0 * Math.Sin(lat / 30.0 * Math.PI)) * 2.0 / 3.0;
            return ret;
        }
    }
}