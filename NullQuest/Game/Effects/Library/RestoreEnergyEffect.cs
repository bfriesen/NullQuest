using System;
using NullQuest.Game.Combat;
using NullQuest.Game.Extensions;

namespace NullQuest.Game.Effects.Library
{
    public class RestoreEnergyEffect : NonCombatEffect
    {
        public Magnitude Magnitude { get; set; }

        public override EffectOutcome Execute(IDice dice, Combatant combatant)
        {
            int energyBeforeHeal = combatant.CurrentEnergy;
            var restoreAmount = (int)Math.Round(dice.Roll(Magnitude) * (1 + ((double)combatant.Wisdom.GetStatModifier() / 9)), MidpointRounding.AwayFromZero);
            combatant.RestoreEnergy(restoreAmount);
            int actualRestoreAmount = combatant.CurrentHitPoints - energyBeforeHeal;

            return new EffectOutcome()
            {
                Healing = actualRestoreAmount,
                Description = string.Format("{0} regained {1} energy points", combatant.Name, actualRestoreAmount)
            };
        }

        public override CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return new CombatOutcome() { }; // AI does not value energy restoration
        }

        public override bool Equals(IEffect other)
        {
            var otherEffect = other as RestoreEnergyEffect;
            if (otherEffect == null)
            {
                return false;
            }

            return Magnitude.Equals(otherEffect.Magnitude);
        }

        public override string GetDescription()
        {
            return string.Format("Restore Energy ({0})", Magnitude);
        }
    }
}
