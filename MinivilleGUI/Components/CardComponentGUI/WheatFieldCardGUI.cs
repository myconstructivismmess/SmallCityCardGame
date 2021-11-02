using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components.CardComponentGUI
{
	public class WheatFieldCardGUI : CardComponentGUI
	{
		public override string CardName => "Wheat Field";

		public WheatFieldCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
}