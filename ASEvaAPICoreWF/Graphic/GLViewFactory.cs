using System;
using System.Collections.Generic;
using System.Text;
using ASEva.UIEto;
using Eto.Forms;
using Eto.WinForms;

namespace ASEva.UICoreWF
{
    class GLViewFactoryCoreWF : GLBackendFactory
    {
        public void CreateGLBackend(GLCallback glView, GLOptions options, out Control etoControl, out GLBackend glViewBackend)
        {
            if (!options.EnableOnscreenRendering || options.UseTextTasks)
            {
                var openglControl = new OpenGLControl();
                openglControl.SetCallback(glView);
                etoControl = openglControl.ToEto();
                glViewBackend = openglControl;
            }
            else
            {
                var openglControl = new OpenGLOnscreenControl();
                openglControl.SetCallback(glView);
                etoControl = openglControl.ToEto();
                glViewBackend = openglControl;
            }
        }
    }
}
