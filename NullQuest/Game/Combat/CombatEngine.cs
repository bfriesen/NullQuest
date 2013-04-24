using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Extensions;
using NullQuest.Game.Factories;
using NullQuest.Game.Items;

namespace NullQuest.Game.Combat
{
    public class CombatEngine : ICombatEngine
    {
        private readonly CombatContext _combatContext;
        private readonly ICombatantSelector _combantSelector;
        private readonly IDice _dice;
        private bool _hasPlayerActionBeenApplied;

        public CombatEngine(IMonsterFactory monsterFactory, ICombatantSelector combantSelector, IDice dice)
        {
            _combantSelector = combantSelector;
            _dice = dice;
            _combatContext = new CombatContext();
            _combatContext.Player = GameWorld.Player;
            _combatContext.Monster = monsterFactory.CreateMonster(_combatContext);
            _combatContext.Player.HasFledCombat = false;
            _combatContext.Monster.HasFledCombat = false;
        }

        public IEnumerable<CombatStep> GetSteps()
        {
            yield return CombatStep.Start;

            while (true)
            {
                var endOfCombatStep = GetEndOfCombatStepOrNull();
                if (endOfCombatStep.HasValue)
                {
                    yield return endOfCombatStep.Value;
                    yield break;
                }

                Combatant currentAttacter;

                while ((currentAttacter = _combantSelector.GetNextCombatant(CombatContext)) is Monster)
                {
                    yield return CombatStep.MonsterActionStart;

                    var combatAction = CombatContext.Monster.GetCombatAction();
                    combatAction.Execute(_dice, CombatContext.Monster, CombatContext.Player, CombatContext.CombatLog);

                    yield return CombatStep.MonsterActionEnd;

                    endOfCombatStep = GetEndOfCombatStepOrNull();
                    if (endOfCombatStep.HasValue)
                    {
                        yield return endOfCombatStep.Value;
                        yield break;
                    }
                }

                _hasPlayerActionBeenApplied = false;
                yield return CombatStep.PlayerAction;
                if (!_hasPlayerActionBeenApplied)
                {
                    throw new InvalidOperationException("A player action must be applied immediately following receipt of CombatStep.PlayerAction");
                }

                endOfCombatStep = GetEndOfCombatStepOrNull();
                if (endOfCombatStep.HasValue)
                {
                    yield return endOfCombatStep.Value;
                    yield break;
                }
            }
        }

        public void ApplyPlayerAction(ICombatAction combatAction)
        {
            combatAction.Execute(_dice, CombatContext.Player, CombatContext.Monster, CombatContext.CombatLog);
            _hasPlayerActionBeenApplied = true;
        }
        
        public CombatContext CombatContext
        {
            get { return _combatContext; }
        }

        private CombatStep? GetEndOfCombatStepOrNull()
        {
            if (CombatContext.Player.HasFledCombat)
            {
                return CombatStep.CombatEnded;
            }

            if (CombatContext.Monster.HasFledCombat)
            {
                return CombatStep.CombatEnded;
            }

            if (!CombatContext.Player.IsAlive)
            {
                CombatContext.CombatLog.Add(new CombatLogEntry { Text = string.Format("Dude, the {0} totally killed you! Bummer.", CombatContext.Monster.Name) });
                return CombatStep.PlayerDead;
            }

            if (!CombatContext.Monster.IsAlive)
            {
                CombatContext.CombatLog.Add(
                    new CombatLogEntry
                    {
                        Text = string.Format(
                            "Dude, you totally killed that {0}! Awesome.{1}You received {2} XP and {3} gold.",
                                CombatContext.Monster.Name,
                                Environment.NewLine,
                                CombatContext.Monster.Experience,
                                CombatContext.Monster.Gold)
                    });

                // If the monster has a weapon, it should be dropped and given to the player
                if (CombatContext.Monster.Weapon != null &&
                    !CombatContext.Monster.Weapon.Equals(Weapon.BareHands))
                {
                    CombatContext.CombatLog.Add(CreateItemDroppedLogEntry(CombatContext.Monster.Name, CombatContext.Monster.Weapon));
                    CombatContext.Player.AddItemToInventory(CombatContext.Monster.Weapon);
                    CombatContext.Monster.Weapon = null;
                }

                // Any items in inventory should also be dropped and given to the player
                CombatContext.Monster.Inventory.Select(i => CreateItemDroppedLogEntry(CombatContext.Monster.Name, i)
                    ).ToList().ForEach(x => CombatContext.CombatLog.Add(x));
                CombatContext.Monster.Inventory.ToList().ForEach(i => CombatContext.Player.AddItemToInventory(i));

                GameWorld.TotalNumberOfMonstersDefeated++;

                if (CombatContext.Monster.IsBoss)
                {
                    GameWorld.CurrentDungeonLevel++;
                    GameWorld.SetRequiredNumberOfMonstersInCurrentDungeonLevelBeforeBoss(_dice);
                }
                else
                {
                    GameWorld.NumberOfMonstersDefeatedInCurrentDungeonLevel++;
                }

                CombatContext.Player.AddExperience(CombatContext.Monster.Experience);
                CombatContext.Player.Gold += CombatContext.Monster.Gold;

                return CombatStep.MonsterDead;
            }

            return null;
        }

        private static CombatLogEntry CreateItemDroppedLogEntry(string monsterName, IItem item)
        {
            return new CombatLogEntry()
                {
                    Text =
                        string.Format("     {0} dropped {1}x {2}", monsterName, item.Quantity, item.GetLeveledName())
                };
        }
    }
}
