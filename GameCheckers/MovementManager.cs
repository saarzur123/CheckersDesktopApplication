using System;
using System.Text;
using System.Collections.Generic;

namespace GameCheckers
{
    public delegate void PieceMoveEventHandler(object sender, PieceMovedArgs e);

    public class MovementManager
    {
        protected const int k_InitCoordinate = 0;

        private Coordinate m_CurrentCoordinate = new Coordinate(k_InitCoordinate, k_InitCoordinate);

        private Coordinate m_NextCoordinate = new Coordinate(k_InitCoordinate, k_InitCoordinate);

        private List<Coordinate> m_PositionsWhereCanEatFrom { get; } = new List<Coordinate>();

        private List<Coordinate> m_ValidAfterEatPositions = new List<Coordinate>();

        public event PieceMoveEventHandler PieceHasMoved;

        public List<Coordinate> ValidAfterEatPositions
        {
            get { return m_ValidAfterEatPositions; }
            set { m_ValidAfterEatPositions = value; }
        }

        public Coordinate CurrentCoord
        {
            get { return m_CurrentCoordinate; }
            set { m_CurrentCoordinate = value; }
        }

        public Coordinate NextCoord
        {
            get { return m_NextCoordinate; }
            set { m_NextCoordinate = value; }
        }

        public List<Coordinate> PositionsWhereCanEatFrom
        {
            get { return m_PositionsWhereCanEatFrom; }
        }

        public void SetCoordsInfoForValidEatMove(string i_PlayerMoveInput, GameBoard i_GameBoard, Player i_CurrPlayer)
        {
            SetMoveCoordinates(i_PlayerMoveInput);
            ValidAfterEatPositions = checkCanEatPlacesFromCurrentCell(i_CurrPlayer.Direction, i_GameBoard, i_CurrPlayer);
        }

        private string chooseComputerEatMove(Player i_Computer, GameBoard io_GameBoard)
        {
            string computerMoveStr = string.Empty;
            Random randomEatPosition = new Random();
            int rangeOfPositions = i_Computer.NextEatCoords.Count;
            int afterEatPositionIndex;
            int eatFromPositionIndex = randomEatPosition.Next(rangeOfPositions);

            m_CurrentCoordinate = i_Computer.NextEatCoords[eatFromPositionIndex];
            m_ValidAfterEatPositions = checkCanEatPlacesFromCurrentCell(PlayerDirection.ePlayerDirection.Down, io_GameBoard, i_Computer);
            afterEatPositionIndex = randomEatPosition.Next(m_ValidAfterEatPositions.Count);
            Coordinate nextCompCoord = m_ValidAfterEatPositions[afterEatPositionIndex];
            computerMoveStr = getCompStringInput(m_CurrentCoordinate, nextCompCoord);

            return computerMoveStr;
        }

        private string getCompStringInput(Coordinate i_CurrCompCoord, Coordinate i_NextCompCoord)
        {
            StringBuilder computerMoveSB = new StringBuilder();
            const char k_InputBiggerSign = '>';

            computerMoveSB.Append(Coordinate.ConvertFromCoordToString(i_CurrCompCoord));
            computerMoveSB.Append(k_InputBiggerSign);
            computerMoveSB.Append(Coordinate.ConvertFromCoordToString(i_NextCompCoord));

            return computerMoveSB.ToString();
        }

        public string GetComputerInputContinueEating(List<Coordinate> i_NextValidEatingPositions, Coordinate i_CurrentCoord)
        {
            Random getRandomNextEatingPosition = new Random();
            int nextEatingPositionIndex = getRandomNextEatingPosition.Next(i_NextValidEatingPositions.Count);

            return getCompStringInput(i_CurrentCoord, i_NextValidEatingPositions[nextEatingPositionIndex]);
        }

        public string RequestValidComputerMove(GameBoard io_GameBoard, Player io_Computer)
        {
            Random rand = new Random();
            int range = io_Computer.CoordsOfSoldiers.Count;
            string compInputStr = string.Empty;

            if (io_Computer.CheckIfPlayerMustEat())
            {
                compInputStr = chooseComputerEatMove(io_Computer, io_GameBoard);
            }
            else
            {
                compInputStr = chooseComputerNoEatMove(io_Computer, io_GameBoard);
            }

            return compInputStr;
        }

