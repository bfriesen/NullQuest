using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game;
using NullQuest.Game.Combat;

namespace NullQuest.Data
{
    public interface IMonsterDataRepository
    {
        IEnumerable<MonsterArchetype> GetAllMonsterArchetypes();
        IEnumerable<MonsterModifier> GetAllMonsterModifiers();
    }
}
