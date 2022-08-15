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
        public GLViewFactoryCoreWF(bool useTextTasks)
        {
            this.useTextTasks = useTextTasks;
        }

        public void CreateGLViewBackend(GLView.GLViewCallback glView, out Control etoControl, out GLView.GLViewBackend glViewBackend)
        {
            if (useTextTasks)
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

        private bool useTextTasks;
    }
}
