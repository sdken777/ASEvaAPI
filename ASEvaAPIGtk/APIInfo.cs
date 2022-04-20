/*! \mainpage
 * 此类库为ASEva-API中基于Gtk#的部分。详见 ASEva.UIGtk \n
 * 本文档对应API版本：2.1.3\n
 * \n
 * Gtk#官方仓库: https://github.com/GtkSharp/GtkSharp \n
 * 镜像仓库: https://hub.fastgit.org/GtkSharp/GtkSharp \n
 * API文档(C): https://gnome.pages.gitlab.gnome.org/gtk/gtk3
 */

using System;

namespace ASEva.UIGtk
{
    /// <summary>
    /// version=2.1.3
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:gtk=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 1, 3, 0); // 更改样式令字体尺寸等与其他平台大致相同
        }
    }
}
