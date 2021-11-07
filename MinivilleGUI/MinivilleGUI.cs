using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Linq;
using System;

using MinivilleGUI.Components;

using Core;

namespace MinivilleGUI
{
	public class MinivilleGUI : Game
	{
		private static ComponentsManagerGUI _componentsManagerGUI;
		public static GraphicsDeviceManager GraphicsDeviceManager;
		private static GameGUI _game;
		private static SpriteBatch _spriteBatch;
		public static SpriteFont DebugFont;
		private Random _random = new Random();
		
		

		private Vector2 _windowSize = new Vector2(
			1000, // Initial Window Width
			800 // Initial Window Height
		);

		// Components
		private List<CardComponentGUI> _playerCardsComponentsGUI;
		private List<CardComponentGUI> _iaCardsComponentsGUI;

		private CoinsHolderComponentGUI _playerCoinsHolderComponentGUI;
		private CoinsHolderComponentGUI _iaCoinsHolderComponentGUI;
		
		private MonumentsHolderComponentGUI _playerMonumentsHolderComponentGUI;
		private MonumentsHolderComponentGUI _iaMonumentsHolderComponentGUI;
		
		private SideButtonComponentGUI _passTurnButtonComponentGUI;
		private SideButtonComponentGUI _buyCardsButtonComponentGUI;
		
		private DiceComponentGUI _diceComponentGUI;
		
		private TurnComponentGUI _turnComponentGUI;
		
		private SideDiceSelectorComponentGUI _diceSelectorComponentGUI;
		
		private EndComponentGUI _endComponentGUI;
		
		private WindowComponentGUI _shopWindowComponentGUI;
		private int[,] _shopContentCardsCoordsAndSizes = new int[0, 0];
		private CardType[] _shopContentCardsTypes = Array.Empty<CardType>();
		private int _shopScrollValue;

		private int _scrollWheelValue;

		public MinivilleGUI()
		{
			_game = new GameGUI();
			
			// Components Manager Initialization
			_componentsManagerGUI = new ComponentsManagerGUI();
			
			// Graphics Device Manager Initialization
			GraphicsDeviceManager = new GraphicsDeviceManager(this);

			// Set Mouse Visibility
			IsMouseVisible = true;
			
			// Set Window Resizability
			Window.AllowUserResizing = true;
			
			// Set Assets Directory
			Content.RootDirectory = "Content";
			
			// Set Window Size
			GraphicsDeviceManager.PreferredBackBufferWidth = (int)_windowSize.X;
			GraphicsDeviceManager.PreferredBackBufferHeight = (int)_windowSize.Y;

			// Set Components Managers Window Size
			ComponentsManagerGUI.ResizeWindow((int)_windowSize.X, (int)_windowSize.Y);

			// Subscribe To Window Resizing Event
			Window.ClientSizeChanged += Window_ClientSizeChanged;
		}

		private void Window_ClientSizeChanged(object sender, EventArgs e)
		{
			_windowSize = new Vector2(
				Window.ClientBounds.Width,
				Window.ClientBounds.Height
			);

			// Set Window Size
			GraphicsDeviceManager.PreferredBackBufferWidth = (int)_windowSize.X;
			GraphicsDeviceManager.PreferredBackBufferHeight = (int)_windowSize.Y;

			// Set Components Managers Window Size
			ComponentsManagerGUI.ResizeWindow((int)_windowSize.X, (int)_windowSize.Y);

			_shopWindowComponentGUI.Width = (int)_windowSize.X - 350;

			GraphicsDeviceManager.ApplyChanges();
		}

		protected override void Initialize()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Players Cards Initialization
			_playerCardsComponentsGUI = _game.Player.Deck.Select(
				x => CardComponentGUI.CreateCard(
					x.CardType, 
					SnapMode.Bottom, 
					new Vector2(0, 500)
				)
			).ToList();

			SnapPlayerCards();

			foreach (CardComponentGUI card in _playerCardsComponentsGUI)
				_componentsManagerGUI.Components.Add(card);

