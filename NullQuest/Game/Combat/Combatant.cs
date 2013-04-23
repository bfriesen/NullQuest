using NullQuest.Game.Extensions;
using NullQuest.Game.Items;
using NullQuest.Game.Spells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NullQuest.Game.Combat
{
    public abstract class Combatant : IHasDescription
    {
        protected readonly IList<IItem> _inventory = new List<IItem>();
        protected readonly IList<ISpell> _spellBook = new List<ISpell>();
        private Weapon _weapon;

        public string Name { get; set; }
        public int Level { get; set; }

        public int Gold { get; set; }
        public int Experience { get; set; }

        public IEnumerable<IItem> Inventory { get { return _inventory; } }
        public IEnumerable<ISpell> SpellBook { get { return _spellBook; } }

        public CharacterStats BaseStats { get; set; }

        public int Strength { get { return BaseStats.Strength + BaseStatsModifier.BaseStrength.GetLevelModifier(Level); } }
        public int Endurance { get { return BaseStats.Endurance + BaseStatsModifier.BaseEndurance.GetLevelModifier(Level); } }
        public int Dexterity { get { return BaseStats.Dexterity + BaseStatsModifier.BaseDexterity.GetLevelModifier(Level); } }
        public int Agility { get { return BaseStats.Agility + BaseStatsModifier.BaseAgility.GetLevelModifier(Level); } }
        public int Intelligence { get { return BaseStats.Intelligence + BaseStatsModifier.BaseIntelligence.GetLevelModifier(Level); } }
        public int Wisdom { get { return BaseStats.Wisdom + BaseStatsModifier.BaseWisdom.GetLevelModifier(Level); } }
        public int Luck { get { return BaseStats.Luck + BaseStatsModifier.BaseLuck.GetLevelModifier(Level); } }

        public int CurrentHitPoints { get; set; }
        public int MaxHitPoints { get { return (int) Math.Round((double) (25 + (Level * (8 + Endurance.GetStatModifier()))), MidpointRounding.AwayFromZero); } }

        public int CurrentEnergy { get; set; }
        public int MaxEnergy { get { return (int)Math.Round((double)(25 + (Level * (8 + Wisdom.GetStatModifier()))), MidpointRounding.AwayFromZero); } }

        public Weapon Weapon
        {
            get { return _weapon ?? Weapon.BareHands; }
            set { _weapon = value; }
        }

        public IEnumerable<int> AttackModifiers
        {
            get { yield break; }
        }

        public IEnumerable<int> DefenseModifiers
        {
            get { yield break; }
        }

        public IEnumerable<int> DamageModifiers
        {
            get { yield break; }
        }

        public int ToHitAttack
        {
            get
            {
                return 1 + Math.Max(Dexterity.GetStatModifier() + AttackModifiers.Sum(), 1);
            }
        }

        public int ToHitDefense
        {
            get
            {
                return Math.Max(Agility.GetStatModifier() + DefenseModifiers.Sum(), 1);
            }
        }

        public int ToHitMagicAttack
        {
            get
            {
                return 1 + Math.Max(Intelligence.GetStatModifier() + AttackModifiers.Sum(), 1);
            }
        }

        public int ToHitMagicDefense
        {
            get
            {
                return Math.Max(Wisdom.GetStatModifier() + DefenseModifiers.Sum(), 1);
            }
        }

        public int GetAttackDamage(IDice dice)
        {
            return Strength.GetStatModifier() + DamageModifiers.Sum() + (dice.Roll(Weapon.Damage) * (Weapon.Level + 1));
        }

        public void ClearInventory()
        {
            _inventory.Clear();
        }

        public void AddItemToInventory(IItem item)
        {
            var existingItem = _inventory.SingleOrDefault(x => x.Equals(item));
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                if (item.Quantity == 0)
                {
                    item.Quantity = 1;
                }

                _inventory.Add(item);
            }
        }

        public void RemoveItemFromInventory(IItem item)
        {
            var existingItem = _inventory.SingleOrDefault(x => x.Equals(item));
            if (existingItem != null)
            {
                existingItem.Quantity--;
                if (existingItem.Quantity == 0)
                {
                    _inventory.Remove(existingItem);
                }
            }
        }

        public void MoveItemToTopOfInventory(IItem item, int currentIndex)
        {
            _inventory.RemoveAt(currentIndex);
            _inventory.Insert(0, item);
        }

        public void ClearSpellBook()
        {
            _spellBook.Clear();
        }

        public void AddSpellToSpellBook(ISpell spell)
        {
            var existingSpell = _spellBook.SingleOrDefault(x => x.Equals(spell));
            if (existingSpell != null)
            {
                existingSpell.Level++;
            }
            else
            {
                _spellBook.Add(spell);
            }
        }

        public void MoveSpellToTopOfSpellBook(ISpell spell, int currentIndex)
        {
            _spellBook.RemoveAt(currentIndex);
            _spellBook.Insert(0, spell);
        }

        public abstract string BareHandsAttackName { get; }
        protected abstract StatsModifier BaseStatsModifier { get; }

        protected abstract IEnumerable<ICombatAction> AdditionalActions(IContext context);
        public IEnumerable<ICombatAction> GetAllowedActions(IContext context)
        {
            yield return new Attack(Weapon.Equals(Weapon.BareHands) ? BareHandsAttackName : Weapon.GetLeveledName());

            foreach (var castSpell in _spellBook.Where(x => x.CanCast(this, context)).Take(3).Select(x => new CastSpell(x)))
            {
                yield return castSpell;
            }

            foreach (var additionalAction in AdditionalActions(context))
            {
                yield return additionalAction;
            }
        }

        public void RestoreHealth(int healthToRestore)
        {
            CurrentHitPoints = Math.Min(MaxHitPoints, CurrentHitPoints + healthToRestore);
        }

        public void RestoreEnergy(int energyToRestore)
        {
            CurrentEnergy = Math.Min(MaxEnergy, CurrentEnergy + energyToRestore);
        }

        public void LowerHealth(int healthToLower)
        {
            CurrentHitPoints = Math.Max(CurrentHitPoints - healthToLower, 0);
        }

        public void LowerEnergy(int energyToLower)
        {
            CurrentEnergy = Math.Max(CurrentEnergy - energyToLower, 0);
        }

        public bool IsAlive
        {
            get { return CurrentHitPoints > 0; }
        }

        public bool HasFledCombat { get; set; }
        public string GetDescription()
        {
            double hpPercent = (double) CurrentHitPoints/MaxHitPoints;
            if (CurrentHitPoints <= 0)
                return "dead";
            if (hpPercent < 0.10)
                return "nearly dead";
            if (hpPercent < 0.25)
                return "severely wounded";
            if (hpPercent < 0.40)
                return "badly injured";
            if (hpPercent < 0.50)
                return "seriously damaged";
            if (hpPercent < 0.65)
                return "noticeably hurt";
            if (hpPercent < 0.80)
                return "somewhat bruised";
            if (hpPercent < 0.90)
                return "barely scratched";
            return "healthy";
        }
    }
}
