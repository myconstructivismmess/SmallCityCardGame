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
            var tuple = new Tuple<int, int>(0, 0);
            var proceed = false;
            CardType cardChoice = CardType.Bakery;

            //Roll the Dice
            GameDice.Roll();

            // Display of the Dice
            Display.DiceDisplay(GameDice);

            // Card Activate Red
            tuple = HumanPlayer.OpponentTurn(ComputerPlayer, GameDice.Value);
            opponentGain += tuple.Item1;
            loss += tuple.Item2;

            // Card Activate Blue and Green
            gain += HumanPlayer.PlayerTurn(GameDice.Value);

            // Display win and loosing money
            Display.AllTransactionDisplay(gain, loss, opponentGain, HumanPlayer, ComputerPlayer);

            // If the stack isn't empty
            if (Stack.GetStackSize() > 0)
            {
                // Display Wallet
                Display.WalletDisplay(HumanPlayer);
                // Display ask want to buy or saving money
                HumanPlayer.ListDeck();
                Display.AskDisplay();
                humanChoice = Console.ReadLine();

                // If card player want to buy a card
                if (humanChoice == "Buy")
                {
                    while (!proceed)
                    {
                        // Display card remaining
                        Display.CardStackDisplay(Stack);

                        // Display what card do you want to buy
                        Display.AskCardDisplay();
                        humanChoice = Console.ReadLine();
                        try
                        {
                            if (int.Parse(humanChoice) >= 1 && int.Parse(humanChoice) <= 8)
                            {
                                switch (humanChoice)
                                {
                                    case "1":
                                        if (HumanPlayer.Wallet >= 1)
                                        {
                                            cardChoice = CardType.WheatField;
                                            if (Stack.GetCardCount(CardType.WheatField) > 0)
                                                proceed = true;
                                            else
                                                Display.CardMissingDisplay(cardChoice);
                                        }
                                        else
                                            Display.PlayerIsPoor();

                                        break;
                                    case "2":
                                        if (HumanPlayer.Wallet >= 2)
                                        {
                                            cardChoice = CardType.Farm;
                                            if (Stack.GetCardCount(CardType.Farm) > 0)
                                                proceed = true;
                                            else
                                                Display.CardMissingDisplay(cardChoice);
                                        }
                                        else
                                            Display.PlayerIsPoor();

                                        break;
                                    case "3":
                                        if (HumanPlayer.Wallet >= 1)
                                        {
                                            cardChoice = CardType.Bakery;
                                            if (Stack.GetCardCount(CardType.Bakery) > 0)
                                                proceed = true;
                                            else
                                                Display.CardMissingDisplay(cardChoice);
                                        }
                                        else
                                            Display.PlayerIsPoor();

                                        break;
                                    case "4":
                                        if (HumanPlayer.Wallet >= 2)
                                        {
                                            cardChoice = CardType.CoffeeShop;
                                            if (Stack.GetCardCount(CardType.CoffeeShop) > 0)
                                                proceed = true;
                                            else
                                                Display.CardMissingDisplay(cardChoice);
                                        }
                                        else
                                            Display.PlayerIsPoor();

                                        break;
                                    case "5":
                                        if (HumanPlayer.Wallet >= 2)
                                        {
                                            cardChoice = CardType.GroceryStore;
                                            if (Stack.GetCardCount(CardType.GroceryStore) > 0)
                                                proceed = true;
                                            else
                                                Display.CardMissingDisplay(cardChoice);
                                        }
                                        else
                                            Display.PlayerIsPoor();

                                        break;
                                    case "6":
                                        if (HumanPlayer.Wallet >= 2)
                                        {
                                            cardChoice = CardType.Forest;
                                            if (Stack.GetCardCount(CardType.Forest) > 0)
                                                proceed = true;
                                            else
                                                Display.CardMissingDisplay(cardChoice);
                                        }
                                        else
                                            Display.PlayerIsPoor();

                                        break;
                                    case "7":
                                        if (HumanPlayer.Wallet >= 4)
                                        {
                                            cardChoice = CardType.Restaurant;
                                            if (Stack.GetCardCount(CardType.Restaurant) > 0)
                                                proceed = true;
                                            else
                                                Display.CardMissingDisplay(cardChoice);
                                        }
                                        else
                                            Display.PlayerIsPoor();

                                        break;
                                    case "8":
                                        if (HumanPlayer.Wallet >= 6)
                                        {
                                            cardChoice = CardType.Stadium;
                                            if (Stack.GetCardCount(CardType.Stadium) > 0)
                                                proceed = true;
                                            else
                                                Display.CardMissingDisplay(cardChoice);
                                        }
                                        else
                                            Display.PlayerIsPoor();

                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Veuillez choisir un nombre valide");
                        }
                    }

                    //Add Card
                    HumanPlayer.BuyCard(Stack.PickCard(cardChoice));

                    // Display card buy
                    Display.CardBuyDisplay(HumanPlayer, cardChoice);
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
            var proceed = false;
            int choice = 0;
            CardType cardChoice = CardType.Bakery;
            //Roll the Dice
            GameDice.Roll();
            // Display
            Display.DiceDisplay(GameDice);

            // Card Activate Red
            tuple = ComputerPlayer.OpponentTurn(HumanPlayer, GameDice.Value);
            opponentGain += tuple.Item1;
            loss += tuple.Item2;

            // Card Activate Blue and Green
            gain += ComputerPlayer.PlayerTurn(GameDice.Value);

            // Display Transaction
            Display.AllTransactionDisplay(gain, loss, opponentGain, ComputerPlayer, HumanPlayer);
            ComputerPlayer.ListDeck();

            //Choose randomly between buy and saving money
            if (Stack.GetStackSize() > 0)
                choice = _random.Next(0, 2);

            if (choice == 0)
            {
                Display.EconomyDisplay(ComputerPlayer);
                // Display Wallet
                Display.WalletDisplay(ComputerPlayer);
            }

            else
            {
                while (!proceed)
                {
                    choice = _random.Next(0, 9);
                    switch (choice)
                    {
                        case 1:
                            if (HumanPlayer.Wallet >= 1)
                            {
                                cardChoice = CardType.WheatField;
                                if (Stack.GetCardCount(CardType.WheatField) > 0)
                                    proceed = true;
                            }

                            break;
                        case 2:
                            if (HumanPlayer.Wallet >= 2)
                            {
                                cardChoice = CardType.Farm;
                                if (Stack.GetCardCount(CardType.Farm) > 0)
                                    proceed = true;
                            }

                            break;
                        case 3:
                            if (HumanPlayer.Wallet >= 1)
                            {
                                cardChoice = CardType.Bakery;
                                if (Stack.GetCardCount(CardType.Bakery) > 0)
                                    proceed = true;
                            }

                            break;
                        case 4:
                            if (HumanPlayer.Wallet >= 2)
                            {
                                cardChoice = CardType.CoffeeShop;
                                if (Stack.GetCardCount(CardType.CoffeeShop) > 0)
                                    proceed = true;
                            }

                            break;
                        case 5:
                            if (HumanPlayer.Wallet >= 2)
                            {
                                cardChoice = CardType.GroceryStore;
                                if (Stack.GetCardCount(CardType.GroceryStore) > 0)
                                    proceed = true;
                            }

                            break;
                        case 6:
                            if (HumanPlayer.Wallet >= 2)
                            {
                                cardChoice = CardType.Forest;
                                if (Stack.GetCardCount(CardType.Forest) > 0)
                                    proceed = true;
                            }

                            break;
                        case 7:
                            if (HumanPlayer.Wallet >= 4)
                            {
                                cardChoice = CardType.Restaurant;
                                if (Stack.GetCardCount(CardType.Restaurant) > 0)
                                    proceed = true;
                            }

                            break;
                        case 8:
                            if (HumanPlayer.Wallet >= 6)
                            {
                                cardChoice = CardType.Stadium;
                                if (Stack.GetCardCount(CardType.Stadium) > 0)
                                    proceed = true;
                            }

                            break;
                        default:
                            break;
                    }
                }

                ComputerPlayer.BuyCard(Stack.PickCard(cardChoice));

                // Display card buy
                Display.CardBuyDisplay(ComputerPlayer, cardChoice);
                // Display Wallet
                Display.WalletDisplay(ComputerPlayer);
            }

            Console.ReadLine();
        }
    }
}