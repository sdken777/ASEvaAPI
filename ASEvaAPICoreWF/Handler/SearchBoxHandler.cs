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

			var deleteButton = new swf.Label();
			deleteButton.AutoSize = false;
			deleteButton.Width = (int)(12.0f * Control.DeviceDpi / 96);
			deleteButton.Text = "x";
			deleteButton.Dock = swf.DockStyle.Right;
			Control.Controls.Add(deleteButton);

			Control.EnabledChanged += delegate
			{
				updatePlaceholder();
			};

			Control.MouseMove += (o, e) =>
			{
				if (e.Button == swf.MouseButtons.None) Cursor = Cursors.IBeam;
			};
			deleteButton.MouseMove += (o, e) =>
			{
				if (e.Button == swf.MouseButtons.None) Cursor = Cursors.Default;
			};
			deleteButton.MouseClick += delegate
			{
				Control.Text = "";
			};
		}

		private void updatePlaceholder()
        {
			if (Control.Enabled) Control.PlaceholderText = Agency.GetAppLanguage() == Language.Chinese ? "搜索" : "Search";
			else Control.PlaceholderText = "";
		}
	}
}
