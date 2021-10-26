using Core;

namespace MinivilleConsole
{
    public class Game : CoreGame
    {
        public Game(string playerName) : base(playerName)
        {
            
        }

        public override void Run()
        {
            while (true)
            {
                HumanTurn();
                if (IsPlayerWin())
                {
                    // Phrase de victoire du Joueur
                }

                ComputerTurn();
                if (IsComputerWin())
                {
                    // Phrase de victoire du de l'IA
                }
            }
        }

        public override void HumanTurn()
        {
            // Start the Human Turn
            //Roll the Dice
            GameDice.Roll();
            // Display of the Dice
            // Card Activate Blue and Green
            HumanPlayer.PlayerTurn(GameDice.Value);
            // Card Activate Red
            HumanPlayer.OpponentTurn(ComputerPlayer,GameDice.Value);
            // Display Win or Loosing money
            // Display Ask buy a card or nothing
        }

        public override void ComputerTurn()
        {
            // Phrase de début de tour de l'ia
        }
    }
}