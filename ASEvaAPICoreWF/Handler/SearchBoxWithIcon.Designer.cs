
namespace ASEva.UICoreWF
{
    partial class SearchBoxWithIcon
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureSearch = new System.Windows.Forms.PictureBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.textBox = new Eto.WinForms.Forms.Controls.EtoTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureSearch
            // 
            this.pictureSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureSearch.Location = new System.Drawing.Point(0, 2);
            this.pictureSearch.Name = "pictureSearch";
            this.pictureSearch.Size = new System.Drawing.Size(20, 20);
            this.pictureSearch.TabIndex = 0;
            this.pictureSearch.TabStop = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDelete.Location = new System.Drawing.Point(180, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(20, 20);
            this.buttonDelete.TabIndex = 1;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            //
            // textBox
            //
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(22, 1);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(156, 23);
            this.textBox.TabIndex = 2;
            // 
            // SearchBoxWithIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.pictureSearch);
            this.Controls.Add(this.textBox);
            this.Name = "SearchBoxWithIcon";
            this.Size = new System.Drawing.Size(200, 24);
            this.MinimumSize = new System.Drawing.Size(120, 24);
            ((System.ComponentModel.ISupportInitialize)(this.pictureSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureSearch;
        private System.Windows.Forms.Button buttonDelete;
        private Eto.WinForms.Forms.Controls.EtoTextBox textBox;
    }
}
