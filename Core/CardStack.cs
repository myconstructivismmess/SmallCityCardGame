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
				// switching trough all card colors to associate it with the correspondent cards type
				foreach (var value in Enum.GetValues(typeof(CardColor)).Cast<CardColor>()) {
					switch (value) {
						case CardColor.Blue:
							_cards.Add(new (CardType.WheatField, value));
							_cards.Add(new (CardType.Farm, value));
							_cards.Add(new (CardType.Forest, value));
							_cards.Add(new (CardType.Stadium, value));
							break;
						case CardColor.Green:
							_cards.Add(new (CardType.Bakery, value));
							_cards.Add(new (CardType.GroceryStore, value));
							break;
						case CardColor.Red:
							_cards.Add(new (CardType.CoffeeShop, value));
							_cards.Add(new (CardType.Restaurant, value));
							break;
					}
				}
			}
		}

		public void Shuffle() {
			// This function use a random generator to order the list to shuffle it randomly
			_cards = _cards.OrderBy(x => _random.Next()).ToList();
		}

		public Card PickCard() {
			Card card = _cards.Last();
			_cards.RemoveAt(_cards.Count - 1);
			return card;
		}
	}
}