using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Combat;
using BadSnowstorm;

namespace NullQuest.Game.Dungeon
{
    public class CombatViewModel : ViewModel
    {
        public StatsViewModel Stats { get; set; }
        public string DungeonName { get; set; }
        public string Information { get; set; }
        public string CombatLog { get; set; }
        public string AsciiArt { get; set; }
    }
}
