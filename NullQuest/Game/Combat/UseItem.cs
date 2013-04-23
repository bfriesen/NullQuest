using NullQuest.Game.Extensions;
using NullQuest.Game.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NullQuest.Game.Combat
{
    public class UseItem : ICombatAction
    {
        private readonly IItem _item;

        public UseItem(IItem item)
        {
            _item = item;
        }

        public string Name
        {
            get { return string.Format("Use {0}", _item.GetLeveledName()); }
        }

        public void Execute(IDice dice, Combatant attacker, Combatant defender, IList<CombatLogEntry> combatLog)
        {
            var combatItem = _item as ICombatItem;
            if (combatItem != null)
            {
                int attack;
                int defend;

                if (combatItem.IsMagic)
                {
                    attack = attacker.ToHitMagicAttack;
                    defend = defender.ToHitMagicDefense;
                }
                else
                {
                    attack = attacker.ToHitAttack;
                    defend = defender.ToHitDefense;
                }

                var toHitThreshold = Math.Min(Math.Max((attack + ((attacker.Level - defender.Level) / 2)) / ((double)attack + defend), 0.10), 0.90);
                Debug.WriteLine("{0} has a {1:P0} chance to use {2} on {3}", attacker.Name, _item.GetLeveledName(), toHitThreshold, defender.Name);

                if (dice.Random() < toHitThreshold)
                {
                    var logEntry = combatItem.Use(dice, attacker, defender);
                    combatLog.Add(logEntry);
                }
                else
                {
                    combatLog.Add(
                        new CombatLogEntry
                        {
                            Text = string.Format("{0} attempts to use {1} on {2} and fails miserably!", attacker.Name, _item.GetLeveledName(), defender.Name),
                            Attacker = attacker
                        });
                }
            }

            var nonCombatItem = _item as INonCombatItem;
            if (nonCombatItem != null)
            {
                var logEntry = nonCombatItem.Use(dice, attacker);
                combatLog.Add(logEntry);
            }
        }

        public CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return _item.GetPotentialCombatOutcome(attacker);
        }
    }
}
