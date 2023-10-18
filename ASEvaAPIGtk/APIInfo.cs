/*! \mainpage
 * \~English This library contains the APIs for Gtk#. See ASEva.UIGtk for details. \n
 * \~Chinese 此类库为ASEva-API中基于Gtk#的部分。详见 ASEva.UIGtk \n
 * \~English This document corresponds to API version: 2.7.1 \n\n
 * \~Chinese 本文档对应API版本：2.7.1 \n\n
 * 
 * \~English Gtk# official repository: https://github.com/GtkSharp/GtkSharp \n
 * \~Chinese Gtk#官方仓库: https://github.com/GtkSharp/GtkSharp \n
 * \~English API reference (C): https://docs.gtk.org/gtk3
 * \~Chinese API文档(C): https://docs.gtk.org/gtk3
 */

using System;

namespace ASEva.UIGtk
{
    /// <summary>
    /// version=2.7.1
    /// </summary>
    public class APIInfo
    {
        /// \~English
        /// <summary>
        /// (api:gtk=2.0.0) Get API version
        /// </summary>
        /// <returns>The API version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:gtk=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 7, 1, 3); // Update log / 更新记录: 修正HistLineGraph在单柱时的显示
        }
    }
}