        private string chooseComputerNoEatMove(Player i_Computer, GameBoard io_GameBoard)
        {
            int i;
            string computerMoveStr = string.Empty;
            bool foundCurrCoord = false;
            List<Coordinate> MovesAfterCurrPosition = new List<Coordinate>();
            Random randomNextMove = new Random();

            for (i = 0; i < i_Computer.CoordsOfSoldiers.Count && !foundCurrCoord; i++)
            {
                MovesAfterCurrPosition = getNextComputerStep(i_Computer.CoordsOfSoldiers[i], io_GameBoard, i_Computer.NextEatCoords);
                foundCurrCoord = MovesAfterCurrPosition.Count > 0;
            }

            if (foundCurrCoord)
            {
                int rangeOfNextMoves = MovesAfterCurrPosition.Count;
                Coordinate nextCompCoord = MovesAfterCurrPosition[randomNextMove.Next(rangeOfNextMoves)];

                computerMoveStr = getCompStringInput(i_Computer.CoordsOfSoldiers[i - 1], nextCompCoord);
            }

            return computerMoveStr;
        }

        private bool checkForValidComputerNextStep(
                     Coordinate i_CurrentComputerPosition,
                     GameBoard i_GameBoard,
                     Coordinate i_NextComputerCoord,
                     bool i_IsKing,
                     List<Coordinate> i_NextEatMoves)
        {
            bool isValidMove;

            m_CurrentCoordinate.CoordRow = i_CurrentComputerPosition.CoordRow;
            m_CurrentCoordinate.CoordCol = i_CurrentComputerPosition.CoordCol;
            m_NextCoordinate.CoordRow = i_NextComputerCoord.CoordRow;
            m_NextCoordinate.CoordCol = i_NextComputerCoord.CoordCol;
            isValidMove = isValidCurrentPosition(PlayerDirection.ePlayerDirection.Down, i_GameBoard) &&
                               isValidNextPosition(i_GameBoard);
            if (isValidMove)
            {
                isValidMove = checkValidDirection(
                              PlayerDirection.ePlayerDirection.Down,
                              i_CurrentComputerPosition,
                              i_NextComputerCoord,
                              i_IsKing,
                              i_NextEatMoves);
            }

            return isValidMove;
        }

        private List<Coordinate> getNextComputerStep(
                                 Coordinate i_CurrentComputerPosition,
                                 GameBoard i_GameBoard,
                                 List<Coordinate> i_NextEatMoves)
        {
            List<Coordinate> nextSteps = new List<Coordinate>();
            const int k_OneStepSkip = 1;
            bool isKingInCell = i_GameBoard.CheckIfCharInCellIsKing(i_CurrentComputerPosition);

            addToValidCompNextStepList(
            nextSteps,
            i_CurrentComputerPosition,
            i_GameBoard,
            new Coordinate(i_CurrentComputerPosition.CoordRow + k_OneStepSkip, i_CurrentComputerPosition.CoordCol + k_OneStepSkip),
            isKingInCell,
            i_NextEatMoves);
            addToValidCompNextStepList(
            nextSteps,
            i_CurrentComputerPosition,
            i_GameBoard,
            new Coordinate(i_CurrentComputerPosition.CoordRow + k_OneStepSkip, i_CurrentComputerPosition.CoordCol - k_OneStepSkip),
            isKingInCell,
            i_NextEatMoves);
            addToValidCompNextStepList(
            nextSteps,
            i_CurrentComputerPosition,
            i_GameBoard,
            new Coordinate(i_CurrentComputerPosition.CoordRow - k_OneStepSkip, i_CurrentComputerPosition.CoordCol + k_OneStepSkip),
            isKingInCell,
            i_NextEatMoves);
            addToValidCompNextStepList(
            nextSteps,
            i_CurrentComputerPosition,
            i_GameBoard,
            new Coordinate(i_CurrentComputerPosition.CoordRow - k_OneStepSkip, i_CurrentComputerPosition.CoordCol - k_OneStepSkip),
            isKingInCell,
            i_NextEatMoves);

            return nextSteps;
        }

        private void addToValidCompNextStepList(
                     List<Coordinate> io_NextSteps,
                     Coordinate i_CurrentComputerPosition,
                     GameBoard i_GameBoard,
                     Coordinate i_NextStepCoord,
                     bool i_IsKing,
                     List<Coordinate> i_NextEatMoves)
        {
            if (checkForValidComputerNextStep(i_CurrentComputerPosition, i_GameBoard, i_NextStepCoord, i_IsKing, i_NextEatMoves))
            {
                io_NextSteps.Add(new Coordinate(i_NextStepCoord.CoordRow, i_NextStepCoord.CoordCol));
            }
        }

