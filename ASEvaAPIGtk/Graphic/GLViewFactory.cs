using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.GtkSharp;

namespace ASEva.UIGtk
{
    class GLViewFactoryGtk : GLView.GLViewBackendFactory
    {
        public GLViewFactoryGtk(String uiBackend)
        {
            this.uiBackend = uiBackend;
        }

        public void CreateGLViewBackend(GLView.GLViewCallback glView, out Control etoControl, out GLView.GLViewBackend glViewBackend)
        {
            try
            {
                if (uiBackend == "x11") // DefaultOnscreenView在x11下存在不少兼容性问题
                {
                    var view = new X11OffscreenView();
                    view.SetCallback(glView);
                    etoControl = view.ToEto();
                    glViewBackend = view;
                }
                else if (uiBackend == "wayland")
                {
                    var envGdkGl = Environment.GetEnvironmentVariable("GDK_GL");
                    var isLegacy = envGdkGl != null && envGdkGl == "LEGACY";
                    if (isLegacy)
                    {
                        var view = new DefaultOnscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                    }
                    else // 非LEGACY的wayland下DefaultOnscreenView不支持部分OpenGL3以下的API，只能使用离屏渲染
                    {
                        var view = new WaylandOffscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                    }
                }
                else
                {
                    var view = new DefaultOnscreenView();
                    view.SetCallback(glView);
                    etoControl = view.ToEto();
                    glViewBackend = view;
                }
            }
            catch (Exception)
            {
                etoControl = null;
                glViewBackend = null;
            }
        }

        private String uiBackend;
    }
}