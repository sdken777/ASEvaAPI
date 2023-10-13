using System;
using System.Windows.Forms;

namespace ASEva.UICoreWF
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:corewf=2.0.0) 名称编辑控件
    /// </summary>
    public partial class NameEditor : UserControl
    {
        public NameEditor()
        {
            InitializeComponent();
        }

        public String TargetName
        {
            get { return linkLabelName.Text; }
            set { linkLabelName.Text = value; }
        }

        public event EventHandler NameChanged;

        private void updateName()
        {
            if (textBoxName.Text.Length > 0 && linkLabelName.Text != textBoxName.Text)
            {
                linkLabelName.Text = textBoxName.Text;
                if (NameChanged != null) NameChanged(this, null);
            }
            textBoxName.Visible = false;
            linkLabelName.Visible = true;
        }

        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                updateName();
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            updateName();
        }

        private void linkLabelName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBoxName.Text = linkLabelName.Text;
            linkLabelName.Visible = false;
            textBoxName.Visible = true;
            textBoxName.Focus();
        }
    }
}
