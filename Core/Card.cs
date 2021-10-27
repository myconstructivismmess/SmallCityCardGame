namespace Core {
	public abstract class Card
	{
		public abstract string Name { get; }
		public abstract int[] ActivationValue { get; }
		public abstract int Profit { get; }
		public abstract int Cost { get; }
		public abstract CardType CardType { get; }
		public abstract CardColor CardColor { get; }
		public abstract CardCategory CardCategory { get; }
	}

	public class WheatField : Card
	{
		public override string Name => "Wheat Field";
		public override int[] ActivationValue => new int[]{1};
		public override int Profit => 1;
		public override int Cost => 1;
		public override CardType CardType => CardType.WheatField;
		public override CardColor CardColor => CardColor.Blue;
		public override CardCategory CardCategory => CardCategory.Field;
	}
	
	public class Farm : Card
	{
		public override string Name => "Farm";
		public override int[] ActivationValue => new int[]{1};
		public override int Profit => 1;
		public override int Cost => 2;
		public override CardType CardType => CardType.Farm;
		public override CardColor CardColor => CardColor.Blue;
		public override CardCategory CardCategory => CardCategory.Farm;
	}
	
	public class Bakery : Card
	{
		public override string Name => "Bakery";
		public override int[] ActivationValue => new int[]{2, 3};
		public override int Profit => 2;
		public override int Cost => 1;
		public override CardType CardType => CardType.Bakery;
		public override CardColor CardColor => CardColor.Green;
		public override CardCategory CardCategory => CardCategory.Shop;
	}
	
	public class CoffeeShop : Card
	{
		public override string Name => "Coffee";
		public override int[] ActivationValue => new int[]{3};
		public override int Profit => 1;
		public override int Cost => 2;
		public override CardType CardType => CardType.CoffeeShop;
		public override CardColor CardColor => CardColor.Red;
		public override CardCategory CardCategory => CardCategory.Food;
	}
	
	public class GroceryStore : Card
	{
		public override string Name => "Grocery Store";
		public override int[] ActivationValue => new int[]{4};
		public override int Profit => 3;
		public override int Cost => 2;
		public override CardType CardType => CardType.GroceryStore;
		public override CardColor CardColor => CardColor.Green;
		public override CardCategory CardCategory => CardCategory.Shop;
	}
	
	public class Forest : Card
	{
		public override string Name => "Forest";
		public override int[] ActivationValue => new int[]{5};
		public override int Profit => 1;
		public override int Cost => 2;
		public override CardType CardType => CardType.Forest;
		public override CardColor CardColor => CardColor.Blue;
		public override CardCategory CardCategory => CardCategory.Natural;
	}
	
	public class Restaurant : Card
	{
		public override string Name => "Restaurant";
		public override int[] ActivationValue => new int[]{9, 10};
		public override int Profit => 2;
		public override int Cost => 4;
		public override CardType CardType => CardType.Restaurant;
		public override CardColor CardColor => CardColor.Red;
		public override CardCategory CardCategory => CardCategory.Food;
	}

	public class Stadium : Card
	{
		public override string Name => "Stadium";
		public override int[] ActivationValue => new int[]{6};
		public override int Profit => 4;
		public override int Cost => 6;
		public override CardType CardType => CardType.Stadium;
		public override CardColor CardColor => CardColor.Blue;
		public override CardCategory CardCategory => CardCategory.Building;
	}
	
	// Monument
	public abstract class Monument
	{
		public abstract string Name { get; }
		public abstract int Cost { get; }
		public abstract CardType CardType { get; }
		public abstract CardCategory CardCategory { get; }
	}

	public class Station : Monument
	{
		public override string Name => "Station";
		public override int Cost => 4;
		public override CardType CardType => CardType.Station;
		public override CardCategory CardCategory => CardCategory.Building;
	}
	
	public class ShoppingCenter: Monument
	{
		public override string Name => "ShoppingCenter";
		public override int Cost => 10;
		public override CardType CardType => CardType.ShoppingCenter;
		public override CardCategory CardCategory => CardCategory.Building;
	}
	
	public class RadioTower: Monument
	{
		public override string Name => "RadioTower";
		public override int Cost => 22;
		public override CardType CardType => CardType.RadioTower;
		public override CardCategory CardCategory => CardCategory.Building;
	}
	
	public class ThemePark: Monument
	{
		public override string Name => "ThemePark";
		public override int Cost => 16;
		public override CardType CardType => CardType.ThemePark;
		public override CardCategory CardCategory => CardCategory.Building;
	}
}