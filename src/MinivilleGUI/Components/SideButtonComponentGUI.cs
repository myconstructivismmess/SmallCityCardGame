using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components
{
	public class SideButtonComponentGUI : ComponentGUI
	{
		public static Texture2D BackgroundTexture;
		public static SpriteFont Font;
		public static float FontScale = 1f;
		public static int BorderWidth = 10;

		
		public event ComponentGUIEvent Pressed;
		public event ComponentGUIEvent PressedElseWhere;

		public bool Enabled = true;
		

		private bool _pressed;
		
		private readonly int _stripWidth;
		private readonly string _buttonName;
		private readonly bool _canRise;
		
		private float _tRise;
		private float _tRiseTarget;
		
		private float _tHighlight;
		private float _tHighlightTarget;

		public SideButtonComponentGUI(SnapMode snapMode, Vector2 startPosition, string buttonName, bool canRise = true, int stripWidth = 40) : base(snapMode, startPosition)
		{
			_buttonName = buttonName;
			_canRise = canRise;
			_tRiseTarget = _tRise = canRise ? 0f : 1f;
			_tHighlight = 0f;
			_tHighlightTarget = 0f;
			_stripWidth = stripWidth;
		}

		public override void Update(double deltaTime)
		{
			MouseState mouseState = Mouse.GetState();

			bool hovered = IsHovered(mouseState);

			if (_pressed != (mouseState.LeftButton == ButtonState.Pressed))
			{
				_pressed = mouseState.LeftButton == ButtonState.Pressed;

				if (_pressed)
				{
					if (hovered)
					{
						if (Enabled)
							Pressed?.Invoke();
					}
					else
					{
						PressedElseWhere?.Invoke();
					}
				}
			}
			
			_tRiseTarget = _canRise && !hovered ? 0f : 1f;
			_tHighlightTarget = Enabled ? (hovered ? 1f : 0f) : 0.6f;
			
			_tRise = (_tRiseTarget - _tRise) * 0.1f + _tRise;
			_tHighlight = (_tHighlightTarget - _tHighlight) * 0.1f + _tHighlight;
			
			base.Update(deltaTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 textSize = Font.MeasureString(_buttonName) * FontScale;

			Vector2 buttonSize = Vector2.One * BorderWidth * 2
			                     + textSize;

			Vector2 drawPosition = DisplayPosition + SnapMode switch
			{
				SnapMode.TopLeft => new Vector2(0, -((buttonSize.Y - _stripWidth) * (1 - _tRise))),
				SnapMode.Left => new Vector2(-((buttonSize.X - _stripWidth) * (1 - _tRise)), -buttonSize.Y / 2f),
				SnapMode.BottomLeft => new Vector2(0, -(buttonSize.Y - _stripWidth) * _tRise - _stripWidth),
				SnapMode.Top => new Vector2(-buttonSize.X / 2f, -((buttonSize.Y - _stripWidth) * (1 - _tRise))),
				SnapMode.Free => new Vector2(-buttonSize.X / 2f, -buttonSize.Y / 2f),
				SnapMode.Bottom => new Vector2(-buttonSize.X / 2f, -(buttonSize.Y - _stripWidth) * _tRise - _stripWidth),
				SnapMode.TopRight => new Vector2(-buttonSize.X, -((buttonSize.Y - _stripWidth) * (1 - _tRise))),
				SnapMode.Right => new Vector2(-_stripWidth - (buttonSize.X - _stripWidth) * _tRise, -buttonSize.Y / 2f),
				SnapMode.BottomRight => new Vector2(-buttonSize.X, -(buttonSize.Y - _stripWidth) * _tRise - _stripWidth),
				_ => new Vector2(0, 0)
			};

			float c = 1 - _tHighlight;
			
			spriteBatch.Draw(
				BackgroundTexture,
				new Rectangle(
					drawPosition.ToPoint(),
					buttonSize.ToPoint()
				),
				new Color(c, c, c)
			);
			
			spriteBatch.DrawString(
				Font,
				_buttonName,
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
			
			Vector2 textSize = Font.MeasureString(_buttonName) * FontScale;

			Vector2 buttonSize = Vector2.One * BorderWidth * 2
			                     + textSize;

			Vector2 drawPosition = DisplayPosition + SnapMode switch
			{
				SnapMode.TopLeft => new Vector2(0, -((buttonSize.Y - _stripWidth) * (1 - _tRise))),
				SnapMode.Left => new Vector2(-((buttonSize.X - _stripWidth) * (1 - _tRise)), -buttonSize.Y / 2f),
				SnapMode.BottomLeft => new Vector2(0, -(buttonSize.Y - _stripWidth) * _tRise - _stripWidth),
				SnapMode.Top => new Vector2(-buttonSize.X / 2f, -((buttonSize.Y - _stripWidth) * (1 - _tRise))),
				SnapMode.Free => new Vector2(-buttonSize.X / 2f, -buttonSize.Y / 2f),
				SnapMode.Bottom => new Vector2(-buttonSize.X / 2f, -(buttonSize.Y - _stripWidth) * _tRise - _stripWidth),
				SnapMode.TopRight => new Vector2(-buttonSize.X, -((buttonSize.Y - _stripWidth) * (1 - _tRise))),
				SnapMode.Right => new Vector2(-_stripWidth - (buttonSize.X - _stripWidth) * _tRise, -buttonSize.Y / 2f),
				SnapMode.BottomRight => new Vector2(-buttonSize.X, -(buttonSize.Y - _stripWidth) * _tRise - _stripWidth),
				_ => new Vector2(0, 0)
			};

			Vector2 p1 = drawPosition;
			Vector2 p2 = drawPosition + buttonSize;
			
			return p1.X < mouseState.X && mouseState.X < p2.X && p1.Y < mouseState.Y && mouseState.Y < p2.Y;
		}

		public override int ZIndex => 10;

		protected override float? MovementSpeed => null;
	}
}