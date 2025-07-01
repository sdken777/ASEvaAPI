using System;
using System.Drawing;

namespace ASEva.UICoreWF
{
	class ButtonHandler : Eto.WinForms.Forms.Controls.ButtonHandler
	{
		protected override void Initialize()
		{
			base.Initialize();

			// CHECK: 修正部分情况下按钮背景色不透明问题
			Control.BackColor = Color.Transparent;
		}
	}
}
