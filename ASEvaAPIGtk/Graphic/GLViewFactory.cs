using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.GtkSharp;

namespace ASEva.UIGtk
{
    class GLViewFactoryGtk : GLView.GLViewBackendFactory
    {
        public GLViewFactoryGtk(String uiBackend, bool useTextTasks, bool useLegacyAPI)
        {
            this.uiBackend = uiBackend;
            this.useTextTasks = useTextTasks;
            this.useLegacyAPI = useLegacyAPI;
        }

        public void CreateGLViewBackend(GLView.GLViewCallback glView, out Control etoControl, out GLView.GLViewBackend glViewBackend)
        {
            try
            {
                if (uiBackend == "x11")
                {
                    if (useTextTasks)
                    {
                        var view = new X11OffscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                    }
                    else
                    {
                        var view = new X11OnscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                    }
                }
                else if (uiBackend == "wayland")
                {
                        var view = new WaylandOffscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                }
                else
                {
                    if (useLegacyAPI)
                    {
                        var envGdkGl = Environment.GetEnvironmentVariable("GDK_GL");
                        var isLegacy = envGdkGl != null && envGdkGl == "LEGACY";
                        if (isLegacy)
                        {
                            var view = new DefaultOffscreenView();
                            view.SetCallback(glView);
                            etoControl = view.ToEto();
                            glViewBackend = view;
                        }
                        else
                        {
                            etoControl = null;
                            glViewBackend = null;
                        }
                    }
                    else
                    {
                        var view = new DefaultOffscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                    }
                }
            }
            catch (Exception)
            {
                etoControl = null;
                glViewBackend = null;
            }
        }

        private String uiBackend;
        private bool useTextTasks;
        private bool useLegacyAPI;
    }
}