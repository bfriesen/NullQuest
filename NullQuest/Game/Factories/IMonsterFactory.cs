using NullQuest.Game.Combat;

namespace NullQuest.Game.Factories
{
    public interface IMonsterFactory
    {
        Monster CreateMonster(GameWorld gameWorld, CombatContext combatContext);
    }
}