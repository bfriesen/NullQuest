using NullQuest.Game;

namespace NullQuest
{
    public interface IStatsGenerator
    {
        CharacterStats GenerateStats(params StatsModifier[] statsModifiers);
    }
}
