using System;
using NullQuest.Game.Combat;

namespace NullQuest.Game.Effects
{
    public interface IEffect : IHasDescription, IEquatable<IEffect>
    {
        EffectOutcome Execute(IDice dice, Combatant attacker, Combatant defender);
        CombatOutcome GetPotentialCombatOutcome(Combatant attacker);
    }
}