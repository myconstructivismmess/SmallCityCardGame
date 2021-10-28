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

        public static void TurnDisplay(Player player)
        {
            Console.WriteLine($"C'est à {player.Name} de jouer. \n");
        }
        
        public static void DiceDisplay(Dice gameDice)
        {
            Console.WriteLine(gameDice.ToString());
        }

        public static void AllTransactionDisplay(int gain, int loss, int opponentGain, int opponentLoss, Player player, Player opponent)
        {
            Console.WriteLine($"{player.Name} gagne {gain}, {player.Name} perd {loss}, {opponent.Name} gagne {opponentGain}, {opponent.Name} perd {opponentLoss}.\n");
        }

        public static void AskDisplay()
        {
            Console.WriteLine("Voulez-vous acheter un batiment (\"Buy\") ou faire des economies () ?");
        }

        public static void EconomyDisplay(Player player)
        {
            Console.WriteLine($"{player.Name} fait des economies. \n");
        }

        public static void AskCardDisplay()
        {
            Console.WriteLine("Quel carte voulez-vous achetez (un nombre est attendue). ");
        }

        public static void CardStackDisplay(CardStack stack, Player player)
        {
            string toString = "(0) Économiser.\n";
            int count = 0;

            count = stack.GetCardCount(CardType.WheatField);
            if (count > 0)
                toString += $"(1) Il reste {count} champs de blé, cela coute 1 piece.\n";
            
            count = stack.GetCardCount(CardType.Farm);
            if (count > 0)
                toString += $"(2) Il reste {count} ferme, cela coute 1 piece.\n";
            
            count = stack.GetCardCount(CardType.Bakery);
            if (count > 0)
                toString += $"(3) Il reste {count} boulangerie, cela coute 1 piece.\n";
            
            count = stack.GetCardCount(CardType.CoffeeShop);
            if (count > 0)
                toString += $"(4) Il reste {count} café, cela coute 2 pieces.\n";
            
            count = stack.GetCardCount(CardType.GroceryStore);
            if (count > 0)
                toString += $"(5) Il reste {count} superette, cela coute 2 pieces.\n";
            
            count = stack.GetCardCount(CardType.Forest);
            if (count > 0)
                toString += $"(6) Il reste {count} foret, cela coute 3 pieces.\n";
            
            count = stack.GetCardCount(CardType.BusinessCenter);
            if (count > 0)
                toString += $"(7) Il reste {count} centre d'affaires, cela coute 8 pieces.\n";
            
            count = stack.GetCardCount(CardType.Stadium);
            if (count > 0)
                toString += $"(8) Il reste {count} stade, cela coute 6 pieces.\n";
            
            count = stack.GetCardCount(CardType.TelevisionChannel);
            if (count > 0)
                toString += $"(9) Il reste {count} chaines de télé, cela coute 7 pieces.\n";
            
            count = stack.GetCardCount(CardType.CheeseShop);
            if (count > 0)
                toString += $"(10) Il reste {count} fromagerie, cela coute 5 pieces.\n";
            
            count = stack.GetCardCount(CardType.FurnitureShop);
            if (count > 0)
                toString += $"(11) Il reste {count} fabrique de meubles, cela coute 3 pieces.\n";
            
            count = stack.GetCardCount(CardType.Mine);
            if (count > 0)
                toString += $"(12) Il reste {count} mine, cela coute 6 pieces.\n";
            
            count = stack.GetCardCount(CardType.Restaurant);
            if (count > 0)
                toString += $"(13) Il reste {count} restaurant, cela coute 3 pieces.\n";
            
            count = stack.GetCardCount(CardType.Orchard);
            if (count > 0)
                toString += $"(14) Il reste {count} verger, cela coute 3 pieces.\n";
            
            count = stack.GetCardCount(CardType.Market);
            if (count > 0)
                toString += $"(15) Il reste {count} market, cela coute 2 pieces.\n\n";
            
            if (!player.Monuments[0].Build)
                toString += $"(16) Achetez la Gare !\n";
            if (!player.Monuments[1].Build)
                toString += $"(17) Achetez le Centre Commercial !\n";
            if (!player.Monuments[2].Build)
                toString += $"(18) Achetez la Tour Radio !\n";
            if (!player.Monuments[3].Build)
                toString += $"(19) Achetez le Parc d'Attraction !\n";
                

            Console.WriteLine(toString);
        }

        public static void CardBuyDisplay(Player player,CardType card)
        {
            Console.WriteLine($"{player.Name} achete un(e) {card}.");
        }

        public static void CardMissingDisplay(CardType card)
        {
            Console.WriteLine($"{card} n'est plus dans le paquet.");
            Console.ReadLine();
        }

        public static void PlayerIsPoor()
        {
            Console.WriteLine($"Vous n'avez pas l'argent requis.");
            Console.ReadLine();
        }
        
        public static void PlayerIsRich()
        {
            Console.WriteLine($"Vous avez déjà cette carte.");
            Console.ReadLine();
        }

        public static void WalletDisplay(Player player)
        {
            Console.WriteLine($"{player.Name} à {player.Wallet} piece dans votre porte monnaie.\n");
        }

        public static void PlayerVictoryDisplay(Player player)
        {
            Console.WriteLine($"{player.Name} à gagner il a réussit à amasser {player.Wallet} pieces.");
        }
        
        public static void ComputerVictoryDisplay(Player player)
        {
            Console.WriteLine($"{player.Name}à gagner il a réussit à amasser {player.Wallet} pieces.");
        }
        
        public static void EqualityDisplay(Player player, Player computer)
        {
            Console.WriteLine($"La partie s'arrête sur un match nul entre {player.Name} qui a amassé {player.Wallet}pieces. \n " +
                              $"Et l'odinateur qui a amassé {computer.Wallet} pieces.");
        }

        public static void DiceAskDisplay()
        {
            Console.WriteLine("Voulez-vous lancer 1 ou 2 dés ??(un nombre est attendue)");
        }

        public static void RollAskDisplay()
        {
            Console.WriteLine("Voulez-vous relancer votre/vos dé(s) (1) Oui (2) Non??");
        }
    }
}