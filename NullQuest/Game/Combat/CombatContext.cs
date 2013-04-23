using System;
using System.Collections.Generic;

namespace NullQuest.Game.Combat
{
    public class CombatContext : IContext
    {
        public CombatContext()
        {
            CombatLog = new List<CombatLogEntry>();
        }

        public Player Player { get; set; }
        public Monster Monster { get; set; }
        public IList<CombatLogEntry> CombatLog { get; private set; }
        public bool IsCombat { get { return true; } }
    }
}