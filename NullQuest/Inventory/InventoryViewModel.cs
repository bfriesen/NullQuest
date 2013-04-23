using BadSnowstorm;

namespace NullQuest.Game.Inventory
{
    public class InventoryViewModel : ViewModel
    {
        public StatsViewModel Stats { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
        public string AsciiArt { get; set; }
    }
}
