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
			Control = new EtoTextBox { PlaceholderText = "Search" };
		}
	}
}
