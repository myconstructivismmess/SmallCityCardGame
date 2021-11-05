using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components
{
	public delegate void DiceRolledEvent(int value);
	
	public class DiceComponentGUI : ComponentGUI
	{
		public static Texture2D[] DiceTextures;
		public static int DiceSize = 100;
		public static Texture2D BackgroundTexture;
		public static int BorderWidth = 40;

		public int Value => _rollValue;
		public event DiceRolledEvent Rolled;
		
		private bool _open;

		private bool _twoDice;

		private bool _roll;
		private float _rollDuration;
		private float _rollTimer;
		private int _rollValue = 1;
		private int _rollValue2 = 1;

		private bool _show;
		private float _showTimer = 0;
		private float _showDuration = 2f;

		private Vector2 _position;
		private Vector2 _targetPosition;
		
		public DiceComponentGUI(SnapMode snapMode, Vector2 snappedPosition) : base(snapMode, snappedPosition)
		{
			_targetPosition = _position = SnapMode switch
			{
				SnapMode.TopLeft => new Vector2(-(DiceSize + 2 * BorderWidth + 10), 0),
				SnapMode.Left => new Vector2(-(DiceSize + 2 * BorderWidth + 10), 0),
				SnapMode.BottomLeft => new Vector2(-(DiceSize + 2 * BorderWidth + 10), 0),
				
				SnapMode.TopRight => new Vector2(DiceSize + 2 * BorderWidth + 10, 0),
				SnapMode.Right => new Vector2(DiceSize + 2 * BorderWidth + 10, 0),
				SnapMode.BottomRight => new Vector2(DiceSize + 2 * BorderWidth + 10, 0),
				
				SnapMode.Top => new Vector2(0, -(DiceSize + 2 * BorderWidth + 10)),
				SnapMode.Bottom => new Vector2(0, DiceSize + 2 * BorderWidth + 10),
				
				SnapMode.Free => Vector2.Zero,
				_ => Vector2.Zero
			};
		}

		public override void Update(double deltaTime)
		{
			base.Update(deltaTime);
			
			_targetPosition = _open ? Vector2.Zero : SnapMode switch
			{
				SnapMode.TopLeft => new Vector2(-(DiceSize + 2 * BorderWidth + 10), 0),
				SnapMode.Left => new Vector2(-(DiceSize + 2 * BorderWidth + 10), 0),
				SnapMode.BottomLeft => new Vector2(-(DiceSize + 2 * BorderWidth + 10), 0),
				
				SnapMode.TopRight => new Vector2(DiceSize + 2 * BorderWidth + 10, 0),
				SnapMode.Right => new Vector2(DiceSize + 2 * BorderWidth + 10, 0),
				SnapMode.BottomRight => new Vector2(DiceSize + 2 * BorderWidth + 10, 0),
				
				SnapMode.Top => new Vector2(0, -(DiceSize + 2 * BorderWidth + 10)),
				SnapMode.Bottom => new Vector2(0, DiceSize + 2 * BorderWidth + 10),
				
				SnapMode.Free => Vector2.Zero,
				_ => Vector2.Zero
			};

			if (_roll)
			{
				_rollTimer += (float)deltaTime;

				_rollDuration += 0.12f * (float)deltaTime;
				
				if (_rollTimer >= _rollDuration)
                {
	                _rollValue = new Random().Next(1, 7);
	                if (_twoDice) _rollValue2 = new Random().Next(1, 7);
	                
	                _rollTimer = 0;
	                
	                if (_rollDuration >= 0.9f)
	                {
		                _roll = false;
		                _show = true;
	                }
                }
			}

			if (_show)
			{
				_showTimer += (float)deltaTime;

				if (_showTimer >= _showDuration)
				{
					_show = false;
					_showTimer = 0;
					_open = false;
					
					Rolled?.Invoke(_rollValue + (_twoDice ? _rollValue2 : 0));
				}
			}

			_position = (_targetPosition - _position) * 0.2f + _position;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 drawPosition = DisplayPosition + _position + SnapMode switch
			{
				SnapMode.Free => new Vector2(-(DiceSize + 2 * BorderWidth) / 2f, -(DiceSize + 2 * BorderWidth) / 2f),
				SnapMode.TopLeft => new Vector2(0, 0),
				SnapMode.Left => new Vector2(0, -(DiceSize + 2 * BorderWidth) / 2f),
				SnapMode.BottomLeft => new Vector2(0, -(DiceSize + 2 * BorderWidth)),
				SnapMode.TopRight => new Vector2(-(DiceSize + 2 * BorderWidth), 0),
				SnapMode.Right => new Vector2(-(DiceSize + 2 * BorderWidth), -(DiceSize + 2 * BorderWidth) / 2f),
				SnapMode.BottomRight => new Vector2(-(DiceSize + 2 * BorderWidth), -(DiceSize + 2 * BorderWidth)),
				SnapMode.Top => new Vector2(-(DiceSize + 2 * BorderWidth) / 2f, 0),
				SnapMode.Bottom => new Vector2(-(DiceSize + 2 * BorderWidth) / 2f, -(DiceSize + 2 * BorderWidth)),
				_ => new Vector2(0, 0)
			};

			spriteBatch.Draw(
				BackgroundTexture,
				new Rectangle(
					drawPosition.ToPoint(),
					new Point(DiceSize + 2 * BorderWidth, DiceSize + 2 * BorderWidth)
				),
				Color.White
			);

			if (_twoDice)
			{
				spriteBatch.Draw(
					DiceTextures[_rollValue - 1],
					new Rectangle(
						(drawPosition + new Vector2(BorderWidth, BorderWidth)).ToPoint(),
						new Point(DiceSize / 2, DiceSize / 2)
					),
					Color.White
				);
				
				spriteBatch.Draw(
					DiceTextures[_rollValue2 - 1],
					new Rectangle(
						(drawPosition + new Vector2(BorderWidth + DiceSize / 2, BorderWidth + DiceSize / 2)).ToPoint(),
						new Point(DiceSize / 2, DiceSize / 2)
					),
					Color.White
				);
			}
			else
			{
				spriteBatch.Draw(
					DiceTextures[_rollValue - 1],
					new Rectangle(
						(drawPosition + new Vector2(BorderWidth, BorderWidth)).ToPoint(),
						new Point(DiceSize, DiceSize)
					),
					Color.White
				);
			}
		}

		public void Roll(bool twoDice = false)
		{
			_twoDice = twoDice;
			
			_open = true;
			_roll = true;
			_rollDuration = 0.1f;
			_rollTimer = 0;
			_rollValue = 1;
		}

		public override int ZIndex => 12;

		protected override float? MovementSpeed => null;
	}
}