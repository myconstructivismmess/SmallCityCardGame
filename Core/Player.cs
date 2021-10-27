using System;
using System.Collections.Generic;
using System.Linq;

namespace Core {
	public class Player {
		public string Name { get; }

		protected List<Card> Deck;
		public int Wallet { get; private set; }

		public Player(string name) {
			Name = name;
			Deck = new List<Card>();
			Deck.Add(new WheatField());
			Deck.Add(new Bakery());
			Wallet = 3;
		}

		public int PlayerTurn(int diceValue) {
			// To call when it's the actual object turn
			int gain = 0;
			foreach (var card in Deck) {
				if (card.ActivationValue.Contains(diceValue)) {
					switch (card.CardColor) {
						case CardColor.Blue:
						case CardColor.Green:
							gain += card.Profit;
							break;
					}
				}
			}

			Wallet += gain;
			return gain;
		}

		public Tuple<int,int> OpponentTurn(Player opponent, int diceValue) {
			// To call when it's the opponent player
			int gain = 0;
			int loss = 0;
			foreach (var card in opponent.Deck) {
				if (card.ActivationValue.Contains(diceValue)) {
					switch (card.CardColor) {
						case CardColor.Blue:
							opponent.Wallet += card.Profit;
							gain += card.Profit;
							break;
						case CardColor.Red:
							Wallet -= card.Profit;
							opponent.Wallet += card.Profit;
							loss += card.Profit;
							gain += card.Profit;
							if (Wallet < 0)
							{
								opponent.Wallet += Wallet;
								loss += Wallet;
								gain += Wallet;
								Wallet = 0;
							}
							break;
					}
				}
			}
			var num = new Tuple<int,int>(gain,loss);
			return num;
		}

		public void BuyCard(Card card) {
			Wallet -= card.Cost;
			Deck.Add(card);
		}

		public void ListDeck()
		{
			foreach (var card in Deck)
				Console.Write("| " + card.Name + " ");
			Console.Write("|\n");
		}
	}
}