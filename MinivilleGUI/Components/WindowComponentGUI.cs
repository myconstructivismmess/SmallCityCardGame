using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MinivilleGUI.Components
{
	public class WindowComponentGUI : ComponentGUI
	{
		public static Texture2D BackgroundTexture;
		public static SpriteFont Font;
		public static float FontScale = 1f;
		public static int BorderWidth = 10;

		public bool Open = true;
		public int Width;
		public int Height;

		private string _windowName;
		
		public WindowComponentGUI(SnapMode snapMode, Vector2 snappedPosition, int width, int height, string windowName = "") : base(snapMode, snappedPosition)
		{
			Width = width;
			Height = height;
			_windowName = windowName;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!Open) return;
			Vector2 windowNameSize = Font.MeasureString(_windowName) * FontScale;
			
			Vector2 windowSize = new Vector2(Width, Height + BorderWidth * 2 + windowNameSize.Y);

			Vector2 drawPosition = DisplayPosition + SnapMode switch
			{
				SnapMode.TopLeft => new Vector2(0, 0),
				SnapMode.Left => new Vector2(0, -windowSize.Y / 2f),
				SnapMode.BottomLeft => new Vector2(0, -windowSize.Y),
				SnapMode.Top => new Vector2(-windowSize.X / 2f, 0),
				SnapMode.Free => new Vector2(-windowSize.X / 2f, -windowSize.Y / 2f),
				SnapMode.Bottom => new Vector2(-windowSize.X / 2f, -windowSize.Y),
				SnapMode.TopRight => new Vector2(-windowSize.X, 0),
				SnapMode.Right => new Vector2(-windowSize.X, -windowSize.Y / 2f),
				SnapMode.BottomRight => new Vector2(-windowSize.X, -windowSize.Y),
				_ => new Vector2(0, 0)
			};

			spriteBatch.Draw(
				BackgroundTexture,
				new Rectangle(
					drawPosition.ToPoint(),
					windowSize.ToPoint()
				),
				Color.White
			);
		}
		
		/*private bool IsHovered(MouseState mouseState)
		{
			Vector2 textSize = Font.MeasureString(_buttonName) * FontScale;

			Vector2 buttonSize = Vector2.One * BorderWidth * 2
			                     + textSize;

			Vector2 drawPosition = DisplayPosition + SnapMode switch
			{
				SnapMode.TopLeft => new Vector2(0, -((buttonSize.Y - StripWidth) * (1 - _tRise))),
				SnapMode.Left => new Vector2(-((buttonSize.X - StripWidth) * (1 - _tRise)), -buttonSize.Y / 2f),
				SnapMode.BottomLeft => new Vector2(0, -(buttonSize.Y - StripWidth) * _tRise - StripWidth),
				SnapMode.Top => new Vector2(-buttonSize.X / 2f, -((buttonSize.Y - StripWidth) * (1 - _tRise))),
				SnapMode.Free => new Vector2(-buttonSize.X / 2f, -buttonSize.Y / 2f),
				SnapMode.Bottom => new Vector2(-buttonSize.X / 2f, -(buttonSize.Y - StripWidth) * _tRise - StripWidth),
				SnapMode.TopRight => new Vector2(-buttonSize.X, -((buttonSize.Y - StripWidth) * (1 - _tRise))),
				SnapMode.Right => new Vector2(-StripWidth - (buttonSize.X - StripWidth) * _tRise, -buttonSize.Y / 2f),
				SnapMode.BottomRight => new Vector2(-buttonSize.X, -(buttonSize.Y - StripWidth) * _tRise - StripWidth),
				_ => new Vector2(0, 0)
			};

			Vector2 p1 = drawPosition;
			Vector2 p2 = drawPosition + buttonSize;
			
			return p1.X < mouseState.X && mouseState.X < p2.X && p1.Y < mouseState.Y && mouseState.Y < p2.Y;
		}*/

		public override int ZIndex => 11;

		protected override float? MovementSpeed => null;
	}
}