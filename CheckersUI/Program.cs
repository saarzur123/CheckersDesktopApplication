using System;

namespace CheckersUI
{
    public class Program
    {
        public static void Main()
        {
            CheckersUIManager CheckersUIManager = new CheckersUIManager();
            CheckersUIManager.StartGameIfNoExitSettings();
        }
    }
}
