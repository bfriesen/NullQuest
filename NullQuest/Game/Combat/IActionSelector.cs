using System.Collections.Generic;
namespace NullQuest.Game.Combat
{
    public interface IActionSelector
    {
        ICombatAction SelectAction(CombatContext combatContext, IEnumerable<ICombatAction> allowedActions);
    }
}