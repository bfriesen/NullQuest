using NullQuest.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Data
{
    public interface ISaveGameRepository
    {
        IEnumerable<SaveGameData> GetSaveGameSlots();
        SaveGameData CreateGame(string characterName, int saveGamePositon, Race race, Class @class, CharacterStats stats);
        void SaveGame(SaveGameData saveGameData);
    }
}
