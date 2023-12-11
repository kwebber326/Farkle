namespace Farkle
{
    partial class FarkleGame
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gameLogUserControl1 = new Farkle.UserControls.GameLogUserControl();
            this.diceTray1 = new Farkle.UserControls.DiceTray();
            this.scoringDisplay1 = new Farkle.UserControls.ScoringDisplay();
            this.saveFileDialogGameLog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Farkle.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(1669, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // gameLogUserControl1
            // 
            this.gameLogUserControl1.Location = new System.Drawing.Point(1063, 676);
            this.gameLogUserControl1.Name = "gameLogUserControl1";
            this.gameLogUserControl1.Size = new System.Drawing.Size(882, 538);
            this.gameLogUserControl1.TabIndex = 3;
            // 
            // diceTray1
            // 
            this.diceTray1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.diceTray1.CurrentPlayer = null;
            this.diceTray1.Location = new System.Drawing.Point(12, 676);
            this.diceTray1.Name = "diceTray1";
            this.diceTray1.RuleSet = null;
            this.diceTray1.Size = new System.Drawing.Size(1045, 820);
            this.diceTray1.TabIndex = 2;
            this.diceTray1.TopScore = 0;
            this.diceTray1.TurnScore = 0;
            // 
            // scoringDisplay1
            // 
            this.scoringDisplay1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.scoringDisplay1.FarkleSetting = null;
            this.scoringDisplay1.Location = new System.Drawing.Point(12, 12);
            this.scoringDisplay1.Name = "scoringDisplay1";
            this.scoringDisplay1.Size = new System.Drawing.Size(1201, 416);
            this.scoringDisplay1.TabIndex = 0;
            // 
            // saveFileDialogGameLog
            // 
            this.saveFileDialogGameLog.Filter = "Text Files|*.txt";
            // 
            // FarkleGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1805, 1474);
            this.Controls.Add(this.gameLogUserControl1);
            this.Controls.Add(this.diceTray1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.scoringDisplay1);
            this.Name = "FarkleGame";
            this.Text = "Farkle";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FarkleGame_FormClosing);
            this.Load += new System.EventHandler(this.FarkleGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.ScoringDisplay scoringDisplay1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private UserControls.DiceTray diceTray1;
        private UserControls.GameLogUserControl gameLogUserControl1;
        private System.Windows.Forms.SaveFileDialog saveFileDialogGameLog;
    }
}