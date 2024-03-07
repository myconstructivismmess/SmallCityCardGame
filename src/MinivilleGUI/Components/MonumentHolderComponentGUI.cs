#nullable enable

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.Collections.Generic;

using Core;

namespace MinivilleGUI.Components
{
	public class MonumentsHolderComponentGUI : ComponentGUI
	{
		public static Dictionary<string, Texture2D> BlueprintTextures;
		public static Dictionary<string, Texture2D> BuildingTextures;
		public static int BorderWidth = 6;


		private int _width;

		public bool TrainStationBuilt = false;
		public bool ShoppingCenterBuilt = false;
		public bool RadioTowerBuilt = false;
		public bool ThemeParkBuilt = false;

		public MonumentsHolderComponentGUI(SnapMode snapMode, Vector2 snappedPosition, int width) : base(snapMode, snappedPosition)
		{
			_width = width;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			int monumentWidth = (_width - 3 * BorderWidth) / 2;
			int monumentHeight = (int)(BlueprintTextures["Train Station"].Height * (monumentWidth / (float)BlueprintTextures["Train Station"].Width));
		
			int height = monumentHeight * 2 + 3 * BorderWidth;
			
			Vector2 size = new Vector2(_width, height);
			
			Vector2 drawPosition = DisplayPosition + SnapMode switch
			{
				SnapMode.TopLeft => new Vector2(0, 0),
				SnapMode.Left => new Vector2(0, -size.Y / 2f),
				SnapMode.BottomLeft => new Vector2(0, -size.Y),
				
				SnapMode.Top => new Vector2(-size.X / 2f, 0),
				SnapMode.Free => new Vector2(-size.X / 2f, -size.Y / 2f),
				SnapMode.Bottom => new Vector2(-size.X / 2f, -size.Y),
				
				SnapMode.TopRight => new Vector2(-size.X, 0),
				SnapMode.Right => new Vector2(-size.X, -size.Y / 2f),
				SnapMode.BottomRight => new Vector2(-size.X, -size.Y),
				_ => new Vector2(0, 0)
			};
			
			spriteBatch.Draw(
				(TrainStationBuilt ? BuildingTextures : BlueprintTextures)["Train Station"],
                new Rectangle(
	                BorderWidth + (int)drawPosition.X,
	                BorderWidth + (int)drawPosition.Y,
                    monumentWidth,
                    monumentHeight
                ),
                Color.White
            );
			
			spriteBatch.Draw(
				(ShoppingCenterBuilt ? BuildingTextures : BlueprintTextures)["Shopping Center"],
				new Rectangle(
					BorderWidth + (int)drawPosition.X + monumentWidth + BorderWidth,
					BorderWidth + (int)drawPosition.Y,
					monumentWidth,
					monumentHeight
				),
				Color.White
			);
			
			spriteBatch.Draw(
				(RadioTowerBuilt ? BuildingTextures : BlueprintTextures)["Radio Tower"],
				new Rectangle(
					BorderWidth + (int)drawPosition.X,
					BorderWidth + (int)drawPosition.Y + monumentHeight + BorderWidth,
					monumentWidth,
					monumentHeight
				),
				Color.White
			);
			
			spriteBatch.Draw(
				(ThemeParkBuilt ? BuildingTextures : BlueprintTextures)["Theme Park"],
				new Rectangle(
					BorderWidth + (int)drawPosition.X + monumentWidth + BorderWidth,
					BorderWidth + (int)drawPosition.Y + monumentHeight + BorderWidth,
					monumentWidth,
					monumentHeight
				),
				Color.White
			);
		}
		
		public static Texture2D? GetTexture(CardType cardType)
		{
			string? textureName = cardType switch
			{
				CardType.Station => "Train Station",
				CardType.ShoppingCenter => "Shopping Center",
				CardType.RadioTower => "Radio Tower",
				CardType.ThemePark => "Theme Park",
				_ => null
			};
			
			if (textureName == null)
				return null;
			
			if (BuildingTextures.ContainsKey(textureName))
				return BuildingTextures[textureName];
			
			return null;
		}

		public override int ZIndex => 1;
	}
}