using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game
{
    public interface IContext
    {
        bool IsCombat { get; }
    }
}
