using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameCheckers;

namespace CheckersUI
{
    internal delegate void TimeComputerMoveHandler(object sender, TimeComputerMoveArgs e);

    public partial class CheckersBoardForm : Form
    {
        private const int k_FirstPlayerIndex = 0;
        private const int k_SecondPlayerIndex = 1;
        private readonly int r_ButtonWidth = 50;
        private readonly int r_TableSize;
        private readonly Timer r_ComputerTurnTimer = new Timer();
        private BoardGameButton[,] m_BoardGameUI;
        private GameManager m_GameManager;
        private List<Coordinate> m_MoveFromTo = new List<Coordinate>();
        private List<Coordinate> m_NextEatCoords = new List<Coordinate>();
        private BoardGameButton m_BlueButton;
        private bool m_BlueButtonExist = false;
        private int m_PlayerIndex = 0;
        private bool m_SwitchTurns = false;
        private DialogResult m_RestartGameResult;
        private eGameEndingResult m_EndingResult = eGameEndingResult.NoResult;

        public DialogResult RestartGameResult
        {
            get { return m_RestartGameResult; }
        }

        public int CurrentPlayerIndex
        {
            get { return m_PlayerIndex; }
        }

        public CheckersBoardForm(
            int i_TableSize,
            string i_FirstName,
            string i_SecondName,
            GameManager i_GameManager)
        {
            r_TableSize = i_TableSize;
            m_BoardGameUI = new BoardGameButton[i_TableSize, i_TableSize];
            m_GameManager = i_GameManager;
            InitializeComponent();
            setLablesName(i_FirstName, i_SecondName);
            buildBoard();
            initBoardGameMatrix(i_GameManager.GameBoard);
            i_GameManager.MovementManager.PieceHasMoved += buttonsClicked_PieceMoved;
        }

        private void buildBoard()
        {
            int startingRow = lableFirstPlayer.Bottom + 25;
            int startingCol = this.Left;
            for (int i = 0; i < r_TableSize; i++)
            {
                for (int j = 0; j < r_TableSize; j++)
                {
                    BoardGameButton currButton = new BoardGameButton();
                    currButton.Width = r_ButtonWidth;
                    currButton.Height = r_ButtonWidth;
                    currButton.Location = new Point(startingCol, startingRow);
                    setButtonColor(i, j, currButton);
                    currButton.CurrCoordinate = new Coordinate(i + 1, j + 1);
                    currButton.Click += boardButton_Click;
                    this.Controls.Add(currButton);
                    m_BoardGameUI[i, j] = currButton;
                    startingCol += r_ButtonWidth;
                }

                startingCol = this.Left;
                startingRow += r_ButtonWidth;
            }

            this.Height = startingRow + 70;
            this.Width = this.Left + labelInstruction.Right + 20;
        }

        private void setLablesName(string i_FirstName, string i_SecondName)
        {
            lableFirstPlayer.Text = i_FirstName += ": ";
            labelSecondPlayerName.Text = i_SecondName += ": ";
            labelCurrentTurnName.Text = i_FirstName;
        }

        private void setButtonColor(int i_ButtonRow, int i_ButtonCol, BoardGameButton o_CurrButton)
        {
            bool isBlack = (i_ButtonRow % 2 == 0 && i_ButtonCol % 2 == 0) ||
                (i_ButtonRow % 2 == 1 && i_ButtonCol % 2 == 1);

            if (isBlack)
            {
                o_CurrButton.BackColor = Color.Black;
                o_CurrButton.Enabled = false;
            }
            else
            {
                o_CurrButton.BackColor = Color.White;
            }
        }

        private void initBoardGameMatrix(GameBoard i_GameBoard)
        {
            for (int i = 1; i < r_TableSize + 1; i++)
            {
                for (int j = 1; j < r_TableSize + 1; j++)
                {
                    if (i_GameBoard.BoardGameMatrix[i, j].CharInCell == GameCheckers.BoardCell.k_XFigure)
                    {
                        m_BoardGameUI[i - 1, j - 1].Image = Properties.Resources.Piece_Black_Reg;
                    }
                    else if (i_GameBoard.BoardGameMatrix[i, j].CharInCell == GameCheckers.BoardCell.k_OFigure)
                    {
                        m_BoardGameUI[i - 1, j - 1].Image = Properties.Resources.Piece_White_Reg;
                    }
                    else
                    {
                        m_BoardGameUI[i - 1, j - 1].Image = null;
                    }
                }
            }
        }

