using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

using MinivilleGUI.Components;

using Core;

namespace MinivilleGUI
{
	public class MinivilleGUI : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private int[] _windowSize = { 1000, 800 };
		
		// ReSharper disable once InconsistentNaming
		private ComponentsManagerGUI _componentsManagerGUI;
		
		// ReSharper disable once InconsistentNaming
		private List<CardComponentGUI> _playerCardsComponentsGUI;
		// ReSharper disable once InconsistentNaming
		private List<CardComponentGUI> _iaCardsComponentsGUI;
		
		// ReSharper disable once InconsistentNaming
		private CoinsHolderComponentGUI _playerCoinsHolderComponentGUI;
		// ReSharper disable once InconsistentNaming
		private CoinsHolderComponentGUI _iaCoinsHolderComponentGUI;

		// ReSharper disable once InconsistentNaming
		private SideButtonComponentGUI _passTurnButtonComponentGUI;
		// ReSharper disable once InconsistentNaming
		private SideButtonComponentGUI _buyCardsButtonComponentGUI;

		// ReSharper disable once InconsistentNaming
		private WindowComponentGUI _shopWindowComponentGUI;

		private GameGUI _game;

		public MinivilleGUI()
		{
			_game = new GameGUI();
			
			_graphics = new GraphicsDeviceManager(this);

			// Set Mouse Visibility
			IsMouseVisible = true;
			
			// Set Window Resizability
			Window.AllowUserResizing = true;
			
			// Set Assets Directory
			Content.RootDirectory = "Content";
			
			// Set Window Size
			_graphics.PreferredBackBufferWidth = _windowSize[0];
			_graphics.PreferredBackBufferHeight = _windowSize[1];
			
			// Set Components Managers Window Size
			ComponentsManagerGUI.ResizeWindow(_windowSize[0], _windowSize[1]);

			// Subscribe To Window Resizing Event
			Window.ClientSizeChanged += Window_ClientSizeChanged;
		}
		
		private void Window_ClientSizeChanged(object sender, EventArgs e)
		{
			_windowSize = new[] { Window.ClientBounds.Width, Window.ClientBounds.Height };
			
			// Set Window Size
			_graphics.PreferredBackBufferWidth = _windowSize[0];
			_graphics.PreferredBackBufferHeight = _windowSize[1];

			// Set Components Managers Window Size
			ComponentsManagerGUI.ResizeWindow(_windowSize[0], _windowSize[1]);

			_graphics.ApplyChanges();
		}

		protected override void Initialize()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			
			// Components Manager Initialization
			_componentsManagerGUI = new ComponentsManagerGUI();
			
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
			_componentsManagerGUI.Components.Add(_buyCardsButtonComponentGUI);

			_shopWindowComponentGUI = new WindowComponentGUI(SnapMode.Right, Vector2.Zero, 600, 400, "Test");
			_componentsManagerGUI.Components.Add(_shopWindowComponentGUI);
			
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Background Initialization
			_componentsManagerGUI.Components.Add(
				new RepeatedBackgroundTextureGUI(
					Content.Load<Texture2D>("Background")
				)
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
							j * spacing - (cardsNumbersLength - 1) / 2f * spacing,
							-150 + i * 30
						)
					);
				}
				
				++j;
			}
		}
		
		private void OnBuyCardsButtonPressed()
		{
			BakeryCardGUI card = new BakeryCardGUI(SnapMode.Bottom, new Vector2(0, 500));
			
			_componentsManagerGUI.ComponentsToAdd.Push(card);
			_playerCardsComponentsGUI.Add(card);
			
			SnapPlayerCards();

			_buyCardsButtonComponentGUI.Enabled = false;
		}
		
		private void OnPassTurnButtonPressed()
		{
			WheatFieldCardGUI card = new WheatFieldCardGUI(SnapMode.Bottom, new Vector2(0, 500));
			
			_componentsManagerGUI.ComponentsToAdd.Push(card);
			_playerCardsComponentsGUI.Add(card);
			
			SnapPlayerCards();
		}

		protected override void Update(GameTime gameTime)
		{
			_componentsManagerGUI.Update(gameTime.ElapsedGameTime.TotalSeconds);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			
			_spriteBatch.Begin();
			_componentsManagerGUI.Draw(_spriteBatch);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}