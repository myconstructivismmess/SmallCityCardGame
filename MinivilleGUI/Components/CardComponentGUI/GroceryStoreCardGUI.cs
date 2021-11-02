using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components.CardComponentGUI
{
	public class GroceryStoreCardGUI : CardComponentGUI
	{
		public override string CardName => "Grocery Store";
		
		public GroceryStoreCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
}