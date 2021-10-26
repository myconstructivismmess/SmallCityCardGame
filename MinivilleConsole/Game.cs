using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Core;

namespace MinivilleConsole
{
    public class Game : CoreGame
    {
        private static Random _random = new Random();
        public Game(string playerName) : base(playerName)
        {
            
        }

        public override void Run()
        {
            while (true)
            {
                //Human Turn
                HumanTurn();
                if (IsComputerWin() && IsPlayerWin())
                {
                    Display.EqualityDisplay(HumanPlayer,ComputerPlayer);
                }
                else if (IsPlayerWin())
                {
                    Display.PlayerVictoryDisplay(HumanPlayer);
                    break;
                }
                else if (IsComputerWin())
                {
                    Display.ComputerVictoryDisplay(ComputerPlayer);
                    break;
                }
                
                //Computer Turn
                ComputerTurn();
                
                if (IsComputerWin() && IsPlayerWin())
                {
                    Display.EqualityDisplay(HumanPlayer,ComputerPlayer);
                }
                else if (IsPlayerWin())
                {
                    Display.PlayerVictoryDisplay(HumanPlayer);
                    break;
                }
                else if (IsComputerWin())
                {
                    Display.ComputerVictoryDisplay(ComputerPlayer);
                    break;
                }
            }
        }

        public override void HumanTurn()
        {
            // Start the Human Turn
            var humanChoice = "";
            var gain = 0;
            var loss = 0;
            var opponentGain = 0;
            var tuple = new Tuple<int, int>(0, 0);
            CardType cardChoice;
            
            //Roll the Dice
            GameDice.Roll();
            
            // Display of the Dice
            Display.DiceDisplay(GameDice);
            
            // Card Activate Blue and Green
            gain += HumanPlayer.PlayerTurn(GameDice.Value);
            
            // Card Activate Red
            tuple = HumanPlayer.OpponentTurn(ComputerPlayer,GameDice.Value);
            opponentGain += tuple.Item1;
            loss += tuple.Item2;
            
            // Display win and loosing money
            Display.AllTransactionDisplay(gain,loss,opponentGain,HumanPlayer,ComputerPlayer);
            gain = 0;
            loss = 0;
            opponentGain = 0;
            
            // Display Wallet
            Display.WalletDisplay(HumanPlayer);
            // Display ask want to buy or saving money
            Display.AskDisplay();
            humanChoice = Console.ReadLine();
            
            // If card player want to buy a card
            if (humanChoice == "Buy")
            {
                while (true)
                {
                    // Display card remaining
                    Display.CardStackDisplay(Stack);
                    // Display what card do you want to buy
                    Display.AskCardDisplay();
                    humanChoice = Console.ReadLine();
                    try
                    {
                        if (int.Parse(humanChoice) >=1 && int.Parse(humanChoice) <= 8 )
                            break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Veuillez choisir un nombre valide");
                    }
                }
                //Add Card
                switch (humanChoice)
                {
                    case "1":
                        cardChoice = CardType.WheatField;
                        break;
                    case "2":
                        cardChoice = CardType.Farm;
                        break;
                    case "3":
                        cardChoice = CardType.Bakery;
                        break;
                    case "4":
                        cardChoice = CardType.CoffeeShop;
                        break;
                    case "5":
                        cardChoice = CardType.GroceryStore;
                        break;
                    case "6":
                        cardChoice = CardType.Forest;
                        break;
                    case "7":
                        cardChoice = CardType.Restaurant;
                        break;
                    case "8":
                        cardChoice = CardType.Stadium;
                        break;
                    default:
                        cardChoice = CardType.Bakery;
                        break;
                }
                HumanPlayer.BuyCard(Stack.PickCard(cardChoice));
                
                // Display card buy
                Display.CardBuyDisplay(HumanPlayer,cardChoice);
                // Display Wallet
                Display.WalletDisplay(HumanPlayer);
            }
            else
            {
                //Display economy
                Display.EconomyDisplay(HumanPlayer);
                // Display Wallet
                Display.WalletDisplay(HumanPlayer);
            }
        }

        public override void ComputerTurn()
        {
            // Phrase de début de tour de l'ia
            var gain = 0;
            var loss = 0;
            var opponentGain = 0;
            var tuple = new Tuple<int, int>(0, 0);
            int choose = 0;
            //Roll the Dice
            GameDice.Roll();
            // Display
            Display.DiceDisplay(GameDice);
            
            // Card Activate Blue and Green
            gain += ComputerPlayer.PlayerTurn(GameDice.Value);
            // Card Activate Red
            tuple = ComputerPlayer.OpponentTurn(HumanPlayer,GameDice.Value);
            opponentGain += tuple.Item1;
            loss += tuple.Item2;
            
            // Display Transaction
            Display.AllTransactionDisplay(gain,loss,opponentGain,ComputerPlayer,HumanPlayer);
            gain = 0;
            loss = 0;
            opponentGain = 0;
            
            //Choose randomly between buy and saving money
            choose =_random.Next(0, 2);

            switch (choose)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }
    }
}