namespace CheckersUI
{
    public partial class CheckersBoardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckersBoardForm));
            this.labelInstruction = new System.Windows.Forms.Label();
            this.labelCurrentTurnName = new System.Windows.Forms.Label();
            this.labelCurrTurn = new System.Windows.Forms.Label();
            this.labelSecondPlayerPoints = new System.Windows.Forms.Label();
            this.labelSecondPlayerName = new System.Windows.Forms.Label();
            this.labelFirstPlayerPoints = new System.Windows.Forms.Label();
            this.lableFirstPlayer = new System.Windows.Forms.Label();
            this.labelPlayerScores = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelInstruction
            // 
            this.labelInstruction.AutoSize = true;
            this.labelInstruction.BackColor = System.Drawing.Color.Transparent;
            this.labelInstruction.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelInstruction.Location = new System.Drawing.Point(396, 81);
            this.labelInstruction.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Size = new System.Drawing.Size(227, 16);
            this.labelInstruction.TabIndex = 13;
            this.labelInstruction.Text = "Please select starting and end position";
            // 
            // labelCurrentTurnName
            // 
            this.labelCurrentTurnName.AutoSize = true;
            this.labelCurrentTurnName.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentTurnName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelCurrentTurnName.Location = new System.Drawing.Point(396, 41);
            this.labelCurrentTurnName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCurrentTurnName.Name = "labelCurrentTurnName";
            this.labelCurrentTurnName.Size = new System.Drawing.Size(59, 19);
            this.labelCurrentTurnName.TabIndex = 12;
            this.labelCurrentTurnName.Text = "label1";
            // 
            // labelCurrTurn
            // 
            this.labelCurrTurn.AutoSize = true;
            this.labelCurrTurn.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrTurn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelCurrTurn.Location = new System.Drawing.Point(396, 61);
            this.labelCurrTurn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCurrTurn.Name = "labelCurrTurn";
            this.labelCurrTurn.Size = new System.Drawing.Size(113, 19);
            this.labelCurrTurn.TabIndex = 11;
            this.labelCurrTurn.Text = "it\'s your turn";
            // 
            // labelSecondPlayerPoints
            // 
            this.labelSecondPlayerPoints.AutoSize = true;
            this.labelSecondPlayerPoints.BackColor = System.Drawing.Color.Transparent;
            this.labelSecondPlayerPoints.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelSecondPlayerPoints.Location = new System.Drawing.Point(205, 77);
            this.labelSecondPlayerPoints.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSecondPlayerPoints.Name = "labelSecondPlayerPoints";
            this.labelSecondPlayerPoints.Size = new System.Drawing.Size(18, 19);
            this.labelSecondPlayerPoints.TabIndex = 10;
            this.labelSecondPlayerPoints.Text = "0";
            // 
            // labelSecondPlayerName
            // 
            this.labelSecondPlayerName.AutoSize = true;
            this.labelSecondPlayerName.BackColor = System.Drawing.Color.Transparent;
            this.labelSecondPlayerName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelSecondPlayerName.Location = new System.Drawing.Point(205, 59);
            this.labelSecondPlayerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSecondPlayerName.Name = "labelSecondPlayerName";
            this.labelSecondPlayerName.Size = new System.Drawing.Size(82, 19);
            this.labelSecondPlayerName.TabIndex = 9;
            this.labelSecondPlayerName.Text = "Player 2:";
            // 
            // labelFirstPlayerPoints
            // 
            this.labelFirstPlayerPoints.AutoSize = true;
            this.labelFirstPlayerPoints.BackColor = System.Drawing.Color.Transparent;
            this.labelFirstPlayerPoints.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelFirstPlayerPoints.Location = new System.Drawing.Point(11, 77);
            this.labelFirstPlayerPoints.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFirstPlayerPoints.Name = "labelFirstPlayerPoints";
            this.labelFirstPlayerPoints.Size = new System.Drawing.Size(18, 19);
            this.labelFirstPlayerPoints.TabIndex = 8;
            this.labelFirstPlayerPoints.Text = "0";
            // 
            // lableFirstPlayer
            // 
            this.lableFirstPlayer.AutoSize = true;
            this.lableFirstPlayer.BackColor = System.Drawing.Color.Transparent;
            this.lableFirstPlayer.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lableFirstPlayer.Location = new System.Drawing.Point(11, 59);
            this.lableFirstPlayer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lableFirstPlayer.Name = "lableFirstPlayer";
            this.lableFirstPlayer.Size = new System.Drawing.Size(82, 19);
            this.lableFirstPlayer.TabIndex = 7;
            this.lableFirstPlayer.Text = "Player 1:";
            // 
            // labelPlayerScores
            // 
            this.labelPlayerScores.AutoSize = true;
            this.labelPlayerScores.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayerScores.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayerScores.Location = new System.Drawing.Point(11, 38);
            this.labelPlayerScores.Name = "labelPlayerScores";
            this.labelPlayerScores.Size = new System.Drawing.Size(60, 19);
            this.labelPlayerScores.TabIndex = 14;
            this.labelPlayerScores.Text = "Score:";
            // 
            // CheckersBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CheckersUI.Properties.Resources.Form_Background;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.labelPlayerScores);
            this.Controls.Add(this.labelInstruction);
            this.Controls.Add(this.labelCurrentTurnName);
            this.Controls.Add(this.labelCurrTurn);
            this.Controls.Add(this.labelSecondPlayerPoints);
            this.Controls.Add(this.labelSecondPlayerName);
            this.Controls.Add(this.labelFirstPlayerPoints);
            this.Controls.Add(this.lableFirstPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "CheckersBoardForm";
            this.Text = "Checkers";
            this.Load += new System.EventHandler(this.CheckersBoardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Label labelCurrentTurnName;
        private System.Windows.Forms.Label labelCurrTurn;
        private System.Windows.Forms.Label labelSecondPlayerPoints;
        private System.Windows.Forms.Label labelSecondPlayerName;
        private System.Windows.Forms.Label labelFirstPlayerPoints;
        private System.Windows.Forms.Label lableFirstPlayer;
        private System.Windows.Forms.Label labelPlayerScores;
    }
}