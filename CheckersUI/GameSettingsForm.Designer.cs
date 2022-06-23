namespace CheckersUI
{
    public partial class GameSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonBoardSize6 = new System.Windows.Forms.RadioButton();
            this.radioButtonBoardSize8 = new System.Windows.Forms.RadioButton();
            this.radioButtonBoardSize10 = new System.Windows.Forms.RadioButton();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxSecondPlayer = new System.Windows.Forms.CheckBox();
            this.textBoxFirstPlayerName = new System.Windows.Forms.TextBox();
            this.textBoxSecondPlayerName = new System.Windows.Forms.TextBox();
            this.buttonDoneSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size:";
            // 
            // radioButtonBoardSize6
            // 
            this.radioButtonBoardSize6.AutoSize = true;
            this.radioButtonBoardSize6.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonBoardSize6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBoardSize6.Location = new System.Drawing.Point(34, 67);
            this.radioButtonBoardSize6.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonBoardSize6.Name = "radioButtonBoardSize6";
            this.radioButtonBoardSize6.Size = new System.Drawing.Size(63, 23);
            this.radioButtonBoardSize6.TabIndex = 1;
            this.radioButtonBoardSize6.TabStop = true;
            this.radioButtonBoardSize6.Text = "6 x 6";
            this.radioButtonBoardSize6.UseVisualStyleBackColor = false;
            this.radioButtonBoardSize6.CheckedChanged += new System.EventHandler(this.setBoardSize);
            // 
            // radioButtonBoardSize8
            // 
            this.radioButtonBoardSize8.AutoSize = true;
            this.radioButtonBoardSize8.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonBoardSize8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBoardSize8.Location = new System.Drawing.Point(129, 68);
            this.radioButtonBoardSize8.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonBoardSize8.Name = "radioButtonBoardSize8";
            this.radioButtonBoardSize8.Size = new System.Drawing.Size(63, 23);
            this.radioButtonBoardSize8.TabIndex = 2;
            this.radioButtonBoardSize8.TabStop = true;
            this.radioButtonBoardSize8.Text = "8 x 8";
            this.radioButtonBoardSize8.UseVisualStyleBackColor = false;
            this.radioButtonBoardSize8.CheckedChanged += new System.EventHandler(this.setBoardSize);
            // 
            // radioButtonBoardSize10
            // 
            this.radioButtonBoardSize10.AutoSize = true;
            this.radioButtonBoardSize10.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonBoardSize10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBoardSize10.Location = new System.Drawing.Point(232, 68);
            this.radioButtonBoardSize10.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonBoardSize10.Name = "radioButtonBoardSize10";
            this.radioButtonBoardSize10.Size = new System.Drawing.Size(81, 23);
            this.radioButtonBoardSize10.TabIndex = 3;
            this.radioButtonBoardSize10.TabStop = true;
            this.radioButtonBoardSize10.Text = "10 x 10";
            this.radioButtonBoardSize10.UseVisualStyleBackColor = false;
            this.radioButtonBoardSize10.CheckedChanged += new System.EventHandler(this.setBoardSize);
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayers.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblPlayers.Location = new System.Drawing.Point(31, 122);
            this.lblPlayers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(75, 19);
            this.lblPlayers.TabIndex = 4;
            this.lblPlayers.Text = "Players:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(50, 154);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Player 1 name:";
            // 
            // checkBoxSecondPlayer
            // 
            this.checkBoxSecondPlayer.AutoSize = true;
            this.checkBoxSecondPlayer.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxSecondPlayer.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxSecondPlayer.Location = new System.Drawing.Point(35, 190);
            this.checkBoxSecondPlayer.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxSecondPlayer.Name = "checkBoxSecondPlayer";
            this.checkBoxSecondPlayer.Size = new System.Drawing.Size(135, 23);
            this.checkBoxSecondPlayer.TabIndex = 6;
            this.checkBoxSecondPlayer.Text = "Player 2 name:";
            this.checkBoxSecondPlayer.UseVisualStyleBackColor = false;
            this.checkBoxSecondPlayer.CheckedChanged += new System.EventHandler(this.checkBoxSecondPlayer_CheckedChanged);
            // 
            // textBoxFirstPlayerName
            // 
            this.textBoxFirstPlayerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFirstPlayerName.Location = new System.Drawing.Point(170, 155);
            this.textBoxFirstPlayerName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxFirstPlayerName.Name = "textBoxFirstPlayerName";
            this.textBoxFirstPlayerName.Size = new System.Drawing.Size(141, 22);
            this.textBoxFirstPlayerName.TabIndex = 7;
            this.textBoxFirstPlayerName.TextChanged += new System.EventHandler(this.textBoxPlayerName_TextChanged);
            // 
            // textBoxSecondPlayerName
            // 
            this.textBoxSecondPlayerName.Enabled = false;
            this.textBoxSecondPlayerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSecondPlayerName.Location = new System.Drawing.Point(170, 192);
            this.textBoxSecondPlayerName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSecondPlayerName.Name = "textBoxSecondPlayerName";
            this.textBoxSecondPlayerName.Size = new System.Drawing.Size(141, 22);
            this.textBoxSecondPlayerName.TabIndex = 8;
            this.textBoxSecondPlayerName.Text = "[Computer]";
            this.textBoxSecondPlayerName.TextChanged += new System.EventHandler(this.textBoxPlayerName_TextChanged);
            // 
            // buttonDoneSettings
            // 
            this.buttonDoneSettings.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.buttonDoneSettings.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDoneSettings.Location = new System.Drawing.Point(270, 224);
            this.buttonDoneSettings.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDoneSettings.Name = "buttonDoneSettings";
            this.buttonDoneSettings.Size = new System.Drawing.Size(118, 45);
            this.buttonDoneSettings.TabIndex = 9;
            this.buttonDoneSettings.Text = "Start Game";
            this.buttonDoneSettings.UseVisualStyleBackColor = false;
            this.buttonDoneSettings.Click += new System.EventHandler(this.buttonDoneSettings_Click);
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CheckersUI.Properties.Resources.Form_Background;
            this.ClientSize = new System.Drawing.Size(399, 280);
            this.Controls.Add(this.buttonDoneSettings);
            this.Controls.Add(this.textBoxSecondPlayerName);
            this.Controls.Add(this.textBoxFirstPlayerName);
            this.Controls.Add(this.checkBoxSecondPlayer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.radioButtonBoardSize10);
            this.Controls.Add(this.radioButtonBoardSize8);
            this.Controls.Add(this.radioButtonBoardSize6);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "GameSettingsForm";
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.GameSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonBoardSize6;
        private System.Windows.Forms.RadioButton radioButtonBoardSize8;
        private System.Windows.Forms.RadioButton radioButtonBoardSize10;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxSecondPlayer;
        private System.Windows.Forms.TextBox textBoxFirstPlayerName;
        private System.Windows.Forms.TextBox textBoxSecondPlayerName;
        private System.Windows.Forms.Button buttonDoneSettings;
    }
}