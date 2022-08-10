using swf = System.Windows.Forms;
using Eto.Forms;
using Eto.WinForms.Forms.Controls;

namespace ASEva.UICoreWF
{
	public class SearchBoxHandler : TextBoxHandler<EtoTextBox, TextBox, TextBox.ICallback>, SearchBox.IHandler
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
