using System;
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
            bool isRunning = true;
            while (isRunning)
            {
                //Human Turn
                Display.TurnDisplay(HumanPlayer);
                HumanTurn();

                isRunning = !IsEndGame();
                if (!isRunning) break;
                
                if (HumanPlayer.Monuments[3].Build)
                    while (GameDiceOne.Value == GameDiceTwo.Value)
                    {
                        HumanTurn();
                        isRunning = !IsEndGame();
                        if (!isRunning) break;
                    }
                
                isRunning = !IsEndGame();
                if (!isRunning) break;
                
                //Computer Turn
                Display.TurnDisplay(ComputerPlayer);
                ComputerTurn();
                
                isRunning = !IsEndGame();
                if (!isRunning) break;

                if (ComputerPlayer.Monuments[3].Build)
                    while (GameDiceOne.Value == GameDiceTwo.Value)
                    {
                        ComputerTurn();
                        isRunning = !IsEndGame();
                        if (!isRunning) break;
                    }
                
                isRunning = !IsEndGame();
                if (!isRunning) break;
            }
        }

        public override void HumanTurn()
        {
            // Start the Human Turn
            string humanChoice;
            var gain = 0;
            var loss = 0;
            var opponentGain = 0;
            var opponentLoss = 0;
			var proceed = false;

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
                    {
                        GameDiceTwo.Roll();
                        Display.DiceDisplay(GameDiceTwo);
                    }
                    Display.DiceDisplay(GameDiceOne);
                }
            }
            Display.WalletDisplay(HumanPlayer);

            // Card Activate Opponent Red and Blue
            var tuple = HumanPlayer.OpponentTurn(ComputerPlayer, GameDiceOne.Value+GameDiceTwo.Value);
            opponentGain += tuple.Item1;
            loss += tuple.Item2;

            // Card Activate Blue Green and Purple
            tuple = HumanPlayer.PlayerTurn(ComputerPlayer, GameDiceOne.Value+GameDiceTwo.Value);
            gain += tuple.Item1;
            opponentLoss += tuple.Item2;


            // Display win and loosing money
            Display.AllTransactionDisplay(gain, loss, opponentGain, opponentLoss, HumanPlayer, ComputerPlayer);

            var shop = HumanPlayer.ListBuyableCard(Stack);
            var monument = HumanPlayer.ListBuyableMonuments();

            // If the stack isn't empty
            if (Stack.GetStackSize() > 0)
            {
                // Display Wallet
                Display.WalletDisplay(HumanPlayer);
                Display.WalletDisplay(ComputerPlayer);
                // Display ask want to buy or saving money
                Display.MonumentBuildDisplay(HumanPlayer);
                HumanPlayer.ListDeck();
                
                Console.ReadLine();

                while (!proceed)
                {
                    // Display card remaining
                    //Display.CardStackDisplay(Stack, HumanPlayer);

                    //Diplay shop
                    Display.ShopDisplay(monument,shop, Stack);

                    // Display what card do you want to buy
                    Display.AskCardDisplay();
                    humanChoice = Console.ReadLine();

                    try
                    {
                        if (int.Parse(humanChoice) >= 0 && int.Parse(humanChoice) <= shop.Count+monument.Count)
                        {
                            if (int.Parse(humanChoice) == 0)
                            {
                                Display.EconomyDisplay(HumanPlayer);
                                proceed = true;
                            }  
                            else if (int.Parse(humanChoice) <= shop.Count)
                            {
                                var cardChoice = shop[int.Parse(humanChoice)-1];
                                //Add Card
                                HumanPlayer.BuyCard(Stack.PickCard(cardChoice));
                                
                                // Display card buy
                                Display.CardBuyDisplay(HumanPlayer, cardChoice);
                                proceed = true;
                            }
                            else
                            {
                                HumanPlayer.BuyMonument(monument[int.Parse(humanChoice) - shop.Count - 1]);
                                Display.MonumentBuyDisplay(HumanPlayer,monument[int.Parse(humanChoice) - shop.Count - 1]);
                                proceed = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nVeuillez choisir une valeur correcte !!");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nVeuillez choisir une valeur correcte");
                        //Console.WriteLine(e);
                    }
                }
            }
            Display.WalletDisplay(HumanPlayer);
        }

        public override void ComputerTurn()
        {
            // Phrase de début de tour de l'ia
            var gain = 0;
            var loss = 0;
            var opponentGain = 0;
            var opponentLoss = 0;

			if (ComputerPlayer.Monuments[0].Build)
            {
                if (_random.Next(0, 3) == 0)
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
            
            if (ComputerPlayer.Monuments[2].Build && _random.Next(0, 5) == 0)
            {
                Console.WriteLine("L'IA decide de relancer son/ses dés.");
                Console.ReadLine();
                GameDiceOne.Roll();
                if (GameDiceTwo.Value != 0)
                {
                    GameDiceTwo.Roll();
                    Display.DiceDisplay(GameDiceTwo);
                }
                Display.DiceDisplay(GameDiceOne);
            }
            Display.WalletDisplay(ComputerPlayer);

            // Card Activate Opponent Red and Blue
            var tuple = ComputerPlayer.OpponentTurn(HumanPlayer, GameDiceOne.Value + GameDiceTwo.Value);
            opponentGain += tuple.Item1;
            loss += tuple.Item2;

            // Card Activate Blue Green and Purple
            tuple = ComputerPlayer.PlayerTurn(HumanPlayer, GameDiceOne.Value + GameDiceTwo.Value);
            gain += tuple.Item1;
            opponentLoss += tuple.Item2;

            // Display win and loosing money
            Display.AllTransactionDisplay(gain, loss, opponentGain, opponentLoss, ComputerPlayer, HumanPlayer);
            Display.MonumentBuildDisplay(ComputerPlayer);
            ComputerPlayer.ListDeck();
            
            var shop = ComputerPlayer.ListBuyableCard(Stack);
            var monument = ComputerPlayer.ListBuyableMonuments();
            Display.WalletDisplay(ComputerPlayer);
            Display.WalletDisplay(HumanPlayer);
            
            //Choose randomly between buy and saving money
            if (ComputerPlayer.Wallet > 0)
            {
                if (Stack.GetStackSize() > 0) {
					var choice = _random.Next(0,shop.Count*2);
					if (monument.Count > 0)
                    {
                        ComputerPlayer.BuyMonument(monument[0]);
                        Display.MonumentBuyDisplay(ComputerPlayer, monument[0]);
                    }
                    else if (choice == 0 || choice>=shop.Count)
                    { 
                        Display.EconomyDisplay(ComputerPlayer);
                    }  
                    else
                    {
                        var cardChoice = shop[choice]; 
                        //Add Card
                        ComputerPlayer.BuyCard(Stack.PickCard(cardChoice));
                        // Display card buy
                        Display.CardBuyDisplay(ComputerPlayer, cardChoice);
                    }
				}
                else
                {
                    Display.EconomyDisplay(ComputerPlayer);
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

        public override bool IsEndGame()
        {
            if (IsComputerWin() && IsPlayerWin())
            {
                Display.EqualityDisplay(HumanPlayer, ComputerPlayer);
                return true;
            }
            if (IsPlayerWin())
            {
                Display.PlayerVictoryDisplay(HumanPlayer);
                return true;
            }
            if (IsComputerWin())
            {
                Display.ComputerVictoryDisplay(ComputerPlayer);
                return true;
            }
            return false;
        }
        
        
    }
}