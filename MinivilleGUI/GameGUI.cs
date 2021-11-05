#nullable enable
using System.Collections.Generic;
using Core;

namespace MinivilleGUI
{
	public class GameGUI  {
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


		public void IsEndgame() {
		//	if()
		}


	}
}