			// IA Cards Initialization
			_iaCardsComponentsGUI = _game.Computer.Deck.Select(
				x => CardComponentGUI.CreateCard(
					x.CardType, 
					SnapMode.Top, 
					new Vector2(0, -500)
				)
			).ToList();
			
			SnapIaCards();

			foreach (CardComponentGUI card in _iaCardsComponentsGUI)
				_componentsManagerGUI.Components.Add(card);
			
			// Player Coins Holder Initialization
			_playerCoinsHolderComponentGUI = new CoinsHolderComponentGUI(
				_game.Player.Wallet,
				SnapMode.BottomRight,
				new Vector2(
					500,
					500
				)
			);
			_playerCoinsHolderComponentGUI.SnapTo(new Vector2(-50, -50));
			_componentsManagerGUI.Components.Add(_playerCoinsHolderComponentGUI);
			
			// IA Coins Holder Initialization
			_iaCoinsHolderComponentGUI = new CoinsHolderComponentGUI(
				_game.Computer.Wallet, 
				SnapMode.TopLeft, 
				new Vector2(
					-500,
					-500
				)
			);
			_iaCoinsHolderComponentGUI.SnapTo(new Vector2(50, 50));
			_componentsManagerGUI.Components.Add(_iaCoinsHolderComponentGUI);


			_playerMonumentsHolderComponentGUI =
				new MonumentsHolderComponentGUI(SnapMode.BottomLeft, Vector2.Zero, 200);
			_componentsManagerGUI.Components.Add(_playerMonumentsHolderComponentGUI);
			
			_iaMonumentsHolderComponentGUI =
				new MonumentsHolderComponentGUI(SnapMode.TopRight, Vector2.Zero, 200);
			_componentsManagerGUI.Components.Add(_iaMonumentsHolderComponentGUI);

			// Button to Pass the turn Initialization
			_passTurnButtonComponentGUI =
				new SideButtonComponentGUI(
					SnapMode.Left, 
					new Vector2(0, -40), 
					"Faire des economies  %", 
					true, 
					40
				);
			_passTurnButtonComponentGUI.Pressed += OnPassTurnButtonPressed;
			_componentsManagerGUI.Components.Add(_passTurnButtonComponentGUI);
			
			// Button to Buy Cards Initialization
			_buyCardsButtonComponentGUI =
				new SideButtonComponentGUI(
					SnapMode.Left, 
					new Vector2(0, 40), 
					"Acheter une carte  $",
					true, 
					33
				);
			_buyCardsButtonComponentGUI.Pressed += OnBuyCardsButtonPressed;
			_buyCardsButtonComponentGUI.PressedElseWhere += OnBuyCardsButtonPressedElsewhere;
			_componentsManagerGUI.Components.Add(_buyCardsButtonComponentGUI);

			_shopWindowComponentGUI = new WindowComponentGUI(
				SnapMode.Right,
				Vector2.Zero,
				(int)_windowSize.X - 350, 
				500, 
				"Acheter une carte"
			);
			_componentsManagerGUI.Components.Add(_shopWindowComponentGUI);
			
			_diceComponentGUI = new DiceComponentGUI(SnapMode.TopRight, new Vector2(0, 0));
			_diceComponentGUI.Rolled += OnDiceRolled;
			_componentsManagerGUI.Components.Add(_diceComponentGUI);

			_turnComponentGUI = new TurnComponentGUI(SnapMode.Top, Vector2.Zero);
			_componentsManagerGUI.Components.Add(_turnComponentGUI);

			_diceSelectorComponentGUI = new SideDiceSelectorComponentGUI(SnapMode.BottomLeft, new Vector2(0, -20));
			_componentsManagerGUI.Components.Add(_diceSelectorComponentGUI);
			
			_endComponentGUI = new EndComponentGUI(SnapMode.Free, Vector2.Zero);
			_componentsManagerGUI.Components.Add(_endComponentGUI);
			
