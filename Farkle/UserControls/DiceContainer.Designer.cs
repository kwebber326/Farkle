﻿namespace Farkle.UserControls
{
    partial class DiceContainer
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
            this.pnlChosenDice = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlChosenDice
            // 
            this.pnlChosenDice.AutoScroll = true;
            this.pnlChosenDice.Location = new System.Drawing.Point(3, 3);
            this.pnlChosenDice.Name = "pnlChosenDice";
            this.pnlChosenDice.Size = new System.Drawing.Size(626, 255);
            this.pnlChosenDice.TabIndex = 0;
            // 
            // DiceContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlChosenDice);
            this.Name = "DiceContainer";
            this.Size = new System.Drawing.Size(639, 269);
            this.Load += new System.EventHandler(this.DiceContainer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlChosenDice;
    }
}
