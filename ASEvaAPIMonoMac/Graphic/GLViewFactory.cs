using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIMonoMac
{
    class GLViewFactoryMonoMac : GLBackendFactory
    {
        public void CreateGLBackend(GLCallback glView, GLOptions options, out Control etoControl, out GLBackend glViewBackend, out bool supportOverlay)
        {
            var openglView = new OpenGLViewContainer(glView, options.RequestAntialias, options.UseLegacyAPI);
            etoControl = openglView.ToEto();
            glViewBackend = openglView;
            supportOverlay = true;
        }
    }
}