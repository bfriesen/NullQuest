using System;
using System.Collections.Generic;
using System.Diagnostics;
using NullQuest.Game.Extensions;

namespace NullQuest.Game.Combat
{
    public class Flee : ICombatAction
    {
        public string Name { get { return "Attempt to Flee"; } }

        public void Execute(IDice dice, Combatant attacker, Combatant defender, IList<CombatLogEntry> combatLog)
        {
            int attackerFleeRating = Math.Max(1, attacker.Level + attacker.Agility.GetStatModifier());
            int defenderFleeRating = Math.Max(1, defender.Level + defender.Agility.GetStatModifier());

            var toFleeThreshold = ((attackerFleeRating) / ((double)attackerFleeRating + defenderFleeRating)).ConstrainWithinBounds(0.20, 0.80);
            Debug.WriteLine("{0} has a {1:P0} chance to flee from {2}", attacker.Name, toFleeThreshold, defender.Name);

            if (dice.Random() < toFleeThreshold)
            {
                attacker.HasFledCombat = true;
                combatLog.Add(new CombatLogEntryFromAction<Flee>(Name)
                {
                    Text = string.Format("{0} has fled the battle!", attacker.Name),
                    Attacker = attacker,
                    CombatEffect = CombatOutcome.Empty
                });
            }
            else
            {
                combatLog.Add(new CombatLogEntryFromAction<Flee>(Name)
                {
                    Text = string.Format("{0} attempts to flee but {1} gets in the way!", attacker.Name, defender.Name),
                    Attacker = attacker,
                    CombatEffect = CombatOutcome.Empty
                });
            }
        }

        public CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return CombatOutcome.Empty;
        }
    }
}
