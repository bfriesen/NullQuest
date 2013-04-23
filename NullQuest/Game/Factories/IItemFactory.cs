using NullQuest.Game.Items;

namespace NullQuest.Game.Factories
{
    public interface IItemFactory
    {
        IItem CreateItem(int itemLevel);
    }
}