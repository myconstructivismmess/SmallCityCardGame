using System;

namespace MinivilleGUI
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new MinivilleGUI())
				game.Run();
		}
	}
}