using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Data;

namespace NullQuest.Game
{
    public class GameWorld
    {
        private readonly IDungeonNameGenerator dungeonNameGenerator;

        public GameWorld(IDungeonNameGenerator dungeonNameGenerator)
        {
            this.dungeonNameGenerator = dungeonNameGenerator;
            BossesDefeated = new List<string>();
        }

        public int SaveGameId { get; set; }
        public Player Player { get; set; }
        public int CurrentDungeonLevel { get; set; }
        public int NumberOfMonstersDefeatedInCurrentDungeonLevel { get; set; }
        public int RequiredNumberOfMonstersInCurrentDungeonLevelBeforeBoss { get; set; }
        public int TotalNumberOfMonstersDefeated { get; set; }
        public IList<string> BossesDefeated { get; private set; }

        public void SetRequiredNumberOfMonstersInCurrentDungeonLevelBeforeBoss(IDice dice)
        {
            NumberOfMonstersDefeatedInCurrentDungeonLevel = 0;
            RequiredNumberOfMonstersInCurrentDungeonLevelBeforeBoss = Player.Level + dice.Roll(1, 6);
        }

        public string GetCurrentDungeonName()
        {
            return dungeonNameGenerator.GenerateName(CurrentDungeonLevel);
        }
    }
}
