using System;
using System.Collections.Generic;
using Eto.Forms;

namespace ASEva.UIEto
{
    /// \~English
    /// <summary>
    /// (api:eto=3.3.0) Control and its mouse event source controls
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.3.0) 控件及其鼠标事件源控件
    /// </summary>
    public class ControlAndMouseSources(Control control)
    {
        public Control Control { get; set; } = control;
        public List<Control> MouseSources { get; set; } = [];
    }
}