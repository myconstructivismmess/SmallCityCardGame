using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace MinivilleGUI.Components.CardComponentGUI
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
}