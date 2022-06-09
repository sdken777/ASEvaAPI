using System;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIMonoMac
{
    class GLViewFactoryMonoMac : GLView.GLViewBackendFactory
    {
        public void CreateGLViewBackend(GLView.GLViewCallback glView, out Control etoControl, out GLView.GLViewBackend glViewBackend)
        {
            var openglView = new OpenGLView(new MonoMac.CoreGraphics.CGRect(0, 0, 100, 100));
            openglView.SetCallback(glView);
            etoControl = openglView.ToEto();
            glViewBackend = openglView;
        }
    }
}