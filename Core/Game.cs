namespace Core
{
    public class Game
    {
        private CardStack _stack;
        public Player HumanPlayer;
        public Player ComputerPlayer;

        public Game(string playerName)
        {
            HumanPlayer = new Player(playerName);
            ComputerPlayer = new Player("IA");
            _stack = new CardStack();
        }


    }
}