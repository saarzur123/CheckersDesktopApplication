using System;
using System.Collections.Generic;

namespace CheckersUI
{
    internal class TimeComputerMoveArgs : EventArgs
    {
        public List<bool> m_ValidMovesForPlayerArg { get; set; }

        public TimeComputerMoveArgs(List<bool> i_ValidMovesForPlayers)
        {
            m_ValidMovesForPlayerArg = i_ValidMovesForPlayers;
        }
    }
}
