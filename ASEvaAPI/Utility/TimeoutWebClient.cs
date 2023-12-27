using System;
using System.Net;

namespace ASEva.Utility
{
    #pragma warning disable SYSLIB0014

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) WebClient with Timeout property
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 带Timeout属性的WebClient
    /// </summary>
    public class TimeoutWebClient : WebClient
    {
        /// \~English
        /// <summary>
        /// Timeout, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 超时，单位毫秒
        /// </summary>
        public int Timeout { get; set; }

        /// \~English
        /// <summary>
        /// Default constructor, default timeout is 5 seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 构造函数，默认超时5秒
        /// </summary>
        public TimeoutWebClient()
        {
            Timeout = 5000;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).Timeout = Timeout;
            }
            return request;
        }
    }
}
