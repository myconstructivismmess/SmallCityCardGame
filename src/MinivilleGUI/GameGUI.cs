#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace MinivilleGUI
{
	public class GameGUI {
		public int DiceValue;

		public bool PlayerTurn = true;

		public Player Player;
		public Player Computer;

		public CardStack CardStack;
		
		public GameGUI()
		{
			Player = new Player("Player");
			Computer = new Player("Computer");
			CardStack = new CardStack();
		}


		public bool IsPlayerWin()
		{
			return Player.Monuments.All(x => x.Build);
			//return HumanPlayer.Wallet >= 20;
		}
		
		public bool IsComputerWin()
		{
			return Computer.Monuments.All(x => x.Build);
			//return HumanPlayer.Wallet >= 20;
		}


	}
}