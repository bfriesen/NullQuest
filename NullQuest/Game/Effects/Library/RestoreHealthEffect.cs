using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Combat;
using NullQuest.Game.Extensions;

namespace NullQuest.Game.Effects.Library
{
    public class RestoreHealthEffect : NonCombatEffect
    {
        public Magnitude Magnitude { get; set; }

        public override EffectOutcome Execute(IDice dice, Combatant combatant)
        {
            int healthBeforeHeal = combatant.CurrentHitPoints;
            var healAmount = (int)Math.Round(dice.Roll(Magnitude) * (1 + ((double)combatant.Wisdom.GetStatModifier() / 9)), MidpointRounding.AwayFromZero);
            combatant.RestoreHealth(healAmount);
            int actualHealAmount = combatant.CurrentHitPoints - healthBeforeHeal;

            return new EffectOutcome()
            {
                Healing = actualHealAmount,
                Description = string.Format("{0} healed for {1} hit points", combatant.Name, actualHealAmount)
            };
        }

        public override CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return new CombatOutcome() { Healing = (int)Math.Min(attacker.MaxHitPoints - attacker.CurrentHitPoints, Magnitude.GetAverageValue()) };
        }

        public override bool Equals(IEffect other)
        {
            var otherEffect = other as RestoreHealthEffect;
            if (otherEffect == null)
            {
                return false;
            }

            return Magnitude.Equals(otherEffect.Magnitude);
        }

        public override string GetDescription()
        {
            return string.Format("Restore Health ({0})", Magnitude);
        }
    }
}