        public bool CheckForAvailableMove(Player i_CurrPlayer, GameBoard i_GameBoard, List<Coordinate> io_NextEatCoords)
        {
            bool IsThereValidMoves = false;
            List<Coordinate> tempNextEatCoords = new List<Coordinate>();

            foreach (Coordinate playerPieceCoord in i_CurrPlayer.CoordsOfSoldiers)
            {
                m_CurrentCoordinate.CoordRow = playerPieceCoord.CoordRow;
                m_CurrentCoordinate.CoordCol = playerPieceCoord.CoordCol;
                List<Coordinate> afterEatFromCurrentCell = checkValidNextMoves(
                                                           i_CurrPlayer,
                                                           i_GameBoard,
                                                           playerPieceCoord,
                                                           ref IsThereValidMoves);

                updateListOfCellsWhereCanEatFrom(afterEatFromCurrentCell, playerPieceCoord, tempNextEatCoords);
            }

            io_NextEatCoords.Clear();
            GameCheckersUtils.CopyFromList(tempNextEatCoords, io_NextEatCoords);

            return IsThereValidMoves;
        }

        private void updateListOfCellsWhereCanEatFrom(
                     List<Coordinate> i_AfterEatFromCurrentCell,
                     Coordinate i_PlayerPieceCoord,
                     List<Coordinate> io_NextEatCoords)
        {
            bool canEatFromCell = i_AfterEatFromCurrentCell.Count > 0;

            if (canEatFromCell)
            {
                addPositionsToList(io_NextEatCoords, i_PlayerPieceCoord.CoordRow, i_PlayerPieceCoord.CoordCol);
            }
        }

        private List<Coordinate> checkCanEatPlacesFromCurrentCell(
                                 PlayerDirection.ePlayerDirection i_Direction,
                                 GameBoard i_GameBoard,
                                 Player i_CurrentPlayer)
        {
            bool tempValidNextStep = false;
            List<Coordinate> EatablePositions = new List<Coordinate>();
            bool isMySoldier = i_GameBoard.CheckIfMySoldierInTheCoordinate(i_Direction, m_CurrentCoordinate);

            if (isMySoldier)
            {
                EatablePositions = checkValidNextMoves(i_CurrentPlayer, i_GameBoard, m_CurrentCoordinate, ref tempValidNextStep);
            }

            return EatablePositions;
        }

        private List<Coordinate> checkValidNextMoves(
                                 Player i_CurrentPlayer,
                                 GameBoard i_GameBoard,
                                 Coordinate i_CurrentCoord,
                                 ref bool io_IsThereNextStepValid)
        {
            const int k_OneStepSkip = 1;
            const int k_TwoStepSkip = 2;
            bool isThereCurrStepValid = false;
            bool isKing = i_GameBoard.CheckIfCharInCellIsKing(i_CurrentCoord);
            List<Coordinate> eatablePositions = new List<Coordinate>();

            checkValidNextMoveAndUpdateAfterEatList(
            eatablePositions,
            i_CurrentPlayer,
            i_GameBoard,
            new Coordinate(i_CurrentCoord.CoordRow + k_OneStepSkip, i_CurrentCoord.CoordCol + k_OneStepSkip),
            new Coordinate(i_CurrentCoord.CoordRow + k_TwoStepSkip, i_CurrentCoord.CoordCol + k_TwoStepSkip),
            ref isThereCurrStepValid,
            isKing);
            checkValidNextMoveAndUpdateAfterEatList(
            eatablePositions,
            i_CurrentPlayer,
            i_GameBoard,
            new Coordinate(i_CurrentCoord.CoordRow + k_OneStepSkip, i_CurrentCoord.CoordCol - k_OneStepSkip),
            new Coordinate(i_CurrentCoord.CoordRow + k_TwoStepSkip, i_CurrentCoord.CoordCol - k_TwoStepSkip),
            ref isThereCurrStepValid,
            isKing);
            checkValidNextMoveAndUpdateAfterEatList(
            eatablePositions,
            i_CurrentPlayer,
            i_GameBoard,
            new Coordinate(i_CurrentCoord.CoordRow - k_OneStepSkip, i_CurrentCoord.CoordCol - k_OneStepSkip),
            new Coordinate(i_CurrentCoord.CoordRow - k_TwoStepSkip, i_CurrentCoord.CoordCol - k_TwoStepSkip),
            ref isThereCurrStepValid,
            isKing);
            checkValidNextMoveAndUpdateAfterEatList(
            eatablePositions,
            i_CurrentPlayer,
            i_GameBoard,
            new Coordinate(i_CurrentCoord.CoordRow - k_OneStepSkip, i_CurrentCoord.CoordCol + k_OneStepSkip),
            new Coordinate(i_CurrentCoord.CoordRow - k_TwoStepSkip, i_CurrentCoord.CoordCol + k_TwoStepSkip),
            ref isThereCurrStepValid,
            isKing);
            io_IsThereNextStepValid = io_IsThereNextStepValid || isThereCurrStepValid;

            return eatablePositions;
        }

