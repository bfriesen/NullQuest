using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Combat;

namespace NullQuest.Game.Effects
{
    public abstract class NonCombatEffect : IEffect
    {
        public abstract EffectOutcome Execute(IDice dice, Combatant combatant);

        public EffectOutcome Execute(IDice dice, Combatant attacker, Combatant defender)
        {
            return Execute(dice, attacker);
        }

        public abstract CombatOutcome GetPotentialCombatOutcome(Combatant attacker);
        public abstract bool Equals(IEffect other);
        public abstract string GetDescription();
    }
}
