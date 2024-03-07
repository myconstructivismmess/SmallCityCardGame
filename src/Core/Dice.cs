using System;

namespace Core
{
    public class Dice
    {
        private readonly int  _nbFaces;
        private readonly Random _random;
        public int Value;


        public Dice(int nbFaces=6)
        {
            _nbFaces = nbFaces;
            _random = new Random();
            Value = _random.Next(1, _nbFaces+1);
        }

        public void Roll()
        {
            Value = _random.Next(1, _nbFaces + 1);
        }
        
        public override string ToString()
        {
            string toString = String.Format("+---+\n| " +Value+" |\n+---+\n");
            return toString;

        }
    }
}