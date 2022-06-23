using System;
using System.Collections.Generic;

namespace GameCheckers
{
    internal class GameCheckersUtils
    {
        internal static void CopyFromList(List<Coordinate> i_Source, List<Coordinate> io_Dest)
        {
            bool canCopy = i_Source.Count > 0;

            if (canCopy)
            {
                foreach (Coordinate sourceCoord in i_Source)
                {
                    io_Dest.Add(new Coordinate(sourceCoord.CoordRow, sourceCoord.CoordCol));
                }
            }
        }
    }
}
