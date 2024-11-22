using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Extensions for container object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 方便操作控件容器的扩展
    /// </summary>
    public static class ContainerExtensions
    {
        /// \~English
        /// <summary>
        /// Release the resources of all views such as SkiaView in the container
        /// </summary>
        /// <param name="container">Container object</param>
        /// \~Chinese
        /// <summary>
        /// 释放控件容器中的所有SkiaView等视图的资源
        /// </summary>
        /// <param name="container">控件容器对象</param>
        public static void CloseRecursively(this Container container)
        {
            foreach (var control in container.Controls)
            {
                if (control is GLView) (control as GLView).Close();
                else if (control is SkiaView) (control as SkiaView).Close();
                else if (control is Container) (control as Container).CloseRecursively();
            }
        }
    }
}