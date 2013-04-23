using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game
{
    public class StatsGenerator : IStatsGenerator
    {
        private readonly IDice _dice;

        public StatsGenerator(IDice dice)
        {
            _dice = dice;
        }

        public CharacterStats GenerateStats(params StatsModifier[] statsModifiers)
        {
            int baseStrength = statsModifiers.Sum(x => x.BaseStrength);
            int baseEndurance = statsModifiers.Sum(x => x.BaseEndurance);
            int baseDexterity = statsModifiers.Sum(x => x.BaseDexterity);
            int baseAgility = statsModifiers.Sum(x => x.BaseAgility); 
            int baseIntelligence = statsModifiers.Sum(x => x.BaseIntelligence);
            int baseWisdom = statsModifiers.Sum(x => x.BaseWisdom);
            int baseLuck = statsModifiers.Sum(x => x.BaseLuck);

            return new CharacterStats
            {
                Strength = Math.Min(baseStrength + 4 + _dice.Roll(1, 6), 25),
                Endurance = Math.Min(baseEndurance + 4 + _dice.Roll(1, 6), 25),
                Dexterity = Math.Min(baseDexterity + 4 + _dice.Roll(1, 6), 25),
                Agility = Math.Min(baseAgility + 4 + _dice.Roll(1, 6), 25),
                Intelligence = Math.Min(baseIntelligence + 4 + _dice.Roll(1, 6), 25),
                Wisdom = Math.Min(baseWisdom + 4 + _dice.Roll(1, 6), 25),
                Luck = Math.Min(baseLuck + 4 + _dice.Roll(1, 6), 25),
            };
        }
    }
}
