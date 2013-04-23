using BadSnowstorm;
using NullQuest.Game;

namespace NullQuest.SpellBook
{
    public class SpellBookViewModel : ViewModel
    {
        public StatsViewModel Stats { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
        public string AsciiArt { get; set; }
    }
}
