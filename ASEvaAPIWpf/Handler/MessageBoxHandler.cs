using System;
using Eto.Forms;
using sw = System.Windows;
//using WpfMessageBox = Xceed.Wpf.Toolkit.MessageBox;
using WpfMessageBox = System.Windows.MessageBox;
using Eto;
using Eto.Wpf;
using Eto.Wpf.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using swm = System.Windows.Media;

namespace ASEva.UIWpf
{
	#pragma warning disable SYSLIB0003
    class MessageBoxHandler : WidgetHandler<Widget>, MessageBox.IHandler
	{
		public string Text { get; set; }

		public string Caption { get; set; }

		public MessageBoxType Type { get; set; }

		public MessageBoxButtons Buttons { get; set; }

		public MessageBoxDefaultButton DefaultButton { get; set; }

		public DialogResult ShowDialog(Control parent)
		{
			using (var visualStyles = new EnableThemingInScope(ApplicationHandler.EnableVisualStyles))
			{
				var parentWindow = parent?.ParentWindow;
				if (parentWindow?.HasFocus == false)
					parentWindow.Focus();

				var element = parent == null ? null : parent.GetContainerControl();
				var window = element == null ? null : GetVisualParent<sw.Window>(element);
				sw.MessageBoxResult result;
				var buttons = Convert(Buttons);
				var defaultButton = Convert(DefaultButton, Buttons);
				var icon = Convert(Type);
				var caption = Caption ?? parentWindow?.Title;

				// CHECK: 修正caption为null则显示"错误"
				if (caption == null) caption = "";
				if (window != null) result = WpfMessageBox.Show(window, Text, caption, buttons, icon, defaultButton);
				else result = WpfMessageBox.Show(Text, caption, buttons, icon, defaultButton);
				WpfFrameworkElementHelper.ShouldCaptureMouse = false;
				return Convert(result);
			}
		}

		public static sw.MessageBoxResult Convert(MessageBoxDefaultButton defaultButton, MessageBoxButtons buttons)
		{
			switch (defaultButton)
			{
				case MessageBoxDefaultButton.OK:
					return sw.MessageBoxResult.OK;
				case MessageBoxDefaultButton.No:
					return sw.MessageBoxResult.No;
				case MessageBoxDefaultButton.Cancel:
					return sw.MessageBoxResult.Cancel;
				case MessageBoxDefaultButton.Default:
					switch (buttons)
					{
						case MessageBoxButtons.OK:
							return sw.MessageBoxResult.OK;
						case MessageBoxButtons.OKCancel:
							return sw.MessageBoxResult.Cancel;
						case MessageBoxButtons.YesNo:
							return sw.MessageBoxResult.No;
						case MessageBoxButtons.YesNoCancel:
							return sw.MessageBoxResult.Cancel;
						default:
							throw new NotSupportedException();
					}
				default:
					throw new NotSupportedException();
			}
		}


		static sw.MessageBoxImage Convert(MessageBoxType type)
		{
			switch (type)
			{
				case MessageBoxType.Information:
					return sw.MessageBoxImage.Information;
				case MessageBoxType.Error:
					return sw.MessageBoxImage.Error;
				case MessageBoxType.Question:
					return sw.MessageBoxImage.Question;
				case MessageBoxType.Warning:
					return sw.MessageBoxImage.Warning;
				default:
					throw new NotSupportedException();
			}
		}

		static DialogResult Convert(sw.MessageBoxResult result)
		{
			switch (result)
			{
				case sw.MessageBoxResult.Cancel: return DialogResult.Cancel;
				case sw.MessageBoxResult.No: return DialogResult.No;
				case sw.MessageBoxResult.None: return DialogResult.None;
				case sw.MessageBoxResult.Yes: return DialogResult.Yes;
				case sw.MessageBoxResult.OK: return DialogResult.Ok;
				default: throw new NotSupportedException();
			}
		}

		static sw.MessageBoxButton Convert(MessageBoxButtons value)
		{
			switch (value)
			{
				case MessageBoxButtons.YesNo:
					return sw.MessageBoxButton.YesNo;
				case MessageBoxButtons.YesNoCancel:
					return sw.MessageBoxButton.YesNoCancel;
				case MessageBoxButtons.OK:
					return sw.MessageBoxButton.OK;
				case MessageBoxButtons.OKCancel:
					return sw.MessageBoxButton.OKCancel;
				default:
					throw new NotSupportedException();
			}
		}

