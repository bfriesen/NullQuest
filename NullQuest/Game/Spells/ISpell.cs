using System.Xml.Serialization;
using NullQuest.Game.Combat;
using System;

namespace NullQuest.Game.Spells
{
    public interface ISpell : IHasName, IHasLevel, IHasDescription, IEquatable<ISpell>
    {
        int EnergyCost { get; set; }
        int RequiredIntelligence { get; set; }

        /// <summary>
        /// Returns whether the spell can be cast by the given caster in the given context.
        /// </summary>
        bool CanCast(Combatant caster, IContext context);

        CombatOutcome GetPotentialCombatOutcome(Combatant attacker);
    }

    public interface ICombatSpell : ISpell
    {
        CombatLogEntry Cast(IDice dice, Combatant attacker, Combatant defender);
    }

    public interface INonCombatSpell : ISpell
    {
        CombatLogEntry Cast(IDice dice, Combatant caster);
    }
}