        private bool checkIfThereValidMove(bool i_IsNextStepValid, List<Coordinate> i_EatablePositions)
        {
            bool isThereValidMove = false;
            isThereValidMove = i_IsNextStepValid || i_EatablePositions.Count > 0;

            return isThereValidMove;
        }

        private void checkValidNextMoveAndUpdateAfterEatList(
                     List<Coordinate> io_EatablePositions,
                     Player i_CurrentPlayer,
                     GameBoard i_GameBoard,
                     Coordinate i_EnemyCoord,
                     Coordinate i_NextStepCoord,
                     ref bool o_IsNextStepValid,
                     bool i_IsKing)
        {
            bool isCurrEnemyStepValid = false;
            ////check also boundries inside
            bool isEnemy = i_GameBoard.CheckIfEnemyIsAtCoordinate(i_CurrentPlayer.Direction, i_EnemyCoord, ref isCurrEnemyStepValid);

            if (isEnemy || isCurrEnemyStepValid)
            {
                bool isValidDirectionForMe = checkValidDirectionAfterAndBeforeEat(
                                             i_CurrentPlayer,
                                             i_IsKing,
                                             i_EnemyCoord,
                                             i_NextStepCoord);
                if (isValidDirectionForMe)
                {
                    updateAfterEatPositionsIfEnemyEatable(
                    isEnemy,
                    i_GameBoard,
                    i_NextStepCoord,
                    io_EatablePositions);
                }
                else
                {
                    isCurrEnemyStepValid = false;
                }
            }

            o_IsNextStepValid = isCurrEnemyStepValid || o_IsNextStepValid;
            ////if the next step isnt valid because i can eat - the eating is valid move
            o_IsNextStepValid = o_IsNextStepValid || checkIfThereValidMove(o_IsNextStepValid, io_EatablePositions);
        }

        private bool checkValidDirectionAfterAndBeforeEat(
                     Player i_CurrentPlayer,
                     bool i_IsKing,
                     Coordinate i_EnemyCoord,
                     Coordinate i_NextStepCoord)
        {
            bool thereArePlacesToEatFrom = i_CurrentPlayer.NextEatCoords.Count > 0;
            Coordinate checkForValidCoord;

            if (thereArePlacesToEatFrom)
            {
                checkForValidCoord = new Coordinate(i_NextStepCoord.CoordRow, i_NextStepCoord.CoordCol);
            }
            else
            {
                checkForValidCoord = new Coordinate(i_EnemyCoord.CoordRow, i_EnemyCoord.CoordCol);
            }

            return checkValidDirection(
                   i_CurrentPlayer.Direction,
                   m_CurrentCoordinate,
                   checkForValidCoord,
                   i_IsKing,
                   i_CurrentPlayer.NextEatCoords);
        }

        private void updateAfterEatPositionsIfEnemyEatable(
                     bool i_IsEnemy,
                     GameBoard i_GameBoard,
                     Coordinate i_NextStepCoord,
                     List<Coordinate> io_EatablePositions)
        {
            bool isValidToEatEnemy;

            if (i_IsEnemy)
            {
                isValidToEatEnemy = i_GameBoard.CheckIfMoveInBounds(i_NextStepCoord) &&
                                         !i_GameBoard.CheckIfOccupied(i_NextStepCoord);

                if (isValidToEatEnemy)
                {
                    addPositionsToList(io_EatablePositions, i_NextStepCoord.CoordRow, i_NextStepCoord.CoordCol);
                }
            }
        }

