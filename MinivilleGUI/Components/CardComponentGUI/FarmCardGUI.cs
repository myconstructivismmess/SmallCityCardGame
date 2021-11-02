using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components.CardComponentGUI
{
	public class FarmCardGUI : CardComponentGUI
	{
		public override string CardName => "Farm";

		public FarmCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
}