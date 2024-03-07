using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Linq;
using System;

namespace MinivilleGUI.Components
{
	public class CoinsHolderComponentGUI : ComponentGUI
	{
		public static Dictionary<string, Texture2D> Textures;

		
		private int _borderRight;
		protected override int BorderRight => _borderRight;
		
		private int _borderLeft;
		protected override int BorderLeft => _borderLeft;
		
		private int _borderTop;
		protected override int BorderTop => _borderTop;
		
		private int _borderBottom;
		protected override int BorderBottom => _borderBottom;

		
		public static int CoinsRadius = 30;
		public static int CoinsSpacing = 6;

		private int _coinsRadius = -1;
		private int _coinsSpacing = -1;

		public bool Visible = true;

		private int _value = -1;
		private string[] _coins;
		public int Value
		{
			set
			{
				value = Math.Max(0, value);
				
				if (_value == value) return;

				_value = value;
				
				List<string> coins = new List<string>();

				// 10 Coins
				int nb10Coins = value / 10;
				
				for (int i = 0; i < nb10Coins; i++)
				{
					coins.Add("10");
					value -= 10;
				}

				// 5 Coin
				if (value >= 5)
				{
					coins.Add("5");
					value -= 5;
				}
			
				// 1 Coins
				for (int i = 0; i < value; i++)
					coins.Add("1");

				_coins = coins.Count == 0 ? Array.Empty<string>() : coins.ToArray();

				UpdateCoinsPositions();
			}
			get
			{
				return _value;
			}
		}
		
		private Vector2[] _coinsPositions;
		private void UpdateCoinsPositions()
		{
			List<Vector2> coinsPositions = new List<Vector2>();
			
			int coinLayerIndex = 0;
			int coinsPerLayer = 1;
			int circleRadius = 0;
			
			for (int i = 0; i < _coins.Length; i++)
			{
				float angle = (float)(coinLayerIndex / (float)coinsPerLayer * 2f * Math.PI);
				
				Vector2 positionOnCircle = new Vector2(
					(float)(Math.Cos(angle) * circleRadius),
					-(float)(Math.Sin(angle) * circleRadius)
				);
				
				coinsPositions.Add(positionOnCircle);
				++coinLayerIndex;
				
				if (coinLayerIndex >= coinsPerLayer)
				{
					circleRadius += CoinsRadius * 2 + CoinsSpacing;
					coinLayerIndex = 0;
					coinsPerLayer = (int)((2f * Math.PI * circleRadius) / (CoinsRadius * 2 + CoinsSpacing));
				}
			}

			_borderLeft = _borderRight = _borderTop = _borderBottom = (int)Enumerable.Max(
				Enumerable.Concat(
					Enumerable.Concat(
						Enumerable.Concat(
							coinsPositions.Select(coinPosition => -coinPosition.X),
							coinsPositions.Select(coinPosition => coinPosition.X)
						),
						Enumerable.Concat(
							coinsPositions.Select(coinPosition => -coinPosition.Y),
							coinsPositions.Select(coinPosition => coinPosition.Y)
						)
					),
					new[] { 0f }
				)
			);

			_coinsPositions = coinsPositions.Count == 0 ? Array.Empty<Vector2>() : coinsPositions.ToArray();
		}
		

		public CoinsHolderComponentGUI(int value, SnapMode corner, Vector2 startPosition) : base(corner,
			startPosition)
		{
			Value = value;
		}

		public override void Update(double deltaTime)
		{
			if (CoinsRadius != _coinsRadius || CoinsSpacing != _coinsSpacing)
			{
				_coinsRadius = CoinsRadius;
				_coinsSpacing = CoinsSpacing;
				
				UpdateCoinsPositions();
			}

			base.Update(deltaTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!Visible) return;

			for (var index = 0; index < _coins.Length; index++)
			{
				spriteBatch.Draw(Textures[_coins[index]], new Rectangle((DisplayPosition + _coinsPositions[index] - new Vector2(CoinsRadius, CoinsRadius)).ToPoint(), new Point(CoinsRadius * 2, CoinsRadius * 2)), Color.White);
			}
		}

		public override int ZIndex => 1;
	}
}