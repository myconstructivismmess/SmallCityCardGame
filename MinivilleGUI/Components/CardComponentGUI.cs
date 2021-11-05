using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.Collections.Generic;

using Core;

namespace MinivilleGUI.Components
{
	public abstract class CardComponentGUI : ComponentGUI
	{
		public static Dictionary<string, Texture2D> Textures;
		public static float TextureScale = 0.1f;

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
					return new BusinessCenterCardGUI(snapMode, snappedPosition);
				case CardType.Stadium:
					return new StadiumCardGUI(snapMode, snappedPosition);
				case CardType.TelevisionChannel:
					return new TelevisionChannelCardGUI(snapMode, snappedPosition);
				case CardType.CheeseFactory:
					return new CheeseFactoryCardGUI(snapMode, snappedPosition);
				case CardType.FurnitureFactory:
					return new FurnitureFactoryCardGUI(snapMode, snappedPosition);
				case CardType.Mine:
					return new MineCardGUI(snapMode, snappedPosition);
				case CardType.Restaurant:
					return new RestaurantCardGUI(snapMode, snappedPosition);
				case CardType.Orchard:
					return new OrchardCardGUI(snapMode, snappedPosition);
				case CardType.Market:
					return new VegetablesMarketCardGUI(snapMode, snappedPosition);
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

	public class BusinessCenterCardGUI : CardComponentGUI
	{
		public override string CardName => "Business Center";

		public BusinessCenterCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class CheeseFactoryCardGUI : CardComponentGUI
	{
		public override string CardName => "Cheese Factory";

		public CheeseFactoryCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class FurnitureFactoryCardGUI : CardComponentGUI
	{
		public override string CardName => "Furniture Factory";

		public FurnitureFactoryCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class MineCardGUI : CardComponentGUI
	{
		public override string CardName => "Mine";

		public MineCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class OrchardCardGUI : CardComponentGUI
	{
		public override string CardName => "Orchard";

		public OrchardCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class RestaurantCardGUI : CardComponentGUI
	{
		public override string CardName => "Restaurant";

		public RestaurantCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class StadiumCardGUI : CardComponentGUI
	{
		public override string CardName => "Stadium";

		public StadiumCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class TelevisionChannelCardGUI : CardComponentGUI
	{
		public override string CardName => "Television Channel";

		public TelevisionChannelCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
	
	public class VegetablesMarketCardGUI : CardComponentGUI
	{
		public override string CardName => "Vegetables Market";

		public VegetablesMarketCardGUI(SnapMode corner, Vector2 startPosition) : base(corner, startPosition) { }
	}
}