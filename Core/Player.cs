using System;
using System.Collections.Generic;
using System.Linq;

namespace Core {
	public class Player {
		public string Name { get; }

		private readonly List<Card> _deck;
		
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
			foreach (var card in _deck)
			{
				if (card.ActivationValue.Contains(diceValue)) {
					switch (card.CardColor) {
						case CardColor.Blue:
							if ((card.CardCategory == CardCategory.Farm || card.CardCategory == CardCategory.Shop) && Monuments[1].Build)
								gain++;
							gain += card.Profit;
							break;
						
						case CardColor.Green:
							if ((card.CardCategory == CardCategory.Farm || card.CardCategory == CardCategory.Shop) && Monuments[1].Build)
								gain++;
							if (card.CardProfitCat == CardCategory.None)
							{
								gain += card.Profit;
								break;
							}
							else
							{
								foreach (var cardTheSecond in _deck)
									if (card.CardProfitCat == cardTheSecond.CardCategory)
										gain += card.Profit; 
								break;
							}

						case CardColor.Purple:
							if (card.CardType != CardType.BusinessCenter)
							{
								gain += card.Profit;
								loss += card.Profit;
							}
							else
							{
								foreach (var mon in Monuments)
									if (mon.Build)
									{
										gain += card.Profit;
										loss += card.Profit;
									}
							}
							break;
					}
				}
			}
			opponent.Wallet -= loss;
			Wallet += gain;
			if (opponent.Wallet < 0)
			{
				Wallet += opponent.Wallet;
				loss += opponent.Wallet;
				gain += opponent.Wallet;
				opponent.Wallet = 0;
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
							if ((card.CardCategory == CardCategory.Farm || card.CardCategory == CardCategory.Shop) && Monuments[1].Build)
								gain++;
							gain += card.Profit;
							break;
						case CardColor.Red:
							if ((card.CardCategory == CardCategory.Farm || card.CardCategory == CardCategory.Shop) && Monuments[1].Build)
							{
								gain++;
								loss++;
							}
							gain += card.Profit;
							loss += card.Profit;
							break;
					}
				}
			}
			Wallet -= loss;
			opponent.Wallet += gain;
			if (Wallet < 0)
			{
				opponent.Wallet += Wallet;
				loss += Wallet;
				gain += Wallet;
				Wallet = 0;
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
		
		public List<CardType> ListBuyableCard(CardStack stack)
		{
			List<CardType> buyableCard = new List<CardType>();
			if (Wallet >= 1)
			{
				if (stack.GetCardCount(CardType.WheatField) > 0)
					buyableCard.Add(CardType.WheatField);
				if (stack.GetCardCount(CardType.Farm) > 0)
					buyableCard.Add(CardType.Farm);
				if (stack.GetCardCount(CardType.Bakery) > 0)
					buyableCard.Add(CardType.Bakery);
			}
			if (Wallet >= 2)
			{
				if (stack.GetCardCount(CardType.CoffeeShop) > 0)
					buyableCard.Add(CardType.CoffeeShop);
				if (stack.GetCardCount(CardType.GroceryStore) > 0)
					buyableCard.Add(CardType.GroceryStore);
				if (stack.GetCardCount(CardType.Market) > 0)
					buyableCard.Add(CardType.Market);
			}
			if (Wallet >= 3)
			{
				if (stack.GetCardCount(CardType.Forest) > 0)
					buyableCard.Add(CardType.Forest);
				if (stack.GetCardCount(CardType.FurnitureShop) > 0)
					buyableCard.Add(CardType.FurnitureShop);
				if (stack.GetCardCount(CardType.Orchard) > 0)
					buyableCard.Add(CardType.Orchard);
				if (stack.GetCardCount(CardType.Restaurant) > 0)
					buyableCard.Add(CardType.Restaurant);
			}
			if (Wallet >= 5)
				if (stack.GetCardCount(CardType.CheeseShop) > 0)
					buyableCard.Add(CardType.CheeseShop);
			if (Wallet >= 6)
			{
				if (stack.GetCardCount(CardType.Mine) > 0)
					buyableCard.Add(CardType.Mine);
				if (stack.GetCardCount(CardType.Stadium) > 0 && !HasCard(CardType.Stadium))
					buyableCard.Add(CardType.Stadium);
			}
			if (Wallet >= 7)
				if (stack.GetCardCount(CardType.TelevisionChannel) > 0 && !HasCard(CardType.TelevisionChannel))
					buyableCard.Add(CardType.TelevisionChannel);
			if (Wallet >= 8)
				if (stack.GetCardCount(CardType.BusinessCenter) > 0 && !HasCard(CardType.BusinessCenter))
					buyableCard.Add(CardType.BusinessCenter);

			return buyableCard;
		}

		public List<Monument> ListBuyableMonuments()
		{
			List<Monument> buyableCard = new List<Monument>();
			if (!Monuments[0].Build && Wallet >= Monuments[0].Cost)
				buyableCard.Add(Monuments[0]);
			if (!Monuments[1].Build && Wallet >= Monuments[1].Cost)
				buyableCard.Add(Monuments[1]);
			if (!Monuments[2].Build && Wallet >= Monuments[2].Cost)
				buyableCard.Add(Monuments[2]);
			if (!Monuments[3].Build && Wallet >= Monuments[3].Cost)
				buyableCard.Add(Monuments[3]);
			return buyableCard;
		}
	}
}