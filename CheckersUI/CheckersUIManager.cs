using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GameCheckers;

namespace CheckersUI
{
    public class CheckersUIManager
    {
        private const int k_NumOfPlayers = 2;
        private GameManager m_GameManager;
        private CheckersBoardForm m_CheckersBoardForm;
        private bool m_IsGameRunning = true;

        public GameManager GameManager
        {
            get { return m_GameManager; }
        }

        public CheckersUIManager()
        {
            const string k_DefaultPlayerTwoName = "Computer";
            GameSettingsForm settingForm = new GameSettingsForm();
            m_IsGameRunning = settingForm.ShowDialog() == DialogResult.OK;
            bool isPlayerTwoHuman = settingForm.SecondPlayerName != string.Empty;
            string playerOneNameStr = settingForm.FirstPlayerName;
            string playerTwoNameStr = k_DefaultPlayerTwoName;
            string gameBoardSizeStr = convertBoardSizeStringToParsableString(settingForm.BoardSize);

            if (isPlayerTwoHuman)
            {
                playerTwoNameStr = settingForm.SecondPlayerName;
            }

            setGameAndBoardSettings(
                 playerOneNameStr,
                 playerTwoNameStr,
                 gameBoardSizeStr,
                 isPlayerTwoHuman);
        }

        private string convertBoardSizeStringToParsableString(string i_BoardSizeStr)
        {
            string boardSize = "6";

            if (i_BoardSizeStr == "8 x 8")
            {
                boardSize = "8";
            }
            else if(i_BoardSizeStr == "10 x 10")
            {
                boardSize = "10";
            }

            return boardSize;
        }
        
        public bool IsGameRun
        {
            get { return m_IsGameRunning; }
        }

        private void setGameAndBoardSettings(
            string i_PlayerOneNameStr,
                string i_PlayerTwoNameStr,
                string i_GameBoardSizeStr,
                bool i_IsPlayerTwoHuman)
        {
            m_GameManager = new GameManager(
                i_PlayerOneNameStr,
                i_PlayerTwoNameStr,
                i_GameBoardSizeStr,
                i_IsPlayerTwoHuman);
            m_CheckersBoardForm = new CheckersBoardForm(
                int.Parse(i_GameBoardSizeStr),
                i_PlayerOneNameStr,
                i_PlayerTwoNameStr,
                m_GameManager);
        }

        public void StartGameIfNoExitSettings()
        {
            if (m_IsGameRunning)
            {
                mainGameConsoleLoop();
            }
        }

        private void mainGameConsoleLoop()
        {
            while (m_IsGameRunning)
            {
                if (m_CheckersBoardForm.ShowDialog() != DialogResult.None)
                {                    
                    m_GameManager.CurrGameStatus = GameCheckers.eGameStatus.GameExitStatus;
                    m_CheckersBoardForm.DisplayResultAndRequestCheckIfPlayAnotherRound();
                    m_GameManager.CalcPlayerScoreAfterGame(
                        Player.GetEnemyPlayerIndex(m_CheckersBoardForm.CurrentPlayerIndex));
                    m_CheckersBoardForm.UpdatePlayerPointLabelAfterEnding();
                    checkIfRestartGame(m_CheckersBoardForm.RestartGameResult);                    
                }
            }

            m_CheckersBoardForm.Close();
            evaluateFullGameEndingResult();            
        }

        private void evaluateFullGameEndingResult()
        {
            bool isGameEndedInDraw = false;
            string message = string.Empty;
            string caption = string.Empty;
            StringBuilder finalGameVictor = new StringBuilder();

            finalGameVictor.Append(m_GameManager.GetFinalResult(ref isGameEndedInDraw));
            if (isGameEndedInDraw)
            {
                message = "Game ended with a draw";
                caption = "Game over";
            }
            else
            {
                finalGameVictor.Append(" you are the final victor!!!");
                message = finalGameVictor.ToString();
                caption = "Game over";
            }

            MessageBox.Show(message, caption);
        }

        private void checkIfRestartGame(DialogResult i_IsGameReset)
        {
            m_IsGameRunning = i_IsGameReset == DialogResult.Yes;
            if (m_IsGameRunning)
            {
                m_GameManager.ReinitializeGameObjects();
                m_CheckersBoardForm.InitializeBoardForm(m_GameManager.GameBoard);
            }
        }
    }
}
