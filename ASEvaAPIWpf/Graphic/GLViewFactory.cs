using System;
using System.Collections.Generic;
using System.Text;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Wpf;

namespace ASEva.UIWpf
{
    class GLViewFactoryWpf : GLBackendFactory
    {
        public void CreateGLBackend(GLCallback glView, GLOptions options, out Control etoControl, out GLBackend glViewBackend, out bool supportOverlay)
        {
            if (options.EnableOnscreenRendering)
            {
                var openglControl = new OpenGLBlitControlWpf(glView, options.RequestAntialias);
                etoControl = openglControl.ToEto();
                glViewBackend = openglControl;
                supportOverlay = true;
            }
            else
            {
                var openglControl = new OpenGLControlWpf(glView, options.RequestAntialias);
                etoControl = openglControl.ToEto();
                glViewBackend = openglControl;
                supportOverlay = true;
            }
        }
    }
}
