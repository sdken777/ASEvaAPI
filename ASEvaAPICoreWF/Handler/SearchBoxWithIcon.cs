using System;
using System.Windows.Forms;
using Eto.WinForms.Forms.Controls;

namespace ASEva.UICoreWF
{
    public partial class SearchBoxWithIcon : UserControl
    {
        public SearchBoxWithIcon()
        {
            InitializeComponent();
            pictureSearch.BackgroundImage = Properties.Resources.search;
            buttonDelete.BackgroundImage = Properties.Resources.delete;
        }

        public EtoTextBox TextBox
        {
            get { return textBox; }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            textBox.Clear();
        }
    }
}
