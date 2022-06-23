using System;

namespace GameCheckers
{
    public class PieceMovedArgs : EventArgs
    {
        private readonly Coordinate r_PieceStartCoordinate;
        private readonly Coordinate r_PieceEndCoordinate;
        private readonly bool r_IsBecomeKing;
        private readonly bool r_IsPerformedEat;

        public PieceMovedArgs(Coordinate i_StartCoord, Coordinate i_EndCoord, bool i_IsBecomeKing)
        {
            const int k_EatingTileOffset = 2;

            r_PieceStartCoordinate = new Coordinate(i_StartCoord.CoordRow, i_StartCoord.CoordCol);
            r_PieceEndCoordinate = new Coordinate(i_EndCoord.CoordRow, i_EndCoord.CoordCol);
            r_IsBecomeKing = i_IsBecomeKing;
            if(Math.Abs(i_StartCoord.CoordRow - i_EndCoord.CoordRow) == k_EatingTileOffset)
            {
                r_IsPerformedEat = true;
            }
        }

        public bool IsPerformedEat
        {
            get { return r_IsPerformedEat; }
        }

        public bool IsBecomeKing
        {
            get { return r_IsBecomeKing; }
        }

        public Coordinate PieceStartCoordinate
        {
            get { return r_PieceStartCoordinate; }
        }

        public Coordinate PieceEndCoordinate
        {
            get { return r_PieceEndCoordinate; }
        }
    }
}
