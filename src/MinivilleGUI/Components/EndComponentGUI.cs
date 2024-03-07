using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components
{
	public class EndComponentGUI : ComponentGUI
	{
		public static Texture2D FailTexture;
		public static Texture2D SuccessTexture;
		public static float TextureScale = 0.6f;

		public bool Visible = false;
		public bool Success = false;

		public EndComponentGUI(SnapMode snapMode, Vector2 snappedPosition) : base(snapMode, snappedPosition) { }

		public override void Draw(SpriteBatch spriteBatch)
        {
	        if (Visible)
	        {
		        Texture2D texture = Success ? SuccessTexture : FailTexture;
		        
		        spriteBatch.Draw(
			        texture,
			        new Rectangle(
				        (int)(ComponentsManagerGUI.Width / 2f - (texture.Width * TextureScale) / 2f),
				        (int)(ComponentsManagerGUI.Height / 2f - (texture.Height * TextureScale) / 2f),
				        (int)(texture.Width * TextureScale),
				        (int)(texture.Height * TextureScale)
				    ),
			        Color.White
			    );
	        }
        }

		public override int ZIndex => 50;
	}
}