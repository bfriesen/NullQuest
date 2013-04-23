using BadSnowstorm;

namespace NullQuest.Game.Dungeon
{
    public class DungeonViewModel : ViewModel
    {
        public StatsViewModel Stats { get; set; }
        public string DungeonName { get; set; }
        public string Information { get; set; }
        public string AsciiArt { get; set; }
    }
}
