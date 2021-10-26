using System;
using System.Collections.Generic;
using System.Linq;

namespace Core {
	public class CardStack {
		private List<Card> _cards;
		
		private static Random _random = new Random();
		public CardStack() {
			// Creating a new stack of card with 6 of each cards
			_cards = new List<Card>();
			
			for (int i = 0; i < 6; i++) {
				if (i < 4) {
					// Only 4 of these because of the start deck of each players
					_cards.Add(new WheatField());
					_cards.Add(new Bakery());
				}
				_cards.Add(new Farm());
				_cards.Add(new Forest());
				_cards.Add(new Stadium());
				_cards.Add(new GroceryStore());
				_cards.Add(new CoffeeShop());
				_cards.Add(new Restaurant());
			}
			Shuffle();
		}

		private void Shuffle() {
			// This function use a random generator to order the list to shuffle it randomly
			_cards = _cards.OrderBy(x => _random.Next()).ToList();
		}

		public Card PickCard() {
			Card card = _cards.Last();
			_cards.RemoveAt(_cards.Count - 1);
			return card;
		}

		public void ReturnCard(Card card) {
			_cards.Add(card);
			Shuffle();
		}
	}
}