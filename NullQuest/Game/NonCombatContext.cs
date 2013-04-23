using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game
{
    public class NonCombatContext : IContext
    {
        public bool IsCombat { get { return false; } }
    }
}
