using System;
using System.Net.Configuration;

namespace Core {
	
	// Card
	public abstract class Card
	{
		public abstract string Name { get; }
		public abstract int[] ActivationValue { get; }
		public abstract int Profit { get; }
		public abstract int Cost { get; }
		public abstract CardType CardType { get; }
		public abstract CardColor CardColor { get; }
		public abstract CardCategory CardCategory { get; }
		public abstract CardCategory CardProfitCat { get; }
	}
	
	// 1
	public class WheatField : Card
	{
		public override string Name => "Wheat Field";
		public override int[] ActivationValue => new int[]{1};
		public override int Profit => 1;
		public override int Cost => 1;
		public override CardType CardType => CardType.WheatField;
		public override CardColor CardColor => CardColor.Blue;
		public override CardCategory CardCategory => CardCategory.Field;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 2
	public class Farm : Card
	{
		public override string Name => "Farm";
		public override int[] ActivationValue => new int[]{2};
		public override int Profit => 1;
		public override int Cost => 1;
		public override CardType CardType => CardType.Farm;
		public override CardColor CardColor => CardColor.Blue;
		public override CardCategory CardCategory => CardCategory.Farm;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 3
	public class Bakery : Card
	{
		public override string Name => "Bakery";
		public override int[] ActivationValue => new int[]{2, 3};
		public override int Profit => 1;
		public override int Cost => 1;
		public override CardType CardType => CardType.Bakery;
		public override CardColor CardColor => CardColor.Green;
		public override CardCategory CardCategory => CardCategory.Shop;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 4
	public class CoffeeShop : Card
	{
		public override string Name => "Coffee";
		public override int[] ActivationValue => new int[]{3};
		public override int Profit => 1;
		public override int Cost => 2;
		public override CardType CardType => CardType.CoffeeShop;
		public override CardColor CardColor => CardColor.Red;
		public override CardCategory CardCategory => CardCategory.Food;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 5
	public class GroceryStore : Card
	{
		public override string Name => "Grocery Store";
		public override int[] ActivationValue => new int[]{4};
		public override int Profit => 3;
		public override int Cost => 2;
		public override CardType CardType => CardType.GroceryStore;
		public override CardColor CardColor => CardColor.Green;
		public override CardCategory CardCategory => CardCategory.Shop;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 6
	public class Forest : Card
	{
		public override string Name => "Forest";
		public override int[] ActivationValue => new int[]{5};
		public override int Profit => 1;
		public override int Cost => 3;
		public override CardType CardType => CardType.Forest;
		public override CardColor CardColor => CardColor.Blue;
		public override CardCategory CardCategory => CardCategory.Natural;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 7
	public class BusinessCenter : Card
	{
		public override string Name => "Business Center";
		public override int[] ActivationValue => new int[]{6};
		public override int Profit => 4;
		public override int Cost => 8;
		public override CardType CardType => CardType.BusinessCenter;
		public override CardColor CardColor => CardColor.Purple;
		public override CardCategory CardCategory => CardCategory.Building;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 8
	public class Stadium : Card
	{
		public override string Name => "Stadium";
		public override int[] ActivationValue => new int[]{6};
		public override int Profit => 3;
		public override int Cost => 6;
		public override CardType CardType => CardType.Stadium;
		public override CardColor CardColor => CardColor.Purple;
		public override CardCategory CardCategory => CardCategory.Building;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 9
	public class TelevisionChannel : Card
	{
		public override string Name => "Television Channel";
		public override int[] ActivationValue => new int[]{6};
		public override int Profit => 5;
		public override int Cost => 7;
		public override CardType CardType => CardType.TelevisionChannel;
		public override CardColor CardColor => CardColor.Purple;
		public override CardCategory CardCategory => CardCategory.Building;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 10
	public class CheeseShop : Card
	{
		public override string Name => "Cheese Shop";
		public override int[] ActivationValue => new int[]{7};
		public override int Profit => 3;
		public override int Cost => 5;
		public override CardType CardType => CardType.CheeseShop;
		public override CardColor CardColor => CardColor.Green;
		public override CardCategory CardCategory => CardCategory.Factory;
		public override CardCategory CardProfitCat => CardCategory.Farm;
	}
	// 11
	public class FurnitureShop : Card
	{
		public override string Name => "Furniture Shop";
		public override int[] ActivationValue => new int[]{8};
		public override int Profit => 3;
		public override int Cost => 3;
		public override CardType CardType => CardType.FurnitureShop;
		public override CardColor CardColor => CardColor.Green;
		public override CardCategory CardCategory => CardCategory.Factory;
		public override CardCategory CardProfitCat => CardCategory.Natural;
	}
	// 12
	public class Mine : Card
	{
		public override string Name => "Mine";
		public override int[] ActivationValue => new int[]{9};
		public override int Profit => 5;
		public override int Cost => 6;
		public override CardType CardType => CardType.Mine;
		public override CardColor CardColor => CardColor.Blue;
		public override CardCategory CardCategory => CardCategory.Natural;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 13
	public class Restaurant : Card
	{
		public override string Name => "Restaurant";
		public override int[] ActivationValue => new int[]{9, 10};
		public override int Profit => 2;
		public override int Cost => 3;
		public override CardType CardType => CardType.Restaurant;
		public override CardColor CardColor => CardColor.Red;
		public override CardCategory CardCategory => CardCategory.Food;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 14
	public class Orchard : Card
	{
		public override string Name => "Orchard";
		public override int[] ActivationValue => new int[]{10};
		public override int Profit => 3;
		public override int Cost => 3;
		public override CardType CardType => CardType.Orchard;
		public override CardColor CardColor => CardColor.Blue;
		public override CardCategory CardCategory => CardCategory.Field;
		public override CardCategory CardProfitCat => CardCategory.None;
	}
	// 15
	public class Market : Card
	{
		public override string Name => "Market";
		public override int[] ActivationValue => new int[]{11, 12};
		public override int Profit => 2;
		public override int Cost => 2;
		public override CardType CardType => CardType.Market;
		public override CardColor CardColor => CardColor.Green;
		public override CardCategory CardCategory => CardCategory.Fruit;
		public override CardCategory CardProfitCat => CardCategory.Farm;
	}
	
	
	// Monument
	public abstract class Monument
	{
		public abstract string Name { get; }
		public abstract int Cost { get; }
		public abstract bool Build { get; set; }
		public abstract CardType CardType { get; }
		public abstract CardCategory CardCategory { get; }
	}
	
