using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MinivilleGUI.Components
{
	public class ComponentsManagerGUI
	{
		public static int Width { private set; get; }
		public static int Height { private set; get; }
		
		public static void ResizeWindow(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public static Vector2 GetWindowCornerCoordinates(SnapMode snapMode)
		{
			Vector2 cornerPosition = Vector2.Zero;

			switch (snapMode)
			{
				case SnapMode.Top:
				case SnapMode.Bottom:
					cornerPosition.X = Width / 2;
					break;
				case SnapMode.TopRight:
				case SnapMode.Right:
				case SnapMode.BottomRight:
					cornerPosition.X = Width;
					break;
			}
			
			switch (snapMode)
			{
				case SnapMode.Left:
				case SnapMode.Right:
					cornerPosition.Y = Height / 2;
					break;
				case SnapMode.BottomLeft:
				case SnapMode.Bottom:
				case SnapMode.BottomRight:
					cornerPosition.Y = Height;
					break;
			}

			return cornerPosition;
		}
		
		public List<ComponentGUI> Components = new List<ComponentGUI>();
		public Stack<ComponentGUI> ComponentsToAdd = new Stack<ComponentGUI>();

		public void Update(double deltaTime)
		{
			while (ComponentsToAdd.Count > 0)
			{
				Components.Add(ComponentsToAdd.Pop());
			}
			
			foreach (ComponentGUI component in Components)
				component.Update(deltaTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (ComponentGUI component in OrderedComponentsDraw())
				component.Draw(spriteBatch);
		}
		
		private IEnumerable<ComponentGUI> OrderedComponentsDraw()
		{
			foreach (ComponentGUI component in Components.OrderBy(component => component.DisplayPosition.Y + component.ZIndex * 10000))
				yield return component;
		}
	}
}