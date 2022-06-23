using System.Collections.Generic;

namespace GameCheckers
{
    public class Player
    {
        private readonly string r_PlayerName;
        private readonly PlayerDirection.ePlayerDirection r_Direction;
        private readonly char r_PlayerPieceSymbol;
        private readonly char r_PlayerKingSymbol;
        private readonly bool r_isHumanPlayer = false;
        private int m_PlayerPoints = 0;
        private List<Coordinate> m_CoordsOfSoldiers = new List<Coordinate>();
        private List<Coordinate> m_NextEatCoords = new List<Coordinate>();

        public Player(
            PlayerDirection.ePlayerDirection i_PlayerDirection,
            char i_PlayerPiece,
            char i_PlayerKing,
            string i_PlayerName,
            bool i_isHumanPlayer,
            GameBoard i_gameBoard)
        {
            r_PlayerKingSymbol = i_PlayerKing;
            r_Direction = i_PlayerDirection;
            r_PlayerPieceSymbol = i_PlayerPiece;
            r_PlayerName = i_PlayerName;
            r_isHumanPlayer = i_isHumanPlayer;
            UpdateCoordsOfSoldiersList(i_gameBoard);
        }

        public static int GetEnemyPlayerIndex(int i_CurrPlayerIndex)
        {
            const int k_PlayerOneIndex = 0;
            const int k_PlayerTwoIndex = 1;
            int enemyPlayerIndex = k_PlayerOneIndex;

            if (i_CurrPlayerIndex == k_PlayerOneIndex)
            {
                enemyPlayerIndex = k_PlayerTwoIndex;
            }

            return enemyPlayerIndex;
        }

        public int calcNumOfPlayerSoldiers(GameBoard i_GameBoard)
        {
            const int k_KingSoldierWorth = 4;
            int numOfPlayerSoldiers = 0;

            foreach (Coordinate soldierCoord in m_CoordsOfSoldiers)
            {
                if (i_GameBoard.BoardGameMatrix[soldierCoord.CoordRow,
                    soldierCoord.CoordCol].CharInCell == r_PlayerPieceSymbol)
                {
                    numOfPlayerSoldiers++;
                }
                else if (i_GameBoard.BoardGameMatrix[soldierCoord.CoordRow,
                    soldierCoord.CoordCol].CharInCell == r_PlayerKingSymbol)
                {
                    numOfPlayerSoldiers += k_KingSoldierWorth;
                }
            }

            return numOfPlayerSoldiers;
        }

        public void UpdateCoordsOfSoldiersList(GameBoard i_GameBoard)
        {
            m_CoordsOfSoldiers.Clear();
            foreach (BoardCell cellInBoard in i_GameBoard.BoardGameMatrix)
            {
                if (cellInBoard.CharInCell == r_PlayerPieceSymbol || cellInBoard.CharInCell == r_PlayerKingSymbol)
                {
                    m_CoordsOfSoldiers.Add(cellInBoard.CellCoord);
                }
            }
        }

        internal bool CheckIfPlayerMustEat()
        {
            bool isComputerMustEat = false;
            isComputerMustEat = NextEatCoords.Count > 0;

            return isComputerMustEat;
        }

        public List<Coordinate> NextEatCoords
        {
            get { return m_NextEatCoords; }
        }

        public List<Coordinate> CoordsOfSoldiers
        {
            get { return m_CoordsOfSoldiers; }
        }

        public bool isHumanPlayer
        {
            get { return r_isHumanPlayer; }
        }

        public PlayerDirection.ePlayerDirection Direction
        {
            get { return r_Direction; }
        }

        public char PlayerPieceSymbol
        {
            get { return r_PlayerPieceSymbol; }
        }

        public string PlayerName
        {
            get { return r_PlayerName; }
        }

        public int PlayerPoints
        {
            get { return m_PlayerPoints; }
            set { m_PlayerPoints = value; }
        }
    }
}