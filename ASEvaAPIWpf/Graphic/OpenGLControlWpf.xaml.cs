using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SharpGL;
using ASEva.UIEto;

namespace ASEva.UIWpf
{
    /// <summary>
    /// OpenGLControlWpf.xaml 的交互逻辑
    /// </summary>
    partial class OpenGLControlWpf : UserControl, GLView.GLViewBackend
    {
        public OpenGLControlWpf()
        {
            InitializeComponent();
        }

        public void SetCallback(GLView.GLViewCallback callback)
        {
            openglControl.SetCallback(callback);
        }

        public void ReleaseGL()
        {
            openglControl.ReleaseGL();
        }

        public void QueueRender()
        {
            var rootWindow = Window.GetWindow(this);
            if (rootWindow != null && rootWindow.WindowState != WindowState.Minimized && Visibility == Visibility.Visible)
            {
                openglControl.QueueDrawWithoutCheckingParentForm();
            }
        }
    }
}
