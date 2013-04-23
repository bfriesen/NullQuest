using System;
using System.Collections.Generic;
using System.Diagnostics;
using NullQuest.Game.Extensions;

namespace NullQuest.Game.Combat
{
    public class Attack : ICombatAction
    {
        private readonly string name;

        public Attack(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public void Execute(IDice dice, Combatant attacker, Combatant defender, IList<CombatLogEntry> combatLog)
        {
            int attack = attacker.ToHitAttack;
            int defend = defender.ToHitDefense;

            var toHitThreshold = ((attack + ((attacker.Level - defender.Level) / 2)) / ((double)attack + defend)).ConstrainWithinBounds(0.20, 0.80);
            Debug.WriteLine("{0} has a {1:P0} chance to hit {2}", attacker.Name, toHitThreshold, defender.Name);

            if (dice.Random() < toHitThreshold)
            {
                var damage = Math.Max(1, attacker.GetAttackDamage(dice));
                defender.LowerHealth(damage);
                combatLog.Add(new CombatLogEntryFromAction<Attack>(Name)
                {
                    Text = string.Format("{0} hits {1} with {2} for {3} points!", attacker.Name, defender.Name, Name, damage),
                    Attacker = attacker,
                    CombatEffect = new CombatOutcome() { Damage = damage }
                });
            }
            else
            {
                combatLog.Add(
                    new CombatLogEntry
                    {
                        Text = string.Format("{0} attempts to hit {1} with {2} and fails miserably!", attacker.Name, defender.Name, Name),
                        Attacker = attacker
                    });
            }
        }

        public CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return new CombatOutcome
            {
                Damage = (int)Math.Round(attacker.Weapon.Damage.GetAverageValue(), MidpointRounding.AwayFromZero) + attacker.Strength.GetStatModifier()
            };
        }
    }
}
