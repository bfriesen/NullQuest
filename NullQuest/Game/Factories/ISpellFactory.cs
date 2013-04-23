using NullQuest.Game.Spells;

namespace NullQuest.Game.Factories
{
    public interface ISpellFactory
    {
        ISpell CreateSpell(int spellLevel);
    }
}