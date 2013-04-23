using NullQuest.Game.Extensions;
using NullQuest.Game.Spells;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NullQuest.Game.Combat
{
    public class CastSpell : ICombatAction
    {
        private readonly ISpell _spell;

        public CastSpell(ISpell spell)
        {
            _spell = spell;
        }

        public string Name
        {
            get { return string.Format("Cast {0}", _spell.GetLeveledName()); }
        }

        public void Execute(IDice dice, Combatant attacker, Combatant defender, IList<CombatLogEntry> combatLog)
        {
            var combatSpell = _spell as ICombatSpell;
            if (combatSpell != null)
            {
                int attack = attacker.ToHitMagicAttack;
                int defend = defender.ToHitMagicDefense;

                var toHitThreshold = Math.Min(Math.Max((attack + ((attacker.Level - defender.Level) / 2)) / ((double)attack + defend), 0.10), 0.90);
                Debug.WriteLine("{0} has a {1:P0} chance to cast {2} on {3}", attacker.Name, toHitThreshold, _spell.GetLeveledName(), defender.Name);

                if (dice.Random() < toHitThreshold)
                {
                    var logEntry = combatSpell.Cast(dice, attacker, defender);
                    combatLog.Add(logEntry);
                }
                else
                {
                    combatLog.Add(
                        new CombatLogEntry
                        {
                            Text = string.Format("{0} attempts to cast {1} on {2} and fails miserably!", attacker.Name, _spell.GetLeveledName(), defender.Name),
                            Attacker = attacker
                        });
                }

                attacker.CurrentEnergy = Math.Max(0, attacker.CurrentEnergy - _spell.EnergyCost);
            }

            var nonCombatSpell = _spell as INonCombatSpell;
            if (nonCombatSpell != null)
            {
                var logEntry = nonCombatSpell.Cast(dice, attacker);
                combatLog.Add(logEntry);
            }
        }

        public CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return _spell.GetPotentialCombatOutcome(attacker);
        }
    }
}
