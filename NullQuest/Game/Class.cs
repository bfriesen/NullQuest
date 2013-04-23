using System;
namespace NullQuest.Game
{
    public class Class : StatsModifier
    {
        public readonly static Class Fighter = new Class
        {
            Name = "Fighter",
            BareHandsAttack = "Punch",
            BaseStrength = 8,
            BaseEndurance = 3,
            BaseDexterity = 2,
            BaseAgility = 2,
            BaseIntelligence = 0,
            BaseWisdom = 0,
            BaseLuck = 0
        };
        
        public readonly static Class Wizard = new Class
        {
            Name = "Wizard",
            BareHandsAttack = "Magic Punch",
            BaseStrength = 0,
            BaseEndurance = 0,
            BaseDexterity = 0,
            BaseAgility = 4,
            BaseIntelligence = 8,
            BaseWisdom = 3,
            BaseLuck = 0
        };
        
        public readonly static Class Thief = new Class
        {
            Name = "Thief",
            BareHandsAttack = "Sneaky Punch",
            BaseStrength = 0,
            BaseEndurance = 0,
            BaseDexterity = 6,
            BaseAgility = 6,
            BaseIntelligence = 0,
            BaseWisdom = 0,
            BaseLuck = 3
        };
        
        public readonly static Class Cleric = new Class
        {
            Name = "Cleric",
            BareHandsAttack = "Holy Punch",
            BaseStrength = 0,
            BaseEndurance = 4,
            BaseDexterity = 0,
            BaseAgility = 0,
            BaseIntelligence = 3,
            BaseWisdom = 8,
            BaseLuck = 0
        };
        
        public readonly static Class Bard = new Class
        {
            Name = "Bard",
            BareHandsAttack = "Musical Punch",
            BaseStrength = 3,
            BaseEndurance = 3,
            BaseDexterity = 3,
            BaseAgility = 3,
            BaseIntelligence = 3,
            BaseWisdom = 3,
            BaseLuck = 3
        };

        private Class()
        {
        }

        public string Name { get; private set; }
        public string BareHandsAttack { get; private set; }

        public static Class FromName(string name)
        {
            switch (name)
            {
                case "Fighter":
                    return Fighter;
                case "Wizard":
                    return Wizard;
                case "Thief":
                    return Thief;
                case "Cleric":
                    return Cleric;
                case "Bard":
                    return Bard;
                default:
                    throw new ArgumentException(string.Format("No class matches '{0}'.", name), "name");
            }
        }
    }
}