			_diceComponentGUI.Roll();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_componentsManagerGUI.Components.Add(
				new RepeatedBackgroundTextureGUI(Content.Load<Texture2D>("Background"))
			);

			// Set Cards Textures
			CardComponentGUI.Textures = new Dictionary<string, Texture2D>
			{
				{ "Bakery", Content.Load<Texture2D>("Cards/Bakery") },
				{ "Business Center", Content.Load<Texture2D>("Cards/Business Center") },
				{ "Cheese Factory", Content.Load<Texture2D>("Cards/Cheese Factory") },
				{ "Coffee", Content.Load<Texture2D>("Cards/Coffee") },
				{ "Farm", Content.Load<Texture2D>("Cards/Farm") },
				{ "Forest", Content.Load<Texture2D>("Cards/Forest") },
				{ "Furniture Factory", Content.Load<Texture2D>("Cards/Furniture Factory") },
				{ "Grocery Store", Content.Load<Texture2D>("Cards/Grocery Store") },
				{ "Mine", Content.Load<Texture2D>("Cards/Mine") },
				{ "Orchard", Content.Load<Texture2D>("Cards/Orchard") },
				{ "Restaurant", Content.Load<Texture2D>("Cards/Restaurant") },
				{ "Stadium", Content.Load<Texture2D>("Cards/Stadium") },
				{ "Television Channel", Content.Load<Texture2D>("Cards/Television Channel") },
				{ "Vegetables Market", Content.Load<Texture2D>("Cards/Vegetables Market") },
				{ "Wheat Field", Content.Load<Texture2D>("Cards/Wheat Field") }
			};
			
			// Set Monuments Blueprint Textures
			MonumentsHolderComponentGUI.BlueprintTextures = new Dictionary<string, Texture2D>
			{
				{ "Radio Tower", Content.Load<Texture2D>("Monuments/Blueprint/Radio Tower") },
				{ "Shopping Center", Content.Load<Texture2D>("Monuments/Blueprint/Shopping Center") },
				{ "Theme Park", Content.Load<Texture2D>("Monuments/Blueprint/Theme Park") },
				{ "Train Station", Content.Load<Texture2D>("Monuments/Blueprint/Train Station") }
			};
			
			// Set Monuments Building Textures
			MonumentsHolderComponentGUI.BuildingTextures = new Dictionary<string, Texture2D>
            {
                { "Radio Tower", Content.Load<Texture2D>("Monuments/Building/Radio Tower") },
                { "Shopping Center", Content.Load<Texture2D>("Monuments/Building/Shopping Center") },
                { "Theme Park", Content.Load<Texture2D>("Monuments/Building/Theme Park") },
                { "Train Station", Content.Load<Texture2D>("Monuments/Building/Train Station") }
            };

			// Set Dice Textures
			DiceComponentGUI.DiceTextures = new []
            {
	            Content.Load<Texture2D>("Dice/1"),
	            Content.Load<Texture2D>("Dice/2"),
	            Content.Load<Texture2D>("Dice/3"),
	            Content.Load<Texture2D>("Dice/4"),
	            Content.Load<Texture2D>("Dice/5"),
	            Content.Load<Texture2D>("Dice/6")
            };

			// Set Dice Background Texture
			DiceComponentGUI.BackgroundTexture = Content.Load<Texture2D>("UI/BackgroundTexture");

			// Set Coins Textures
			CoinsHolderComponentGUI.Textures = new Dictionary<string, Texture2D>
			{
				{"1", Content.Load<Texture2D>("Coins/1")},
				{"5", Content.Load<Texture2D>("Coins/5")},
				{"10", Content.Load<Texture2D>("Coins/10")}
			};
			
			EndComponentGUI.FailTexture = Content.Load<Texture2D>("UI/Fail");
			EndComponentGUI.SuccessTexture = Content.Load<Texture2D>("UI/Success");

			WindowComponentGUI.BackgroundTexture = Content.Load<Texture2D>("UI/BackgroundTexture");
			WindowComponentGUI.Font = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");
			
