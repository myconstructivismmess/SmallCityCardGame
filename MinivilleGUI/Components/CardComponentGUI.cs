using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinivilleGUI.Components
{
	public abstract class CardComponentGUI : ComponentGUI
	{
		public static Dictionary<string, Texture2D> Textures;
		public static float TextureScale = 0.30f;
		public static SpriteFont TypeFont;
		public static SpriteFont DescriptionFont;
		
		public abstract string CardName { get; }
		
		protected CardComponentGUI(SnapMode snapMode, Vector2 snappedPosition) : base(snapMode, snappedPosition) {}
		
		// Drawing
		public override void Draw(SpriteBatch spriteBatch)
		{
			Texture2D texture = Textures[CardName];
			
			int width = (int)(texture.Width * TextureScale);
			int height = (int)(texture.Height * TextureScale);
			
			spriteBatch.Draw(
				texture,
				new Rectangle(
					(int)(DisplayPosition.X - width / 2),
					(int)(DisplayPosition.Y - height / 2),
					width,
					height
				),
				Color.White
			);
		}
	}
	
	public class BakeryCardGUI : CardComponentGUI
	{
		public override string CardName => "Bakery";

		public BakeryCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class CoffeeCardGUI : CardComponentGUI
	{
		public override string CardName => "Coffee";
		
		public CoffeeCardGUI(SnapMode snapMode, Vector2 snappedPosition) : base(snapMode, snappedPosition) { }
	}
	
	public class FarmCardGUI : CardComponentGUI
	{
		public override string CardName => "Farm";

		public FarmCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class ForestCardGUI : CardComponentGUI
	{
		public override string CardName => "Forest";

		public ForestCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class GroceryStoreCardGUI : CardComponentGUI
	{
		public override string CardName => "Grocery Store";
		
		public GroceryStoreCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class WheatFieldCardGUI : CardComponentGUI
	{
		public override string CardName => "Wheat Field";

		public WheatFieldCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
}