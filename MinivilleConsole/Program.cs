using System;
using Core;

namespace MinivilleConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
			Console.WriteLine("Quelle est votre nom ?");
            var g = new Game(Console.ReadLine());
            g.Run();
        }
    }
}