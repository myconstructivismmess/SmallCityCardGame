using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinivilleGUI.Components.IconComponentGUI
{
	public class ShopIconComponentGUI : IconComponentGUI
	{
		public static Texture2D Texture;
		public static float DefaultScale = .2f;
		public static float HoveredScale = .25f;

		public ShopIconComponentGUI(SnapMode snapMode, Vector2 position) : base(Texture, snapMode, position, DefaultScale, HoveredScale) { }
	}
}