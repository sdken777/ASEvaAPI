namespace ASEva.UICoreWF
{
    partial class ChannelSelector
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChannelSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChannelSelector";
            this.Size = new System.Drawing.Size(264, 22);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChannelSelector_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChannelSelector_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChannelSelector_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
