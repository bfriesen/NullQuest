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

        public static SaveGameData FromGameWorld()
        {
            var saveGameData = new SaveGameData
            {
                BossesDefeated = new List<string>(GameWorld.BossesDefeated),
                CharacterName = GameWorld.Player.Name,
                Class = GameWorld.Player.Class.Name,
                CurrentDungeonLevel = GameWorld.CurrentDungeonLevel,
                Id = GameWorld.SaveGameId,
                Level = GameWorld.Player.Level,
                Gold = GameWorld.Player.Gold,
                Experience = GameWorld.Player.Experience,
                Race = GameWorld.Player.Race.Name,
                Stats = GameWorld.Player.BaseStats,
                TotalNumberOfMonstersDefeated = GameWorld.TotalNumberOfMonstersDefeated,
                Weapon = GameWorld.Player.Weapon,
                Inventory = GameWorld.Player.Inventory.ToList(),
                SpellBook = GameWorld.Player.SpellBook.ToList()
            };

            return saveGameData;
        }

        public void LoadToGameWorld()
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
            GameWorld.Player = player;

            GameWorld.SaveGameId = Id;
            GameWorld.CurrentDungeonLevel = CurrentDungeonLevel;
            GameWorld.TotalNumberOfMonstersDefeated = TotalNumberOfMonstersDefeated;

            GameWorld.Player.ClearInventory();
            if (Inventory != null)
            {
                foreach (var item in Inventory)
                {
                    GameWorld.Player.AddItemToInventory(item);
                }
            }

            GameWorld.Player.ClearSpellBook();
            if (SpellBook != null)
            {
                foreach (var spell in SpellBook)
                {
                    GameWorld.Player.AddSpellToSpellBook(spell);
                }
            }

            GameWorld.BossesDefeated.Clear();
            if (BossesDefeated != null)
            {
                foreach (var boss in BossesDefeated)
                {
                    GameWorld.BossesDefeated.Add(boss);
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
