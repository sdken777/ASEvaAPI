using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASEva.UIWpf
{
    partial class PeekablePasswordBox : UserControl
    {
        public PeekablePasswordBox()
        {
            InitializeComponent();

            passwordBox.PasswordChanged += delegate
            {
                if (!useTextBox) PasswordChanged?.Invoke(null, EventArgs.Empty);
            };
            textBox.TextChanged += delegate
            {
                if (useTextBox) PasswordChanged?.Invoke(null, EventArgs.Empty);
            };
        }

        public char PasswordChar
        {
            get
            {
                if (useTextBox) return (char)0;
                else return passwordBox.PasswordChar;
            }
            set
            {
                if (value == (char)0)
                {
                    if (useTextBox) return;
                    textBox.Text = passwordBox.Password;
                    textBox.Visibility = Visibility.Visible;
                    passwordBox.Visibility = Visibility.Collapsed;
                    useTextBox = true;
                }
                else
                {
                    passwordBox.PasswordChar = value;
                    if (!useTextBox) return;
                    passwordBox.Password = textBox.Text;
                    passwordBox.Visibility = Visibility.Visible;
                    textBox.Visibility = Visibility.Collapsed;
                    useTextBox = false;
                }
            }
        }
        public int MaxLength
        {
            get
            {
                return passwordBox.MaxLength;
            }
            set
            {
                passwordBox.MaxLength = textBox.MaxLength = value;
            }
        }
        public String Password
        {
            get
            {
                if (useTextBox) return textBox.Text;
                else return passwordBox.Password;
            }
            set
            {
                if (useTextBox) textBox.Text = value;
                else passwordBox.Password = value;
            }
        }

        public event EventHandler? PasswordChanged;

        private bool useTextBox = false;
    }
}
