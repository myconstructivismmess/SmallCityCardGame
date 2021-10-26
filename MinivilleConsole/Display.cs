using System;
using Core;


namespace MinivilleConsole
{
    public class Display
    {
        public Display ()
        {
        }

        public static void DiceDisplay(Dice gameDice)
        {
            Console.WriteLine(gameDice.ToString());
        }

        public static void AllTransactionDisplay(int gain, int loss, int opponentGain)
        {
            Console.WriteLine($"Vous avez gagné [gain], vous avez perdu [loss], votre adversaire a gagné [opponentGain]\n");
        }

        public static void AskDisplay()
        {
            Console.WriteLine("Voulez-vous acheter un batiment (Buy) ou faire des economies ()");
        }

        public static void EconomyDisplay()
        {
            Console.WriteLine("Vous faites des economies ");
        }
    }
}