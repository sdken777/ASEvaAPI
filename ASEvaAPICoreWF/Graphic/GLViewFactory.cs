using System;
using System.Collections.Generic;
using System.Text;
using ASEva.UIEto;
using Eto.Forms;
using Eto.WinForms;

namespace ASEva.UICoreWF
{
    class GLViewFactoryCoreWF : GLView.GLViewBackendFactory
    {
        public void CreateGLViewBackend(GLView.GLViewCallback glView, out Control etoControl, out GLView.GLViewBackend glViewBackend)
        {
            var openglControl = new OpenGLControl();
            openglControl.SetCallback(glView);
            etoControl = openglControl.ToEto();
            glViewBackend = openglControl;
        }
    }
}
