using System.Collections.Generic;
using System.Text;

namespace GameCheckers
{
    public class GameManager
    {
        private const int k_PlayerOneIndex = 0;
        private const int k_PlayerTwoIndex = 1;

        private List<Player> m_GamePlayers;

        private GameBoard m_GameBoard;

        private bool m_isGameRunning;

        private MovementManager m_GameMovement = new MovementManager();

        public GameBoard GameBoard
        {
            get { return m_GameBoard; }
        }

        public MovementManager MovementManager
        {
            get { return m_GameMovement; }
        }

        private eGameStatus m_CurrGameStatus;

        public List<Player> GamePlayers
        {
            get { return m_GamePlayers; }
        }

        public bool IsGameRunning
        {
            get { return m_isGameRunning; }
            set { m_isGameRunning = value; }
        }

        public eGameStatus CurrGameStatus
        {
            get { return m_CurrGameStatus; }
            set { m_CurrGameStatus = value; }
        }

        public GameManager(string i_PlayerOneName, string i_PlayerTwoName, string i_BoardSizeStr, bool i_IsTwoHumanPlayer)
        {
            m_CurrGameStatus = eGameStatus.GameOnGoing;
            m_isGameRunning = true;
            m_GamePlayers = new List<Player>();
            m_GameBoard = new GameBoard(i_BoardSizeStr);
            initializePlayers(i_PlayerOneName, i_PlayerTwoName, i_IsTwoHumanPlayer);
        }

        private void initializePlayers(string i_PlayerOneName, string i_PlayerTwoName, bool i_IsPlayerTwoHuman)
        {
            const bool v_PlayerOneIsHuman = true;

            m_GamePlayers.Add(new Player(
                PlayerDirection.ePlayerDirection.Up,
                BoardCell.k_XFigure,
                BoardCell.k_KFigure,
                i_PlayerOneName,
                v_PlayerOneIsHuman,
                m_GameBoard));
            m_GamePlayers.Add(new Player(
                PlayerDirection.ePlayerDirection.Down,
                BoardCell.k_OFigure,
                BoardCell.k_UFigure,
                i_PlayerTwoName,
                i_IsPlayerTwoHuman,
                m_GameBoard));
        }

        public bool CheckIfEnded(List<bool> i_PlayersValidMove, ref int o_WonPlayerInd)
        {
            bool isGameEnded = false;
            const bool v_GameEnded = true;
            int playerOneNumSoldiers = m_GamePlayers[k_PlayerOneIndex].CoordsOfSoldiers.Count;
            int playerTwoNumSoldiers = m_GamePlayers[k_PlayerTwoIndex].CoordsOfSoldiers.Count;

            if (checkIfAnyPlayerWon(playerOneNumSoldiers, playerTwoNumSoldiers, m_GamePlayers, ref o_WonPlayerInd))
            {
                m_CurrGameStatus = eGameStatus.GameIsFinished;
                isGameEnded = v_GameEnded;
            }

            return isGameEnded;
        }

        private bool checkIfAnyPlayerWon(
            int i_PlayerOneNumSoldiers,
            int i_PlayerTwoNumSoldiers,
            List<Player> io_GamePlayers,
            ref int o_WonPlayerIndex)
        {
            bool isAnyPlayerWon = false;
            const int k_NoSoldiers = 0;

            if (isAnyPlayerWon = i_PlayerOneNumSoldiers == k_NoSoldiers)
            {
                o_WonPlayerIndex = k_PlayerTwoIndex;
            }
            else if (isAnyPlayerWon = i_PlayerTwoNumSoldiers == k_NoSoldiers)
            {
                o_WonPlayerIndex = k_PlayerOneIndex;
            }

            return isAnyPlayerWon;
        }

