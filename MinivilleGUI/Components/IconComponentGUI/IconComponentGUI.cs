using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components.IconComponentGUI
{
	public delegate void IconComponentGUIEvent();
	
	public class IconComponentGUI : ComponentGUI
	{
		private Texture2D _texture;
		
		private float _scale, _targetScale;
		
		private readonly float _defaultScale, _hoveredScale;

		private bool _pressed;

		public event IconComponentGUIEvent Pressed;
		public event IconComponentGUIEvent PressedElseWhere;

		public IconComponentGUI(Texture2D texture, SnapMode snapMode, Vector2 position, float defaultScale, float hoveredScale) : base(snapMode, position)
		{
			_texture = texture;
			
			_defaultScale = defaultScale;
			_hoveredScale = hoveredScale;

			_targetScale = defaultScale;
			_scale = defaultScale;
		}

		public override void Update(double deltaTime)
		{
			base.Update(deltaTime);

			MouseState mouseState = Mouse.GetState();
			bool hovered = IsHovered(mouseState);

			if (_pressed != (mouseState.LeftButton == ButtonState.Pressed))
			{
				_pressed = mouseState.LeftButton == ButtonState.Pressed;
				
				if (_pressed)
					if (hovered)
						Pressed?.Invoke();
					else
						PressedElseWhere?.Invoke();
			}

			_targetScale = hovered ? _hoveredScale : _defaultScale;
			_scale = 0.3f * (_targetScale - _scale) + _scale;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			int width = (int)(_texture.Width * _scale);
			int height = (int)(_texture.Height * _scale);
			
			spriteBatch.Draw(
				_texture,
				new Rectangle(
					(DisplayPosition - new Vector2(width / 2, height / 2)).ToPoint(),
					new Point(width, height)
				),
				Color.White
			);
		}
		
		private bool IsHovered(MouseState mouseState)
		{
			int width = (int)(_texture.Width * _scale);
			int height = (int)(_texture.Height * _scale);

			return ((DisplayPosition.X - width / 2) < mouseState.X && (DisplayPosition.X + width / 2) > mouseState.X &&
			        (DisplayPosition.Y - height / 2) < mouseState.Y && (DisplayPosition.Y + height / 2) > mouseState.Y);
		}
	}
}