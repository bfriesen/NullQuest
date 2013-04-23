using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game;
using NullQuest.Game.Combat;
using NullQuest.Game.Items;
using NullQuest.Game.Spells;

namespace NullQuest.Data
{
    class HardCodedMonsterDataRepository : IMonsterDataRepository
    {
        public IEnumerable<MonsterArchetype> GetAllMonsterArchetypes()
        {
            yield return new MonsterArchetype() { Name = "Rat", Level = new Range(1, 1), AttackName = "Nibble"};
            yield return new MonsterArchetype() { Name = "Spider", Level = new Range(1, 2), AttackName = "Bite" };
            yield return new MonsterArchetype() { Name = "Bat", Level = new Range(2, 3), AttackName = "Wing Flap" };
            yield return new MonsterArchetype() { Name = "Skeleton", Level = new Range(2, 5), AttackName = "Boney Hit" };
            yield return new MonsterArchetype() { Name = "Scorpion", Level = new Range(3, 6), AttackName = "Sting" };
            yield return new MonsterArchetype() { Name = "Zombie", Level = new Range(4, 8), AttackName = "Braiiins" };
            yield return new MonsterArchetype() { Name = "Crocodile", Level = new Range(5, 11), AttackName = "Chomp" };
            yield return new MonsterArchetype() { Name = "Dire Wolf", Level = new Range(7, 14), AttackName = "Bite" };
            yield return new MonsterArchetype() { Name = "Bear", Level = new Range(9, 18), AttackName = "Maul" };
            yield return new MonsterArchetype() { Name = "Mummy", Level = new Range(12, 24), AttackName = "Bandage Whip" };
            yield return new MonsterArchetype() { Name = "Centaur", Level = new Range(15, 30), AttackName = "Spear Thrust" };
            yield return new MonsterArchetype() { Name = "Dragonling", Level = new Range(18, 36), AttackName = "Claw" };
            yield return new MonsterArchetype() { Name = "Troll", Level = new Range(22, 44), AttackName = "Batter" };
            yield return new MonsterArchetype() { Name = "Ogre", Level = new Range(26, 52), AttackName = "Clobber" };
            yield return new MonsterArchetype() { Name = "Giant", Level = new Range(30, 60), AttackName = "Stomp" };
            yield return new MonsterArchetype() { Name = "Wraith", Level = new Range(35, 70), AttackName = "Ethereal Attack" };
            yield return new MonsterArchetype() { Name = "Ghost", Level = new Range(40, 80), AttackName = "Ethereal Impact" };
            yield return new MonsterArchetype() { Name = "Gargoyle", Level = new Range(45, 90), AttackName = "Wicked Bite" };
            yield return new MonsterArchetype() { Name = "Wooly Mammoth", Level = new Range(50, 100), AttackName = "Gore" };
            yield return new MonsterArchetype() { Name = "Vampire", Level = new Range(60, 120), AttackName = "Suck Blood" };
            yield return new MonsterArchetype() { Name = "Lich", Level = new Range(70, 140), AttackName = "Death Attack" };
            yield return new MonsterArchetype() { Name = "Cyclops", Level = new Range(80, 160), AttackName = "One-Eyed Focus" };
            yield return new MonsterArchetype() { Name = "Efreet", Level = new Range(90, 180), AttackName = "Fire Blast" };
            yield return new MonsterArchetype() { Name = "Dragon", Level = new Range(150, 200), AttackName = "Dragon's Breath" };
        }

        public IEnumerable<MonsterModifier> GetAllMonsterModifiers()
        {
            yield return new MonsterModifier() {Prefix = "Frail", LevelModifier = new Magnitude(0,0,-2)};
            yield return new MonsterModifier() {Prefix = "Weak", LevelModifier = new Magnitude(0,0,-1)};
            yield return new MonsterModifier() { Prefix = "Angry", LevelModifier = new Magnitude(1, 2, 0) };
            yield return new MonsterModifier() { Prefix = "Strong", LevelModifier = new Magnitude(1, 4, 0) };
            yield return new MonsterModifier() { Prefix = "Giant", LevelModifier = new Magnitude(1, 6, 0) };
            yield return new MonsterModifier() { Prefix = "Blessed", LevelModifier = new Magnitude(2, 4, 0) };
            yield return new MonsterModifier() { Prefix = "Champion", LevelModifier = new Magnitude(2, 6, 2) };
            yield return new MonsterModifier() { Prefix = "Brutal", LevelModifier = new Magnitude(3, 6, 10) };
            yield return new MonsterModifier() { Prefix = "Deadly", LevelModifier = new Magnitude(5, 6, 15) };
        }
    }
}
