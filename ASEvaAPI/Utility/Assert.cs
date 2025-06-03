using System;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.9.4) Used to ensure that subsequent code is executed under specified conditions 
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.9.4) 用于确保在指定条件下执行后续代码
    /// </summary>
    public class Assert
    {
        /// \~English
        /// <summary>
        /// Throw an exception if not running on the host side
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 若非主机端运行则触发异常
        /// </summary>
        public static void HostSide()
        {
            if (AgencyLocal.ClientSide) throw new InvalidOperationException("Only available for host side program.");
        }

        /// \~English
        /// <summary>
        /// Throw an exception if not running on the client side
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 若非客户端运行则触发异常
        /// </summary>
        public static void ClientSide()
        {
            if (!AgencyLocal.ClientSide) throw new InvalidOperationException("Only available for client side program.");
        }
    }
}