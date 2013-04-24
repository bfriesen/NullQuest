using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Data;

namespace NullQuest.Game
{
    public static class GameWorld
    {
        private static readonly IDungeonNameGenerator dungeonNameGenerator;

        static GameWorld()
        {
            dungeonNameGenerator = new DeterministicDungeonNameGenerator();
            BossesDefeated = new List<string>();
        }

        public static int SaveGameId { get; set; }
        public static Player Player { get; set; }
        public static int CurrentDungeonLevel { get; set; }
        public static int NumberOfMonstersDefeatedInCurrentDungeonLevel { get; set; }
        public static int RequiredNumberOfMonstersInCurrentDungeonLevelBeforeBoss { get; set; }
        public static int TotalNumberOfMonstersDefeated { get; set; }
        public static IList<string> BossesDefeated { get; private set; }

        public static void SetRequiredNumberOfMonstersInCurrentDungeonLevelBeforeBoss(IDice dice)
        {
            NumberOfMonstersDefeatedInCurrentDungeonLevel = 0;
            RequiredNumberOfMonstersInCurrentDungeonLevelBeforeBoss = Player.Level + dice.Roll(1, 6);
        }

        public static string GetCurrentDungeonName()
        {
            return dungeonNameGenerator.GenerateName(CurrentDungeonLevel);
        }
    }
}
