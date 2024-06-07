namespace ASEva.UICoreWF
{
    partial class OpenGLOnscreenControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OpenGLOnscreenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "OpenGLOnscreenControl";
            this.SizeChanged += new System.EventHandler(this.pictureBox_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OpenGLOnscreenControl_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OpenGLOnscreenControl_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OpenGLOnscreenControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OpenGLOnscreenControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpenGLOnscreenControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
