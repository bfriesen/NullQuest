using System.Collections.Generic;
using NullQuest.Game.Combat;
using NullQuest.Game.Items;

namespace NullQuest.Data
{
    public interface IItemDataRepository
    {
        IEnumerable<ItemArchetype> GetAllItems(); 
    }
}