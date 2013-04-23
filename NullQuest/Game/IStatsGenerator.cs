namespace NullQuest.Game
{
    public interface IStatsGenerator
    {
        CharacterStats GenerateStats(params StatsModifier[] statsModifiers);
    }
}
