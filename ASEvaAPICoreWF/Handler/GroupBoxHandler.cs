using sd = System.Drawing;
using swf = System.Windows.Forms;
using Eto.Forms;
using Eto.Drawing;
using Eto.WinForms.Forms;

namespace ASEva.UICoreWF
{
	public class GroupBoxHandler : WindowsPanel<GroupBoxHandler.EtoGroupBox, GroupBox, GroupBox.ICallback>, GroupBox.IHandler
	{
		readonly swf.Panel content;

		public class EtoGroupBox : swf.GroupBox
		{
			public sd.Size GetBorderSize()
			{
				return SizeFromClientSize(sd.Size.Empty);
			}
		}

		public GroupBoxHandler()
		{
			var fontHandler = new Eto.WinForms.Drawing.FontHandler();
			var font = fontHandler.GetFont();

			Control = new EtoGroupBox
			{
				AutoSize = true,
				AutoSizeMode = swf.AutoSizeMode.GrowAndShrink
			};
			content = new swf.Panel
			{
				Font = font,
				Dock = swf.DockStyle.Fill,
				ForeColor = sd.SystemColors.ControlText,
				AutoSize = true,
				AutoSizeMode = swf.AutoSizeMode.GrowAndShrink
			};
			Control.Controls.Add(content);
		}

		protected override Size ContentPadding
		{
			get
			{
				var borderSize = Control.GetBorderSize();
				return new Size(borderSize.Width, borderSize.Height) + base.ContentPadding;
			}
		}

		public override swf.Control ContainerContentControl
		{
			get { return content; }
		}

		public override string Text
		{
			get { return Control.Text; }
			set { Control.Text = value; }
		}
	}
}
