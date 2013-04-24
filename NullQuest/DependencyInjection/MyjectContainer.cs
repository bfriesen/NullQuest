using NullQuest.Game.Dungeon;
using NullQuest.Game.Inventory;
using NullQuest.Inn;
using NullQuest.MainMenu;
using NullQuest.SpellBook;
using NullQuest.Store;
using NullQuest.Town;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.DependencyInjection
{
    public class MyjectContainer : IjectContainer
    {
        public T Get<T>()
            where T : class
        {
            var type = typeof(T);
            object instance;

            if (type == typeof(GameLaunchedController))
            {
                instance = new GameLaunchedController();
            }
            else if (type == typeof(NewGameController))
            {
                instance = new NewGameController();
            }
            else if (type == typeof(LoadGameController))
            {
                instance = new LoadGameController();
            }
            else if (type == typeof(TownController))
            {
                instance = new TownController();
            }
            else if (type == typeof(InnController))
            {
                instance = new InnController();
            }
            else if (type == typeof(StoreController))
            {
                instance = new StoreController();
            }
            else if (type == typeof(DungeonController))
            {
                instance = new DungeonController();
            }
            else if (type == typeof(CombatController))
            {
                instance = new CombatController();
            }
            else if (type == typeof(InventoryController))
            {
                instance = new InventoryController();
            }
            else if (type == typeof(SpellBookController))
            {
                instance = new SpellBookController();
            }
            else
            {
                throw new InvalidOperationException("No implementation found for " + typeof(T).FullName);
            }

            return (T)instance;
        }
    }
}
