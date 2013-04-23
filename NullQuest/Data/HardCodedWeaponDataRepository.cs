using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game;
using NullQuest.Game.Combat;

namespace NullQuest.Data
{
    public class HardCodedWeaponDataRepository : IWeaponDataRepository
    {
        public IEnumerable<WeaponArchetype> GetAllWeapons()
        {
            // Swords
            yield return new WeaponArchetype("Dagger") { Damage = new Magnitude(1,4,1), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Short Sword") { Damage = new Magnitude(2, 6, 0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Sabre") { Damage = new Magnitude(2, 8, 1), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Scimitar") { Damage = new Magnitude(4, 6, 1), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Blade") { Damage = new Magnitude(7, 4, 1), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Falchion") { Damage = new Magnitude(6, 6, 0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Claymore") { Damage = new Magnitude(7, 6, 0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Long Sword") { Damage = new Magnitude(8, 6, 1), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Broad Sword") { Damage = new Magnitude(9, 6, 0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Bastard Sword") { Damage = new Magnitude(8, 6, 6), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Two Handed Sword") { Damage = new Magnitude(11, 6, 0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Great Sword") { Damage = new Magnitude(7, 10, 4), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};

            // Clubs
            yield return new WeaponArchetype("Club") { Damage = new Magnitude(1,6,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Spiked Club") { Damage = new Magnitude(3,4,2), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Mace") { Damage = new Magnitude(5,6,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Morning Star") { Damage = new Magnitude(5,8,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("War Hammer") { Damage = new Magnitude(11,4,2), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Flail") { Damage = new Magnitude(8,8,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Maul") { Damage = new Magnitude(8,8,6), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};

            // Axes
            yield return new WeaponArchetype("Small Axe") { Damage = new Magnitude(1,6,1), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Axe") { Damage = new Magnitude(2,10,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Large Axe") { Damage = new Magnitude(4,8,2), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Broad Axe") { Damage = new Magnitude(5,8,6), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Battle Axe") { Damage = new Magnitude(6,10,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Great Axe") { Damage = new Magnitude(17,4,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};

            // Bows
            yield return new WeaponArchetype("Short Bow") { Damage = new Magnitude(1,10,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Ranged};
            yield return new WeaponArchetype("Hunter's Bow") { Damage = new Magnitude(2,6,1), DamageType = DamageType.Normal, WeaponType = WeaponType.Ranged};
            yield return new WeaponArchetype("Long Bow") { Damage = new Magnitude(3,10,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Ranged};
            yield return new WeaponArchetype("Composite Bow") { Damage = new Magnitude(2,20,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Ranged};
            yield return new WeaponArchetype("Short Battle Bow") { Damage = new Magnitude(2,20,5), DamageType = DamageType.Normal, WeaponType = WeaponType.Ranged};
            yield return new WeaponArchetype("Long Battle Bow") { Damage = new Magnitude(3,20,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Ranged};
            yield return new WeaponArchetype("Short War Bow") { Damage = new Magnitude(3,20,5), DamageType = DamageType.Normal, WeaponType = WeaponType.Ranged};
            yield return new WeaponArchetype("Long War Bow") { Damage = new Magnitude(4,20,0), DamageType = DamageType.Normal, WeaponType = WeaponType.Ranged};

            // Staves
            yield return new WeaponArchetype("Short Staff") { Damage = new Magnitude(2,4,1), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Long Staff") { Damage = new Magnitude(3,6,1), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Composite Staff") { Damage = new Magnitude(9,4,1), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("Battle Staff") { Damage = new Magnitude(12,4,2), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
            yield return new WeaponArchetype("War staff") { Damage = new Magnitude(10,6,7), DamageType = DamageType.Normal, WeaponType = WeaponType.Melee};
        }
    }
}