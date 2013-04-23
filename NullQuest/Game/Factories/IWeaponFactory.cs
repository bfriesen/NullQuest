using NullQuest.Game.Combat;

namespace NullQuest.Game.Factories
{
    public interface IWeaponFactory
    {
        Weapon CreateWeapon(int itemLevel, bool useMaxModifier);
    }
}