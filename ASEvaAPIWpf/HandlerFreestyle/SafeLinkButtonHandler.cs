using System;
using Eto.Forms;
using Eto.Wpf.Forms.Controls;

namespace ASEva.UIWpf
{
    // CHECK: 修正重复设置Text导致鼠标事件失效问题
    class SafeLinkButtonHandler : LinkButtonHandler, TextControl.IHandler
    {
        public new string Text
        {
            get => base.Text;
            set
            {
                var str = value == null ? "" : value;
                if (str != base.Text) base.Text = value;
            }
        }
    }
}