        private void boardButton_Click(object sender, EventArgs e)
        {
            BoardGameButton currButton = sender as BoardGameButton;
            bool changeColor = !m_BlueButtonExist;

            if (changeColor)
            {
                handleSelectedFirstButton(currButton);
            }
            else
            {
                handleSelectedSecondButton(currButton);
            }
        }

        private void handleSelectedSecondButton(BoardGameButton i_CurrentButton)
        {
            if (m_BlueButtonExist)
            {
                bool deleteChoice = i_CurrentButton.CurrCoordinate.Equals(m_MoveFromTo[0]);

                if (deleteChoice)
                {
                    m_MoveFromTo.Remove(i_CurrentButton.CurrCoordinate);
                    m_BlueButton.BackColor = Color.White;
                }
                else
                {
                    m_MoveFromTo.Add(i_CurrentButton.CurrCoordinate);
                    checkPlayersMoveAndMakeMovement();
                }

                m_BlueButtonExist = false;
                m_BlueButton.BackColor = Color.White;
            }
        }

        private void handleSelectedFirstButton(BoardGameButton i_CurrentButton)
        {
            m_MoveFromTo.Clear();
            i_CurrentButton.BackColor = Color.LightBlue;
            m_BlueButton = i_CurrentButton;
            m_MoveFromTo.Add(i_CurrentButton.CurrCoordinate);
            m_BlueButtonExist = true;
        }

        private void checkPlayersMoveAndMakeMovement()
        {
            List<bool> validMovesForEachPlayer = new List<bool>();
            
            if (m_GameManager.CurrGameStatus == GameCheckers.eGameStatus.GameOnGoing)
            {
                m_GameManager.CheckIfMovesAvailable(m_PlayerIndex, validMovesForEachPlayer);
                if (validMovesForEachPlayer[k_FirstPlayerIndex])
                {
                    getInputAndPerformMoveIfAvailableMoves();
                }

                if (m_SwitchTurns)
                {
                    m_PlayerIndex = (m_PlayerIndex + 1) % 2;
                    UpdateCurrentTurnNameLabel();
                }

                checkIfComputerPlaysAndPerformHisMove(validMovesForEachPlayer);
                checkEndingAndUpdateWinnerIndex(validMovesForEachPlayer);
            }
        }

        private void checkIfComputerPlaysAndPerformHisMove(List<bool> i_ValidMovesForPlayers)
        {
            const bool v_EnableButtons = true;
            bool isComputer;
            const int k_TwoSecondsIntervalForComputersTurn = 2000;

            isComputer = !m_GameManager.GamePlayers[m_PlayerIndex].isHumanPlayer;
            if (isComputer && m_SwitchTurns)
            {
                enableOrDisableButtonsWhileComputersTurn(!v_EnableButtons);
                m_SwitchTurns = false;
                UpdateCurrentTurnNameLabel();
                TimeComputerMoveArgs computerMoveArgs = new TimeComputerMoveArgs(i_ValidMovesForPlayers);
                r_ComputerTurnTimer.Tick += (object sender, EventArgs e) 
                    => computerTimer_ReachedInterval(sender, e, computerMoveArgs);
                r_ComputerTurnTimer.Interval = k_TwoSecondsIntervalForComputersTurn;
                r_ComputerTurnTimer.Start();
            }
        }

        private void enableOrDisableButtonsWhileComputersTurn(bool i_IsToEnable)
        {
            foreach(BoardGameButton button in m_BoardGameUI)
            {
                button.Enabled = i_IsToEnable;
            }
        }

        internal void DisplayResultAndRequestCheckIfPlayAnotherRound()
        {
            StringBuilder message = new StringBuilder();
            string caption = "Game end";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            message.Clear();
            message = createRoundFinishMessage(message);
            m_RestartGameResult = MessageBox.Show(message.ToString(), caption, buttons);
            this.DialogResult = DialogResult.OK;
        }

        private StringBuilder createRoundFinishMessage(StringBuilder i_MessageStr)
        {
            const string k_AnotherRoundMsgStr = "Choose if to play another round";

            if (m_EndingResult == eGameEndingResult.Tie)
            {
                i_MessageStr.Append(@"Tie!");
                i_MessageStr.Append(k_AnotherRoundMsgStr);
            }
            else
            {
                if (m_EndingResult == eGameEndingResult.NoResult)
                {
                    m_EndingResult = (eGameEndingResult)Player.GetEnemyPlayerIndex(m_PlayerIndex);
                }

                string winMsg = string.Format(
@"{0} you won this round!
", 
m_GameManager.GamePlayers[(int)m_EndingResult].PlayerName);
                i_MessageStr.Append(winMsg);
                i_MessageStr.Append(k_AnotherRoundMsgStr);
            }

            return i_MessageStr;
        }

