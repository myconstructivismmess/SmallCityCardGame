using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Core;


namespace MinivilleConsole
{
    public class Display
    {
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

        public static void EconomyDisplay(Player player)
        {
            Console.WriteLine($"{player.Name} fait des economies. \n");
        }

        public static void AskCardDisplay()
        {
            Console.WriteLine("Quel carte voulez-vous achetez (un nombre est attendue). ");
        }

        public static void ShopDisplay(List<Monument> monument,List<CardType> shop,CardStack stack )
        {
            int i = 1;
            string toString = "(0) Économiser.\n";
            foreach (var elem in shop)
            {
                toString += $"({i}) Il reste {stack.GetCardCount(elem)} {elem}, cela coute {stack.GetCard(elem).Cost} piece.\n";
                i++;
            }
            toString += "====================== ";
            foreach (var elem in monument)
            {
                toString += $"({i}) Achetez {elem.Name}, cela coute {elem.Cost} piece.\n";
                i++;
            } 
            Console.WriteLine(toString);
        }

        public static void CardBuyDisplay(Player player,CardType card)
        {
            Console.WriteLine($"{player.Name} achete un(e) {card}.");
        }

        public static void WalletDisplay(Player player)
        {
            Console.WriteLine($"{player.Name} à {player.Wallet} piece dans son porte monnaie.\n");
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

        public static void MonumentBuyDisplay(Player player,Monument monument)
        {
            Console.WriteLine($"{player.Name} a acheté le/la {monument.Name}");
        }
        
        public static void MonumentBuildDisplay(Player player)
        {
            string toString = $"{player.Name} à construit";
            foreach (var elem in player.Monuments)
            {
                if (elem.Build)
                {
                    toString += elem.Name+" ";
                }
            }
            Console.WriteLine(toString);
        }
    }
}