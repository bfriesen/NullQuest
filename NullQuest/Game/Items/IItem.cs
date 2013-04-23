using System.Collections.Generic;
using System.Xml.Serialization;
using NullQuest.Game.Combat;
using NullQuest.Game.Effects;
using System;

namespace NullQuest.Game.Items
{
    public interface IItem : IHasName, IHasLevel, IHasDescription, IEquatable<IItem>
    {
        int Quantity { get; set; }

        /// <summary>
        /// Returns whether the item can be used by the given user in the given context.
        /// </summary>
        bool CanUse(Combatant combatant, IContext context);

        CombatOutcome GetPotentialCombatOutcome(Combatant attacker);
    }

    public interface ICombatItem : IItem
    {
        CombatLogEntry Use(IDice dice, Combatant attacker, Combatant defender);
        bool IsMagic { get; set; }
    }

    public interface INonCombatItem : IItem
    {
        CombatLogEntry Use(IDice dice, Combatant combatant);
    }
}
