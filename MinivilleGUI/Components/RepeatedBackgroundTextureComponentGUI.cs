using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components
{
	public class RepeatedBackgroundTextureGUI : ComponentGUI
	{
		private Texture2D _texture;
		
		public RepeatedBackgroundTextureGUI(Texture2D texture) : base(SnapMode.Free, Vector2.Zero)
		{
			_texture = texture;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			for (int x = 0; x < ComponentsManagerGUI.Width; x += _texture.Width)
				for (int y = 0; y < ComponentsManagerGUI.Height; y += _texture.Height)
					spriteBatch.Draw(
						_texture,
						new Vector2(x, y),
						Color.White
					);
		}

		public override int ZIndex => -1;
	}
}