using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckersUI
{
    public partial class GameSettingsForm : Form
    {
        private string m_BoardSize = string.Empty;
        private string m_FirstPlayerName = string.Empty;
        private string m_SecondPlayerName = string.Empty;
        private bool m_SettingsComplete = false;
        private bool m_IsTwoPlayersChosen = false;

        public string BoardSize
        {
            get { return m_BoardSize; }
        }

        public string FirstPlayerName
        {
            get { return m_FirstPlayerName; }
        }

        public string SecondPlayerName
        {
            get { return m_SecondPlayerName; }
        }

        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void setBoardSize(object sender, EventArgs e)
        {
            bool isUpdateBoardSize = m_BoardSize != string.Empty;

            foreach (var radioButton in this.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    m_BoardSize = radioButton.Text.ToString();
                }
            }
        }

        private void textBoxPlayerName_TextChanged(object sender, EventArgs e)
        {
            bool isValidName = validatePlayersName(sender as TextBox);

            if (!isValidName)
            {
                string message = @"Player's name must be under 21 characters,
        and must not contain any spaces";
                string caption = "Error message";
                MessageBox.Show(message, caption);
            }
        }

        private bool validatePlayersName(TextBox i_PlayerNameStr)
        {
            string text = i_PlayerNameStr.Text;
            const short k_MaxNumLettersInName = 20;
            bool isValidPlayerName = text.Length <= k_MaxNumLettersInName;

            if (isValidPlayerName)
            {
                foreach (char charOfStr in text)
                {
                    isValidPlayerName = charOfStr != ' ';

                    if (!isValidPlayerName)
                    {
                        break;
                    }
                }
            }

            if (!isValidPlayerName)
            {
                i_PlayerNameStr.Text = string.Empty;
            }

            return isValidPlayerName;
        }

        private void buttonDoneSettings_Click(object sender, EventArgs e)
        {
            updateSettingsStatus();

            if (m_SettingsComplete)
            {
                m_FirstPlayerName = textBoxFirstPlayerName.Text;
                if (checkBoxSecondPlayer.Checked)
                {
                    m_SecondPlayerName = textBoxSecondPlayerName.Text;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                string message = @"Please check if fields have been filled";
                string caption = "Error message";
                MessageBox.Show(message, caption);
            }
        }

        private void updateSettingsStatus()
        {
            m_SettingsComplete =
                            textBoxFirstPlayerName.Text != string.Empty &&
                            ((checkBoxSecondPlayer.Checked &&
                            textBoxSecondPlayerName.Text != string.Empty)
                            || (!checkBoxSecondPlayer.Checked));
        }

        private void checkBoxSecondPlayer_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_IsTwoPlayersChosen)
            {
                textBoxSecondPlayerName.Enabled = true;
                textBoxSecondPlayerName.Text = string.Empty;
                m_IsTwoPlayersChosen = true;
            }
            else
            {
                textBoxSecondPlayerName.Enabled = false;
                textBoxSecondPlayerName.Text = "[Computer]";
                m_IsTwoPlayersChosen = false;
            }
        }

        private void GameSettingsForm_Load(object sender, EventArgs e)
        {
        }
    }
}
