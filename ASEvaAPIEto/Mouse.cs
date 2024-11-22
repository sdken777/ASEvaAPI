using System;
using System.Collections.Generic;
using Eto.Forms;

namespace ASEva.UIEto
{
    /// \~English
    /// <summary>
    /// (api:eto=3.1.1) Control and its mouse event source controls
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.1.1) 控件及其鼠标事件源控件
    /// </summary>
    public class ControlAndMouseSources
    {
        public Control Control { get; set; }
        public List<Control> MouseSources { get; set; }
    }
}