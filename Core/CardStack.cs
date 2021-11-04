#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core {
	public class CardStack
	{
		private List<Card> _cards;
		public CardStack() {
			// Creating a new stack of card with 6 of each cards
			_cards = new List<Card>();

			for (var i = 0; i < 6; i++) {
				if (i < 2)
				{
					// Only 2 of these because the rules say so
					// (Only one per player)
					_cards.Add(new BusinessCenter());
					_cards.Add(new Stadium());
					_cards.Add(new TelevisionChannel());
				}
				if (i < 4)
				{
					// Only 4 of these because of the start deck of each players
					_cards.Add(new WheatField());
					_cards.Add(new Bakery());
				}
				_cards.Add(new Farm());
				_cards.Add(new CoffeeShop());
				_cards.Add(new GroceryStore());
				_cards.Add(new Forest());
				_cards.Add(new CheeseShop());
				_cards.Add(new FurnitureShop());
				_cards.Add(new Mine());
				_cards.Add(new Restaurant());
				_cards.Add(new Orchard());
				_cards.Add(new Market());
			}
		}

		public int GetCardCount(CardType type) {
			return _cards.Sum(x => x.CardType == type ? 1 : 0);
		}

		public Card? GetCard(CardType type)
		{
			foreach (var card in _cards)
			{
				if (card.CardType == type)
					return card;
			}
			return null;
		}

		public Card? PickCard(CardType type) {
			Card? result = null;
			foreach (var card in _cards) {
				if (card.CardType == type) {
					result = card;
					_cards.Remove(card);
					break;
				}
			}

			return result;
		}

		public int GetStackSize()
		{
			return _cards.Count();
		}
	}
}