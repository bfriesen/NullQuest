using System;
using System.Collections.Generic;

namespace NullQuest.Game
{
    [Serializable]
    public class Magnitude
    {
        public int NumberOfDice { get; set; }
        public int NumberOfSides { get; set; }
        public int BaseAmount { get; set; }

        public Magnitude()
        {
        }

        public Magnitude(int numberOfDice, int numberOfSides, int baseAmount)
        {
            NumberOfDice = numberOfDice;
            NumberOfSides = numberOfSides;
            BaseAmount = baseAmount;
        }

        public double GetAverageValue()
        {
            return (BaseAmount + (NumberOfDice + (NumberOfDice * NumberOfSides)) / 2.0);
        }

        public int GetMinimumValue()
        {
            return BaseAmount + NumberOfDice;
        }

        public int GetMaximumValue()
        {
            return BaseAmount + (NumberOfDice * NumberOfSides);
        }

        public override string ToString()
        {
            var dicePart = NumberOfDice > 0 ? string.Format("{0}d{1}", NumberOfDice, NumberOfSides) : "";
            string bonusPart = "";
            if (BaseAmount > 0) bonusPart = "+" + BaseAmount;
            if (BaseAmount < 0) bonusPart = "-" + Math.Abs(BaseAmount);
            var returnString = dicePart + bonusPart;
            if (!string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }
            return "0";
        }

        private sealed class MagnitudeEqualityComparer : IEqualityComparer<Magnitude>
        {
            public bool Equals(Magnitude x, Magnitude y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.NumberOfDice == y.NumberOfDice && x.NumberOfSides == y.NumberOfSides && x.BaseAmount == y.BaseAmount;
            }

            public int GetHashCode(Magnitude obj)
            {
                unchecked
                {
                    int hashCode = obj.NumberOfDice;
                    hashCode = (hashCode*397) ^ obj.NumberOfSides;
                    hashCode = (hashCode*397) ^ obj.BaseAmount;
                    return hashCode;
                }
            }
        }

        protected bool Equals(Magnitude other)
        {
            return NumberOfDice == other.NumberOfDice && NumberOfSides == other.NumberOfSides && BaseAmount == other.BaseAmount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Magnitude)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = NumberOfDice;
                hashCode = (hashCode * 397) ^ NumberOfSides;
                hashCode = (hashCode * 397) ^ BaseAmount;
                return hashCode;
            }
        }
    }
}