			SideButtonComponentGUI.BackgroundTexture = Content.Load<Texture2D>("UI/BackgroundTexture");
			SideButtonComponentGUI.Font = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");
			
			TurnComponentGUI.PlayerBackgroundTexture = Content.Load<Texture2D>("UI/BackgroundTexture");
			TurnComponentGUI.EnemyBackgroundTexture = Content.Load<Texture2D>("UI/RedBackgroundTexture");
			TurnComponentGUI.Font = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");
			
			SideDiceSelectorComponentGUI.BackgroundTexture = Content.Load<Texture2D>("UI/BackgroundTexture");
			SideDiceSelectorComponentGUI.Font = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");
			
			DebugFont = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");
		}

		private void SnapIaCards()
		{
			int spacing = 150;
			int verticalSpacing = 120;

			Dictionary<string, List<int>> cardsNumbers = new Dictionary<string, List<int>>();
			int cardsNumbersLength = 0;

			for (int i = 0; i < _iaCardsComponentsGUI.Count; i++)
			{
				string cardName = _iaCardsComponentsGUI[i].CardName;
				
				if (cardsNumbers.ContainsKey(cardName))
					cardsNumbers[cardName].Add(i);
				else
				{
					cardsNumbers[cardName] = new List<int> { i };
					++cardsNumbersLength;
				}
					
			}

			int j = 0;
			foreach (KeyValuePair<string, List<int>> cardsNumber in cardsNumbers)
			{
				for (int i = 0; i < cardsNumber.Value.Count; i++)
				{
					_iaCardsComponentsGUI[cardsNumber.Value[i]].SnapTo(
						SnapMode.Top,
						new Vector2(
							j * spacing - (cardsNumbersLength - 1) / 2f * spacing - i * 25,
							verticalSpacing + i * 25
						)
					);
				}
				
				++j;
			}
		}
		
		private void SnapPlayerCards()
		{
			int spacing = 150;
			int verticalSpacing = 120;

			Dictionary<string, List<int>> cardsNumbers = new Dictionary<string, List<int>>();
			int cardsNumbersLength = 0;

			for (int i = 0; i < _playerCardsComponentsGUI.Count; i++)
			{
				string cardName = _playerCardsComponentsGUI[i].CardName;
				
				if (cardsNumbers.ContainsKey(cardName))
					cardsNumbers[cardName].Add(i);
				else
				{
					cardsNumbers[cardName] = new List<int> { i };
					++cardsNumbersLength;
				}
					
			}

			int j = 0;
			foreach (KeyValuePair<string, List<int>> cardsNumber in cardsNumbers)
			{
				for (int i = 0; i < cardsNumber.Value.Count; i++)
				{
					_playerCardsComponentsGUI[cardsNumber.Value[i]].SnapTo(
						SnapMode.Bottom,
						new Vector2(
							j * spacing - (cardsNumbersLength - 1) / 2f * spacing + i * 25,
							-verticalSpacing - i * 25
						)
					);
				}
				
				++j;
			}
		}
		
		private void OnBuyCardsButtonPressed()
		{
			_shopWindowComponentGUI.Open = true;
		}
		