		static T GetVisualParent<T>(sw.DependencyObject control)
			where T : class
		{
			var ce = control as sw.ContentElement;
			while (ce != null)
			{
				control = sw.ContentOperations.GetParent(ce);
				ce = control as sw.ContentElement;
			}

			while (control is swm.Visual || control is swm.Media3D.Visual3D)
			{
				control = swm.VisualTreeHelper.GetParent(control);
				var tmp = control as T;
				if (tmp != null)
					return tmp;
			}
			return null;
		}
	}

	[SuppressUnmanagedCodeSecurity]
	class EnableThemingInScope : IDisposable
	{
		// Private data
		IntPtr cookie;
		static ACTCTX enableThemingActivationContext;
		static IntPtr hActCtx;
		static bool contextCreationSucceeded = false;

		public EnableThemingInScope(bool enable)
		{
			cookie = IntPtr.Zero;
			if (enable && System.Windows.Forms.OSFeature.Feature.IsPresent(System.Windows.Forms.OSFeature.Themes))
			{
				if (EnsureActivateContextCreated())
				{
					if (!ActivateActCtx(hActCtx, out cookie))
					{
						// Be sure cookie always zero if activation failed
						cookie = IntPtr.Zero;
					}
				}
			}
		}

		~EnableThemingInScope()
		{
			Dispose(false);
		}

		void IDisposable.Dispose()
		{
			Dispose(true);
		}

		void Dispose(bool disposing)
		{
			if (cookie != IntPtr.Zero)
			{
				if (DeactivateActCtx(0, cookie))
				{
					// deactivation succeeded...
					cookie = IntPtr.Zero;
				}
			}
		}

		bool EnsureActivateContextCreated()
		{
			lock (typeof(EnableThemingInScope))
			{
				if (!contextCreationSucceeded)
				{
					// Pull manifest from the .NET Framework install
					// directory

					string assemblyLoc = null;

					FileIOPermission fiop = new FileIOPermission(PermissionState.None);
					fiop.AllFiles = FileIOPermissionAccess.PathDiscovery;
					fiop.Assert();
					try
					{
						assemblyLoc = typeof(Object).Assembly.Location;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}

					string manifestLoc = null;
					string installDir = null;
					if (assemblyLoc != null)
					{
						installDir = Path.GetDirectoryName(assemblyLoc);
						const string manifestName = "XPThemes.manifest";
						manifestLoc = Path.Combine(installDir, manifestName);
					}

					if (manifestLoc != null && installDir != null)
					{
						enableThemingActivationContext = new ACTCTX();
						enableThemingActivationContext.cbSize = Marshal.SizeOf(typeof(ACTCTX));
						enableThemingActivationContext.lpSource = manifestLoc;

						// Set the lpAssemblyDirectory to the install
						// directory to prevent Win32 Side by Side from
						// looking for comctl32 in the application
						// directory, which could cause a bogus dll to be
						// placed there and open a security hole.
						enableThemingActivationContext.lpAssemblyDirectory = installDir;
						enableThemingActivationContext.dwFlags = ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID;

						// Note this will fail gracefully if file specified
						// by manifestLoc doesn't exist.
						hActCtx = CreateActCtx(ref enableThemingActivationContext);
						contextCreationSucceeded = (hActCtx != new IntPtr(-1));
					}
				}

				// If we return false, we'll try again on the next call into
				// EnsureActivateContextCreated(), which is fine.
				return contextCreationSucceeded;
			}
		}

		// All the pinvoke goo...
		[DllImport("Kernel32.dll")]
		extern static IntPtr CreateActCtx(ref ACTCTX actctx);
		[DllImport("Kernel32.dll")]
		extern static bool ActivateActCtx(IntPtr hActCtx, out IntPtr lpCookie);
		[DllImport("Kernel32.dll")]
		extern static bool DeactivateActCtx(uint dwFlags, IntPtr lpCookie);

		const int ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 0x004;

		struct ACTCTX
		{
			public int cbSize;
			public uint dwFlags;
			public string lpSource;
			public ushort wProcessorArchitecture;
			public ushort wLangId;
			public string lpAssemblyDirectory;
			public string lpResourceName;
			public string lpApplicationName;
		}
	}
}
