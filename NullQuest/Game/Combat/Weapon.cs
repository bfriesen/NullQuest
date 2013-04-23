using System.Text;
using NullQuest.Game.Extensions;
using NullQuest.Game.Items;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace NullQuest.Game.Combat
{
    [Serializable]
    public class Weapon : INonCombatItem
    {
        public static readonly Weapon BareHands =
            new Weapon
            {
                Name = "Bare Hands",
                WeaponType = WeaponType.Melee,
                DamageType = DamageType.Normal,
                Damage =
                    new Magnitude
                    {
                        BaseAmount = 0,
                        NumberOfDice = 1,
                        NumberOfSides = 4
                    }
            };

        public Weapon()
        {
        }

        public Weapon(WeaponArchetype weaponArchetype)
        {
            Damage = weaponArchetype.Damage;
            DamageType = weaponArchetype.DamageType;
            WeaponType = weaponArchetype.WeaponType;
            Name = weaponArchetype.Name;
        }

        public CombatLogEntry Use(IDice dice, Combatant combatant)
        {
            if (!combatant.Weapon.Equals(BareHands))
            {
                combatant.AddItemToInventory(combatant.Weapon);
            }
            
            combatant.RemoveItemFromInventory(this);
            combatant.Weapon = this.DeepClone();
            combatant.Weapon.Quantity = 1;

            return null;
        }

        public bool CanUse(Combatant combatant, IContext context)
        {
            return !context.IsCombat
                && combatant.Inventory.Any(x => ReferenceEquals(x, this))
                && Quantity > 0
                && combatant.Strength >= RequiredStrength;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Level { get; set; }
        public WeaponType WeaponType { get; set; }
        public DamageType DamageType { get; set; }
        public Magnitude Damage { get; set; }
        public int RequiredStrength { get; set; }

        public CombatOutcome GetPotentialCombatOutcome(Combatant attacker)
        {
            return CombatOutcome.Empty;
        }

        public bool Equals(IItem other)
        {
            var otherWeapon = other as Weapon;
            if (otherWeapon == null)
            {
                return false;
            }

            return Name == otherWeapon.Name
                && WeaponType == otherWeapon.WeaponType
                && DamageType == otherWeapon.DamageType
                && Level == otherWeapon.Level
                && Damage.Equals(otherWeapon.Damage);
        }

        public string GetDescription()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Weapon: {0}", this.GetLeveledName()).AppendLine();
            sb.AppendFormat("Damage: {0}", Damage).AppendLine();
            //sb.AppendFormat("Effects: {0}", string.Join(", ", Effects.Select(x => x.GetDescription()))).AppendLine();
            return sb.ToString();

        }
    }
}