		private void OnBuyCardsButtonPressedElsewhere()
		{
			MouseState mouseState = Mouse.GetState();
			
			if (mouseState.X < 0 || 
			    mouseState.Y < 0 || 
			    mouseState.X > _windowSize.X || 
			    mouseState.Y > _windowSize.Y) return;

			if (_shopWindowComponentGUI.IsHovered(mouseState))
			{
				Vector2 mousePositionInShop = new Vector2(mouseState.X, mouseState.Y)
				                              - _shopWindowComponentGUI.GetContentPosition();

				if (mousePositionInShop.X > 0 && 
				    mousePositionInShop.Y > 0 &&
				    mousePositionInShop.X < _shopWindowComponentGUI.Width &&
				    mousePositionInShop.Y < _shopWindowComponentGUI.Height)
				{
					int cardIndex = -1;

					for (int i = 0; i < _shopContentCardsCoordsAndSizes.GetLength(0); i++)
					{
						if (mousePositionInShop.X > _shopContentCardsCoordsAndSizes[i, 0] &&
						    mousePositionInShop.Y > _shopContentCardsCoordsAndSizes[i, 1] &&
						    mousePositionInShop.X < _shopContentCardsCoordsAndSizes[i, 0] +
						    _shopContentCardsCoordsAndSizes[i, 2] &&
						    mousePositionInShop.Y < _shopContentCardsCoordsAndSizes[i, 1] +
						    _shopContentCardsCoordsAndSizes[i, 3])
						{
							cardIndex = i;
							break;
						}
					}
					
					if (cardIndex != -1 && cardIndex >= 0 && cardIndex < _shopContentCardsTypes.Length) {
						CardType type = _shopContentCardsTypes[cardIndex];
						switch (type) {
							case CardType.Station:
								_game.Player.BuyMonument(_game.Player.Monuments[0]);
								break;
							case CardType.ShoppingCenter:
								_game.Player.BuyMonument(_game.Player.Monuments[1]);
								break;
							case CardType.RadioTower:
								_game.Player.BuyMonument(_game.Player.Monuments[2]);
								break;
							case CardType.ThemePark:
								_game.Player.BuyMonument(_game.Player.Monuments[3]);
								break;
							default:
								_game.Player.BuyCard(_game.CardStack.PickCard(type));
								CardComponentGUI card = CardComponentGUI.CreateCard(_shopContentCardsTypes[cardIndex], SnapMode.Bottom, new Vector2(0, 500));
								_playerCardsComponentsGUI.Add(card);
								_componentsManagerGUI.ComponentsToAdd.Push(card);
			
								SnapPlayerCards();
								break;
								
						}
						
						_shopWindowComponentGUI.Open = false;
						_game.PlayerTurn = false;
						_diceComponentGUI.Roll(_game.Player.Monuments[0].Build && _random.Next(0, 3) == 0);
					}
				}
			}
			else
				_shopWindowComponentGUI.Open = false;
		}
		
		private void OnPassTurnButtonPressed()
		{
			_game.PlayerTurn = false;
			_diceComponentGUI.Roll(_game.Computer.Monuments[0].Build && _random.Next(0, 3) == 0);
			/*Random random = new Random();

			if (random.NextDouble() > 0.5f)
			{
				CardComponentGUI card;
			
				if (random.NextDouble() > 0.5f)
					card = new WheatFieldCardGUI(SnapMode.Bottom, new Vector2(0, 500));
				else
					card = new BakeryCardGUI(SnapMode.Bottom, new Vector2(0, 500));
				
				_componentsManagerGUI.ComponentsToAdd.Push(card);
				_playerCardsComponentsGUI.Add(card);
				
				SnapPlayerCards();
			}
			else
			{
				CardComponentGUI card;
			
				if (random.NextDouble() > 0.5f)
					card = new WheatFieldCardGUI(SnapMode.Top, new Vector2(0, -500));
				else
					card = new BakeryCardGUI(SnapMode.Top, new Vector2(0, -500));
				
				_componentsManagerGUI.ComponentsToAdd.Push(card);
				_iaCardsComponentsGUI.Add(card);
				
				SnapIaCards();
			}*/

			//_passTurnButtonComponentGUI.Enabled = false;
		}

