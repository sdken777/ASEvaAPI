using swf = System.Windows.Forms;
using Eto.Forms;
using Eto.WinForms.Forms.Controls;

namespace ASEva.UICoreWF
{
	public class SearchBoxHandler : TextBoxHandler<SearchBoxWithIcon, TextBox, TextBox.ICallback>, SearchBox.IHandler
	{
		public SearchBoxHandler()
        {
			Control = new SearchBoxWithIcon();
        }

		public override swf.TextBox SwfTextBox => Control.TextBox;

		public override EtoTextBox EtoTextBox => Control.TextBox;
	}
}
