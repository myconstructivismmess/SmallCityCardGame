using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components.CardComponentGUI
{
	public class BakeryCardGUI : CardComponentGUI
	{
		public override string CardName => "Bakery";

		public BakeryCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
}