		private void OnDiceRolled(int value1, int value2) {
			bool playerTurn = _game.PlayerTurn;
			int value = value1 + value2;
			
			if (playerTurn) {
				_game.Player.OpponentTurn(_game.Computer, value);
				_game.Player.PlayerTurn(_game.Computer, value);
				if (_game.Player.Monuments[3].Build && value1 == value2) {
					_diceComponentGUI.Roll(_diceSelectorComponentGUI.TwoDice);
				}
			}
			else {
				// Temporary
				_game.Computer.OpponentTurn(_game.Player, value);
				_game.Computer.PlayerTurn(_game.Player, value);
				
				if (_game.Computer.Monuments[3].Build && value1 == value2) {
					_diceComponentGUI.Roll(_game.Computer.Monuments[0].Build && _random.Next(0, 3) == 0);
					return;
				}

				var shop = _game.Computer.ListBuyableCard(_game.CardStack);
				var monument = _game.Computer.ListBuyableMonuments();
				if (_game.Computer.Wallet > 0)
				{
					if (_game.CardStack.GetStackSize() > 0) {
						var choice = _random.Next(0,shop.Count*2);
						if (monument.Count > 0)
						{
							_game.Computer.BuyMonument(monument[0]);
						}
						else if (choice == 0 || choice>shop.Count) {}  
						else
						{
							CardType type = shop[choice-1]; 
							//Add Card
							_game.Computer.BuyCard(_game.CardStack.PickCard(type));
							CardComponentGUI card = CardComponentGUI.CreateCard(type, SnapMode.Top, new Vector2(0, -500));
							_iaCardsComponentsGUI.Add(card);
							_componentsManagerGUI.ComponentsToAdd.Push(card);
							
							SnapIaCards();
							
						}
					}
				}
				_game.PlayerTurn = true;
				_diceComponentGUI.Roll(_diceSelectorComponentGUI.TwoDice);
			}
		}