        private void addPositionsToList(List<Coordinate> o_EatablePositions, int i_EatEnemyRow, int i_EatEnemyCol)
        {
            Coordinate newCoordinate = new Coordinate(k_InitCoordinate, k_InitCoordinate);

            newCoordinate.CoordCol = i_EatEnemyCol;
            newCoordinate.CoordRow = i_EatEnemyRow;
            o_EatablePositions.Add(newCoordinate);
        }

        private bool isValidCurrentPosition(PlayerDirection.ePlayerDirection i_Direction, GameBoard i_GameBoard)
        {
            bool validPosition = i_GameBoard.CheckIfMoveInBounds(m_CurrentCoordinate) &&
                                 i_GameBoard.CheckIfMySoldierInTheCoordinate(i_Direction, m_CurrentCoordinate);

            return validPosition;
        }

        private bool isValidNextPosition(GameBoard i_GameBoard)
        {
            bool validPosition = i_GameBoard.CheckIfMoveInBounds(m_NextCoordinate) &&
                                 !i_GameBoard.CheckIfOccupied(m_NextCoordinate);

            return validPosition;
        }

        public List<Coordinate> MakeMove(GameBoard io_GameBoard, ref bool o_EatEnemy, Player i_CurrPlayer)
        {
            char playerFigureInCell = 
                io_GameBoard.BoardGameMatrix[m_CurrentCoordinate.CoordRow, m_CurrentCoordinate.CoordCol].CharInCell;
            List<Coordinate> afterEatPositions = new List<Coordinate>();
            GameCheckersUtils.CopyFromList(m_ValidAfterEatPositions, afterEatPositions);
            bool thereAreAfterEatPositions = m_ValidAfterEatPositions.Count > 0;

            o_EatEnemy = false;
            if (thereAreAfterEatPositions)
            {
                o_EatEnemy = checkIfNextMoveIsEating(m_ValidAfterEatPositions);
            }

            if (!thereAreAfterEatPositions || (thereAreAfterEatPositions && o_EatEnemy))
            {
                updateBoardAfterEating(ref playerFigureInCell, i_CurrPlayer.Direction, io_GameBoard);
            }

            if (o_EatEnemy)
            {
                deleteEnemyAfterEating(i_CurrPlayer, io_GameBoard);
                m_CurrentCoordinate.CoordRow = m_NextCoordinate.CoordRow;
                m_CurrentCoordinate.CoordCol = m_NextCoordinate.CoordCol;
                afterEatPositions = checkCanEatPlacesFromCurrentCell(i_CurrPlayer.Direction, io_GameBoard, i_CurrPlayer);
            }

            return afterEatPositions;
        }

        private void updateBoardAfterEating(ref char io_PlayerFigure, PlayerDirection.ePlayerDirection i_Direction, GameBoard io_GameBoard)
        {
            bool isBecomeKing;
            io_GameBoard.BoardGameMatrix[m_CurrentCoordinate.CoordRow, m_CurrentCoordinate.CoordCol].RemoveCell();
            isBecomeKing = io_GameBoard.CheckIfBecomeKingAndUpdatePlayerFigure(ref io_PlayerFigure, i_Direction, m_NextCoordinate);
            io_GameBoard.BoardGameMatrix[m_NextCoordinate.CoordRow, m_NextCoordinate.CoordCol].CharInCell = io_PlayerFigure;
            PieceMoved(isBecomeKing);
        }

        private void deleteEnemyAfterEating(Player i_CurrPlayer, GameBoard io_GameBoard)
        {
            Coordinate enemyCoord = new Coordinate(k_InitCoordinate, k_InitCoordinate);
            getEnemyCoord(i_CurrPlayer, io_GameBoard, enemyCoord);
            io_GameBoard.BoardGameMatrix[enemyCoord.CoordRow, enemyCoord.CoordCol].RemoveCell();
        }

