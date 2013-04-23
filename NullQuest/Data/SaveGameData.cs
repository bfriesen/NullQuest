using NullQuest.Game;
using NullQuest.Game.Combat;
using NullQuest.Game.Items;
using NullQuest.Game.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NullQuest.Data
{
    public class SaveGameData
    {
        public SaveGameData()
        {
            Level = 1;
            CurrentDungeonLevel = 1;
        }

        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute]
        public string CharacterName { get; set; }

        public string Race { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }
        public CharacterStats Stats { get; set; }
        public Weapon Weapon { get; set; }

        [XmlArrayItem("Item")]
        public List<IItem> Inventory { get; set; }

        [XmlArrayItem("Spell")]
        public List<ISpell> SpellBook { get; set; }

        public int CurrentDungeonLevel { get; set; }
        public int TotalNumberOfMonstersDefeated { get; set; }

        [XmlArrayItem("Boss")]
        public List<string> BossesDefeated { get; set; }

        public string Title
        {
            get { return CharacterName ?? "Empty"; }
        }

        public bool IsEmpty
        {
            get { return CharacterName == null; }
        }

        public void Clear()
        {
            CharacterName = null;
            Race = null;
            Class = null;
            Level = 0;
            Gold = 0;
            Experience = 0;
            Stats = null;
            Weapon = null;
            Inventory = null;
            SpellBook = null;
            CurrentDungeonLevel = 0;
            TotalNumberOfMonstersDefeated = 0;
            BossesDefeated = null;
        }

        public static SaveGameData Empty(int id)
        {
            return new SaveGameData
            {
                Id = id
            };
        }

        public static SaveGameData FromGameWorld(GameWorld gameWorld)
        {
            var saveGameData = new SaveGameData
            {
                BossesDefeated = new List<string>(gameWorld.BossesDefeated),
                CharacterName = gameWorld.Player.Name,
                Class = gameWorld.Player.Class.Name,
                CurrentDungeonLevel = gameWorld.CurrentDungeonLevel,
                Id = gameWorld.SaveGameId,
                Level = gameWorld.Player.Level,
                Gold = gameWorld.Player.Gold,
                Experience = gameWorld.Player.Experience,
                Race = gameWorld.Player.Race.Name,
                Stats = gameWorld.Player.BaseStats,
                TotalNumberOfMonstersDefeated = gameWorld.TotalNumberOfMonstersDefeated,
                Weapon = gameWorld.Player.Weapon,
                Inventory = gameWorld.Player.Inventory.ToList(),
                SpellBook = gameWorld.Player.SpellBook.ToList()
            };

            return saveGameData;
        }

        public void LoadToGameWorld(GameWorld gameWorld)
        {
            var player = new Player
            {
                BaseStats = Stats,
                Class = Game.Class.FromName(Class),
                Level = Level,
                Gold = Gold,
                Experience = Experience,
                Name = CharacterName,
                Race = Game.Race.FromName(Race),
                Weapon = Weapon
            };

            player.CurrentHitPoints = player.MaxHitPoints;
            player.CurrentEnergy = player.MaxEnergy;
            gameWorld.Player = player;

            gameWorld.SaveGameId = Id;
            gameWorld.CurrentDungeonLevel = CurrentDungeonLevel;
            gameWorld.TotalNumberOfMonstersDefeated = TotalNumberOfMonstersDefeated;

            gameWorld.Player.ClearInventory();
            if (Inventory != null)
            {
                foreach (var item in Inventory)
                {
                    gameWorld.Player.AddItemToInventory(item);
                }
            }

            gameWorld.Player.ClearSpellBook();
            if (SpellBook != null)
            {
                foreach (var spell in SpellBook)
                {
                    gameWorld.Player.AddSpellToSpellBook(spell);
                }
            }

            gameWorld.BossesDefeated.Clear();
            if (BossesDefeated != null)
            {
                foreach (var boss in BossesDefeated)
                {
                    gameWorld.BossesDefeated.Add(boss);
                }
            }
        }

        public bool ShouldSerializeLevel()
        {
            return !IsEmpty;
        }

        public bool ShouldSerializeGold()
        {
            return !IsEmpty;
        }

        public bool ShouldSerializeExperience()
        {
            return !IsEmpty;
        }

        public bool ShouldSerializeCurrentDungeonLevel()
        {
            return !IsEmpty;
        }

        public bool ShouldSerializeTotalNumberOfMonstersDefeated()
        {
            return !IsEmpty;
        }
    }
}