		protected override void Update(GameTime gameTime)
		{
			_componentsManagerGUI.Update(gameTime.ElapsedGameTime.TotalSeconds);
			/*
			if (true) {
				CardComponentGUI card = CardComponentGUI.CreateCard(CardType.Bakery, SnapMode.Top, new Vector2(0, -500));
				_iaCardsComponentsGUI.Add(card);
				_componentsManagerGUI.ComponentsToAdd.Push(card);
			
				SnapIaCards();
			}
			*/
			
			_buyCardsButtonComponentGUI.Enabled = _game.PlayerTurn;
			_passTurnButtonComponentGUI.Enabled = _game.PlayerTurn;
			_diceSelectorComponentGUI.Enabled = _game.Player.Monuments[0].Build;

			_iaMonumentsHolderComponentGUI.TrainStationBuilt = _game.Computer.Monuments[0].Build;
			_iaMonumentsHolderComponentGUI.ShoppingCenterBuilt = _game.Computer.Monuments[1].Build;
			_iaMonumentsHolderComponentGUI.RadioTowerBuilt = _game.Computer.Monuments[2].Build;
			_iaMonumentsHolderComponentGUI.ThemeParkBuilt = _game.Computer.Monuments[3].Build;
			
			_playerMonumentsHolderComponentGUI.TrainStationBuilt = _game.Player.Monuments[0].Build;
			_playerMonumentsHolderComponentGUI.ShoppingCenterBuilt = _game.Player.Monuments[1].Build;
			_playerMonumentsHolderComponentGUI.RadioTowerBuilt = _game.Player.Monuments[2].Build;
			_playerMonumentsHolderComponentGUI.ThemeParkBuilt = _game.Player.Monuments[3].Build;
			
			// TODO: End Screen 
			if(_game.IsPlayerWin())
				Exit();
			if(_game.IsComputerWin())
				Exit();
			
			MouseState mouseState = Mouse.GetState();

			int scrollWheelValueDelta =
				mouseState.ScrollWheelValue + mouseState.HorizontalScrollWheelValue - _scrollWheelValue;

			_scrollWheelValue = mouseState.ScrollWheelValue + mouseState.HorizontalScrollWheelValue;

			if (_shopWindowComponentGUI.IsHovered(mouseState))
				_shopScrollValue += scrollWheelValueDelta / 2;
			
			_playerCoinsHolderComponentGUI.Value = _game.Player.Wallet;
			_iaCoinsHolderComponentGUI.Value = _game.Computer.Wallet;

			_turnComponentGUI.PlayerTurn = _game.PlayerTurn;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			DrawShop();
			
			_spriteBatch.Begin();
			_componentsManagerGUI.Draw(_spriteBatch);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
		
		private void DrawShop()
		{
			int cardSpacing = 20;

			List<Vector2> cardCoordinates = new List<Vector2>();
			List<Vector2> cardSizes = new List<Vector2>();
			List<Texture2D> cardTextures = new List<Texture2D>();
			List<CardType> cardTypes = new List<CardType>();

			int width = 0;
			foreach (CardType cardType in _game.Player.ListBuyableCard(_game.CardStack))
			{
				Texture2D texture = CardComponentGUI.GetTexture(cardType);

				if (!ReferenceEquals(texture, null))
				{
					cardTextures.Add(texture);
					cardTypes.Add(cardType);
					
					int cardHeight = _shopWindowComponentGUI.Height - 2 * cardSpacing;
					int cardWidth = (int)(cardHeight / (float)texture.Height * texture.Width);
					
					cardSizes.Add(new Vector2(cardWidth, cardHeight));

					width += cardWidth + cardSpacing;
					
					cardCoordinates.Add(
						new Vector2(
							cardSpacing + width,
							cardSpacing
						)
					);
				}
				else
				{
					width += 200;
				}
			}
			
			foreach (Monument monument in _game.Player.ListBuyableMonuments())
			{
				Texture2D texture = MonumentsHolderComponentGUI.GetTexture(monument.CardType);

				if (!ReferenceEquals(texture, null))
				{
					cardTextures.Add(texture);
					cardTypes.Add(monument.CardType);
					
					int cardHeight = _shopWindowComponentGUI.Height - 2 * cardSpacing;
					int cardWidth = (int)(cardHeight / (float)texture.Height * texture.Width);
					
					cardSizes.Add(new Vector2(cardWidth, cardHeight));

					width += cardWidth + cardSpacing;
					
					cardCoordinates.Add(
						new Vector2(
							cardSpacing + width,
							cardSpacing
						)
					);
				}
				else
				{
					width += 200;
				}
			}

			_shopScrollValue = Math.Min(0, _shopScrollValue);
			_shopScrollValue = Math.Max(-width + _shopWindowComponentGUI.Width - cardSpacing, _shopScrollValue);

			int widthSpacing = Math.Max((_shopWindowComponentGUI.Width - width), 0);
			if (widthSpacing != 0)
				widthSpacing /= 2;

			_shopContentCardsCoordsAndSizes = new int[cardTextures.Count, 4];
			_shopContentCardsTypes = new CardType[cardTextures.Count];
			
			GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(_shopWindowComponentGUI.Content);
			GraphicsDeviceManager.GraphicsDevice.Clear(Color.Transparent);
			_spriteBatch.Begin();

			for (int i = 0; i < cardTextures.Count; i++)
			{
				_spriteBatch.Draw(
					cardTextures[i],
					new Rectangle(
						(int)(-widthSpacing + _shopScrollValue + cardCoordinates[i].X - cardSizes[i].X - cardSpacing),
						(int)cardCoordinates[i].Y,
						(int)(cardSizes[i].X + 6),
						(int)(cardSizes[i].Y + 4)
					),
					new Color(0.5f, 0.5f, 0.5f)
				);
				
				_shopContentCardsCoordsAndSizes[i, 0] = (int)(-widthSpacing + _shopScrollValue + cardCoordinates[i].X - cardSizes[i].X - cardSpacing);
				_shopContentCardsCoordsAndSizes[i, 1] = (int)cardCoordinates[i].Y;
				_shopContentCardsCoordsAndSizes[i, 2] = (int)cardSizes[i].X;
				_shopContentCardsCoordsAndSizes[i, 3] = (int)cardSizes[i].Y;
				
				_spriteBatch.Draw(
					cardTextures[i],
					new Rectangle(
						(int)(-widthSpacing + _shopScrollValue + cardCoordinates[i].X - cardSizes[i].X - cardSpacing),
						(int)cardCoordinates[i].Y,
						(int)cardSizes[i].X,
						(int)cardSizes[i].Y
					),
					Color.White
				);
			}

			_shopContentCardsTypes = cardTypes.ToArray();

			_spriteBatch.End();
			GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
		}
	}
}