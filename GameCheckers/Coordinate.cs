using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCheckers
{
    public class Coordinate
    {
        private int m_Row;
        private int m_Column;

        public static bool AreTwoCoordsEqual(Coordinate i_FirstCoord, Coordinate i_SecondCoord)
        {
            bool areCoordsEqual = false;

            areCoordsEqual = (i_FirstCoord.m_Column == i_SecondCoord.m_Column) && (i_FirstCoord.m_Row == i_SecondCoord.m_Row);
            return areCoordsEqual;
        }

        public static Coordinate FromStringToCoordinate(string i_UserMove, int i_IndexToStart)
        {
            Coordinate newCoordinate = new Coordinate(
                                            i_UserMove[i_IndexToStart + 1] - 'a' + 1,
                                            i_UserMove[i_IndexToStart] - 'A' + 1);

            return newCoordinate;
        }

        public static string ConvertFromCoordToString(Coordinate i_CoordToConvert)
        {
            StringBuilder convertedCoordToSB = new StringBuilder();
            const char k_CapsAsciiNum = 'A';
            const char k_NotCapsAsciiNum = 'a';

            convertedCoordToSB.Append((char)(k_CapsAsciiNum + i_CoordToConvert.m_Column - 1));
            convertedCoordToSB.Append((char)(k_NotCapsAsciiNum + i_CoordToConvert.m_Row - 1));

            return convertedCoordToSB.ToString();
        }

        public int CoordRow
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int CoordCol
        {
            get { return m_Column; }
            set { m_Column = value; }
        }

        public Coordinate(int i_CurrentRow, int i_CurrentCol)
        {
            m_Column = i_CurrentCol;
            m_Row = i_CurrentRow;
        }

        public void ResetCoordinate()
        {
            m_Column = 0;
            m_Row = 0;
        }
    }
}
