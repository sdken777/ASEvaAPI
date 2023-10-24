using swf = System.Windows.Forms;
using Eto.Forms;
using Eto.WinForms.Forms.Controls;

namespace ASEva.UICoreWF
{
	// CHECK: 增加文字为空时的水印显示
	class SearchBoxHandler : TextBoxHandler<EtoTextBox, TextBox, TextBox.ICallback>, SearchBox.IHandler
    {
		public override swf.TextBox SwfTextBox => Control;

		public override EtoTextBox EtoTextBox => Control;

		public SearchBoxHandler()
		{
			var lang = Agency.GetAppLanguage();
			Control = new EtoTextBox { PlaceholderText = lang != null && lang == "ch" ? "搜索" : "Search" };
		}
	}
}
