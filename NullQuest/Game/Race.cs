using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game
{
    public class Race : StatsModifier
    {
        public static readonly Race Human = new Race
        {
            Name = "Human",
            BaseStrength = 4,
            BaseEndurance = 4,
            BaseDexterity = 4,
            BaseAgility = 4,
            BaseIntelligence = 4,
            BaseWisdom = 4,
            BaseLuck = 1
        };

        public static readonly Race Elf = new Race
        {
            Name = "Elf",
            BaseStrength = 0,
            BaseEndurance = 0,
            BaseDexterity = 8,
            BaseAgility = 8,
            BaseIntelligence = 9,
            BaseWisdom = 0,
            BaseLuck = 0
        };

        public static readonly Race Orc = new Race
        {
            Name = "Orc",
            BaseStrength = 9,
            BaseEndurance = 6,
            BaseDexterity = 5,
            BaseAgility = 5,
            BaseIntelligence = 0,
            BaseWisdom = 0,
            BaseLuck = 0
        };

        public static readonly Race Dwarf = new Race
        {
            Name = "Dwarf",
            BaseStrength = 7,
            BaseEndurance = 8,
            BaseDexterity = 2,
            BaseAgility = 0,
            BaseIntelligence = 0,
            BaseWisdom = 8,
            BaseLuck = 0
        };

        private Race()
        {
        }
        
        public string Name { get; private set; }

        public static Race FromName(string name)
        {
            switch (name)
            {
                case "Human":
                    return Human;
                case "Elf":
                    return Elf;
                case "Orc":
                    return Orc;
                case "Dwarf":
                    return Dwarf;
                default:
                    throw new ArgumentException(string.Format("No race matches '{0}'.", name), "name");
            }
        }
    }
}
