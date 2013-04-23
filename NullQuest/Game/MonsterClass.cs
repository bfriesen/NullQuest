using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game
{
    public class MonsterClass : StatsModifier
    {
        public static readonly MonsterClass Monster = new MonsterClass();
        public static readonly MonsterClass Boss =
            new MonsterClass
            {
                BaseAgility = 5,
                BaseDexterity = 5,
                BaseEndurance = 5,
                BaseIntelligence = 5,
                BaseLuck = 5,
                BaseStrength = 5,
                BaseWisdom = 5,
                IsBoss = true
            };

        public bool IsBoss { get; private set; }
    }
}
