using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System;

namespace MinivilleGUI.Components
{
	public class SideDiceSelectorComponentGUI : ComponentGUI
	{
		public static Texture2D BackgroundTexture;
		public static SpriteFont Font;
		public static float FontScale = 1f;
		public static int BorderWidth = 10;


		public bool Enabled = true;
		
		public bool TwoDice = false;
		
		
		private bool _pressed;

		private Vector2 _position;
		private Vector2 _targetPosition;

		public SideDiceSelectorComponentGUI(SnapMode snapMode, Vector2 startPosition) : base(snapMode, startPosition)
		{
			_position = _targetPosition = new Vector2(-100, 0);
		}

		public override void Update(double deltaTime)
		{
			Vector2 textSize = Font.MeasureString("Nombre de des") * FontScale;
			Vector2 textSize1 = Font.MeasureString("1") * FontScale;
			Vector2 textSize2 = Font.MeasureString("2") * FontScale;

			Vector2 size = new Vector2(textSize.X + BorderWidth * 2,
				textSize.Y + Math.Max(textSize1.Y, textSize2.Y) + BorderWidth * 3);
			
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
			
			MouseState mouseState = Mouse.GetState();
			bool hovered = IsHovered(mouseState);

			if (_pressed != (mouseState.LeftButton == ButtonState.Pressed))
			{
				_pressed = mouseState.LeftButton == ButtonState.Pressed;

				if (_pressed && hovered && Enabled)
				{
					Vector2 p1 = drawPosition +
					              new Vector2(0, textSize.Y + BorderWidth * 1.5f);
					Vector2 p2 = p1 + new Vector2(size.X / 2f, size.Y - BorderWidth * 1.5f - textSize.Y);
					
					Vector2 p3 = drawPosition +
					             new Vector2(size.X / 2f, textSize.Y + BorderWidth * 1.5f);
					Vector2 p4 = p3 + new Vector2(size.X / 2f, size.Y - BorderWidth * 1.5f - textSize.Y);
					
					if (mouseState.X > p1.X && mouseState.X < p2.X && mouseState.Y > p1.Y && mouseState.Y < p2.Y)
                    {
                        TwoDice = false;
                    }
                    else if (mouseState.X > p3.X && mouseState.X < p4.X && mouseState.Y > p3.Y && mouseState.Y < p4.Y)
                    {
                        TwoDice = true;
                    }
				}
			}

			// Todo : Add other snap modes
			_targetPosition = Enabled ? (hovered ? Vector2.Zero : new Vector2(-size.X + 10, 0)) : new Vector2(-(size.X + 10), 0);

			_position = (_targetPosition - _position) * 0.2f + _position;

			base.Update(deltaTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 textSize = Font.MeasureString("Nombre de des") * FontScale;
			Vector2 textSize1 = Font.MeasureString("1") * FontScale;
			Vector2 textSize2 = Font.MeasureString("2") * FontScale;

			Vector2 size = new Vector2(textSize.X + BorderWidth * 2,
				textSize.Y + Math.Max(textSize1.Y, textSize2.Y) + BorderWidth * 3);

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
				BackgroundTexture,
				new Rectangle(
					drawPosition.ToPoint(),
					size.ToPoint()
				),
				Color.White
			);
			
			spriteBatch.Draw(
				BackgroundTexture,
				new Rectangle(
					(drawPosition + new Vector2(0, textSize.Y + BorderWidth * 1.5f)).ToPoint(),
					new Vector2(size.X, size.Y - BorderWidth * 1.5f - textSize.Y).ToPoint()
				),
				new Color(0.8f, 0.8f, 0.8f)
			);
			
			spriteBatch.Draw(
				BackgroundTexture,
				new Rectangle(
					(drawPosition + new Vector2(TwoDice ? size.X / 2f : 0, textSize.Y + BorderWidth * 1.5f)).ToPoint(),
					new Vector2(size.X / 2f, size.Y - BorderWidth * 1.5f - textSize.Y).ToPoint()
				),
				new Color(0.4f, 0.4f, 0.4f)
			);

			spriteBatch.DrawString(
				Font,
				"Nombre de des",
				drawPosition + new Vector2(BorderWidth, BorderWidth),
				Color.White,
				0f,
				Vector2.Zero,
				FontScale,
				SpriteEffects.None,
				0f
			);
			
			spriteBatch.DrawString(
				Font,
				"1",
				drawPosition + new Vector2(size.X / 4 - textSize2.X / 2, textSize1.Y + BorderWidth * 2),
				Color.White,
				0f,
				Vector2.Zero,
				FontScale,
				SpriteEffects.None,
				0f
			);
			
			spriteBatch.DrawString(
				Font,
				"2",
				drawPosition + new Vector2(size.X / 4 * 3 - textSize2.X / 2, textSize1.Y + BorderWidth * 2),
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
			
			Vector2 textSize = Font.MeasureString("Nombre de des") * FontScale;
			Vector2 textSize1 = Font.MeasureString("1") * FontScale;
			Vector2 textSize2 = Font.MeasureString("2") * FontScale;

			Vector2 size = new Vector2(textSize.X + BorderWidth * 2,
				textSize.Y + Math.Max(textSize1.Y, textSize2.Y) + BorderWidth * 3);

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