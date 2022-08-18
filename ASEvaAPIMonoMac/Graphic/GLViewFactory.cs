using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIMonoMac
{
    class GLViewFactoryMonoMac : GLBackendFactory
    {
        public void CreateGLBackend(GLCallback glView, GLOptions options, out Control etoControl, out GLBackend glViewBackend)
        {
            var openglView = new OpenGLView(new MonoMac.CoreGraphics.CGRect(0, 0, 100, 100));
            openglView.SetCallback(glView);
            etoControl = openglView.ToEto();
            glViewBackend = openglView;
        }
    }
}