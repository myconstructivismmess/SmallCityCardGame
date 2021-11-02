using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components
{
	public abstract class ComponentGUI
	{
		// Display
		public virtual int ZIndex => 0;
		public Vector2 DisplayPosition { private set; get; }
		public Vector2 TargetDisplayPosition;
		
		// Snapping
		protected virtual float MovementSpeed => 0.99f;
		public Vector2 SnappedPosition;
		public SnapMode SnapMode;
		
		// Borders
		protected virtual int BorderRight => 0;
		protected virtual int BorderLeft => 0;
		protected virtual int BorderTop => 0;
		protected virtual int BorderBottom => 0;
		
		// Snapping Methods
		public void SnapTo(SnapMode snapMode, Vector2 snappedPosition)
		{
			SnapMode = snapMode;
			SnappedPosition = snappedPosition;
		}
		public void SnapTo(Vector2 snappedPosition)
		{
			SnappedPosition = snappedPosition;
		}

		// Constructors
		public ComponentGUI(SnapMode snapMode, Vector2 snappedPosition)
		{
			SnapMode = snapMode;
			SnappedPosition = snappedPosition;
			
			Vector2 offset = Vector2.Zero;

			offset.X = SnapMode switch
			{
				SnapMode.TopLeft => BorderLeft,
				SnapMode.Left => BorderLeft,
				SnapMode.BottomLeft => BorderLeft,
				SnapMode.TopRight => -BorderRight,
				SnapMode.Right => -BorderRight,
				SnapMode.BottomRight => -BorderRight,
				_ => offset.X
			};

			offset.Y = SnapMode switch
			{
				SnapMode.TopLeft => BorderTop,
				SnapMode.Top => BorderTop,
				SnapMode.TopRight => BorderTop,
				SnapMode.BottomLeft => -BorderBottom,
				SnapMode.Bottom => -BorderBottom,
				SnapMode.BottomRight => -BorderBottom,
				_ => offset.Y
			};
			
			DisplayPosition = TargetDisplayPosition = SnappedPosition + ComponentsManagerGUI.GetWindowCornerCoordinates(SnapMode) + offset;
		}

		// Update
		public virtual void Update(double deltaTime)
		{
			UpdatePosition((float)deltaTime);
		}
		private void UpdatePosition(float deltaTime)
		{
			Vector2 offset = Vector2.Zero;

			offset.X = SnapMode switch
			{
				SnapMode.TopLeft => BorderLeft,
				SnapMode.Left => BorderLeft,
				SnapMode.BottomLeft => BorderLeft,
				SnapMode.TopRight => -BorderRight,
				SnapMode.Right => -BorderRight,
				SnapMode.BottomRight => -BorderRight,
				_ => offset.X
			};

			offset.Y = SnapMode switch
			{
				SnapMode.TopLeft => BorderTop,
				SnapMode.Top => BorderTop,
				SnapMode.TopRight => BorderTop,
				SnapMode.BottomLeft => -BorderBottom,
				SnapMode.Bottom => -BorderBottom,
				SnapMode.BottomRight => -BorderBottom,
				_ => offset.Y
			};

			TargetDisplayPosition = SnappedPosition + ComponentsManagerGUI.GetWindowCornerCoordinates(SnapMode) + offset;
			DisplayPosition = Vector2.Lerp(DisplayPosition, TargetDisplayPosition, MovementSpeed * deltaTime);
		}
		
		// Drawing
		public abstract void Draw(SpriteBatch spriteBatch);
	}
}