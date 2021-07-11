using System;
using System.Drawing;
using System.Windows.Forms;

namespace ASEva.CoreWF
{
    /// <summary>
    /// (api:corewf=1.0.0) 图像显示窗口
    /// </summary>
    public partial class ImageShow : Form
    {
        public ImageShow(Bitmap image, String title = "Image Viewer")
        {
            InitializeComponent();

            this.Text = title;

            var targetWidth = this.Width + image.Width - pictureBox1.Size.Width;
            var targetHeight = this.Height + image.Height - pictureBox1.Size.Height;
            this.Size = new Size(targetWidth, targetHeight);

            pictureBox1.Image = image;
        }
    }
}
