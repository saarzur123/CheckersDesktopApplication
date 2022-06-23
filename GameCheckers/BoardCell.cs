namespace GameCheckers
{
    public class BoardCell
    {
        public const char k_OFigure = 'O';
        public const char k_XFigure = 'X';
        public const char k_UFigure = 'U';
        public const char k_KFigure = 'K';

        private readonly Coordinate m_CellCoord;
        private char m_CharInCell = ' ';

        public BoardCell(int i_BoardRow, int i_BoardCol)
        {
            m_CellCoord = new Coordinate(i_BoardRow, i_BoardCol);
        }

        public Coordinate CellCoord
        {
            get { return m_CellCoord; }
        }

        public char CharInCell
        {
            get { return m_CharInCell; }
            set { m_CharInCell = value; }
        }

        public void RemoveCell()
        {
            m_CharInCell = ' ';
        }
    }
}
