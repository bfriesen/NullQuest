using System.Collections.Generic;
using NullQuest.Game.Combat;

namespace NullQuest.Data
{
    public interface IWeaponDataRepository
    {
        IEnumerable<WeaponArchetype> GetAllWeapons();
    }
}