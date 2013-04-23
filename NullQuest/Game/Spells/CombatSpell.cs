using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Combat;
using NullQuest.Game.Effects;
using NullQuest.Game.Extensions;

namespace NullQuest.Game.Spells
{
    public class CombatSpell : ICombatSpell
    {
        public IList<IEffect> Effects { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int EnergyCost { get; set; }
        public int RequiredIntelligence { get; set; }
        
        public CombatLogEntry Cast(IDice dice, Combatant attacker, Combatant defender)
        {
            var outcomes = Effects.Select(x => x.Execute(dice, attacker, defender)).ToList();

            var logEntry = new CombatLogEntry
            {
                Attacker = attacker,
                CombatEffect = new CombatOutcome
                {
                    Healing = outcomes.Aggregate(0, (healing, outcome) => healing + outcome.Healing),
                    Damage = outcomes.Aggregate(0, (damage, outcome) => damage + outcome.Damage),
                },
                Text = string.Format(
                    "{0} cast {1} on {2}.{3}{4}",
                    attacker.Name,
                    this.GetLeveledName(),
                    defender.Name,
                    Environment.NewLine,
                    string.Join(Environment.NewLine, outcomes.Select(x => x.Description)))
            };

            return logEntry;
        }

        /// <summary>
        /// Returns whether the spell can be cast by the given caster in the given context.
        /// </summary>
        public bool CanCast(Combatant caster, IContext context)
        {
            return context.IsCombat && caster.CurrentEnergy >= EnergyCost;
        }

        public CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return Effects.Aggregate(CombatOutcome.Empty, (outcome, effect) => outcome + effect.GetPotentialCombatOutcome(attacker));
        }
        
        public bool Equals(ISpell other)
        {
            var otherCombatSpell = other as CombatSpell;
            if (otherCombatSpell == null)
            {
                return false;
            }

            return Name == otherCombatSpell.Name
                && Level == otherCombatSpell.Level
                && Effects.HaveSameItems(otherCombatSpell.Effects);
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
