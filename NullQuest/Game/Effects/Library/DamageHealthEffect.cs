using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Combat;
using NullQuest.Game.Extensions;

namespace NullQuest.Game.Effects.Library
{
    public class DamageHealthEffect : IEffect
    {
        public Magnitude Magnitude { get; set; }

        public EffectOutcome Execute(IDice dice, Combatant attacker, Combatant defender)
        {
            var damage = (int) Math.Round(dice.Roll(Magnitude) * (1 + ((double)attacker.Intelligence.GetStatModifier() / 9)), MidpointRounding.AwayFromZero);
            defender.LowerHealth(damage);

            return new EffectOutcome()
                {
                    Description = string.Format("{0} takes {1} points of damage!", defender.Name, damage),
                    Damage = damage
                };
        }

        public CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return new CombatOutcome() { Damage = (int)Magnitude.GetAverageValue() };
        }

        public bool Equals(IEffect other)
        {
            var otherEffect = other as DamageHealthEffect;
            if (otherEffect == null)
            {
                return false;
            }

            return Magnitude.Equals(otherEffect.Magnitude);
        }

        public string GetDescription()
        {
            return string.Format("Damage Health ({0})", Magnitude);
        }
    }
}