        private void getEnemyCoord(Player i_CurrPlayer, GameBoard i_GameBoard, Coordinate o_EnemyCoord)
        {
            const int k_OneStepSkip = 1;
            const int k_TwoStepSkip = 2;
            bool isEnemy = false;
            isEnemy = updateEnemyRowCol(
                      isEnemy,
                      new Coordinate(m_CurrentCoordinate.CoordRow + k_OneStepSkip, m_CurrentCoordinate.CoordCol + k_OneStepSkip),
                      i_GameBoard,
                      i_CurrPlayer,
                      o_EnemyCoord,
                      new Coordinate(m_CurrentCoordinate.CoordRow + k_TwoStepSkip, m_CurrentCoordinate.CoordCol + k_TwoStepSkip));
            isEnemy = updateEnemyRowCol(
                      isEnemy,
                      new Coordinate(m_CurrentCoordinate.CoordRow + k_OneStepSkip, m_CurrentCoordinate.CoordCol - k_OneStepSkip),
                      i_GameBoard,
                      i_CurrPlayer,
                      o_EnemyCoord,
                      new Coordinate(m_CurrentCoordinate.CoordRow + k_TwoStepSkip, m_CurrentCoordinate.CoordCol - k_TwoStepSkip));
            isEnemy = updateEnemyRowCol(
                      isEnemy,
                      new Coordinate(m_CurrentCoordinate.CoordRow - k_OneStepSkip, m_CurrentCoordinate.CoordCol + k_OneStepSkip),
                      i_GameBoard,
                      i_CurrPlayer,
                      o_EnemyCoord,
                      new Coordinate(m_CurrentCoordinate.CoordRow - k_TwoStepSkip, m_CurrentCoordinate.CoordCol + k_TwoStepSkip));
            isEnemy = updateEnemyRowCol(
                      isEnemy,
                      new Coordinate(m_CurrentCoordinate.CoordRow - k_OneStepSkip, m_CurrentCoordinate.CoordCol - k_OneStepSkip),
                      i_GameBoard,
                      i_CurrPlayer,
                      o_EnemyCoord,
                      new Coordinate(m_CurrentCoordinate.CoordRow - k_TwoStepSkip, m_CurrentCoordinate.CoordCol - k_TwoStepSkip));
        }

        private bool updateEnemyRowCol(
                     bool i_IsEnemy,
                     Coordinate i_EnemyCoord,
                     GameBoard i_GameBoard,
                     Player i_CurrPlayer,
                     Coordinate o_EnemyCoord,
                     Coordinate i_NextStepCoord)
        {
            bool temp = false;
            bool isEnemy = i_IsEnemy;

            if (!i_IsEnemy)
            {
                isEnemy = i_GameBoard.CheckIfEnemyIsAtCoordinate(i_CurrPlayer.Direction, i_EnemyCoord, ref temp) &&
                                       i_NextStepCoord.CoordRow == m_NextCoordinate.CoordRow &&
                                       i_NextStepCoord.CoordCol == m_NextCoordinate.CoordCol;

                if (isEnemy)
                {
                    o_EnemyCoord.CoordRow = i_EnemyCoord.CoordRow;
                    o_EnemyCoord.CoordCol = i_EnemyCoord.CoordCol;
                }
            }

            return isEnemy;
        }

        private bool checkIfNextMoveIsEating(List<Coordinate> i_AfterEatPositions)
        {
            bool afterEatPosition = false;

            for (int i = 0; i < i_AfterEatPositions.Count && !afterEatPosition; i++)
            {
                afterEatPosition = i_AfterEatPositions[i].CoordRow == m_NextCoordinate.CoordRow &&
                                   i_AfterEatPositions[i].CoordCol == m_NextCoordinate.CoordCol;
            }

            return afterEatPosition;
        }

        public bool CheckValidMove(Player i_CurrPlayer, GameBoard i_GameBoard, string i_UserStringInput)
        {
            bool isValidMove = false;
            SetMoveCoordinates(i_UserStringInput);
            bool isKing = i_GameBoard.CheckIfCharInCellIsKing(m_CurrentCoordinate);
            isValidMove = isValidCurrentPosition(i_CurrPlayer.Direction, i_GameBoard) && isValidNextPosition(i_GameBoard);

            if (isValidMove)
            {
                m_ValidAfterEatPositions = checkCanEatPlacesFromCurrentCell(i_CurrPlayer.Direction, i_GameBoard, i_CurrPlayer);
                isValidMove = checkValidDirection(
                              i_CurrPlayer.Direction,
                              m_CurrentCoordinate,
                              m_NextCoordinate,
                              isKing,
                              i_CurrPlayer.NextEatCoords);
            }

            return isValidMove;
        }