        private void computerTimer_ReachedInterval(object sender, EventArgs e, TimeComputerMoveArgs args)
        {
            const bool v_EnableButtons = true;

            r_ComputerTurnTimer.Stop();
            while (!m_SwitchTurns)
            {
                m_GameManager.CheckIfMovesAvailable(m_PlayerIndex, args.m_ValidMovesForPlayerArg);
                if (args.m_ValidMovesForPlayerArg[k_SecondPlayerIndex])
                {
                    getInputAndPerformMoveIfAvailableMoves();
                }

                if (m_SwitchTurns)
                {
                    m_PlayerIndex = (m_PlayerIndex + 1) % 2;
                    UpdateCurrentTurnNameLabel();
                }
            }

            enableOrDisableButtonsWhileComputersTurn(v_EnableButtons);
            checkEndingAndUpdateWinnerIndex(args.m_ValidMovesForPlayerArg);
        }

        private void checkEndingAndUpdateWinnerIndex(List<bool> i_ValidMovesForEachPlayer)
        {
            const int k_UnassignedPlayerIndex = 2;
            bool endGame = false;
            int winningPlayerIndex = k_UnassignedPlayerIndex;

            if (endGame = m_GameManager.CheckIfGameAtDraw(i_ValidMovesForEachPlayer))
            {
                m_EndingResult = eGameEndingResult.Tie;
            }
            else
            {
                if (endGame = m_GameManager.CheckIfEnded(i_ValidMovesForEachPlayer, ref winningPlayerIndex))
                {
                    updateWinnerPlayer(winningPlayerIndex);
                }
            }

            if (endGame)
            {
                if (!m_GameManager.GamePlayers[k_SecondPlayerIndex].isHumanPlayer)
                {
                    r_ComputerTurnTimer.Stop();
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void updateWinnerPlayer(int i_WinnerPlayerIndex)
        {
            if (i_WinnerPlayerIndex == k_FirstPlayerIndex)
            {
                m_EndingResult = eGameEndingResult.PlayerOneWon;
            }
            else
            {
                m_EndingResult = eGameEndingResult.PlayerTwoOne;
            }
        }

        internal void UpdatePlayerPointLabelAfterEnding()
        {
            List<Player> gamePlayers = m_GameManager.GamePlayers;

            labelFirstPlayerPoints.Text = gamePlayers[k_FirstPlayerIndex].PlayerPoints.ToString();
            labelSecondPlayerPoints.Text = gamePlayers[k_SecondPlayerIndex].PlayerPoints.ToString();
        }

        private void getInputAndPerformMoveIfAvailableMoves()
        {
            bool isPossibleToEatMore = false;
            string playerInputStr = string.Empty;
            bool isValidMove = false;

           getAndValidateInput(m_PlayerIndex, ref isValidMove);
            if (m_GameManager.CurrGameStatus == GameCheckers.eGameStatus.GameOnGoing)
            {
                if (isValidMove)
                {
                    m_NextEatCoords = m_GameManager.MakeMoveFromMoveManager(m_PlayerIndex, ref isPossibleToEatMore);
                    checkEatMoreAndPerformMove(isPossibleToEatMore, m_PlayerIndex);
                }
            }
        }

        private bool checkEatMoreAndPerformMove(bool i_IsCanEatMore, int i_PlayerIndex)
        {
            bool isTurnFinished = false;
            StringBuilder playerInputSB = new StringBuilder();
            bool isValidForEat = false;

            m_SwitchTurns = !m_GameManager.CheckIfContinueEat(i_IsCanEatMore, i_PlayerIndex, m_NextEatCoords);
            if (!m_SwitchTurns)
            {
                if (!isValidForEat && m_GameManager.CurrGameStatus == GameCheckers.eGameStatus.GameOnGoing)
                {
                    m_GameManager.UpdateGamePlayersSoldiers();
                }
            }

            isTurnFinished = !i_IsCanEatMore || m_NextEatCoords.Count == 0;
            return isTurnFinished;
        }

        internal void InitializeBoardForm(GameBoard i_GameBoard)
        {
            m_EndingResult = eGameEndingResult.NoResult;
            initBoardGameMatrix(i_GameBoard);
            m_PlayerIndex = 0;
            m_SwitchTurns = false;
        }

        private string getPlayerTurnInput()
        {
            string userTurnInputStr = Coordinate.ConvertFromCoordToString(m_MoveFromTo[0]);
            userTurnInputStr += ">";
            userTurnInputStr += Coordinate.ConvertFromCoordToString(m_MoveFromTo[1]);

            return userTurnInputStr;
        }

        internal string getAndValidateInput(int i_PlayerIndex, ref bool o_IsValid)
        {
            string playerInputStr = string.Empty;
            Player currentPlayer = m_GameManager.GamePlayers[i_PlayerIndex];

            playerInputStr = getInputFromUserOrComputer(i_PlayerIndex, ref o_IsValid);
            if (m_GameManager.CurrGameStatus == GameCheckers.eGameStatus.GameOnGoing)
            {
                o_IsValid = m_GameManager.CheckChosenMoveValid(currentPlayer, playerInputStr);
                if (!o_IsValid)
                {
                    presenetInvalidMoveMessage();   
                }
                else
                {
                    m_SwitchTurns = true; 
                }
            }

            return playerInputStr;
        }

        private void presenetInvalidMoveMessage()
        {
            string message = "Not Valid Move! try again";
            string caption = "Wrong Move";

            m_SwitchTurns = false;
            MessageBox.Show(message, caption);
        }
                
        private string getInputFromUserOrComputer(int i_PlayerIndex, ref bool io_IsValidForEat)
        {
            string userInputStr = string.Empty;

            if (m_GameManager.GamePlayers[i_PlayerIndex].isHumanPlayer)
            {
                userInputStr = getPlayerTurnInput();
            }
            else
            {
                userInputStr = m_GameManager.GetComputerInput(m_GameManager.GamePlayers[i_PlayerIndex]);
            }

            return userInputStr;
        }

        internal void UpdateCurrentTurnNameLabel()
        {
            string currPlayerName = string.Empty;

            if (m_PlayerIndex % 2 == 0)
            {
                currPlayerName = lableFirstPlayer.Text;
            }
            else
            {
                currPlayerName = labelSecondPlayerName.Text;
            }

            labelCurrentTurnName.Text = currPlayerName;
        }

        private void updateCellsAsKingImage(Coordinate i_EndCoord)
        {
            if (m_PlayerIndex == k_FirstPlayerIndex)
            {
                m_BoardGameUI[i_EndCoord.CoordRow - 1, i_EndCoord.CoordCol - 1].Image = Properties.Resources.Piece_Black_King;
            }
            else
            {
                m_BoardGameUI[i_EndCoord.CoordRow - 1, i_EndCoord.CoordCol - 1].Image = Properties.Resources.Piece_White_King;
            }
        }

        private void updateCellAsRegImage(Coordinate i_EndCoord, Coordinate i_StartCoord)
        {
            m_BoardGameUI[i_EndCoord.CoordRow - 1, i_EndCoord.CoordCol - 1].Image =
                   m_BoardGameUI[i_StartCoord.CoordRow - 1, i_StartCoord.CoordCol - 1].Image;
        }

        private void updateCellAfterEating(Coordinate i_EndCoord, Coordinate i_StartCoord)
        {
            int eatCoordRow;
            int eatCoordCol;

            eatCoordRow = (i_EndCoord.CoordRow - 1 + i_StartCoord.CoordRow - 1) / 2;
            eatCoordCol = (i_EndCoord.CoordCol - 1 + i_StartCoord.CoordCol - 1) / 2;
            m_BoardGameUI[eatCoordRow, eatCoordCol].Image = null;
        }

        private void buttonsClicked_PieceMoved(object sender, GameCheckers.PieceMovedArgs e)
        {
            Coordinate startingCoord = e.PieceStartCoordinate;
            Coordinate endCoord = e.PieceEndCoordinate;
            
            if (e.IsBecomeKing)
            {
                updateCellsAsKingImage(endCoord);
            }
            else
            {
                updateCellAsRegImage(endCoord, startingCoord);
            }

            if(e.IsPerformedEat)
            {
                updateCellAfterEating(endCoord, startingCoord);
            }
            
            m_BoardGameUI[startingCoord.CoordRow - 1, startingCoord.CoordCol - 1].Image = null;
        }

        private void CheckersBoardForm_Load(object sender, EventArgs e)
        {
        }
    }
}
