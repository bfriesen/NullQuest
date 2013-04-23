using System;
using System.Linq;
using System.Collections.Generic;

namespace NullQuest.Game
{
    public class Dice : IDice
    {
        private readonly Random _random = new Random();

        public int Roll(int numberOfDice, int numberOfSides)
        {
            return Roll(numberOfSides).Take(numberOfDice).Sum();
        }

        public int Roll(Magnitude magnitude)
        {
            return magnitude.BaseAmount + Roll(magnitude.NumberOfDice, magnitude.NumberOfSides);
        }

        private IEnumerable<int> Roll(int numberOfSides)
        {
            while (true)
            {
                yield return _random.Next(1, numberOfSides + 1);
            }
        }

        public double Random()
        {
            return _random.NextDouble();
        }

        public int Random(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
    }
}
