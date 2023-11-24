namespace Farkle.UserControls
{
    partial class DiceTray
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
            this.pnlLiveDice = new System.Windows.Forms.Panel();
            this.pnlSetAsideDice = new System.Windows.Forms.Panel();
            this.btnSetAside = new System.Windows.Forms.Button();
            this.btnRoll = new System.Windows.Forms.Button();
            this.btnBank = new System.Windows.Forms.Button();
            this.lblTurnScore = new System.Windows.Forms.Label();
            this.diceContainer1 = new Farkle.UserControls.DiceContainer();
            this.animationUserControl1 = new Farkle.UserControls.AnimationUserControl();
            this.SuspendLayout();
            // 
            // pnlLiveDice
            // 
            this.pnlLiveDice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLiveDice.Location = new System.Drawing.Point(3, 155);
            this.pnlLiveDice.Name = "pnlLiveDice";
            this.pnlLiveDice.Size = new System.Drawing.Size(490, 123);
            this.pnlLiveDice.TabIndex = 0;
            // 
            // pnlSetAsideDice
            // 
            this.pnlSetAsideDice.BackColor = System.Drawing.Color.Maroon;
            this.pnlSetAsideDice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSetAsideDice.Location = new System.Drawing.Point(3, 3);
            this.pnlSetAsideDice.Name = "pnlSetAsideDice";
            this.pnlSetAsideDice.Size = new System.Drawing.Size(490, 133);
            this.pnlSetAsideDice.TabIndex = 1;
            // 
            // btnSetAside
            // 
            this.btnSetAside.Enabled = false;
            this.btnSetAside.Location = new System.Drawing.Point(3, 284);
            this.btnSetAside.Name = "btnSetAside";
            this.btnSetAside.Size = new System.Drawing.Size(101, 37);
            this.btnSetAside.TabIndex = 3;
            this.btnSetAside.Text = "Set Aside";
            this.btnSetAside.UseVisualStyleBackColor = true;
            this.btnSetAside.Click += new System.EventHandler(this.BtnSetAside_Click);
            // 
            // btnRoll
            // 
            this.btnRoll.Location = new System.Drawing.Point(110, 284);
            this.btnRoll.Name = "btnRoll";
            this.btnRoll.Size = new System.Drawing.Size(101, 37);
            this.btnRoll.TabIndex = 4;
            this.btnRoll.Text = "Roll";
            this.btnRoll.UseVisualStyleBackColor = true;
            this.btnRoll.Click += new System.EventHandler(this.BtnRoll_Click);
            // 
            // btnBank
            // 
            this.btnBank.Location = new System.Drawing.Point(217, 284);
            this.btnBank.Name = "btnBank";
            this.btnBank.Size = new System.Drawing.Size(101, 37);
            this.btnBank.TabIndex = 5;
            this.btnBank.Text = "Bank";
            this.btnBank.UseVisualStyleBackColor = true;
            this.btnBank.Click += new System.EventHandler(this.BtnBank_Click);
            // 
            // lblTurnScore
            // 
            this.lblTurnScore.AutoSize = true;
            this.lblTurnScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurnScore.Location = new System.Drawing.Point(523, 284);
            this.lblTurnScore.Name = "lblTurnScore";
            this.lblTurnScore.Size = new System.Drawing.Size(133, 25);
            this.lblTurnScore.TabIndex = 6;
            this.lblTurnScore.Text = "Turn Score: ";
            // 
            // diceContainer1
            // 
            this.diceContainer1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.diceContainer1.Location = new System.Drawing.Point(499, 3);
            this.diceContainer1.Name = "diceContainer1";
            this.diceContainer1.Size = new System.Drawing.Size(656, 275);
            this.diceContainer1.TabIndex = 7;
            // 
            // animationUserControl1
            // 
            this.animationUserControl1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.animationUserControl1.Location = new System.Drawing.Point(380, 321);
            this.animationUserControl1.Name = "animationUserControl1";
            this.animationUserControl1.Size = new System.Drawing.Size(775, 602);
            this.animationUserControl1.TabIndex = 8;
            // 
            // DiceTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.animationUserControl1);
            this.Controls.Add(this.diceContainer1);
            this.Controls.Add(this.lblTurnScore);
            this.Controls.Add(this.btnBank);
            this.Controls.Add(this.btnRoll);
            this.Controls.Add(this.btnSetAside);
            this.Controls.Add(this.pnlSetAsideDice);
            this.Controls.Add(this.pnlLiveDice);
            this.Name = "DiceTray";
            this.Size = new System.Drawing.Size(1172, 956);
            this.Load += new System.EventHandler(this.DiceTray_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlLiveDice;
        private System.Windows.Forms.Panel pnlSetAsideDice;
        private System.Windows.Forms.Button btnSetAside;
        private System.Windows.Forms.Button btnRoll;
        private System.Windows.Forms.Button btnBank;
        private System.Windows.Forms.Label lblTurnScore;
        private DiceContainer diceContainer1;
        private AnimationUserControl animationUserControl1;
    }
}
