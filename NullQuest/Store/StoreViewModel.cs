using BadSnowstorm;
using NullQuest.Game;

namespace NullQuest.Store
{
    public class StoreViewModel : ViewModel
    {
        public string Title { get; set; }
        public StatsViewModel Stats { get; set; }
        public string AsciiArt { get; set; }
    }
}