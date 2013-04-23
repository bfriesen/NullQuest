using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Data
{
    public interface IAsciiArtRepository
    {
        string GetTitleArt();
        string GetTownArt();
        string GetDungeonArt(int dungeonLevel);
        string GetPlayerDeadArt(int dungeonLevel);
        string GetStoreArt();
        string GetInnArt();
        string GetInventoryArt();
        string GetSpellBookArt();
    }
}
