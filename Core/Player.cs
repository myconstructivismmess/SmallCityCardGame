using System;
using System.Collections.Generic;

namespace Core {
	public class Player {
		public string Name { get; }

		private List<Card> _deck;
		public int Wallet { get; private set; }

		public Player(string name) {
			Name = name;
			_deck = new List<Card>();
			Wallet = 3;
		}

		public int PlayerTurn(int diceValue) {
			// To call when it's the actual object turn
			int gain = 0;
			foreach (var card in _deck) {
				if (card.ActivationValue == diceValue) {
					switch (card.CardColor) {
						case CardColor.Blue | CardColor.Green:
							Wallet += card.Profit;
							gain += card.Profit;
							break;
					}
				}
			}
			return gain;
		}

		public Tuple<int,int> OpponentTurn(Player opponent, int diceValue) {
			// To call when it's the opponent player
			int gain = 0;
			int loss = 0;
			foreach (var card in _deck) {
				if (card.ActivationValue == diceValue) {
					switch (card.CardColor) {
						case CardColor.Blue:
							Wallet += card.Profit;
							gain += card.Profit;
							break;
						case CardColor.Red:
							opponent.Wallet -= card.Profit;
							Wallet += card.Profit;
							loss += card.Profit;
							gain += card.Profit;
							break;
					}
				}
			}
			var num = new Tuple<int,int>(gain,loss);
			return num;
		}

		public void BuyCard(Card card) {
			Wallet -= card.Cost;
			_deck.Add(card);
		}
	}
}