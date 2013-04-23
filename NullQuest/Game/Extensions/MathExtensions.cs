using System;

namespace NullQuest.Game.Extensions
{
    public static class MathExtensions
    {
         public static double ConstrainWithinBounds(this double value, double lowerBound, double upperBound)
         {
             return Math.Min(Math.Max(value, lowerBound), upperBound);
         }

         public static int GetBossLevel(this int dungeonLevel)
         {
             return Math.Max(dungeonLevel + 2, Convert.ToInt32(dungeonLevel * 1.5));
         }
    }
}