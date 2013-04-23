using System.Collections.Generic;

namespace NullQuest.Game.Combat
{
    public interface ICombatEngine
    {
        IEnumerable<CombatStep> GetSteps();
        void ApplyPlayerAction(ICombatAction combatAction);
        CombatContext CombatContext { get; }
    }

    public enum CombatStep
    {
        Start,
        PlayerAction,
        MonsterActionStart,
        MonsterActionEnd,
        PlayerDead,
        MonsterDead,
        CombatEnded
    }
}