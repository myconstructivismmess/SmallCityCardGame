namespace Core {
	public abstract class Card
	{
		public abstract string Name { get; }
		public abstract int ActivationValue { get; }
		public abstract int Profit { get; }
		public abstract int Cost { get; }
		public abstract CardColor CardColor { get; }
	}

	public class WheatField : Card
	{
		public override string Name => "Wheat Field";
		public override int ActivationValue => 1;
		public override int Profit => 1;
		public override int Cost => 1;
		public override CardColor CardColor => CardColor.Blue;
	}
	
	public class Farm : Card
	{
		public override string Name => "Farm";
		public override int ActivationValue => 1;
		public override int Profit => 1;
		public override int Cost => 2;
		public override CardColor CardColor => CardColor.Blue;
	}
	
	public class Bakery : Card
	{
		public override string Name => "Bakery";
		public override int ActivationValue => 2;
		public override int Profit => 2;
		public override int Cost => 1;
		public override CardColor CardColor => CardColor.Green;
	}
	
	public class Coffee : Card
	{
		public override string Name => "Coffee";
		public override int ActivationValue => 3;
		public override int Profit => 1;
		public override int Cost => 2;
		public override CardColor CardColor => CardColor.Red;
	}
	
	public class GroceryStore : Card
	{
		public override string Name => "Grocery Store";
		public override int ActivationValue => 4;
		public override int Profit => 3;
		public override int Cost => 2;
		public override CardColor CardColor => CardColor.Green;
	}
	
	public class Forest : Card
	{
		public override string Name => "Forest";
		public override int ActivationValue => 5;
		public override int Profit => 1;
		public override int Cost => 2;
		public override CardColor CardColor => CardColor.Blue;
	}
	
	public class Restaurant : Card
	{
		public override string Name => "Restaurant";
		public override int ActivationValue => 5;
		public override int Profit => 2;
		public override int Cost => 4;
		public override CardColor CardColor => CardColor.Red;
	}

	public class Stadium : Card
	{
		public override string Name => "Stadium";
		public override int ActivationValue => 6;
		public override int Profit => 4;
		public override int Cost => 6;
		public override CardColor CardColor => CardColor.Blue;
	}
}