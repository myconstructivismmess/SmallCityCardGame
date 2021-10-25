using System;

namespace Core
{
    public class Dice
    {
        private readonly int  _nbFaces;
        private readonly Random _random;
        public int Face;


        public Dice(int nbFaces=6)
        {
            _nbFaces = nbFaces;
            _random = new Random();
            Face = _random.Next(1, _nbFaces+1);
        }

        public void Lancer()
        {
            Face = _random.Next(1, _nbFaces + 1);
        }   
    }
}