	// 16
	public class Station : Monument
	{
		public override string Name => "Station";
		public override int Cost => 4;
		public override bool Build
		{
			get { return Build; }

			set { Build = value; }
		}

		public override CardType CardType => CardType.Station;
		public override CardCategory CardCategory => CardCategory.Building;
	}
	// 17
	public class ShoppingCenter: Monument
	{
		public override string Name => "ShoppingCenter";
		public override int Cost => 10;
		public override bool Build
		{
			get { return Build; }

			set { Build = value; }
		}
		public override CardType CardType => CardType.ShoppingCenter;
		public override CardCategory CardCategory => CardCategory.Building;
	}
	// 18
	public class RadioTower: Monument
	{
		public override string Name => "RadioTower";
		public override int Cost => 22;
		public override bool Build
		{
			get { return Build; }

			set { Build = value; }
		}
		public override CardType CardType => CardType.RadioTower;
		public override CardCategory CardCategory => CardCategory.Building;
	}
	// 19
	public class ThemePark: Monument
	{
		public override string Name => "ThemePark";
		public override int Cost => 16;
		public override bool Build
		{
			get { return Build; }

			set { Build = value; }
		}
		public override CardType CardType => CardType.ThemePark;
		public override CardCategory CardCategory => CardCategory.Building;
	}
}