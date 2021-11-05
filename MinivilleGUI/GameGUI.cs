#nullable enable
using System.Collections.Generic;
using Core;

namespace MinivilleGUI
{
	public class GameGUI {
		public int DiceValue;

		private bool _asPlayed = false;

		public Player Player;
		public Player Computer;
		
		public GameGUI()
		{
			Player = new Player("Player");
			Computer = new Player("Computer");
		}
		public Card? ComputerTurn() {
			if (Computer.Monuments[0].Build) {
				// 2 Dés
			}
			else {
				// 1 Dés
			}

			return null;
		}


	}
}