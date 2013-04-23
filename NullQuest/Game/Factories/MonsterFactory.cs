using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NullQuest.Data;
using NullQuest.Game.Combat;
using NullQuest.Game.Extensions;
using NullQuest.Game.Items;

namespace NullQuest.Game.Factories
{
    public class MonsterFactory : IMonsterFactory
    {
        private readonly IWeaponFactory weaponFactory;
        private readonly ISpellFactory spellFactory;
        private readonly IItemFactory itemFactory;
        private readonly IActionSelector _actionSelector;
        private readonly IStatsGenerator _statsGenerator;
        private readonly IDice _dice;

        private readonly List<MonsterArchetype> monsterArchetypes = new List<MonsterArchetype>();
        private readonly List<MonsterModifier> monsterModifiers = new List<MonsterModifier>();

        public MonsterFactory(IWeaponFactory weaponFactory, ISpellFactory spellFactory, IItemFactory itemFactory, IMonsterDataRepository monsterDataRepository, IActionSelector actionSelector, IStatsGenerator statsGenerator, IDice dice)
        {
            this.weaponFactory = weaponFactory;
            this.spellFactory = spellFactory;
            this.itemFactory = itemFactory;
            _actionSelector = actionSelector;
            _statsGenerator = statsGenerator;
            _dice = dice;

            monsterArchetypes = monsterDataRepository.GetAllMonsterArchetypes().ToList();
            monsterModifiers = monsterDataRepository.GetAllMonsterModifiers().ToList();
        }

        public Monster CreateMonster(GameWorld gameWorld, CombatContext combatContext)
        {
            bool isBoss = gameWorld.NumberOfMonstersDefeatedInCurrentDungeonLevel >= gameWorld.RequiredNumberOfMonstersInCurrentDungeonLevelBeforeBoss;
            var monster = CreateMonster(gameWorld.CurrentDungeonLevel, isBoss, combatContext);
            Debug.WriteLine("Created {0} (Level {1}) HP:{6} ATK:{2} DEF:{3}. Player ATK:{4} DEF:{5}", monster.Name, monster.Level, monster.ToHitAttack, monster.ToHitDefense, combatContext.Player.ToHitAttack, combatContext.Player.ToHitDefense, monster.MaxHitPoints);
            if (monster.Weapon != null) Debug.WriteLine("Monster is wielding a {0} ({1})", monster.Weapon.GetLeveledName(), monster.Weapon.Damage);
            return monster;
        }

        public Monster CreateMonster(int dungeonLevel, bool boss, CombatContext combatContext)
        {
            Monster newMonster;
            if (boss)
            {
                int bossLevel = dungeonLevel.GetBossLevel();
                newMonster = CreateArchetypedMonster(bossLevel, bossLevel);
                newMonster.Name = newMonster.Name.ToUpper();
                newMonster.Class = MonsterClass.Boss;
            }
            else
            {
                int minLevel = Convert.ToInt32((dungeonLevel * 1.2) / 2);
                int maxLevel = dungeonLevel;

                newMonster  = CreateArchetypedMonster(minLevel, maxLevel);
                newMonster.Class = MonsterClass.Monster;
            }

            return AssignMonsterStats(newMonster, combatContext);
        }

        private Monster AssignMonsterStats(Monster monster, CombatContext combatContext)
        {
            monster.BaseStats = _statsGenerator.GenerateStats(monster.Class);

            monster.SetActionSelector(_actionSelector);
            monster.SetCombatContext(combatContext);

            monster.CurrentHitPoints = monster.MaxHitPoints;
            monster.CurrentEnergy = monster.MaxEnergy;

            var bossMultiplier = monster.IsBoss ? 4 : 1;

            monster.Gold = monster.Level * _dice.Roll(3 * bossMultiplier, 6);
            monster.Experience = (monster.Level * _dice.Roll(2 * bossMultiplier, 6));

            if (monster.IsBoss)
            {
                monster.Weapon = weaponFactory.CreateWeapon(monster.Level + 1, true);
            }
            else
            {
                if (_dice.Random(1, 6) == 1)
                {  // 1/6 monsters will have a weapon
                    monster.Weapon = weaponFactory.CreateWeapon(monster.Level, false);
                }
            }

            monster = Itemize(monster);

            return monster;
        }

        private Monster Itemize(Monster monster)
        {
            double roll = _dice.Random();
            while (roll < .25)
            {
                if (roll < 0.08)
                {
                    var spell = spellFactory.CreateSpell(monster.Level);
                    var newScroll = new Scroll() { Quantity = 1, Spell = spell };
                    monster.AddItemToInventory(newScroll);
                }
                else
                {
                    monster.AddItemToInventory(itemFactory.CreateItem(monster.Level));
                }
                roll = _dice.Random();
            }
            return monster;
        }

