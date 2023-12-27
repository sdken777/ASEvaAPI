using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ASEva.UICoreWF
{
	/// \~English
	/// <summary>
	/// (api:corewf=2.0.0) Wrapper of FlowLayoutPanel
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:corewf=2.0.0) FlowLayoutPanel封装
	/// </summary>
	public class FlowPanelManager
	{
		public FlowPanelManager(FlowLayoutPanel panel, bool autoResizeControls)
		{
			this.panel = panel;
			this.autoResize = autoResizeControls;

			if (autoResize) panel.SizeChanged += panel_SizeChanged;
		}

		public void Add(Control control, bool visible)
		{
			controls.Add(control);
			visibles[control] = visible;
		}

		public void Insert(int index, Control control, bool visible)
		{
			if (index >= controls.Count) Add(control, visible);
			else
			{
				controls.Insert(Math.Max(0, index), control);
				visibles[control] = visible;
			}
		}

		public void Remove(Control control)
		{
			if (visibles.ContainsKey(control))
			{
				if (visibles[control])
				{
					panel.Controls.Remove(control);
					removedFromPanel = true;
				}
				controls.Remove(control);
				visibles.Remove(control);
			}
		}

		public Control[] GetControls()
		{
			return controls.ToArray();
		}

		public void SetVisible(Control control, bool visible)
		{
			if (visibles.ContainsKey(control))
			{
				visibles[control] = visible;
				if (!visible)
				{
					panel.Controls.Remove(control);
					removedFromPanel = true;
				}
			}
		}

		public void UpdateToPanel(int count)
		{
			bool added = false;
			for (int i = 0; i < count; i++)
			{
				int index = 0;
				foreach (var control in controls)
				{
					if (visibles[control])
					{
						if (!panel.Controls.Contains(control))
						{
							panel.Controls.Add(control);
							panel.Controls.SetChildIndex(control, index);
							added = true;
							break;
						}
						index++;
					}
				}
			}

			if (added || removedFromPanel)
			{
				removedFromPanel = false;
				resizeControls();
			}
		}

		public void ScrollTo(Control control)
		{
			if (visibles.ContainsKey(control) && visibles[control])
			{
				panel.ScrollControlIntoView(control);
			}
		}

		private FlowLayoutPanel panel;
		private bool autoResize = false;

		private List<Control> controls = new List<Control>();
		private Dictionary<Control, bool> visibles = new Dictionary<Control, bool>();

		private bool removedFromPanel = false;

		private void resizeControls()
		{
			var dpiRatio = (float)panel.DeviceDpi / 96;
			if (panel.FlowDirection == FlowDirection.LeftToRight || panel.FlowDirection == FlowDirection.RightToLeft)
			{
				if (panel.Width < dpiRatio * 50) return;
				foreach (Control c in panel.Controls)
				{
					c.Width = (int)(panel.Width - dpiRatio * (8 + (panel.VerticalScroll.Visible ? 20 : 0)));
				}
			}
			else
			{
				if (panel.Height < dpiRatio * 50) return;
				foreach (Control c in panel.Controls)
				{
					c.Height = (int)(panel.Height - dpiRatio * (8 + (panel.HorizontalScroll.Visible ? 20 : 0)));
				}
			}
		}

		private void panel_SizeChanged (object sender, EventArgs e)
		{
			resizeControls();
		}
	}
}

