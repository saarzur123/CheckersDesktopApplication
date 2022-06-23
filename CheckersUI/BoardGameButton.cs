using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameCheckers;

namespace CheckersUI
{
    public class BoardGameButton : Button
    {
        private Coordinate m_Coordinate;

        public Coordinate CurrCoordinate
        {
            get { return m_Coordinate; }
            set { m_Coordinate = value; }
        }
    }
}