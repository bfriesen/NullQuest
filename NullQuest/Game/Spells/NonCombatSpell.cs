using System.Linq;
using System.Collections.Generic;
using System.Text;
using NullQuest.Game.Combat;
using NullQuest.Game.Effects;
using System;
using NullQuest.Game.Extensions;

namespace NullQuest.Game.Spells
{
    public sealed class NonCombatSpell : INonCombatSpell
    {
        public IList<NonCombatEffect> Effects { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int EnergyCost { get; set; }
        public int RequiredIntelligence { get; set; }

        public CombatLogEntry Cast(IDice dice, Combatant caster)
        {
            var outcomes = Effects.Select(x => x.Execute(dice, caster)).ToList();

            var logEntry = new CombatLogEntry
            {
                Attacker = caster,
                CombatEffect = new CombatOutcome
                {
                    Healing = outcomes.Aggregate(0, (healing, outcome) => healing + outcome.Healing),
                    Damage = outcomes.Aggregate(0, (damage, outcome) => damage + outcome.Damage),
                },
                Text = string.Format(
                    "{0} cast {1}.{2}{3}",
                    caster.Name,
                    this.GetLeveledName(),
                    Environment.NewLine,
                    string.Join(Environment.NewLine, outcomes.Select(x => x.Description)))
            };

            caster.CurrentEnergy = Math.Max(0, caster.CurrentEnergy - EnergyCost);

            return logEntry;
        }

        /// <summary>
        /// Returns whether the spell can be cast by the given caster in the given context.
        /// </summary>
        public bool CanCast(Combatant caster, IContext context)
        {
            return caster.CurrentEnergy >= EnergyCost;
        }

        public CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return Effects.Aggregate(CombatOutcome.Empty, (outcome, effect) => outcome + effect.GetPotentialCombatOutcome(attacker));
        }

        public bool Equals(ISpell other)
        {
            var otherNonCombatSpell = other as NonCombatSpell;
            if (otherNonCombatSpell == null)
            {
                return false;
            }

            return Name == otherNonCombatSpell.Name
                && Level == otherNonCombatSpell.Level
                && Effects.HaveSameItems(otherNonCombatSpell.Effects);
        }

        public string GetDescription()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Spell: {0}", this.GetLeveledName()).AppendLine();
            sb.AppendFormat("Energy Cost: {0}", EnergyCost).AppendLine();
            sb.AppendFormat("Effects: {0}", string.Join(", ", Effects.Select(x => x.GetDescription()))).AppendLine();
            return sb.ToString();

        }
    }
}