        public bool CheckIfGameAtDraw(List<bool> i_PlayersValidMove)
        {
            int playerOneNumSoldiers = m_GamePlayers[k_PlayerOneIndex].CoordsOfSoldiers.Count;
            int playerTwoNumSoldiers = m_GamePlayers[k_PlayerTwoIndex].CoordsOfSoldiers.Count;
            bool isGameAtDraw = false;
            const int k_BothPlayersWereCheckedForValidMoves = 2;
            const int k_NoSoldiers = 0;

            if (i_PlayersValidMove.Count == k_BothPlayersWereCheckedForValidMoves)
            {
                if (!i_PlayersValidMove[k_PlayerOneIndex] && !i_PlayersValidMove[k_PlayerTwoIndex])
                {
                    if (isGameAtDraw = playerOneNumSoldiers > k_NoSoldiers && playerTwoNumSoldiers > k_NoSoldiers)
                    {
                        m_CurrGameStatus = eGameStatus.GameIsFinished;
                    }
                }
            }

            return isGameAtDraw;
        }

        public void CheckIfMovesAvailable(int i_CurrentPlayerIndex, List<bool> o_AvailableMovesForEachPlayer)
        {
            if (m_CurrGameStatus == eGameStatus.GameOnGoing)
            {
                o_AvailableMovesForEachPlayer.Add(m_GameMovement.CheckForAvailableMove(
                    m_GamePlayers[i_CurrentPlayerIndex],
                    m_GameBoard,
                    m_GamePlayers[i_CurrentPlayerIndex].NextEatCoords));
            }
        }

        public List<Coordinate> MakeMoveFromMoveManager(int i_CurrPlayerIndex, ref bool o_IsPossibleEatMore)
        {
            List<Coordinate> possibleNextMoves = m_GameMovement.MakeMove(
                                                                m_GameBoard,
                                                                ref o_IsPossibleEatMore,
                                                                m_GamePlayers[i_CurrPlayerIndex]);

            m_GamePlayers[i_CurrPlayerIndex].UpdateCoordsOfSoldiersList(m_GameBoard);

            return possibleNextMoves;
        }

        public bool CheckIfContinueEat(
            bool i_IsEatMore,
            int i_PlayerInd,
            List<Coordinate> i_PossibleNextMoves)
        {
            const int k_NoPossibleNextMoves = 0;
            bool isCanContinueToEat = false;

            if (i_IsEatMore)
            {
                m_GamePlayers[Player.GetEnemyPlayerIndex(i_PlayerInd)].UpdateCoordsOfSoldiersList(m_GameBoard);
            }

            isCanContinueToEat = i_IsEatMore && i_PossibleNextMoves.Count > k_NoPossibleNextMoves;

            return isCanContinueToEat;
        }

        public void CheckMoveWasToEatAndMakeIfWas(bool i_IsValidForEat, StringBuilder i_PlayerInputSB, int i_PlayerIndex)
        {
            if (i_IsValidForEat)
            {
                m_GameMovement.SetCoordsInfoForValidEatMove(
                    i_PlayerInputSB.ToString(),
                    m_GameBoard,
                    m_GamePlayers[i_PlayerIndex]);
            }
        }

        public string GetCompEatInput(ref bool o_IsValidForEat, List<Coordinate> i_PossibleNextMoves)
        {
            string playerInputStr = m_GameMovement.GetComputerInputContinueEating(
                i_PossibleNextMoves,
                m_GameMovement.NextCoord);
           
            o_IsValidForEat = true;

            return playerInputStr;
        }

        public bool CheckChosenCurrCoordForContinueEat(StringBuilder i_PlayerInputStr)
        {
            const int k_StartStrIndex = 0;
            Coordinate userChosenCoord = Coordinate.FromStringToCoordinate(i_PlayerInputStr.ToString(), k_StartStrIndex);

            bool isValidCurrCoord = Coordinate.AreTwoCoordsEqual(userChosenCoord, m_GameMovement.NextCoord);

            return isValidCurrCoord;
        }

        public bool CheckChosenNextCoordForEating(StringBuilder i_PlayerInputSB, List<Coordinate> i_AfterEatCoords)
        {
            const int k_nextCoordIndex = 3;
            bool isCoordEqualToInCoordList = false;
            Coordinate userNextCoord = Coordinate.FromStringToCoordinate(i_PlayerInputSB.ToString(), k_nextCoordIndex);

            for (int i = 0; i < i_AfterEatCoords.Count && !isCoordEqualToInCoordList; i++)
            {
                isCoordEqualToInCoordList = Coordinate.AreTwoCoordsEqual(i_AfterEatCoords[i], userNextCoord);
            }

            return isCoordEqualToInCoordList;
        }