        public void SetMoveCoordinates(string i_UserStringInput)
        {
            const int k_CurrCoordStartingIndex = 0;
            const int k_NextCoordStartingIndex = 3;

            m_CurrentCoordinate = Coordinate.FromStringToCoordinate(i_UserStringInput, k_CurrCoordStartingIndex);
            m_NextCoordinate = Coordinate.FromStringToCoordinate(i_UserStringInput, k_NextCoordStartingIndex);
        }

        private bool checkValidDirection(
                     PlayerDirection.ePlayerDirection i_Direction,
                     Coordinate i_CurrentCoord,
                     Coordinate i_NextCoord,
                     bool i_IsKing,
                     List<Coordinate> i_NextEatMoves)
        {
            const int k_NoEatableCoords = 0;
            bool isValid = false;
            bool isUp = i_Direction == PlayerDirection.ePlayerDirection.Up;
            bool isTherePlacesToEat = i_NextEatMoves.Count > k_NoEatableCoords;

            if (isTherePlacesToEat)
            {
                isValid = checkValidEatDirection(isUp, i_CurrentCoord, i_NextCoord, i_IsKing);
            }
            else
            {
                isValid = checkValidNoEatDirection(isUp, i_CurrentCoord, i_NextCoord, i_IsKing);
            }

            return isValid;
        }

        private bool checkValidEatDirection(bool i_IsUp, Coordinate i_CurrCoord, Coordinate i_NextCoord, bool i_IsKing)
        {
            const int k_NumberOfEatStepsToSkip = 2;

            return checkValidDirectionBySteps(i_IsUp, i_CurrCoord, i_NextCoord, i_IsKing, k_NumberOfEatStepsToSkip);
        }

        private bool checkValidNoEatDirection(bool i_IsUp, Coordinate i_CurrCoord, Coordinate i_NextCoord, bool i_IsKing)
        {
            const int k_NumberOfNoEatStepsToSkip = 1;

            return checkValidDirectionBySteps(i_IsUp, i_CurrCoord, i_NextCoord, i_IsKing, k_NumberOfNoEatStepsToSkip);
        }

        private bool checkValidDirectionBySteps(
                     bool i_IsUp,
                     Coordinate i_CurrCoord,
                     Coordinate i_NextCoord,
                     bool i_IsKing,
                     int i_StepsNumber)
        {
            bool v_MoveForward = true;
            bool isValidDir = false;
            bool isValidCol = i_NextCoord.CoordCol == i_CurrCoord.CoordCol - i_StepsNumber
                || i_NextCoord.CoordCol == i_CurrCoord.CoordCol + i_StepsNumber;

            isValidDir = isMovingForwardOrBackward(isValidCol, i_IsUp, i_CurrCoord.CoordRow, i_NextCoord.CoordRow, i_StepsNumber, v_MoveForward);
            if (i_IsKing && !isValidDir)
            {
                isValidDir = isMovingForwardOrBackward(isValidCol, i_IsUp, i_CurrCoord.CoordRow, i_NextCoord.CoordRow, i_StepsNumber, !v_MoveForward);
            }

            return isValidDir;
        }

        private bool isMovingForwardOrBackward(bool i_ValidCol, bool i_IsUp, int i_CurrentRow, int i_NextRow, int i_StepsNumber, bool i_IsCheckForward)
        {
            bool validDirection = false;

            if (i_IsUp)
            {
                if (i_IsCheckForward)
                {
                    validDirection = i_NextRow == i_CurrentRow - i_StepsNumber && i_ValidCol;
                }
                else
                {
                    validDirection = i_NextRow == i_CurrentRow + i_StepsNumber && i_ValidCol;
                }
            }
            else
            {
                if (i_IsCheckForward)
                {
                    validDirection = i_NextRow == i_CurrentRow + i_StepsNumber && i_ValidCol;
                }
                else
                {
                    validDirection = i_NextRow == i_CurrentRow - i_StepsNumber && i_ValidCol;
                }
            }

            return validDirection;
        }

        protected virtual void PieceMoved(bool isBecomeKing)
        {
            if(PieceHasMoved != null)
            {
                PieceMovedArgs movingCoords = new PieceMovedArgs(m_CurrentCoordinate, m_NextCoordinate, isBecomeKing);
                PieceHasMoved(this, movingCoords); 
            }
        }
    }
}