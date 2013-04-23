using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game
{
    public abstract class StatsModifier
    {
        public int BaseStrength { get; protected set; }
        public int BaseEndurance { get; protected set; }
        public int BaseDexterity { get; protected set; }
        public int BaseAgility { get; protected set; }
        public int BaseIntelligence { get; protected set; }
        public int BaseWisdom { get; protected set; }
        public int BaseLuck { get; protected set; }

        public static StatsModifier operator +(StatsModifier lhs, StatsModifier rhs)
        {
            return new ConcreteStatsModifier
            {
                BaseStrength = lhs.BaseStrength + rhs.BaseStrength,
                BaseEndurance = lhs.BaseEndurance + rhs.BaseEndurance,
                BaseDexterity = lhs.BaseDexterity + rhs.BaseDexterity,
                BaseAgility = lhs.BaseAgility + rhs.BaseAgility,
                BaseIntelligence = lhs.BaseIntelligence + rhs.BaseIntelligence,
                BaseWisdom = lhs.BaseWisdom + rhs.BaseWisdom,
                BaseLuck = lhs.BaseLuck + rhs.BaseLuck,
            };
        }

        private class ConcreteStatsModifier : StatsModifier
        {
        }
    }
}
