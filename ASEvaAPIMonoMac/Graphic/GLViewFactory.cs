using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIMonoMac
{
    class GLViewFactoryMonoMac : GLBackendFactory
    {
        public void CreateGLBackend(GLCallback glView, GLOptions options, out Control etoControl, out GLBackend glViewBackend, out bool supportOverlay)
        {
            var openglView = new OpenGLView(options.UseLegacyAPI);
            openglView.SetCallback(glView);
            etoControl = openglView.ToEto();
            glViewBackend = openglView;
            supportOverlay = true;
        }
    }
}