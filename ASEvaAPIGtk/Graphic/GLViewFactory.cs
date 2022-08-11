using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.GtkSharp;

namespace ASEva.UIGtk
{
    class GLViewFactoryGtk : GLView.GLViewBackendFactory
    {
        public GLViewFactoryGtk(String uiBackend, bool preferOnscreen)
        {
            this.uiBackend = uiBackend;
            this.preferOnscreen = preferOnscreen;
        }

        public void CreateGLViewBackend(GLView.GLViewCallback glView, out Control etoControl, out GLView.GLViewBackend glViewBackend)
        {
            try
            {
                if (uiBackend == "x11") // DefaultOnscreenView在x11下存在不少兼容性问题
                {
                    if (preferOnscreen)
                    {
                        var view = new X11OnscreenView();
                        view.SetCallback(glView);
                        etoControl = view.ToEto();
                        glViewBackend = view;
                    }
                    else
                    {
                        var view = new X11OffscreenView();
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
        private bool preferOnscreen;
    }
}