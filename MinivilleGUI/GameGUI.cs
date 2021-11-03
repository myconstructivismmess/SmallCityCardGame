using System.Collections.Generic;
using Core;

namespace MinivilleGUI
{
	public class GameGUI
	{
		public Stack<Transaction> Transactions = new Stack<Transaction>();
		public Dice GameDiceOne { get; }
		public Dice GameDiceTwo { get; }

		private bool _asPlayed = false;

		public Player Player;
		public Player Computer;
		
		public GameGUI()
		{
			GameDiceOne = new Dice();
			GameDiceTwo = new Dice();
			Player = new Player("Player");
			Computer = new Player("Computer");
		}

		public void Update()
		{
			if (!_asPlayed) return;
			_asPlayed = false;
			
			//Player.Monuments[0].
		}
	}
}