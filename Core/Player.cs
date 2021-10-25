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

		public void PlayerTurn(int diceValue) {
			// To call when it's the actual object turn
			foreach (var card in _deck) {
				if (card.ActivationValue == diceValue) {
					switch (card.CardColor) {
						case CardColor.Blue | CardColor.Green:
							Wallet += card.Profit;
							break;
					}
				}
			}
		}

		public void OpponentTurn(Player opponent, int diceValue) {
			// To call when it's the opponent player
			foreach (var card in _deck) {
				if (card.ActivationValue == diceValue) {
					switch (card.CardColor) {
						case CardColor.Blue:
							Wallet += card.Profit;
							break;
						case CardColor.Red:
							opponent.Wallet -= card.Profit;
							Wallet += card.Profit;
							break;
					}
				}
			}
		}

		public void BuyCard(Card card) {
			Wallet -= card.Cost;
			_deck.Add(card);
		}
	}
}