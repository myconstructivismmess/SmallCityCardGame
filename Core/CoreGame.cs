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

		protected bool IsPlayerWin()
		{
			//return HumanPlayer.Monuments[0].Build == true && HumanPlayer.Monuments[1].Build == true && HumanPlayer.Monuments[2].Build == true && HumanPlayer.Monuments[3].Build == true;
			return HumanPlayer.Wallet >= 20;
		}

		protected bool IsComputerWin()
		{
			//return ComputerPlayer.Monuments[0].Build == true && ComputerPlayer.Monuments[1].Build == true && ComputerPlayer.Monuments[2].Build == true && ComputerPlayer.Monuments[3].Build == true;
			return ComputerPlayer.Wallet >= 20;
		}
    }
}