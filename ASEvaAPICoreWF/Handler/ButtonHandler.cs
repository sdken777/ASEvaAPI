using System;
using System.Drawing;

namespace ASEva.UICoreWF
{
	class ButtonHandler : Eto.WinForms.Forms.Controls.ButtonHandler
	{
		protected override void Initialize()
		{
			base.Initialize();

			// CHECK: ������������°�ť����ɫ��͸������
			Control.BackColor = Color.Transparent;
		}
	}
}
