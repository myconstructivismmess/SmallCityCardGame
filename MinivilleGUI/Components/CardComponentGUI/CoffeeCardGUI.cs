using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components.CardComponentGUI
{
	public class CoffeeCardGUI : CardComponentGUI
	{
		public override string CardName => "Coffee";
		
		public CoffeeCardGUI(SnapMode snapMode, Vector2 snappedPosition) : base(snapMode, snappedPosition) { }
	}
}