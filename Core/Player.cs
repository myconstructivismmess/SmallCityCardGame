using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;

namespace Core {
	public class Player {
		public string Name { get; }

		private List<Card> _deck;
		
		public readonly List<Monument> Monuments;
		
		public int Wallet { get; private set; }

		public Player(string name) {
			Name = name;
			_deck = new List<Card>();
			Monuments = new List<Monument>();
			_deck.Add(new WheatField());
			_deck.Add(new Bakery());
			
			Monuments.Add(new Station());
			Monuments.Add(new ShoppingCenter());
			Monuments.Add(new RadioTower());
			Monuments.Add(new ThemePark());
			
			Wallet = 3;
		}

		public Tuple<int,int> PlayerTurn(Player opponent, int diceValue) {
			// To call when it's the actual object turn
			int gain = 0;
			int loss = 0;
			foreach (var card in _deck) {
				if (card.ActivationValue.Contains(diceValue)) {
					switch (card.CardColor) {
						case CardColor.Blue:
							gain += card.Profit;
							Wallet += gain;
							break;
						
						case CardColor.Green:
							if (card.CardProfitCat != CardCategory.None)
							{
								gain += card.Profit;
								Wallet += gain;
								break;
							}
							else
							{
								foreach (var cardTheSecond in _deck)
									if (card.CardProfitCat == cardTheSecond.CardCategory)
										gain += card.Profit;
								Wallet += gain;
								break;
							}

						case CardColor.Purple:
							Wallet += card.Profit;
							opponent.Wallet -= card.Profit;
							loss += card.Profit;
							gain += card.Profit;
							if (opponent.Wallet < 0)
							{
								Wallet += opponent.Wallet;
								loss += opponent.Wallet;
								gain += opponent.Wallet;
								opponent.Wallet = 0;
							}
							break;
						
						default:
							break;
					}
				}
			}

			var num = new Tuple<int,int>(gain,loss);
			return num;
		}

		public Tuple<int,int> OpponentTurn(Player opponent, int diceValue) {
			// To call when it's the opponent player
			int gain = 0;
			int loss = 0;
			foreach (var card in opponent._deck) {
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
			_deck.Add(card);
		}
		
		public void BuyMonument(Monument monument) {
			Wallet -= monument.Cost;
			monument.Build = true;
		}

		public void ListDeck()
		{
			foreach (var card in _deck)
				Console.Write("| " + card.Name + " ");
			Console.Write("|\n");
		}

		public bool HasCard(CardType type)
		{
			foreach (var card in _deck)
				if (card.CardType == type)
					return true;
			return false;
		}

		public int GetCardCount(CardType type) {
			return _deck.Sum(x => x.CardType == type ? 1 : 0);
		}
	}
}