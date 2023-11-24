namespace Farkle.UserControls
{
    partial class FarkleSettingsControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPlayers = new System.Windows.Forms.ComboBox();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.playerNameControl1 = new Farkle.UserControls.PlayerNameControl();
            this.playerNameControl2 = new Farkle.UserControls.PlayerNameControl();
            this.playerNameControl3 = new Farkle.UserControls.PlayerNameControl();
            this.playerNameControl4 = new Farkle.UserControls.PlayerNameControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "# of Players:";
            // 
            // cmbPlayers
            // 
            this.cmbPlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlayers.FormattingEnabled = true;
            this.cmbPlayers.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbPlayers.Location = new System.Drawing.Point(168, 5);
            this.cmbPlayers.Name = "cmbPlayers";
            this.cmbPlayers.Size = new System.Drawing.Size(121, 28);
            this.cmbPlayers.TabIndex = 1;
            this.cmbPlayers.SelectedIndexChanged += new System.EventHandler(this.CmbPlayers_SelectedIndexChanged);
            // 
            // pnlSettings
            // 
            this.pnlSettings.AutoScroll = true;
            this.pnlSettings.Location = new System.Drawing.Point(9, 214);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(496, 647);
            this.pnlSettings.TabIndex = 2;
            // 
            // playerNameControl1
            // 
            this.playerNameControl1.Location = new System.Drawing.Point(9, 45);
            this.playerNameControl1.Name = "playerNameControl1";
            this.playerNameControl1.Number = 1;
            this.playerNameControl1.Size = new System.Drawing.Size(496, 38);
            this.playerNameControl1.TabIndex = 3;
            this.playerNameControl1.TitleText = "Player #1 Name:";
            // 
            // playerNameControl2
            // 
            this.playerNameControl2.Location = new System.Drawing.Point(9, 76);
            this.playerNameControl2.Name = "playerNameControl2";
            this.playerNameControl2.Number = 2;
            this.playerNameControl2.Size = new System.Drawing.Size(496, 38);
            this.playerNameControl2.TabIndex = 4;
            this.playerNameControl2.TitleText = "Player #2 Name:";
            // 
            // playerNameControl3
            // 
            this.playerNameControl3.Location = new System.Drawing.Point(9, 108);
            this.playerNameControl3.Name = "playerNameControl3";
            this.playerNameControl3.Number = 3;
            this.playerNameControl3.Size = new System.Drawing.Size(496, 38);
            this.playerNameControl3.TabIndex = 5;
            this.playerNameControl3.TitleText = "Player #3 Name:";
            this.playerNameControl3.Visible = false;
            // 
            // playerNameControl4
            // 
            this.playerNameControl4.Location = new System.Drawing.Point(9, 139);
            this.playerNameControl4.Name = "playerNameControl4";
            this.playerNameControl4.Number = 4;
            this.playerNameControl4.Size = new System.Drawing.Size(496, 38);
            this.playerNameControl4.TabIndex = 6;
            this.playerNameControl4.TitleText = "Player #4 Name:";
            this.playerNameControl4.Visible = false;
            // 
            // FarkleSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.playerNameControl4);
            this.Controls.Add(this.playerNameControl3);
            this.Controls.Add(this.playerNameControl2);
            this.Controls.Add(this.playerNameControl1);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.cmbPlayers);
            this.Controls.Add(this.label1);
            this.Name = "FarkleSettingsControl";
            this.Size = new System.Drawing.Size(612, 864);
            this.Load += new System.EventHandler(this.FarkleSettingsControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPlayers;
        private System.Windows.Forms.Panel pnlSettings;
        private PlayerNameControl playerNameControl1;
        private PlayerNameControl playerNameControl2;
        private PlayerNameControl playerNameControl3;
        private PlayerNameControl playerNameControl4;
    }
}
