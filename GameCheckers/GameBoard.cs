using System;

namespace GameCheckers
{
    public class GameBoard
    {
        private readonly short r_RowBoundry;
        private readonly short r_ColBoundry;
        private BoardCell[,] m_BoardGameMatrix;

        private static short convertStrToBoardSize(string i_UserChosenBoardSize)
        {
            const short k_SixBoardSize = 6;
            const short k_EightBoardSize = 8;
            const short k_TenBoardSize = 10;
            short chosenBoardSize = k_SixBoardSize;
            const string k_BoardSizeEight = "8";
            const string k_BoardSizeTen = "10";

            if (i_UserChosenBoardSize == k_BoardSizeEight)
            {
                chosenBoardSize = k_EightBoardSize;
            }
            else if (i_UserChosenBoardSize == k_BoardSizeTen)
            {
                chosenBoardSize = k_TenBoardSize;
            }

            return chosenBoardSize;
        }

        public GameBoard(string i_BoardSizeStr)
        {
            short boardSize = convertStrToBoardSize(i_BoardSizeStr);

            ++boardSize;
            r_RowBoundry = boardSize;
            r_ColBoundry = boardSize;
            m_BoardGameMatrix = new BoardCell[r_ColBoundry, r_RowBoundry];
            for (int i = 0; i < r_RowBoundry; i++)
            {
                for (int j = 0; j < r_ColBoundry; j++)
                {
                    m_BoardGameMatrix[i, j] = new BoardCell(i, j);
                }
            }

            InitBoardGameMatrix();
        }

        internal bool CheckIfOccupied(Coordinate i_CheckedCoord)
        {
            char charInCell = BoardGameMatrix[i_CheckedCoord.CoordRow, i_CheckedCoord.CoordCol].CharInCell;
            bool isOccupied = charInCell != ' ';

            return isOccupied;
        }

        internal bool CheckIfCharInCellIsKing(Coordinate i_CurrentCoord)
        {
            char charInCell = BoardGameMatrix[i_CurrentCoord.CoordRow, i_CurrentCoord.CoordCol].CharInCell;
            bool isKing = charInCell == BoardCell.k_UFigure || charInCell == BoardCell.k_KFigure;

            return isKing;
        }

        internal bool CheckIfMoveInBounds(Coordinate i_CheckedCoord)
        {
            bool isValid = false;
            const int k_BoardUpperAndLeftLimit = 0;

            isValid = ColBoundry > i_CheckedCoord.CoordCol && i_CheckedCoord.CoordCol > k_BoardUpperAndLeftLimit
                   && RowBoundry > i_CheckedCoord.CoordRow && i_CheckedCoord.CoordRow > k_BoardUpperAndLeftLimit;

            return isValid;
        }

        internal bool CheckIfBecomeKingAndUpdatePlayerFigure(
                     ref char io_CurrFigureInCell,
                     PlayerDirection.ePlayerDirection i_Direction,
                     Coordinate i_NextCoord)
        {
            bool isBecomeKing = false;
            char kingFigure;
            bool isUp = i_Direction == PlayerDirection.ePlayerDirection.Up;

            if (isUp)
            {
                isBecomeKing = io_CurrFigureInCell == BoardCell.k_XFigure && i_NextCoord.CoordRow == 1;
                kingFigure = BoardCell.k_KFigure;
            }
            else
            {
                isBecomeKing = io_CurrFigureInCell == BoardCell.k_OFigure && i_NextCoord.CoordRow == RowBoundry - 1;
                kingFigure = BoardCell.k_UFigure;
            }

            if (isBecomeKing)
            {
                io_CurrFigureInCell = kingFigure;
            }

            return isBecomeKing;
        }

        internal bool CheckIfEnemyIsAtCoordinate(
                     PlayerDirection.ePlayerDirection i_Direction,
                     Coordinate i_EnemyCoord,
                     ref bool o_IscurrStepValid)
        {
            bool isEnemy = false;
            bool isInBoundries = CheckIfMoveInBounds(i_EnemyCoord);

            if (isInBoundries)
            {
                bool isOccupiedCell = CheckIfOccupied(i_EnemyCoord);

                if (isOccupiedCell)
                {
                    bool isMySoldier = CheckIfMySoldierInTheCoordinate(i_Direction, i_EnemyCoord);

                    if (!isMySoldier)
                    {
                        isEnemy = true;
                    }
                }
                else
                {
                    o_IscurrStepValid = true;
                }
            }

            return isEnemy;
        }

        internal bool CheckIfMySoldierInTheCoordinate(PlayerDirection.ePlayerDirection i_Direction, Coordinate i_Coordinate)
        {
            char currentCharInCell = BoardGameMatrix[i_Coordinate.CoordRow, i_Coordinate.CoordCol].CharInCell;
            bool isMySoldier = false;
            bool isUp = i_Direction == PlayerDirection.ePlayerDirection.Up;

            if (isUp)
            {
                isMySoldier = currentCharInCell == BoardCell.k_KFigure || currentCharInCell == BoardCell.k_XFigure;
            }
            else
            {
                isMySoldier = currentCharInCell == BoardCell.k_OFigure || currentCharInCell == BoardCell.k_UFigure;
            }

            return isMySoldier;
        }

        public BoardCell[,] BoardGameMatrix
        {
            get { return m_BoardGameMatrix; }
        }

        public short RowBoundry
        {
            get { return r_RowBoundry; }
        }

        public short ColBoundry
        {
            get { return r_ColBoundry; }
        }

        public void InitBoardGameMatrix()
        {
            int numberOfRowsForPlayer = (r_RowBoundry - 1) / 2;
            const int k_PlayerOneOffsetFromMid = 1;
            const int k_PlayerTwoOffsetFromMid = 2;
            const int k_firstRowCol = 0;

            emptyBoardCells();
            for (int i = 1; i < r_ColBoundry; i++)
            {
                m_BoardGameMatrix[k_firstRowCol, i].CharInCell = (char)('A' + i - 1);
            }

            for (int i = 1; i < r_RowBoundry; i++)
            {
                m_BoardGameMatrix[i, k_firstRowCol].CharInCell = (char)('a' + i - 1);
            }

            initPlayerSoldiers(k_PlayerOneOffsetFromMid, numberOfRowsForPlayer, BoardCell.k_OFigure);
            initPlayerSoldiers(numberOfRowsForPlayer + k_PlayerTwoOffsetFromMid, r_RowBoundry, BoardCell.k_XFigure);
        }

        private void emptyBoardCells()
        {
            foreach (BoardCell currentCell in m_BoardGameMatrix)
            {
                currentCell.RemoveCell();
            }
        }

        private void initPlayerSoldiers(int i_StartRow, int i_EndRow, char i_SoldierFigure)
        {
            for (int row = i_StartRow; row < i_EndRow; row++)
            {
                bool isOddRow = row % 2 == 1;

                for (int col = 1; col < r_ColBoundry; col++)
                {
                    bool isOddCol = col % 2 == 1;

                    if (isOddRow && !isOddCol)
                    {
                        m_BoardGameMatrix[row, col].CharInCell = i_SoldierFigure;
                    }
                    else if (!isOddRow && isOddCol)
                    {
                        m_BoardGameMatrix[row, col].CharInCell = i_SoldierFigure;
                    }
                }
            }
        }
    }
}
