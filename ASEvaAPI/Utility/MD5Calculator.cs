using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.0.0) MD5计算
    /// </summary>
    public class MD5Calculator
    {
        /// <summary>
        /// 计算文件MD5
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>MD5字符串</returns>
        public static string Calculate(string fileName)
        {
            try
            {
                var file = new FileStream(fileName, FileMode.Open);
                var md5 = new MD5CryptoServiceProvider();
                var retVal = md5.ComputeHash(file);
                file.Close();
 
                var sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception) {}
            return null;
        }

        /// <summary>
        /// 计算二进制数据块MD5
        /// </summary>
        /// <param name="data">二进制数据块</param>
        /// <returns>MD5字符串</returns>
        public static string Calculate(byte[] data)
        {
            try
            {
                var md5 = new MD5CryptoServiceProvider();
                var retVal = md5.ComputeHash(data);

                var sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception) { }
            return null;
        }
    }
}
