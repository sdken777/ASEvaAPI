using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.8.4) 方便操作控件容器的扩展
    /// </summary>
    public static class ContainerExtensions
    {
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