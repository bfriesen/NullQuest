using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game.Combat
{
    // Attack
    // Use Item
    // Flee
    // Cast Spell

    public interface ICombatAction
    {
        string Name { get; }
        void Execute(IDice dice, Combatant attacker, Combatant defender, IList<CombatLogEntry> combatLog);
        CombatOutcome GetPotentialCombatOutcome(Combatant attacker);
    }

    public static class CombatActionHelpers
    {
        public static int CreateHash(this ICombatAction combatAction)
        {
            return CreateHash(combatAction.GetType(), combatAction.Name).GetHashCode();
        }

        public static int CreateHash(this Type type, string name)
        {
            return (type + ":" + name).GetHashCode();
        }
    }
}
