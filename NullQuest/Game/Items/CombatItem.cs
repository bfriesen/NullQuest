using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using NullQuest.Game.Combat;
using NullQuest.Game.Effects;
using NullQuest.Game.Extensions;

namespace NullQuest.Game.Items
{
    public sealed class CombatItem : ICombatItem
    {
        public IList<IEffect> Effects { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Quantity { get; set; }
        public bool IsMagic { get; set; }

        public CombatLogEntry Use(IDice dice, Combatant attacker, Combatant defender)
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
                    "{0} used {1} on {2}.{3}{4}",
                    attacker.Name,
                    this.GetLeveledName(),
                    defender.Name,
                    Environment.NewLine,
                    string.Join(Environment.NewLine, outcomes.Select(x => x.Description)))
            };

            return logEntry;
        }

        /// <summary>
        /// Returns whether the item can be used by the given user in the given context.
        /// </summary>
        public bool CanUse(Combatant user, IContext context)
        {
            return context.IsCombat && user.Inventory.Any(x => ReferenceEquals(x, this)) && Quantity > 0;
        }

        public CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return Effects.Aggregate(CombatOutcome.Empty, (outcome, effect) => outcome + effect.GetPotentialCombatOutcome(attacker));
        }

        public bool Equals(IItem other)
        {
            var otherCombatItem = other as CombatItem;
            if (otherCombatItem == null)
            {
                return false;
            }

            return Name == otherCombatItem.Name
                && IsMagic == otherCombatItem.IsMagic
                && Level == otherCombatItem.Level;
        }

        public string GetDescription()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Item: {0}", this.GetLeveledName()).AppendLine();
            sb.AppendFormat("Effects: {0}", string.Join(", ", Effects.Select(x => x.GetDescription()))).AppendLine();
            return sb.ToString();

        }
    }
}
