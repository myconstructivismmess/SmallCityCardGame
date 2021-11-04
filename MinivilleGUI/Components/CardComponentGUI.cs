using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Core;

namespace MinivilleGUI.Components
{
	public abstract class CardComponentGUI : ComponentGUI
	{
		public static Dictionary<string, Texture2D> Textures;
		public static float TextureScale = 0.30f;
		public static SpriteFont TypeFont;
		public static SpriteFont DescriptionFont;
		
		public abstract string CardName { get; }
		
		
		public static CardComponentGUI CreateCard(CardType type, SnapMode snapMode, Vector2 snappedPosition) {
			switch (type) {
				case CardType.Bakery:
					return new BakeryCardGUI(snapMode, snappedPosition);
				case CardType.CoffeeShop:
					return new CoffeeCardGUI(snapMode, snappedPosition);
				case CardType.WheatField:
					return new WheatFieldCardGUI(snapMode, snappedPosition);
				case CardType.Farm:
					return new FarmCardGUI(snapMode, snappedPosition);
				case CardType.GroceryStore:
					return new GroceryStoreCardGUI(snapMode, snappedPosition);
				case CardType.Forest:
					return new ForestCardGUI(snapMode, snappedPosition);
				case CardType.BusinessCenter:
					break;
				case CardType.Stadium:
					break;
				case CardType.TelevisionChannel:
					break;
				case CardType.CheeseShop:
					break;
				case CardType.FurnitureShop:
					break;
				case CardType.Mine:
					break;
				case CardType.Restaurant:
					break;
				case CardType.Orchard:
					break;
				case CardType.Market:
					break;
				case CardType.Station:
					break;
				case CardType.ShoppingCenter:
					break;
				case CardType.RadioTower:
					break;
				case CardType.ThemePark:
					break;
			}
			// Very temporary
			return new BakeryCardGUI(snapMode, snappedPosition);
		}
		
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