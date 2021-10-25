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
                    
                }
                ComputerTurn();
                if (IsComputerWin())
                {
                    
                }
            }
            
        }

        public override void HumanTurn()
        {
            
        }

        public override void ComputerTurn()
        {
            
        }
    }
}