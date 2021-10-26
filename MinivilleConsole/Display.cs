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
            Console.WriteLine("Voulez-vous acheter un batiment (Buy) ou faire des economies () ");
        }

        public static void EconomyDisplay()
        {
            Console.WriteLine("Vous faites des economies \n");
        }

        public static void AskCardDisplay()
        {
            Console.WriteLine("Quel carte voulez-vous achetez (un nombre est attendue)");
        }

        public static void CardStackDisplay(CardStack stack)
        {
            string toString = "";
            int wheatField = stack.GetCardCount(CardType.WheatField);
            toString += $"(1) Il reste {wheatField} champs de blé, cela coute 1\n";
            
            int farm = stack.GetCardCount(CardType.Farm);
            toString += $"(2) Il reste {farm} ferme, cela coute 2 \n";
            
            int bakery = stack.GetCardCount(CardType.Bakery);
            toString += $"(3) Il reste {bakery} boulangerie, cela coute 1 \n";
            
            int coffeeShop = stack.GetCardCount(CardType.CoffeeShop);
            toString += $"(4) Il reste {coffeeShop} café, cela coute 2\n";
            
            int groceryStore = stack.GetCardCount(CardType.GroceryStore);
            toString += $"(5) Il reste {groceryStore} superette, cela coute 2\n";
            
            int forest = stack.GetCardCount(CardType.Forest);
            toString += $"(6) Il reste {forest} foret, cela coute 2\n";
            
            int restaurant = stack.GetCardCount(CardType.Restaurant);
            toString += $"(7) Il reste {restaurant} restaurant, cela coute 4\n";
            
            int stadium = stack.GetCardCount(CardType.Stadium);
            toString += $"(8) Il reste {stadium} stade, cela coute 6 \n";
            Console.WriteLine(toString);
        }

        public static void CardBuyDisplay(CardType card)
        {
            Console.WriteLine($"Vous acheter un(e) {card}");
        }

        public static void WalletDisplay(Player player)
        {
            Console.WriteLine($"Vous avez {player.Wallet} piece dans votre porte monnaie");
        }
    }
}