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

        public void CreateGLBackend(GLCallback glView, GLOptions options, out Control etoControl, out GLBackend glViewBackend, out bool supportOverlay)
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
                        supportOverlay = true;
                    }
                    else
                    {
                        var view = new X11OnscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                        supportOverlay = false;
                    }
                }
                else if (uiBackend == "wayland")
                {
                    if (options.EnableOnscreenRendering)
                    {
                        var view = new DefaultBlitView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                        supportOverlay = true;
                    }
                    else
                    {
                        var view = new WaylandOffscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                        supportOverlay = true;
                    }
                }
                else
                {
                    if (options.EnableOnscreenRendering)
                    {
                        var view = new DefaultBlitView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                        supportOverlay = true;
                    }
                    else
                    {
                        var view = new DefaultOffscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                        supportOverlay = true;
                    }
                }
            }
            catch (Exception)
            {
                etoControl = null;
                glViewBackend = null;
                supportOverlay = true;
            }
        }

        private String uiBackend;
    }
}