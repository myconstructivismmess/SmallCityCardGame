using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System;

namespace MinivilleGUI.Components
{
	public class TurnComponentGUI : ComponentGUI
	{
		public static Texture2D PlayerBackgroundTexture;
		public static Texture2D EnnemyBackgroundTexture;
		
		public static SpriteFont Font;
		public static float FontScale = 1f;
		public static int BorderWidth = 10;

		private Vector2 _position;
		private Vector2 _targetPosition;

		private bool _change;
		private float _timerChange;
		private float _timerChangeDuration = 2f;

		public bool PlayerTurn
		{
			get
			{
				return _playerTurn;
			}
			set
			{
				if (value != _playerTurn)
				{
					_playerTurn = value;

					_change = true;
					_timerChange = _timerChangeDuration;
				}
			}
		}
		private bool _playerTurn;

		public TurnComponentGUI(SnapMode snapMode, Vector2 startPosition) : base(snapMode, startPosition)
		{
			_targetPosition = _position = Vector2.Zero;
		}

		public override void Update(double deltaTime)
		{
			MouseState mouseState = Mouse.GetState();
			Vector2 textSize = Font.MeasureString(PlayerTurn ? "Tour du joueur" : "Tour de l'ordinateur") * FontScale;

			bool hovered = IsHovered(mouseState);
			
			_targetPosition = (hovered || _change) ? Vector2.Zero : SnapMode switch
			{
				SnapMode.TopLeft => new Vector2(-(textSize.X + 2 * BorderWidth + 10), 0),
				SnapMode.Left => new Vector2(-(textSize.X + 2 * BorderWidth + 10), 0),
				SnapMode.BottomLeft => new Vector2(-(textSize.X + 2 * BorderWidth + 10), 0),
	            
				SnapMode.TopRight => new Vector2(textSize.X + 2 * BorderWidth + 10, 0),
				SnapMode.Right => new Vector2(textSize.X + 2 * BorderWidth + 10, 0),
				SnapMode.BottomRight => new Vector2(textSize.X + 2 * BorderWidth + 10, 0),
	            
				SnapMode.Top => new Vector2(0, -(textSize.Y + BorderWidth * 2 + 10)),
				SnapMode.Bottom => new Vector2(0, textSize.Y + BorderWidth * 2 + 10),

				SnapMode.Free => Vector2.Zero,
				_ => Vector2.Zero
			};
			
			_position = (_targetPosition - _position) * 0.2f + _position;
			
			_timerChange -= (float)deltaTime;
			if (_timerChange <= 0)
                _change = false;

			base.Update(deltaTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 textSize = Font.MeasureString(PlayerTurn ? "Tour du joueur" : "Tour de l'ordinateur") * FontScale;

			Vector2 size = textSize + new Vector2(BorderWidth * 2, BorderWidth * 2);

			Vector2 drawPosition = DisplayPosition + _position + SnapMode switch
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
				PlayerTurn ? PlayerBackgroundTexture : EnnemyBackgroundTexture,
				new Rectangle(
					drawPosition.ToPoint(),
					size.ToPoint()
				),
				Color.White
			);
			
			spriteBatch.DrawString(
				Font,
				PlayerTurn ? "Tour du joueur" : "Tour de l'ordinateur",
				drawPosition + new Vector2(BorderWidth, BorderWidth),
				Color.White,
				0f,
				Vector2.Zero,
				FontScale,
				SpriteEffects.None,
				0f
			);
		}
		
		public bool IsHovered(MouseState mouseState)
		{
			if (mouseState.X < 0 || mouseState.Y < 0 ||
			    mouseState.X > ComponentsManagerGUI.Width || mouseState.Y > ComponentsManagerGUI.Height) return false;
			
			Vector2 textSize = Font.MeasureString(PlayerTurn ? "Tour du joueur" : "Tour de l'ordinateur") * FontScale;

			Vector2 size = textSize + new Vector2(BorderWidth * 2, BorderWidth * 2);

			Vector2 drawPosition = DisplayPosition + _position + SnapMode switch
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

			Vector2 p1 = drawPosition;
			Vector2 p2 = drawPosition + size;
			
			return p1.X < mouseState.X && mouseState.X < p2.X && p1.Y < mouseState.Y && mouseState.Y < p2.Y;
		}

		public override int ZIndex => 10;

		protected override float? MovementSpeed => null;
	}
}