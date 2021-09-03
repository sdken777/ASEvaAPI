namespace ASEva.UICoreWF
{
    partial class NameEditor
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.linkLabelName = new System.Windows.Forms.LinkLabel();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // linkLabelName
            // 
            this.linkLabelName.ActiveLinkColor = System.Drawing.Color.Black;
            this.linkLabelName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelName.Location = new System.Drawing.Point(0, 0);
            this.linkLabelName.Name = "linkLabelName";
            this.linkLabelName.Size = new System.Drawing.Size(500, 23);
            this.linkLabelName.TabIndex = 11;
            this.linkLabelName.TabStop = true;
            this.linkLabelName.Text = "(Name)";
            this.linkLabelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelName_LinkClicked);
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(0, 0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(500, 23);
            this.textBoxName.TabIndex = 12;
            this.textBoxName.Text = "(name)";
            this.textBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxName.Visible = false;
            this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
            this.textBoxName.Leave += new System.EventHandler(this.textBoxName_Leave);
            // 
            // NameEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.linkLabelName);
            this.Controls.Add(this.textBoxName);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(1000, 23);
            this.MinimumSize = new System.Drawing.Size(100, 23);
            this.Name = "NameEditor";
            this.Size = new System.Drawing.Size(500, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelName;
        private System.Windows.Forms.TextBox textBoxName;
    }
}
