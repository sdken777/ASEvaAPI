using System;
using swc = System.Windows.Controls;
using sw = System.Windows;
using Eto.Forms;
using Eto.Drawing;
using Eto.Wpf.Forms;

namespace ASEva.UIWpf
{
    // CHECK: 改为支持show password的版本
    class PasswordBoxHandler : WpfControl<Handler.PeekablePasswordBox, PasswordBox, PasswordBox.ICallback>, PasswordBox.IHandler
	{
        protected override sw.Size DefaultSize => new sw.Size(80, 23);

        protected override bool PreventUserResize { get { return true; } }

        public PasswordBoxHandler()
		{
			Control = new Handler.PeekablePasswordBox();
		}

        public override bool UseMousePreview { get { return true; } }

        public override bool UseKeyPreview { get { return true; } }

        public override void AttachEvent(string id)
        {
            switch (id)
            {
                case TextControl.TextChangedEvent:
                    Control.PasswordChanged += delegate
                    {
                        Callback.OnTextChanged(Widget, EventArgs.Empty);
                    };
                    break;
                default:
                    base.AttachEvent(id);
                    break;
            }
        }

        public bool ReadOnly
        {
            get { return false; }
            set { }
        }

        public char PasswordChar
        {
            get { return Control.PasswordChar; }
            set { Control.PasswordChar = value; }
        }

        public int MaxLength
        {
            get { return Control.MaxLength; }
            set { Control.MaxLength = value; }
        }

        public string Text
        {
            get { return Control.Password; }
            set { Control.Password = value; }
        }
    }
}
