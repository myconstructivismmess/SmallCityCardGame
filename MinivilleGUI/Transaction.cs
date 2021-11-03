using Core;

namespace MinivilleGUI
{
	public enum Addressees
	{
		Player,
		Computer,
		Shop
	}
	
	public class Transaction
	{
		public Addressees From { get; }
		public Addressees To { get; }
		
		public Transaction(Addressees from, Addressees to)
		{
			From = from;
			To = to;
		}
	}

	public class MoneyTransaction : Transaction
	{
		public int Amount { get; }
		
		public MoneyTransaction(int amount, Addressees from, Addressees to) : base(from, to)
		{
			Amount = amount;
		}
	}
	
	public class CardTransaction : Transaction
	{
		public CardType CardType { get; }
		
		public CardTransaction(CardType cardType, Addressees from, Addressees to) : base(from, to)
		{
			CardType = cardType;
		}
	}
}