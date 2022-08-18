using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.GtkSharp;

namespace ASEva.UIGtk
{
    class GLViewFactoryGtk : GLBackendFactory
    {
        public GLViewFactoryGtk(String uiBackend)
        {
            this.uiBackend = uiBackend;
        }

        public void CreateGLBackend(GLCallback glView, GLOptions options, out Control etoControl, out GLBackend glViewBackend)
        {
            try
            {
                if (uiBackend == "x11")
                {
                    if (!options.EnableOnscreenRendering || options.UseTextTasks)
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
                    if (options.UseLegacyAPI)
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
    }
}