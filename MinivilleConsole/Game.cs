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
                Display.TurnDisplay(HumanPlayer);
                HumanTurn();

                if (IsComputerWin() && IsPlayerWin())
                {
                    Display.EqualityDisplay(HumanPlayer, ComputerPlayer);
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
                Display.TurnDisplay(ComputerPlayer);
                ComputerTurn();

                if (IsComputerWin() && IsPlayerWin())
                {
                    Display.EqualityDisplay(HumanPlayer, ComputerPlayer);
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
            var opponentLoss = 0;
            var tuple = new Tuple<int, int>(0, 0);
            var proceed = false;
            CardType cardChoice = CardType.Bakery;

            // Station Effect
            if (HumanPlayer.Monuments[0].Build)
            {
                Display.DiceAskDisplay();
                humanChoice = Console.ReadLine();
                if (humanChoice == "2")
                {
                    //Roll the Dice
                    GameDiceTwo.Roll();
                    // Display of the Dice
                    Display.DiceDisplay(GameDiceTwo);
                }
                else
                {
                    GameDiceTwo.Value = 0;
                }
            }
            else
            {
                GameDiceTwo.Value = 0;
            }

            //Roll the Dice
            GameDiceOne.Roll();
            // Display of the Dice
            Display.DiceDisplay(GameDiceOne);
            
            //Radio Tower Effect
            if (HumanPlayer.Monuments[2].Build)
            {
                Display.RollAskDisplay();
                humanChoice = Console.ReadLine();
                if (humanChoice == "1")
                {
                    GameDiceOne.Roll();
                    if (GameDiceTwo.Value != 0)
                        GameDiceTwo.Roll();
                }
            }

            // Card Activate Opponent Red and Blue
            tuple = HumanPlayer.OpponentTurn(ComputerPlayer, GameDiceOne.Value+GameDiceTwo.Value);
            opponentGain += tuple.Item1;
            loss += tuple.Item2;

            // Card Activate Blue Green and Purple
            tuple = HumanPlayer.PlayerTurn(ComputerPlayer, GameDiceOne.Value+GameDiceTwo.Value);
            gain += tuple.Item1;
            opponentLoss += tuple.Item2;


            // Display win and loosing money
            Display.AllTransactionDisplay(gain, loss, opponentGain, opponentLoss, HumanPlayer, ComputerPlayer);

            // If the stack isn't empty
            if (Stack.GetStackSize() > 0)
            {
                // Display Wallet
                Display.WalletDisplay(HumanPlayer);
                // Display ask want to buy or saving money
                HumanPlayer.ListDeck();
                Console.ReadLine();

                while (!proceed)
                {
                    // Display card remaining
                    Display.CardStackDisplay(Stack);

                    // Display what card do you want to buy
                    Display.AskCardDisplay();
                    humanChoice = Console.ReadLine();

                    switch (humanChoice)
                    {
                        case "1":
                            if (HumanPlayer.Wallet >= new WheatField().Cost)
                                cardChoice = CardType.WheatField;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "2":
                            if (HumanPlayer.Wallet >= new Farm().Cost)
                                cardChoice = CardType.Farm;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "3":
                            if (HumanPlayer.Wallet >= new Bakery().Cost)
                                cardChoice = CardType.Bakery;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "4":
                            if (HumanPlayer.Wallet >= new CoffeeShop().Cost)
                                cardChoice = CardType.CoffeeShop;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "5":
                            if (HumanPlayer.Wallet >= new GroceryStore().Cost)
                                cardChoice = CardType.GroceryStore;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "6":
                            if (HumanPlayer.Wallet >= new Forest().Cost)
                                cardChoice = CardType.Forest;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "7":
                            if (HumanPlayer.Wallet >= new BusinessCenter().Cost)
                                cardChoice = CardType.BusinessCenter;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "8":
                            if (HumanPlayer.Wallet >= new Stadium().Cost)
                                cardChoice = CardType.Stadium;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "9":
                            if (HumanPlayer.Wallet >= new TelevisionChannel().Cost)
                                cardChoice = CardType.TelevisionChannel;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "10":
                            if (HumanPlayer.Wallet >= new CheeseShop().Cost)
                                cardChoice = CardType.CheeseShop;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "11":
                            if (HumanPlayer.Wallet >= new FurnitureShop().Cost)
                                cardChoice = CardType.FurnitureShop;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "12":
                            if (HumanPlayer.Wallet >= new Mine().Cost)
                                cardChoice = CardType.Mine;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "13":
                            if (HumanPlayer.Wallet >= new Restaurant().Cost)
                                cardChoice = CardType.Restaurant;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "14":
                            if (HumanPlayer.Wallet >= new Orchard().Cost)
                                cardChoice = CardType.Orchard;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case "15":
                            if (HumanPlayer.Wallet >= new Market().Cost)
                                cardChoice = CardType.Market;
                            else
                                Display.PlayerIsPoor();
                            break;
                        default:
                            proceed = true;
                            break;
                    }

                    if (!proceed)
                    {
                        if (Stack.GetCardCount(cardChoice) > 0)
                            proceed = true;
                        else
                            Display.CardMissingDisplay(cardChoice);
                    }

                }

                try
                {
                    if (int.Parse(humanChoice) >= 1 && int.Parse(humanChoice) <= 15)
                    {
                        //Add Card
                        HumanPlayer.BuyCard(Stack.PickCard(cardChoice));

                        // Display card buy
                        Display.CardBuyDisplay(HumanPlayer, cardChoice);
                    }
                    else
                        //Display economy
                        Display.EconomyDisplay(HumanPlayer);
                }
                catch (Exception)
                {
                    //Display economy
                    Display.EconomyDisplay(HumanPlayer);
                }
            }
            else
                //Display economy
                Display.EconomyDisplay(HumanPlayer);

            // Display Wallet
            Display.WalletDisplay(HumanPlayer);

            Console.ReadLine();
        }

        public override void ComputerTurn()
        {
            // Phrase de début de tour de l'ia
            var gain = 0;
            var loss = 0;
            var opponentGain = 0;
            var opponentLoss = 0;
            var tuple = new Tuple<int, int>(0, 0);
            var proceed = false;
            int choice = 0;
            CardType cardChoice = CardType.Bakery;

            if (ComputerPlayer.Monuments[0].Build)
            {
                choice = _random.Next(0, 2);
                if (choice == 1)
                {
                    //Roll the Dice
                    GameDiceTwo.Roll();
                    // Display of the Dice
                    Display.DiceDisplay(GameDiceTwo);
                }
                else
                {
                    GameDiceTwo.Value = 0;
                }
            }
            else
            {
                GameDiceTwo.Value = 0;
            }

            //Roll the Dice
            GameDiceOne.Roll();
            // Display
            Display.DiceDisplay(GameDiceOne);

            // Card Activate Opponent Red and Blue
            tuple = ComputerPlayer.OpponentTurn(HumanPlayer, GameDiceOne.Value + GameDiceTwo.Value);
            opponentGain += tuple.Item1;
            loss += tuple.Item2;

            // Card Activate Blue Green and Purple
            tuple = ComputerPlayer.PlayerTurn(HumanPlayer, GameDiceOne.Value + GameDiceTwo.Value);
            gain += tuple.Item1;
            opponentLoss += tuple.Item2;


            // Display win and loosing money
            Display.AllTransactionDisplay(gain, loss, opponentGain, opponentLoss, ComputerPlayer, HumanPlayer);
            ComputerPlayer.ListDeck();

            //Choose randomly between buy and saving money
            if (Stack.GetStackSize() > 0)
            {
                while (!proceed)
                {
                    choice = _random.Next(0, 2 * Enum.GetNames(typeof(CardType)).Length - 4);
                    switch (choice)
                    {
                        case 1:
                            if (ComputerPlayer.Wallet >= new WheatField().Cost)
                                cardChoice = CardType.WheatField;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 2:
                            if (ComputerPlayer.Wallet >= new Farm().Cost)
                                cardChoice = CardType.Farm;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 3:
                            if (ComputerPlayer.Wallet >= new Bakery().Cost)
                                cardChoice = CardType.Bakery;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 4:
                            if (ComputerPlayer.Wallet >= new CoffeeShop().Cost)
                                cardChoice = CardType.CoffeeShop;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 5:
                            if (ComputerPlayer.Wallet >= new GroceryStore().Cost)
                                cardChoice = CardType.GroceryStore;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 6:
                            if (ComputerPlayer.Wallet >= new Forest().Cost)
                                cardChoice = CardType.Forest;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 7:
                            if (ComputerPlayer.Wallet >= new BusinessCenter().Cost)
                                cardChoice = CardType.BusinessCenter;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 8:
                            if (ComputerPlayer.Wallet >= new Stadium().Cost)
                                cardChoice = CardType.Stadium;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 9:
                            if (ComputerPlayer.Wallet >= new TelevisionChannel().Cost)
                                cardChoice = CardType.TelevisionChannel;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 10:
                            if (ComputerPlayer.Wallet >= new CheeseShop().Cost)
                                cardChoice = CardType.CheeseShop;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 11:
                            if (ComputerPlayer.Wallet >= new FurnitureShop().Cost)
                                cardChoice = CardType.FurnitureShop;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 12:
                            if (ComputerPlayer.Wallet >= new Mine().Cost)
                                cardChoice = CardType.Mine;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 13:
                            if (ComputerPlayer.Wallet >= new Restaurant().Cost)
                                cardChoice = CardType.Restaurant;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 14:
                            if (ComputerPlayer.Wallet >= new Orchard().Cost)
                                cardChoice = CardType.Orchard;
                            else
                                Display.PlayerIsPoor();
                            break;
                        case 15:
                            if (ComputerPlayer.Wallet >= new Market().Cost)
                                cardChoice = CardType.Market;
                            break;
                        default:
                            proceed = true;
                            break;
                    }

                    if (!proceed)
                    {
                        if (Stack.GetCardCount(cardChoice) > 0)
                            proceed = true;
                    }
                }

                if (choice < 1 || choice > 15)
                    Display.EconomyDisplay(ComputerPlayer);
                else
                {
                    ComputerPlayer.BuyCard(Stack.PickCard(cardChoice));
                    // Display card buy
                    Display.CardBuyDisplay(ComputerPlayer, cardChoice);
                }
            }
            else
            {
                Display.EconomyDisplay(ComputerPlayer);
            }

            // Display Wallet
            Display.WalletDisplay(ComputerPlayer);

            Console.ReadLine();
        }
    }
}