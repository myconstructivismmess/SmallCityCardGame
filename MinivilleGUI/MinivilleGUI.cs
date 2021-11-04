using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

using MinivilleGUI.Components;

namespace MinivilleGUI
{
	public class MinivilleGUI : Game
	{
		private static ComponentsManagerGUI _componentsManagerGUI;
		public static GraphicsDeviceManager GraphicsDeviceManager;
		private static GameGUI _game;
		private static SpriteBatch _spriteBatch;
		public static SpriteFont DebugFont;

		private Vector2 _windowSize = new Vector2(
			1000, // Initial Window Width
			800 // Initial Window Height
		);

		// Components
		private List<CardComponentGUI> _playerCardsComponentsGUI;
		private List<CardComponentGUI> _iaCardsComponentsGUI;

		private CoinsHolderComponentGUI _playerCoinsHolderComponentGUI;
		private CoinsHolderComponentGUI _iaCoinsHolderComponentGUI;
		
		private SideButtonComponentGUI _passTurnButtonComponentGUI;
		private SideButtonComponentGUI _buyCardsButtonComponentGUI;
		
		private WindowComponentGUI _shopWindowComponentGUI;
		private int _shopScrollValue;

		private int _scrollWheelValue;

		public MinivilleGUI()
		{
			_game = new GameGUI();
			
			_componentsManagerGUI = new ComponentsManagerGUI();
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
			
			// Components Manager Initialization

			// Players Cards Initialization
			_playerCardsComponentsGUI = new List<CardComponentGUI>
			{
				new WheatFieldCardGUI(SnapMode.Bottom, new Vector2(0, 500)),
				new BakeryCardGUI(SnapMode.Bottom, new Vector2(0, 500))
			};
			
			SnapPlayerCards();

			foreach (CardComponentGUI card in _playerCardsComponentsGUI)
				_componentsManagerGUI.Components.Add(card);

			// IA Cards Initialization
			_iaCardsComponentsGUI = new List<CardComponentGUI>
			{
				new WheatFieldCardGUI(SnapMode.Top, new Vector2(0, -500)),
				new BakeryCardGUI(SnapMode.Top, new Vector2(0, -500))
			};
			
			SnapIaCards();

			foreach (CardComponentGUI card in _iaCardsComponentsGUI)
				_componentsManagerGUI.Components.Add(card);
			
			// Player Coins Holder Initialization
			_playerCoinsHolderComponentGUI = new CoinsHolderComponentGUI(3, SnapMode.BottomRight, new Vector2(500, 500));
			_playerCoinsHolderComponentGUI.SnapTo(new Vector2(-50, -50));
			_componentsManagerGUI.Components.Add(_playerCoinsHolderComponentGUI);
			
			// IA Coins Holder Initialization
			_iaCoinsHolderComponentGUI = new CoinsHolderComponentGUI(3, SnapMode.TopLeft, new Vector2(-500, -500));
			_iaCoinsHolderComponentGUI.SnapTo(new Vector2(50, 50));
			_componentsManagerGUI.Components.Add(_iaCoinsHolderComponentGUI);

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

			_shopWindowComponentGUI = new WindowComponentGUI(SnapMode.Right, Vector2.Zero, (int)_windowSize.X - 350, 300, "Acheter une carte");
			_componentsManagerGUI.Components.Add(_shopWindowComponentGUI);

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
				{ "Wheat Field", Content.Load<Texture2D>("Cards/Wheat Field") },
				{ "Grocery Store", Content.Load<Texture2D>("Cards/Grocery Store") },
				{ "Bakery", Content.Load<Texture2D>("Cards/Bakery") },
				{ "Coffee", Content.Load<Texture2D>("Cards/Coffee") },
				{ "Forest", Content.Load<Texture2D>("Cards/Forest") },
				{ "Farm", Content.Load<Texture2D>("Cards/Farm") }
			};

			// Set Cards Fonts
			CardComponentGUI.TypeFont = Content.Load<SpriteFont>("Fonts/Rajdhani-SemiBold");
			CardComponentGUI.DescriptionFont = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");

			// Set Coins Textures
			CoinsHolderComponentGUI.Textures = new Dictionary<string, Texture2D>
			{
				{"1", Content.Load<Texture2D>("Coins/1")},
				{"5", Content.Load<Texture2D>("Coins/5")},
				{"10", Content.Load<Texture2D>("Coins/10")}
			};

