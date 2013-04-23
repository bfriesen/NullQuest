using NullQuest.Game.Spells;

namespace NullQuest.Game.Extensions
{
    public static class NameExtensions
    {
        public static string GetLeveledName<THasLevelAndName>(this THasLevelAndName instance)
            where THasLevelAndName : IHasLevel, IHasName
        {
            return GetInternalLeveledName(instance);
        }

        private static string GetInternalLeveledName<THasLevelAndName>(THasLevelAndName instance)
            where THasLevelAndName : IHasLevel, IHasName
        {
            return instance.Level > 0 ? instance.Name + " +" + instance.Level : instance.Name;
        }

        public static string GetLeveledName(this ISpell spell)
        {
            return string.Format("{0} (EN: {1})", GetInternalLeveledName(spell), spell.EnergyCost);
        }
    }
}