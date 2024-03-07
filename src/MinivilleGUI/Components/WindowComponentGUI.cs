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
		
		public RenderTarget2D Content;

		public bool Open = false;

		public int Width
		{
			get => _width;
			set
			{
				_width = value;
				Content = new RenderTarget2D(
					MinivilleGUI.GraphicsDeviceManager.GraphicsDevice,
					_width,
					_height
				);
			}
		}
		public int Height
		{
			get => _height;
			set
			{
				_height = value;
				Content = new RenderTarget2D(
					MinivilleGUI.GraphicsDeviceManager.GraphicsDevice,
					_width,
					_height
				);
			}
		}

		private string _windowName;
		private int _width;
		private int _height;

		private int _x;
		private int _targetX;
		
		public WindowComponentGUI(SnapMode snapMode, Vector2 snappedPosition, int width, int height, string windowName = "") : base(snapMode, snappedPosition)
		{
			_width = width;
			_height = height;
			_windowName = windowName;
			Content = new RenderTarget2D(
				MinivilleGUI.GraphicsDeviceManager.GraphicsDevice,
				_width,
				_height
			);
			_x = _targetX = _width + 10;
		}

		public override void Update(double deltaTime)
		{
			base.Update(deltaTime);

			_targetX = Open ? 0 : _width + 10;
			_x = (int)((_targetX - _x) * 0.2f + _x);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 windowNameSize = Font.MeasureString(_windowName) * FontScale;
			
			Vector2 windowSize = new Vector2(Width, Height + BorderWidth * 2 + windowNameSize.Y);

			Vector2 drawPosition = DisplayPosition + new Vector2(_x, 0) + SnapMode switch
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
			
			spriteBatch.Draw(
				BackgroundTexture,
				new Rectangle(
					drawPosition.ToPoint(),
					new Point((int)windowSize.X, BorderWidth * 2 + (int)windowNameSize.Y)
				),
				new Color(0.7f, 0.7f, 0.7f)
			);
			
			spriteBatch.DrawString(
				Font,
				_windowName,
				drawPosition + new Vector2(windowSize.X / 2f - windowNameSize.X / 2f, BorderWidth),
				Color.White,
				0f,
				Vector2.Zero,
				FontScale,
				SpriteEffects.None,
				0f
			);

			spriteBatch.Draw(
				Content,
				drawPosition + new Vector2(0, BorderWidth * 2 + windowNameSize.Y),
				Color.White
			);
		}
		
		public bool IsHovered(MouseState mouseState)
		{
			if (mouseState.X < 0 || mouseState.Y < 0 ||
			    mouseState.X > ComponentsManagerGUI.Width || mouseState.Y > ComponentsManagerGUI.Height) return false;

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

			Vector2 p1 = drawPosition;
			Vector2 p2 = drawPosition + windowSize;
			
			return p1.X < mouseState.X && mouseState.X < p2.X && p1.Y < mouseState.Y && mouseState.Y < p2.Y;
		}
		
		public Vector2 GetContentPosition()
		{
			Vector2 windowNameSize = Font.MeasureString(_windowName) * FontScale;
			
			Vector2 windowSize = new Vector2(Width, Height + BorderWidth * 2 + windowNameSize.Y);

			Vector2 drawPosition = DisplayPosition + new Vector2(_x, BorderWidth * 2 + windowNameSize.Y) + SnapMode switch
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

			return drawPosition;
		}

		public override int ZIndex => 11;

		protected override float? MovementSpeed => null;
	}
}