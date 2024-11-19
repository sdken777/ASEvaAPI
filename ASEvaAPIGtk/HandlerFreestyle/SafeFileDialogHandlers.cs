using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Eto;
using Eto.Forms;
using Eto.GtkSharp.Forms;

namespace ASEva.UIGtk
{
	class SafeSaveFileDialogHandler : Eto.GtkSharp.Forms.SaveFileDialogHandler
	{
		// CHECK: 修正输入文件名无后缀时未补上的问题
        public override string? FileName
		{
			get => filename;
			set => base.FileName = value;
		}
		private String genFileName()
		{
			var rawPath = Control.Filename;
			if (String.IsNullOrEmpty(rawPath)) return rawPath;

			Gtk.FileFilter? controlFilter = null;
			if (Control.Filter != null) controlFilter = Control.Filter;
			else if (Control.Filters != null && Control.Filters.Length > 0 && Control.Filters[0] != null) controlFilter = Control.Filters[0];
			else return rawPath;

			var dir = Path.GetDirectoryName(rawPath);
			if (dir == null) return rawPath;

			if (dir.EndsWith(Path.DirectorySeparatorChar)) dir = dir.Substring(0, dir.Length - 1);
			var fullName = Path.GetFileName(rawPath);
			var name = Path.GetFileNameWithoutExtension(rawPath);
			var extension = Path.GetExtension(rawPath).ToLower();
			foreach (var filter in Widget.Filters)
			{
				if (controlFilter.Name != filter.Name) continue;
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

		// CHECK: 修正对话框打开关闭后无法退出应用程序问题
        public override DialogResult ShowDialog(Eto.Forms.Window parent)
		{
			var result = base.ShowDialog(parent);
			filename = genFileName();
			Control.Dispose();
			return result;
		}

		private String? filename = null;
	}

	class SafeOpenFileDialogHandler : OpenFileDialogHandler, OpenFileDialog.IHandler
	{
		// CHECK: 修正对话框打开关闭后无法退出应用程序问题
        public override DialogResult ShowDialog(Eto.Forms.Window parent)
		{
			var result = base.ShowDialog(parent);
			filename = Control.Filename;
			filenames = Control.Filenames;
			Control.Dispose();
			return result;
		}
        public override string? FileName
		{
			get => filename;
			set => base.FileName = value;
		}
		private String? filename = null;

		public new IEnumerable<string> Filenames
		{
			get => filenames;
		}
		private String[] filenames = [];
	}

	class SafeSelectFolderDialogHandler : SelectFolderDialogHandler, SelectFolderDialog.IHandler, CommonDialog.IHandler
	{
		// CHECK: 修正对话框打开关闭后无法退出应用程序问题
        public new DialogResult ShowDialog(Eto.Forms.Window parent)
		{
			var result = base.ShowDialog(parent);
			currentFolder = Control.Filename ?? Control.CurrentFolder;
			Control.Dispose();
			return result;
		}
		public new string? Directory
		{
			get => currentFolder;
			set => base.Directory = value;
		}
		private String? currentFolder = null;
	}
}