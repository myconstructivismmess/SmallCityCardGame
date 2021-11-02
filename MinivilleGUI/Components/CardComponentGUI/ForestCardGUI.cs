using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components.CardComponentGUI
{
	public class ForestCardGUI : CardComponentGUI
	{
		public override string CardName => "Forest";

		public ForestCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
}