        public string GetComputerInput(Player i_CurrentPlayer)
        {
            string computerTurnInput = m_GameMovement.RequestValidComputerMove(m_GameBoard, i_CurrentPlayer);
           
            return computerTurnInput;
        }

        public bool CheckChosenMoveValid(Player i_CurrentPlayer, string i_PlayerTurnInput)
        {
            bool isChosenMoveValid = false;
            isChosenMoveValid = m_GameMovement.CheckValidMove(i_CurrentPlayer, m_GameBoard, i_PlayerTurnInput);

            return isChosenMoveValid;
        }

        public void UpdateGamePlayersSoldiers()
        {
            m_GamePlayers[k_PlayerOneIndex].UpdateCoordsOfSoldiersList(m_GameBoard);
            m_GamePlayers[k_PlayerTwoIndex].UpdateCoordsOfSoldiersList(m_GameBoard);
        }

        public bool VerifyUserInputIsEatableMove(
            StringBuilder i_PlayerInputSB,
            List<Coordinate> i_NextEatCoords,
            int i_CurrentPlayerIndex)
        {
            bool isEatMoveFromUserValid = false;

            isEatMoveFromUserValid = CheckChosenCurrCoordForContinueEat(i_PlayerInputSB) &&
                                             CheckChosenNextCoordForEating(i_PlayerInputSB, i_NextEatCoords);
            CheckMoveWasToEatAndMakeIfWas(isEatMoveFromUserValid, i_PlayerInputSB, i_CurrentPlayerIndex);

            return isEatMoveFromUserValid;
        }

        public void CalcPlayerScoreAfterGame(int i_PlayerWonIndex)
        {
            int playerOneScore = m_GamePlayers[k_PlayerOneIndex].calcNumOfPlayerSoldiers(m_GameBoard);
            int playerTwoScore = m_GamePlayers[k_PlayerTwoIndex].calcNumOfPlayerSoldiers(m_GameBoard);

            m_CurrGameStatus = eGameStatus.GameIsFinished;
            if (i_PlayerWonIndex == k_PlayerOneIndex)
            {
                if (playerOneScore - playerTwoScore > 0)
                {
                    m_GamePlayers[k_PlayerOneIndex].PlayerPoints += playerOneScore - playerTwoScore;
                }
            }
            else
            {
                if (playerTwoScore - playerOneScore > 0)
                {
                    m_GamePlayers[k_PlayerTwoIndex].PlayerPoints += playerTwoScore - playerOneScore;
                }
            }
        }

        public void ReinitializeGameObjects()
        {
            m_CurrGameStatus = eGameStatus.GameOnGoing;
            m_GameMovement.PositionsWhereCanEatFrom.Clear();
            m_GameMovement.ValidAfterEatPositions.Clear();
            m_GameMovement.CurrentCoord.ResetCoordinate();
            m_GameMovement.NextCoord.ResetCoordinate();
            m_GameBoard.InitBoardGameMatrix();
            foreach (Player currentPlayer in m_GamePlayers)
            {
                currentPlayer.UpdateCoordsOfSoldiersList(m_GameBoard);
                currentPlayer.NextEatCoords.Clear();
            }
        }

        public string GetFinalResult(ref bool io_IsGameAtDraw)
        {
            int firstPlayerScore = m_GamePlayers[k_PlayerOneIndex].PlayerPoints;
            int secondPlayerScore = m_GamePlayers[k_PlayerTwoIndex].PlayerPoints;
            string finalVictorName = m_GamePlayers[k_PlayerOneIndex].PlayerName;

            io_IsGameAtDraw = secondPlayerScore == firstPlayerScore;
            if (!io_IsGameAtDraw)
            {
                if (secondPlayerScore > firstPlayerScore)
                {
                    finalVictorName = m_GamePlayers[k_PlayerTwoIndex].PlayerName;
                }
            }

            return finalVictorName;
        }
    }
}