        private Monster CreateArchetypedMonster(int monsterMinLevel, int monsterMaxLevel)
        {
            var newMonster = new Monster();

            var targetMonsterLevel = _dice.Random(monsterMinLevel, monsterMaxLevel);

            var modifierMinimum = 0;
            var modifierMaximum = 0;

            bool targetMonsterHasModifier = _dice.Random(1, 3) == 1; // 1/3 chance of having a modifier

            if (targetMonsterHasModifier)
            {
                modifierMinimum = monsterModifiers.Select(x => x.LevelModifier.GetMinimumValue()).OrderBy(x => x).First();
                modifierMaximum = monsterModifiers.Select(x => x.LevelModifier.GetMaximumValue()).OrderByDescending(x => x).First();
            }

            int targetMonsterMaxLevel = Math.Max(1, targetMonsterLevel - modifierMinimum);
            int targetMonsterMinLevel = Math.Max(1, targetMonsterLevel - modifierMaximum);

            var archetype = ChooseArchetypeInLevelRange(targetMonsterMinLevel, targetMonsterMaxLevel);

            // We now have the archetype. Figure out what level it's going to be
            int monsterLevel = _dice.Random(archetype.Level.MinValue, archetype.Level.MaxValue);

            if (targetMonsterHasModifier)
            {
                // If we're going to have a modifier, start applying random modifiers until we get a monster in our level range.
                var modifier = ChooseModifierInLevelRange(monsterMinLevel - monsterLevel, monsterMaxLevel - monsterLevel);
                monsterLevel += _dice.Roll(modifier.LevelModifier);
                newMonster.Name = modifier.Prefix + " " + archetype.Name;
            }
            else
            {
                // Otherwise, force the monster to be within our level range if the randomly generated level is out of range.
                newMonster.Name = archetype.Name;
            }

            newMonster.Level = Math.Max(Math.Min(monsterLevel, monsterMaxLevel), monsterMinLevel);

            newMonster.SetBareHandsAttackName(archetype.AttackName);

            return newMonster;
        }

        private MonsterModifier ChooseModifierInLevelRange(int minLevel, int maxLevel)
        {
            var randomMatchingModifier = monsterModifiers.Where(x => Overlap(minLevel, maxLevel, x.LevelModifier.GetMinimumValue(), x.LevelModifier.GetMaximumValue())).OrderBy(x => _dice.Random()).FirstOrDefault();

            if (randomMatchingModifier == null)
            {
                // Didn't find any. Either our minlevel is too high, or our maxlevel is too low.
                if (monsterArchetypes.All(x => x.Level.MaxValue < minLevel))
                {
                    // If all the monsters are too low, return the highest-level one
                    return monsterModifiers.OrderByDescending(x => x.LevelModifier.GetMaximumValue()).First();
                }
                else
                {
                    // Otherwise return the lowest-level one
                    return monsterModifiers.OrderBy(x => x.LevelModifier.GetMinimumValue()).First();
                }
            }
            return randomMatchingModifier;
        }

        private MonsterArchetype ChooseArchetypeInLevelRange(int minLevel, int maxLevel)
        {
            var matchingArchetypes = monsterArchetypes.Where(x => Overlap(minLevel, maxLevel, x.Level.MinValue, x.Level.MaxValue));
            var randomMatchingArchetype = matchingArchetypes.OrderBy(x => _dice.Random()).FirstOrDefault();

            if (randomMatchingArchetype == null)
            {
                // Didn't find any. Either our minlevel is too high, or our maxlevel is too low.
                if (monsterArchetypes.All(x => x.Level.MaxValue < minLevel))
                { 
                    // If all the monsters are too low, return the highest-level one
                    return monsterArchetypes.OrderByDescending(x => x.Level.MaxValue).First();
                }
                else
                {
                    // Otherwise return the lowest-level one
                    return monsterArchetypes.OrderBy(x => x.Level.MinValue).First();
                }
            }
            return randomMatchingArchetype;
        }

        private bool Overlap(int minLevel, int maxLevel, int levelMinValue, int levelMaxValue)
        {
            if (minLevel >= levelMinValue && minLevel <= levelMaxValue)
                return true;
            if (levelMinValue >= minLevel && levelMinValue <= maxLevel)
                return true;
            return false;
        }
    }
}