			WindowComponentGUI.BackgroundTexture = Content.Load<Texture2D>("UI/BackgroundTexture");
			WindowComponentGUI.Font = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");
			
			SideButtonComponentGUI.BackgroundTexture = Content.Load<Texture2D>("UI/BackgroundTexture");
			SideButtonComponentGUI.Font = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");
			
			DebugFont = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");
		}

		private void SnapIaCards()
		{
			int spacing = 210;

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
							j * spacing - (cardsNumbersLength - 1) / 2f * spacing,
							150 + i * 30
						)
					);
				}
				
				++j;
			}
		}
		
		private void SnapPlayerCards()
		{
			int spacing = 210;

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
							-150 - i * 25
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
			if (!_shopWindowComponentGUI.IsHovered(Mouse.GetState()))
				_shopWindowComponentGUI.Open = false;
		}
		
		private void OnPassTurnButtonPressed()
		{
			Random random = new Random();
			
			CardComponentGUI card;
			
			if (random.NextDouble() > 0.5f)
				card = new WheatFieldCardGUI(SnapMode.Bottom, new Vector2(0, 500));
			else
				card = new BakeryCardGUI(SnapMode.Bottom, new Vector2(0, 500));

			_componentsManagerGUI.ComponentsToAdd.Push(card);
			_playerCardsComponentsGUI.Add(card);
			
			SnapPlayerCards();
			
			//_passTurnButtonComponentGUI.Enabled = false;
		}

		protected override void Update(GameTime gameTime)
		{
			_componentsManagerGUI.Update(gameTime.ElapsedGameTime.TotalSeconds);

			MouseState mouseState = Mouse.GetState();

			int scrollWheelValueDelta =
				mouseState.ScrollWheelValue + mouseState.HorizontalScrollWheelValue - _scrollWheelValue;

			_scrollWheelValue = mouseState.ScrollWheelValue + mouseState.HorizontalScrollWheelValue;

			if (_shopWindowComponentGUI.IsHovered(mouseState))
				_shopScrollValue += scrollWheelValueDelta / 2;

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

			int[,] coords = new int[CardComponentGUI.Textures.Values.Count, 2];
			int[,] sizes = new int[CardComponentGUI.Textures.Values.Count, 2];
			Texture2D[] textures = new Texture2D[CardComponentGUI.Textures.Values.Count];

			int width = 0;
			int index = 0;
			foreach (Texture2D texture in CardComponentGUI.Textures.Values)
			{
				textures[index] = texture;
				
				sizes[index, 1] = _shopWindowComponentGUI.Height - 2 * cardSpacing;
				sizes[index, 0] = (int)(texture.Width * (sizes[index, 1] / (float)texture.Height));

				width += sizes[index, 0] + cardSpacing;

				coords[index, 0] = cardSpacing + width;
				coords[index, 1] = cardSpacing;

				++index;
			}
			
			_shopScrollValue = Math.Min(0, _shopScrollValue);
			_shopScrollValue = Math.Max(-width + _shopWindowComponentGUI.Width - cardSpacing, _shopScrollValue);

			int widthSpacing = Math.Max((_shopWindowComponentGUI.Width - width), 0);
			if (widthSpacing != 0)
				widthSpacing /= 2;
			
			GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(_shopWindowComponentGUI.Content);
			GraphicsDeviceManager.GraphicsDevice.Clear(Color.Transparent);
			_spriteBatch.Begin();

			for (int i = 0; i < textures.GetLength(0); i++)
			{
				_spriteBatch.Draw(
					textures[i],
					new Rectangle(
						-widthSpacing + _shopScrollValue + coords[i, 0] - sizes[i, 0] - cardSpacing,
						coords[i, 1],
						sizes[i, 0] + 6,
						sizes[i, 1] + 4
					),
					new Color(0.5f, 0.5f, 0.5f)
				);
				
				_spriteBatch.Draw(
					textures[i],
					new Rectangle(
						-widthSpacing + _shopScrollValue + coords[i, 0] - sizes[i, 0] - cardSpacing,
						coords[i, 1],
						sizes[i, 0],
						sizes[i, 1]
					),
					Color.White
				);
			}

			_spriteBatch.End();
			GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
		}
	}
}