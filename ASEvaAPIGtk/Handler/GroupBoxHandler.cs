using Eto.Forms;
using Eto.Drawing;
using Eto.GtkSharp;
using Eto.GtkSharp.Forms;

namespace ASEva.UIGtk
{
	class GroupBoxHandler : GtkPanel<Gtk.Frame, GroupBox, GroupBox.ICallback>, GroupBox.IHandler
	{
		public GroupBoxHandler ()
		{
			Control = new Gtk.Frame ();

			// CHECK: 标题改为居中显示
			Control.LabelXalign = 0.5f;
		}

		protected override Gtk.Widget FontControl => Control.LabelWidget ?? new Gtk.Label();

		// CHECK: 修正标题两侧无空隙问题
		public override string Text {
			get { return textValue; }
			set
			{
				textValue = value;
				var needsFont = Control.LabelWidget == null && Font != null;
				Control.Label = " " + textValue + " ";
				if (needsFont)
					Control.LabelWidget?.SetFont(Font.ToPango());
			}
		}
		private string textValue = "";

		public override Size ClientSize {
			get {
				if (Control.Visible && Control.Child != null)
					return Control.Child.Allocation.Size.ToEto ();
				else {
					var label = Control.LabelWidget;
					var size = Size;
					size.Height -= label.Allocation.Height + 10;
					size.Width -= 10;
					return size;
				}
			}
			set {
				var label = Control.LabelWidget;
				var size = value;
				size.Height += label.Allocation.Height + 10;
				size.Width += 10;
				Size = size;
			}
		}

		protected override void SetContainerContent(Gtk.Widget content)
		{
			Control.Add(content);

			/*if (clientSize != null) {
				var label = Control.LabelWidget;
				Control.SetSizeRequest(clientSize.Value.Width + 10, clientSize.Value.Height + label.Allocation.Height + 10);
				clientSize = null;
			}*/
		}

		public Color TextColor
		{
			get { return Control.LabelWidget.GetForeground(); }
			set { Control.LabelWidget.SetForeground(value); }
		}
	}
}
