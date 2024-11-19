using System;
using System.Linq;
using Eto.Forms;
using Eto.Mac.Forms.Controls;
using Eto.Drawing;
using Eto.Mac.Forms;
using MonoMac.AppKit;
using MonoMac.Foundation;
using Eto.Mac;

namespace ASEva.UIMonoMac
{
	// CHECK: 重新实现，支持显示密码功能
	class PasswordBoxHandler : MacText<NSTextField, PasswordBox, PasswordBox.ICallback>, PasswordBox.IHandler, ITextBoxWithMaxLength
	{
		public override bool HasFocus
		{
			get
			{
				if (Widget.ParentWindow == null)
					return false;
				return ((IMacWindow)Widget.ParentWindow.Handler).FieldEditorClient == Control;
			}
		}

		protected override SizeF GetNaturalSize(SizeF availableSize)
		{
			var size = base.GetNaturalSize(availableSize);
			size.Width = Math.Max(100, size.Height);
			return size;
		}

		protected override NSTextField CreateControl()
		{
			var textField = new EtoTextField();
			textField.Changed += textField_Changed;
			return textField;
		}

        private void textField_Changed(object? sender, EventArgs e)
        {
			if (showPassword)
			{
				password = base.Text;
				return;
			}

			if (base.Text.Length < password.Length)
			{
				password = "";
				updateBaseText();
			}

            var targetChars = base.Text.ToCharArray();
			var oldChars = password.ToCharArray().ToList();
			for (int i = 0; i < targetChars.Length; i++)
			{
				if (targetChars[i] != '●')
				{
					if (i >= oldChars.Count) oldChars.Add(targetChars[i]);
					else oldChars.Insert(i, targetChars[i]);
					password = new String(oldChars.ToArray());
					updateBaseText();
					return;
				}
			}
        }

        protected override void Initialize()
		{
			MaxLength = -1;

			base.Initialize();
		}

		public override void AttachEvent(string id)
		{
			switch (id)
			{
				case TextControl.TextChangedEvent:
					Control.Changed += HandleChanged;
					break;
				default:
					base.AttachEvent(id);
					break;
			}
		}

		static void HandleChanged(object? sender, EventArgs e)
		{
			var handler = GetHandler(sender) as PasswordBoxHandler;
			if (handler != null)
				handler.Callback.OnTextChanged(handler.Widget, EventArgs.Empty);
		}

		public bool ReadOnly
		{
			get { return !Control.Editable; }
			set { Control.Editable = !value; }
		}

		public override string Text
		{
			get { return password; }
			set
			{
				if (value.Contains('●')) throw new Exception("Invalid characters in Text.");
				password = value == null ? "" : value;
				updateBaseText();
			}
		}

		public int MaxLength
		{
			get;
			set;
		}

		public Char PasswordChar
		{
			get { return showPassword ? (char)0 : '●'; }
			set
			{
				showPassword = value == (char)0;
				updateBaseText();
			}
		}

		private void updateBaseText()
		{
			var textField = Control as EtoTextField;
			if (textField == null) return;
			
			textField.Changed -= textField_Changed;
			if (showPassword) base.Text = password;
			else
			{
				var secureText = "";
				for (int i = 0; i < password.Length; i++) secureText += '●';
				base.Text = secureText;
			}
			textField.Changed += textField_Changed;
		}

		private String password = "";
		private bool showPassword = false;
	}
}
