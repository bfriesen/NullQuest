using BadSnowstorm;
using NullQuest.Game;

namespace NullQuest.Inn
{
    public class InnViewModel : ViewModel
    {
        public string Title { get; set; }
        public StatsViewModel Stats { get; set; }
        public string AsciiArt { get; set; }
    }
}