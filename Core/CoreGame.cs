using System;

namespace Core
{
    public abstract class CoreGame
    {
        protected readonly CardStack Stack;
		protected Player HumanPlayer { get; }
		protected Player ComputerPlayer { get; }
		protected Dice GameDiceOne { get; }
		protected Dice GameDiceTwo { get; }

        protected CoreGame(string playerName)
        {
            HumanPlayer = new Player(playerName);
            ComputerPlayer = new Player("IA");
            Stack = new CardStack();
			GameDiceOne = new Dice();
			GameDiceTwo = new Dice();
		}

		public abstract void Run();

		public abstract void HumanTurn();
		
		public abstract void ComputerTurn();

		public abstract bool IsEndGame();

		protected bool IsPlayerWin()
		{
			return HumanPlayer.Monuments[0].Build && HumanPlayer.Monuments[1].Build && HumanPlayer.Monuments[2].Build && HumanPlayer.Monuments[3].Build;
			//return HumanPlayer.Wallet >= 20;
		}

		protected bool IsComputerWin()
		{
			return ComputerPlayer.Monuments[0].Build && ComputerPlayer.Monuments[1].Build && ComputerPlayer.Monuments[2].Build && ComputerPlayer.Monuments[3].Build;
			//return ComputerPlayer.Wallet >= 20;
		}
    }
}