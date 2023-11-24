namespace Farkle.UserControls
{
    partial class AnimationUserControl
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
            this.lblPlayerAction = new System.Windows.Forms.Label();
            this.pnlAnimation = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblPlayerAction
            // 
            this.lblPlayerAction.AutoSize = true;
            this.lblPlayerAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerAction.Location = new System.Drawing.Point(23, 22);
            this.lblPlayerAction.Name = "lblPlayerAction";
            this.lblPlayerAction.Size = new System.Drawing.Size(206, 37);
            this.lblPlayerAction.TabIndex = 0;
            this.lblPlayerAction.Text = "Sample Text";
            // 
            // pnlAnimation
            // 
            this.pnlAnimation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAnimation.Location = new System.Drawing.Point(29, 74);
            this.pnlAnimation.Name = "pnlAnimation";
            this.pnlAnimation.Size = new System.Drawing.Size(713, 504);
            this.pnlAnimation.TabIndex = 1;
            // 
            // AnimationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.pnlAnimation);
            this.Controls.Add(this.lblPlayerAction);
            this.Name = "AnimationUserControl";
            this.Size = new System.Drawing.Size(775, 602);
            this.Load += new System.EventHandler(this.AnimationUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPlayerAction;
        private System.Windows.Forms.Panel pnlAnimation;
    }
}
