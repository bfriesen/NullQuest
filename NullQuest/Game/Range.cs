using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game
{
    public class Range
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public Range(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}
