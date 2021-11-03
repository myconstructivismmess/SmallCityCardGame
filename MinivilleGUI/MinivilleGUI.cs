using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

using MinivilleGUI.Components;
using MinivilleGUI.Components.CardComponentGUI;
using MinivilleGUI.Components.IconComponentGUI;

using Core;

namespace MinivilleGUI
{
	public class MinivilleGUI : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private int[] _windowSize = { 1000, 800 };
		
		private ComponentsManagerGUI _componentsManagerGUI;
		
		private List<CardComponentGUI> _playerGUICardsComponents;
		private List<CardComponentGUI> _iaGUICardsComponents;
		
		private CoinsHolderComponentGUI _playerGUICoinsHolder;
		private CoinsHolderComponentGUI _iaGUICoinsHolder;

		private ShopIconComponentGUI _guiShopComponent;

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

		private void TestFunc()
		{
			BakeryCardGUI card = new BakeryCardGUI(SnapMode.Bottom, new Vector2(0, 500));
			
			_componentsManagerGUI.ComponentsToAdd.Push(card);
			_playerGUICardsComponents.Add(card);
			
			SnapPlayerCards();
		}

		protected override void Initialize()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			
			// Components Manager Initialization
			_componentsManagerGUI = new ComponentsManagerGUI();
			
			// Players Cards Initialization
			_playerGUICardsComponents = new List<CardComponentGUI>
			{
				new WheatFieldCardGUI(SnapMode.Bottom, new Vector2(0, 500)),
				new BakeryCardGUI(SnapMode.Bottom, new Vector2(0, 500))
			};
			
			SnapPlayerCards();

			foreach (CardComponentGUI card in _playerGUICardsComponents)
				_componentsManagerGUI.Components.Add(card);

			// IA Cards Initialization
			_iaGUICardsComponents = new List<CardComponentGUI>
			{
				new WheatFieldCardGUI(SnapMode.Top, new Vector2(0, -500)),
				new BakeryCardGUI(SnapMode.Top, new Vector2(0, -500))
			};
			
			SnapIaCards();

			foreach (CardComponentGUI card in _iaGUICardsComponents)
				_componentsManagerGUI.Components.Add(card);
			
			// Player Coins Holder Initialization
			_playerGUICoinsHolder = new CoinsHolderComponentGUI(3, SnapMode.BottomRight, new Vector2(500, 500));
			_playerGUICoinsHolder.SnapTo(new Vector2(-50, -50));
			_componentsManagerGUI.Components.Add(_playerGUICoinsHolder);
			
			// IA Coins Holder Initialization
			_iaGUICoinsHolder = new CoinsHolderComponentGUI(3, SnapMode.TopLeft, new Vector2(-500, -500));
			_iaGUICoinsHolder.SnapTo(new Vector2(50, 50));
			_componentsManagerGUI.Components.Add(_iaGUICoinsHolder);

			SideButtonComponentGUI bottomButton =
				new SideButtonComponentGUI(SnapMode.Bottom, Vector2.Zero, "Bottom", SideButtonDirection.Top);
			bottomButton.Pressed += TestFunc;
			
			_componentsManagerGUI.Components.Add(new SideButtonComponentGUI(SnapMode.Left, Vector2.Zero, "Left", SideButtonDirection.Right));
			_componentsManagerGUI.Components.Add(new SideButtonComponentGUI(SnapMode.Right, Vector2.Zero, "Right", SideButtonDirection.Left));
			_componentsManagerGUI.Components.Add(new SideButtonComponentGUI(SnapMode.Top, Vector2.Zero, "Top", SideButtonDirection.Bottom));
			_componentsManagerGUI.Components.Add(bottomButton);
			_componentsManagerGUI.Components.Add(new SideButtonComponentGUI(SnapMode.TopLeft, Vector2.Zero, "Top Left", SideButtonDirection.Right));
			_componentsManagerGUI.Components.Add(new SideButtonComponentGUI(SnapMode.TopRight, Vector2.Zero, "Top Right", SideButtonDirection.Left));
			_componentsManagerGUI.Components.Add(new SideButtonComponentGUI(SnapMode.BottomLeft, Vector2.Zero, "Bottom Left", SideButtonDirection.Bottom));
			_componentsManagerGUI.Components.Add(new SideButtonComponentGUI(SnapMode.BottomRight, Vector2.Zero, "Bottom Right", SideButtonDirection.Top));
			_componentsManagerGUI.Components.Add(new SideButtonComponentGUI(SnapMode.Free, Vector2.Zero, "Free", SideButtonDirection.Top));
			
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Background Initialization
			_componentsManagerGUI.Components.Add(new RepeatedBackgroundTextureGUI(Content.Load<Texture2D>("Background")));
			
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

			// Set Shop Icon Texture
			ShopIconComponentGUI.Texture = Content.Load<Texture2D>("Shop/Shop");

			SideButtonComponentGUI.BackgroundTexture = Content.Load<Texture2D>("SideButtonBackgroundTexture");
			SideButtonComponentGUI.Font = Content.Load<SpriteFont>("Fonts/Rajdhani-Medium");
			
			_guiShopComponent = new ShopIconComponentGUI(SnapMode.Left, new Vector2(-500, 0));
			_guiShopComponent.SnapTo(new Vector2(100, 0));
			_componentsManagerGUI.Components.Add(_guiShopComponent);
			
			//GUIShopComponent.MenuSelectorBackground = Content.Load<Texture2D>("Shop/MainMenuSelectorBackground");

			// GUICoinsHolderComponent.SpriteFont = Content.Load<SpriteFont>("Fonts/Roboto");
		}

		private void SnapIaCards()
		{
			int spacing = 210;

			Dictionary<string, List<int>> cardsNumbers = new Dictionary<string, List<int>>();
			int cardsNumbersLength = 0;

			for (int i = 0; i < _iaGUICardsComponents.Count; i++)
			{
				string cardName = _iaGUICardsComponents[i].CardName;
				
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
					_iaGUICardsComponents[cardsNumber.Value[i]].SnapTo(
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

			for (int i = 0; i < _playerGUICardsComponents.Count; i++)
			{
				string cardName = _playerGUICardsComponents[i].CardName;
				
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
					_playerGUICardsComponents[cardsNumber.Value[i]].SnapTo(
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

		protected override void Update(GameTime gameTime)
		{
			_componentsManagerGUI.Update(gameTime.ElapsedGameTime.TotalSeconds);

			CardType type = CardType.Bakery;

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