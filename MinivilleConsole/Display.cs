using System;
using System.Security.Cryptography;
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

        public static void AllTransactionDisplay(int gain, int loss, int opponentGain, Player player, Player opponent)
        {
            Console.WriteLine($"{player.Name} gagne {gain}, {player.Name} perd {loss}, {opponent.Name} gagne {opponentGain}\n");
        }

        public static void AskDisplay()
        {
            Console.WriteLine("Voulez-vous acheter un batiment (Buy) ou faire des economies () ");
        }

        public static void EconomyDisplay(Player player)
        {
            Console.WriteLine($"{player.Name} fait des economies \n");
        }

        public static void AskCardDisplay()
        {
            Console.WriteLine("Quel carte voulez-vous achetez (un nombre est attendue)");
        }

        public static void CardStackDisplay(CardStack stack)
        {
            string toString = "";
            int wheatField = stack.GetCardCount(CardType.WheatField);
            toString += $"(1) Il reste {wheatField} champs de blé, cela coute 1 piece\n";
            
            int farm = stack.GetCardCount(CardType.Farm);
            toString += $"(2) Il reste {farm} ferme, cela coute 2 pieces\n";
            
            int bakery = stack.GetCardCount(CardType.Bakery);
            toString += $"(3) Il reste {bakery} boulangerie, cela coute 1 piece\n";
            
            int coffeeShop = stack.GetCardCount(CardType.CoffeeShop);
            toString += $"(4) Il reste {coffeeShop} café, cela coute 2 pieces\n";
            
            int groceryStore = stack.GetCardCount(CardType.GroceryStore);
            toString += $"(5) Il reste {groceryStore} superette, cela coute 2 pieces\n";
            
            int forest = stack.GetCardCount(CardType.Forest);
            toString += $"(6) Il reste {forest} foret, cela coute 2 pieces\n";
            
            int restaurant = stack.GetCardCount(CardType.Restaurant);
            toString += $"(7) Il reste {restaurant} restaurant, cela coute 4 pieces\n";
            
            int stadium = stack.GetCardCount(CardType.Stadium);
            toString += $"(8) Il reste {stadium} stade, cela coute 6 pieces\n";
            Console.WriteLine(toString);
        }

        public static void CardBuyDisplay(Player player,CardType card)
        {
            Console.WriteLine($"{player.Name} achete un(e) {card}");
        }

        public static void WalletDisplay(Player player)
        {
            Console.WriteLine($"{player.Name} à {player.Wallet} piece dans votre porte monnaie");
        }

        public static void PlayerVictoryDisplay(Player player)
        {
            Console.WriteLine($"{player.Name} à gagner il a réussit à amasser {player.Wallet} pieces");
        }
        
        public static void ComputerVictoryDisplay(Player player)
        {
            Console.WriteLine($"{player.Name}rà gagner il a réussit à amasser {player.Wallet} pieces");
        }
        
        public static void EqualityDisplay(Player player, Player computer)
        {
            Console.WriteLine($"La partie s'arrête sur un match nul entre {player.Name} qui a amassé {player.Wallet}pieces \n " +
                              $"Et l'odinateur qui a amassé {computer.Wallet} pieces");
        }
    }
}