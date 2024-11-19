using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Eto;
using Eto.Forms;
using Eto.GtkSharp;

namespace ASEva.UIGtk
{
#if GTKCORE
	class SaveFileDialogHandler : WidgetHandler<Gtk.FileChooserNative, SaveFileDialog>, SaveFileDialog.IHandler
#else
	class SaveFileDialogHandler : WidgetHandler<Gtk.FileChooserDialog, SaveFileDialog>, SaveFileDialog.IHandler
#endif
	{
		public SaveFileDialogHandler()
		{
#if GTKCORE
			Control = new Gtk.FileChooserNative(string.Empty, null, Gtk.FileChooserAction.Save, null, null);
#else
			Control = new Gtk.FileChooserDialog(string.Empty, null, Gtk.FileChooserAction.Save);
			Control.AddButton(Gtk.Stock.Cancel, Gtk.ResponseType.Cancel);
			Control.AddButton(Gtk.Stock.Save, Gtk.ResponseType.Ok);
			Control.DefaultResponse = Gtk.ResponseType.Ok;
#endif
			Control.DoOverwriteConfirmation = true;
			Control.SetCurrentFolder(System.IO.Directory.GetCurrentDirectory());
		}

		// 修正输入文件名无后缀时未补上的问题（通过SafeSaveFileDialogHandler解决）
		public string FileName
		{
			get
			{
				var rawPath = Control.Filename;
				if (String.IsNullOrEmpty(rawPath)) return rawPath;

				var dir = Path.GetDirectoryName(rawPath);
				if (dir?.EndsWith(Path.DirectorySeparatorChar) ?? false) dir = dir.Substring(0, dir.Length - 1);
				var fullName = Path.GetFileName(rawPath);
				var name = Path.GetFileNameWithoutExtension(rawPath);
				var extension = Path.GetExtension(rawPath).ToLower();
				foreach (var filter in Widget.Filters)
				{
					if (Control.Filter.Name != filter.Name) continue;
					if (filter.Extensions == null || filter.Extensions.Length == 0) continue;

					bool found = false;
					foreach (var ext in filter.Extensions)
					{
						if (ext.ToLower() == extension)
						{
							found = true;
							break;
						}
					}

					if (!found)
					{
						name = fullName;
						extension = filter.Extensions[0];
					}
				}
				return dir + Path.DirectorySeparatorChar + name + extension;
			}
			set
			{
				Control.SetCurrentFolder(Path.GetDirectoryName(value));
				Control.SetFilename(value);
				Control.CurrentName = Path.GetFileName(value);
			}
		}
		
		public Uri Directory {
			get {
				return new Uri(Control.CurrentFolderUri);
			}
			set {
				Control.SetCurrentFolderUri(value.AbsoluteUri);
			}
		}
		
		
		public void SetFilters()
		{
			var list = Control.Filters.ToArray();
			foreach (Gtk.FileFilter filter in list)
			{
				Control.RemoveFilter(filter);
			}
			
			foreach (var val in Widget.Filters)
			{
				var filter = new Gtk.FileFilter();
				filter.Name = val.Name;
				foreach (string pattern in val.Extensions) filter.AddPattern("*" + pattern);
				Control.AddFilter(filter);
			}
		}

		public int CurrentFilterIndex
		{
			get
			{
				Gtk.FileFilter[] filters = Control.Filters;
				for (int i=0; i<filters.Length; i++)
				{
					if (filters[i] == Control.Filter) return i;
				}
				return -1;
			}
			set
			{
				Gtk.FileFilter[] filters = Control.Filters;
				Control.Filter = filters[value];
			}
		}

		public bool CheckFileExists
		{
			get { return false; }
			set {  }
		}

		public string Title
		{
			get { return Control.Title; }
			set { Control.Title = value; }
		}


		public DialogResult ShowDialog(Window parent)
		{
			SetFilters();
#if !GTKCORE
			if (parent != null) Control.TransientFor = (Gtk.Window)parent.ControlObject;
#endif

			int result = Control.Run();

			Control.Hide();
#if !GTKCORE
			Control.Unrealize();
#endif

			DialogResult response = ((Gtk.ResponseType)result).ToEto ();
			if (response == DialogResult.Ok && !string.IsNullOrEmpty(Control.CurrentFolder))
				System.IO.Directory.SetCurrentDirectory(Control.CurrentFolder);
			
			return response;
		}

		public void InsertFilter(int index, FileFilter filter)
		{
			var gtkFilter = new Gtk.FileFilter();
			gtkFilter.Name = filter.Name;
			foreach (var extension in filter.Extensions)
				gtkFilter.AddPattern(extension);

			var filters = new List<Gtk.FileFilter>(Control.Filters);
			if (index < filters.Count)
			{
				for (int i = 0; i < filters.Count; i++)
					Control.RemoveFilter(filters[i]);
				filters.Insert(index, gtkFilter);
				for (int i = 0; i < filters.Count; i++)
					Control.AddFilter(filters[i]);
			}
			else
				Control.AddFilter(gtkFilter);
		}

		public void RemoveFilter(int index)
		{
			Control.RemoveFilter(Control.Filters[index]);
		}

		public void ClearFilters()
		{
			for (int i = 0; i < Control.Filters.Length; i++)
				Control.RemoveFilter(Control.Filters[0]);
		}
	}
}