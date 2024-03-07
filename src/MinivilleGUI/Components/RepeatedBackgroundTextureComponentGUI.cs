using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components
{
	public class RepeatedBackgroundTextureGUI : ComponentGUI
	{
		private Texture2D _texture;
		private RenderTarget2D _preRenderedBackground;
		private int _width;
		private int _height;
		
		public RepeatedBackgroundTextureGUI(Texture2D texture) : base(SnapMode.Free, Vector2.Zero)
		{
			_texture = texture;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (_width != ComponentsManagerGUI.Width || _height != ComponentsManagerGUI.Height)
			{
				_width = ComponentsManagerGUI.Width;
				_height = ComponentsManagerGUI.Height;

				ReRender(spriteBatch);
			}
			
			spriteBatch.Draw(
				_preRenderedBackground,
				new Vector2(0, 0),
				Color.White
			);
		}

		private void ReRender(SpriteBatch spriteBatch)
		{
			_preRenderedBackground = new RenderTarget2D(
				MinivilleGUI.GraphicsDeviceManager.GraphicsDevice,
				ComponentsManagerGUI.Width,
				ComponentsManagerGUI.Height
			);
			spriteBatch.End();
			
			MinivilleGUI.GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(_preRenderedBackground);
			MinivilleGUI.GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin();

			for (int x = 0; x < ComponentsManagerGUI.Width; x += _texture.Width)
				for (int y = 0; y < ComponentsManagerGUI.Height; y += _texture.Height)
					spriteBatch.Draw(
						_texture,
						new Vector2(x, y),
						Color.White
					);
			
			spriteBatch.End();
			MinivilleGUI.GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
			
			spriteBatch.Begin();
		}

		public override int ZIndex => -1;
	}
}