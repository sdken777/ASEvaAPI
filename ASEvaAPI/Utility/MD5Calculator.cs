using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ASEva.Utility
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) MD5 calculator
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) MD5计算
    /// </summary>
    public class MD5Calculator
    {
        /// \~English
        /// <summary>
        /// Calculate MD5 of file
        /// </summary>
        /// <param name="fileName">File path</param>
        /// <returns>MD5 string</returns>
        /// \~Chinese
        /// <summary>
        /// 计算文件MD5
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>MD5字符串</returns>
        public static string? Calculate(string fileName)
        {
            try
            {
                
                var file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var md5 = MD5.Create();
                var retVal = md5.ComputeHash(file);
                file.Close();
 
                var sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex) { Dump.Exception(ex); }
            return null;
        }

        /// \~English
        /// <summary>
        /// Calculate MD5 of binary data
        /// </summary>
        /// <param name="data">Binary data</param>
        /// <returns>MD5 string</returns>
        /// \~Chinese
        /// <summary>
        /// 计算二进制数据块MD5
        /// </summary>
        /// <param name="data">二进制数据块</param>
        /// <returns>MD5字符串</returns>
        public static string? Calculate(byte[] data)
        {
            try
            {
                var md5 = MD5.Create();
                var retVal = md5.ComputeHash(data);

                var sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex) { Dump.Exception(ex); }
            return null;
        }
    }
}
