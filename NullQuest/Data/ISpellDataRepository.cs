using System.Collections.Generic;
using NullQuest.Game.Combat;
using NullQuest.Game.Spells;

namespace NullQuest.Data
{
    public interface ISpellDataRepository
    {
        IEnumerable<SpellArchetype> GetAllSpells();
    }
}