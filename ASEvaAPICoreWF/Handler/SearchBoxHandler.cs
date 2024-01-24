using swf = System.Windows.Forms;
using Eto.Forms;
using Eto.WinForms.Forms.Controls;

namespace ASEva.UICoreWF
{
	// CHECK: 增加文字为空时的水印显示 (eto-2.7.5新增的官方版本风格与winform不一致)
	class SearchBoxHandler : TextBoxHandler<EtoTextBox, TextBox, TextBox.ICallback>, SearchBox.IHandler
    {
		public override swf.TextBox SwfTextBox => Control;

		public override EtoTextBox EtoTextBox => Control;

		public SearchBoxHandler()
		{
			Control = new EtoTextBox();
			updatePlaceholder();

			Control.EnabledChanged += delegate
			{
				updatePlaceholder();
			};
		}

		private void updatePlaceholder()
        {
			var lang = Agency.GetAppLanguage();
			if (Control.Enabled) Control.PlaceholderText = lang != null && lang == "ch" ? "搜索" : "Search";
			else Control.PlaceholderText = "";
		}
	}
}
