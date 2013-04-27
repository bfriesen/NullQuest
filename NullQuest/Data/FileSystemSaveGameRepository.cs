using NullQuest.Game;
using NullQuest.Game.Effects;
using NullQuest.Game.Items;
using NullQuest.Game.Spells;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XSerializer;

namespace NullQuest.Data
{
    public class FileSystemSaveGameRepository
    {
        private static readonly XmlSerializer<SaveGameData[]> _serializer;

        private static readonly string _path =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "NullQuest",
                "SaveGameData.xml");

        private readonly SaveGameData[] _savedGames;

        static FileSystemSaveGameRepository()
        {
            _serializer = new XmlSerializer<SaveGameData[]>(options => options.SetRootElementName("SavedGames").Indent(),
                typeof(ISpell).Assembly.GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface
                        && (typeof(IItem).IsAssignableFrom(t)
                            || typeof(ISpell).IsAssignableFrom(t)
                            || typeof(IEffect).IsAssignableFrom(t))).ToArray());
        }

        public FileSystemSaveGameRepository()
        {
            if (!File.Exists(_path))
            {
		Directory.CreateDirectory(Path.GetDirectoryName(_path));
                _savedGames = CreateEmptySaveGameData();
            }
            else
            {
                _savedGames = LoadSaveGameData();
            }
        }

        public IEnumerable<SaveGameData> GetSaveGameSlots()
        {
            return _savedGames;
        }

        public SaveGameData CreateGame(string characterName, int saveGamePositon, Race race, Class @class, CharacterStats stats)
        {
            var saveGameData = _savedGames.Single(x => x.Id == saveGamePositon);
            
            saveGameData.Clear();            
            saveGameData.CharacterName = characterName;
            saveGameData.Race = race.Name;
            saveGameData.Class = @class.Name;
            saveGameData.Stats = stats;
            saveGameData.Level = 1;
            saveGameData.CurrentDungeonLevel = 1;

            Persist(_savedGames);

            return saveGameData;
        }

        public void SaveGame(SaveGameData saveGameData)
        {
            var index = _savedGames.Select((x, i) => new { SavedGameData = x, Index = i }).First(x => x.SavedGameData.Id == saveGameData.Id).Index;
            _savedGames[index] = saveGameData;
            Persist(_savedGames);
        }

        private SaveGameData[] CreateEmptySaveGameData()
        {
            var savedGames = new[]
            {
                SaveGameData.Empty(1),
                SaveGameData.Empty(2),
                SaveGameData.Empty(3),
            };

            Persist(savedGames);

            return savedGames;
        }

        private SaveGameData[] LoadSaveGameData()
        {
            using (var reader = new StreamReader(_path, Encoding.UTF8))
            {
                return _serializer.Deserialize(reader);
            }
        }

        private void Persist(SaveGameData[] savedGames)
        {
            using (var writer = new StreamWriter(_path, false, Encoding.UTF8))
            {
                _serializer.Serialize(writer, savedGames);
            }
        }
    }
}
